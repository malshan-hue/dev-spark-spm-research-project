using devspark_core_model.DeveloperPortalModels;
using devspark_core_model.SystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_data_access_layer
{
    public class DataTransactionManager : IDisposable
    {
        private string _connectionString;

        public DataTransactionManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region User Manager

        private DataManager<User> _userDatamanager;
        public DataManager<User> userDataManager
        {
            get
            {
                if(this._userDatamanager == null)
                {
                    this._userDatamanager = new DataManager<User>(_connectionString);
                }

                return this._userDatamanager;
            }
        }

        #endregion

        #region Developer

        private DataManager<Folder> _devSpaceManager;
        public DataManager<Folder> devSpaceManager
        {
            get
            {
                if (this._devSpaceManager == null)
                {
                    this._devSpaceManager = new DataManager<Folder>(_connectionString);
                }

                return this._devSpaceManager;
            }
        }

        #endregion

        private bool _disposed = false;
        protected void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {

                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
