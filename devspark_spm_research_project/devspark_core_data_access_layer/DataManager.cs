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

                        sqlCommand.Parameters.AddWithValue("@JsonData", jsonString);

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

                        if (parameters != null)
                        {
                            sqlCommand.Parameters.AddRange(parameters);
                        }

                        sqlConnection.Open();

                        var jsonData = sqlCommand.ExecuteScalar() as string;

                        if (!string.IsNullOrEmpty(jsonData))
                        {
                            data = JsonConvert.DeserializeObject<ICollection<TEntity>>(jsonData) ?? new List<TEntity>();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
          
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
