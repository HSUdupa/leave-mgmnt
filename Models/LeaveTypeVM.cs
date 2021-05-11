using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_mgmnt.Models
{
    public class LeaveTypeVM
    {
       
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class createLeaveTypeVM
    {

        [Required]
        public string Name { get; set; }
        
    }
}
