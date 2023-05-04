using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Services.Model;


namespace Services
{
    public class Service
    {
        private string _sqlConnString = String.Empty;

        public Service()
        {
            PrepareConnection();
        }


        public void InsertRecord(RecordModel record)
        {
            string query = $"Insert into dbo.Game (playerName,hp,atk)" +
                $"VALUES( '{record.playerName}','{record.hp}','{record.atk}')";


            using (SqlConnection conn = new SqlConnection(_sqlConnString))
            {
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                conn.Open();

                sqlCommand.ExecuteNonQuery();

                conn.Close();
            }
        }

        public int APIInsertRecord(RecordModel record)
        {

            using (SqlConnection conn = new SqlConnection(_sqlConnString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("[dbo].[ApiInsertRecord]", conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@id", record.id).Direction = ParameterDirection.Input;
                    sqlCommand.Parameters.AddWithValue("@playerName", record.playerName).Direction = ParameterDirection.Input;
                    sqlCommand.Parameters.AddWithValue("@hp", record.hp).Direction = ParameterDirection.Input;
                    sqlCommand.Parameters.AddWithValue("@atk", record.atk).Direction = ParameterDirection.Input;
                    sqlCommand.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.Output;


                    conn.Open();
                    sqlCommand.ExecuteNonQuery();
                    var result = (int)sqlCommand.Parameters["@ReturnValue"].Value;
                    conn.Close();
                    return result;

                }
            }
        }
        public List<RecordModel> APIGetAll()
        {
            List<RecordModel> list = new List<RecordModel>();

            using (SqlConnection conn = new SqlConnection(_sqlConnString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("[dbo].[ApiGetAll]", conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    var result = sqlCommand.ExecuteReader();


                    while (result.Read())
                    {
                        RecordModel model = new RecordModel();
                        model.id = (int)result["runID"];
                        model.playerName = (string)result["playerName"];
                        model.hp = (int)result["hp"];
                        model.atk = (int)result["atk"];
                        list.Add(model);
                    }

                }

            }
            return list;
        }

        public List<RecordModel> APIGetbyName(string name)
        {
            List<RecordModel> list = new List<RecordModel>();

            using (SqlConnection conn = new SqlConnection(_sqlConnString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("[dbo].[ApiGetByName]", conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@playerName", name).Direction = ParameterDirection.Input;
                    conn.Open();
                    var result = sqlCommand.ExecuteReader();


                    while (result.Read())
                    {
                        RecordModel model = new RecordModel();
                        model.id = (int)result["runID"];
                        model.playerName = (string)result["playerName"];
                        model.hp = (int)result["hp"];
                        model.atk = (int)result["atk"];
                        list.Add(model);
                    }

                }

            }

            return list;
        }

        public RecordModel APIGetById(int id)
        {
            RecordModel model = new RecordModel();

            using (SqlConnection conn = new SqlConnection(_sqlConnString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("[dbo].[ApiGetById]", conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@id", id).Direction = ParameterDirection.Input;

                    conn.Open();
                    var result = sqlCommand.ExecuteReader();


                    while (result.Read())
                    {

                        model.id = (int)result["runID"];
                        model.playerName = (string)result["playerName"];
                        model.atk = (int)result["atk"];
                        model.hp = (int)result["hp"];
                    }

                }

            }

            return model;
        }

        public string APIDeleteById(int id)
        {


            using (SqlConnection conn = new SqlConnection(_sqlConnString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("[dbo].[ApiDeleteById]", conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@id", id).Direction = ParameterDirection.Input;
                    sqlCommand.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.Output;

                    conn.Open();
                    string result = sqlCommand.ExecuteNonQuery() == 0 ? "Fail" : "Success";
                    conn.Close();
                    return result;


                }

            }

        }

        private void PrepareConnection()
        {
            SqlConnectionStringBuilder connBldr = new SqlConnectionStringBuilder();
            connBldr.DataSource = $"(localdb)\\MSSQLLocalDB";
            connBldr.IntegratedSecurity = true;
            connBldr.InitialCatalog = $"PROG455SP23";
            _sqlConnString = connBldr.ConnectionString;
        }
    }

}
