using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Work_Flow_App.Dtos;

namespace Work_Flow_App.Models
{
    public class RequestViewOrApproveModel
    {
        public RequestDto Request { get; set; }
        public bool IsAdmin { get; set; }

        [Required(ErrorMessage = "Reason required for Reject/Return")]
        public string Reason { get; set; }
    }
}
