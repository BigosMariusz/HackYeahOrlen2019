using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckYourSecurityCSharp.SecurityServices.Models
{
    public class InputDataModel
    {
        public Settings settings { get; set; }

    }

    public class Settings
    {
        public string OSVersion { get; set; }
        public List<User> users { get; set; }
        public List<String> services { get; set; }
        public List<Antivirus> antivirus { get; set; }
    }

    public class User
    {
        public string name { get; set; }
        public List<string> groups { get; set; }
        public string state { get; set; }
    }
    public class Antivirus
    {
        public string name { get; set; }
        public string productState { get; set; }

    }
}
