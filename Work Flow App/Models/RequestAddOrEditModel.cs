using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work_Flow_App.Dtos;

namespace Work_Flow_App.Models
{
    public class RequestAddOrEditModel
    {
        public RequestDto Request { get; set; }
        public List<IFormFile> FileToUpload { get; set; }
    }
}
