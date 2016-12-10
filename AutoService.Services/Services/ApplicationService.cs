using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoService.DAL;
using AutoService.DAL.Models;
using AutoService.Infrastructure.Logger;
using AutoService.Services.Interfaces;
using AutoService.Services.Enums;
using AutoService.Services;
using AutoService.Services.Services;
using AutoService.Services.ViewModels;

namespace AutoService.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IRepository<Application> repository;
        private readonly IDateTimeProvider timeProvider;
        private IPermissionService permissionService;
        private User currentUser;
        private ILogger Logger;

        public ApplicationService(IRepository<Application> _repository, IDateTimeProvider _timeProvider, User _currentUser)
        {
            repository = _repository;
            timeProvider = _timeProvider;
            currentUser = _currentUser;
            permissionService = ServicesFactory.CreatePermissionService();
            Logger = new Logger();
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

        public void Delete(int id)
        {
            repository.Delete(id);
            repository.Save();
        }

        public void Approve(int id)
        {
            var application = GetById(id);
            application.IsApproved = true;
            application.Status = application.RequestType == (int)RequestTypes.Diagnostic ? 
                (int)ApplicationStatus.WaitForDiagnostic : (int)ApplicationStatus.WaitForReparing;

            repository.Save();
        }


        public Application GetById(int id)
        {
            Application application = repository.Get(id);
            return application;
        }

        public IEnumerable<Application> GetAll()
        {
            return repository.GetAll().AsEnumerable();
        }

        public IEnumerable<Application> GetFiltered(AutoService.DAL.FilterModel.ApplicationFilter filter)
        {
            var applications = repository.GetAll();

            if (filter != null && applications != null)
            {
                if (filter.Status.HasValue)
                {
                    applications = applications.Where(t => t.Status == filter.Status);
                }

                if (filter.RequestType.HasValue)
                {
                    applications = applications.Where(t => t.RequestType == filter.RequestType.Value);
                }

                if (!string.IsNullOrWhiteSpace(filter.CarModel))
                {
                    applications = applications.Where(t => t.CarModel.ToLower().Contains(filter.CarModel.ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(filter.CarNumber))
                {
                    applications = applications.Where(t => t.CarNumber.ToLower().Contains(filter.CarNumber.ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(filter.CreatedBy) 
                    && (filter.CreatedBy == currentUser.Login || permissionService.HasRole((int)Roles.Admin, currentUser.Login)))
                {
                    applications = applications.Where(t => t.CreatedBy.ToLower().Contains(filter.CreatedBy.ToLower()));
                }

                if (filter.Date.HasValue)
                {
                    applications = applications.Where(t => t.Date == filter.Date.Value);
                }

                if (filter.CreatedAt.HasValue)
                {
                    applications = applications.Where(t => t.CreatedAt == filter.CreatedAt.Value);
                }
            }

            return applications.AsEnumerable();
        }

        public string Create(ApplicationEdit model)
        {
            Application newItem = new Application();
            model.Copy(newItem);
            newItem.CreatedAt = timeProvider.Now;
            newItem.CreatedBy = currentUser != null ? currentUser.Login : string.Empty;

            if (!IsFreeTime(newItem.Date))
            {
                return "К сожалению это время занято. Пожалуйста, выберете другую дату или время";
            }

            Logger.Info("Запись в бд новой заявки...");
            repository.Create(newItem);
            repository.Save();
            Logger.Info("Успешно!");
            
            return string.Empty;
        }

        public string Edit(ref ApplicationEdit model)
        {
            Application item = GetById(model.id);
            DateTime dateTime = item.Date;
            model.Copy(item);

            if (dateTime != model.Date)
            {
                if (!IsFreeTime(item.Date))
                {
                    model = new ApplicationEdit(item);
                    return "К сожалению это время занято. Пожалуйста, выберете другую дату или время";
                }
            }

            Logger.Info("Запись в бд новой заявки...");
            repository.Update(item);
            repository.Save();
            Logger.Info("Успешно!");

            model = new ApplicationEdit(item);
            return string.Empty;
        }

        public string CreateCoordinationRequest(CoordinationRequest model)
        {
            IRepository<CoordinationRequest> requestRepository = new Repository<CoordinationRequest>(DBContext.GetDBContext());
            Logger.Info("Запись в бд новой заявки...");
            requestRepository.Create(model);
            requestRepository.Save();
            Logger.Info("Успешно!");

            return string.Empty;
        }

        public string CreateCoordinationResponse(CoordinationResponseCreate model)
        {
            IRepository<CoordinationRequest> requestRepository = new Repository<CoordinationRequest>(DBContext.GetDBContext());
            IRepository<CoordinationResponse> responceRepository = new Repository<CoordinationResponse>(DBContext.GetDBContext());
            CoordinationResponse response = new CoordinationResponse();

            response.IsAgree = model.IsAgree;
            response.Message = model.Message;

            Logger.Info("Запись в бд новой заявки...");
            responceRepository.Create(response);
            responceRepository.Save();

            var newResponse = responceRepository.GetAll().OrderByDescending(t => t.id).FirstOrDefault();
            var request = requestRepository.Get(model.CoordinationRequestId);
            request.CoordinationResponseId = newResponse.id;

            requestRepository.Update(request);
            requestRepository.Save();
            Logger.Info("Успешно!");

            return string.Empty;
        }
    }
}
