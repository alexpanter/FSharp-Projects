module blueprint

// This is the blueprint file.
// It contains information about how a game map is represented
// in the game in terms of data types.



(*   THIS SHOULD BE COPIED AND MODIFIED FOR EACH INSTANCE:
    let map: int [] [] =
        [| [| 0; 0; 0; 0; 0; 0; 0; 0; 0; |]
           [| 0; 0; 0; 0; 0; 0; 0; 0; 0; |]
           [| 0; 0; 0; 0; 0; 0; 0; 0; 0; |]
           [| 0; 0; 0; 0; 0; 0; 0; 0; 0; |]
           [| 0; 0; 0; 0; 0; 0; 0; 0; 0; |]
           [| 0; 0; 0; 0; 0; 0; 0; 0; 0; |]
           [| 0; 0; 0; 0; 0; 0; 0; 0; 0; |]
           [| 0; 0; 0; 0; 0; 0; 0; 0; 0; |]
           [| 0; 0; 0; 0; 0; 0; 0; 0; 0; |]
        |]
 *)



type GameMap(_map: int [,]) = class

    let height = Array2D.length1 _map
    let width = Array2D.length2 _map
    do
        if (height) <> (width) then
            failwithf "map must be of size n x n but was %i x %i\n"
                height width

    member this.Size = width
    member this.Map = _map

end
