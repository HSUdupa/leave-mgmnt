using leave_mgmnt.Contracts;
using leave_mgmnt.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_mgmnt.Repository
{
    public class LeaveAllocationRepository : ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveAllocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool Create(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Add(entity);
            return Save();
        }

        public bool Delete(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Remove(entity);
            return Save();
        }

        public LeaveAllocation FindById(int id)
        {
            return _db.LeaveAllocations.Include(q=>q.LeaveType).Include(q=>q.employee).FirstOrDefault(e=>e.Id==id);
        }

        public ICollection<LeaveAllocation> fndAll()
        {
            var rtnData= _db.LeaveAllocations.Include(q => q.LeaveType).ToList();
            return rtnData;
        }

        public ICollection<LeaveAllocation> getLeaveAllocationDetail(string id)
        {
             var period = DateTime.Now.Year;
             var rtnData=fndAll().Where(q => q.EmployeeId == id && q.Period==period).ToList();
            return rtnData;
        }

        public LeaveAllocation getLeaveAllocationDetailonLeaveTypeId(string employeeId, int leaveTypeId)
        {
            var period = DateTime.Now.Year;
            var rtnData = fndAll().FirstOrDefault(q => q.EmployeeId == employeeId && q.Period == period&&q.LeaveTypeId==leaveTypeId);
            return rtnData;
        }

        public bool isExists(int id)
        {
            return _db.LeaveAllocations.Any(q => q.Id == id);
        }

        public bool isLeaveExiss(int LeavetypeId, string empId)
        {
            var period = DateTime.Now.Year;
            return fndAll().Where(q => q.EmployeeId == empId && q.LeaveTypeId == LeavetypeId && q.Period == period).Any();
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        public bool Update(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Update(entity);
            return Save();
        }
    }
}
