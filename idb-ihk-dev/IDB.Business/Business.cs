
namespace IDB.Business
{
    using IDB.Model;
    using IDB.DataAccess;


 public class Business
    {
        List<string> datentypen = new List<string>();
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
        public int chosenIDB {  get; set; }
        public List<IDB> Get_AllIDBs(string URLWebAPI)
        {
            DataAccess obj = new DataAccess();
            return obj.Get_AllIDBs(URLWebAPI);
        }  
        public List<FileUploadDTO> Get_FileMeta(string URLWebAPI)
        {
            DataAccess obj = new DataAccess();
            return obj.Get_FileMeta (URLWebAPI);
        }  
        public async Task<List<IDB>> Get_AllIDBsAsync(string URLWebAPI)
        {
            return await Task.Run(() =>
            {
                DataAccess obj = new DataAccess();
                return obj.Get_AllIDBs(URLWebAPI);
            });
        }
        public IDB Get_IDBbyID(string URLWebAPI, int id)
        {
            DataAccess obj = new DataAccess();
            return obj.Get_IDBbyID(URLWebAPI, id); 
        }
        public List<Column> Get_AllColumns(string URLWebAPI)
        {
            DataAccess obj = new DataAccess();
            return obj.Get_AllColumns(URLWebAPI);
            
        }
        public async Task<List<Column>> Get_AllColumnsAsync(string URLWebAPI)
        {
            return await Task.Run(() =>
            {
                DataAccess obj = new DataAccess();
                return obj.Get_AllColumns(URLWebAPI);
            });
        }

        public List<Cell> Get_AllCellData(string URLWebAPI)
        {
            DataAccess obj = new DataAccess();
            return obj.Get_AllCellData(URLWebAPI);
        }
        public async Task<List<Cell>> Get_AllCellDataAsync(string URLWebAPI)
        {
            return await Task.Run(() =>
            {
                DataAccess obj = new DataAccess();
                return obj.Get_AllCellData(URLWebAPI);
            });
        }

        public bool Edit_IDB(string URLWebAPI, IDB idb)
        {
            DataAccess obj = new DataAccess();
            return obj.Edit_IDB(URLWebAPI, idb);
        }
        public int Insert_IDB(string URLWebAPI, IDB idb)
        {
            idb.CreatedAt = DateTime.Now;
            DataAccess obj = new DataAccess();
            return obj.Insert_IDB(URLWebAPI, idb);
        }
        public bool Insert_Column(string URLWebAPI, Column col)
        {
            DataAccess obj = new DataAccess();
            return obj.Insert_Column(URLWebAPI, col);
        }
        public bool Insert_Celldata(string URLWebAPI, Cell cell)
        {
            DataAccess obj = new DataAccess();
            return obj.Insert_CellData(URLWebAPI, cell);
        }
        public bool Update_Celldata(string URLWebAPI, Cell cell)
        {
            DataAccess obj = new DataAccess();
            return obj.Update_CellData(URLWebAPI, cell);
        }
        public bool Update_Column(string URLWebAPI, Column col)
        {
            DataAccess obj = new DataAccess();
            return obj.Update_Column(URLWebAPI, col);
        }
        public bool Delete_Celldatarow(string URLWebAPI, Cell cell)
        {
            DataAccess obj = new DataAccess();
            return obj.Delete_CellDataRow(URLWebAPI, cell);
        }
        public async Task<bool> Delete_FileAsync(string URLWebAPI, FileUploadDTO file)
        {
            DataAccess obj = new DataAccess();
            return await obj.Delete_FileAsync(URLWebAPI, file);
        }
        public bool Upload_File(string URLWebAPI, FileUploadDTO filedto)
        {
            DataAccess obj = new DataAccess();
            return obj.Upload_File(URLWebAPI, filedto);
        }
        public FileUploadDTO Get_File(string URLWebAPI, FileUploadDTO filedto)
        {
            DataAccess obj = new DataAccess();
            return obj.Get_File(URLWebAPI, filedto);
        }
        }
}