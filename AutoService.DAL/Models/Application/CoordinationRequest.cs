﻿using System.ComponentModel.DataAnnotations.Schema;

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

        public int? CoordinationResponseId { get; set; }

        public virtual Application Application { get; set; }

        public virtual CoordinationResponse CoordinationResponse { get; set; }

    }
}
