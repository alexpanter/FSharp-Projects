open System

#load "messages.fs"
#load "control.fsx"
open calender


let menu0 = "main"
let menu10 = "this week"
let menu11 = "next week"
let menu12(i: int) = String.Format("week {0}",i)
let menu2(dayOfWeek: string) = String.Format("{0}",dayOfWeek)

let userInput(s: string) =
    Console.ForegroundColor <- ConsoleColor.Blue
    Console.Write(s)
    Console.ForegroundColor <- ConsoleColor.Green
    Console.Write("$ ")
    Console.ResetColor()



let taskAddMode(day: classes.Day, weekText: string) =
    let startHour = ref 0
    let startMinute = ref 0
    let stopHour = ref 0
    let stopMinute = ref 0
    let b = ref false
    // start hour
    while not !b do
        printf "Write the start hour: "
        try
            startHour := Int32.Parse <| Console.ReadLine()
            if not (!startHour < 0 || !startHour > 23) then
                b := true
        with
            | _ -> printf ""
    b := false
    // start minute
    while not !b do
        printf "Write the start minute: "
        try
            startMinute := Int32.Parse <| Console.ReadLine()
            if not (!startMinute < 0 || !startMinute > 59) then
                b := true
        with
            | _ -> printf ""
    b := false
    // stop hour
    while not !b do
        printf "Write the stop hour: "
        try
            stopHour := Int32.Parse <| Console.ReadLine()
            if not (!stopHour < 0 || !stopHour > 23) then
                b := true
        with
            | _ -> printf ""
    b := false
    // stop minute
    while not !b do
        printf "Write the stop minute: "
        try
            stopMinute := Int32.Parse <| Console.ReadLine()
            if not (!stopMinute < 0 || !stopMinute > 59) then
                b := true
        with
            | _ -> printf ""
    b := false
    let s = ref ""
    while not !b do
        printfn "Write a description for the task:"
        s := Console.ReadLine()
        if !s <> "" then
            b := true

    let t = new classes.Task((!startHour,!startMinute),(!stopHour,!stopMinute),!s)
    if day.AddTask(t) then
        Console.WriteLine("The task has now been added.\n")
    else
        Console.WriteLine("The task could not be added.\n")




let taskRemoveMode(day: classes.Day, weekText: string) =
    let tl = day.getPrintFormat()
    if tl.Length < 1 then
        messages.printNoTasks(weekText + "/" + day.DayText)
    else
        messages.printTasks(weekText + "/" + day.DayText)
        for (i,t) in tl do
            let start = t.StartTime.ToString()
            let stop = t.StopTime.ToString()
            printfn "%i)  (%s - %s) %s" i start stop t.Description
        let b = ref false
        let id = ref 0
        while not !b do
            printf "Enter the number of the task you wish to remove: "
            try
                id := Int32.Parse(Console.ReadLine())
                b := true
            with
                | _ -> printf "\n"
        if day.RemoveTask(!id) then
            Console.WriteLine("The task has been removed.\n")
        else
            Console.WriteLine("The task could not be removed.\n")
        




let dayMode(day: classes.Day, weekText: string) =

    let rec runDay() =
        userInput(menu0 + "/" + weekText + "/" + day.DayText)
        match Console.ReadLine() with
            | "1" | "show" ->
                let tl = day.getPrintFormat()
                if tl.Length > 0 then
                    messages.printTasks(weekText + "/" + day.DayText)
                    for (i,t) in tl do
                        let start = t.StartTime.ToString()
                        let stop = t.StopTime.ToString()
                        printfn "%i)  (%s - %s) %s" i start stop t.Description
                else
                    messages.printNoTasks(weekText + "/" + day.DayText)
                runDay()
            //add a new task
            | "2" | "add" ->
                taskAddMode(day, weekText)
                runDay()
            //remove an existing task
            | "3" | "remove" ->
                taskRemoveMode(day, weekText)
                runDay()
            //print the menu
            | "menu" ->
                messages.showDayMenu()
                runDay()
            //clear the screen
            | "clear" ->
                Console.Clear()
                runDay()
            //exit day mode
            | "0" | "exit" ->
                ()
            //non-valid entry
            | _ ->
                runDay()

    runDay()




let weekMode(w: classes.Week, weekText: string) =

    let rec runWeek() =
        userInput(menu0 + "/" + weekText)
        match Console.ReadLine() with
            //monday
            | "1" | "monday" as k->
                let monday = w.getDay(k)
                dayMode(monday,weekText)
                runWeek()
            //tuesday
            | "2" | "tuesday" as k->
                let tuesday = w.getDay(k)
                dayMode(tuesday,weekText)
                runWeek()
            //wednesday
            | "3" | "wednesday" as k->
                let wednesday = w.getDay(k)
                dayMode(wednesday,weekText)
                runWeek()
            //thursday
            | "4" | "thursday" as k->
                let thursday = w.getDay(k)
                dayMode(thursday,weekText)
                runWeek()
            //friday
            | "5" | "friday" as k->
                let friday = w.getDay(k)
                dayMode(friday,weekText)
                runWeek()
            //saturday
            | "6" | "saturday" as k->
                let saturday = w.getDay(k)
                dayMode(saturday,weekText)
                runWeek()
            //sunday
            | "7" | "sunday" as k->
                let sunday = w.getDay(k)
                dayMode(sunday,weekText)
                runWeek()
            //show all days
            | "8" as k ->
                ()
                runWeek()
            | "9" | "today" ->
                let today = control.today()
                dayMode(w.getDay(today),weekText)
                runWeek()
            //print the menu
            | "menu" ->
                messages.showWeekMenu()
                runWeek()
            //clear the screen
            | "clear" ->
                Console.Clear()
                runWeek()
            //exit week mode
            | "0" | "exit" ->
                ()
            //non-valid entry
            | _ ->
                runWeek()

    runWeek()





let main() =

    //displays welcome message to the user
    messages.welcome()

    let rec runMain() =
        userInput(menu0)
        match Console.ReadLine() with
            //this week
            | "1" | "this week" ->
                let mutable w = Unchecked.defaultof<classes.Week>
                let n = control.thisWeek() |> string
                try
                    w <- Option.get(control.getWeek())
                with
                    | _ ->
                        w <- new classes.Week()
                        let path =
                            ("data",control.year() |> string,n)
                            |> IO.Path.Combine
                        w.Path <- path
                weekMode(w,"week " + n)
                classes.CalenderSezializer.Serialize(box w,w.Path)
                runMain()
            //next week
            | "2" | "next week" ->
                let mutable w = Unchecked.defaultof<classes.Week>
                let n = control.nextWeek() |> string
                try
                    w <- Option.get(control.getNextWeek())
                with
                    | _ ->
                        w <- new classes.Week()
                        let path =
                            ("data",control.year() |> string,n)
                            |> IO.Path.Combine
                        w.Path <- path
                weekMode(w,"week " + n)
                classes.CalenderSezializer.Serialize(box w,w.Path)
                runMain()
            //enter a week number
            | "3" ->
                try
                    userInput("please enter a week number:")
                    let n = Int32.Parse(Console.ReadLine())
                    if control.isWeekNumValid(n) then
                        let mutable w = Unchecked.defaultof<classes.Week>
                        match control.getWeekFromBinary(n) with
                            | Some(x) ->
                                w <- x
                            | None ->
                                w <- new classes.Week()
                        let path =
                            ("data",
                             control.year() |> string,
                             n |> string)
                            |> IO.Path.Combine
                        w.Path <- path
                        weekMode(w,"week " + (string n))
                        classes.CalenderSezializer.Serialize(box w,w.Path)
                with
                    | _ -> ()
                runMain()
            //print the menu
            | "menu" ->
                messages.showMainMenu()
                runMain()
            //clear the screen
            | "clear" ->
                Console.Clear()
                runMain()
            //exit calender application
            | "0" | "exit" ->
                ()
            //non-valid entry
            | _ ->
                runMain()

    runMain()




// Starting the program
System.Console.Clear()
main()



