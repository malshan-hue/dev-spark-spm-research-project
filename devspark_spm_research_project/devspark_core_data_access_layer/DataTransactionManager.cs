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

        #region Forum Manager

        private DataManager<Question> _questionDataManager;
        public DataManager<Question> questionDataManager
        {
            get
            {
                if (this._questionDataManager == null)
                {
                    this._questionDataManager = new DataManager<Question>(_connectionString);
                }

                return this._questionDataManager;
            }
        }

        private DataManager<Answer> _answerDataManager;
        public DataManager<Answer> answerDataManager
        {
            get
            {
                if (this._answerDataManager == null)
                {
                    this._answerDataManager = new DataManager<Answer>(_connectionString);
                }

                return this._answerDataManager;
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
