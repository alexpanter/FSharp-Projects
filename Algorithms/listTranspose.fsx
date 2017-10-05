

let rec takeList = function
    | [], ys       -> []
    | x::xs, []    -> [x] :: takeList (xs, [])
    | x::xs, y::ys -> (x::y) :: takeList (xs,ys)

let rec transpose = function
    | [] -> []
    | x::xs -> takeList (x, (transpose xs))

printfn "%A\n" <| transpose (transpose [[1;2];[3;4]])
printfn "%A\n" <| transpose [[1;2;3];[4;5;6];[7;8;9]]
printfn "%A\n" <| transpose [[1;2;3;4];[5;6;7;8]]
