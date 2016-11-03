using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutoService.Services.Enums
{
    public enum RequestTypes
    {
        [Description("Диагностика")]
        Diagnostic = 0,

        [Description("Кузовной ремонт")]
        BodyRepair = 1,

        [Description("Шиномонтаж")]
        Tire = 2,

        [Description("Слесарный ремонт")]
        BenchRepair = 3
    }
}