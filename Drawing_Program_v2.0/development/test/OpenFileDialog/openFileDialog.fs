module test

open System.Windows.Forms
open System.Drawing


let opend() =
    let openFileDialog1 = new OpenFileDialog()

    openFileDialog1.InitialDirectory <- __SOURCE_DIRECTORY__
    openFileDialog1.Filter <- "shd files (*.shd)|*.shd|All files (*.*)|*.*"
    openFileDialog1.FilterIndex <- 2
    openFileDialog1.RestoreDirectory <- true

    if (openFileDialog1.ShowDialog() = DialogResult.OK) then
        printfn "yes"
    else printfn "no"


