using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace CDCNPM_XtraReport.Reports
{
    public partial class MyReport : DevExpress.XtraReports.UI.XtraReport
    {
        public MyReport(DataTable dataSource, string title)
        {
            InitializeComponent(dataSource, title);
        }
    }
}
