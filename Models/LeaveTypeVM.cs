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

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name ="Default days")]
        [Range(1,25,ErrorMessage ="Enter a valid number.")]
        public int DefaultDays { get; set; }

        [Display (Name ="Created Date")]
        public DateTime? DateCreated { get; set; }
    }

    
}
