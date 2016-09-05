namespace Anna.Source.ViewModel

open System
open System.Xml
open System.Windows
open System.Windows.Data
open System.Windows.Input
open System.ComponentModel
open System.Collections.ObjectModel
open Anna.Source.Model
open Anna.Source.Repository
open System.Reflection

type ApprovalStatus =
    | Approved
    | Rejected

type ExpenseItHomeViewModel(expenseReportRepository : ExpenseReportRepository) =   
    inherit ViewModelBase()
    let mutable lastApprovalDisplayMessage = ""
    let mutable selectedExpenseReport = 
        {Name=""; Department=""; ExpenseLineItems = []}
    let handleApprovalAction (this:ExpenseItHomeViewModel) approvalStatus name =
        match approvalStatus with
        | ApprovalStatus.Approved -> 
            this.LastApprovalDisplayMessage <- sprintf "Expense report approved for %s" name
        | ApprovalStatus.Rejected ->
            this.LastApprovalDisplayMessage <- sprintf "Expense report rejected for %s" name
    new () = ExpenseItHomeViewModel(ExpenseReportRepository())
    member x.ExpenseReports = 
        new ObservableCollection<ExpenseReport>(
            expenseReportRepository.GetAll())
    member x.SelectedExpenseReport 
        with get () = selectedExpenseReport
        and set value = selectedExpenseReport <- value
                        x.LastApprovalDisplayMessage <- ""
                        x.OnPropertyChanged "SelectedExpenseReport"
    member x.LastApprovalDisplayMessage 
        with get() = lastApprovalDisplayMessage
        and set value = 
            lastApprovalDisplayMessage <- value 
            x.OnPropertyChanged "LastApprovalDisplayMessage"
    member x.ApproveExpenseReportCommand = 
        new RelayCommand ((fun canExecute -> true), 
            (fun action -> handleApprovalAction x ApprovalStatus.Approved x.SelectedExpenseReport.Name)) 
    member x.RejectExpenseReportCommand = 
        new RelayCommand ((fun canExecute -> true), 
            (fun action -> handleApprovalAction x ApprovalStatus.Rejected x.SelectedExpenseReport.Name)) 
    member x.MouseEnterButtonCommand =
        new RelayCommand ((fun _ -> true), 
            (fun value -> x.LastApprovalDisplayMessage <- 
                              sprintf "%s %s's Expense Report?" (string value) x.SelectedExpenseReport.Name))
    member x.MouseLeaveButtonCommand =
        // Note: This is just for example purposes. It does not exemplify how this should be done in a production app.
        new RelayCommand ((fun _ -> true), 
            (fun value -> 
                if (not (x.LastApprovalDisplayMessage.StartsWith "Expense")) then
                    x.LastApprovalDisplayMessage <- ""))