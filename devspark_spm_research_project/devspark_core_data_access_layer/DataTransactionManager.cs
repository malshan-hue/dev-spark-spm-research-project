using devspark_core_model.LearnerPortalModels;
using devspark_core_model.DeveloperPortalModels;
using devspark_core_model.ForumPortalModels;
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

        private DataManager<EntraIdUser> _entraIdUserDatamanager;
        public DataManager<EntraIdUser> EntraIdUserDataManager
        {
            get
            {
                if(this._entraIdUserDatamanager == null)
                {
                    this._entraIdUserDatamanager = new DataManager<EntraIdUser>(_connectionString);
                }

                return this._entraIdUserDatamanager;
            }
        }

        #endregion

        #region Lerver Service

        private DataManager<Course> _courseDatamanager;
        public DataManager<Course> CourseDataManager
        {
            get
            {
                if (this._courseDatamanager == null)
                {
                    this._courseDatamanager = new DataManager<Course>(_connectionString);
                }

                return this._courseDatamanager;
            }
        }

        private DataManager<CourseProgress> _courseProgressDatamanager;
        public DataManager<CourseProgress> CourseProgressDataManager
        {
            get
            {
                if (this._courseProgressDatamanager == null)
                {
                    this._courseProgressDatamanager = new DataManager<CourseProgress>(_connectionString);
                }

                return this._courseProgressDatamanager;
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
