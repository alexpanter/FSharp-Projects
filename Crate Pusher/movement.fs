module movement

// this function checks if the player can move in the chosen
// direction (represented by a coordinate(i,j) and a unit vector).
// The function also takes in as input an array2D and the size
// of the array (which is always a square).
let checkMove(coord: int * int, vec: int * int, a: int [,], size: int) =
    let i,j = coord
    let x,y = vec

    // checking input arguments
    if (x = y) || (abs(x + y) <> 1) then
        failwithf "checkMove did not receive a proper unit vector: %A" vec
    elif (i < 0 || j < 0) || (i >= size) || (j >= size) then
        failwithf "checkMove did not receive valid coordinates: %A" coord
    elif (x + j >= size - 1) || (y + i >= size - 1) then
        false
    elif (i = 0 && y = -1) || (j = 0 && x = -1) then
        false

    // checking collisions
    elif a.[i+y,j+x] = 1 then
        false //trying to move to an occupied space
    elif a.[i+y,j+x] = 2 then
        let mutable b = 1 //what's behind the crate
        try
            b <- a.[i+y+y,j+x+x]
        with
            | :? System.IndexOutOfRangeException -> b <- 1
        (b <> 1 && b <> 2) // 1 is wall, 2 is another crate
    else
        true




(*
// UNIT TESTING - basic collisions
printfn "ALL unit tests must return 'false'.\n"
let left = (-1,0)
let right = (1,0)
let up = (0,-1)
let down = (0,1)

let a1 = [[1;5];[1;0]] |> array2D
let a2 = [[1;1;1];[1;5;1];[1;1;1]] |> array2D

printfn "a1 left: %b" <| checkMove((0,1),left,a1,2)
printfn "a1 right: %b" <| checkMove((0,1),right,a1,2)
printfn "a1 up: %b" <| checkMove((0,1),up,a1,2)
printfn "a1 down: %b" <| checkMove((0,1),down,a1,2)

printfn "a2 left: %b" <| checkMove((1,1),left,a2,3)
printfn "a2 right: %b" <| checkMove((1,1),right,a2,3)
printfn "a2 up: %b" <| checkMove((1,1),up,a2,3)
printfn "a2 down: %b" <| checkMove((1,1),down,a2,3)



// UNIT TESTING - pushing crates
let a3 = [[1;1;1];[5;2;0];[1;1;1]] |> array2D
let a4 = [[1;1;1];[5;2;1];[1;1;1]] |> array2D
let a5 = [[1;1;1];[5;2;2];[1;1;1]] |> array2D
let a6 = [[1;1;1];[5;2;3];[1;1;1]] |> array2D

printfn "a3 right: %b" <| (checkMove((1,0),right,a3,3) |> not)
printfn "a4 right: %b" <| checkMove((1,0),right,a4,3)
printfn "a5 right: %b" <| checkMove((1,0),right,a5,3)
printfn "a6 right: %b" <| (checkMove((1,0),right,a6,3) |> not)
*)


