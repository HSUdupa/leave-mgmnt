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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace leave_mgmnt.Controllers
{ 
    [Authorize]
public class LeaveRequestController : Controller
{
    private readonly ILeaveRequestRepository _leaveRequestrepo;
    private readonly ILeaveTypeRepository _leaveTypeRepo;
    private readonly ILeaveAllocationRepository _leavaAllocRepo;
    private readonly IMapper _mapper;
    private readonly UserManager<Employee> _userManager;


    public LeaveRequestController(UserManager<Employee> userManager, ILeaveAllocationRepository leavaAllocRepo, ILeaveTypeRepository leaveTypeRepo, ILeaveRequestRepository repo, IMapper mapper)
    {

        _leaveRequestrepo = repo;
        _leaveTypeRepo = leaveTypeRepo;
        _leavaAllocRepo = leavaAllocRepo;
        _mapper = mapper;
        _userManager = userManager;
    }

    [Authorize(Roles = "Administrator")]
    // GET: LeaveHistoryController
    public ActionResult Index()
    {
        var leaveRequest = _leaveRequestrepo.fndAll();
        var mappedleaveRequest = _mapper.Map<List<LeaveRequestVM>>(leaveRequest);
        var model = new adminLeaveRequestIndexVM
        {
            totalRequests = mappedleaveRequest.Count,
            approvedRequests = mappedleaveRequest.Count(q => q.Approved == true),
            rejectedeRequests = mappedleaveRequest.Count(q => q.Approved == false),
            pendingRequests = mappedleaveRequest.Count(q => q.Approved == null),
            leaveRequets = mappedleaveRequest
        };

        return View(model);
    }
        public ActionResult CancelRequest(int id)
        {
            var leaveDeatails = _leaveRequestrepo.FindById(id);
            leaveDeatails.Cancelled = true;
            _leaveRequestrepo.Update(leaveDeatails);
            /*if (leaveDeatails.Approved == true)
            {
                var totalDays = (leaveDeatails.endDate.Date - leaveDeatails.startDate.Date).Days;
                var leavetypeId = leaveDeatails.LeaveTypeId;
                var empid = leaveDeatails.requestEmployeeid;
                
                var leaveAllocationData = _leavaAllocRepo.getLeaveAllocationDetailonLeaveTypeId(empid, leavetypeId);
                leaveAllocationData.NumberOfDays += totalDays;
                _leavaAllocRepo.Update(leaveAllocationData);

            }*/

            return RedirectToAction("MyLeave");
        }
        public ActionResult MyLeave(int id)
        {
            var emp = _userManager.GetUserAsync(User).Result;
            var empId = emp.Id;
            var allocation = _leavaAllocRepo.getLeaveAllocationDetail(empId);
            var leaveRequests = _leaveRequestrepo.GetAllrequestforEmployeeId(empId);

            var allocationModel = _mapper.Map<List<LeaveAllocationVM>>(allocation);
            var leaveRequestModel = _mapper.Map<List<LeaveRequestVM>>(leaveRequests);

            var model = new employeeLreaveRequestVM
            {
                LeaveAllocation = allocationModel,
                LeaveType = leaveRequestModel
            };
            return View(model);


        }
    // GET: LeaveHistoryController/Details/5
    public ActionResult Details(int id)
    {
            var leaveRequest = _leaveRequestrepo.FindById(id);
            var model = _mapper.Map<LeaveRequestVM>(leaveRequest);
        return View(model);
    }

    public ActionResult ApproveRequest(int id)
    {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                var leaveRequest = _leaveRequestrepo.FindById(id);
                var empId = leaveRequest.requestEmployeeid;
                var leaveId = leaveRequest.LeaveTypeId;
                var allocation = _leavaAllocRepo.getLeaveAllocationDetailonLeaveTypeId(empId, leaveId);
                var requestedDays =(int) (leaveRequest.endDate.Date - leaveRequest.startDate.Date).TotalDays;
                allocation.NumberOfDays -= requestedDays;

                leaveRequest.Approved = true;
                leaveRequest.approvedByid = user.Id;
                leaveRequest.dateActioned = DateTime.Now;

                 _leaveRequestrepo.Update(leaveRequest);
                _leavaAllocRepo.Update(allocation);

                
                
                   return RedirectToAction(nameof(Index));
                
            }
            catch (Exception ex)
            {

                return RedirectToAction(nameof(Index));
                
            }
    }

    public ActionResult RejectRequest(int id)
    {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                var leaveRequest = _leaveRequestrepo.FindById(id);
                leaveRequest.Approved = false;
                leaveRequest.approvedByid = user.Id;
                leaveRequest.dateActioned = DateTime.Now;

                var isSuccess = _leaveRequestrepo.Update(leaveRequest);


                return RedirectToAction(nameof(Index), "LeaveRequest");

            }
            catch (Exception ex)
            {

                return RedirectToAction(nameof(Index), "LeaveRequest");

            }
        }

    // GET: LeaveHistoryController/Create
    public ActionResult Create()
    {
        var leaveTypes = _leaveTypeRepo.fndAll();
        var leaveTypeItem = leaveTypes.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value=q.Id.ToString()
            });

            var model = new createLeaveRequestVM
            {
                leaveTypes = leaveTypeItem
            };

        return View(model);
    }

    // POST: LeaveHistoryController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(createLeaveRequestVM model)
    {
        try
        {
                var startDate = Convert.ToDateTime(model.startDate);
                var endDate = Convert.ToDateTime(model.endDate);
                var leaveTypes = _leaveTypeRepo.fndAll();
                var leaveTypeItem = leaveTypes.Select(q => new SelectListItem
                {
                    Text = q.Name,
                    Value = q.Id.ToString()
                });
                model.leaveTypes = leaveTypeItem;


                if (ModelState.IsValid)
                {
                    if (DateTime.Compare(startDate, endDate) > 0)
                    {
                        ModelState.AddModelError("", "Start date cannot be greater than end date.");
                        return View(model);
                    }
                    else
                    {
                        var employee = _userManager.GetUserAsync(User).Result;
                        var allocations = _leavaAllocRepo.getLeaveAllocationDetailonLeaveTypeId(employee.Id, model.LeaveTypeId);
                        int requestedDays = (int)(endDate.Date - startDate.Date).TotalDays;
                        if (requestedDays > allocations.NumberOfDays)
                        {
                            ModelState.AddModelError("", "You dont have sufficient days left , please choose less days.");
                            return View(model);
                        }

                        var leaveRequestData = new LeaveRequestVM
                        {
                            requestEmployeeid = employee.Id,
                            LeaveTypeId=model.LeaveTypeId,
                            startDate = startDate,
                            endDate = endDate,
                            Approved = null,
                            dateRequested = DateTime.Now,
                            dateActioned = DateTime.Now,
                            RequestComments=model.RequestComments
                            
                        };
                        var leaveRequestUpdate = _mapper.Map<LeaveRequest>(leaveRequestData);
                        var isSuccess = _leaveRequestrepo.Create(leaveRequestUpdate);
                        if (!isSuccess)
                        {
                            ModelState.AddModelError("", "Unable to process the request, Something went wron while submitting!");
                            return View(model);
                        }
                    }

                }
                else
                {
                    return View(model);
                }
                return RedirectToAction(nameof(Index), "Home");
            
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", "Somethig went wrong!");
            return View(model);
        }
    }

    // GET: LeaveHistoryController/Edit/5
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: LeaveHistoryController/Edit/5
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

    // GET: LeaveHistoryController/Delete/5
    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: LeaveHistoryController/Delete/5
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
