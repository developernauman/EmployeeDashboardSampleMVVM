using System.Threading.Tasks;

namespace Sample.Common
{
    public abstract class CommonBaseDataProvider<T, M> : IDataProvider<M> where T : class where M : class
    {
        #region Properties

        private bool _allowExecute = true;
        protected readonly string _authHeader;

        protected T Service { get; set; }
        public M Data { get; protected set; }

        #endregion

        #region Constructor

        protected CommonBaseDataProvider(string authHeader, bool processHeader)
        {
            _authHeader = authHeader;

            if (processHeader && !string.IsNullOrWhiteSpace(_authHeader))
            {
                _allowExecute = Task.Run(async () => await AttemptToParseHeader()).Result;
            }
        }

        #endregion

        #region Public Methods

        public async Task Execute()
        {
            if (!_allowExecute)
            {
                _allowExecute = await Task.Run(() => false);
                return;
            }

            await ExecuteBody();
        }

        #endregion

        #region Protected/Abstract Methods

        protected abstract Task ExecuteBody();

        #endregion

        #region Private Methods

        private async Task<bool> AttemptToParseHeader()
        {
            return true;
        }

        #endregion
    }
}
