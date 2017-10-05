module globals

let internal os = System.Environment.OSVersion



// This value is computed to either '/' or '\\' (escape character),
// depending on the computer's operating system.
// This ensures that path separation works properly and that the
// application will be portable to different platforms.
let pathSeparator: string =
    if os.Platform.ToString() = "Win32NT" then "\\"
    else "/"



// Main folder - this is the root folder for the application.
let rootFolder: string =
    __SOURCE_DIRECTORY__ + pathSeparator + ".." + pathSeparator




// Saves folder - this is where project files are stored
// when saved in the application.
let savesFolder: string =
    rootFolder + "saves" + pathSeparator




// declaring the file extension of the save files.
let fileExtension: string = ".shd"




// The name of the '.ini' file to be used on startup for 
// setting program variables.
let settingsFile: string =
    rootFolder + "assets" + pathSeparator + ".shadow-draw-settings"




// This is the setting for whether or not the application should
// use the default '.shadow-draw-settings' file on startup.
let useSettingsFile: bool = true



// The official name of the program
let programName = "Shadow Draw v. 2.0"




// FOR TESTING PURPOSES:
// Printing all definitions in this file for quick check before building
// run in terminal:
// >> fsharpi settings_globals.fs
// >> open globals;;
// >> printAllSettings();;
let internal printAllSettings() =
    printfn "pathSeparator: %s" pathSeparator
    printfn "rootFolder: %s" rootFolder
    printfn "savesFolder: %s" savesFolder
    printfn "fileExtension: %s" fileExtension
    printfn "settingsFile: %s" settingsFile
    printfn "useSettingsFile: %b" useSettingsFile