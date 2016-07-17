module ApplicationForm

#load "hexagonBoard.fs"

open System
open System.Windows.Forms


[<Sealed>]
[<AttributeUsage(AttributeTargets.Class,AllowMultiple = false)>]
type MainForm() as this = class
    inherit Form()

    // geometric properties
    let mutable width = 11 * 50
    let a = HexagonBoard.sideA(width)
    let b = HexagonBoard.sideB(a)
    let height = 7.0 * b |> System.Math.Round |> int

    // setting up
    do
        this.Width <- width + this.PreferredSize.Width
        this.Height <- height + this.PreferredSize.Height


    // member attributes and functions
    member this.DrawingContainer = ()
    
end

