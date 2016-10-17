using System;
using System.Collections.Generic;
using AutoService.DAL.Models;
using AutoService.Services.Enums;
using AutoService.DAL.FilterModel;

namespace AutoService.Services.Interfaces
{
    public interface IApplicationService
    {
        bool IsFreeTime(DateTime dateTime);
        void ChangeStatus(int id, ApplicationStatus status);
        void CoordinationRequest(int ticketId, ApplicationsActionType actionType, string description);
        void CoordinationResponse(int ticketId);
        Application GetById(int id);
        IEnumerable<Application> GetAll();
        IEnumerable<Application> GetFiltered(ApplicationFilter filter);

    }
}
