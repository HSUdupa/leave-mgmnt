using leave_mgmnt.Contracts;
using leave_mgmnt.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace leave_mgmnt.Repository
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveRequestRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(LeaveRequest entity)
        {
           await _db.LeaveRequests.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(LeaveRequest entity)
        {
            _db.LeaveRequests.Remove(entity);
            return await Save();
        }

        public async Task<LeaveRequest> FindById(int id)
        {
           return await _db.LeaveRequests
                .Include(q => q.approvedBy)
                .Include(q => q.requestEmployee)
                .Include(q => q.leaveType).FirstOrDefaultAsync(q=>q.id==id);
           
        }

        public async Task<ICollection<LeaveRequest>> fndAll()
        {
            return await _db.LeaveRequests
                .Include(q=>q.approvedBy)
                .Include(q=>q.requestEmployee)
                .Include(q=>q.leaveType)
                .ToListAsync();
            
        }

        public async Task<ICollection<LeaveRequest>> GetAllrequestforEmployeeId(string emp_id)
        {
            var data =await _db.LeaveRequests.
                Include(q=>q.requestEmployee)
                .Include(q=>q.approvedBy)
                .Include(q=>q.leaveType)
                .Where(q=>q.requestEmployeeid==emp_id).ToListAsync();
            return data;
        }

        public async Task<bool> isExists(int id)
        {
            return await _db.LeaveRequests.AnyAsync(q => q.id == id);
        }
        public async Task<bool> Save()
        {
            var changes=await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(LeaveRequest entity)
        {
             _db.LeaveRequests.Update(entity);
            return await Save();
        }
    }
}
