using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.DAL;
using AutoService.Services.Interfaces;
using AutoService.Services.Enums;

namespace AutoService.Services
{
    public class ApplicationService : IApplicationService
    {
        private UnitOfWork uow;

        public ApplicationService()
        {
            uow = new UnitOfWork();
        }

        public bool IsFreeTime(DateTime dateTime)
        {
            if (dateTime < DateTime.Now)
                return false;

            return !uow.Applications.Any(app => app.Date.Equals(dateTime));
        }

        public void ChangeStatus(int id, ApplicationStatus status)
        {
            throw new NotImplementedException();
        }

        public void CoordinationRequest(int ticketIdid, ApplicationsActionType actionType, string description)
        {
            throw new NotImplementedException();
        }

        public void CoordinationResponse(int ticketId)
        {
            throw new NotImplementedException();
        }
    }
}
