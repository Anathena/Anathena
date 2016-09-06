namespace Anna.Source.Main

open System
open System.Collections.ObjectModel
open System.Windows
open System.Windows.Data
open System.Windows.Input
open System.ComponentModel
open Anna.Source.MVVM.ViewModel
open Anna.Source

type MainWindowViewModel() =
    inherit ViewModelBase()

    member this.OpenProcessSelector = 
        new RelayCommand ((fun canExecute -> true), 
            (fun action ->
                let mainWindowViewModel = Application.LoadComponent(ViewUris.processSelectorView) :?> Window
                mainWindowViewModel.DataContext <- new MainWindowViewModel()
                mainWindowViewModel.Show()))