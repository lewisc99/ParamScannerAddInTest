using System;
using System.Windows;

namespace ParamScannerAddIn.Utils
{
    public static class ExceptionHandler
    {
        /// <summary>
        /// Handle the Exception Message to show more information to the user.
        /// </summary>
        /// <param name="ex"></param>
        public static void HandleException(Exception ex)
        {
            MessageBox.Show($"An error occurred:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
