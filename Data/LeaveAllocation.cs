using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace leave_mgmnt.Data
{
    public class LeaveAllocation
    {
        public int Id { get; set; }
        public int NumberOfDays { get; set; }
        public DateTime DateCreated { get; set; }

        [ForeignKey ("EmployeeId")]
        public Employee employee { get; set; }
        public string EmployeeId { get; set; }

        [ForeignKey ("LeaveTypeId")]
        public LeaveType leaveType { get; set; }
        public int leaveId { get; set; }


    }
}
