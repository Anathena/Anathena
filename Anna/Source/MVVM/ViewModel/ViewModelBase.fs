namespace Anna.Source.MVVM.ViewModel

open System
open System.Windows
open System.Windows.Input
open System.ComponentModel
open Anna.Source.Engine.OperatingSystems

type ViewModelBase() =
    let propertyChangedEvent = new DelegateEvent<PropertyChangedEventHandler>()

    let mutable selectedProcess = {
        processId = 0
        processName = String.Empty
        startTime = DateTime.MinValue
        isSystemProcess = false
        icon = null
    }

    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

    member this.OnPropertyChanged propertyName = 
        propertyChangedEvent.Trigger([| this; new PropertyChangedEventArgs(propertyName) |])

    member this.SelectedProcess 
        with get () = selectedProcess
        and set value = selectedProcess <- value