module DrawingPanel

#load "hexagonBoard.fs"
#load "mouseClickCheck.fs"

open System.Collections.Generic
open System.Windows.Forms
open System.Drawing


type MainPanel(parent: Form) as this = class
    inherit Panel()

    // drawing and color properties
    let wool = List.init 5   (fun x -> Color.LawnGreen)
    let grain = List.init 5  (fun x -> Color.FromArgb(230,230,0))
    let ore = List.init 3    (fun x -> Color.DarkSlateGray)
    let forest = List.init 5 (fun x -> Color.ForestGreen)
    let desert = Color.FromArgb(245,200,0)

    let waterList = [1..4]@[5;9;10;15;16;22;23;28;29;33]@[34..37]
    let landsList = wool @ grain @ ore @ forest
    let lands = List<Color>(landsList)

    let brush = new SolidBrush(Color.Empty)

    let ran = System.Random()

    let getRandomColor() =
        Color.FromArgb(ran.Next(256),ran.Next(256),ran.Next(256))
    let getWaterColor() =
        Color.FromArgb(ran.Next(50,100),ran.Next(50,100),ran.Next(170,230))


    // geometric properties
    let mutable width = parent.ClientSize.Width
    let mutable a = HexagonBoard.sideA(width)
    let mutable b = HexagonBoard.sideB(a)
    let mutable c = HexagonBoard.sideC(a)

    let height() = 7.0 * b |> System.Math.Round |> int
    let calcA() = a <- HexagonBoard.sideA(width)
    let calcB() = b <- HexagonBoard.sideB(a)
    let calcC() = c <- HexagonBoard.sideC(a)


    // game board data
    let mutable points = HexagonBoard.generateBoard(width)
    let mutable hexagonClicked = -1

    let rec mapWithColorData = function
        | [],_ -> []
        | p :: ps,i when List.exists (fun x -> x = i) waterList ->
            (p,getWaterColor()) :: mapWithColorData (ps,i+1)
        | p :: ps,19 ->
            (p,desert) :: mapWithColorData (ps,20)
        | p :: ps,i ->
            let r = ran.Next(lands.Count)
            let col = lands.[r]
            lands.RemoveAt(r)
            (p,col) :: mapWithColorData (ps,i+1)

    let mutable gameBoard = mapWithColorData(points,1)

    // drawing handler for the game board
    let drawingHandler = fun (g: PaintEventArgs) ->
        let mutable i = 1
        for (p,col) in gameBoard do
            let cp = HexagonBoard.calcPoints(a,b,c,p)
            if i = hexagonClicked then
                brush.Color <- Color.Black
            else brush.Color <- col
            g.Graphics.FillPolygon(brush,cp)
            i <- i + 1

        lands.AddRange(landsList)
            (*
            IMPLEMENT to work with images instead of a SolidBrush!
            use original setters images :
            if active brick (implement mouse-click tracking!) then
                g.Grahics.FillImage(...)
                >> USE ImageAttributes to recolor (more gamma?) <<
            else
                >> USE same but with shading, e.g. <<
            //https://msdn.microsoft.com/en-us/library/system.drawing.imaging.imageattributes(v=vs.110).aspx
            *)
            

    // mouse click handler for when the player clicks on a hexagon
    let rec findClickedHexagon (mousePoint: Point) = function
        | [],_ -> -1
        | (p: PointF) :: ps,i ->
            let vecX,vecY = ((float mousePoint.X) - (float p.X),
                             (float mousePoint.Y) - (float p.Y) - b)
            if MouseClickCheck.checkAllCorners(a,b,c)(vecX,vecY) then
                i
            else findClickedHexagon mousePoint (ps,i+1)

    let mouseClickHandler = fun (m: MouseEventArgs) ->
        let mouseLocation = m.Location
        let hexIndex = findClickedHexagon mouseLocation (points,1)
        if hexIndex = hexagonClicked then
            hexagonClicked <- -1
        else hexagonClicked <- hexIndex
        this.Refresh()


    // setting up
    do
        this.DoubleBuffered <- true
        this.Parent <- parent
        this.Dock <- DockStyle.Fill

        lands.AddRange(landsList)
        
        this.BackColor <- Color.FromArgb(22,22,22)
        this.Paint.Add(drawingHandler)

        this.MouseDown.Add(mouseClickHandler)

        this.Refresh()


    // member attributes and functions
    member this.updateDimensions() =
        width <- parent.ClientSize.Width
        calcA()
        calcB()
        calcC()
        points <- HexagonBoard.generateBoard(width)
        gameBoard <- mapWithColorData(points,1)

    member this.Points = points
        
    
end





