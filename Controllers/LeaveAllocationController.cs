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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace leave_mgmnt.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class LeaveAllocationController : Controller
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepo;
        private readonly ILeaveTypeRepository _leaveTypeRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;

        public LeaveAllocationController(UserManager<Employee> userManager,ILeaveAllocationRepository leaveAllocationRepo, ILeaveTypeRepository leaveTypeRepo,IMapper mapper)
        {
            _leaveAllocationRepo = leaveAllocationRepo;
            _leaveTypeRepo = leaveTypeRepo;
            _userManager = userManager;
            _mapper = mapper;
        }
        // GET: LeaveAllocationController
        public async Task<ActionResult> Index()
        {
            var leaveType =await _leaveTypeRepo.fndAll();
            var mappedData = _mapper.Map<List<LeaveType>, List<LeaveTypeVM>>(leaveType.ToList());
            var model = new CreateLeaveAllocationVM
            {
                LeaveType = mappedData,
                NumberUpdated = 0
            };
            return View(model);
        }


        public async Task<ActionResult> SetLeave(int id)
        {
            var leaveType = await _leaveTypeRepo.FindById(id);
            var employee = await _userManager.GetUsersInRoleAsync("Employee");

            foreach(var emp in employee)
            {
                if (await _leaveAllocationRepo.isLeaveExiss(id, emp.Id))
                    continue;
                var allocation = new LeaveAllocationVM
                {
                    DateCreated = DateTime.Now,
                    EmployeeId = emp.Id,
                    leaveId = id,
                    NumberOfDays = leaveType.DefaultDays,
                    Period = DateTime.Now.Year
                };
                var leaveAllocation = _mapper.Map<LeaveAllocation>(allocation);
                await _leaveAllocationRepo.Create(leaveAllocation);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> listEmployees()
        {
            var employee = await _userManager.GetUsersInRoleAsync("Employee");
            var model = _mapper.Map<List<EmployeeVM>>(employee);
            return View(model);
        }
        // GET: LeaveAllocationController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var employeeData = _mapper.Map<EmployeeVM>(await _userManager.FindByIdAsync(id));
            var allocation =_mapper.Map<List<LeaveAllocationVM>>( await _leaveAllocationRepo.getLeaveAllocationDetail(id));
            var model = new viewLeaveAllocationVM
            {
                employee = employeeData,
               // employeeId = id,
                leaveAllocation = allocation
            };

            return View(model);
        }

        // GET: LeaveAllocationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveAllocationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: LeaveAllocationController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var editData = await _leaveAllocationRepo.FindById(id);
            var model = _mapper.Map<EditLeaveAllocationVM>(editData);
            return View(model);
        }

        // POST: LeaveAllocationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditLeaveAllocationVM data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var record = await _leaveAllocationRepo.FindById(data.Id);
                    record.NumberOfDays = data.NumberOfDays;
                    var isSuccess =await  _leaveAllocationRepo.Update(record);
                    
                    if (!isSuccess)
                    { 
                        ModelState.AddModelError("", "Something went wrong...");
                        return View(data);
                    }
                    return RedirectToAction(nameof(Details),new { id=data.EmployeeId});
                }
                else
                {
                    return View(data);
                }
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View(data);
            }
        }

        // GET: LeaveAllocationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveAllocationController/Delete/5
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
