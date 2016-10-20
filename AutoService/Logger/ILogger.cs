using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService.Logger
{
    public interface ILogger
    {
        void Info(string log);
        void Debug(string log);
        void Error(string log, Exception exception = null);
    }
}
