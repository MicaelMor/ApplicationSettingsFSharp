module ApplicationSettingsFSharp.Helper

open System
open System.IO

let copySettingsFile originConfigFileNameWithoutExtension destinationConfigFileNameWithoutExtension shouldReplaceExistingConfig destinationPath =
    let assemblyName =
        Reflection.Assembly.GetExecutingAssembly().GetName().Name

    let originalFilePath =
        Reflection.Assembly.GetExecutingAssembly().Location.Replace((assemblyName + ".dll"), (originConfigFileNameWithoutExtension + ".json"))

    let destinationFilePath =
        Path.Combine [| destinationPath
                        destinationConfigFileNameWithoutExtension + ".json" |]

    (FileInfo(destinationFilePath)).Directory.Create()

    if not
       <| File.Exists destinationFilePath
       && not shouldReplaceExistingConfig then
        File.Copy(originalFilePath, destinationFilePath)
    else if shouldReplaceExistingConfig then
        File.Copy(originalFilePath, destinationFilePath, shouldReplaceExistingConfig)
