namespace Anna.Source.MVVM.Behavior

open System;
open System.Windows
open System.Windows.Data

module ConverterBase =

    let nullFunction = fun value target param culture -> value

    [<AbstractClass>]
    type ConverterBase(convertFunction, convertBackFunction) =    
        /// constructor take nullFunction as inputs
        new() = ConverterBase(nullFunction, nullFunction)

        // implement the IValueConverter
        interface IValueConverter with
            /// convert a value to new vlaue
            override this.Convert(value, targetType, parameter, culture) =
                this.Convert value targetType parameter culture

            /// convert a value back
            override this.ConvertBack(value, targetType, parameter, culture) =
                this.ConvertBack value targetType parameter culture
    
        /// abstract member that allows the convert function to be overridden
        abstract member Convert : (Object -> Type -> Object -> Globalization.CultureInfo -> Object)

        /// default Convert implementation
        default this.Convert = convertFunction

        /// abstract member that allows the convert back function to be overridden
        abstract member ConvertBack : (Object -> Type -> Object -> Globalization.CultureInfo -> Object)

        /// default ConvertBack implementation 
        default this.ConvertBack = convertBackFunction