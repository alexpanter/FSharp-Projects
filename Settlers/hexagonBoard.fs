module HexagonBoard

open System.Drawing

let pi = System.Math.PI
let sin = fun x -> System.Math.Sin (x * pi / 180.0)

let f (x: float) = float32 x

let sideA (panelWidth: int) = (float panelWidth) / 11.0
let sideB (a: float) = ( a * sin(120.0) ) / sin(30.0)
let sideC (a: float) = ( a * sin(30.0) ) / sin(90.0)


type Speed =
    | Extracting
    | Retracting


let generateBoard(panelWidth: int) =
    let a = sideA (panelWidth)
    let b = sideB (a)
    let c = sideC (a)
    let mutable startX = 0.0
    let mutable startY = b * 1.5
    let numList = [4;5;6;7;6;5;4]

    let rec generate = function
        //during a column left to the middle of the board
        | (x,y),Extracting,l::ls,n when n < l ->
            PointF(f x,f y) :: generate ((x,y+b),Extracting,l::ls,n+1)
        //last hexagon coordinate of the middle column
        | (x,y),Extracting,l::ls,n when n = l && l = 7 ->
            startX <- startX + a + c
            startY <- startY + b / 2.0
            PointF(f x,f y) :: generate ((startX,startY),Retracting,ls,1)
        //last hexagon coordinate of a column left to the middle
        | (x,y),Extracting,l::ls,n when n = l ->
            startX <- startX + a + c
            startY <- startY - b / 2.0
            PointF(f x,f y) :: generate ((startX,startY),Extracting,ls,1)
        //during a column right to the middle of the board
        | (x,y),Retracting,l::ls,n when n < l ->
            PointF(f x,f y) :: generate ((x,y+b),Retracting,l::ls,n+1)
        //last hexagon coordinate of the last column right to the middle
        | (x,y),Retracting,l::[],n when n = l ->
            PointF(f x,f y) :: []
        //last hexagon coordinate of a column right to the middle
        | (x,y),Retracting,l::ls,n when n = l ->
            startX <- startX + a + c
            startY <- startY + b / 2.0
            PointF(f x,f y) :: generate ((startX, startY),Retracting,ls,1)
        //something went wrong, and I can see exactly what
        | _ as k -> failwithf "generate:\nInvalid pattern match \"%A\"\n" k

    generate((startX,startY),Extracting,numList,1)




let calcPoints =
    fun (a: float,b: float,c: float,p: PointF) ->
        [|
         PointF (p.X + f (c),
                 p.Y + f (0.0))
         PointF (p.X + f (c + a),
                 p.Y + f (0.0))
         PointF (p.X + f (2.0*c + a),
                 p.Y + f (b / 2.0))
         PointF (p.X + f (c + a),
                 p.Y + f (b))
         PointF (p.X + f (c),
                 p.Y + f (b))
         PointF (p.X + f (0.0),
                 p.Y + f (b / 2.0))
         |]



// testing
//List.iter (fun x -> printfn "%A" x) (generateBoard(100))
