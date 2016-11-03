using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService.DAL.Models
{
    public partial class User
    {
        public string GetFullName() => $"{FirstName} {LastName}";
    }
}
