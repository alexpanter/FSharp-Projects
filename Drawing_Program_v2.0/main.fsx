// reading all custom files
// only included for Visual Studio autocompletion
// the 'Makefile' includes them in the compiling commands
#r "lib\drawing.dll"
#r "lib\graphics.dll"
#r "lib\instance.dll"

// Creating the program instance containg all necessary subtypes
let main() =

    let mainInstance = new instance.main.MainInstance()

    instance.controls.Control.AttachKeyHandler(mainInstance)
    instance.controls.Control.AttachMainFormHandlers(mainInstance)
    mainInstance.MainMenu <- instance.menu.Menu.CreateMenu(mainInstance)


    mainInstance.RunApplication()

// Running the application in a Single Treaded Apartment.
// This ensures that OpenFileDialog and SaveFileDialog
// forms are properly displayed.
[<System.STAThread>]
main()
