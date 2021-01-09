module ApplicationSettingsFSharp.Configuration

open ApplicationSettingsFSharp.Helper
open Config.Net

let Settings<'T when 'T: not struct> originConfigFileNameWithoutExtension destinationPath destinationConfigFileNameWithoutExtension =

    copySettingsFile originConfigFileNameWithoutExtension destinationConfigFileNameWithoutExtension false destinationPath

    let fullConfigPath =
        System.IO.Path.Combine [| destinationPath
                                  (destinationConfigFileNameWithoutExtension + ".json") |]

    let settings =
        ConfigurationBuilder<'T>()
        |> fun x ->
            x.UseJsonFile fullConfigPath
            |> fun x -> x.Build()

    settings
