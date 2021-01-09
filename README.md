This project allows for simple reading and writing of configuration files on a json file.

Example usage:

  Create file called AppConfig.json, in it place the following:
  ```json
    {
      "DbUsername": "SomeDefaultUsername",
      "DbPassword": "SomeDefaultPassword",
      "ConnectionString": {
        "Database": "Host=SomeAddress.com;Port=5432"
      }
    }
  ```
    
  Make sure the file is copied to the output directory
  ```fsproj
  <Content Include="AppConfig.json">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </Content>
  ```
  
  Afterwards set up the following:
  ```F#
  module SomeApp.AppSettings
  
  open Config.Net
  
  //This sets up the interface declaring the types of the json file and their read/write access
  //For more information and additional options see https://github.com/aloneguid/config
  type SettingsInterface =
    [<Option(DefaultValue = "")>]
    abstract DbUsername: string with get, set

    [<Option(DefaultValue = "")>]
    abstract DbPassword: string with get, set

    [<Option(Alias = "ConnectionString.Database")>]
    abstract ConnectionStringDatabase: string with get, set
    
  //This creates the file with the name SomeAppSettingsFileName in the folder "AppConfig" "C:\Users\SomeUser\Something\ApplicationFolder", and gives access to those settings.
  let settings () = ApplicationSettingsFSharp.Configuration.Settings<SettingsInterface> "AppConfig" "C:\Users\SomeUser\Something\ApplicationFolder" "SomeAppSettingsFileName"
  ```
  
  Finally use it like so:
  
  ```F#
  
  open SomeApp.AppSettings
  
  //Reading settings
  let settings = settings()
  
  let dbUsername = settings.DbUsername
  let dbPassword = settings.DbPassword
  let connectionString = settings.ConnectionStringDatabase
  
  //Writing settings
  let settings = settings()
  
  settings.DbUsername <- "Some New Username"
  settings.DbPassword <- "Some New Password"
  ```
