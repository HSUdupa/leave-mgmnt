using leave_mgmnt.Contracts;
using leave_mgmnt.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace leave_mgmnt.Repository
{
    public class LeaveAllocationRepository : ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveAllocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(LeaveAllocation entity)
        {
            await _db.LeaveAllocations.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Remove(entity);
            return await Save();
        }

        public async Task<LeaveAllocation> FindById(int id)
        {
            return await _db.LeaveAllocations.Include(q=>q.LeaveType).Include(q=>q.employee).FirstOrDefaultAsync(e=>e.Id==id);
        }

        public async Task<ICollection<LeaveAllocation>> fndAll()
        {
            var rtnData= await _db.LeaveAllocations.Include(q => q.LeaveType).ToListAsync();
            return rtnData;
        }

        public async Task<ICollection<LeaveAllocation>> getLeaveAllocationDetail(string id)
        {
             var period = DateTime.Now.Year;
            var rtnData = await fndAll();
            return rtnData.Where(q => q.EmployeeId == id && q.Period == period).ToList();
        }

        public async Task<LeaveAllocation> getLeaveAllocationDetailonLeaveTypeId(string employeeId, int leaveTypeId)
        {
            var period = DateTime.Now.Year;
            var rtnData = await fndAll();
            return rtnData.FirstOrDefault(q => q.EmployeeId == employeeId && q.Period == period && q.LeaveTypeId == leaveTypeId);
        }

        public async Task<bool> isExists(int id)
        {
            return await _db.LeaveAllocations.AnyAsync(q => q.Id == id);
        }

        public async Task<bool> isLeaveExiss(int LeavetypeId, string empId)
        {
            var period = DateTime.Now.Year;
            var find= await fndAll();
            return find.Where(q => q.EmployeeId == empId && q.LeaveTypeId == LeavetypeId && q.Period == period).Any();
        }

        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Update(entity);
            return await Save();
        }
    }
}
