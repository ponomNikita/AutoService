using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AutoService.DAL.Models
{
    [Table("CoordinationRequest", Schema = "app")]
    public class CoordinationRequest : TEntity
    {
        public new int id { get; set; }

        public string SourceUser { get; set; }

        public string DistanationUser { get; set; }

        public int Type { get; set; }

        public string Message { get; set; }

        public int ApplicationId { get; set; }

        public int CoordinationRequestId { get; set; }

        public virtual Application Application { get; set; }

        public CoordinationResponse CoordinationResponse { get; set; }


    }
}
