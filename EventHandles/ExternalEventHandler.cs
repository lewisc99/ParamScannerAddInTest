using Autodesk.Revit.UI;

namespace ParamScannerAddIn.EventHandles
{
    public abstract class ExternalEventHandler<TType> : IExternalEventHandler
    {
        private readonly object _lock;
        private TType _savedArgs;
        private readonly ExternalEvent _revitEvent;

        protected ExternalEventHandler()
        {
            _revitEvent = ExternalEvent.Create(this);
            _lock = new object();
        }

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

        public string GetName() => GetType().Name;

        public void Raise(TType args)
        {
            lock (_lock)
            {
                _savedArgs = args;
            }

            _revitEvent.Raise();
        }

        public abstract void Execute(UIApplication app, TType args);
    }
}
