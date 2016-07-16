#load "hexagonBoard.fs"

open System.Drawing
open System.Windows.Forms
open System.Collections.Generic

//color properties
let ran = System.Random()
let getRandomColor() =
    Color.FromArgb(ran.Next(256),ran.Next(256),ran.Next(256))
let getWaterColor() =
    Color.FromArgb(ran.Next(50,100),ran.Next(50,100),ran.Next(170,230))
let wool = List.init 5 (fun x -> Color.LawnGreen)
let grain = List.init 5 (fun x -> Color.FromArgb(230,230,0))
let ore = List.init 3 (fun x -> Color.DarkSlateGray)
let forest = List.init 5 (fun x -> Color.ForestGreen)
let desert = Color.FromArgb(245,200,0)
let brush = new SolidBrush(Color.Empty)

//hexagon measurements
let width = 11 * 50 //a will be 50px
let a = HexagonBoard.sideA(width)
let b = HexagonBoard.sideB(a)
let c = HexagonBoard.sideC(a)
let height = 7.0 * b |> System.Math.Round |> int

//main form and main panel
let f = new Form()
f.ClientSize <- Size(width,height)
f.MinimumSize <- f.ClientSize + f.PreferredSize
f.MaximumSize <- f.ClientSize + f.PreferredSize
f.Text <- "The Settlers"

type DoubleBufferedPanel() =
    inherit Panel()
    do base.DoubleBuffered <- true

let pan = new DoubleBufferedPanel()
pan.Parent <- f
pan.Width <- f.ClientSize.Width
pan.Height <- f.ClientSize.Height

//generating points
let mutable points = HexagonBoard.generateBoard(width)

//paint event handler
pan.Paint.Add(fun g ->
    let waterList = [1..4]@[5;9;10;15;16;22;23;28;29;33]@[34..37]
    let lands = List<Color>(wool @ grain @ ore @ forest)
    let mutable i = 1
    for p in points do
        if List.exists (fun x -> x = i) waterList then
            brush.Color <- getWaterColor()
        elif i = 19 then
            brush.Color <- desert
        else
            let r = ran.Next(lands.Count)
            brush.Color <- lands.[r]
            lands.RemoveAt(r)
        let cp = HexagonBoard.calcPoints(a,b,c,p)
        g.Graphics.FillPolygon(brush,cp)
        i <- i + 1)



//drawing the test window
pan.Refresh()
Application.Run(f)
