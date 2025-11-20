using DevExpress.Blazor.Reporting;
using DevExpress.XtraReports.UI;
using IDB.Business;
using IDB.Model;
using IDB.Reports;
using IDB.Reports.Reports;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IDB.UI.Components.Pages
{
    public partial class VorgangverwaltungReportViewer
    {
        [Parameter] public int? RowId { get; set; }
        [Parameter] public string SelectedIds { get; set; }

        AppState appState = new AppState();
        List<Column> _Columns = new List<Column>();
        List<Cell> _Cells = new List<Cell>();
        List<VorgangsverwaltungReportData> reportData = new List<VorgangsverwaltungReportData>();
        IDB.Business.Business obj = new IDB.Business.Business();

        private DxReportViewer _reportViewer;
        private XtraReport2 _Report;

        protected override void OnInitialized()
        {
            _Columns = obj.Get_AllColumns(appState.APIurl);
            _Cells = obj.Get_AllCellData(appState.APIurl);

            var reportData = PrepareReportData();

            _Report = new XtraReport2();
            _Report.DataSource = reportData;
        }

        private object PrepareReportData()
        {
            var allReportData = ConvertToReportData();

            if (RowId.HasValue)
            {
                var item = allReportData.FirstOrDefault(x => x.RowId == RowId.Value);
                return item != null ? new List<VorgangsverwaltungReportData> { item } : new List<VorgangsverwaltungReportData>();
            }
            else if (!string.IsNullOrEmpty(SelectedIds))
            {
                var ids = SelectedIds.Split(',').Select(int.Parse).ToList();
                return allReportData.Where(x => ids.Contains(x.RowId)).ToList();
            }
            else
            {
                return allReportData;
            }
        }
        private static bool? ParseNullableBool(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            value = value.Trim().ToLowerInvariant();
            if (value == "1" || value == "ja" || value == "true" || value == "x")
                return true;
            if (value == "0" || value == "nein" || value == "false" || value == "")
                return false;
            if (bool.TryParse(value, out var result))
                return result;
            return null;
        }

        private static DateTime? ParseNullableDate(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;
            if (DateTime.TryParse(value, out var result))
                return result;
            return null;
        }

        private List<VorgangsverwaltungReportData> ConvertToReportData()
        {
            var result = new List<VorgangsverwaltungReportData>();
            var tableColumns = _Columns.Where(c => c.Id_table == 30).ToList();
            var tableCells = _Cells.Where(c => c.Id_table == 30).ToList();
            var rowGroups = tableCells.GroupBy(cell => cell.Id_row);

            foreach (var rowGroup in rowGroups)
            {
                var reportItem = new VorgangsverwaltungReportData { RowId = rowGroup.Key };

                foreach (var cell in rowGroup)
                {
                    var column = tableColumns.FirstOrDefault(c => c.Id == cell.Id_column);
                    if (column == null) continue;

                    switch (column.Name)
                    {
                        case "Aktenzeichen":
                            reportItem.Aktenzeichen = cell.Data_value;
                            break;
                        case "Vorgang":
                            reportItem.Vorgang = cell.Data_value;
                            break;
                        case "Amt 1":
                            reportItem.Amt1 = cell.Data_value;
                            break;
                        case "Amt 2":
                            reportItem.Amt2 = cell.Data_value;
                            break;
                        case "Betreff":
                            reportItem.Betreff = cell.Data_value;
                            break;
                        case "Stichworte":
                            reportItem.Stichworte = cell.Data_value;
                            break;
                        case "Rechtsgrundlage":
                            reportItem.Rechtsgrundlage = cell.Data_value;
                            break;
                        case "TB-Nr.":
                            reportItem.TBNr = cell.Data_value;
                            break;
                        case "Ablage":
                            reportItem.Ablage = ParseNullableBool(cell.Data_value);
                            break;
                        case "TB-Beitrag":
                            reportItem.TBBeitrag = ParseNullableBool(cell.Data_value);
                            break;
                        case "Eingangsdatum":
                            reportItem.Eingangsdatum = ParseNullableDate(cell.Data_value);
                            break;
                        case "Ablagedatum":
                            reportItem.Ablagedatum = ParseNullableDate(cell.Data_value);
                            break;
                        case "WV-Datum":
                            reportItem.WVDatum = ParseNullableDate(cell.Data_value);
                            break;
                        default:
                            break;
                    }
                }
                result.Add(reportItem);
            }
            return result;
        }
    }
}