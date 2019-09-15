using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckYourSecurityCSharp.RaportGenerator.Models
{
    public class TestResultsModel
    {
        public string Name { get; set; }
        public string ExpectedValue { get; set; }
        public string RevicedValue { get; set; }
    }
}
