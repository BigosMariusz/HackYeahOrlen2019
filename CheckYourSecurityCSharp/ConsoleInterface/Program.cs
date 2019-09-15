using CheckYourSecurityCSharp.SecurityServices;
using Microsoft.Win32;
using CheckYourSecurityCSharp.SecurityServices;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Management;
using System.Management.Automation;
using CheckYourSecurityCSharp.RaportGenerator;
using CheckYourSecurityCSharp.RaportGenerator.Models;
using System.Linq;

namespace CheckYourSecurityCSharp.ConsoleInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            var osDetailsService = new OSDetailsService();
            var userDetailsService = new UsersDetailsService();
            var aplicationService = new AplicationService();
            var antivirusService = new AntivirusService();
            var dataToPdf = new DataToPdf();
            Console.WriteLine(osDetailsService.GetOSVersion());
            Console.WriteLine(osDetailsService.IsOs64Bit());
            Console.WriteLine(osDetailsService.GetLoggedUserName());

            var ins = new ComparationService("loaded.json", null);
            var report = ins.getAndFillMistakes();

            var pdfGenerator = new DataToPdf();
            var reportModel = new ReportDetailsModel();
            reportModel.FaliureCount = report.Where(x => x.IsError == true).ToList().Count;
            reportModel.NumberOfTests = report.Count;
            pdfGenerator.RenderPdf(report, reportModel);
        }
    }
}
