module instance.main

#r "graphics.dll"
#r "globals.dll"

#load "instance_FileInstance.fsx"
#load "instance_Settings.fsx"
#load "instance_IO.fsx"

open instance.file

open System.Runtime.Serialization.Formatters.Binary
open System.Collections.Generic
open System.Windows.Forms
open System.Drawing
open System.IO




type MainInstance() = class

    let mainForm = graphics.graphics.createMainWindow()
    let mutable mainMenu = Unchecked.defaultof<MainMenu>

    let mainPanel = graphics.graphics.createMainPanel(mainForm)
    let sidePanel = graphics.graphics.createSidePanel(mainForm)

    let drawingPanel = graphics.graphics.createDrawingPanel(mainPanel)
    let messagePanel = graphics.graphics.createMessagePanel(mainPanel)

    let shapesPanel = graphics.graphics.createShapesPanel(sidePanel)
    let toolsPanel = graphics.graphics.createToolsPanel(sidePanel)

    let mutable fileInstance = Unchecked.defaultof<FileInstance>
    let mutable mainSize = graphics.graphics.mainSize

    let mutable isOpenChildForm = false

    member this.FileInstance
        with get() = fileInstance
        and private set(i: FileInstance) = fileInstance <- i

    member this.MainMenu
        with get() = mainMenu
        and set(m: MainMenu) = mainMenu <- m

    member this.MainForm = mainForm
    
    member this.MainSize
        with get() = mainSize
        and set(s: int * int) = mainSize <- s

    member this.MainPanel = mainPanel
    member this.SidePanel = sidePanel
    member this.DrawingPanel = drawingPanel
    member this.MessagePanel = messagePanel
    member this.ShapesPanel = shapesPanel
    member this.ToolsPanel = toolsPanel

    member this.IsOpenChildForm
        with get() = isOpenChildForm
        and set(b: bool) = isOpenChildForm <- b

    // Creating save file dialog
    member private this.SaveDialog() =
        let dialog = graphics.dialogs.createSaveFileDialog()
        if dialog.ShowDialog() = DialogResult.OK then
            let name = Path.GetFileNameWithoutExtension(dialog.FileName)
            this.FileInstance.InstanceName <- name
            instance.io.writeFileInstance(this.FileInstance)
        else
            printfn "the file was not saved"


    // Saving the associated FileInstance object
    member this.SaveFileInstance() =
        if (this.FileInstance <> Unchecked.defaultof<FileInstance>) then
            if (not this.FileInstance.IsFileSaved) then
                this.SaveDialog()
                this.FileInstance.IsFileSaved <- true
            elif (this.FileInstance.IsChangesUnsaved) then
                instance.io.writeFileInstance(this.FileInstance)
            else
                ()


    // Saving As the associated FileInstance object
    member this.SaveAsFileInstance() =
        if (this.FileInstance <> Unchecked.defaultof<FileInstance>) then
            this.SaveDialog()


    // Loading an Instance from a binary file stream to access a saved program state
    member this.LoadFileInstance() =
        let load() =
            let dialog = graphics.dialogs.createOpenFileDialog()
            if dialog.ShowDialog() = DialogResult.OK then
                match instance.io.readFileInstance(dialog.FileName) with
                | None -> printfn "\nLoadFileInstance(): file does not exist - \"%s\"\n\n" dialog.SafeFileName
                | Some(data) -> this.FileInstance <- data
                printfn "FileInstance was loaded from file \"%s\"" dialog.FileName
            else printfn "ShowDialog -> did not press OK"

        if (this.FileInstance <> Unchecked.defaultof<FileInstance>) then
            if (this.FileInstance.IsChangesUnsaved) || not (this.FileInstance.IsFileSaved) then
                match graphics.dialogs.createCloseFileDialog(this.MainForm) with
                | DialogResult.Yes    -> printfn "LoadFileInstance(): pressed Yes"
                                         this.SaveFileInstance()
                | DialogResult.No     -> printfn "LoadFileInstance(): pressed no"
                                         load()
                | DialogResult.Cancel -> ()
                                         printfn "LoadFileInstance(): pressed cancel"
                | _ as res -> printfn "LoadFileInstance(): DialogResult was unexpected - \"%A\"" res

        else
            load()
            
        

            


    // creating a new FileInstance to be drawn upon
    member this.CreateNewFileInstance() =
        this.FileInstance <- new FileInstance()
        printfn "created a new file instance"


    // Saving program settings for easy startup
    member this.SaveProgramSettings() =
        let settings = new instance.settings.ProgramSettings()
        settings.WindowSize <- this.MainForm.Size
        settings.WindowLocation <- this.MainForm.DesktopLocation
        instance.io.writeSettingsFile(settings)

    // Load program settings for easy startup
    member this.LoadProgramSettings() =
        let settings = instance.io.readSettingsFile()
        this.MainForm.Size <- settings.WindowSize
        if settings.WindowLocation <> Unchecked.defaultof<Point> then
            this.MainForm.SetDesktopLocation(settings.WindowLocation.X,
                                             settings.WindowLocation.Y)


    // closing current FileInstance
    member this.CloseCurrentFileInstance() =
        if (this.FileInstance <> Unchecked.defaultof<FileInstance>) then
            match graphics.dialogs.createCloseFileDialog(this.MainForm) with
            | DialogResult.Yes    -> printfn "CloseFile() -> pressed yes."
                                     this.SaveFileInstance()
                                     this.FileInstance <- Unchecked.defaultof<FileInstance>
                                     // this.MainForm.Refresh() ??
            | DialogResult.No     -> printfn "CloseFile() -> pressed no."
                                     this.FileInstance <- Unchecked.defaultof<FileInstance>
                                     // this.MainForm.Refresh() ??
            | DialogResult.Cancel -> printfn "CloseFile() -> pressed cancel."
            | _                   -> printfn "CloseFile() -> dialogresult was unexpected"


    // exiting the entire program
    member this.ExitProgram() =
        match graphics.dialogs.createExitDialog(this.MainForm) with
        | DialogResult.Yes    -> printfn "ExitProgram() -> pressed yes"
                                 this.SaveFileInstance()
                                 this.MainForm.Close()
        | DialogResult.No     -> printfn "ExitProgram() -> pressed no"
                                 this.MainForm.Close()
        | DialogResult.Cancel -> printfn "ExitProgram() -> pressed cancel"
                                 ()
        | _                   -> printfn "ExitProgram() -> dialogresult was unexpected"


    // shows or hides the menu items under the 'Tools' menu,
    // depending on whether or not one or more shapes are selected.
    member this.ShowHideToolsMenuItems() =
        if (this.MainMenu <> null) && (this.FileInstance <> Unchecked.defaultof<FileInstance>) then
            let titles = ["&Move";"&Rotate";"&Scale"]
            if (this.FileInstance.SelectedShapes.Count = 0) then
                for item in this.MainMenu.MenuItems do
                    if item.Text = "&Tools" then
                        for subitem in item.MenuItems do
                            subitem.Enabled <- false
                        printfn "&Tools was disabled"
            else
                for item in this.MainMenu.MenuItems do
                    if item.Text = "&Tools" then
                        item.Enabled <- true
                    

    member this.PrintAllSettings() =
        printfn "\nPRINTING ALL SETTINGS:\n"
        printfn "globals.fileExtension: \"%s\"" globals.fileExtension
        printfn "globals.pathSeparator: \"%s\"" globals.pathSeparator
        printfn "globals.rootFolder: \"%s\"" globals.rootFolder
        printfn "globals.savesFolder: \"%s\"" globals.savesFolder
        printfn "globals.settingsFile: \"%s\"" globals.settingsFile
        printfn "globals.useSettingsFile: \"%b\"" globals.useSettingsFile
        printfn ""
        if this.FileInstance <> Unchecked.defaultof<FileInstance> then
            printfn "this.FileInstance.InstanceName: \"%s.shd\""
                this.FileInstance.InstanceName
            printfn "this.FileInstance.CreatedShapes.Count \"%i\""
                this.FileInstance.CreatedShapes.Count
            printfn "this.FileInstance.SelectedShapes.Count: \"%i\""
                this.FileInstance.SelectedShapes.Count
        else
            printfn "this.FileInstance is 'Unchecked.defaultof<FileInstance>' aka 'null'\n"


    // Initializes all necessary settings and variables,
    // then runs the application
    member this.RunApplication() =
        
        // building the program window attaching all panels properly
        this.MainPanel.Controls.AddRange [|this.DrawingPanel;this.MessagePanel|]
        this.SidePanel.Controls.AddRange [|this.ShapesPanel;this.ToolsPanel|]
        this.MainForm.Controls.AddRange [|this.MainPanel;this.SidePanel|]
        
        // adding the menu strip to the mainForm
        this.MainForm.Menu <- this.MainMenu

        // running the program
        this.MainForm.Show()
        Application.Run(mainForm)

end
