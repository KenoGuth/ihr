using System.Data;

namespace IDB.Service.Klassen
{
    using IDB.Model;
    using System;
    using System.Data.SqlClient;
    using System.Data.SqlTypes;
    using System.Drawing.Text;
    using System.Net.Mime;
    using System.Xml.Linq;

    public class IDBExtended
    {
        private string basePathFileStorage = "F:\\Services\\SVS_Service\\FileStorage\\IDB\\files\\";
        #region Gets
        public List<IDB> Get_AllIDBs()
        {
            List<IDB> lst = new List<IDB>();
            SqlCommand mySqlCommand = new SqlCommand();
            SqlConnection myConnection = new SqlConnection();
            SqlDataAdapter mySqlAdapter = new SqlDataAdapter();
            DataSet myDataSet = new DataSet();

            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            myConnection.ConnectionString = "Server = 10.247.2.70; Initial Catalog = DBIDB; Integrated Security = true; Pooling = true;";
            mySqlCommand.CommandText = "get_allidbs";
            mySqlCommand.Connection = myConnection;
            mySqlAdapter.SelectCommand = mySqlCommand;
            mySqlAdapter.Fill(myDataSet);

            if ((myDataSet != null) && (myDataSet.Tables.Count > 0) && (myDataSet.Tables[0].Rows.Count > 0))
            {
                foreach (DataRow reihe in myDataSet.Tables[0].Rows)
                {
                    lst.Add
                        (
                            new IDB
                            {
                                Id = reihe.Field<int>("id"),
                                // Pnr = reihe.Field<string>("pnr") ?? string.Empty,
                                Table_Name = reihe.Field<string>("table_name") ?? string.Empty,
                                CreatedAt = reihe.Field<DateTime>("created_at"),
                                Menu_allowed = reihe.Field<string>("menu_allowed") ?? string.Empty
                            }
                        );
                }
            }

            return lst;
        }
        public FileUploadDTO Get_File(FileUploadDTO file)
        {
            try
            {


                string fileExtension = Path.GetExtension(file.FileName);



                string folderPath = Path.Combine(basePathFileStorage, file.PlannedStoragePath);
                Directory.CreateDirectory(folderPath);


                string fullFilePath = Path.Combine(folderPath, file.FileName);

                file.FileData = File.ReadAllBytes(fullFilePath);


                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<FileUploadDTO> Get_FileMeta()
        {
            List<FileUploadDTO> lst = new List<FileUploadDTO>();
            SqlCommand mySqlCommand = new SqlCommand();
            SqlConnection myConnection = new SqlConnection();
            SqlDataAdapter mySqlAdapter = new SqlDataAdapter();
            DataSet myDataSet = new DataSet();

            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            myConnection.ConnectionString = "Server = 10.247.2.70; Initial Catalog = DBIDB; Integrated Security = true; Pooling = true;";
            mySqlCommand.CommandText = "get_filemeta";
            mySqlCommand.Connection = myConnection;
            mySqlAdapter.SelectCommand = mySqlCommand;
            mySqlAdapter.Fill(myDataSet);

            if ((myDataSet != null) && (myDataSet.Tables.Count > 0) && (myDataSet.Tables[0].Rows.Count > 0))
            {
                foreach (DataRow reihe in myDataSet.Tables[0].Rows)
                {
                    lst.Add
                        (
                            new FileUploadDTO
                            {
                                RecordId = reihe.Field<int>("Id"),
                                // Pnr = reihe.Field<string>("pnr") ?? string.Empty,
                                FileName = reihe.Field<string>("StoredFileName") ?? string.Empty,
                                ContentType = reihe.Field<string>("ContentType") ?? string.Empty,
                                PlannedStoragePath = reihe.Field<string>("StoragePath") ?? string.Empty,
                                UploadDate = reihe.Field<DateTime>("UploadDate")
                            }
                        );
                }
            }

            return lst;
        }

        public List<Ausfuellhilfe> Get_All_Ausfuellhilfen()
        {
            List<Ausfuellhilfe> lst = new List<Ausfuellhilfe>();
            SqlCommand mySqlCommand = new SqlCommand();
            SqlConnection myConnection = new SqlConnection();
            SqlDataAdapter mySqlAdapter = new SqlDataAdapter();
            DataSet myDataSet = new DataSet();

            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            myConnection.ConnectionString = "Server = 10.247.2.70; Initial Catalog = DBIDB; Integrated Security = true; Pooling = true;";
            mySqlCommand.CommandText = "get_allausfuellhilfen";
            mySqlCommand.Connection = myConnection;
            mySqlAdapter.SelectCommand = mySqlCommand;
            mySqlAdapter.Fill(myDataSet);

            if ((myDataSet != null) && (myDataSet.Tables.Count > 0) && (myDataSet.Tables[0].Rows.Count > 0))
            {
                foreach (DataRow reihe in myDataSet.Tables[0].Rows)
                {
                    lst.Add
                        (
                            new Ausfuellhilfe
                            {
                                Id = reihe.Field<int>("id"),                    
                                Titel = reihe.Field<string>("Titel") ?? string.Empty,
                                Notiz = reihe.Field<string>("Notiz") ?? string.Empty,
                                
                            }
                        );
                }
            }

            return lst;
        } 
        public List<AusfuellhilfeItem> Get_All_Ausfuellhilfen_Items()
        {
            List<AusfuellhilfeItem> lst = new ();
            SqlCommand mySqlCommand = new SqlCommand();
            SqlConnection myConnection = new SqlConnection();
            SqlDataAdapter mySqlAdapter = new SqlDataAdapter();
            DataSet myDataSet = new DataSet();

            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            myConnection.ConnectionString = "Server = 10.247.2.70; Initial Catalog = DBIDB; Integrated Security = true; Pooling = true;";
            mySqlCommand.CommandText = "get_allausfuellhilfenitems";
            mySqlCommand.Connection = myConnection;
            mySqlAdapter.SelectCommand = mySqlCommand;
            mySqlAdapter.Fill(myDataSet);

            if ((myDataSet != null) && (myDataSet.Tables.Count > 0) && (myDataSet.Tables[0].Rows.Count > 0))
            {
                foreach (DataRow reihe in myDataSet.Tables[0].Rows)
                {
                    lst.Add
                        (
                            new AusfuellhilfeItem
                            {
                                Id = reihe.Field<int>("id"),                    
                                Id_ausfuellhilfe = reihe.Field<int>("id_ausfuellhilfe"),
                                Text = reihe.Field<string>("text") ?? string.Empty,
                                
                            }
                        );
                }
            }

            return lst;
        }


        public IDB Get_IDBbyId(int id)
        {
            IDB IDB;
            SqlCommand mySqlCommand = new SqlCommand();
            SqlConnection myConnection = new SqlConnection();
            SqlDataAdapter mySqlAdapter = new SqlDataAdapter();
            DataSet myDataSet = new DataSet();
            Add_SQLParameter(mySqlCommand, "@id", id);

            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            myConnection.ConnectionString = "Server = 127.0.0.1; Initial Catalog = DBIDB; Integrated Security = true; Pooling = true;";
            mySqlCommand.CommandText = "get_idbbyid";
            mySqlCommand.Connection = myConnection;
            mySqlAdapter.SelectCommand = mySqlCommand;
            mySqlAdapter.Fill(myDataSet);

            if ((myDataSet != null) && (myDataSet.Tables.Count > 0) && (myDataSet.Tables[0].Rows.Count > 0))
            {

                IDB = new IDB
                {
                    Id = myDataSet.Tables[0].Rows[0].Field<int>("id"),
                    Table_Name = myDataSet.Tables[0].Rows[0].Field<string>("table_name") ?? string.Empty,
                    CreatedAt = myDataSet.Tables[0].Rows[0].Field<DateTime>("created_at"),
                    Menu_allowed = myDataSet.Tables[0].Rows[0].Field<string>("menu_allowed") ?? string.Empty
                };
            }
            else
            {
                IDB = new IDB();
            }

            return IDB;
        }

        public List<Column> Get_AllColumns()
        {
            List<Column> lst = new List<Column>();
            SqlCommand mySqlCommand = new SqlCommand();
            SqlConnection myConnection = new SqlConnection();
            SqlDataAdapter mySqlAdapter = new SqlDataAdapter();
            DataSet myDataSet = new DataSet();

            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            myConnection.ConnectionString = "Server = 127.0.0.1; Initial Catalog = DBIDB; Integrated Security = true; Pooling = true;";
            mySqlCommand.CommandText = "get_allcolumns";
            mySqlCommand.Connection = myConnection;
            mySqlAdapter.SelectCommand = mySqlCommand;
            mySqlAdapter.Fill(myDataSet);

            if ((myDataSet != null) && (myDataSet.Tables.Count > 0) && (myDataSet.Tables[0].Rows.Count > 0))
            {
                foreach (DataRow reihe in myDataSet.Tables[0].Rows)
                {
                    lst.Add
                        (
                            new Column
                            {
                                Id = reihe.Field<int>("id"),

                                Id_table = reihe.Field<int>("id_table"),

                                Id_ausfuellhilfe = reihe.Field<int?>("id_ausfuellhilfe"),

                                Column_no = reihe.Field<int>("Column_no"),

                                Name = reihe.Field<string>("name") ?? string.Empty,

                                Data_type = reihe.Field<string>("data_type") ?? string.Empty,

                                Is_nullable = reihe.Field<bool>("is_nullable"),

                                Default_value = reihe.Field<string>("default_value") ?? string.Empty,

                                Svs_data = reihe.Field<bool?>("svs_data") ?? false, 
                                Archived = reihe.Field<bool>("archived")

                                //Id = reihe.Field<int>("id"),
                                //Pnr = reihe.Field<string>("pnr") ?? string.Empty,
                                //Name = reihe.Field<string>("name") ?? string.Empty,
                                //CreatedAt = reihe.Field<DateTime>("created_at"),
                                //Menu_allowed = reihe.Field<string>("menu_allowed") ?? string.Empty
                            }
                        );
                }
            }

            return lst;
        }
        

        public List<Cell> Get_AllCellData()
        {
            List<Cell> lst = new List<Cell>();
            SqlCommand mySqlCommand = new SqlCommand();
            SqlConnection myConnection = new SqlConnection();
            SqlDataAdapter mySqlAdapter = new SqlDataAdapter();
            DataSet myDataSet = new DataSet();

            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            myConnection.ConnectionString = "Server = 127.0.0.1; Initial Catalog = DBIDB; Integrated Security = true; Pooling = true;";
            mySqlCommand.CommandText = "get_allcelldata";
            mySqlCommand.Connection = myConnection;
            mySqlAdapter.SelectCommand = mySqlCommand;
            mySqlAdapter.Fill(myDataSet);

            if ((myDataSet != null) && (myDataSet.Tables.Count > 0) && (myDataSet.Tables[0].Rows.Count > 0))
            {
                foreach (DataRow reihe in myDataSet.Tables[0].Rows)
                {
                    lst.Add
                        (
                            new Cell
                            {
                                Id = reihe.Field<int>("id"),

                                Id_row = reihe.Field<int>("id_row"),

                                Id_table = reihe.Field<int>("id_table"),

                                Id_column = reihe.Field<int>("id_column"),

                                Data_value = reihe.Field<string>("data_value") ?? string.Empty,

                                

                            }
                        );
                }
            }

            return lst;
        }
        #endregion


        #region Edits
        public bool Edit_IDB(IDB idb)
        {
            bool bReturn = false;

            SqlCommand mySqlCommand = new SqlCommand();
            SqlConnection myConnection = new SqlConnection();

            #region Parameter
            if (idb.Table_Name != null && idb.Menu_allowed != null)  //if, um möglichen null-Verweis zu vermeiden
            {
                //Add_SQLParameter(mySqlCommand, "@PNR", idb.Pnr);
                Add_SQLParameter(mySqlCommand, "@NAME", idb.Table_Name);
                Add_SQLParameter(mySqlCommand, "@MENUALLOWED", idb.Menu_allowed);
                Add_SQLParameter(mySqlCommand, "@ID", idb.Id);
            }
            else { return bReturn; }
            #endregion

            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            myConnection.ConnectionString = "Server = 127.0.0.1; Initial Catalog = DBIDB; Integrated Security = true; Pooling = true;";
            mySqlCommand.CommandText = "edit_idb";
            mySqlCommand.Connection = myConnection;

            myConnection.Open();
            SqlTransaction myTrans = myConnection.BeginTransaction();
            mySqlCommand.Transaction = myTrans;

            try
            {
                mySqlCommand.ExecuteNonQuery();
                myTrans.Commit();
                bReturn = true;
            }
            catch (Exception)
            {
                myTrans.Rollback();
            }
            finally
            {
                myConnection.Close();
            }

            return bReturn;

        }

        public bool Update_CellData(Cell cell)
        {
            bool bReturn = false;

            SqlCommand mySqlCommand = new SqlCommand();
            SqlConnection myConnection = new SqlConnection();

            #region Parameter

            Add_SQLParameter(mySqlCommand, "@ID_TABLE", cell.Id_table);

            Add_SQLParameter(mySqlCommand, "@ID_ROW", cell.Id_row);
            Add_SQLParameter(mySqlCommand, "@ID_COLUMN", cell.Id_column);
            Add_SQLParameter(mySqlCommand, "@VALUE", cell.Data_value);


            #endregion

            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            myConnection.ConnectionString = "Server = 127.0.0.1; Initial Catalog = DBIDB; Integrated Security = true; Pooling = true;";
            mySqlCommand.CommandText = "update_celldata";
            mySqlCommand.Connection = myConnection;

            myConnection.Open();
            SqlTransaction myTrans = myConnection.BeginTransaction();
            mySqlCommand.Transaction = myTrans;

            try
            {
                mySqlCommand.ExecuteNonQuery();
                myTrans.Commit();
                bReturn = true;
            }
            catch (Exception)
            {
                myTrans.Rollback();
            }
            finally
            {
                myConnection.Close();
            }

            return bReturn;
        }

        public bool Update_Column(Column col)
        {
            bool bReturn = false;

            SqlCommand mySqlCommand = new SqlCommand();
            SqlConnection myConnection = new SqlConnection();

            #region Parameter

            Add_SQLParameter(mySqlCommand, "@COL_NO", col.Column_no);
            Add_SQLParameter(mySqlCommand, "@ID", col.Id);
            Add_SQLParameter(mySqlCommand, "@NAME", col.Name);
            Add_SQLParameter(mySqlCommand, "@DATA_TYPE", col.Data_type);
            Add_SQLParameter(mySqlCommand, "@ARCHIVED", col.Archived);


            #endregion

            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            myConnection.ConnectionString = "Server = 127.0.0.1; Initial Catalog = DBIDB; Integrated Security = true; Pooling = true;";
            mySqlCommand.CommandText = "update_column";
            mySqlCommand.Connection = myConnection;

            myConnection.Open();
            SqlTransaction myTrans = myConnection.BeginTransaction();
            mySqlCommand.Transaction = myTrans;

            try
            {
                mySqlCommand.ExecuteNonQuery();
                myTrans.Commit();
                bReturn = true;
            }
            catch (Exception)
            {
                myTrans.Rollback();
            }
            finally
            {
                myConnection.Close();
            }

            return bReturn;
        }
        public bool Update_ausfuellhilfeName(Ausfuellhilfe ausfuellhilfe)
        {
            bool bReturn = false;

            SqlCommand mySqlCommand = new SqlCommand();
            SqlConnection myConnection = new SqlConnection();

            #region Parameter
          
            Add_SQLParameter(mySqlCommand, "@ID", ausfuellhilfe.Id);
            Add_SQLParameter(mySqlCommand, "@TITEL", ausfuellhilfe.Titel);
            Add_SQLParameter(mySqlCommand, "@NOTIZ", ausfuellhilfe.Notiz);


            #endregion

            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            myConnection.ConnectionString = "Server = 127.0.0.1; Initial Catalog = DBIDB; Integrated Security = true; Pooling = true;";
            mySqlCommand.CommandText = "update_AusfuellhilfeName";
            mySqlCommand.Connection = myConnection;

            myConnection.Open();
            SqlTransaction myTrans = myConnection.BeginTransaction();
            mySqlCommand.Transaction = myTrans;

            try
            {
                mySqlCommand.ExecuteNonQuery();
                myTrans.Commit();
                bReturn = true;
            }
            catch (Exception)
            {
                myTrans.Rollback();
            }
            finally
            {
                myConnection.Close();
            }

            return bReturn;
        }
        #endregion

        #region Inserts
        public int Insert_IDB(IDB idb)
        {
            //bool bReturn = false;
            int returnId = 0;
            SqlCommand mySqlCommand = new SqlCommand();
            SqlConnection myConnection = new SqlConnection();

            #region Parameter
            if (idb.Table_Name != null && idb.Menu_allowed != null)  //if, um möglichen null-Verweis zu vermeiden
            {
                //Add_SQLParameter(mySqlCommand, "@PNR", idb.Pnr);
                Add_SQLParameter(mySqlCommand, "@NAME", idb.Table_Name);
                Add_SQLParameter(mySqlCommand, "@MENUALLOWED", idb.Menu_allowed);
            }
            else { return returnId; }
            #endregion

            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            myConnection.ConnectionString = "Server = 127.0.0.1; Initial Catalog = DBIDB; Integrated Security = true; Pooling = true;";
            mySqlCommand.CommandText = "insert_idb";
            mySqlCommand.Connection = myConnection;

            myConnection.Open();
            SqlTransaction myTrans = myConnection.BeginTransaction();
            mySqlCommand.Transaction = myTrans;

            try
            {
                object result = mySqlCommand.ExecuteScalar();

                if (result == null)
                    throw new Exception("Kein Rückgabewert von insert_idb.");

                returnId = Convert.ToInt32(result); // Exception bei Fehlerhaftem Rückgabewert

                myTrans.Commit();
            }
            catch (Exception)
            {
                myTrans.Rollback();
            }
            finally
            {
                myConnection.Close();
            }

            return returnId;

        }
        public int Insert_AusfuellhilfeName(Ausfuellhilfe ausfuellhilfe)
        {
            //bool bReturn = false;
            int returnId = 0;
            SqlCommand mySqlCommand = new SqlCommand();
            SqlConnection myConnection = new SqlConnection();

            #region Parameter
          
                //Add_SQLParameter(mySqlCommand, "@PNR", idb.Pnr);
                Add_SQLParameter(mySqlCommand, "@TITEL", ausfuellhilfe.Titel);
                Add_SQLParameter(mySqlCommand, "@NOTIZ",  ausfuellhilfe.Notiz);
            
            #endregion

            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            myConnection.ConnectionString = "Server = 127.0.0.1; Initial Catalog = DBIDB; Integrated Security = true; Pooling = true;";
            mySqlCommand.CommandText = "insert_AusfuellhilfeName";
            mySqlCommand.Connection = myConnection;

            myConnection.Open();
            SqlTransaction myTrans = myConnection.BeginTransaction();
            mySqlCommand.Transaction = myTrans;

            try
            {
                object result = mySqlCommand.ExecuteScalar();

                if (result == null)
                    throw new Exception("Kein Rückgabewert von insert_AusfuellhilfeName.");

                returnId = Convert.ToInt32(result); // Exception bei Fehlerhaftem Rückgabewert

                myTrans.Commit();
            }
            catch (Exception)
            {
                myTrans.Rollback();
            }
            finally
            {
                myConnection.Close();
            }

            return returnId;

        }
        public bool Insert_AusfuellhilfeItem(AusfuellhilfeItem ausfuellhilfeItem)
        {
            //bool bReturn = false;
            int returnId = 0;
            SqlCommand mySqlCommand = new SqlCommand();
            SqlConnection myConnection = new SqlConnection();
            bool bReturn = false;

            #region Parameter

            //Add_SQLParameter(mySqlCommand, "@PNR", idb.Pnr);
            Add_SQLParameter(mySqlCommand, "@ID_AUSFUELLHILFE", ausfuellhilfeItem.Id_ausfuellhilfe);
            Add_SQLParameter(mySqlCommand, "@TEXT", ausfuellhilfeItem.Text);

            #endregion

            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            myConnection.ConnectionString = "Server = 127.0.0.1; Initial Catalog = DBIDB; Integrated Security = true; Pooling = true;";
            mySqlCommand.CommandText = "insert_AusfuellhilfeItem";
            mySqlCommand.Connection = myConnection;

            myConnection.Open();
            SqlTransaction myTrans = myConnection.BeginTransaction();
            mySqlCommand.Transaction = myTrans;

            try
            {
                mySqlCommand.ExecuteNonQuery();
                myTrans.Commit();
                bReturn = true;
            }
            catch (Exception)
            {
                myTrans.Rollback();
            }
            finally
            {
                myConnection.Close();
            }

            return bReturn;

        }


        public bool Insert_Column(Column col)
        {
            bool bReturn = false;

            SqlCommand mySqlCommand = new SqlCommand();
            SqlConnection myConnection = new SqlConnection();

            #region Parameter
            if (col.Name != null && col.Data_type != null)  //if, um möglichen null-Verweis zu vermeiden
            {
                //Add_SQLParameter(mySqlCommand, "@PNR", idb.Pnr);
                Add_SQLParameter(mySqlCommand, "@ID_TABLE", col.Id_table);
                if (col.Id_ausfuellhilfe != null)
                {
                    Add_SQLParameter(mySqlCommand, "@AUSFUELLHILFE", col.Id_ausfuellhilfe);
                }
                Add_SQLParameter(mySqlCommand, "@COLUMN_NO", col.Column_no);
                Add_SQLParameter(mySqlCommand, "@NAME", col.Name);
                
                Add_SQLParameter(mySqlCommand, "@DATA_TYPE", col.Data_type);
                Add_SQLParameter(mySqlCommand, "@IS_NULLABLE", col.Is_nullable);
                if (col.Default_value != null)
                {
                    Add_SQLParameter(mySqlCommand, "@DEFAUL_VALUE", col.Default_value);
                }
                if (col.Svs_data != null)
                {
                    Add_SQLParameter(mySqlCommand, "@SVS_DATA", col.Svs_data);
                }
                Add_SQLParameter(mySqlCommand, "@ARCHIVED", false);
            }
            else { return bReturn; }
                #endregion

                mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                myConnection.ConnectionString = "Server = 127.0.0.1; Initial Catalog = DBIDB; Integrated Security = true; Pooling = true;";
                mySqlCommand.CommandText = "insert_column";
                mySqlCommand.Connection = myConnection;

                myConnection.Open();
                SqlTransaction myTrans = myConnection.BeginTransaction();
                mySqlCommand.Transaction = myTrans;

                try
                {
                    mySqlCommand.ExecuteNonQuery();
                    myTrans.Commit();
                    bReturn = true;
                }
                catch (Exception)
                {
                    myTrans.Rollback();
                }
                finally
                {
                    myConnection.Close();
                }

                return bReturn;
        }

        public bool Insert_CellData(Cell cell)
        {
            bool bReturn = false;

            SqlCommand mySqlCommand = new SqlCommand();
            SqlConnection myConnection = new SqlConnection();

            #region Parameter
            
                //Add_SQLParameter(mySqlCommand, "@PNR", idb.Pnr);
                Add_SQLParameter(mySqlCommand, "@ID_TABLE", cell.Id_table);
             
                Add_SQLParameter(mySqlCommand, "@ID_ROW", cell.Id_row);
                Add_SQLParameter(mySqlCommand, "@ID_COLUMN", cell.Id_column);
                Add_SQLParameter(mySqlCommand, "@VALUE", cell.Data_value);
               
          
            #endregion

            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            myConnection.ConnectionString = "Server = 127.0.0.1; Initial Catalog = DBIDB; Integrated Security = true; Pooling = true;";
            mySqlCommand.CommandText = "insert_celldata";
            mySqlCommand.Connection = myConnection;

            myConnection.Open();
            SqlTransaction myTrans = myConnection.BeginTransaction();
            mySqlCommand.Transaction = myTrans;

            try
            {
                mySqlCommand.ExecuteNonQuery();
                myTrans.Commit();
                bReturn = true;
            }
            catch (Exception)
            {
                myTrans.Rollback();
            }
            finally
            {
                myConnection.Close();
            }

            return bReturn;
        }


        #endregion
        #region delete
        public bool Delete_CellDataRow(Cell cell)
        {
            bool bReturn = false;

            SqlCommand mySqlCommand = new SqlCommand();
            SqlConnection myConnection = new SqlConnection();

            #region Parameter

            //Add_SQLParameter(mySqlCommand, "@PNR", idb.Pnr);
            Add_SQLParameter(mySqlCommand, "@ID_TABLE", cell.Id_table);

            Add_SQLParameter(mySqlCommand, "@ID_ROW", cell.Id_row);



            #endregion

            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            myConnection.ConnectionString = "Server = 127.0.0.1; Initial Catalog = DBIDB; Integrated Security = true; Pooling = true;";
            mySqlCommand.CommandText = "delete_celldatarow";
            mySqlCommand.Connection = myConnection;

            myConnection.Open();
            SqlTransaction myTrans = myConnection.BeginTransaction();
            mySqlCommand.Transaction = myTrans;

            try
            {
                mySqlCommand.ExecuteNonQuery();
                myTrans.Commit();
                bReturn = true;
            }
            catch (Exception)
            {
                myTrans.Rollback();
            }
            finally
            {
                myConnection.Close();
            }

            return bReturn;
        }    
        public bool Delete_Ausfuellhilfe(int ausgewaehlteAusfuellhilfe)
        {
            bool bReturn = false;

            SqlCommand mySqlCommand = new SqlCommand();
            SqlConnection myConnection = new SqlConnection();

            #region Parameter

            // Add_SQLParameter(mySqlCommand, "@ID", id);
            mySqlCommand.Parameters.AddWithValue("@GEWAEHLTE_ID", ausgewaehlteAusfuellhilfe);
            #endregion

            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            myConnection.ConnectionString = "Server = 127.0.0.1; Initial Catalog = DBIDB; Integrated Security = true; Pooling = true;";
            mySqlCommand.CommandText = "delete_ausfuellhilfe";
            mySqlCommand.Connection = myConnection;

            myConnection.Open();
            SqlTransaction myTrans = myConnection.BeginTransaction();
            mySqlCommand.Transaction = myTrans;

            try
            {
                mySqlCommand.ExecuteNonQuery();
                myTrans.Commit();
                bReturn = true;
            }
            catch (Exception ex)
            {
                myTrans.Rollback();
                throw ex;
            }
            finally
            {
                myConnection.Close();
            }

            return bReturn;
        }
        public bool Delete_FileMeta(int ausgewaehlteDatei)
        {
            bool bReturn = false;

            SqlCommand mySqlCommand = new SqlCommand();
            SqlConnection myConnection = new SqlConnection();

            #region Parameter

            // Add_SQLParameter(mySqlCommand, "@ID", id);
            mySqlCommand.Parameters.AddWithValue("@GEWAEHLTE_ID", ausgewaehlteDatei);
            #endregion

            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            myConnection.ConnectionString = "Server = 127.0.0.1; Initial Catalog = DBIDB; Integrated Security = true; Pooling = true;";
            mySqlCommand.CommandText = "delete_filemeta";
            mySqlCommand.Connection = myConnection;

            myConnection.Open();
            SqlTransaction myTrans = myConnection.BeginTransaction();
            mySqlCommand.Transaction = myTrans;

            try
            {
                mySqlCommand.ExecuteNonQuery();
                myTrans.Commit();
                bReturn = true;
            }
            catch (Exception ex)
            {
                myTrans.Rollback();
                throw ex;
            }
            finally
            {
                myConnection.Close();
            }

            return bReturn;
        }
        public bool Upload_File(FileUploadDTO file)
        {
            try
            {
               
               
                string fileExtension = Path.GetExtension(file.FileName);
               

                
                string folderPath = Path.Combine(basePathFileStorage, file.PlannedStoragePath);
                Directory.CreateDirectory(folderPath); 

              
                string fullFilePath = Path.Combine(folderPath, file.FileName);

                File.WriteAllBytes(fullFilePath, file.FileData);


                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete_File(FileUploadDTO file)
        {
            try
            {
               
               
                string fileExtension = Path.GetExtension(file.FileName);
               

                
                string folderPath = Path.Combine(basePathFileStorage, file.PlannedStoragePath);
                

              
                string fullFilePath = Path.Combine(folderPath, file.FileName);

                File.Delete(fullFilePath);


                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Upload_FileData(FileUploadDTO file)
        {
            bool bReturn = false;

            SqlCommand mySqlCommand = new SqlCommand();
            SqlConnection myConnection = new SqlConnection();

            #region Parameter

            // Add_SQLParameter(mySqlCommand, "@ID", id);
            mySqlCommand.Parameters.AddWithValue("@STOREDFILENAME", file.FileName);
            mySqlCommand.Parameters.AddWithValue("@CONTENTTYPE", file.ContentType);
            mySqlCommand.Parameters.AddWithValue("@UPLOADEDBY", "Keno Guthier");
            mySqlCommand.Parameters.AddWithValue("@STORAGEPATH", file.PlannedStoragePath);
              
                
            #endregion

            mySqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            myConnection.ConnectionString = "Server = 127.0.0.1; Initial Catalog = DBIDB; Integrated Security = true; Pooling = true;";
            mySqlCommand.CommandText = "insert_file";
            mySqlCommand.Connection = myConnection;

            myConnection.Open();
            SqlTransaction myTrans = myConnection.BeginTransaction();
            mySqlCommand.Transaction = myTrans;

            try
            {
                mySqlCommand.ExecuteNonQuery();
                myTrans.Commit();
                bReturn = true;
            }
            catch (Exception ex)
            {
                myTrans.Rollback();
                throw ex;
            }
            finally
            {
                myConnection.Close();
            }

            return bReturn;
        }

        #endregion
        #region Hilfsmethoden

        public static void Add_SQLParameter(
                               System.Data.SqlClient.SqlCommand mySqlCommand,
                               string sVariable,
                               object objWert,
                               System.Data.ParameterDirection pdParameterRichtung = System.Data.ParameterDirection.Input)
        {
            System.Data.SqlClient.SqlParameter sqlparamMySqlParameter = mySqlCommand.CreateParameter();

            sqlparamMySqlParameter.ParameterName = sVariable;
            sqlparamMySqlParameter.Value = objWert;
            sqlparamMySqlParameter.Direction = pdParameterRichtung;


            mySqlCommand.Parameters.Add(sqlparamMySqlParameter);
            //
        }

        public static void Add_SQLParameter(
                                        System.Data.SqlClient.SqlCommand mySqlCommand,
                                        string sVariable,
                                        object objWert,
                                        System.Data.DbType DBType,
                                        System.Data.ParameterDirection pdParameterRichtung = System.Data.ParameterDirection.Input)
        {
            System.Data.SqlClient.SqlParameter sqlparamMySqlParameter = mySqlCommand.CreateParameter();

            sqlparamMySqlParameter.ParameterName = sVariable;
            sqlparamMySqlParameter.Value = objWert;
            sqlparamMySqlParameter.DbType = DBType;
            sqlparamMySqlParameter.Direction = pdParameterRichtung;

            mySqlCommand.Parameters.Add(sqlparamMySqlParameter);
            //
        }

        public static void Add_SQLParameter(
                                        System.Data.SqlClient.SqlCommand mySqlCommand,
                                        string sVariable,
                                        object objWert,
                                        System.Data.DbType DBType,
                                        int nSize,
                                        System.Data.ParameterDirection pdParameterRichtung = System.Data.ParameterDirection.Input)
        {
            System.Data.SqlClient.SqlParameter sqlparamMySqlParameter = mySqlCommand.CreateParameter();

            sqlparamMySqlParameter.ParameterName = sVariable;
            sqlparamMySqlParameter.Value = objWert;
            sqlparamMySqlParameter.DbType = DBType;
            sqlparamMySqlParameter.Size = nSize;
            sqlparamMySqlParameter.Direction = pdParameterRichtung;

            mySqlCommand.Parameters.Add(sqlparamMySqlParameter);
            //
        }
        #endregion

    }

}
