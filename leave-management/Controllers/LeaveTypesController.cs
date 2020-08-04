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
using Microsoft.AspNetCore.Mvc;

namespace leave_management.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class LeaveTypesController : Controller
    {
        private readonly ILeaveTypeRepository _repo;
        private readonly IMapper _mapper;

        public LeaveTypesController(ILeaveTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// GET: LeaveTypesController
        /// Abre pagina Index para Tipos de Licencia
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var leavetypes = _repo.FindAll().ToList();
            var model = _mapper.Map<List<LeaveType>, List<LeaveTypeViewModel>>(leavetypes);
            return View(model);
        }

        // GET: LeaveTypesController/Details/5
        public ActionResult Details(int id)
        {
            if (!_repo.Exists(id))
            {
                return NotFound();
            }
            var leaveTypeVM = _mapper.Map<LeaveTypeViewModel>(_repo.FindById(id));
            return View(leaveTypeVM);
        }

        /// <summary>
        /// GET: LeaveTypesController/Create
        /// Abre página con formulario para crear tipo de licencia
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: LeaveTypesController/Create
        /// Recibe info enviada desde formulario de la página para crear tipo de licencia
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LeaveTypeViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) {
                    return View(model);
                }
                var leaveType = _mapper.Map<LeaveType>(model);
                leaveType.DateCreated = DateTime.Now;
                if (!_repo.Create(leaveType))
                {
                    ModelState.AddModelError("", "Algo no fué bien");
                    return View(model);
                }
                _repo.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Algo no fué bien");
                return View(model);
            }
        }

        /// <summary>
        /// GET: LeaveTypesController/Edit/5
        /// Envía a pagina de edición 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            if (!_repo.Exists(id)) {
                return NotFound();
            }
            var leaveTypeVM = _mapper.Map<LeaveTypeViewModel>( _repo.FindById(id) );
            return View(leaveTypeVM);
        }

        /// <summary>
        /// POST: LeaveTypesController/Edit/5
        /// Recibe el formulario de la pagina de edición para proceder a guardar la info
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LeaveTypeViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var leaveType = _mapper.Map<LeaveType>(model);
                if (!_repo.Update(leaveType))
                {
                    ModelState.AddModelError("", "Algo no fué bien al guardar");
                    return View(model);
                }
                _repo.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Algo no fué bien al guardar");
                return View(model);
            }
        }

        // GET: LeaveTypesController/Delete/5
        public ActionResult Delete(int id)
        {
            /* if (!_repo.Exists(id))
             {
                 return NotFound();
             }
             var leaveTypeVM = _mapper.Map<LeaveTypeViewModel>(_repo.FindById(id));
             return View(leaveTypeVM);*/
            var leaveType = _repo.FindById(id);
            if (leaveType == null)
            {
                return NotFound();
            }
            if (!_repo.Delete(leaveType))
            {
                ModelState.AddModelError("", "Algo no fué bien al eliminar");
                return BadRequest();
            }
            _repo.Save();
            return RedirectToAction(nameof(Index));
        }

        // POST: LeaveTypesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, LeaveTypeViewModel model)
        {
            try
            {
                var leaveType = _repo.FindById(id);
                if (leaveType == null) {
                    return NotFound();
                }
                if (!_repo.Delete(leaveType))
                {
                    ModelState.AddModelError("", "Algo no fué bien al eliminar");
                    return View(model);
                }
                _repo.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Algo no fué bien al eliminar");
                return View(model);
            }
        }
    }
}
