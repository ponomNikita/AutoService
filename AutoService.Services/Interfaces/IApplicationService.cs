using System;
using AutoService.Services.Enums;

namespace AutoService.Services.Interfaces
{
    public interface IApplicationService
    {
        bool IsFreeTime(DateTime dateTime);
        void ChangeStatus(int id, ApplicationStatus status);
        void CoordinationRequest(int ticketId, ApplicationsActionType actionType, string description);
        void CoordinationResponse(int ticketId);
    }
}
