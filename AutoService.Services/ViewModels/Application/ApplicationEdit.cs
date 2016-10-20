using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutoService.Services.ViewModels
{
    public class ApplicationEdit : AutoService.DAL.Models.Application
    {
        [Required]
        [Display(Name="Время")]
        public string Time { get; set; }
        public new DateTime? Date { get; set; }

        public void Copy(DAL.Models.Application destination)
        {
            destination.Status = Status;
            destination.CarModel = CarModel;
            destination.CarNumber = CarNumber;
            destination.CreatedAt = CreatedAt;
            destination.CreatedBy = CreatedBy;
            destination.RequestType = RequestType;
            if(this.Date.HasValue)
                destination.Date = new DateTime(Date.Value.Year, Date.Value.Month, Date.Value.Day);
            destination.IsApproved = IsApproved;
            destination.Note = Note;

            DateTime? time = StringTimeToDateTime(Time);
            if (time.HasValue && this.Date.HasValue)
            {
                this.Date = this.Date.Value.AddHours(time.Value.Hour);
                this.Date = this.Date.Value.AddMinutes(time.Value.Minute);
            }
            destination.Date = Date ?? DateTime.MinValue;
        }

        /// <summary>
        /// Преобразует строковое представление времени (чч:мм)
        /// в DateTime
        /// </summary>
        public static DateTime? StringTimeToDateTime(string time)
        {
            var t = time.Split(':');
            DateTime? datetime = DateTime.MinValue;
            int? hh;
            int? mm;
            if (t.Length == 2 && t[0] != null && t[1] != null)
            {
                try
                {
                    hh = Convert.ToInt32(t[0]);
                    mm = Convert.ToInt32(t[1]);

                    if (hh > -1 && hh < 24 && mm > -1 && mm < 60)
                    {
                        datetime = datetime.Value.AddHours(hh.Value);
                        datetime = datetime.Value.AddMinutes(mm.Value);
                    }
                    else
                    {
                        datetime = null;
                    }
                }
                catch (Exception ex)
                {
                    datetime = null;
                }
            }
            else
            {
                datetime = null;
            }

            return datetime;
        }
    }
}