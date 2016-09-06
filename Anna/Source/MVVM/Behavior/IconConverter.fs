namespace Anna.Source.MVVM.Behavior

open System
open System.Drawing
open System.Windows
open System.Windows.Data
open System.Windows.Interop
open System.Windows.Media.Imaging
open ConverterBase

/// Returns Visibility.Visible if the string is not null or empty
type IconConverter() =
    inherit ConverterBase()

    let convertFunc = fun (inputObject: Object) _ _ _ ->         
        match inputObject with
        | :? Icon as inputIcon ->
            try
                // Convert from System.Drawing.Icon to System.Windows.Media.Imaging.BitmapSource, as WPF requires
                let bitmap:Bitmap = inputIcon.ToBitmap();
                let hBitmap:IntPtr = bitmap.GetHbitmap();
                Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions())
            with
                | _ -> null
        | _ -> null
        :> Object
    override this.Convert = convertFunc 