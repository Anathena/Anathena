namespace Anna.Source.MVVM.ViewModel

open System
open System.Windows
open System.Windows.Input
open System.ComponentModel

type RelayCommand (canExecute:(Object -> Boolean), action:(Object -> Unit)) =
    let event = new DelegateEvent<EventHandler>()
    interface ICommand with
        [<CLIEvent>]
        member x.CanExecuteChanged = event.Publish
        member x.CanExecute arg = canExecute(arg)
        member x.Execute arg = action(arg)