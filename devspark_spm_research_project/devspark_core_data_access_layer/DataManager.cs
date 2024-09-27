using Newtonsoft.Json;
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

        public bool UpdateData(string procedureName, string jsonString)
        {
            bool status = false;
            try
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    using (var sqlCommand = new SqlCommand(procedureName, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        sqlCommand.Parameters.AddWithValue("@jsonString", jsonString);

                        var executionStatusParam = new SqlParameter
                        {
                            ParameterName = "@executionStatus",
                            SqlDbType = SqlDbType.Bit,
                            Direction = ParameterDirection.Output
                        };
                        sqlCommand.Parameters.Add(executionStatusParam);

                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();

                        status = (bool)sqlCommand.Parameters["@executionStatus"].Value;

                        return status;
                    }
                }
            }
            catch (Exception ex)
            {
                return status;
            }
        }

        public ICollection<TEntity> RetrieveData<TEntity>(string procedureName, SqlParameter[] parameters = null) where TEntity : class
        {
            ICollection<TEntity> data = new List<TEntity>();

            try
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    using (var sqlCommand = new SqlCommand(procedureName, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        sqlCommand.CommandTimeout = 180; // 3 minutes or more

                        if (parameters != null)
                        {
                            sqlCommand.Parameters.AddRange(parameters);
                        }

                        sqlConnection.Open();

                        // Use ExecuteReader to retrieve the JSON result
                        using (var reader = sqlCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var jsonData = reader.GetString(0); // Assumes first column is the JSON result

                                if (!string.IsNullOrEmpty(jsonData))
                                {
                                    try
                                    {
                                        System.Diagnostics.Debug.WriteLine("JSON Data: " + jsonData);


                                        // Deserialize the JSON into a collection of TEntity objects
                                        data = JsonConvert.DeserializeObject<ICollection<TEntity>>(jsonData) ?? new List<TEntity>();
                                    }
                                    catch (JsonReaderException jsonEx)
                                    {
                                        // Handle any JSON-specific deserialization errors
                                        Console.WriteLine($"JSON Deserialization error: {jsonEx.Message}");
                                        throw;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle general exceptions (e.g., database connection issues)
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }

            return data;
        }



        public bool DeleteData(string procedureName, SqlParameter[] parameters = null)
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

                            if (parameters != null)
                            {
                                sqlCommand.Parameters.AddRange(parameters);
                            }

                            var executionStatusParam = new SqlParameter
                            {
                                ParameterName = "@executionStatus",
                                SqlDbType = SqlDbType.Bit,
                                Direction = ParameterDirection.Output
                            };

                            sqlCommand.Parameters.Add(executionStatusParam);

                            sqlConnection.Open();
                            sqlCommand.ExecuteNonQuery();

                            status = (bool)sqlCommand.Parameters["@executionStatus"].Value;

                            return status;

                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                        finally
                        {

                            sqlConnection.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return status;
            }



        }
        public async Task<TEntity> RetrieveSingleData<TEntity>(string procedureName, SqlParameter[] parameters = null) where TEntity : class
        {
            TEntity entity = null;

            try
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    using (var sqlCommand = new SqlCommand(procedureName, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        if (parameters != null)
                        {
                            sqlCommand.Parameters.AddRange(parameters);
                        }

                        var outputParam = new SqlParameter("@jsonResult", SqlDbType.NVarChar, -1)
                        {
                            Direction = ParameterDirection.Output
                        };

                        sqlCommand.Parameters.Add(outputParam);

                        sqlConnection.Open();
                        await sqlCommand.ExecuteNonQueryAsync();

                        var jsonResult = outputParam.Value as string;

                        if (!string.IsNullOrEmpty(jsonResult))
                        {
                            entity = JsonConvert.DeserializeObject<TEntity>(jsonResult);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }

            return entity;
        }

      



    }
}
