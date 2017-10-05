open System

type Point = float * float
type Vector = float * float

let dot(v1: Vector, v2: Vector) =
    (fst v1) * (fst v2) + (snd v1) * (snd v2)

let times(v: Vector, s: float) =
    (s * fst v, s * snd v)

let norm(v: Vector) =
    (fst v)**2.0 + (snd v)**2.0 |> sqrt

let dist(p1: Point, p2: Point) =
    norm((fst p1) - (fst p2), (snd p1) - (snd p2))


let main(c, v1, v2, p) =
    if abs(dot(v1, v2)) > 0.00001 then
        failwith "Vectors are not orthogonal\n"

    let v1norm = norm(v1)
    let v2norm = norm(v2)
    let dst = dist(c, p)

    // trivial case - the ellipse is a circle!
    if abs(v1norm - v2norm) < 0.00001 then
        dst <= v1norm

    else
        let (mi,ma,miv,mav) =
            if v1norm > v2norm then
                (v2norm,v1norm,v2,v1)
            else (v1norm,v2norm,v1,v2)
        let fdist = ma**2.0 - mi**2.0 |> sqrt
        let f1 = times(v2,1.0 - (ma-fdist)/ma)
        let f2 = times(f1, -1.0)
        let maxnorm = 2.0 * dist(miv, f1)
        dist(f1, p) + dist(f2, p) <= maxnorm



// TESTING
let c: Point = (0.0, 0.0)
let v1: Vector = (2.0, 0.0)
let v2: Vector = (0.0, 5.0)

let p1: Point = (0.0, 0.0)   // true
let p2: Point = (10.0, 10.0) // false
let p3: Point = (2.0, 5.0)   // false
let p4: Point = (2.0, 0.0)   // true
let p5: Point = (0.0, 5.0)   // true
let p6: Point = (2.1, 0.0)   // false
let p7: Point = (0.0, 5.1)   // false

let test(p) =
    main(c, v1, v2, p)

printfn "Performing ellipse tests:"
test p1 |> printfn "%b"
test p2 |> not |> printfn "%b"
test p3 |> not |> printfn "%b"
test p4 |> printfn "%b"
test p5 |> printfn "%b"
test p6 |> not |> printfn "%b"
test p7 |> not |> printfn "%b"

