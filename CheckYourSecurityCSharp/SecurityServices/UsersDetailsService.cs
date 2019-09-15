using CheckYourSecurityCSharp.SecurityServices.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Management;
using System.Management.Automation;

namespace CheckYourSecurityCSharp.SecurityServices
{
    public class UsersDetailsService
    {
        private List<string> GetUsersLogins()
        {
            var users = new List<string>();
            var query = new SelectQuery("Win32_UserAccount", $"Domain ='{Environment.UserDomainName}'");

            using (var searcher = new ManagementObjectSearcher(query))
            {
                foreach (ManagementObject user in searcher.Get())
                {
                    foreach (PropertyData prop in user.Properties)
                    {
                        if (prop.Name == "Name")
                        {
                            users.Add(prop.Value.ToString());
                        }
                    }
                }
            }
            
            return users;
        }

        public List<User> GetUsersWithProperties()
        {
            var windowsUsers = new List<User>();
            PrincipalContext oPrincipalContext = new PrincipalContext(ContextType.Machine);

            var users = GetUsersLogins();

            foreach (var userName in users)
            {
                var windowsUser = new User();
                windowsUser.name = userName;

                var userPrincipal = Principal.FindByIdentity(oPrincipalContext, userName);

                if ((userPrincipal as UserPrincipal).Enabled == true)
                { 
                    windowsUser.state = "Enabled";
                }
                else
                {
                    windowsUser.state = "Disabled";
                }

                var groups = userPrincipal.GetGroups();

                windowsUser.groups = new List<string>();

                foreach (Principal group in groups)
                {
                    windowsUser.groups.Add(group.Name);
                }
                windowsUsers.Add(windowsUser);
            }

            return windowsUsers;
        }
    }
}
