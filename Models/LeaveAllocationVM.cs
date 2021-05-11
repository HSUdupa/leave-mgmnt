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
        [Required]
        public int NumberOfDays { get; set; }
        public DateTime DateCreated { get; set; }

        
        public EmployeeVM employee { get; set; }
        public string EmployeeId { get; set; }

        public LeaveTypeVM leaveType { get; set; }
        public int leaveId { get; set; }

        public IEnumerable<SelectListItem> employees { get; set; }
        public IEnumerable<SelectListItem> leaveTypes { get; set; }

    }
}
