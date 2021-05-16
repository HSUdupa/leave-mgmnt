using AutoMapper;
using leave_mgmnt.Data;
using leave_mgmnt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_mgmnt.Mappings
{
    public class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<LeaveType, LeaveTypeVM>().ReverseMap();
           
            CreateMap<LeaveRequest, LeaveRequestVM>().ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationVM>().ReverseMap();
            CreateMap<LeaveAllocation, EditLeaveAllocationVM>().ReverseMap();
            CreateMap<Employee, EmployeeVM>().ReverseMap();
            


        }
    }
}
