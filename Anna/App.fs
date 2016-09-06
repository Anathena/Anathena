namespace Anna

open System
open System.Windows
open System.Windows.Controls
open System.Windows.Markup
open Anna.Source.Main
open Anna.Source

module Entry = 
    // Create the View and bind it to the View Model
    let mainWindowViewModel = Application.LoadComponent(ViewUris.mainView) :?> Window
    mainWindowViewModel.DataContext <- new MainWindowViewModel() 

    // Application Entry point
    [<STAThread>]
    [<EntryPoint>]
    let main(_) = (new Application()).Run(mainWindowViewModel)