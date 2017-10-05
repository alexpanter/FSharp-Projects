
#r "window.dll"
open System.Windows.Forms
open System.Drawing


// Main Window Properties
let mainSize = Size(500,500)

let mainWindow =
    new Form(ClientSize = mainSize,
             Text = "Shadow Draw",
             BackColor = Color.White
             )

let mainClientSize = Size(mainWindow.ClientSize.Width,mainWindow.ClientSize.Height)
mainWindow.MinimumSize <- mainClientSize 
mainWindow.MaximumSize <- mainClientSize

let instructionBox = SDWindow.createInstructions()
instructionBox.Enabled <- false



// Math Functions
let getSin(angle: float) = System.Math.Sin(angle)
let getCos(angle: float) = System.Math.Cos(angle)





// square properties
let mutable radius = 50.0
let mutable center = Point(mainSize.Width / 2, mainSize.Height / 2)
let mutable corner1  = Point(0,0)
let mutable corner2 = Point(0,0)
let mutable corner3  = Point(0,0)
let mutable corner4 = Point(0,0)
let mutable corners = [|corner1;corner2;corner3;corner4|]
let colors = [|Color.Aqua;Color.Black;Color.Blue;Color.ForestGreen;Color.DarkGray;Color.Orange|]
let brush = new SolidBrush(colors.[0])


// rotation configuration
let mutable currentAngle = System.Math.PI / 4.0
let rotationSpeed = 2.0 * System.Math.PI / (2.0 ** 5.0)


// manipulating the square
let clockWise() =
    if currentAngle < rotationSpeed && currentAngle > 0.0 then
        currentAngle <- 0.0
    elif currentAngle = 0.0 then
        currentAngle <- 2.0 * System.Math.PI - rotationSpeed
    else currentAngle <- currentAngle - rotationSpeed

let counterClockWise() =
    if currentAngle + rotationSpeed >= 2.0 * System.Math.PI then
        currentAngle <- 0.0
    elif currentAngle = 0.0 then
        currentAngle <- rotationSpeed
    else currentAngle <- currentAngle + rotationSpeed

let moveUp() = center.Y <- center.Y - 10
let moveDown() = center.Y <- center.Y + 10
let moveLeft() = center.X <- center.X - 10
let moveRight() = center.X <- center.X + 10

let zoomIn() = if radius < 200.0 then radius <- radius + 5.0
let zoomOut() = if radius > 5.0 then radius <- radius - 5.0

let changeColor() =
    let i = Array.findIndex (fun elem -> elem = brush.Color) colors
    if i = colors.Length - 1 then
        brush.Color <- colors.[0]
    else brush.Color <- colors.[i+1]


// other program actions
let toolTip() =
    if instructionBox.Visible then
        instructionBox.Hide()
    else instructionBox.Show()




// refreshing all square points
let refreshSquareAngles() =
    let calcAngle(angle: float) =
        let temp = angle + System.Math.PI / 2.0
        if angle + temp >= 2.0 * System.Math.PI then
            temp - 2.0 * System.Math.PI
        else temp
    let calcPoint(angle: float) =
        let x = getCos(angle) * radius |> System.Math.Round |> int
        let y = getSin(angle) * radius |> System.Math.Round |> int
        Point(center.X + x, center.Y + y)

    let corner1 = calcPoint(currentAngle)

    let angle2 = calcAngle(currentAngle)
    corner2 <- calcPoint(angle2)
    
    let angle3 = calcAngle(angle2)
    corner3 <- calcPoint(angle3)
    
    let angle4 = calcAngle(angle3)
    corner4 <- calcPoint(angle4)
    
    corners <- [|corner1;corner2;corner3;corner4|]




// adding the square to the manin window
let paintSquare() =
    mainWindow.Paint.Add(fun e -> e.Graphics.FillPolygon(brush,corners))
    mainWindow.Refresh()




// key controls
let mainHandler = new KeyEventHandler(fun s e ->
    match e.KeyValue with
    | 76 | 131148 | 65612 ->
        clockWise()         // key 'l' -> rotating clockwise
        refreshSquareAngles()
        paintSquare()
    | 82 | 131154 | 65618 ->
        counterClockWise()  // key 'r' -> rotating counter-clockwise
        refreshSquareAngles()
        paintSquare()
    | 38 ->
        moveUp()            // up -> moves the square up
        refreshSquareAngles()
        paintSquare()
    | 40 ->
        moveDown()          // down -> moves the square down
        refreshSquareAngles()
        paintSquare()
    | 37 ->
        moveLeft()          // left -> moves the square left
        refreshSquareAngles()
        paintSquare()
    | 39 ->
        moveRight()         // right -> moves the square right
        refreshSquareAngles()
        paintSquare()
    | 27 ->
        mainWindow.Close()  // escape -> quits the program
    | 107 | 187 | 131259 | 131179 ->
        zoomIn()            // plus keys -> zooms in
        refreshSquareAngles()
        paintSquare()
    | 109 | 189 | 131261 | 131181 ->
        zoomOut()           // minus keys -> zooms out
        refreshSquareAngles()
        paintSquare()
    | 67 ->
        changeColor()
        paintSquare()
    | 73 | 65609 ->
        toolTip()           // key 'i' -> hide/show tooltip
    | _ ->
        ()
    //printfn "%i" e.KeyValue
    )





// running program
mainWindow.Controls.Add(instructionBox)
mainWindow.KeyDown.AddHandler(mainHandler)
mainWindow.Show()
refreshSquareAngles()
paintSquare()
Application.Run(mainWindow)



