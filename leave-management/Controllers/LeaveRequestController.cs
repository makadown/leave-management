using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace leave_management.Controllers
{
    [Authorize]
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveRequestRepository _leaveRequestRepo;
        private readonly ILeaveTypeRepository _leaveRepo;
        private readonly ILeaveAllocationRepository _leaveAllocationRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;

        public LeaveRequestController(ILeaveRequestRepository leaveRequestRepo,
            ILeaveAllocationRepository leaveAllocationRepo,
            ILeaveTypeRepository leaveRepo, IMapper mapper,
            UserManager<Employee> userManager)
        {
            _leaveRequestRepo = leaveRequestRepo;
            _leaveAllocationRepo = leaveAllocationRepo;
            _leaveRepo = leaveRepo;
            _mapper = mapper;
            _userManager = userManager;
        }

        [Authorize(Roles ="Administrator")]
        // GET: LeaveRequestController
        public ActionResult Index()
        {
            var leaveRequests = _leaveRequestRepo.FindAll();
            var leaveRequestsModel = _mapper.Map<List<LeaveRequestViewModel>>(leaveRequests);
            var model = new AdminLeaveRequestViewVM
            {
                TotalRequests = leaveRequestsModel.Count,
                ApprovedRequests = leaveRequestsModel.Count(q => q.Approved == true),
                PendingRequests = leaveRequestsModel.Count(q => q.Approved == null),
                RejectedRequests = leaveRequestsModel.Count(q => q.Approved == false),
                LeaveRequests = leaveRequestsModel
            };
            return View(model);
        }

        // GET: LeaveRequestController/Details/5
        public ActionResult Details(int id)
        {
            var leaveRequest = _leaveRequestRepo.FindById(id);
            var model = _mapper.Map<LeaveRequestViewModel>(leaveRequest);
            return View(model);
        }

        // GET: LeaveRequestController/Create
        public ActionResult Create()
        {
            var leaveTypes = _leaveRepo.FindAll();
            var leaveTypeItems = leaveTypes
                    .Select(q => new SelectListItem {
                        Text = q.Name,
                        Value = q.Id.ToString()
                    });
            var model = new CreateLeaveRequestVM() {
                LeaveTypes = leaveTypeItems
            };
            return View(model);
        }

        // POST: LeaveRequestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateLeaveRequestVM model)
        {
            var leaveTypes = _leaveRepo.FindAll();
            var leaveTypeItems = leaveTypes
                    .Select(q => new SelectListItem
                    {
                        Text = q.Name,
                        Value = q.Id.ToString()
                    });
            model.LeaveTypes = leaveTypeItems;
            try
            {
                var startDate = Convert.ToDateTime(model.StartDate);
                var endDate = Convert.ToDateTime(model.EndDate);

                if (!ModelState.IsValid) {
                    return View(model);
                }

                if (DateTime.Compare(startDate, endDate) > 1) {
                    ModelState.AddModelError("", "Fecha de inicio no puede ser mayor que la fecha final");
                    return View(model);
                }

                var employee = _userManager.GetUserAsync(User).Result;
                var allocation = _leaveAllocationRepo.GetLeaveAllocationByEmployeeAndType(employee.Id, model.LeaveTypeId);
                int daysRequested = (int)(startDate.Date - endDate.Date).TotalDays;
                if (daysRequested > allocation.NumberOfDays) {
                    ModelState.AddModelError("", "Los dias pedidos superan el limite de dias permitidos");
                    return View(model);
                }

                var leaveRequestModel = new LeaveRequestViewModel {
                    RequestingEmployeeId = employee.Id,
                    LeaveTypeId = model.LeaveTypeId,
                    StartDate = startDate,
                    EndDate = endDate,
                    Approved = null,
                    DateRequested = DateTime.Now,
                    DateActioned = DateTime.Now
                };
                var leaveRequest = _mapper.Map<LeaveRequest>(leaveRequestModel);

                if (!_leaveRequestRepo.Create(leaveRequest)) {
                    ModelState.AddModelError("", "Algo falló al crear registro");
                    return View(model);
                }

                return RedirectToAction(nameof(Index), "Home");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "Something went wrong"); 
                return View(model);
            }
        }

        // GET: LeaveRequestController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeaveRequestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ApproveRequest(int id)
        {
            var employee = _userManager.GetUserAsync(User).Result;
            var leaveRequest = _leaveRequestRepo.FindById(id);
            var allocation = _leaveAllocationRepo
                    .GetLeaveAllocationByEmployeeAndType(leaveRequest.RequestingEmployeeId,
                        leaveRequest.LeaveTypeId);

            int daysRequested = (int)(leaveRequest.EndDate.Date - leaveRequest.StartDate.Date).TotalDays;
            allocation.NumberOfDays -= daysRequested;

            leaveRequest.Approved = true;
            leaveRequest.ApprovedById = employee.Id;
            leaveRequest.DateActioned = DateTime.Now;

            _leaveAllocationRepo.Update(allocation);
            _leaveRequestRepo.Update(leaveRequest);
            _leaveRequestRepo.Save();
            _leaveAllocationRepo.Save();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult RejectRequest(int id)
        {
            var employee = _userManager.GetUserAsync(User).Result;
            var leaveRequest = _leaveRequestRepo.FindById(id);
            leaveRequest.Approved = false;
            leaveRequest.ApprovedById = employee.Id;
            leaveRequest.DateActioned = DateTime.Now;

            _leaveRequestRepo.Update(leaveRequest);
            _leaveRequestRepo.Save();
            return RedirectToAction(nameof(Index));
        }
        

        // GET: LeaveRequestController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveRequestController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
