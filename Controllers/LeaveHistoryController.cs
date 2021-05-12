using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using leave_mgmnt.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace leave_mgmnt.Controllers
{
    public class LeaveHistoryController : Controller
    {
        private readonly ILeaveHistoryRepository _repo;
        private readonly IMapper _mapper;

        public LeaveHistoryController(ILeaveHistoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        // GET: LeaveHistoryController
        public ActionResult Index()
        {
            return View();
        }

        // GET: LeaveHistoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LeaveHistoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveHistoryController/Create
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
