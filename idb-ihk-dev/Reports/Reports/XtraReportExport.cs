using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.XtraPrinting;
using DevExpress.Drawing.Printing; // ← Wichtig für DXPaperKind

namespace IDB.Reports.Reports
{
    public partial class XtraReportExport : XtraReport
    {
        public XtraReportExport(List<string> columnNames)
        {
            InitializeComponent();

            // 1. QUERFORMAT einstellen (korrigierter Typ)
            this.PaperKind = DXPaperKind.A4; // ← DevExpress-Typ verwenden
            this.Landscape = true;

            // 2. PageHeader-Band erstellen (falls nicht vorhanden)
            if (this.Bands[BandKind.PageHeader] == null)
            {
                var pageHeaderBand = new PageHeaderBand();
                this.Bands.Add(pageHeaderBand);
            }

            // 3. HEADER-TABELLE (nur einmal, im PageHeader-Band)
            XRTable headerTable = new XRTable();
            headerTable.WidthF = this.PageWidth - this.Margins.Left - this.Margins.Right;
            headerTable.Borders = BorderSide.All;
            headerTable.LocationF = new DevExpress.Utils.PointFloat(0, 0);

            XRTableRow headerRow = new XRTableRow();
            foreach (var col in columnNames)
            {
                var headerCell = new XRTableCell
                {
                    Text = col,
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    BackColor = Color.LightGray,
                    TextAlignment = TextAlignment.MiddleCenter
                };
                headerRow.Cells.Add(headerCell);
            }
            headerTable.Rows.Add(headerRow);

            // Header-Tabelle ins PageHeader-Band
            var pageHeader = (PageHeaderBand)this.Bands[BandKind.PageHeader];
            pageHeader.Controls.Add(headerTable);
            pageHeader.HeightF = 30;

            // 4. DETAIL-TABELLE (wird für jede Datenzeile wiederholt)
            XRTable detailTable = new XRTable();
            detailTable.WidthF = this.PageWidth - this.Margins.Left - this.Margins.Right;
            detailTable.Borders = BorderSide.All;
            detailTable.LocationF = new DevExpress.Utils.PointFloat(0, 0);

            XRTableRow detailRow = new XRTableRow();
            foreach (var col in columnNames)
            {
                var detailCell = new XRTableCell();
                detailCell.DataBindings.Add(new XRBinding("Text", null, col));
                detailCell.Font = new Font("Arial", 10, FontStyle.Regular);
                detailCell.TextAlignment = TextAlignment.MiddleLeft;
                detailRow.Cells.Add(detailCell);
            }
            detailTable.Rows.Add(detailRow);

            // Detail-Tabelle ins Detail-Band
            this.Detail.Controls.Add(detailTable);
            this.Detail.HeightF = 25;
        }
    }
}