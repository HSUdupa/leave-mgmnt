using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_mgmnt.Models
{
    public class LeaveAllocationVM
    {
        public int Id { get; set; }
    
        public int NumberOfDays { get; set; }
        public DateTime DateCreated { get; set; }

        public int Period { get; set; }

        public EmployeeVM employee { get; set; }
        public string EmployeeId { get; set; }

        public LeaveTypeVM leaveType { get; set; }
        public int leaveId { get; set; }

        

    }

    public class CreateLeaveAllocationVM
    {
        public int NumberUpdated { get; set; }
        public List<LeaveTypeVM> LeaveType { get; set; }
        
    }

    public class EditLeaveAllocationVM
    {
        public int Id { get; set; }
       
        public EmployeeVM employee { get; set; }
        public string EmployeeId { get; set; }
        public LeaveTypeVM LeaveType { get; set; }
        public int NumberOfDays { get; set; }
    }

    public class viewLeaveAllocationVM
    {
        public EmployeeVM employee { get; set; }
        public string employeeId { get; set; }
        public List<LeaveAllocationVM> leaveAllocation { get; set; }
    }
}
