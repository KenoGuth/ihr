using IDB.Model;


namespace IDB.UI.Components.Pages
{
    using IDB.Business;

    public partial class InsertIDB
    {
        Business obj = new Business();
        private Model.IDB idb = new Model.IDB();
        private string? responseMessage;
        private int? countdown;
        private int id_newTable = 0;
        private int zaehler_spalten = 0;
        private List<string> dataTypes = new();
        private List<Column> columns = new List<Column>();

        private List<Model.IDB> tables = new List<Model.IDB>();
        
        // Wird beim Laden der Seite ausgeführt
        protected override void OnInitialized()
        {
            Business obj = new Business();
            tables = obj.Get_AllIDBs(appState.APIurl);
            AddAnotherColumn();
            dataTypes = obj.Get_Datentypen();
        }
       
        // Fügt eine neue Spalte zur Liste hinzu
        private void AddAnotherColumn()
        {
            columns.Add(new Column());
        }

        // Wird beim Absenden des Formulars aufgerufen
        private async Task HandleValidSubmit()
        {
            // Neue IDB in der Datenbank speichern
            id_newTable = obj.Insert_IDB(appState.APIurl, idb);
            if (id_newTable > 0)
            {
                SaveColumns();
                
                // Countdown anzeigen und dann Seite neu laden
                countdown = 5;
                responseMessage = "IDB erfolgreich hinzugefügt";
                for (int i = 0; i < 5; i++)
                {
                    await Task.Delay(1000);
                    countdown--;
                    StateHasChanged();
                }
                
                Navigation.NavigateTo(Navigation.Uri, true);
            }
            else { 
                responseMessage = "Das Einfügen einer neuen IDB hat nicht geklappt"; 
            }
        }
        
        // Speichert alle definierten Spalten für die neue IDB
        private void SaveColumns()
        {
            int columnzaehler = 1;
            bool responseMessage = false;
            
            foreach (Column col in columns)
            {
                Business obj = new Business();
                col.Column_no = columnzaehler;
                col.Id_table = id_newTable;

                responseMessage = obj.Insert_Column(appState.APIurl, col);
            }
            
            Console.WriteLine("Spalten gespeichert!");
            foreach (var col in columns)
            {
                Console.WriteLine($"Name: {col.Name}, Datentyp: {col.Data_type}, Nullable: {col.Is_nullable}, Standardwert: {col.Default_value}");
            }
        }
    }
}