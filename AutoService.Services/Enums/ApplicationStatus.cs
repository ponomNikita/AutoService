using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AutoService.Services.Enums
{
    public enum ApplicationStatus
    {
        [Description("Ожидает подтверждения")]
        WaitForApprove = 0,
    }
}
