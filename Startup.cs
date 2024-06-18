using Autodesk.Revit.UI;
using ParamScannerAddIn.Views.MainWindow;
using System.Reflection;
using System.Windows.Media.Imaging;
using System;
using ParamScannerAddIn.Utils;
using ParamScannerAddIn.EventHandles;
using System.Windows;
using System.Windows.Threading;

namespace ParamScannerAddIn
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class Startup : IExternalApplication
    {
        #region Properties
        public static Startup _thisApplication;
        private MainWindow _mMyMainWindow; 
        #endregion

        #region OnShutdown
        /// <summary>
        /// When the application is closing
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public Result OnShutdown(UIControlledApplication a) => Result.Succeeded;
        #endregion

        #region OnStartup
        /// <summary>
        /// When the Application Startup will call this method
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public Result OnStartup(UIControlledApplication application)
        {
            _mMyMainWindow = null;
            _thisApplication = this;

            try
            {
                #region Create Ribbon Panel
                RibbonPanel panel = CreateRibbonPanel(application);

                string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

                if (panel.AddItem(new PushButtonData("ParamScannerAddIn", "Parameter Scanner", thisAssemblyPath, "ParamScannerAddIn.MainCommand"))
                    is PushButton button)
                {
                    button.ToolTip = "Parameter Scanner";

                    Uri uriImage = new Uri("pack://application:,,,/ParamScannerAddIn;component/Resources/Parameters.ico");
                    BitmapImage bitmapImage = new BitmapImage(uriImage);
                    button.LargeImage = bitmapImage;
                }
                #endregion

                return Result.Succeeded;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return Result.Failed;
            }
        }
        #endregion

        #region Create Ribbon Panel
        /// <summary>
        /// Method To Create the Ribbon Panel and tab
        /// </summary>
        /// <param name="uiControlApp"></param>
        /// <returns></returns>
        public RibbonPanel CreateRibbonPanel(UIControlledApplication uiControlApp)
        {
            string tab = "Parametes";

            try
            {
                uiControlApp.CreateRibbonTab(tab);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }

            RibbonPanel ribbonPanel = null;

            try
            {
                ribbonPanel = uiControlApp.CreateRibbonPanel(tab, "Scanner Parameters");
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }

            return ribbonPanel;
        }

        #endregion

        #region Show Main Window App
        /// <summary>
        /// To Show the Main Window
        /// </summary>
        /// <param name="uiapp"></param>
        public void ShowWindow(UIApplication uiapp)
        {
            if (_mMyMainWindow == null || !_mMyMainWindow.IsVisible)
            {
                IsolateElementsHandler evStr = new IsolateElementsHandler();
                _mMyMainWindow = new MainWindow(uiapp, evStr);

                if (Application.Current != null)
                {
                    Application.Current.DispatcherUnhandledException += OnDispatcherUnhandledException;
                }
            }
            else
            {
                _mMyMainWindow.Activate();
                return;
            }

            _mMyMainWindow.Show();
        }
        #endregion

        #region To Handle uncaught exceptions
        /// <summary>
        /// To Handle uncaught exceptions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            ExceptionHandler.HandleException(e.Exception);
        } 
        #endregion
    }
}
