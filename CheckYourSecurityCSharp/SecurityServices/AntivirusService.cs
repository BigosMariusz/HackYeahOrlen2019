using CheckYourSecurityCSharp.SecurityServices.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace CheckYourSecurityCSharp.SecurityServices
{
    public class AntivirusService
    {
        public List<Antivirus> AntivirusInstalled()
        {
            using (PowerShell powerShell = PowerShell.Create())
            {
                var antivurusesList = new List<Antivirus>();
                powerShell.AddScript("Get-CimInstance -Namespace root/SecurityCenter2 -ClassName AntivirusProduct");

                Collection<PSObject> antivuruses = powerShell.Invoke();

                foreach (var antivurus in antivuruses)
                {
                    var antivirusLocal = new Antivirus();
                    foreach (var prop in antivurus.Properties)
                    {
                        if (prop.Name == "displayName")
                        {
                            antivirusLocal.name = prop.Value.ToString();
                        }
                        if (prop.Name == "productState")
                        {
                            antivirusLocal.productState = prop.Value.ToString();
                        }
                    }
                    antivurusesList.Add(antivirusLocal);
                }

                return antivurusesList;
            }
        }

    }
}
