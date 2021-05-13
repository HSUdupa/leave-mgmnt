using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using leave_mgmnt.Contracts;
using leave_mgmnt.Data;
using leave_mgmnt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace leave_mgmnt.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class LeaveTypeController : Controller
    {
        private readonly ILeaveTypeRepository _repo;
        private readonly IMapper _mapper;

        
        public LeaveTypeController(ILeaveTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        // GET: LeaveTypeController
       
        public ActionResult Index()
        {
            var leaveType = _repo.fndAll().ToList();
            var model = _mapper.Map<List<LeaveType>, List<LeaveTypeVM>>(leaveType);
            return View(model);
        }

        // GET: LeaveTypeController/Details/5
        public ActionResult Details(int id)
        {
            if (_repo.isExists(id))
            {
                var leavetype = _repo.FindById(id);
                var model = _mapper.Map<LeaveTypeVM>(leavetype);
                return View(model);
            }
            else
            {
                return NotFound();
            }
            
        }

        // GET: LeaveTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LeaveTypeVM collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var leaveType = _mapper.Map<LeaveType>(collection);
                    leaveType.DateCreated = DateTime.Now;
                    var isSuccess = _repo.Create(leaveType);
                    if (!isSuccess)
                    {
                        ModelState.AddModelError("", "Something went wrong...");
                        return View(collection);
                    }
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(collection);
                }
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View(collection);
            }
        }

        // GET: LeaveTypeController/Edit/5
        public ActionResult Edit(int id)
        {
            if (_repo.isExists(id))
            {
                var leaveType = _repo.FindById(id);
                var model = _mapper.Map<LeaveTypeVM>(leaveType);
                return View(model);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: LeaveTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LeaveTypeVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var leaveType = _mapper.Map<LeaveType>(model);
                   
                    var isSuccess = _repo.Update(leaveType);
                    if (!isSuccess)
                    {
                        ModelState.AddModelError("", "Something went wrong...");
                        return View(model);
                    }
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(model);
                }

            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View(model);
            }
        }

        // GET: LeaveTypeController/Delete/5
        public ActionResult Delete(int id)
        {
            var leavetype = _repo.FindById(id);
            if (leavetype == null)
            {
                return NotFound();
            }
            var isSuccess = _repo.Delete(leavetype);

            if (!isSuccess)
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View();
            }
            return RedirectToAction(nameof(Index));

        }

        // POST: LeaveTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id,LeaveTypeVM collection)
        {
            try
            {

                var leavetype = _repo.FindById(id);
                if (leavetype == null)
                {
                    return NotFound();
                }
                var isSuccess = _repo.Delete(leavetype);
                
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrong...");
                    return View(collection);
                }
                return RedirectToAction(nameof(Index));

              
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View(collection);
            }
        }
    }
}
