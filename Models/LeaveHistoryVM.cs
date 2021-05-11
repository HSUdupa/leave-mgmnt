using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_mgmnt.Models
{
    public class LeaveHistoryVM
    {
       
        public int id { get; set; }
        public EmployeeVM requestEmployee { get; set; }
        public string requestEmployeeid { get; set; }
        [Required]
        public DateTime startDate { get; set; }
        [Required]
        public DateTime endDate { get; set; }
        public LeaveTypeVM leaveType { get; set; }
        public IEnumerable<SelectListItem> leaveTypes { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime dateRequested { get; set; }
        public DateTime dateActioned { get; set; }
        public bool? Approved { get; set; }

        public EmployeeVM approvedBy { get; set; }
        public string approvedByid { get; set; }
    }
}
