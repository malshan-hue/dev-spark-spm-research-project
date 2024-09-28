using devspark_core_model.ContributionPortalModels;
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

        private DataManager<Module> _moduleDatamanager;
        public DataManager<Module> ModuleDataManager
        {
            get
            {
                if (this._moduleDatamanager == null)
                {
                    this._moduleDatamanager = new DataManager<Module>(_connectionString);
                }

                return this._moduleDatamanager;
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

        #region Contributer

        private DataManager<CodeSnippetLibrary> _codeSnippetManager;
        public DataManager<CodeSnippetLibrary> codeSnippetManager
        {
            get
            {
                if (this._codeSnippetManager == null)
                {
                    this._codeSnippetManager = new DataManager<CodeSnippetLibrary>(_connectionString);
                }

                return this._codeSnippetManager;
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
