using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.DTOs
{
    public class StopCreateDTO
    {
        public string StopName { get; set; }
        public int StopId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Order { get; set; }
    }
}
