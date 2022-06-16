using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work_Flow_App.Models;
using Work_Flow_App.Domain;
using Work_Flow_App.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Work_Flow_App.Infrastructure.Interfaces;
using AutoMapper;
using Work_Flow_App.Dtos;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Work_Flow_App.Helpers;

namespace Work_Flow_App.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        private readonly IRequestService _requestService;
        private readonly IAttachmentService _attachmentService;
        private readonly IRequestWorkFlowService _requestWorkFlowService;
        private readonly ICurrentUserService _currentUser;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public RequestController(IRequestService requestService,
                                 IAttachmentService attachmentService,
                                 IRequestWorkFlowService requestWorkFlowService,
                                 ICurrentUserService currentUser,
                                 IIdentityService identityService,
                                 IMapper mapper,
                                 IWebHostEnvironment env)
        {
            _requestService = requestService;
            _attachmentService = attachmentService;
            _requestWorkFlowService = requestWorkFlowService;
            _currentUser = currentUser;
            _identityService = identityService;
            _mapper = mapper;
            _env = env;
        }

    
        public async Task<IActionResult> Index()
        {
            var currentUser = _currentUser.GetId();
            var user = await _identityService.GetByIdAsync(currentUser);

            if (user == null)
            {
                var newRequestViewModel = new RequestIndexModel
                {
                    IsAdmin = false,
                    Requests = new List<RequestDto>()
                };
                return View(newRequestViewModel);
            }

            List<RequestDto> requests = new List<RequestDto>();

            if (!user.IsAdmin)
            {
                // User - Show only user requests
                var result = await _requestService.GetByUserIdAsync(currentUser);
                if (result != null)
                {
                    requests = _mapper.Map<List<RequestDto>>(result);
                }
            }
            else
            {
                // Admin - Show only pending/rejected/approved requests
                var result = await _requestService.GetForAdminAsync();
                if (result != null)
                {
                    requests = _mapper.Map<List<RequestDto>>(result);
                }
            }


            var requestViewModel = new RequestIndexModel
            {
                IsAdmin = user.IsAdmin,
                Requests = requests
            };
            return View(requestViewModel);
        }

        public async Task<IActionResult> AddOrEdit(int? id)
        {
            if (id == null)
            {
                var newRequestAddOrEditModel = new RequestAddOrEditModel
                {
                    Request = new RequestDto()
                };
                newRequestAddOrEditModel.Request.Attachments = new List<AttachmentDto>();
                return View(newRequestAddOrEditModel);
            }
            //update
            var request = await _requestService.GetByIdAsync(id ?? 0);
            if (request == null)
            {
                return NotFound();
            }
            var requestDto = _mapper.Map<RequestDto>(request);
            var requestAddOrEditModel = new RequestAddOrEditModel
            {
                Request = requestDto,
            };
            return View(requestAddOrEditModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(RequestAddOrEditModel model)
        {
            var fileValidation = UploadFileValidate(model.FileToUpload);
            var currentUser = _currentUser.GetId();
            if (ModelState.IsValid && fileValidation == "Success")
            {
                var requestId = 0;
                var request = _mapper.Map<Request>(model.Request);
                if (model.Request.Id == 0)
                {
                    // Create as with Draft status.
                    requestId = await _requestService.CreateAsync(request);
                    await _requestWorkFlowService.CreatedAsync(requestId, currentUser);
                }
                else
                {
                    requestId = model.Request.Id;
                    var requestFromDb = await _requestService.GetByIdAsync(requestId);
                    if (requestFromDb == null)
                    {
                        return NotFound();
                    }

                    requestFromDb.Title = request.Title;
                    requestFromDb.Details = request.Details;
                    requestFromDb.Notes = request.Notes;
                    
                    await _requestService.UpdateAsync(requestFromDb);
                    await _requestWorkFlowService.ModifiedAsync(requestId, currentUser);
                }

                if (requestId > 0 && model.FileToUpload != null)
                {
                    await UploadFiles(model.FileToUpload, requestId, currentUser);
                }
                

                return RedirectToAction("Index");
            }
            ViewBag.Message = fileValidation;
            return View(model);
        }

        public async Task<IActionResult> ViewOrApprove(int id)
        {
            var currentUser = _currentUser.GetId();
            var user = await _identityService.GetByIdAsync(currentUser);
            var request = await _requestService.GetByIdAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            var requestDto = _mapper.Map<RequestDto>(request);
            var requestAddOrEditModel = new RequestViewOrApproveModel
            {
                IsAdmin = user.IsAdmin,
                Request = requestDto,
                Reason = ""
            };
            return View(requestAddOrEditModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ViewOrApprove(RequestViewOrApproveModel model, string submitButton)
        {
            var currentUser = _currentUser.GetId();

            var user = await _identityService.GetByIdAsync(currentUser);
            var requestId = model.Request.Id;

            if (user == null)
            {
                return NotFound();
            }

            var action = "";
            if (user.IsAdmin == false)
            {
                action = "submitted";
            }
            else
            {
                if (submitButton != null)
                {
                    action = submitButton;
                } 
            }

            var updateStatus = await _requestService.ChanveApprvalStatusAsync(requestId, action);

            if (updateStatus)
            {
                if (action == "submitted")
                {
                    await _requestWorkFlowService.SendForApprovalAsync(requestId, currentUser, model.Reason);
                }
                else if (action == "approved" || action == "rejected" || action == "returned")
                {
                    await _requestWorkFlowService.AdminActionAsync(requestId, currentUser, model.Reason, action);
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ViewHistory(int id)
        {
            var requestWorkFlows = await _requestWorkFlowService.GetByRequestIdAsync(id);

            if (requestWorkFlows == null)
            {
                var newViewHistoryModel = new ViewHistoryModel
                {
                    RequestWorkFlow = new List<RequestWorkFlowDto>()
                };
                return View(newViewHistoryModel);
            }
            var requestWorkFlowDtos = _mapper.Map<List<RequestWorkFlowDto>>(requestWorkFlows);
            var viewHistoryModel = new ViewHistoryModel
            {
                RequestWorkFlow = requestWorkFlowDtos
            };
            return View(viewHistoryModel);
        }

        private string UploadFileValidate(List<IFormFile> files)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    var result = FileUploadValidator.IsValidFile(file);
                    if (result != "Success")
                    {
                        return result;
                    }
                }
            }
            return "Success";
        }

        private async Task<List<AttachmentDto>> UploadFiles(List<IFormFile> files, int requestId, int currentUser)
        {
            var webRootPath = _env.WebRootPath;
            var fileUploadPath = Path.Combine(webRootPath + "\\Upload\\");
            bool basePathExists = System.IO.Directory.Exists(fileUploadPath);
            if (!basePathExists) Directory.CreateDirectory(fileUploadPath);

            List<AttachmentDto> attachments = new List<AttachmentDto>();

            foreach (var file in files)
            {

                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var guid = Guid.NewGuid();
                var filePath = Path.Combine(fileUploadPath, $"___{guid}___{file.FileName}"); 
                var extension = Path.GetExtension(file.FileName);
                if (!System.IO.File.Exists(filePath))
                {
                    if (file.Length > 0)
                    {
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                    }
                    var attachment = new Attachment
                    {
                        RequestId = requestId,
                        CreatedOn = DateTime.UtcNow,
                        UploadedBy = currentUser,
                        FileType = file.ContentType,
                        Extension = extension,
                        Name = fileName,
                        Description = "",
                      };
                    await _attachmentService.CreateAsync(attachment);
                    var attachmentDto = _mapper.Map<AttachmentDto>(attachment);
                    attachments.Add(attachmentDto);
                }
            }
            return attachments;
        }
    }
}
