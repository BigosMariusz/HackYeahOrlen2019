using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace CheckYourSecurityCSharp.Helpers
{
    public static class PowerShellHelper
    {
        public static Collection<PSObject> RunCommand(string command)
        {
            using (PowerShell powerShell = PowerShell.Create())
            {
                powerShell.AddScript(command);

                Collection<PSObject> result = powerShell.Invoke();

                return result;
            }
        }
    }
}