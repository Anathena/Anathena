namespace Anna.Source

open System

module ViewUris =
    let viewBase = "/App;component/GUI/"

    let mainView = new System.Uri(viewBase + "Main.xaml", UriKind.Relative)
    let processSelectorView = new System.Uri(viewBase + "ProcessSelector.xaml", UriKind.Relative)