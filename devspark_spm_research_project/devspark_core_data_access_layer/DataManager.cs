using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_data_access_layer
{
    public class DataManager<TEntity> where TEntity : class
    {
        private string _connectionString;

        public DataManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool InsertData(string procedureName, string jsonString)
        {
            bool status = false;

            try
            {

                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    using (var sqlCommand = new SqlCommand(procedureName, sqlConnection))
                    {

                        try
                        {

                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            sqlCommand.Parameters.AddWithValue("@jsonString", jsonString);

                            var executionStatusParam = new SqlParameter
                            {
                                ParameterName = "@executionStatus",
                                SqlDbType = SqlDbType.Bit,
                                Direction = ParameterDirection.Output,
                            };

                            sqlCommand.Parameters.Add(executionStatusParam);

                            sqlConnection.Open();
                            sqlCommand.ExecuteNonQuery();

                            status = (bool)sqlCommand.Parameters["@executionStatus"].Value;

                            return status;
                        }
                        catch (Exception ex)
                        {
                            return status;
                        }
                        finally
                        {
                            sqlConnection.Close();
                        }

                    }
                }

            }catch (Exception ex)
            {
                return status;
            }
        }
    }
}
