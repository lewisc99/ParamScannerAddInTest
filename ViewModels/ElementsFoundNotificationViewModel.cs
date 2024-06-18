using Autodesk.Revit.DB;
using ParamScannerAddIn.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections.ObjectModel;
using ParamScannerAddIn.Commands;
using System;
using ParamScannerAddIn.Utils;

namespace ParamScannerAddIn.ViewModels
{
    public class ElementsFoundNotificationViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ElementParamModel> _elementsList;
        private string _elementsCountText;

        public ICommand ListOfElementsFoundCommand { get; }

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="elementsFoundList">List of all Elements found to be inserted into a list</param>
        public ElementsFoundNotificationViewModel(List<Element> elementsFoundList)
        {
            _elementsList = new ObservableCollection<ElementParamModel>();
            ListOfElementsFoundCommand = new RelayCommand(param => ListOfElementsFound(elementsFoundList));

            ListOfElementsFound(elementsFoundList);
        }
        #endregion

        #region ElementsList
        public ObservableCollection<ElementParamModel> ElementsList
        {
            get => _elementsList;
            set
            {
                _elementsList = value;
                OnPropertyChanged();
            }
        } 
        #endregion

        #region ElementsCountText
        public string ElementsCountText
        {
            get => _elementsCountText;
            set
            {
                _elementsCountText = value;
                OnPropertyChanged();
            }
        } 
        #endregion
       
        #region Config Event Handler
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region List Of Elements Found
        /// <summary>
        /// Will handle the list and add into a new list to show in the Elements Found View
        /// </summary>
        /// <param name="elementsFoundList">Elements found match by the Parameter Name and value</param>
        private void ListOfElementsFound(List<Element> elementsFoundList)
        {
            try
            {
                foreach (Element matchingElement in elementsFoundList)
                {
                    ElementParamModel element = new ElementParamModel
                    {
                        ElementId = matchingElement.Id.ToString(),
                        ElementName = matchingElement.Name
                    };

                    _elementsList.Add(element);
                }

                ElementsCountText = $"Parameters with Value Found: {_elementsList.Count}";
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
        } 
        #endregion
    }
}
