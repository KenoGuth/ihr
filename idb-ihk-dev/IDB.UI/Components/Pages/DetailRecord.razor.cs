namespace IDB.UI.Components.Pages
{
    using DevExpress.Blazor;
    using IDB.Business;
    using IDB.Model;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Forms;

    partial class DetailRecord
    {
        private string AppendVersion(string path) => $"{path}?v={typeof(DevExpress.Blazor.ResourcesConfigurator).Assembly.GetName().Version}";
        [Parameter] public int selectedIDB { get; set; }
        [Parameter] public int? selectedRowID { get; set; }
        private List<Model.Ausfuellhilfe> ausfuellhilfe = new();
        private List<Model.AusfuellhilfeItem> ausfuellhilfeItems = new();
        private AppState appState = new AppState();
        private List<Column> _ColumnsSingleIDB = new List<Column>();
        private List<Cell> _CellSingleIDB = new List<Cell>();
        private Dictionary<int, string> recordData = new Dictionary<int, string>();
        private Dictionary<int, string> originalData = new Dictionary<int, string>();
        private Dictionary<int, string> validationErrors = new Dictionary<int, string>();
        AccordionExpandMode ExpandMode { get; set; } = AccordionExpandMode.SingleOrNone;
        DxAccordion Accordion;
        AccordionExpandCollapseAction ExpandCollapseAction { get; set; } = AccordionExpandCollapseAction.HeaderClick;
        private int columnID = 0;
        private InputFile inputFile;
        private const int maxFileSize = 15000000; // 15MB
        private const int chunkSize = 32768; // 32KB chunks
        private List<FileUploadDTO> files = new();
        private IDB _IDB = new IDB();
        private Business obj = new Business();
        private string? responseMessage;
        private bool isSuccess = false;
        private bool isProcessing = false;
        private bool isLoading = false;
        private bool isPdfViewer = false;
        private bool showDeleteModal = false;
        private bool _isChanged;
        private string? uploadMessage = null;
        private FileUploadDTO? viewFile = null;

        private bool IsEditMode => selectedRowID.HasValue && selectedRowID.Value > 0;
        async Task OnInputFileChange(InputFileChangeEventArgs e)
        {

            try
            {
                //isLoading = true;
                //await InvokeAsync(StateHasChanged);

                foreach (var file in e.GetMultipleFiles(e.FileCount))
                {
                    var memoryStream = new MemoryStream();
                    await file.OpenReadStream(10240000).CopyToAsync(memoryStream);

                    FileUploadDTO anhang = new FileUploadDTO
                    {
                        ContentType = file.Name.Split('.')[file.Name.Split('.').Length - 1],
                        FileName = file.Name,
                        FileData = memoryStream.ToArray(),
                        UploadedBy = "Keno",
                        FileSize = file.Size,
                        UploadDate = DateTime.Now,
                        PlannedStoragePath = selectedIDB + "/" + selectedRowID
                    };
                    obj.Upload_File(appState.APIurl, anhang);
                    files.Add(anhang);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            isLoading = false;
            await InvokeAsync(StateHasChanged);

        }

        protected override async Task OnInitializedAsync()
        {
            ausfuellhilfe = obj.Get_All_Ausfuellhilfe(appState.APIurl);
            ausfuellhilfeItems = obj.Get_All_AusfuellhilfeItems(appState.APIurl);
            files = GetFiles();
            await LoadTableStructure();
            await LoadRecordData();
        }
        private void ViewFile(FileUploadDTO file)
        {
            viewFile = obj.Get_File(appState.APIurl, file);
            if(viewFile != null) { isPdfViewer = true; }
        }
        private void ClosePdfViewer() { isPdfViewer = false; }
        private List<FileUploadDTO> GetFiles()
        {
            return obj.Get_FileMeta(appState.APIurl)
                 .Where(c => c.PlannedStoragePath == selectedIDB + "/" + selectedRowID)
                 .ToList();
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


        private async Task LoadTableStructure()
        {
            try
            {
                var allColumns = obj.Get_AllColumns(appState.APIurl);
                _IDB = obj.Get_IDBbyID(appState.APIurl, selectedIDB);

                _ColumnsSingleIDB = allColumns
                    .Where(column => column.Id_table == selectedIDB)
                    .OrderBy(column => column.Id)
                    .ToList();

                foreach (var column in _ColumnsSingleIDB)
                {
                    recordData[column.Id] = string.Empty;
                    originalData[column.Id] = string.Empty;
                }

                await InvokeAsync(StateHasChanged);
            }
            catch (Exception ex)
            {
                responseMessage = $"Fehler beim Laden der Tabellenstruktur: {ex.Message}";
                isSuccess = false;
            }
        }

        private async Task LoadRecordData()
        {
            if (!IsEditMode) return;

            try
            {


                var allCells = obj.Get_AllCellData(appState.APIurl);
                _CellSingleIDB = allCells
                    .Where(cell => cell.Id_table == selectedIDB && cell.Id_row == selectedRowID.Value)
                    .ToList();

                foreach (var cell in _CellSingleIDB)
                {
                    if (recordData.ContainsKey(cell.Id_column))
                    {
                        recordData[cell.Id_column] = cell.Data_value ?? string.Empty;
                        originalData[cell.Id_column] = cell.Data_value ?? string.Empty;
                    }
                }

                await InvokeAsync(StateHasChanged);
            }
            catch (Exception ex)
            {
                responseMessage = $"Fehler beim Laden der Datensatzdaten: {ex.Message}";
                isSuccess = false;
            }
        }
        private async Task Delete_FileAsync(FileUploadDTO file)
        {
            await obj.Delete_FileAsync(appState.APIurl, file);
            files = GetFiles();
            StateHasChanged();
        }
        private async Task SaveRecord()
        {
            if (isProcessing) return;

            ClearMessage();
            validationErrors.Clear();

            if (!ValidateForm()) return;

            isProcessing = true;
            await InvokeAsync(StateHasChanged);

            try
            {
                if (IsEditMode)
                {
                    await UpdateExistingRecord();
                }
                else
                {
                    await CreateNewRecord();
                }
            }
            catch (Exception ex)
            {
                responseMessage = $"Fehler beim Speichern: {ex.Message}";
                isSuccess = false;
            }
            finally
            {
                isProcessing = false;
                await InvokeAsync(StateHasChanged);
            }
        }

        private async Task CreateNewRecord()
        {
            var allCells = obj.Get_AllCellData(appState.APIurl);
            var tableCells = allCells.Where(cell => cell.Id_table == selectedIDB).ToList();
            int nextRowId = tableCells.Any() ? tableCells.Max(c => c.Id_row) + 1 : 1;

            bool allSuccess = true;
            var failedColumns = new List<string>();

            foreach (var column in _ColumnsSingleIDB)
            {
                string value = recordData[column.Id];

                var newCell = new Cell
                {
                    Id_table = selectedIDB,
                    Id_row = nextRowId,
                    Id_column = column.Id,
                    Data_value = string.IsNullOrWhiteSpace(value) ? null : value
                };

                bool result = obj.Insert_Celldata(appState.APIurl, newCell);
                if (!result)
                {
                    allSuccess = false;
                    failedColumns.Add(column.Name);
                }
            }

            if (allSuccess)
            {
                responseMessage = "Datensatz erfolgreich erstellt.";
                isSuccess = true;
                await Task.Delay(1000);
                Navigation.NavigateTo($"/detailrecord/{selectedIDB}/{nextRowId}");
            }
            else
            {
                responseMessage = $"Fehler beim Erstellen des Datensatzes. Probleme bei: {string.Join(", ", failedColumns)}";
                isSuccess = false;
            }
        }

        private async Task UpdateExistingRecord()
        {
            bool anyChanges = false;
            bool allSuccess = true;
            var failedColumns = new List<string>();

            foreach (var column in _ColumnsSingleIDB)
            {
                string currentValue = recordData[column.Id];
                string originalValue = originalData[column.Id];

                if (currentValue != originalValue)
                {
                    anyChanges = true;

                    var updatedCell = new Cell
                    {
                        Id_table = selectedIDB,
                        Id_row = selectedRowID.Value,
                        Id_column = column.Id,
                        Data_value = string.IsNullOrWhiteSpace(currentValue) ? null : currentValue
                    };

                    bool result = obj.Update_Celldata(appState.APIurl, updatedCell);

                    if (result)
                    {
                        originalData[column.Id] = currentValue;
                    }
                    else
                    {
                        allSuccess = false;
                        failedColumns.Add(column.Name);
                    }
                }
            }

            if (!anyChanges)
            {
                responseMessage = "Keine Änderungen erkannt.";
                isSuccess = true;
            }
            else if (allSuccess)
            {
                responseMessage = "Änderungen erfolgreich gespeichert.";
                isSuccess = true;
            }
            else
            {
                responseMessage = $"Fehler beim Speichern der Änderungen. Probleme bei: {string.Join(", ", failedColumns)}";
                isSuccess = false;
            }
        }

        private async Task DeleteRecord()
        {
            if (!IsEditMode) return;

            HideDeleteConfirmation();
            isProcessing = true;
            await InvokeAsync(StateHasChanged);

            try
            {
                var cellToDelete = new Cell
                {
                    Id_table = selectedIDB,
                    Id_row = selectedRowID.Value,
                    Id_column = 0
                };

                bool result = obj.Delete_Celldatarow(appState.APIurl, cellToDelete);

                if (result)
                {
                    responseMessage = "Datensatz erfolgreich gelöscht.";
                    isSuccess = true;
                    await Task.Delay(1500);
                    Navigation.NavigateTo($"/details/{selectedIDB}");
                }
                else
                {
                    responseMessage = "Fehler beim Löschen des Datensatzes.";
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                responseMessage = $"Fehler beim Löschen: {ex.Message}";
                isSuccess = false;
            }
            finally
            {
                isProcessing = false;
                await InvokeAsync(StateHasChanged);
            }
        }

        private bool ValidateForm()
        {
            bool isValid = true;

            foreach (var column in _ColumnsSingleIDB)
            {
                string value = recordData[column.Id];


                if (!string.IsNullOrWhiteSpace(value))
                {
                    string dataType = GetColumnDataType(column);

                    if (dataType == "number" && !IsNumeric(value))
                    {
                        validationErrors[column.Id] = $"{column.Name} muss eine gültige Zahl sein.";
                        isValid = false;
                    }
                    else if (dataType == "email" && !IsValidEmail(value))
                    {
                        validationErrors[column.Id] = $"{column.Name} muss eine gültige E-Mail-Adresse sein.";
                        isValid = false;
                    }
                }
            }

            return isValid;
        }

        private void ResetForm()
        {
            foreach (var column in _ColumnsSingleIDB)
            {
                recordData[column.Id] = originalData[column.Id];
            }
            validationErrors.Clear();
            ClearMessage();
        }

        private void ShowDeleteConfirmation()
        {
            showDeleteModal = true;
        }

        private void HideDeleteConfirmation()
        {
            showDeleteModal = false;
        }

        private void NavigateBack()
        {
            Navigation.NavigateTo($"/details/{selectedIDB}");
        }

        private void ClearMessage()
        {
            responseMessage = null;
        }

        private string GetPageTitle()
        {
            return "";
            // return IsEditMode ? "Datensatz bearbeiten" : "Neuen Datensatz erstellen";
        }

        private string GetValidationClass(int columnId)
        {
            return validationErrors.ContainsKey(columnId) ? "is-invalid" : "";
        }

        private string GetPlaceholder(Column column)
        {
            return $"{column.Name} eingeben...";
        }



        private string FormatDateForInput(object columnIdKey)
        {
            var key = (int)columnIdKey;
            if (recordData.TryGetValue(key, out var value) && value is string dateStr)
            {
                if (System.DateTime.TryParseExact(dateStr, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var dt))
                {
                    return dt.ToString("yyyy-MM-dd");
                }
                if (System.DateTime.TryParseExact(dateStr, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out _))
                {
                    return dateStr;
                }
            }
            return string.Empty;
        }

        private void HandleDateChange(ChangeEventArgs e, int columnId)
        {
            var isoDate = e.Value?.ToString();
            if (!string.IsNullOrEmpty(isoDate))
            {
                if (System.DateTime.TryParseExact(isoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var dt))
                {
                    recordData[columnId] = dt.ToString("dd.MM.yyyy");
                }
                else
                {
                    recordData[columnId] = string.Empty;
                }
            }
            else
            {
                recordData[columnId] = string.Empty;
            }
        }

        private string GetColumnDataType(Column column)
        {
            string type = column.Data_type?.ToLower() ?? "";

            if (type.Contains("email") || type.Contains("mail"))
                return "email";
            if (type.Contains("datetime") || type.Contains("datum"))
                return "date";
            if (type.Contains("int") || type.Contains("ganze zahl") || type.Contains("preis"))
                return "number";
            if (type.Contains("varchar") || type.Contains("kommentar") || type.Contains("langer text"))
                return "textarea";
            if (type.Contains("komma"))
                return "decimal";
            if (type.Contains("checkbox"))
                return "checkbox";

            return "text";
        }

        private bool IsNumeric(string value)
        {
            return decimal.TryParse(value, out _);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}