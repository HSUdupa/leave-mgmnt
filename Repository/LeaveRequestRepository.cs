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
        public bool Create(LeaveRequest entity)
        {
            _db.LeaveRequests.Add(entity);
            return Save();
        }

        public bool Delete(LeaveRequest entity)
        {
            _db.LeaveRequests.Remove(entity);
            return Save();
        }

        public LeaveRequest FindById(int id)
        {
           return _db.LeaveRequests
                .Include(q => q.approvedBy)
                .Include(q => q.requestEmployee)
                .Include(q => q.leaveType).FirstOrDefault(q=>q.id==id);
           
        }

        public ICollection<LeaveRequest> fndAll()
        {
            return _db.LeaveRequests
                .Include(q=>q.approvedBy)
                .Include(q=>q.requestEmployee)
                .Include(q=>q.leaveType)
                .ToList();
            
        }

        public ICollection<LeaveRequest> GetAllrequestforEmployeeId(string emp_id)
        {
            var data = _db.LeaveRequests.
                Include(q=>q.requestEmployee)
                .Include(q=>q.approvedBy)
                .Include(q=>q.leaveType)
                .Where(q=>q.requestEmployeeid==emp_id).ToList();
            return data;
        }

        public bool isExists(int id)
        {
            return _db.LeaveRequests.Any(q => q.id == id);
        }
        public bool Save()
        {
            var changes=_db.SaveChanges();
            return changes > 0;
        }

        public bool Update(LeaveRequest entity)
        {
            _db.LeaveRequests.Update(entity);
            return Save();
        }
    }
}
