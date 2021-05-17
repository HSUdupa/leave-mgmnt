using leave_mgmnt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_mgmnt.Contracts
{
     public interface ILeaveAllocationRepository : IRepositoryBase<LeaveAllocation>
    {
        Task<bool> isLeaveExiss(int LeavetypeId, string empId);
        Task<ICollection<LeaveAllocation>> getLeaveAllocationDetail(string id);
        Task<LeaveAllocation> getLeaveAllocationDetailonLeaveTypeId(string employeeId,int leaveTypeId);

    }
}
