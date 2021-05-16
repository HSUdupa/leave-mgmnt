using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_mgmnt.Models
{
    public class LeaveRequestVM
    {
        public int id { get; set; }
        public EmployeeVM requestEmployee { get; set; }
        public string requestEmployeeid { get; set; }
        [Required]
        public DateTime startDate { get; set; }
        [Required]
        public DateTime endDate { get; set; }
        public LeaveTypeVM leaveType { get; set; }

        public int LeaveTypeId { get; set; }
        public DateTime dateRequested { get; set; }
        public DateTime dateActioned { get; set; }
        public bool? Approved { get; set; }

        public EmployeeVM approvedBy { get; set; }
        public string approvedByid { get; set; }

        [Display(Name ="Comments")]
        [MaxLength(300)]
        public string RequestComments { get; set; }
        public bool Cancelled { get; set; }

    }

    public class adminLeaveRequestIndexVM
    {
        [Display(Name ="Total Requests")]
        public int totalRequests { get; set; }

        [Display(Name = "Approved Requests")]
        public int approvedRequests { get; set; }

        [Display(Name = "Rejected Requests")]
        public int rejectedeRequests { get; set; }

        [Display(Name = "Pending  Requests")]
        public int pendingRequests { get; set; }

        public List<LeaveRequestVM> leaveRequets { get; set; }
    }

    public class createLeaveRequestVM
    {
        [Required]
        [Display(Name = "Start Date")]
       // [DataType(DataType.Date)]
        public string startDate { get; set; }
        [Required]
        [Display(Name = "End Date")]
        //[DataType(DataType.Date)]
        public string endDate { get; set; }
        [Display(Name = "Comments")]
        [MaxLength(300)]
        public string RequestComments { get; set; }


        public IEnumerable<SelectListItem> leaveTypes { get; set; }

        [Display(Name ="Leave Type")]
        public int LeaveTypeId { get; set; }
    }

    public class employeeLreaveRequestVM
    {
        public List<LeaveAllocationVM> LeaveAllocation { get; set; }
        public List<LeaveRequestVM> LeaveType { get; set; }
    }
}
