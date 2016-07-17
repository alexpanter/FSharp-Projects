module MouseClickCheck

type internal Location =
    | ABOVE
    | BELOW


// (slope,y_Intersect) (vector x-coord.,vector y-coord)
let internal checkCorner (a: float,b: float) (x: float,y: float) =
    if (a * x + b) < y then
        ABOVE
    else BELOW



let internal cornerUpLeft(a,b)(x,y) =
    match checkCorner(a,b)(x,y) with
        | ABOVE -> false
        | BELOW -> true
let internal cornerUpRight(a,b)(x,y) =
    match checkCorner(a,b)(x,y) with
        | ABOVE -> false
        | BELOW -> true
let internal cornerDownLeft(a,b)(x,y) =
    match checkCorner(a,b)(x,y) with
        | ABOVE -> true
        | BELOW -> false
let internal cornerDownRight(a,b)(x,y) =
    match checkCorner(a,b)(x,y) with
        | ABOVE -> true
        | BELOW -> false

// Using coordinate system vector math to determine whether
// the input vector (vecX,vecY) is pointing towards a point
// inside the hexagon, where origin is the upper-left corner
// of the hexagon's bounding box.
let checkAllCorners (a: float,b: float,c: float) (vecX: float,vecY: float) =
    let slope1 = 1.732050808 //equivalent to an angle of 60 degrees.
    let slope2 = -1.732050808
    let width = a + 2.0 * c
    let halfHeight = b / 2.0
    let b_up = 3.0 * halfHeight
    let b_middle = (-1.0) * halfHeight
    let b_down = -5.0 * halfHeight
    (vecY < 0.0) && (vecY > -b) && (vecX > 0.0) && (vecX < width)
    &&
    cornerUpLeft(slope1,b_middle)(vecX,vecY)
    &&
    cornerUpRight(slope2,b_up)(vecX,vecY)
    &&
    cornerDownLeft(slope2,b_middle)(vecX,vecY)
    &&
    cornerDownRight(slope1,b_down)(vecX,vecY)




(* Successful UNIT-TEST

// UNIT-TEST when a = 10:
let a = 10.0
let b = 17.32050808
let c = 5.0

printfn "%b" <| not (checkAllCorners(a,b,c)(-5.0,-5.0))
printfn "%b" (checkAllCorners(a,b,c)(5.0,-5.0))
printfn "%b" <| not (checkAllCorners(a,b,c)(-5.0,5.0))
printfn "%b" <| not (checkAllCorners(a,b,c)(5.0,5.0))
printfn "%b" <| not (checkAllCorners(a,b,c)(0.3,-0.3))
printfn "%b" <| not (checkAllCorners(a,b,c)(19.7,-0.3))
printfn "%b" <| not (checkAllCorners(a,b,c)(0.3,-17.0))
printfn "%b" <| not (checkAllCorners(a,b,c)(19.7,-17.0))
printfn "%b" <| not (checkAllCorners(a,b,c)(10.0,0.5))
printfn "%b" <| not (checkAllCorners(a,b,c)(10.0,17.6))

printfn "%b" (checkAllCorners(a,b,c)(2.6,-4.5))
printfn "%b" (checkAllCorners(a,b,c)(2.6,-12.8))
printfn "%b" (checkAllCorners(a,b,c)(17.4,-4.5))
printfn "%b" (checkAllCorners(a,b,c)(17.4,-12.8))

*)



(* Successful UNIT-TEST

printfn "checkCorner (a,b) (x,y)\n"
for (a,b,x,y) in [(1.0,5.0,1.0,1.0)] do
    printfn "(checkCorner (%f,%f) (%f,%f)) %A"
        a b x y (checkCorner (a,b) (x,y))
    printfn "(checkCorner (%f,%f) (%f,%f)) %A\n"
        a b y x (checkCorner (a,b) (y,x))

    printfn "(checkCorner (%f,%f) (%f,%f)) %A"
        a b x y (checkCorner (a,b) (-x,y))
    printfn "(checkCorner (%f,%f) (%f,%f)) %A\n"
        a b y x (checkCorner (a,b) (-y,x))

    printfn "(checkCorner (%f,%f) (%f,%f)) %A"
        a b x y (checkCorner (a,b) (-x,-y))
    printfn "(checkCorner (%f,%f) (%f,%f)) %A\n"
        a b y x (checkCorner (a,b) (-y,-x))

    printfn "(checkCorner (%f,%f) (%f,%f)) %A"
        a b x y (checkCorner (a,b) (x,-y))
    printfn "(checkCorner (%f,%f) (%f,%f)) %A\n"
        a b y x (checkCorner (a,b) (y,-x))

*)

