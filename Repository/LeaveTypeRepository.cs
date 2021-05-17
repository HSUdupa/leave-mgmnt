using leave_mgmnt.Contracts;
using leave_mgmnt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace leave_mgmnt.Repository
{
    public class LeaveTypeRepository : ILeaveTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public  LeaveTypeRepository (ApplicationDbContext db)
        {
            _db = db; 
        }
        public async Task<bool> Create(LeaveType entity)
        {
           await  _db.LeaveTypes.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(LeaveType entity)
        {
             _db.LeaveTypes.Remove(entity);
            return await Save();
        }

        public async Task<LeaveType> FindById(int id)
        {
            var leaveType =await _db.LeaveTypes.FindAsync(id);
            return leaveType;
        }

        public async Task<ICollection<LeaveType>> fndAll()
        {
            return await _db.LeaveTypes.ToListAsync();
        }

        public  async Task<bool> isExists(int id)
        {
            return await _db.LeaveTypes.AnyAsync(q => q.Id == id);
        }

        public async Task<bool> Save()
        {
            var change =await _db.SaveChangesAsync();
            return change > 0;
        }

        public async Task<bool> Update(LeaveType entity)
        {
            _db.LeaveTypes.Update(entity);
            return await Save();
        }
    }
}
