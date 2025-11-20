using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Net;


namespace IDB.DataAccess
{
    using IDB.Model;
    using System.Text;

    public class DataAccess {
        // Alle IDBs abrufen
        public List<IDB> Get_AllIDBs(string URLWebAPI)
        {
            string sURL = URLWebAPI + "idb/allidbs";
            HttpClientHandler auth = new HttpClientHandler();
            auth.Credentials = CredentialCache.DefaultCredentials;

            using (var client = new HttpClient(auth))
            {
                client.BaseAddress = new Uri(sURL);
                var response = client.GetStringAsync(sURL).Result;

                List<IDB>? lstTemp = JsonConvert.DeserializeObject<List<IDB>>(response);
                if (lstTemp == null) return new List<IDB>();
                else return lstTemp;
            }
        }
        
        // Alle Datei-Metadaten abrufen
        public List<FileUploadDTO> Get_FileMeta(string URLWebAPI)
        {
            string sURL = URLWebAPI + "idb/AllFileMeta";
            HttpClientHandler auth = new HttpClientHandler();
            auth.Credentials = CredentialCache.DefaultCredentials;

            using (var client = new HttpClient(auth))
            {
                client.BaseAddress = new Uri(sURL);
                var response = client.GetStringAsync(sURL).Result;

                List<FileUploadDTO>? lstTemp = JsonConvert.DeserializeObject<List<FileUploadDTO>>(response);
                if (lstTemp == null) return new List<FileUploadDTO>();
                else return lstTemp;
            }
        }

        // IDB nach ID abrufen
        public IDB Get_IDBbyID(string URLWebAPI, int Id)
        {
            string sURL = URLWebAPI + "idb/idbbyid/" + Id.ToString();
            HttpClientHandler auth = new HttpClientHandler();
            auth.Credentials = CredentialCache.DefaultCredentials;

            using (var client = new HttpClient(auth))
            {
                client.BaseAddress = new Uri(sURL);
                var response = client.GetStringAsync(sURL).Result;
                
                return JsonConvert.DeserializeObject<IDB>(response);
            }
        }
        
        // Alle Spalten abrufen
        public List<Column> Get_AllColumns(string URLWebAPI)
        {
            string sURL = URLWebAPI + "idb/allcolumns";
            HttpClientHandler auth = new HttpClientHandler();
            auth.Credentials = CredentialCache.DefaultCredentials;

            using (var client = new HttpClient(auth))
            {
                client.BaseAddress = new Uri(sURL);
                var response = client.GetStringAsync(sURL).Result;
                List<Column>? list = JsonConvert.DeserializeObject<List<Column>>(response);
                if (list == null) return new List<Column>();
                else return list;
            }
        }

        // Alle Zelldaten abrufen
        public List<Cell> Get_AllCellData(string URLWebAPI)
        {
            string sURL = URLWebAPI + "idb/allcelldata";
            HttpClientHandler auth = new HttpClientHandler();
            auth.Credentials = CredentialCache.DefaultCredentials;

            using (var client = new HttpClient(auth))
            {
                client.BaseAddress = new Uri(sURL);
                var response = client.GetStringAsync(sURL).Result;
                
                List<Cell>? lstTemp =  JsonConvert.DeserializeObject<List<Cell>>(response);
                if (lstTemp == null) return new List<Cell>();
                else  return lstTemp;
            }
        }

        // Neue IDB einfügen
        public int Insert_IDB(string URLWebAPI, IDB idb)
        {
            string sURL = URLWebAPI + "idb/insert/" ;
            HttpClientHandler auth = new HttpClientHandler();
            auth.Credentials = CredentialCache.DefaultCredentials;

            using (var client = new HttpClient(auth))
            {
                client.BaseAddress = new Uri(sURL);
                string Json = JsonConvert.SerializeObject(idb);
                HttpContent content = new StringContent(Json, Encoding.UTF8, "application/json");
                var response = client.PostAsync(sURL, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = response.Content.ReadAsStringAsync().Result;
                    int.TryParse(responseContent, out int newId);
                    return newId;
                }
                else {
                    return 0;
                }
            }
        }
        
        // Neue Spalte einfügen
        public bool Insert_Column(string URLWebAPI, Column col)
        {
            string sURL = URLWebAPI + "idb/insert_column/";
            HttpClientHandler auth = new HttpClientHandler();
            auth.Credentials = CredentialCache.DefaultCredentials;

            using (var client = new HttpClient(auth))
            {
                client.BaseAddress = new Uri(sURL);
                string Json = JsonConvert.SerializeObject(col);
                HttpContent content = new StringContent(Json, Encoding.UTF8, "application/json");
                var response = client.PostAsync(sURL, content).Result;
                if (response.StatusCode == HttpStatusCode.OK) return true;
                else return false;
            }
        }

        // Neue Zelle einfügen
        public bool Insert_CellData(string URLWebAPI, Cell cell)
        {
            string sURL = URLWebAPI + "idb/insert_celldata/";
            HttpClientHandler auth = new HttpClientHandler();
            auth.Credentials = CredentialCache.DefaultCredentials;

            using (var client = new HttpClient(auth))
            {
                client.BaseAddress = new Uri(sURL);
                string Json = JsonConvert.SerializeObject(cell);
                HttpContent content = new StringContent(Json, Encoding.UTF8, "application/json");
                var response = client.PostAsync(sURL, content).Result;
                if (response.StatusCode == HttpStatusCode.OK) return true;
                else return false;
            }
        }
        
        // Zelldaten aktualisieren
        public bool Update_CellData(string URLWebAPI, Cell cell)
        {
            string sURL = URLWebAPI + "idb/update_celldata/";
            HttpClientHandler auth = new HttpClientHandler();
            auth.Credentials = CredentialCache.DefaultCredentials;

            using (var client = new HttpClient(auth))
            {
                client.BaseAddress = new Uri(sURL);
                string Json = JsonConvert.SerializeObject(cell);
                HttpContent content = new StringContent(Json, Encoding.UTF8, "application/json");
                var response = client.PostAsync(sURL, content).Result;
                if (response.StatusCode == HttpStatusCode.OK) return true;
                else return false;
            }
        }
        
        // Spalte aktualisieren
        public bool Update_Column(string URLWebAPI, Column col)
        {
            string sURL = URLWebAPI + "idb/update_column/";
            HttpClientHandler auth = new HttpClientHandler();
            auth.Credentials = CredentialCache.DefaultCredentials;

            using (var client = new HttpClient(auth))
            {
                client.BaseAddress = new Uri(sURL);
                string Json = JsonConvert.SerializeObject(col);
                HttpContent content = new StringContent(Json, Encoding.UTF8, "application/json");
                var response = client.PostAsync(sURL, content).Result;
                if (response.StatusCode == HttpStatusCode.OK) return true;
                else return false;
            }
        } 
        
        // Zelldaten-Zeile löschen
        public bool Delete_CellDataRow(string URLWebAPI, Cell cell)
        {
            string sURL = URLWebAPI + "idb/delete_celldatarow/";
            HttpClientHandler auth = new HttpClientHandler();
            auth.Credentials = CredentialCache.DefaultCredentials;

            using (var client = new HttpClient(auth))
            {
                client.BaseAddress = new Uri(sURL);
                string Json = JsonConvert.SerializeObject(cell);
                HttpContent content = new StringContent(Json, Encoding.UTF8, "application/json");
                var response = client.PostAsync(sURL, content).Result;
                if (response.StatusCode == HttpStatusCode.OK) return true;
                else return false;
            }
        }
        
        // Datei löschen
        public async Task<bool> Delete_FileAsync(string URLWebAPI, FileUploadDTO file) { 
            string sURL = URLWebAPI + "idb/delete_file/";
            HttpClientHandler auth = new HttpClientHandler();
            auth.Credentials = CredentialCache.DefaultCredentials;
            
            using (var client = new HttpClient(auth))
            {
                client.BaseAddress = new Uri(sURL);
                string Json = JsonConvert.SerializeObject(file);
                HttpContent content = new StringContent(Json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(sURL, content);

                string abc = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK) return true;
                else return false;
            }
        }
        
        // IDB bearbeiten
        public bool Edit_IDB(string URLWebAPI, IDB idb)
        {
            string sURL = URLWebAPI + "idb/editidb/";
            HttpClientHandler auth = new HttpClientHandler();
            auth.Credentials = CredentialCache.DefaultCredentials;

            using (var client = new HttpClient(auth))
            {
                client.BaseAddress = new Uri(sURL);
                string Json = JsonConvert.SerializeObject(idb);
                HttpContent content = new StringContent(Json, Encoding.UTF8, "application/json");
                var response = client.PostAsync(sURL, content).Result;
                if (response.StatusCode == HttpStatusCode.OK) return true;
                else return false;
            }
        }
        
        // Datei hochladen
        public bool Upload_File(string URLWebAPI, FileUploadDTO filedto)
        {
            string sURL = URLWebAPI + "idb/upload/";
            HttpClientHandler auth = new HttpClientHandler();
            auth.Credentials = CredentialCache.DefaultCredentials;

            using (var client = new HttpClient(auth))
            {
                client.BaseAddress = new Uri(sURL);
                string Json = JsonConvert.SerializeObject(filedto);
                HttpContent content = new StringContent(Json, Encoding.UTF8, "application/json");
                var response = client.PostAsync(sURL, content).Result;
                if (response.StatusCode == HttpStatusCode.OK) return true;
                else return false;
            }
        }
        
        // Datei abrufen
        public FileUploadDTO Get_File(string URLWebAPI, FileUploadDTO filedto)
        {
            string sURL = URLWebAPI + "idb/get_file/";
            HttpClientHandler auth = new HttpClientHandler();
            auth.Credentials = CredentialCache.DefaultCredentials;

            using (var client = new HttpClient(auth))
            {
                client.BaseAddress = new Uri(sURL);
                string Json = JsonConvert.SerializeObject(filedto);
                HttpContent content = new StringContent(Json, Encoding.UTF8, "application/json");
                var response = client.PostAsync(sURL, content).Result;

                string responseJson = response.Content.ReadAsStringAsync().Result;

                FileUploadDTO? fileDok = JsonConvert.DeserializeObject<FileUploadDTO>(responseJson);
                 return fileDok;
            }
        }
    }
}
