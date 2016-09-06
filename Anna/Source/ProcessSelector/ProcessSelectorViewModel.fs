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

open Anna.Source.Engine.OperatingSystems.Windows.Processes

type ProcessSelectorViewModel(processSelectorModel : ProcessSelectorModel) =   
    inherit ViewModelBase()

    new () = ProcessSelectorViewModel(ProcessSelectorModel())

    member this.ProcessObjects = 
        new ObservableCollection<NormalizedProcess>(
            //processSelectorModel.GetAll())
            (new ProcessCollector()).GetProcesses())