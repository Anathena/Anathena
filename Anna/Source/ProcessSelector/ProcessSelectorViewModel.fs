namespace Anna.Source.ProcessSelector
open System
open System.Xml
open System.Windows
open System.Windows.Data
open System.Windows.Input
open System.ComponentModel
open System.Collections.ObjectModel
open System.Reflection
open Anna.Source.MVVM.ViewModel
open Anna.Source.Engine.OperatingSystems

type ApprovalStatus =
    | Approved
    | Rejected

type ProcessSelectorViewModel(processSelectorPreviewItems : ProcessSelectorPreviewItems) =   
    inherit ViewModelBase()
    let mutable selectedProcessObject = 
        {Name=""; Department=""; ProcessObjectLineItems = []}

    new () = ProcessSelectorViewModel(ProcessSelectorPreviewItems())

    member x.ProcessObjects = 
        new ObservableCollection<ProcessObject>(
            processSelectorPreviewItems.GetAll())