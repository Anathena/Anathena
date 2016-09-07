namespace Anna.Source.MVVM.Behavior

open System
open System.Windows
open System.Windows.Input
open System.Windows.Controls
open System.ComponentModel

(*
type DialogCloser() =
    static let DialogResultProperty =
        DependencyProperty.RegisterAttached("DialogResult", typeof<bool>, typeof<DialogCloser>)

    member this.GetDialogResult (a:DependencyObject) = a.GetValue(DialogResultProperty) :?> bool

    member this.SetDialogResult (a:DependencyObject) (value:string) = a.SetValue(DialogResultProperty, value)

    member this.DialogResultChanged (a:DependencyObject) (e:DependencyPropertyChangedEventArgs) =
        let window = a :?> Window
        match window with
        | null -> failwith "Not a Window"
        | _ -> window.DialogResult <- System.Nullable(e.NewValue :?> bool)
*)

type MouseDoubleClick() =
    
    static let commandProperty: DependencyProperty =
        DependencyProperty.Register("Command", typeof<ICommand>, typeof<MouseDoubleClick>)

    static let commandParameterProperty: DependencyProperty =
        DependencyProperty.Register("CommandParameter", typeof<Object>, typeof<MouseDoubleClick>)

    /// The ICommand
    static member CommandProperty with get() = commandProperty
    
    /// The paramter passed to the ICommand.  If this is set, the EventArgs are ignored
    static member CommandParameterProperty with get() = commandParameterProperty

    static member SetCommand(target: DependencyObject, value: ICommand) =
        target.SetValue(commandProperty, value);

    static member SetCommandParameter(target: DependencyObject, value: Object) =
        target.SetValue(commandParameterProperty, value)

    static member GetCommandParameter(target: DependencyObject) =
        target.GetValue(commandParameterProperty)

    static member CommandChanged(target: DependencyObject, e: DependencyPropertyChangedEventArgs) =
        let control: Control = target :?> Control

        if control <> null then
            if (e.NewValue <> null && e.OldValue = null) then
                () //control.MouseDoubleClick.Add(OnMouseDoubleClick)
            else if (e.NewValue = null && e.OldValue <> null) then
                ()

    member this.OnMouseDoubleClick(e: MouseEventArgs) =
        let control: Control = null //sender :?> Control
        let command: ICommand = control.GetValue(commandProperty) :?> ICommand
        let commandParameter: Object = control.GetValue(commandParameterProperty)
        command.Execute(commandParameter)