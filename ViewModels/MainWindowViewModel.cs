using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ParamScannerAddIn.Commands;
using ParamScannerAddIn.EventHandles;
using ParamScannerAddIn.Models;
using ParamScannerAddIn.Utils;
using ParamScannerAddIn.Views.ElementsFoundNotification;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ParamScannerAddIn.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Properties
        private ParameterModel _parameterModel;
        private readonly Document _doc;
        private readonly UIDocument _uiDoc;
        private readonly IsolateElementsHandler _mIsolateElementsHandler;
        private ElementsFoundNotification _elementsFoundNotificationWindow;
        #endregion

        #region Commands
        public ICommand IsolateInViewCommand { get; }
        public ICommand SelectElementsByParameterCommand { get; }
        #endregion

        #region Constructor
        public MainWindowViewModel(Document doc, UIDocument uiDoc, IsolateElementsHandler isolateElementsHandler)
        {
            _parameterModel = new ParameterModel();
            _doc = doc;
            _uiDoc = uiDoc;
            _mIsolateElementsHandler = isolateElementsHandler;

            IsolateInViewCommand = new RelayCommand(IsolateInView);
            SelectElementsByParameterCommand = new RelayCommand(SelectElementsByParameter);
        }
        #endregion

        #region ParameterName
        public string ParameterName
        {
            get => _parameterModel.ParameterName;
            set
            {
                if (_parameterModel.ParameterName != value)
                {
                    _parameterModel.ParameterName = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region ParameterValue
        public string ParameterValue
        {
            get => _parameterModel.ParameterValue;
            set
            {
                if (_parameterModel.ParameterValue != value)
                {
                    _parameterModel.ParameterValue = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Config Event handlers
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Isolate Elements in a new view
        /// <summary>
        /// Isolate Elements in a new view
        /// </summary>
        /// <param name="parameter">Parameter Name</param>
        private void IsolateInView(object parameter)
        {
            if (string.IsNullOrEmpty(ParameterName))
            {
                ShowMessageNotification("Parameter Name cannot be left empty");
                return;
            }

            try
            {
                if (_elementsFoundNotificationWindow != null)
                {
                    _elementsFoundNotificationWindow.Close();
                    _elementsFoundNotificationWindow = null;
                }

                GetParameterByNameAndValue parameterFinder = new GetParameterByNameAndValue();
                List<Element> elementsFound = parameterFinder.FindParameterByNameAndValue(_doc, ParameterName, ParameterValue);

                if (elementsFound.Count == 0)
                {
                    ShowMessageNotification("No elements found");
                    return;
                }

                _mIsolateElementsHandler.Uidoc = _uiDoc;
                _mIsolateElementsHandler.Raise(elementsFound.Select(elementItem => elementItem.Id).ToList());

                if (!string.IsNullOrEmpty(this.ParameterName) && !string.IsNullOrEmpty(this.ParameterValue))
                {
                    _elementsFoundNotificationWindow = new ElementsFoundNotification(elementsFound);
                    _elementsFoundNotificationWindow.Show();
                }

                ShowMessageNotification("Item Isolated");

            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
        }
        #endregion

        #region Select Elements By Parameter in View
        /// <summary>
        /// Select Elements By Parameter in View
        /// </summary>
        /// <param name="parameter">Parameter Name</param>
        private void SelectElementsByParameter(object parameter)
        {
            if (string.IsNullOrEmpty(ParameterName))
            {
                ShowMessageNotification("Parameter Name cannot be left empty");
                return;
            }

            try
            {

                if (_elementsFoundNotificationWindow != null)
                {
                    _elementsFoundNotificationWindow.Close();
                    _elementsFoundNotificationWindow = null;
                }

                GetParameterByNameAndValue parameterFinder = new GetParameterByNameAndValue();
                List<Element> elementsFound = parameterFinder.FindParameterByNameAndValue(_doc, ParameterName, ParameterValue);

                if (elementsFound.Count == 0)
                {
                    ShowMessageNotification("No elements found");
                    return;
                }

                _uiDoc.Selection.SetElementIds(elementsFound.Select(foundElement => foundElement.Id).ToList());

                if (!string.IsNullOrEmpty(this.ParameterName) && !string.IsNullOrEmpty(this.ParameterValue))
                {
                    _elementsFoundNotificationWindow = new ElementsFoundNotification(elementsFound);
                    _elementsFoundNotificationWindow.Show();
                }

                ShowMessageNotification("Items Selected");

            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
        }
        #endregion

        #region Show Message Notification Box
        /// <summary>
        /// Show  Message Notification Box
        /// </summary>
        /// <param name="message">Message Text</param>
        private void ShowMessageNotification(string message)
        {
            TaskDialog.Show("Notification", message);
        } 
        #endregion
    }
}
