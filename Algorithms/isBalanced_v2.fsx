// ===== IS BALANCED v. 2.0 ===== //
// A students attempt to make an efficient algorithm
// approximating O(n) running time for deciding
// whether a list tree is balanced or not.

// .NET namespace for using a Queue<'T>
open System.Collections.Generic

// Declaring tree type
type listTree<'a> =
    | Node of 'a * listTree<'a> list

// Class for containg a queue and a count
type TreeQueue<'a>() = class
    let mutable count = 0
    let q = Queue<listTree<'a>>()

    member this.Count = count
    member this.Q = q

    member this.AddToCount(breadth) = count <- count + breadth
end


// MAIN function
let isBalanced(lt: listTree<'a> byref) =
    printfn "\nChecking new list tree.."

    let countChildren = function
        | Node(n,[]) -> 0
        | Node(n,xs) -> List.length xs

    //HYPOTHESIS: We believe that the tree is not be balanced, and
    //we will try to prove this. Also the tree is probably not trivial.
    let mutable is_balanced = true
    let mutable not_trivial = true

    //Creating an array of queues corresponding to the number
    //of subtrees from the root.
    let array_of_queues = Array.create (countChildren(lt)) (ref <| new TreeQueue<'a>())


    //let array_of_queues = [| for x = 1 to countChildren(!lt) do
    //                            yield ref <| new TreeQueue<'a>()  |]

    //initializing the queues to contain references to the
    //subtrees of the root.
    match lt with
    | Node(n,[]) -> not_trivial <- false
    | Node(n,xs) -> let mutable i = 0
                    for x in xs do
                        (!array_of_queues.[i]).Q.Enqueue(x)
                        i <- i + 1

    while is_balanced && not_trivial do   //one iteration for each level of the tree!

        let mutable MAX = System.Int32.MinValue     // -2.147.483.648
        let mutable MIN = System.Int32.MaxValue     // 2.147.483.647
        let mutable SPAN_MAX = System.Int32.MinValue
        let mutable SPAN_MIN = System.Int32.MaxValue

        for tq in array_of_queues do
            if is_balanced then
                let breadth = (!tq).Q.Count
                (!tq).AddToCount(breadth)
                for x = 1 to breadth do
                    let subtree = (!tq).Q.Dequeue()
                    match subtree with
                        | Node(n,[]) -> ()
                        | Node(n,xs) -> (for x in xs do (!tq).Q.Enqueue(x))
                    let childCount = countChildren(subtree)
                    if childCount > MAX then MAX <- childCount
                    if childCount < MIN then MIN <- childCount
                if (!tq).Count > SPAN_MAX then SPAN_MAX <- (!tq).Count
                if (!tq).Count < SPAN_MIN then SPAN_MIN <- (!tq).Count
                if SPAN_MAX - SPAN_MIN > 1 then is_balanced <- false
                if MAX - MIN > 1 then is_balanced <- false

        //are we finished and did our hypothesis fail (ie. is the tree balanced)
        if Array.forall (fun (tq: TreeQueue<'a> ref) -> (!tq).Q.Count = 0) array_of_queues then
            not_trivial <- false

    // return value
    printfn "\nWas the tree balanced: %b\n" is_balanced


// Testing isBalanced against different trees -
// none of these trees should be balanced.
let b0 = Node(1,[])
let b1 = Node(1,[Node(2,[])])
let b2 = Node(1,[Node(2,[]); Node(3,[])])
let b3 = Node(1,[Node(2,[]); Node(3,[]); Node(4,[])])
let b4 = Node(1,[Node(2,[]); Node(3,[]); Node(4,[]); Node(5,[])])
let ba lt = Node(1,lt)

let mutable tree7 = Node(1,[b1; b0; b2])
isBalanced(&tree7)

let mutable tree16 = Node(1,[Node(2,[b2;b0;b0]); b4; b3])
isBalanced(&tree16)

let mutable tree19 = Node(1,[Node(1,[Node(1,[Node(1,[Node(1,[]); Node(1,[])]); Node(1,[Node(1,[])])])]);
                             Node(1,[Node(1,[Node(1,[Node(1,[])])])]);
                             Node(1,[Node(1,[Node(1,[Node(1,[])]); Node(1,[Node(1,[]); Node(1,[])])])])])
isBalanced(&tree19)


//
//
//
