using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AutoService.Services.Enums
{
    public enum ECoordinationRequestType
    {
        [Description("Согласование времени")]
        CoordinateTime = 1,

        [Description("Согласование цены")]
        CoordinatePrice = 2,

        [Description("Другое")]
        OtherCoordination = 3


    }
}
