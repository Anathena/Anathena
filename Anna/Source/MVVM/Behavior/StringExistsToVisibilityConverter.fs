namespace Anna.Source.MVVM.Behavior

open System
open System.Windows
open System.Windows.Data
open System.Windows.Media
open ConverterBase

/// <summary>
/// Returns Visibility.Visible if the string is not null or empty
/// </summary>
type StringExistsToVisibilityConverter() =
    inherit ConverterBase()

    let convertFunc = fun (inputString: Object) _ _ _ ->         
        match String.IsNullOrEmpty(string inputString) with
        | false -> Visibility.Visible
        | _ -> Visibility.Collapsed
        :> Object
    override this.Convert = convertFunc 