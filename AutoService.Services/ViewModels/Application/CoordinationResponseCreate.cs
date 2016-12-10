using AutoService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService.Services.ViewModels
{
    public class CoordinationResponseCreate : CoordinationResponse
    {
        public int CoordinationRequestId { get; set; }
    }
}
