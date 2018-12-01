using System.Collections.Generic;
using ItMe.Utils;

namespace ItMe.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Company { get; set; }

        public List<JobRole> Roles { get; set; }
    }
}