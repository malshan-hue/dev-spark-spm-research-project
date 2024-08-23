using devspark_core_business_layer.SystemService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_business_layer.SystemService
{
    public class DatabaseServiceImpl : IDatabaseService
    {
        private string _connectionString;

        public DatabaseServiceImpl() { }
        
        public string GetConnectionString()
        {
            return _connectionString;
        }

        public bool SetConnectionString(string connectionString = "")
        {
           _connectionString = connectionString;
            return true;
        }
    }
}
