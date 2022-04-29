using DevExpress.XtraReports.UI;
using System.Data;
using System.Drawing;

namespace CDCNPM_XtraReport.Reports
{
    partial class MyReport
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
        private void InitializeComponent(DataTable dataSource, string title)
        {
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.Name = "Detail";
            // 
            // XtraReport
            // 
            this.DataSource = dataSource;
            InitBands(this);
            InitDetailsBaseXRTable(this, title);

            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail});
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Version = "19.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        private static void InitBands(XtraReport rep)
        {
            DetailBand detail = new DetailBand();
            PageHeaderBand pageHeader = new PageHeaderBand();
            ReportHeaderBand reportHeader = new ReportHeaderBand();
            ReportFooterBand reportFooter = new ReportFooterBand();
            BottomMarginBand bottomMargin = new BottomMarginBand();
            reportHeader.HeightF = 40;
            detail.HeightF = 20;
            reportFooter.HeightF = 380;
            pageHeader.HeightF = 20;
            rep.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] { reportHeader, detail, pageHeader, reportFooter, bottomMargin });
        }

        private static void InitDetailsBaseXRTable(XtraReport rep, string tit = "")
        {
            var dt = ((DataTable)rep.DataSource);
            int colCount = dt.Columns.Count;
            int colWidth = (rep.PageWidth - (rep.Margins.Left + rep.Margins.Right)) / colCount;
            rep.Margins = new System.Drawing.Printing.Margins(10, 10, 10, 20);
            XRLabel title = new XRLabel();
            title.Text = string.IsNullOrEmpty(tit) ? "" : tit.ToUpper();
            title.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            title.ForeColor = Color.Red;
            title.Font = new Font("Tahoma", 20, FontStyle.Bold, GraphicsUnit.Pixel);
            title.Width = Convert.ToInt32(rep.PageWidth - 50);

            // Create a table to represent headers
            XRTable tableHeader = new XRTable();
            tableHeader.Height = 40;
            tableHeader.BackColor = Color.PeachPuff;
            tableHeader.ForeColor = Color.Black;
            tableHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            tableHeader.Font = new Font("Tahoma", 12, FontStyle.Bold, GraphicsUnit.Pixel);
            tableHeader.Width = (rep.PageWidth - (rep.Margins.Left + rep.Margins.Right));
            tableHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5, 100.0F);
            XRTableRow headerRow = new XRTableRow();
            headerRow.Width = tableHeader.Width;
            tableHeader.Rows.Add(headerRow);
            tableHeader.BeginInit();

            /*Create a table to display data*/
            XRTable tableDetail = new XRTable();
            tableDetail.Height = 20;
            tableDetail.Width = (rep.PageWidth - (rep.Margins.Left + rep.Margins.Right));
            tableDetail.Font = new Font("Tahoma", 12, FontStyle.Regular, GraphicsUnit.Pixel);
            XRTableRow detailRow = new XRTableRow();
            detailRow.Width = tableDetail.Width;
            tableDetail.Rows.Add(detailRow);
            tableDetail.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5, 100.0F);

            tableDetail.BeginInit();
            tableDetail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            tableHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            /*Create table cells, fill the header cells with text, bind the cells to data*/
            for (int i = 0; i < colCount; i++)
            {

                XRTableCell headerCell = new XRTableCell();
                headerCell.Text = dt.Columns[i].Caption;
                XRTableCell detailCell = new XRTableCell();
                detailCell.DataBindings.Add("Text", null, dt.Columns[i].Caption);

                detailCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                headerCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                detailCell.StylePriority.UseBackColor = false;
                detailCell.StylePriority.UseBorderColor = false;
                detailCell.StylePriority.UseBorders = false;
                detailCell.StylePriority.UseTextAlignment = false;
                detailCell.Weight = 0.26012620811029052D;

                headerCell.StylePriority.UseBackColor = false;
                headerCell.StylePriority.UseBorderColor = false;
                headerCell.StylePriority.UseBorders = false;
                headerCell.StylePriority.UseTextAlignment = false;
                headerCell.Weight = 0.26012620811029052D;

                headerCell.Borders = DevExpress.XtraPrinting.BorderSide.All;
                detailCell.Borders = DevExpress.XtraPrinting.BorderSide.All;
                /*Place the cells into the corresponding tables*/
                headerRow.Cells.Add(headerCell);
                detailRow.Cells.Add(detailCell);
            }

            tableHeader.EndInit();
            tableDetail.EndInit();

            // ReportFooter
            // 
            XRLabel xrTotal = new XRLabel();
            XRLabel xrTotalText = new XRLabel();

            // 
            // total
            // 
            xrTotal.LocationFloat = new DevExpress.Utils.PointFloat(50.50002F, 10.00002F);
            xrTotal.Multiline = true;
            xrTotal.Name = "xrLabel2";
            xrTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            xrTotal.SizeF = new System.Drawing.SizeF(60.69553F, 23F);
            xrTotal.StylePriority.UseTextAlignment = false;
            xrTotal.Text = dt.Rows.Count.ToString();
            xrTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            xrTotal.TextFormatString = "{0:#}";
            // 
            // txtTotal
            // 
            xrTotalText.LocationFloat = new DevExpress.Utils.PointFloat(1F, 10.00002F);
            xrTotalText.Multiline = true;
            xrTotalText.Name = "xrLabel1";
            xrTotalText.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            xrTotalText.SizeF = new System.Drawing.SizeF(87.5F, 23F);
            xrTotalText.StylePriority.UseTextAlignment = false;
            xrTotalText.Text = "Total row:";
            xrTotalText.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            var pageInfo1 = new XRPageInfo();
            var pageInfo2 = new XRPageInfo();
            // pageInfo1
            // 
            pageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(20F, 4F);
            pageInfo1.Name = "pageInfo1";
            pageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            pageInfo1.SizeF = new System.Drawing.SizeF(355.5F, 23F);
            pageInfo1.StyleName = "PageInfo";
            // 
            // pageInfo2
            // 
            pageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(430F, 4F);
            pageInfo2.Name = "pageInfo2";
            pageInfo2.SizeF = new System.Drawing.SizeF(355.5F, 23F);
            pageInfo2.StyleName = "PageInfo";
            pageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            pageInfo2.TextFormatString = "Page {0} of {1}";
            // 

            /*Place the table onto a report's Detail band*/
            rep.Bands[BandKind.ReportHeader].Controls.Add(title);
            rep.Bands[BandKind.PageHeader].Controls.Add(tableHeader);
            rep.Bands[BandKind.Detail].Controls.Add(tableDetail);
            rep.Bands[BandKind.ReportFooter].Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            xrTotal,
            xrTotalText});
            rep.Bands[BandKind.BottomMargin].Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            pageInfo1,
            pageInfo2});
        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
    }
}
