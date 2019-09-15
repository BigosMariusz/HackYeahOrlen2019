using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckYourSecurityCSharp.SecurityServices.Models
{
    public class WindowsUser
    {
        public string Name { get; set; }
        public List<string> Groups { get; set; }
        public string Status { get; set; }
    }
}
