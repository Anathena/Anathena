namespace Anna.Source.MVVM.Behavior

open System
open System.Windows
open System.Windows.Data
open System.Windows.Media
open ConverterBase

/// Returns Visibility.Visible if the string is not null or empty
type StringExistsToVisibilityConverter() =
    inherit ConverterBase()
    let convertFunc = fun (v: Object) _ _ _ ->         
        match String.IsNullOrEmpty(string v) with
        | false -> Visibility.Visible
        | _ -> Visibility.Collapsed
        :> Object
    override this.Convert = convertFunc 