using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace leave_mgmnt.Data
{
    public class LeaveRequest
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("requestEmployeeid")]
        public Employee requestEmployee { get; set; }
        public string requestEmployeeid { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        [ForeignKey("LeaveTypeId")]
        public LeaveType leaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime dateRequested { get; set; }
        public DateTime dateActioned { get; set; }
        public bool? Approved { get; set; }
        public bool Cancelled { get; set; }
        [ForeignKey("approvedByid")]
        public Employee approvedBy { get; set; }
        public string approvedByid { get; set; }

        public string RequestComments { get; set; }
    }
}
