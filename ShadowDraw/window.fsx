module SDWindow

#load "textMessages.fsx"
open System.Windows.Forms
open System.Drawing



let createInstructions() =
    new TextBox(Location = Point(0,0),
                MaximumSize = Size(500,50),
                MinimumSize = Size(500,50),
                Font = new Font("Cambria", 10.0f, FontStyle.Bold),
                ForeColor = Color.FromArgb(100,100,100),
                Lines = SDTextMessages.instructions,
                Multiline = true,
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
                )



//let hej = new TextBox()
//hej.ma