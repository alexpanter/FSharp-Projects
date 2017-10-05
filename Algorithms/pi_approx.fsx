(* INEFFICIENT IMPLEMENTATION
// generate uniformly random (x,y) coordinates in the interval [0,1]
let ran = System.Random()
let length = 10000000
let a = Array.init length (fun x -> (ran.NextDouble(), ran.NextDouble()))

// generate distance for each coordinate to the origin
let calcDist(x,y) = sqrt(x**2.0 + y**2.0)

let dist = [| for elem in a do yield calcDist(elem) |]

// calculate number of distances within the unit circle bound
let mutable numIn = 0.0

for elem in dist do
    if elem <= 1.0 then numIn <- numIn + 1.0

// calculate the ration between those two and multiply with 4 to approximate PI
let ratio = numIn / (float length)
let piApprox = ratio * 4.0

printfn "%f" piApprox
printfn "%f" System.Math.PI
*)

let ran = System.Random()
let length = 10000000
let calcDist = fun(x,y) -> (x**2.0 + y**2.0 |> System.Math.Sqrt) <= 1.0
let nxt = fun() -> ran.NextDouble()
//let cnt = List.fold(fun acc elem ->
//                    if calcDist(nxt(),nxt()) then acc+1 else acc) 0 [1..length]
let mutable i = 1
let mutable cnt = 0
while i <= length do
    if calcDist(nxt(),nxt()) then cnt <- cnt + 1
    i <- i + 1

printfn "%A" ((float cnt) / (float length) * 4.0)
