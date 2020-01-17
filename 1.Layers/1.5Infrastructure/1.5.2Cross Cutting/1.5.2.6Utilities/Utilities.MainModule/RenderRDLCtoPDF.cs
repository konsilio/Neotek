using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Data;

namespace Utilities.MainModule
{
    public class RenderRDLCtoPDF
    {
        public void ExportToPDF(string pathReport, string pathSave, List<ReportParameter> reportParams, List<DataTable> dtList, List<string> listName)
        {
            //string FileName = "File_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            // Setup the report viewer object and get the array of bytes
            var viewer = new ReportViewer();
            viewer.LocalReport.Refresh();
            viewer.LocalReport.ReportPath = pathReport;

            for(int i=0; i<=dtList.Count-1; i++)
            {
                viewer.LocalReport.DataSources.Add(new ReportDataSource(listName[i].ToString(), dtList[i]));
            }

            //viewer.LocalReport.ReportPath = "RDLC/Report1.rdlc"; //This is your rdlc name.
            viewer.LocalReport.SetParameters(reportParams);
            //viewer.ProcessingMode = ProcessingMode.Remote;
            //viewer.ServerReport.ReportServerUrl = new Uri("http://localhost/ReportServer");
            //viewer.ServerReport.ReportPath = "/" + path;
            //viewer.ServerReport.SetParameters(reportParams);

            byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            //byte[] bytes = viewer.ServerReport.Render("PDF", null, out mimeType, out encoding, out extension,out streamIds, out warnings);
            using (FileStream fs = File.Create(pathSave))
            {
                fs.Write(bytes, 0, bytes.Length);
            }

            #region Comentado
            //Response.ContentType = mimeType;
            //Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);
            //Response.WriteFile(Server.MapPath("~/image/" + FileName));
            //Response.Flush();

            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            //Response.Buffer = true;
            //Response.Clear();
            //Response.ContentType = mimeType;
            //Response.AddHeader("content-disposition", "attachment; filename=" + fileName + "." + extension);
            //Response.BinaryWrite(bytes); // create the file
            //Response.Flush(); // send it to the client to download
#endregion
        }

    }
}
