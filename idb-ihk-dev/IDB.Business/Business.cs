
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
        public List<Ausfuellhilfe> Get_All_Ausfuellhilfe(string URLWebAPI)
        {
            DataAccess obj = new DataAccess();
            return obj.Get_Ausfuellhilfe(URLWebAPI);
        } 
        public List<AusfuellhilfeItem> Get_All_AusfuellhilfeItems(string URLWebAPI)
        {
            DataAccess obj = new DataAccess();
            return obj.Get_AusfuellhilfeItems(URLWebAPI);
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
            try
            {
               
                DataAccess obj = new DataAccess();
                return obj.Edit_IDB(URLWebAPI, idb);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int Insert_IDB(string URLWebAPI, IDB idb)
        {
            try
            {
                idb.CreatedAt = DateTime.Now;
                DataAccess obj = new DataAccess();
                return obj.Insert_IDB(URLWebAPI, idb);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int Insert_AusfuellhilfeName(string URLWebAPI, Ausfuellhilfe ausfuellhilfe)
        {
            try
            {
               
                DataAccess obj = new DataAccess();
                return obj.Insert_AusfuellhilfeName(URLWebAPI, ausfuellhilfe);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Insert_Column(string URLWebAPI, Column col)
        {
            try
            {
                DataAccess obj = new DataAccess();
                return obj.Insert_Column(URLWebAPI, col);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Insert_AusfuellhilfeItem(string URLWebAPI, AusfuellhilfeItem ausItem)
        {
            try
            {
                DataAccess obj = new DataAccess();
                return obj.Insert_AusfuellhilfeItem(URLWebAPI, ausItem);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Insert_Celldata(string URLWebAPI, Cell cell)
        {
            try
            {
                DataAccess obj = new DataAccess();
                return obj.Insert_CellData(URLWebAPI, cell);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Update_Celldata(string URLWebAPI, Cell cell)
        {
            try
            {
                DataAccess obj = new DataAccess();
                return obj.Update_CellData(URLWebAPI, cell);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Update_Column(string URLWebAPI, Column col)
        {
            try
            {
                DataAccess obj = new DataAccess();
                return obj.Update_Column(URLWebAPI, col);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Update_Ausfuellhilfe(string URLWebAPI, Ausfuellhilfe ausfuellhilfe)
        {
            try
            {
                DataAccess obj = new DataAccess();
                return obj.Update_Ausfuellhilfe(URLWebAPI, ausfuellhilfe);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Delete_Celldatarow(string URLWebAPI, Cell cell)
        {
            try
            {
                DataAccess obj = new DataAccess();
                return obj.Delete_CellDataRow(URLWebAPI, cell);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Delete_Ausfuellhilfe(string URLWebAPI, int id)
        {
            try
            {
                DataAccess obj = new DataAccess();
                return obj.Delete_Ausfuellhilfe(URLWebAPI, id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> Delete_FileAsync(string URLWebAPI, FileUploadDTO file)
        {
            try
            {
                DataAccess obj = new DataAccess();
                return await obj.Delete_FileAsync(URLWebAPI, file);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Upload_File(string URLWebAPI, FileUploadDTO filedto)
        {
            try
            {
                DataAccess obj = new DataAccess();
                return obj.Upload_File(URLWebAPI, filedto);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public FileUploadDTO Get_File(string URLWebAPI, FileUploadDTO filedto)
        {
            try
            {
                DataAccess obj = new DataAccess();
                return obj.Get_File(URLWebAPI, filedto);
            }
            catch (Exception)
            {

                throw;
            }
        }
        }
}