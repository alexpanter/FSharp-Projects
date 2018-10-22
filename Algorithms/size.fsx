let test(arg: string) =
    try
        let input = uint64 arg
        let output = System.Text.StringBuilder()
        let x = 1024UL

        let mutable iter = input

        let tb = iter / (x * x * x * x)
        if tb > 0UL then
            output.Append(sprintf "%i tb,  " tb) |> ignore
            iter <- iter - (tb * x * x * x * x)

        let gb = iter / (x * x * x)
        if gb > 0UL then
            output.Append(sprintf "%i gb,  " gb) |> ignore
            iter <- iter - (gb * x * x * x)

        let mb = iter / (x * x)
        if mb > 0UL then
            output.Append(sprintf "%i mb,  " mb) |> ignore
            iter <- iter - (mb * x * x)

        let kb = iter / x
        if kb > 0UL then
            output.Append(sprintf "%i kb,  " kb) |> ignore
            iter <- iter - (kb * x)

        let bytes = iter
        output.Append(sprintf "%i bytes" bytes) |> ignore

        printfn "%-10i -> %s" input (output.ToString())

        // result for testing
        let test = bytes + kb*x + mb*x*x + gb*x*x*x + tb*x*x*x*x
        test = input

    with
        | _ as x ->
            let len = min arg.Length 7
            for i = 1 to len do
                printf "%c" arg.[i-1]
            for i = 1 to (7 - len) do
                printf " "
            printfn "%s -> -" (if arg.Length > 7 then "..." else "   ")
            false

[<EntryPoint>]
let main(args : string[]) =
    let mutable success = true
    for arg in args do
        success <- success && test arg
    printfn "\n%s" (if success then "SUCCESS!" else "FAILURE!")
    0
