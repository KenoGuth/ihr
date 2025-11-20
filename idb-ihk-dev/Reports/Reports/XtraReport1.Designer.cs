namespace IDB.Reports.Reports
{
    partial class XtraReport1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraReport1));
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrPdfSignature1 = new DevExpress.XtraReports.UI.XRPdfSignature();
            this.xrRichText1 = new DevExpress.XtraReports.UI.XRRichText();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 254F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 254F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrPdfSignature1,
            this.xrRichText1});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 2380.403F;
            this.Detail.Name = "Detail";
            // 
            // xrPdfSignature1
            // 
            this.xrPdfSignature1.Dpi = 254F;
            this.xrPdfSignature1.LocationFloat = new DevExpress.Utils.PointFloat(973.6667F, 2078.567F);
            this.xrPdfSignature1.Name = "xrPdfSignature1";
            this.xrPdfSignature1.Padding = new DevExpress.XtraPrinting.PaddingInfo(21, 21, 21, 21, 254F);
            this.xrPdfSignature1.SignatureOptions.ImageDisplayMode = DevExpress.XtraPrinting.SignatureImageDisplayMode.ShowCertificateNameAsImage;
            this.xrPdfSignature1.SizeF = new System.Drawing.SizeF(508F, 254F);
            // 
            // xrRichText1
            // 
            this.xrRichText1.Dpi = 254F;
            this.xrRichText1.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F);
            this.xrRichText1.LocationFloat = new DevExpress.Utils.PointFloat(84.66666F, 14.81667F);
            this.xrRichText1.Name = "xrRichText1";
            this.xrRichText1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrRichText1.SerializableRtfString = resources.GetString("xrRichText1.SerializableRtfString");
            this.xrRichText1.SizeF = new System.Drawing.SizeF(1386.417F, 1908.387F);
            // 
            // xrLabel1
            // 
            this.xrLabel1.Dpi = 254F;
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(637.1166F, 2009.14F);
            this.xrLabel1.Multiline = true;
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(254F, 58.42F);
            this.xrLabel1.Text = "xrLabel1";
            // 
            // XtraReport1
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail});
            this.Dpi = 254F;
            this.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F);
            this.Margins = new DevExpress.Drawing.DXMargins(254F, 254F, 254F, 254F);
            this.PageHeight = 2970;
            this.PageWidth = 2100;
            this.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.SnapGridSize = 25F;
            this.Version = "24.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrRichText1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRPdfSignature xrPdfSignature1;
        private DevExpress.XtraReports.UI.XRRichText xrRichText1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
    }
}
