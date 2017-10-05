#load "openFileDialog.fs"

open System.Windows.Forms
open System.Drawing
//open System
// single thread apartment

let runApp() =
    let f = new Form(Size = new Size(500,500), Text = "hej med dig")
    f.KeyDown.AddHandler(new KeyEventHandler(fun s e ->
        if e.KeyValue = 79 then test.opend()))



    f.Show()
    Application.Run(f)

[<System.STAThread>]
runApp()
