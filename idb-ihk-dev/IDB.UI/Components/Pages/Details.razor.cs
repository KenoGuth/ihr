namespace IDB.UI.Components.Pages
{
    using DevExpress.XtraPrinting;
    using IDB.Business;
    using IDB.Model;
    using Microsoft.AspNetCore.Components;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Microsoft.JSInterop;
    using static System.Net.WebRequestMethods;
    // NEU: Explizite Referenz für ChangeEventArgs
    using ChangeEventArgs = Microsoft.AspNetCore.Components.ChangeEventArgs;

    partial class Details
    {
        [Parameter] public int selectedIDB { get; set; }
        AppState appState = new AppState();
        List<IDB> _IDBs = new List<IDB>();
        List<Cell> _Cells = new List<Cell>();
        List<Column> _Columns = new List<Column>();
        List<Column> _ColumnsSingleIDB = new List<Column>();
        List<Cell> _CellSingleIDB = new List<Cell>();
        private Dictionary<int, string> filterValues = new Dictionary<int, string>();
        IDB _IDB = new IDB();
        Business obj = new Business();
        private string? responseMessage;
        private List<Dictionary<int, string>> TableRows = new();

        
        private List<int> selectedReportIds = new List<int>();

        protected override void OnInitialized()
        {
            _IDBs = obj.Get_AllIDBs(appState.APIurl);
            _Columns = obj.Get_AllColumns(appState.APIurl);
            _Cells = obj.Get_AllCellData(appState.APIurl);
            _IDB = obj.Get_IDBbyID(appState.APIurl, selectedIDB);

            LoadTable(EventArgs.Empty);
            InitializeFilters();
        }

        public async Task LoadTable(EventArgs e)
        {
            int TableID = selectedIDB;
            if (TableID > 0)
            {
                _ColumnsSingleIDB.Clear();
                _ColumnsSingleIDB = (from Column in _Columns
                                     where Column.Id_table == TableID
                                     select Column)
                                     .OrderBy(c => c.Column_no)
                                     .ToList();
                _CellSingleIDB.Clear();
                _CellSingleIDB = (from Cell in _Cells
                                  where Cell.Id_table == TableID
                                  select Cell).ToList();

                await InvokeAsync(StateHasChanged);
            }
        }

        private void InitializeFilters()
        {
            filterValues.Clear();
            foreach (var column in _ColumnsSingleIDB)
            {
                filterValues[column.Id] = string.Empty;
            }
        }

        private void OnFilterChange()
        {
            selectedReportIds.Clear();
            StateHasChanged();
        }

        private void ClearAllFilters()
        {
            foreach (var column in _ColumnsSingleIDB)
            {
                filterValues[column.Id] = string.Empty;
            }
            selectedReportIds.Clear();
            StateHasChanged();
        }

        private IEnumerable<IGrouping<int, Cell>> GetFilteredRows()
        {
            var allRows = _CellSingleIDB.GroupBy(cell => cell.Id_row);

            bool hasActiveFilters = filterValues.Any(kv => !string.IsNullOrWhiteSpace(kv.Value));

            if (!hasActiveFilters)
            {
                return allRows.OrderBy(g => g.Key);
            }

            var filteredRows = allRows.Where(rowGroup =>
            {
                foreach (var column in _ColumnsSingleIDB)
                {
                    var filterValue = filterValues[column.Id];

                    if (string.IsNullOrWhiteSpace(filterValue))
                        continue;

                    var cell = rowGroup.FirstOrDefault(c => c.Id_column == column.Id);
                    var cellValue = cell?.Data_value ?? string.Empty;

                    if (!cellValue.Contains(filterValue, StringComparison.OrdinalIgnoreCase))
                        return false;
                }
                return true;
            });

            return filteredRows.OrderBy(g => g.Key);
        }

        private void ClearMessage()
        {
            responseMessage = null;
        }

        // Report Methoden
        //TODO: anpassen, dass mehrere IDBs reports haben können
        private void ToggleReportSelection(int rowId, object isChecked)
        {
            bool check = (bool)isChecked;

            if (check)
            {
                if (!selectedReportIds.Contains(rowId))
                    selectedReportIds.Add(rowId);
            }
            else
            {
                selectedReportIds.Remove(rowId);
            }
        }

        private void SelectAllReports(ChangeEventArgs e)
        {
            bool checkAll = (bool)e.Value;

            if (checkAll)
            {
                selectedReportIds = GetFilteredRows().Select(g => g.Key).ToList();
            }
            else
            {
                selectedReportIds.Clear();
            }
        }

        private void OpenSingleReport(int rowId)
        {
            Navigation.NavigateTo($"/11b-vorgangsverwaltung-report-viewer/single/{rowId}");
        }

        private void OpenAllReports()
        {
            var allVisibleRowIds = GetFilteredRows().Select(g => g.Key).ToList();

            if (allVisibleRowIds.Any())
            {
                var idsString = string.Join(",", allVisibleRowIds);
                Navigation.NavigateTo($"/11b-vorgangsverwaltung-report-viewer/selected/{idsString}");
            }
            else
            {
                responseMessage = "Keine Datensätze zum Erstellen eines Reports gefunden.";
            }
        }

        private void OpenSelectedReports()
        {
            if (selectedReportIds.Any())
            {
                var idsString = string.Join(",", selectedReportIds);
                Navigation.NavigateTo($"/11b-vorgangsverwaltung-report-viewer/selected/{idsString}");
            }
            else
            {
                responseMessage = "Bitte wählen Sie mindestens einen Datensatz für den Report aus.";
            }
        }
    }
}