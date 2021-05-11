using leave_mgmnt.Contracts;
using leave_mgmnt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_mgmnt.Repository
{
    public class LeaveHistorysRepository : ILeaveHistoryRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveHistorysRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool Create(LeaveHistorys entity)
        {
            _db.LeaveHistorys.Add(entity);
            return Save();
        }

        public bool Delete(LeaveHistorys entity)
        {
            _db.LeaveHistorys.Remove(entity);
            return Save();
        }

        public LeaveHistorys FindById(int id)
        {
           return _db.LeaveHistorys.Find(id);
           
        }

        public ICollection<LeaveHistorys> fndAll()
        {
            return _db.LeaveHistorys.ToList();
            
        }

        public bool Save()
        {
            var changes=_db.SaveChanges();
            return changes > 0;
        }

        public bool Update(LeaveHistorys entity)
        {
            _db.LeaveHistorys.Update(entity);
            return Save();
        }
    }
}
