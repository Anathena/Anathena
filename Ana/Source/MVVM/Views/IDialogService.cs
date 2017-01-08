namespace Ana.Source.Mvvm.Views
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// An interface defining how dialogs should be displayed in various frameworks such as Windows,  Windows Phone, Android, iOS etc.
    /// </summary>
    internal interface IDialogService
    {
        /// <summary>
        /// Displays information about an error.
        /// </summary>
        /// <param name="message">The message to be shown to the user.</param>
        /// <param name="title">The title of the dialog box. This may be null.</param>
        /// <param name="buttonText">The text shown in the only button in the dialog box. If left null, the text "OK" will be used.</param>
        /// <param name="afterHideCallback">A callback that should be executed after the dialog box is closed by the user.</param>
        /// <returns>A Task allowing this async method to be awaited.</returns>
        Task ShowError(String message, String title, String buttonText, Action afterHideCallback);

        /// <summary>
        /// Displays information about an error.
        /// </summary>
        /// <param name="error">The exception of which the message must be shown to the user.</param>
        /// <param name="title">The title of the dialog box. This may be null.</param>
        /// <param name="buttonText">The text shown in the only button in the dialog box. If left null, the text "OK" will be used.</param>
        /// <param name="afterHideCallback">A callback that should be executed after the dialog box is closed by the user.</param>
        /// <returns>A Task allowing this async method to be awaited.</returns>
        Task ShowError(Exception error, String title, String buttonText, Action afterHideCallback);

        /// <summary>
        /// Displays information to the user. The dialog box will have only one button with the text "OK".
        /// </summary>
        /// <param name="message">The message to be shown to the user.</param>
        /// <param name="title">The title of the dialog box. This may be null.</param>
        /// <returns>A Task allowing this async method to be awaited.</returns>
        Task ShowMessage(String message, String title);

        /// <summary>
        /// Displays information to the user. The dialog box will have only one button.
        /// </summary>
        /// <param name="message">The message to be shown to the user.</param>
        /// <param name="title">The title of the dialog box. This may be null.</param>
        /// <param name="buttonText">The text shown in the only button in the dialog box. If left null, the text "OK" will be used.</param>
        /// <param name="afterHideCallback">A callback that should be executed after the dialog box is closed by the user.</param>
        /// <returns>A Task allowing this async method to be awaited.</returns>
        Task ShowMessage(String message, String title, String buttonText, Action afterHideCallback);

        /// <summary>
        /// Displays information to the user. The dialog box will have only
        /// one button.
        /// </summary>
        /// <param name="message">The message to be shown to the user.</param>
        /// <param name="title">The title of the dialog box. This may be null.</param>
        /// <param name="buttonConfirmText">The text shown in the "confirm" button in the dialog box. If left null, the text "OK" will be used.</param>
        /// <param name="buttonCancelText">The text shown in the "cancel" button in the dialog box. If left null, the text "Cancel" will be used.</param>
        /// <param name="afterHideCallback">A callback that should be executed after the dialog box is closed by the user. The callback method will get a boolean
        /// parameter indicating if the "confirm" button (true) or the "cancel" button (false) was pressed by the user.</param>
        /// <returns>A Task allowing this async method to be awaited. The task will return true or false depending on the dialog result.</returns>
        Task<bool> ShowMessage(String message, String title, String buttonConfirmText, String buttonCancelText, Action<Boolean> afterHideCallback);

        /// <summary>
        /// Displays information to the user in a simple dialog box. The dialog box will have only one button with the text "OK". This method should be used for debugging purposes.
        /// </summary>
        /// <param name="message">The message to be shown to the user.</param>
        /// <param name="title">The title of the dialog box. This may be null.</param>
        /// <returns>A Task allowing this async method to be awaited.</returns>
        Task ShowMessageBox(String message, String title);
    }
    //// End class
}
//// End namespace