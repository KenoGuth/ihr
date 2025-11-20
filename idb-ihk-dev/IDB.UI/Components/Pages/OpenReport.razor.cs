using DevExpress.Blazor.Reporting;
using DevExpress.XtraReports.UI;

namespace IDB.UI.Components.Pages;

public partial class OpenReport
{
   private DxReportViewer _reportViewer;

    XtraReport _Report
    {
        get
        {
            return XtraReport.FromFile(Path.Combine(Directory.GetCurrentDirectory(), @"Report/XtraReport1.repx"));
          
        }
    }
}
