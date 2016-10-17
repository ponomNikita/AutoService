using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.DAL;
using AutoService.DAL.Models;
using AutoService.Services.Interfaces;
using AutoService.Services.Enums;
using AutoService.Services;
using AutoService.Services.Services;

namespace AutoService.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IRepository<Application> repository;
        private readonly IDateTimeProvider timeProvider;

        public ApplicationService(IRepository<Application> repo, IDateTimeProvider dateTimeProvider)
        {
            repository = repo;
            timeProvider = dateTimeProvider;
            timeProvider = dateTimeProvider;
        }

        public bool IsFreeTime(DateTime dateTime)
        {
            if (dateTime < timeProvider.Now)
                return false;

            return !repository.GetAll().Any(app => app.Date.Equals(dateTime));
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
