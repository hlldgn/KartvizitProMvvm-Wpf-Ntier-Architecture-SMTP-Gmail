using KartvizitPro.View;
using MaterialDesignThemes.Wpf;
using System.Windows;

namespace KartvizitPro.ViewModel
{
    public static class CustomMessageBoxViewModel
    {
        private const MessageBoxButton _defaultButton = MessageBoxButton.OK;

        /// <summary>
        /// Displays a message box that has a message, a title bar caption, a specified button combination, a specified icon and a button color scheme; and that returns a result.
        /// </summary>
        /// <param name="messageBoxText">A System.String that specifies the text to display.</param>
        /// <param name="button">A System.Windows.MessageBoxButton value that specifies which button or buttons to display.</param>
        /// <param name="icon">A MaterialDesignThemes.Wpf.PackIconKind that specifies the icon to show to the left of the text.</param>
        /// <returns>A System.Windows.MessageBoxResult value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult ShowDialog(string messageBoxText, MessageBoxButton? button = null, PackIconKind? icon = null)
        {
            return ShowDialogCore(messageBoxText, button ?? _defaultButton, icon);
        }

        private static MessageBoxResult ShowDialogCore(string messageBoxText, MessageBoxButton? button, PackIconKind? icon)
        {
            // Setup new messagebox
            var messageBox = new CustomMessageBox(messageBoxText, button ?? _defaultButton, icon);
            messageBox.ShowDialog();
            return messageBox.Result;
        }
    }
}
