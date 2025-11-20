using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Net;


namespace IDB.DataAccess
{
    using IDB.Model;
    using System.Text;

    public class DataAccess {
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
        public List<Ausfuellhilfe> Get_Ausfuellhilfe(string URLWebAPI)
        {
            string sURL = URLWebAPI + "idb/All_Ausfuellhilfe";
            HttpClientHandler auth = new HttpClientHandler();
            auth.Credentials = CredentialCache.DefaultCredentials;

            using (var client = new HttpClient(auth))
            {
                client.BaseAddress = new Uri(sURL);
                var response = client.GetStringAsync(sURL).Result;

                List<Ausfuellhilfe>? lstTemp = JsonConvert.DeserializeObject<List<Ausfuellhilfe>>(response);
                if (lstTemp == null) return new List<Ausfuellhilfe>();
                else return lstTemp;

               
            }
        }
        public List<AusfuellhilfeItem> Get_AusfuellhilfeItems(string URLWebAPI)
        {
            string sURL = URLWebAPI + "idb/All_Ausfuellhilfe_Item";
            HttpClientHandler auth = new HttpClientHandler();
            auth.Credentials = CredentialCache.DefaultCredentials;

            using (var client = new HttpClient(auth))
            {
                client.BaseAddress = new Uri(sURL);
                var response = client.GetStringAsync(sURL).Result;

                List<AusfuellhilfeItem>? lstTemp = JsonConvert.DeserializeObject<List<AusfuellhilfeItem>>(response);
                if (lstTemp == null) return new List<AusfuellhilfeItem>();
                else return lstTemp;

               
            }
        }

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
        public int Insert_AusfuellhilfeName(string URLWebAPI, Ausfuellhilfe ausfuellhilfe)
        {
            string sURL = URLWebAPI + "idb/insert_ausfuellhilfename";
            HttpClientHandler auth = new HttpClientHandler();
            auth.Credentials = CredentialCache.DefaultCredentials;

            using (var client = new HttpClient(auth))
            {
                client.BaseAddress = new Uri(sURL);
                string Json = JsonConvert.SerializeObject(ausfuellhilfe);
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
        public bool Insert_AusfuellhilfeItem(string URLWebAPI, AusfuellhilfeItem ausfuellhilfeItem)
        {
            string sURL = URLWebAPI + "idb/Insert_AusfuellhilfeItem/";
            HttpClientHandler auth = new HttpClientHandler();
            auth.Credentials = CredentialCache.DefaultCredentials;

            using (var client = new HttpClient(auth))
            {
                client.BaseAddress = new Uri(sURL);
                string Json = JsonConvert.SerializeObject(ausfuellhilfeItem);
                HttpContent content = new StringContent(Json, Encoding.UTF8, "application/json");
                var response = client.PostAsync(sURL, content).Result;
                if (response.StatusCode == HttpStatusCode.OK) return true;
                else return false;
            }
        }

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
        public bool Update_Ausfuellhilfe(string URLWebAPI, Ausfuellhilfe ausfuellhilfe)
        {
            string sURL = URLWebAPI + "idb/update_ausfuellhilfe/";
            HttpClientHandler auth = new HttpClientHandler();
            auth.Credentials = CredentialCache.DefaultCredentials;

            using (var client = new HttpClient(auth))
            {
                client.BaseAddress = new Uri(sURL);
                string Json = JsonConvert.SerializeObject(ausfuellhilfe);
                HttpContent content = new StringContent(Json, Encoding.UTF8, "application/json");
                var response = client.PostAsync(sURL, content).Result;
                if (response.StatusCode == HttpStatusCode.OK) return true;
                else return false;
            }
        }
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
        public bool Delete_Ausfuellhilfe(string URLWebAPI, int id) { 
            string sURL = URLWebAPI + "idb/delete_ausfuellhilfe/";
            HttpClientHandler auth = new HttpClientHandler();
            auth.Credentials = CredentialCache.DefaultCredentials;
            DeleteRequest request = new DeleteRequest();
            request.Id = id;
            using (var client = new HttpClient(auth))
            {
                client.BaseAddress = new Uri(sURL);
                string Json = JsonConvert.SerializeObject(request);
                HttpContent content = new StringContent(Json, Encoding.UTF8, "application/json");
                var response = client.PostAsync(sURL, content).Result;

                string abc = response.Content.ReadAsStringAsync().Result;
                if (response.StatusCode == HttpStatusCode.OK) return true;
                else return false;
            }
        }
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
