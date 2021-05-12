using leave_mgmnt.Contracts;
using leave_mgmnt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_mgmnt.Repository
{
    public class LeaveTypeRepository : ILeaveTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveTypeRepository(ApplicationDbContext db)
        {
            _db = db; 
        }
        public bool Create(LeaveType entity)
        {
            _db.LeaveTypes.Add(entity);
            return Save();
        }

        public bool Delete(LeaveType entity)
        {
            _db.LeaveTypes.Remove(entity);
            return Save();
        }

        public LeaveType FindById(int id)
        {
            var leaveType = _db.LeaveTypes.Find(id);
            return leaveType;
        }

        public ICollection<LeaveType> fndAll()
        {
            return _db.LeaveTypes.ToList();
        }

        public bool isExists(int id)
        {
            return _db.LeaveTypes.Any(q => q.Id == id);
        }

        public bool Save()
        {
            var change =_db.SaveChanges();
            return change > 0;
        }

        public bool Update(LeaveType entity)
        {
            _db.LeaveTypes.Update(entity);
            return Save();
        }
    }
}
