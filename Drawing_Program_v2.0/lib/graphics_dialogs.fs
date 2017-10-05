module graphics.dialogs


// The file 'globals.dll' is included in compile time.


open System.Windows.Forms
open System.Drawing


// This dialog window should be created when the user tries to
// exit the application.
let createExitDialog(f: Form) =
    let text = "Do you want to save your file before exiting Shadow Draw?"
    let caption = "Save changes"
    let buttons = MessageBoxButtons.YesNoCancel
    let icon = MessageBoxIcon.Exclamation
    MessageBox.Show(f, text, caption, buttons, icon)




// This dialog window should be created when the user tries to
// close the current working file.
let createCloseFileDialog(f: Form) =
    let text = "Do you want to save your changes?"
    let caption = "Save changes"
    let buttons = MessageBoxButtons.YesNoCancel
    let icon = MessageBoxIcon.Warning
    MessageBox.Show(f, text, caption, buttons, icon)




// This function creates a windows dialog window for saving
// the FileInstance as a .shd file.
let createSaveFileDialog() =
    let dialog = new SaveFileDialog()
    dialog.InitialDirectory <- globals.savesFolder
    dialog.Filter <- "shd files (*.shd)|*.shd|All files (*.*)|*.*"
    dialog.FilterIndex <- 1
    dialog.RestoreDirectory <- true
    dialog.AddExtension <- true
    dialog.OverwritePrompt <- true
    dialog




// This function created an open dialog box that is used to
// open a saved project as a .shd file.
let createOpenFileDialog() =
    let dialog = new OpenFileDialog()
    dialog.InitialDirectory <- globals.savesFolder
    dialog.Filter <- "shd files (*.shd)|*.shd|All files (*.*)|*.*"
    dialog.FilterIndex <- 1
    dialog.RestoreDirectory <- true
    dialog.Multiselect <- false
    dialog.CheckFileExists <- true
    dialog.CheckPathExists <- true
    dialog




// This function creates a help window that is shown when
// clicking in the main menu: Help -> About.
let createHelpAboutDialog() =
    let form = new Form()
    form.Size <- Size(400,400)
    form.MaximumSize <- form.Size
    form.MinimumSize <- form.Size
    form.MinimizeBox <- false
    form.MaximizeBox <- false

    let textbox = new TextBox()
    textbox.Parent <- form
    textbox.Location <- Point(0,0)
    textbox.Size <- Size(form.Width, form.Height - 70)
    textbox.Multiline <- true
    textbox.ScrollBars <- ScrollBars.Horizontal
    textbox.WordWrap <- true
    textbox.ReadOnly <- true
    textbox.Lines <- [|"hej med dig"
                       "dette er Shadow Draw v. 2.0 Help."
                       ""
                       "Do you like it?"|]

    let button = new Button()
    button.Parent <- form
    button.Location <- Point(140, form.Height - 65)
    button.BackColor <- Color.LightGray
    button.DialogResult <- DialogResult.OK
    button.Text <- "Okay."
    button.Size <- Size(120,43)

    button.Click.AddHandler(System.EventHandler
        (fun s e -> button.Parent.Hide()
                    button.Parent.Dispose()
                    button.Hide()
                    button.Dispose()
        ))

    // finalizing, returning the composed form
    textbox.DeselectAll()
    form.Controls.AddRange [|button;textbox|]
    form
