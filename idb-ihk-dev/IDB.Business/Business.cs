
namespace IDB.Business
{
    using IDB.Model;
    using IDB.DataAccess;

    // Business-Logik-Klasse für IDB-Verwaltung
    public class Business
    {
        List<string> datentypen = new List<string>();
        
        // Gibt die verfügbaren Datentypen zurück
        public List<string> Get_Datentypen()
        {
            datentypen.Add("Kurzer Text");
            datentypen.Add("Langer Text");
            datentypen.Add("Ganze Zahl");
            datentypen.Add("Komma Zahl"); 
            datentypen.Add("Checkbox");
            datentypen.Add("Datum");
            return datentypen;
        }
        
        // Speichert die aktuell ausgewählte IDB
        public int chosenIDB {  get; set; }
        
        // Ruft alle IDBs ab
        public List<IDB> Get_AllIDBs(string URLWebAPI)
        {
            DataAccess obj = new DataAccess();
            return obj.Get_AllIDBs(URLWebAPI);
        }  
        
        // Ruft Datei-Metadaten ab
        public List<FileUploadDTO> Get_FileMeta(string URLWebAPI)
        {
            DataAccess obj = new DataAccess();
            return obj.Get_FileMeta (URLWebAPI);
        }  
        
        // Asynchron alle IDBs abrufen
        public async Task<List<IDB>> Get_AllIDBsAsync(string URLWebAPI)
        {
            return await Task.Run(() =>
            {
                DataAccess obj = new DataAccess();
                return obj.Get_AllIDBs(URLWebAPI);
            });
        }
        
        // IDB nach ID abrufen
        public IDB Get_IDBbyID(string URLWebAPI, int id)
        {
            DataAccess obj = new DataAccess();
            return obj.Get_IDBbyID(URLWebAPI, id); 
        }
        
        // Alle Spalten abrufen
        public List<Column> Get_AllColumns(string URLWebAPI)
        {
            DataAccess obj = new DataAccess();
            return obj.Get_AllColumns(URLWebAPI);
        }
        
        // Asynchron alle Spalten abrufen
        public async Task<List<Column>> Get_AllColumnsAsync(string URLWebAPI)
        {
            return await Task.Run(() =>
            {
                DataAccess obj = new DataAccess();
                return obj.Get_AllColumns(URLWebAPI);
            });
        }

        // Alle Zelldaten abrufen
        public List<Cell> Get_AllCellData(string URLWebAPI)
        {
            DataAccess obj = new DataAccess();
            return obj.Get_AllCellData(URLWebAPI);
        }
        
        // Asynchron alle Zelldaten abrufen
        public async Task<List<Cell>> Get_AllCellDataAsync(string URLWebAPI)
        {
            return await Task.Run(() =>
            {
                DataAccess obj = new DataAccess();
                return obj.Get_AllCellData(URLWebAPI);
            });
        }

        // IDB bearbeiten
        public bool Edit_IDB(string URLWebAPI, IDB idb)
        {
            DataAccess obj = new DataAccess();
            return obj.Edit_IDB(URLWebAPI, idb);
        }
        
        // Neue IDB einfügen
        public int Insert_IDB(string URLWebAPI, IDB idb)
        {
            idb.CreatedAt = DateTime.Now;
            DataAccess obj = new DataAccess();
            return obj.Insert_IDB(URLWebAPI, idb);
        }
        
        // Neue Spalte einfügen
        public bool Insert_Column(string URLWebAPI, Column col)
        {
            DataAccess obj = new DataAccess();
            return obj.Insert_Column(URLWebAPI, col);
        }
        
        // Zelldaten einfügen
        public bool Insert_Celldata(string URLWebAPI, Cell cell)
        {
            DataAccess obj = new DataAccess();
            return obj.Insert_CellData(URLWebAPI, cell);
        }
        
        // Zelldaten aktualisieren
        public bool Update_Celldata(string URLWebAPI, Cell cell)
        {
            DataAccess obj = new DataAccess();
            return obj.Update_CellData(URLWebAPI, cell);
        }
        
        // Spalte aktualisieren
        public bool Update_Column(string URLWebAPI, Column col)
        {
            DataAccess obj = new DataAccess();
            return obj.Update_Column(URLWebAPI, col);
        }
        
        // Zelldaten-Zeile löschen
        public bool Delete_Celldatarow(string URLWebAPI, Cell cell)
        {
            DataAccess obj = new DataAccess();
            return obj.Delete_CellDataRow(URLWebAPI, cell);
        }
        
        // Datei löschen (asynchron)
        public async Task<bool> Delete_FileAsync(string URLWebAPI, FileUploadDTO file)
        {
            DataAccess obj = new DataAccess();
            return await obj.Delete_FileAsync(URLWebAPI, file);
        }
        
        // Datei hochladen
        public bool Upload_File(string URLWebAPI, FileUploadDTO filedto)
        {
            DataAccess obj = new DataAccess();
            return obj.Upload_File(URLWebAPI, filedto);
        }
        
        // Datei abrufen
        public FileUploadDTO Get_File(string URLWebAPI, FileUploadDTO filedto)
        {
            DataAccess obj = new DataAccess();
            return obj.Get_File(URLWebAPI, filedto);
        }
    }
}