using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;

namespace CheckYourSecurityCSharp.SecurityServices
{
    public class AplicationService
    {
        public List<string> GetInstalledApps()
        {
            string displayName;
            RegistryKey key;
            var installedApps = new List<string>();

            key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                displayName = subkey.GetValue("DisplayName") as string;
                installedApps.Add(displayName);
            }

            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                displayName = subkey.GetValue("DisplayName") as string;
                installedApps.Add(displayName);
            }

            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                displayName = subkey.GetValue("DisplayName") as string;
                installedApps.Add(displayName);
            }
            installedApps.RemoveAll(x => x == null);

            return installedApps;
        }

        public List<string> GetRunningServices()
        {
            ServiceController[] services = ServiceController.GetServices();
            var runningServices = services.Where(x => x.Status == ServiceControllerStatus.Running).Select(x => x.ServiceName).ToList();

            return runningServices;
        }
    }
}