open System

#r "libcal.dll"
open calender

let source = __SOURCE_DIRECTORY__
let menu0 = "main"


let userInput(s: string) =
    Console.ForegroundColor <- ConsoleColor.Blue
    Console.Write(s)
    Console.ForegroundColor <- ConsoleColor.Green
    Console.Write("$ ")
    Console.ResetColor()



let taskAddMode(day: classes.Day) =
    let mutable startHour = 0
    let mutable startMinute = 0
    let mutable stopHour = 0
    let mutable stopMinute = 0
    let b = ref false
    
    // start hour
    while not !b do
        printf "Write the start hour: "
        try
            startHour <- Int32.Parse <| Console.ReadLine()
            if not (startHour < 0 || startHour > 23) then
                b := true
        with
            | _ -> printf ""
    b := false
    
    // start minute
    while not !b do
        printf "Write the start minute: "
        try
            startMinute <- Int32.Parse <| Console.ReadLine()
            if not (startMinute < 0 || startMinute > 59) then
                b := true
        with
            | _ -> printf ""
    b := false
    
    // stop hour
    while not !b do
        printf "Write the stop hour: "
        try
            stopHour <- Int32.Parse <| Console.ReadLine()
            if not (stopHour < 0 || stopHour > 23) then
                b := true
        with
            | _ -> printf ""
    b := false
    
    // stop minute
    while not !b do
        printf "Write the stop minute: "
        try
            stopMinute <- Int32.Parse <| Console.ReadLine()
            if not (stopMinute < 0 || stopMinute > 59) then
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

    let t = new classes.Task((startHour,startMinute),(stopHour,stopMinute),!s)
    if day.AddTask(t) then
        Console.WriteLine("The task has now been added.\n")
    else
        Console.WriteLine("The task could not be added.\n")




let taskRemoveMode(day: classes.Day) =
    let tl = day.getPrintFormat()
    if tl.Length < 1 then
        messages.printNoTasks(day.DayText)
    else
        messages.printTasks(day.DayText)
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
        




let dayMode(day: classes.Day, prompt: string) =
    let newprompt = prompt + "/" + day.DayText

    let rec runDay() =
        userInput(newprompt)
        match Console.ReadLine() with
            | "1" | "show" ->
                let tl = day.getPrintFormat()
                if tl.Length > 0 then
                    messages.printTasks(day.DayText)
                    for (i,t) in tl do
                        let start = t.StartTime.ToString()
                        let stop = t.StopTime.ToString()
                        printfn "%i)  (%s - %s) %s" i start stop t.Description
                else
                    messages.printNoTasks(day.DayText)
                runDay()
            //add a new task
            | "2" | "add" ->
                taskAddMode(day)
                runDay()
            //remove an existing task
            | "3" | "remove" ->
                taskRemoveMode(day)
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




let weekMode(w: classes.Week, prompt: string) =
    let y = w.Path.Substring(w.Path.Length-4,4)
    let newprompt = prompt + "/" + y + "/Week " + w.File
    
    let rec runWeek() =
        userInput(newprompt)
        match Console.ReadLine() with
            //monday
            | "1" | "monday" ->
                let monday = w.Monday
                dayMode(monday,newprompt)
                runWeek()
            //tuesday
            | "2" | "tuesday" ->
                let tuesday = w.Tuesday
                dayMode(tuesday,newprompt)
                runWeek()
            //wednesday
            | "3" | "wednesday" ->
                let wednesday = w.Wednesday
                dayMode(wednesday,newprompt)
                runWeek()
            //thursday
            | "4" | "thursday" ->
                let thursday = w.Thursday
                dayMode(thursday,newprompt)
                runWeek()
            //friday
            | "5" | "friday" ->
                let friday = w.Friday
                dayMode(friday,newprompt)
                runWeek()
            //saturday
            | "6" | "saturday" ->
                let saturday = w.Saturday
                dayMode(saturday,newprompt)
                runWeek()
            //sunday
            | "7" | "sunday" ->
                let sunday = w.Sunday
                dayMode(sunday,newprompt)
                runWeek()
            //show all days
            | "8" ->
                ()
                runWeek()
            | "9" | "today" ->
                let today = control.today()
                dayMode(w.getDay(today),newprompt)
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






let pickADay() =
    let mutable dayNumber = 0
    let mutable monthNumber = 0
    let mutable yearNumber = control.thisYear()
    let b = ref false
    
    // year number
    while not !b do
        printf "Write the year number (default is %i): "
            (control.thisYear())
        let input = Console.ReadLine()
        if input <> "" then
            try
                yearNumber <- Int32.Parse(input)
                if (yearNumber >= 2016 && yearNumber < 2099) then
                    b := true
            with
                | _ -> printf ""
        else
            b := true
    b := false
    
    // month number
    while not !b do
        b := true
        printf "Write the month (number or string): "
        match Console.ReadLine() with
            | "january"   | "January"   | "1" -> monthNumber <- 1
            | "february"  | "February"  | "2" -> monthNumber <- 2
            | "march"     | "March"     | "3" -> monthNumber <- 3
            | "april"     | "April"     | "4" -> monthNumber <- 4
            | "may"       | "May"       | "5" -> monthNumber <- 5
            | "june"      | "June"      | "6" -> monthNumber <- 6
            | "july"      | "July"      | "7" -> monthNumber <- 7
            | "august"    | "August"    | "8" -> monthNumber <- 8
            | "september" | "September" | "9" -> monthNumber <- 9
            | "october"   | "October"   | "10" -> monthNumber <- 10
            | "november"  | "November"  | "11" -> monthNumber <- 11
            | "december"  | "December"  | "12" -> monthNumber <- 12
            | _ -> b := false; printf ""
    b := false
    
    // day number
    let maxDay = DateTime.DaysInMonth(yearNumber,monthNumber)
    while not !b do
        printf "Write the day number (1-%i): " maxDay
        try
            dayNumber <- Int32.Parse <| Console.ReadLine()
            if (dayNumber > 0 && dayNumber <= maxDay) then
                b := true
        with
            | _ -> printf ""

    DateTime(yearNumber,monthNumber,dayNumber)








let main() =
    //displays welcome message to the user
    messages.welcome()
    let prompt = menu0

    let rec runMain() =
        userInput(prompt)
        match Console.ReadLine() with
            //this week
            | "1" | "this week" ->
                let mutable w = Unchecked.defaultof<classes.Week>
                try
                    w <- Option.get(control.getThisWeek())
                with
                    | _ ->
                        w <- new classes.Week(control.thisWeek(),
                                              control.thisYear())
                weekMode(w,prompt)
                classes.CalenderSezializer.Serialize(w)
                runMain()
            //next week
            | "2" | "next week" ->
                let mutable w = Unchecked.defaultof<classes.Week>
                try
                    w <- Option.get(control.getNextWeek())
                with
                    | _ ->
                        w <- new classes.Week(control.nextWeek(),
                                              control.nextWeeksYear())
                weekMode(w,prompt)
                classes.CalenderSezializer.Serialize(w)
                runMain()
            //enter a week number
            | "3" ->
                try
                    userInput("please enter a week number:")
                    let weekNumber = Int32.Parse(Console.ReadLine())
                    if control.isWeekNumberValid(weekNumber) then
                        let mutable w = Unchecked.defaultof<classes.Week>
                        match control.getNumberWeek(weekNumber) with
                            | Some(x) ->
                                w <- x
                            | None ->
                                w <- new classes.Week(weekNumber,
                                                      control.thisYear())
                        weekMode(w,prompt)
                        classes.CalenderSezializer.Serialize(w)
                with
                    | _ -> ()
                runMain()
            //pick a day
            | "4" ->
                let dateTime = pickADay()
                let mutable w = Unchecked.defaultof<classes.Week>
                match control.getWeekFromDateTime(dateTime) with
                    | Some(x) -> w <- x
                    | None ->
                        let week = control.weekFromDateTime(dateTime)
                        let y = if dateTime.Month = 1 && week > 50 then
                                    dateTime.Year - 1
                                elif dateTime.Month = 12 && week < 3 then
                                    dateTime.Year + 1
                                else dateTime.Year
                        w <- new classes.Week(week,y)
                weekMode(w,prompt)
                classes.CalenderSezializer.Serialize(w)
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



