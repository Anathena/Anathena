namespace Anna

open System
open System.Windows
open System.Windows.Controls
open System.Windows.Markup
open Anna.Source.VestigialExampleCode

module Entry = 
    // Create the View and bind it to the View Model
    let mainWindowViewModel = Application.LoadComponent(
                                 new System.Uri("/App;component/GUI/Main.xaml", UriKind.Relative)) :?> Window
    mainWindowViewModel.DataContext <- new MainWindowViewModel() 

    // Application Entry point
    [<STAThread>]
    [<EntryPoint>]
    let main(_) = (new Application()).Run(mainWindowViewModel)