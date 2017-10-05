module shapes


open System.Drawing



// SHAPE
type Shape(k: int,c: Point,n: string) = class
    //  k = number of points in the Shape, e.g. 3 is Triangle.
    //  c = the center Point of the Shape (relating to manipulating
    //      the Shape, e.g. moving, scaling, rotating, etc.
    //  n = the name of the Shape. This is shown in the program window.

    let vectors = Array.create k (0.0,0.0)
    let mutable center: Point = c
    let mutable radius: float = 50.0
    let mutable angle: float = 0.0
    let brush = new SolidBrush(Color.Black)
    let mutable name = n
    
    static let mutable triangleID = 0
    static let mutable squareID = 0
    static let mutable rectangleID = 0
    static let mutable pentagonID = 0

    do
        match name with
        | "Triangle"  -> triangleID  <- triangleID  + 1
                         name <- name + (sprintf "%i" triangleID)
        | "Square"    -> squareID    <- squareID    + 1
                         name <- name + (sprintf "%i" squareID)
        | "Rectangle" -> rectangleID <- rectangleID + 1
                         name <- name + (sprintf "%i" rectangleID)
        | "Pentagon"  -> pentagonID  <- pentagonID  + 1
                         name <- name + (sprintf "%i" pentagonID)
        | _ -> failwithf "ERROR: Not defined type: %s" name
        printfn "shape has name: %s" name
    



    // Accessing all points as float * float tuples or as Point(int,int)
    member this.Vectors = vectors

    member this.Points =
        [| for p in vectors do
               yield Point(fst p |> System.Math.Round |> int,
                           snd p |> System.Math.Round |> int)
        |]

    member this.Item
        with get(i: int) = vectors.[i]
        and set(i: int) (v: float * float) = vectors.[i] <- v




    // The Object's drawing properties
    member this.Radius 
        with get() = radius
        and set(r: float) = radius <- r

    member this.Center
        with get() = center
        and set(p: Point) = center.X <- p.X; center.Y <- p.Y

    member this.Angle
        with get() = angle
        and set(a: float) = angle <- a



    // Mathematical drawing member functions
    member this.ScaleRadius(factor: float) = radius <- radius * factor

    member this.MoveCenter(x: int, y: int) =
        center.X <- center.X + x
        center.Y <- center.Y + y

    member private this.RotateAngle(angleDelta: float) =
        angle <- angle + angleDelta




    // More light-weight attributes
    member this.Color
        with get() = brush.Color
        and set(c: Color) = brush.Color <- c

    member this.Brush = brush

    member this.Name
        with get() = name
        and set(nm) = name <- nm
end






// TRIANGLE
[<Sealed>]
type Triangle(center: Point) = class
    inherit Shape(3,center,"Triangle")

end




// SQUARE
[<Sealed>]
type Square(center: Point) = class
    inherit Shape(4,center,"Square")

end




// RECTANGLE
[<Sealed>]
type Rectangle(center: Point) = class
    inherit Shape(4,center,"Rectangle")

end




// EVEN-SIDED PENTAGON
[<Sealed>]
type Pentagon(center: Point) = class
    inherit Shape(5,center,"Pentagon")

end



// N-SIDED POLYGON
[<Sealed>]
type Polygon(center: Point, sides: int) = class
    inherit Shape(sides,center,"Polygon")
end



type allShapes =
    | Tri of Triangle
    | Sq of Square
    | Rect of Rectangle
    | Pent of Pentagon
    | Pol of Polygon


    

let getShape (s: allShapes) =
    match s with
    | (allShapes.Tri s)  -> (s :> Shape)
    | (allShapes.Sq s)   -> (s :> Shape)
    | (allShapes.Rect s) -> (s :> Shape)
    | (allShapes.Pent s) -> (s :> Shape)
    | (allShapes.Pol s)  -> (s :> Shape)

    

