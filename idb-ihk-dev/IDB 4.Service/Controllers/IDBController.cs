using IDB.Service.Klassen;
using Microsoft.AspNetCore.Mvc;

namespace IDB.Service.Controllers
{
    using IDB.Model;
    
    // API-Controller für IDB-Verwaltung
    [ApiController]
    [Route("[controller]")]
    public class IDBController : ControllerBase
    {
        #region GET-Endpunkte
        
        // Datei abrufen
        [HttpPost("get_file")]
        public FileUploadDTO Get_File(FileUploadDTO file)
        {
            IDBExtended obj = new IDBExtended();
            return obj.Get_File(file);
        }

        // Alle IDBs abrufen
        [HttpGet("AllIDBs")]
        public List<IDB> Get_AllIDBs()
        {
            IDBExtended obj = new IDBExtended();
            return obj.Get_AllIDBs();
        }
        
        // Alle Datei-Metadaten abrufen
        [HttpGet("AllFileMeta")]
        public List<FileUploadDTO> Get_FileMeta()
        {
            IDBExtended obj = new IDBExtended();
            return obj.Get_FileMeta();
        }
        
        // IDB nach ID abrufen
        [HttpGet("IDBbyId/{Id}")]
        public IDB Get_IDBbyId(string Id)
        {
            IDBExtended obj = new IDBExtended();
            return obj.Get_IDBbyId(Convert.ToInt32(Id));
        }

        // Alle Spalten abrufen
        [HttpGet("AllColumns")]
        public List<Column> Get_AllColumns()
        {
            IDBExtended obj = new IDBExtended();
            return obj.Get_AllColumns();
        }

        // Alle Zelldaten abrufen
        [HttpGet("AllCellData")]
        public List<Cell> Get_AllCellData()
        {
            IDBExtended obj = new IDBExtended();
            return obj.Get_AllCellData();
        }
        #endregion

        #region POST-Endpunkte
        #region Einfügen
        
        // Neue IDB einfügen
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
        
        // Neue Spalte einfügen
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

        // Zelldaten einfügen
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
        
        #region Löschen
        
        // Zelldaten-Zeile löschen
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
     
        // Datei löschen
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
        #endregion
        
        #region Aktualisieren
        
        // IDB bearbeiten
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
        
        // Zelldaten aktualisieren
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
        
        // Spalte aktualisieren
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
        
        // Datei hochladen
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
