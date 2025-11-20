using IDB.Service.Klassen;
using Microsoft.AspNetCore.Mvc;

namespace IDB.Service.Controllers
{
    using IDB.Model;
    [ApiController]
    [Route("[controller]")]
    public class IDBController : ControllerBase
    {
        #region [HttpGet]
        [HttpPost("get_file")]
        public FileUploadDTO Get_File(FileUploadDTO file)
        {
            IDBExtended obj = new IDBExtended();
            return obj.Get_File(file);
        }

        [HttpGet("AllIDBs")]
        public List<IDB> Get_AllIDBs()
        {
            IDBExtended obj = new IDBExtended();
            return obj.Get_AllIDBs();
        }
        [HttpGet("AllFileMeta")]
        public List<FileUploadDTO> Get_FileMeta()
        {
            IDBExtended obj = new IDBExtended();
            return obj.Get_FileMeta();
        }
        [HttpGet("All_Ausfuellhilfe")]
        public List<Ausfuellhilfe> Get_All_Ausfuellhilfen()
        {
            IDBExtended obj = new IDBExtended();
            return obj.Get_All_Ausfuellhilfen();
        } 
        [HttpGet("All_Ausfuellhilfe_Item")]
        public List<AusfuellhilfeItem> Get_All_Ausfuellhilfen_Items()
        {
            IDBExtended obj = new IDBExtended();
            return obj.Get_All_Ausfuellhilfen_Items();
        }
        [HttpGet("IDBbyId/{Id}")]
        public IDB Get_IDBbyId(string Id)
        {
            IDBExtended obj = new IDBExtended();
            return obj.Get_IDBbyId(Convert.ToInt32(Id));
        }

        [HttpGet("AllColumns")]
        public List<Column> Get_AllColumns()
        {
            IDBExtended obj = new IDBExtended();
            return obj.Get_AllColumns();
        }


        [HttpGet("AllCellData")]
        public List<Cell> Get_AllCellData()
        {
            IDBExtended obj = new IDBExtended();
            return obj.Get_AllCellData();
        }
        #endregion

        #region [HttpPost]
        #region Insert

        [HttpPost("Insert")]
        public ActionResult<int> Insert_IDB(IDB idb)
        {
            IDBExtended obj = new IDBExtended();
            int returnId = obj.Insert_IDB(idb);
            if (returnId > 0)
            {
                return Ok(returnId); 
            }
            else
            {
                return BadRequest("Einfuegen fehlgeschlagen."); 
            }
        }
        [HttpPost("Insert_AusfuellhilfeName")]
        public int Insert_AusfuellhilfeName(Ausfuellhilfe ausfuellhilfe)
        {
          
            IDBExtended obj = new IDBExtended();
            int returnId = obj.Insert_AusfuellhilfeName(ausfuellhilfe);
            if (returnId > 0)
            {
                return returnId; 
            }
            else
            {
                return 0; 
            }
        }
        [HttpPost("Insert_Column")]
        public string Insert_Column(Column col)
        {
            IDBExtended obj = new IDBExtended();
            if (obj.Insert_Column(col))
            {
                return "OK";
            }
            else
            {
                return "ERROR";
            }
        }
        [HttpPost("Insert_AusfuellhilfeItem")]
        public string Insert_AusfuellhilfeItem(AusfuellhilfeItem ausfuellhilfeItem)
        {
            IDBExtended obj = new IDBExtended();
            if (obj.Insert_AusfuellhilfeItem(ausfuellhilfeItem))
            {
                return "OK";
            }
            else
            {
                return "ERROR";
            }
        }

        [HttpPost("Insert_celldata")]
        public string Insert_CellData(Cell cell)
        {
            IDBExtended obj = new IDBExtended();
            if (obj.Insert_CellData(cell))
            {
                return "OK";
            }
            else
            {
                return "ERROR";
            }
        }

        #endregion
        [HttpPost("Delete_celldatarow")]
        public string Delete_CellDataRow(Cell cell)
        {
            IDBExtended obj = new IDBExtended();
            if (obj.Delete_CellDataRow(cell))
            {
                return "OK";
            }
            else
            {
                return "ERROR";
            }
        }
     
        [HttpPost("Delete_Ausfuellhilfe")]
        public IActionResult Delete_Ausfuellhilfe(DeleteRequest request)
        {
           
            
            IDBExtended obj = new IDBExtended();

            if (obj.Delete_Ausfuellhilfe(request.Id))
            {
                return Ok("Datensatz erfolgreich gelöscht.");
                
            }
            else
            {
                return NotFound($"Datensatz mit der ID {request.Id} wurde nicht gefunden.");
            }
        }
        [HttpPost("delete_file")]
        public IActionResult Delete_File(FileUploadDTO file)
        {
           
            
            IDBExtended obj = new IDBExtended();

            if (obj.Delete_FileMeta(file.RecordId))
            {

                if (obj.Delete_File(file))
                {
                    return Ok("Datensatz erfolgreich gelöscht.");
                }
                else
                {
                    return NotFound($"Datensatz mit der ID {file.RecordId} wurde. Aber nicht die Datei");
                }
            }
            else
            {
                return NotFound($"Datensatz mit der ID {file.RecordId} wurde nicht gefunden.");
            }
        }
        #region update
        [HttpPost("EditIDB")]
        public string Edit_IDB(IDB idb)
        {
            IDBExtended obj = new IDBExtended();
            if (obj.Edit_IDB(idb))
            {
                return "OK";
            }
            else
            {
                return "ERROR";
            }
        }
        [HttpPost("Update_celldata")]
        public string Update_CellData(Cell cell)
        {
            IDBExtended obj = new IDBExtended();
            if (obj.Update_CellData(cell))
            {
                return "OK";
            }
            else
            {
                return "ERROR";
            }
        }
        [HttpPost("Update_column")]
        public string Update_Column(Column col)
        {
            IDBExtended obj = new IDBExtended();
            if (obj.Update_Column(col))
            {
                return "OK";
            }
            else
            {
                return "ERROR";
            }
        }
        [HttpPost("Update_ausfuellhilfe")]
        public string Update_ausfuellhilfe(Ausfuellhilfe ausfuellhilfe)
        {
            IDBExtended obj = new IDBExtended();
            if (obj.Update_ausfuellhilfeName(ausfuellhilfe))
            {
                return "OK";
            }
            else
            {
                return "ERROR";
            }
        }
        [HttpPost("Upload")]
        public string Upload(FileUploadDTO file)
        {
            IDBExtended obj = new IDBExtended();
            var uploadFile = obj.Upload_File(file);
            var uploadFiledata = obj.Upload_FileData(file);

            if (uploadFile == true && uploadFiledata == true)
            {
                
                return "OK";
            }
            else
            {
                return "ERROR";
            }
        }
        #endregion
        #endregion
    }
}
