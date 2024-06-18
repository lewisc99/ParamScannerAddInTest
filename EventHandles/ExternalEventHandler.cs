using Autodesk.Revit.UI;

namespace ParamScannerAddIn.EventHandles
{
    public abstract class ExternalEventHandler<TType> : IExternalEventHandler
    {
        #region Properties
        private readonly object _lock;
        private TType _savedArgs;
        private readonly ExternalEvent _revitEvent;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        protected ExternalEventHandler()
        {
            _revitEvent = ExternalEvent.Create(this);
            _lock = new object();
        }
        #endregion

        #region Execute the Event
        /// <summary>
        /// Responsible to Execute Event
        /// </summary>
        /// <param name="app">Application Startup Parameter</param>
        public void Execute(UIApplication app)
        {
            TType args;

            lock (_lock)
            {
                args = _savedArgs;
                _savedArgs = default;
            }

            Execute(app, args);
        } 
        #endregion

        public string GetName() => GetType().Name;

        #region Raise Event
        /// <summary>
        /// Responsible to Raise the Event
        /// </summary>
        /// <param name="args">Generaly Method is passed to Raise External Events that need Transaction</param>
        public void Raise(TType args)
        {
            lock (_lock)
            {
                _savedArgs = args;
            }

            _revitEvent.Raise();
        } 
        #endregion

        public abstract void Execute(UIApplication app, TType args);
    }
}
