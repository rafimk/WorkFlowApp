using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Work_Flow_App.Helpers
{
    public static class FileUploadValidator
    {
        public static string ErrorMessage { get; set; }
        public static decimal filesize { get; set; } = 2 * 1024;
        public static string IsValidFile(IFormFile file)
        {
            try
            {
                var supportedTypes = new[] { "pdf"};
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt))
                {
                    ErrorMessage = "File Extension Is InValid - Only Upload PDF File";
                    return ErrorMessage;
                }
                else if (file.Length > (filesize * 1024))
                {
                    ErrorMessage = $"File size Should Be UpTo {filesize} MB";
                    return ErrorMessage;
                }
                else
                {
                    ErrorMessage = "Success";
                    return ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Upload Container Should Not Be Empty or Contact Admin";
                return ErrorMessage;
            }
        }
    }
}
