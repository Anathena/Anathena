namespace Ana.Source.Mvvm.Views
{
    /// <summary>
    /// An interface defining how navigation between pages should
    /// be performed in various frameworks such as Windows, 
    /// Windows Phone, Android, iOS etc.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// The key corresponding to the currently displayed page.
        /// </summary>
        string CurrentPageKey
        {
            get;
        }

        /// <summary>
        /// If possible, instructs the navigation service
        /// to discard the current page and display the previous page
        /// on the navigation stack.
        /// </summary>
        void GoBack();

        /// <summary>
        /// Instructs the navigation service to display a new page
        /// corresponding to the given key. Depending on the platforms,
        /// the navigation service might have to be configured with a
        /// key/page list.
        /// </summary>
        /// <param name="pageKey">The key corresponding to the page
        /// that should be displayed.</param>
        void NavigateTo(string pageKey);

        /// <summary>
        /// Instructs the navigation service to display a new page
        /// corresponding to the given key, and passes a parameter
        /// to the new page.
        /// Depending on the platforms, the navigation service might 
        /// have to be Configure with a key/page list.
        /// </summary>
        /// <param name="pageKey">The key corresponding to the page
        /// that should be displayed.</param>
        /// <param name="parameter">The parameter that should be passed
        /// to the new page.</param>
        void NavigateTo(string pageKey, object parameter);
    }
    //// End class
}
//// End namespace