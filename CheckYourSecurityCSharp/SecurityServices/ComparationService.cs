using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CheckYourSecurityCSharp.SecurityServices.Models;
using CheckYourSecurityCSharp.SecurityServices;

namespace CheckYourSecurityCSharp.SecurityServices
{
    public class ComparationService
    {
        InputDataModel pattern;
        List<Error> mistakes;
        public ComparationService(string patternPath, string loadedPath)
        {
            mistakes = new List<Error>();
            var textfrompattern = File.ReadAllText(patternPath);
            pattern = JsonConvert.DeserializeObject<InputDataModel>(textfrompattern);
        }
        public List<Error> getAndFillMistakes()
        {
            var os = new OSDetailsService();

            if (pattern.settings.OSVersion != os.GetOSVersion())
            {
                var er = new Error(os.GetOSVersion(), pattern.settings.OSVersion, "Different OS version", nameof(pattern.settings.OSVersion), true);
                mistakes.Add(er);
            }
            else
            {
                var er = new Error(os.GetOSVersion(), pattern.settings.OSVersion, "Different OS version", nameof(pattern.settings.OSVersion), false);
                mistakes.Add(er);
            }

            var usersService = new UsersDetailsService();

            for (int i = 0; i < pattern.settings.users.Count; i++)
            {
                if (pattern.settings.users[i].name == usersService.GetUsersWithProperties()[i].name)
                {
                    pattern.settings.users[i].groups.Sort();
                    usersService.GetUsersWithProperties()[i].groups.Sort();
                    for (int j = 0; j < pattern.settings.users[i].groups.Count; j++)
                    {
                        if (pattern.settings.users[i].groups[j] != usersService.GetUsersWithProperties()[i].groups[j])
                        {
                            var er = new Error(usersService.GetUsersWithProperties()[i].name.ToString(), pattern.settings.users[i].name.ToString(), "Different groups", nameof(pattern.settings.users), true);
                            mistakes.Add(er);
                            break;
                        }
                        else
                        {
                            var er = new Error(usersService.GetUsersWithProperties()[i].name.ToString(), pattern.settings.users[i].name.ToString(), "Different groups", nameof(pattern.settings.users), false);
                            mistakes.Add(er);
                            break;
                        }
                    }

                }
                else
                {
                    var er = new Error("", pattern.settings.users[i].name.ToString(), "Different groups", nameof(pattern.settings.users), true);
                    mistakes.Add(er);
                }
            }

            var ntwService = new AplicationService();
            for (int i = 0; i < pattern.settings.services.Count; i++)
            {
                if (ntwService.GetInstalledApps() == null)
                    continue;
                if (!ntwService.GetInstalledApps().Contains(pattern.settings.services[i]))
                {
                    var er = new Error("", pattern.settings.services[i], "There is no such running service", nameof(pattern.settings.services), true);
                    mistakes.Add(er);
                }
                else
                {
                    var er = new Error("", pattern.settings.services[i], "There is no such running service", nameof(pattern.settings.services), false);
                    mistakes.Add(er);
                }
            }

            var avService = new AntivirusService();
            foreach (var av in avService.AntivirusInstalled())
            {
                switch (av.productState)
                {
                    case "262144": mistakes.Add(new Error(null, null, "Up to date " + "Disabled", av.name, true)); break;
                    case "262160": mistakes.Add(new Error(null, null, "Out of date " + "Disabled", av.name, true)); break;
                    case "266256": mistakes.Add(new Error(null, null, "Out of date " + "Enabled", av.name, true)); break;
                    case "393216": mistakes.Add(new Error(null, null, "Up to date " + "Disabled", av.name, true)); break;
                    case "393232": mistakes.Add(new Error(null, null, "Out of date " + "Disabled", av.name, true)); break;
                    case "393488": mistakes.Add(new Error(null, null, "Out of date " + "Disabled", av.name, true)); break;
                    case "397328": mistakes.Add(new Error(null, null, "Out of date " + "Enabled", av.name, true)); break;
                    case "397584": mistakes.Add(new Error(null, null, "Out of date " + "Enabled", av.name, true)); break;
                    case "266240": mistakes.Add(new Error(null, null, "Up to date " + "Enabled", av.name, false)); break;
                    case "397568": mistakes.Add(new Error(null, null, "Up to date " + "Enabled", av.name, false)); break;
                    case "397312": mistakes.Add(new Error(null, null, "Up to date " + "Enabled", av.name, false)); break;

                }
            }
            return mistakes;
        }

    }
}
