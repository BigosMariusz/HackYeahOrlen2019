using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckYourSecurityCSharp.SecurityServices.Models
{
    public class Error
    {
        string what;
        string insteadOfWhat;
        string message;
        string module;
        bool isError;
        public Error(string wh, string iow, string msg, string mdl, bool ie)
        {
            what = wh;
            insteadOfWhat = iow;
            message = msg;
            module = mdl;
            isError = ie;
        }
        public bool IsError { get { return isError; } set { isError = value; } }
        public string Module { get { return module; } set { module = value; } }
        public string What { get { return what; } set { what = value; } }
        public string InsteadOfWhat { get { return insteadOfWhat; } set { insteadOfWhat = value; } }

    }
}
