using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work_Flow_App.Dtos;

namespace Work_Flow_App.Models
{
    public class RequestIndexModel
    {
        public bool IsAdmin { get; set; }
        public List<RequestDto> Requests { get; set; }
    }
}
