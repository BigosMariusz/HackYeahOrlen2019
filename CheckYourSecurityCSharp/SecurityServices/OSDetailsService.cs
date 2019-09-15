using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management;
using System.Management.Automation;

namespace CheckYourSecurityCSharp.SecurityServices
{
    public class OSDetailsService
    {
        public string GetOSVersion()
        {
            return Environment.OSVersion.VersionString;
        }

        public bool IsOs64Bit()
        {
            return Environment.Is64BitOperatingSystem;
        }
        public string GetLoggedUserName()
        {
            return Environment.UserName;
        }

        public List<string> GetUpdatesList()
        {
            using (PowerShell powerShell = PowerShell.Create())
            {
                powerShell.AddScript("ipconfig /all");
                var hotFixList = new List<string>();
                Collection<PSObject> hotFixes = powerShell.Invoke();

                foreach (var hotFix in hotFixes)
                {
                    foreach (var prop in hotFix.Properties)
                    {
                        if (prop.Name == "HotFixID")
                        {
                            hotFixList.Add(prop.Value.ToString());
                        }
                    }
                }
                return hotFixList;
            }
        }
    }
}
