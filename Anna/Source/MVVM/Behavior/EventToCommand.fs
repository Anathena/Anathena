namespace Anna.Source.MVVM.Behavior

open System
open System.Windows
open System.Windows.Input
// This example requires Microsoft Expression Blend to be installed. Other items are commented out in the XAML markup
//open System.Windows.Interactivity

//type EventToCommand() =
//    inherit TriggerAction<DependencyObject>()
//    [<DefaultValue(false)>] static val mutable private CommandProperty:DependencyProperty
//    [<DefaultValue(false)>] static val mutable private CommandParameterProperty:DependencyProperty
//    
//    /// Set the command dependency property
//    static do 
//        EventToCommand.CommandProperty <-
//            DependencyProperty.Register("Command", typeof<ICommand>, typeof<EventToCommand>)
//    
//    /// Set the command parameter dependency property
//    static do 
//        EventToCommand.CommandParameterProperty <-
//            DependencyProperty.Register("CommandParameter", typeof<obj>, typeof<EventToCommand>)
//    
//    /// Get/Set the Command 
//    member this.Command 
//        with get() = this.GetValue EventToCommand.CommandProperty :?> ICommand
//        and set value = this.SetValue(EventToCommand.CommandProperty, value)
//    
//    /// Get/Set the CommandParameter 
//    member this.CommandParameter 
//        with get() = this.GetValue EventToCommand.CommandParameterProperty 
//        and set value = this.SetValue(EventToCommand.CommandParameterProperty, value)
//    
//    /// Implement the Invoke method from TriggerAction to execute the command
//    override this.Invoke parameter = 
//        let command = this.Command
//        let commandParameter = match this.CommandParameter with
//                               | null -> parameter
//                               | commandParam -> commandParam  
//        if command <> null && command.CanExecute(commandParameter) then
//            command.Execute(commandParameter)