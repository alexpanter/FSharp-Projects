module visuals

open System.Windows.Forms
open System.Drawing
open System.IO


// Creating a custom Form with double buffering enabled.
type DoubleBufferedForm() =
    inherit Form()
    do
        base.DoubleBuffered <- true


// Function for instancing the double buffered main form
let createMainForm() =
    let f = new DoubleBufferedForm()
    f.ClientSize <- Size(540,540)
    f.MaximumSize <- f.ClientSize + f.PreferredSize
    f.MinimumSize <- f.ClientSize + f.PreferredSize
    f.BackColor <- Color.Black
    f.Text <- "Crate Pusher"
    f


// Combine a path using platform specific path separator.
// This adds for portability to Windows.
let assets = (fun x -> Path.Combine("assets",x))


// Declaring functions for importing all assets
let defaultBrickWall() = Image.FromFile(assets "brickwall_01.png")
let defaultCrate() = Image.FromFile(assets "crate_01.png")
let defaultCrateTarget() = Image.FromFile(assets "crateTarget_01.png")

let characterUp() = Image.FromFile(assets "characterUp.png")
let characterDown() = Image.FromFile(assets "characterDown.png")
let characterLeft() = Image.FromFile(assets "characterLeft.png")
let characterRight() = Image.FromFile(assets "characterRight.png")

let characterTargetUp() = Image.FromFile(assets "crateTargetPlayerUp.png")
let characterTargetDown() = Image.FromFile(assets "crateTargetPlayerDown.png")
let characterTargetLeft() = Image.FromFile(assets "crateTargetPlayerLeft.png")
let characterTargetRight() = Image.FromFile(assets "crateTargetPlayerRight.png")

let endGamePic() = Image.FromFile(assets "endGame.png")




