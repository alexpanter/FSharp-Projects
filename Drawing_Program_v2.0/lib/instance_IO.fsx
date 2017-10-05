module instance.io


#r "globals.dll"
#load "instance_Settings.fsx"
#load "instance_FileInstance.fsx"

open System
open System.Runtime.Serialization.Formatters.Binary



// Converts input stream from binary data to an object of type
// instance.file.FileInstanceSettings.
let private convertFromBinary(stream: IO.FileStream) =
    let formatter = BinaryFormatter()
    (formatter.Deserialize(stream) |> unbox)




// Converts data from an input file stream and writes the
// data to the file stream's target file.
let private convertToBinary(stream: IO.FileStream, content: obj) =
    let formatter = BinaryFormatter()
    formatter.Serialize(stream, content)




// Reads data from the application settings file defined in
// 'settings_globals.fs'.
let readSettingsFile() =
    if IO.File.Exists (globals.settingsFile) then
        use stream = new IO.FileStream(globals.settingsFile,IO.FileMode.Open, IO.FileAccess.Read)
        convertFromBinary(stream): instance.settings.ProgramSettings
    else
        new instance.settings.ProgramSettings()




// Writes data from the current ProgramSettings object to the
// application settings file (as defined in 'settings_globals.fs')
let writeSettingsFile(settings: instance.settings.ProgramSettings) =
    use stream = new IO.FileStream(globals.settingsFile,IO.FileMode.Create, IO.FileAccess.Write)
    convertToBinary(stream,settings |> box)
    stream.Close()
    printfn "the file was saved as %s" globals.settingsFile




// Saves a FileInstance object as binary data as the chosen file name
let writeFileInstance(fi: instance.file.FileInstance) =
    if not <| IO.Directory.Exists(globals.savesFolder) then
        IO.Directory.CreateDirectory(globals.savesFolder).Create()
    let target = globals.savesFolder + fi.InstanceName + globals.fileExtension
    use stream = new IO.FileStream(target, IO.FileMode.Create, IO.FileAccess.Write)
    convertToBinary(stream, fi |> box)
    stream.Close()
    printfn "the file was saved as %s" target



// Loads binary data from a save file and returns a FileInstance object.
// The input 'filename' is created from an System.Windows.Forms.OpenFileDialog,
// and therefore contains the full path.
let readFileInstance(path: string) =
    if not <| IO.File.Exists(path) then
        None
    else
        use stream = new IO.FileStream(path, IO.FileMode.Open, IO.FileAccess.Read)
        let data = convertFromBinary(stream): instance.file.FileInstance
        stream.Close()
        Some(data)




