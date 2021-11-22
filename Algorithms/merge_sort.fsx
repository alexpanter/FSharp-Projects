let rec merge = function
    | ([],r) -> r
    | (l,[]) -> l
    | (l::ls,r::rs) when l < r -> l :: merge (ls, r::rs)
    | (l::ls,r::rs)            -> r :: merge (l::ls, rs)

let rec split (l,r) = function
    | [] -> (l,r)
    | x::[]      -> (x::l, r)
    | x::xs::xss -> split (x::l, xs::r) xss

let rec msort lst =
    if List.length lst < 2 then
        lst
    else
        let (l,r) = split ([],[]) lst
        merge (msort l, msort r)


msort [1;4;7;2;3;9;13;8] |> printfn "sorted: %A"
msort ['j';'P';'~';'a';'b';'O';'B';'A';'5';'1';'0'] |> printfn "sorted: %A"
msort [5.7;13.2;-5.1;0.1;0.002;4.2;-3.9;2.0] |> printfn "sorted: %A"

(*
This code works as follows:
merge:
    By recursion, two lists are merged together into one.
    Each recursive step, through guarded pattern match,
    we pick the smallest element by comparing the front
    elements of the two lists; we reserve that element
    and remove it for the next recursive call.
split:
    By recursion, splits a list into a tuple of two lists,
    both of equal length (+/- 1).
msort:
    Split the input list into two lists, and merge these lists
    by subdividing them. This subdivision is done through recursion.
*)
