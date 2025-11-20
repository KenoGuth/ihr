namespace IDB.UI.Components.Pages
{
    using DevExpress.XtraExport.Helpers;
    using IDB.Business;
    using IDB.Model;
    using IDB.UI.Components.Tools;
    using Microsoft.AspNetCore.Components;


    public partial class EditIDB
    {
        [Parameter] public int selectedIDB { get; set; }

        AppState appState = new AppState();
        IDB _IDB = new IDB();
        Business obj = new Business();
        private Column? columnToDelete;
        private string? responseMessage;
        bool isSuccess = false;
        bool _isChanged;
        private int? highlightedColumnId = null;
        private List<Column> _Columns = new List<Column>();
        private List<Model.Ausfuellhilfe> ausfuellhilfen = new List<Model.Ausfuellhilfe>();
        bool showConfirmationModal = false;
        bool showDeleteColModal = false;
        bool showDataTypeModal = false;
        bool showResetModal = false;
        bool showAddColumnForm = false;
        bool dataTypeChange = false;
        private List<string> dataTypes = new();
        private Column newCol = new();
        string newColumnName = "";
        string newColumnDataType = "varchar";
        int newColumnAusfuellhilfe;

        protected override async Task OnInitializedAsync()
        {
            _IDB = obj.Get_IDBbyID(appState.APIurl, selectedIDB);
            dataTypes = obj.Get_Datentypen();
            ausfuellhilfen = obj.Get_All_Ausfuellhilfe(appState.APIurl);
            LoadColumns();
        }

        private bool isChanged
        {
            get => _isChanged;
            set
            {
                if (_isChanged != value)
                {
                    _isChanged = value;

                    if (_isChanged)
                    {
                        isSuccess = false;
                        responseMessage = "Achtung! Sie haben ungespeicherte Änderungen. Bitte speichern Sie Ihren Fortschritt bevor Sie die Seite verlassen!";
                    }
                    else
                    {
                        responseMessage = null;
                    }
                }
            }
        }

        private void LoadColumns()
        {
            try
            {
                var allColumns = obj.Get_AllColumns(appState.APIurl);
                _Columns = allColumns.Where(c => c.Id_table == selectedIDB).ToList();
                isChanged = false;
                StateHasChanged();
                showResetModal = false;
            }
            catch (Exception ex)
            {
                responseMessage = $"Fehler beim Laden: {ex.Message}";
                isSuccess = false;
            }
        }

        private List<Column> GetActiveColumns()
        {
            return _Columns.Where(c => !c.Archived).OrderBy(c => c.Column_no).ToList();
        }

        private void NavigateBackToOverview()
        {
            if (!isChanged)
            {
                NavigationManager.NavigateTo("overview");
            }
            else
            {
                showConfirmationModal = true;
            }
        }
    private void ConfirmedNavigateBackToOverview()
        {
            
                NavigationManager.NavigateTo("overview");
            
        }

        private  async Task HandleValidSubmit()
        {
            try
            {
                bool result = obj.Edit_IDB(appState.APIurl, _IDB);

                if (result)
                {
                    responseMessage = "IDB erfolgreich gespeichert!";
                    isSuccess = true;

                    await Task.Delay(3000);
                    NavigationManager.NavigateTo("overview");
                    
                }
                else
                {
                    responseMessage = "Fehler beim Speichern der IDB.";
                    isSuccess = false;
                   
                }
            }
            catch (Exception ex)
            {
                responseMessage = $"Fehler: {ex.Message}";
                isSuccess = false;
               
            }
        }

        private void ShowAddColumnForm()
        {
            showAddColumnForm = true;
            newColumnName = "";
            newColumnDataType = "varchar";
            isChanged = true;
        }

        private void CancelAddColumn()
        {
            showAddColumnForm = false;
            isChanged = false;
        }

        private void AddNewColumn()
        {
            if (string.IsNullOrEmpty(newColumnName))
            {
                responseMessage = "Bitte einen Spaltennamen eingeben.";
                isSuccess = false;
                return;
            }

            try
            {

                List<Column> nextColumnNo;
                var columnsForThisTable = _Columns.Where(c => c.Id_table == selectedIDB);
                var activeColumnsForThisTable = columnsForThisTable.Where(c => c.Archived == false);
                nextColumnNo = GetActiveColumns();

                
                var newColumn = new Column
                {
                    Id_table = selectedIDB,
                    Name = newColumnName,
                    Data_type = newColumnDataType,
                    Column_no = nextColumnNo.Count+1,
                    Is_nullable = true,
                    Id_ausfuellhilfe = newColumnAusfuellhilfe,
                    Archived = false
                };

                bool result = obj.Insert_Column(appState.APIurl, newColumn);

                if (result)
                {
                    responseMessage = $"Spalte '{newColumnName}' wurde hinzugefügt.";
                    isSuccess = true;
                    showAddColumnForm = false;
                    isChanged = false;
                    LoadColumns();
                }
                else
                {
                    responseMessage = "Fehler beim Hinzufügen der Spalte.";
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                responseMessage = $"Fehler: {ex.Message}";
                isSuccess = false;
            }
        }
        #region Spalten verschieben

        private void MoveColumnUp(Column column)
        {
            var activeColumns = GetActiveColumns();
            var currentIndex = activeColumns.FindIndex(c => c.Id == column.Id);

            if (currentIndex > 0)
            {
                var previousColumn = activeColumns[currentIndex - 1];

                int temp = column.Column_no;
                column.Column_no = previousColumn.Column_no;
                previousColumn.Column_no = temp;
                isChanged = true;
                StateHasChanged();
            }
        }

        private void MoveColumnDown(Column column)
        {
            var activeColumns = GetActiveColumns();
            var currentIndex = activeColumns.FindIndex(c => c.Id == column.Id);

            if (currentIndex < activeColumns.Count - 1)
            {
                var nextColumn = activeColumns[currentIndex + 1];

                int temp = column.Column_no;
                column.Column_no = nextColumn.Column_no;
                nextColumn.Column_no = temp;

                highlightedColumnId = column.Id;
                isChanged = true;
                StateHasChanged();
            }
        }

        private void MoveColumnToBottom(Column column)
        {
            var activeColumns = GetActiveColumns();
            var currentIndex = activeColumns.FindIndex(c => c.Id == column.Id);

            var maxColumnNo = activeColumns.Max(c => c.Column_no);
            var originalColumnNo = column.Column_no;

            foreach (var col in activeColumns.Where(c => c.Column_no > originalColumnNo))
            {
                col.Column_no--;
            }

            column.Column_no = maxColumnNo;
            highlightedColumnId = column.Id;
            isChanged = true;
            StateHasChanged();
        }

        private void MoveColumnToTop(Column column)
        {
            var activeColumns = GetActiveColumns();
            var currentIndex = activeColumns.FindIndex(c => c.Id == column.Id);

            var originalColumnNo = column.Column_no;
            column.Column_no = 0;


            foreach (var col in activeColumns.Where(c => c.Column_no < originalColumnNo))
            {
                col.Column_no++;
            }

            highlightedColumnId = column.Id;
            isChanged = true;
            StateHasChanged();
        }
        #endregion
        private void ConfirmDelete(Column column)
        {
            columnToDelete = column;
            showDeleteColModal = true;
        }

        private async Task DeleteConfirmed()
        {
            if (columnToDelete != null)
            {
                DeleteColumn(columnToDelete);
                columnToDelete = null;
            }

            showDeleteColModal = false;
            await Task.CompletedTask;
        }
        private void DeleteColumn(Column column)
        {
            try
            {
                var activeColumns = GetActiveColumns();
                int? tempAlteNo = column.Column_no;
                column.Archived = true;
                int nextColumnNo = 1000;
                var columnsForThisTable = _Columns.Where(c => c.Id_table == selectedIDB);
                var archivedColumnsForThisTable = _Columns.Where(c => c.Archived == true);
                if (archivedColumnsForThisTable.Max(c => c.Column_no) > 1)
                {
                    nextColumnNo = archivedColumnsForThisTable.Max(c => c.Column_no) + 1;
                }
                column.Column_no = nextColumnNo;
                bool result = obj.Update_Column(appState.APIurl, column);

                foreach (var nextColumn in activeColumns)
                {
                    if (nextColumn.Column_no > tempAlteNo)
                    {
                        nextColumn.Column_no = nextColumn.Column_no - 1;
                    }
                }

                if (result)
                {
                    responseMessage = $"Spalte '{column.Name}' wurde gelöscht.";
                    isSuccess = true;
                    StateHasChanged();
                }
                else
                {
                    column.Archived = false;
                    responseMessage = "Fehler beim Löschen der Spalte.";
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                column.Archived = false;
                responseMessage = $"Fehler: {ex.Message}";
                isSuccess = false;
            }
        }

      
        private void SaveAllColumns()
        {
            try
            {
                int savedCount = 0;
                var activeColumns = GetActiveColumns();
                if (dataTypeChange)
                {
                    showDataTypeModal = true;
                    return;
                    
                }
                foreach (var column in activeColumns)
                {
                    column.Column_no = savedCount + 1;
                    bool result = obj.Update_Column(appState.APIurl, column);
                    if (result) savedCount++;
                }
               HandleValidSubmit();
                if (savedCount == activeColumns.Count)
                {
                    responseMessage = "Alle Spalten wurden gespeichert.";
                    isChanged = false;
                    isSuccess = true;
                }
                else
                {
                    responseMessage = $"Nur {savedCount} von {activeColumns.Count} Spalten wurden gespeichert.";
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                responseMessage = $"Fehler: {ex.Message}";
                isSuccess = false;
            }
        }

        private void ClearMessage()
        {
            responseMessage = null;
        }
    }
}