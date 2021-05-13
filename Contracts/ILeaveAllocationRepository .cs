using leave_mgmnt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_mgmnt.Contracts
{
     public interface ILeaveAllocationRepository : IRepositoryBase<LeaveAllocation>
    {
        bool isLeaveExiss(int LeavetypeId, string empId);
        ICollection<LeaveAllocation> getLeaveAllocationDetail(string id);
    }
}
