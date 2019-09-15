using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckYourSecurityCSharp.RaportGenerator.Models;
using CheckYourSecurityCSharp.SecurityServices.Models;
using IronPdf;

namespace CheckYourSecurityCSharp.RaportGenerator
{
    public class DataToPdf
    {
        public void RenderPdf(List<Error> testResults, ReportDetailsModel reportDetails)
        {
            HtmlToPdf renderer = new HtmlToPdf();

            var html = @"<style> h1, h2, p {text-align:center;} p {font-size: 18px; }";
            html += "table {border-collapse: collapse;} th, td {border: 1px solid black;}</style>" +
                    "<h1>Raport stanu bezpieczeństwa serwera</h1>" + "<h2 style=\"margin-top: 30px;\">Status:";

            if (reportDetails.FaliureCount > 0)
                html += "<span style=\"color: red;\"> Wykryto nieprawidłowości</span></h2>";
            else
                html += "<span style=\"color: green;\"> Brak nieprawidłowości</span></h2>";

            html += "<p style=\"margin-top: 50px;\">Liczba poprawnych elementów konfiguracji: " + $"{reportDetails.NumberOfTests - reportDetails.FaliureCount}/"+ $"{reportDetails.NumberOfTests}</p>";
            html += $"<p>Data wygenerowania dokumentu: {DateTime.Now}";

            html += "<h2 style=\"margin-top: 40px;\">Szczegółowe wyniki:</h2>";
            html += "<table style=\"width:100%; \"><tr><th><p>Typ paramtru</p></th><th><p>Nazwa</p></th><th><p>Czy jest poprawnie skonfigurowany</p></th></tr>";

            foreach (var testResult in testResults)
            {
                html += $"<tr><td><p>{testResult.Module}</p></td>";
                html += $"<td><p>{testResult.InsteadOfWhat}</p></td>";

                if (testResult.IsError)
                    html += $"<td><p>Nie</p></td></tr>";
                 else
                    html += $"<td><p>Tak</p></td></tr>";
            }

            renderer.RenderHtmlAsPdf(html).SaveAs("html-string.pdf");
        }
    }
}
