

namespace IDB.UI.Components.Pages

{
    using IDB.Business;
    using IDB.Model;


    partial class Overview
    {
        // State und Datenlisten für die Übersichtsseite
        AppState appState = new AppState();
        List<IDB> _IDBs = new List<IDB>();
        List<Column> _Columns = new List<Column>();
        List<Column> _ColumnsSingleIDB = new List<Column>();
        List<Cell> _CellSingleIDB = new List<Cell>();
        List<Cell> _Cells = new List<Cell>();

        int _ColumnChosen = 0;
        Business obj = new Business();

        private List<Dictionary<int, string>> TableRows = new();
        private bool IsLoading = true;
        private bool ZeigeVerzögerungsNachricht = false;

        // Wird beim Laden der Seite ausgeführt
        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;
            ZeigeVerzögerungsNachricht = false;
            
            // Nach 5 Sekunden Ladezeit zeigen wir eine lustige Nachricht an
            _ = Task.Run(async () =>
            {
                await Task.Delay(5000);
                if (IsLoading)
                {
                    ZeigeVerzögerungsNachricht = true;
                    await InvokeAsync(StateHasChanged);
                }
            });
            await Task.Delay(1); 

            // Daten von der API laden
            _IDBs = await obj.Get_AllIDBsAsync(appState.APIurl);
            _Columns = await obj.Get_AllColumnsAsync(appState.APIurl);
            _Cells = await obj.Get_AllCellDataAsync(appState.APIurl);

            IsLoading = false;
        }
        
        #region Button-Funktionen

        // Öffnet die Details-Ansicht einer IDB
        public void btnFilterAndOpen(int chosenId)
        {            
            navigationManager.NavigateTo($"details/{chosenId}");
        }
        
        // Öffnet die Bearbeitungsseite einer IDB
        public void btnOpenAndEditIDB(int chosenId)
        {
            BusinessService.chosenIDB = chosenId;
            navigationManager.NavigateTo($"editidb/{chosenId}");
        }
       
        #endregion
    }
}
