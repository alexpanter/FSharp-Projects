module visuals

open System.Windows.Forms
open System.Drawing


type DoubleBufferedForm() =
    inherit Form()
    do
        base.DoubleBuffered <- true

let createMainForm() =
    new DoubleBufferedForm( ClientSize = Size(540,540)
            , BackColor = Color.Black
            )


let defaultBrickWall() = Image.FromFile("assets/brickwall_01.png")
let defaultCrate() = Image.FromFile("assets/crate_01.png")
let defaultCrateTarget() = Image.FromFile("assets/crateTarget_01.png")

let characterUp() = Image.FromFile("assets/characterUp.png")
let characterDown() = Image.FromFile("assets/characterDown.png")
let characterLeft() = Image.FromFile("assets/characterLeft.png")
let characterRight() = Image.FromFile("assets/characterRight.png")

let characterTargetUp() = Image.FromFile("assets/crateTargetPlayerUp.png")
let characterTargetDown() = Image.FromFile("assets/crateTargetPlayerDown.png")
let characterTargetLeft() = Image.FromFile("assets/crateTargetPlayerLeft.png")
let characterTargetRight() = Image.FromFile("assets/crateTargetPlayerRight.png")

let endGamePic() = Image.FromFile("assets/endGame.png")




