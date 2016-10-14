using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutoService.ViewModels.Application
{
    public class ApplicationEdit : AutoService.DAL.Models.Application
    {
        [Required]
        [Display(Name="Время")]
        public string Time { get; set; }

        public void Copy(DAL.Models.Application destination)
        {
            destination.Status = Status;
            destination.CarModel = CarModel;
            destination.CarNumber = CarNumber;
            destination.CreatedAt = CreatedAt;
            destination.CreatedBy = CreatedBy;
            destination.RequestType = RequestType;
            destination.Date = new DateTime(Date.Year, Date.Month, Date.Day);
            destination.IsApproved = IsApproved;
            destination.Note = Note;

            DateTime? time = StringTimeToDateTime(Time);
            if (time.HasValue)
            {
                Date = Date.AddHours(time.Value.Hour);
                Date = Date.AddMinutes(time.Value.Minute);
            }
            destination.Date = Date;
        }

        /// <summary>
        /// Преобразует строковое представление времени (чч:мм)
        /// в DateTime
        /// </summary>
        private DateTime? StringTimeToDateTime(string time)
        {
            var t = time.Split(':');
            DateTime? datetime = null;

            if (t[0] != null && t[1] != null)
            {
                try
                {
                    var hh = Convert.ToInt32(t[0]);
                    var mm = Convert.ToInt32(t[1]);
                    datetime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, hh, mm, 0);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("[StringTimeToDateTime]: Ошибка при конвертировании времени");
                }
            }

            return datetime;
        }
    }
}