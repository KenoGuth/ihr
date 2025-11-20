namespace IDB.UI.Components.Pages
{
    using DevExpress.Blazor.Reporting;
   
    using DevExpress.XtraReports.UI;
    using IDB.Business;
    using IDB.Model;
    using IDB.Reports;
    using IDB.Reports.Reports;
    using Microsoft.AspNetCore.Components;
   
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Microsoft.JSInterop;
    using System.Text.Json;
    using static System.Net.WebRequestMethods;
    // NEU: Explizite Referenz für ChangeEventArgs
    using ChangeEventArgs = Microsoft.AspNetCore.Components.ChangeEventArgs;

    public partial class Export
    {
        [Parameter] public int? selectedIDB { get; set; }


        AppState appState = new AppState();
        List<Column> _Columns = new List<Column>();
        List<Cell> _Cells = new List<Cell>();
        
        Business obj = new();
        private DxReportViewer _reportViewer;
        private XtraReportExport? _Report;
        private object _reportData; // Feld umbenannt mit _
        private List<string> _columnNames = new List<string>(); // Auch konsistent mit _

        protected override void OnInitialized()
        {
            _Columns = obj.Get_AllColumns(appState.APIurl);
            _Cells = obj.Get_AllCellData(appState.APIurl);
            List<Column> columns = _Columns
                                   .Where(c => !c.Archived && c.Id_table == selectedIDB)
                                   .OrderBy(c => c.Column_no)
                                   .ToList();
            _columnNames = columns.Select(c => c.Name).ToList(); // Für UI-Debug
            _reportData = PrepareReportData(columns); // Für UI-Debug

            List<string> columnNames = columns.Select(c => c.Name).ToList();
            var reportData = PrepareReportData(columns);
            
            _Report = new XtraReportExport(columnNames);
            var dataTable = ConvertToDataTable(reportData as List<Dictionary<string, string>>, _columnNames);
            _Report.DataSource = dataTable;
            StateHasChanged();
        }

        private object PrepareReportData(List<Column> columns)
        {
      

            var reportRows = new List<Dictionary<string, string>>();
            var rowsGrouped = _Cells
                               .Where(cell => columns.Any(col => col.Id == cell.Id_column))
                                .GroupBy(cell => cell.Id_row)
                                .ToList();
            foreach (var group in rowsGrouped)
            {
                var dict = new Dictionary<string, string>();
                foreach (var column in columns)
                {
                    var cell = group.FirstOrDefault(c => c.Id_column == column.Id);
                    dict[column.Name] = cell?.Data_value ?? "";
                }
                reportRows.Add(dict);
            }
            return reportRows;
        }
        private System.Data.DataTable ConvertToDataTable(List<Dictionary<string, string>> data, List<string> columnNames)
        {
            var dataTable = new System.Data.DataTable();

            // Spalten hinzufügen
            foreach (var col in columnNames)
            {
                dataTable.Columns.Add(col, typeof(string));
            }

            // Daten hinzufügen
            foreach (var row in data)
            {
                var dataRow = dataTable.NewRow();
                foreach (var col in columnNames)
                {
                    if (row.ContainsKey(col))
                    {
                        dataRow[col] = row[col] ?? string.Empty;
                    }
                    else
                    {
                        dataRow[col] = string.Empty;
                    }
                }
                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }
    }
}