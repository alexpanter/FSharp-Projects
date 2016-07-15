module calender.messages


type ss = System.String

let print = fun x -> Seq.iter (fun y -> printfn "%s" y) x


let printNoTasks(word: string) =
    [ss.Format("\nThere are no tasks for {0}.",word)]
    |> print


let printTasks(word: string) =
    [ss.Format("\nThese are all your tasks for {0}:",word)]
    |> print


[<System.ObsoleteAttribute>]
let weekDays(word: string) =
    [ss.Format("\nPlease enter a day from {0}:",word);
     "1)  monday";
     "2)  tuesday";
     "3)  wednesday";
     "4)  thursday";
     "5)  friday";
     "6)  saturday";
     "7)  sunday";
     "8)  show all days";
     ""]
    |> print


let welcome() =
    ["Welcome to Calender. You can write \"menu\" to see the menu at any given time.";
     ""]
    |> print


let showMainMenu() =
    ["These are the menu options:";
     "1)  this week";
     "2)  next week";
     "3)  enter a week number";
     "0)  exit";
     ""]
    |> print

let showWeekMenu() =
    ["1)  monday";
     "2)  tuesday";
     "3)  wednesday";
     "4)  thursday";
     "5)  friday";
     "6)  saturday";
     "7)  sunday";
     "8)  show all days";
     "9)  today";
     "0)  back"]
    |> print

let showDayMenu() =
    ["1)  show today's tasks";
     "2)  add a new task";
     "3)  remove an existing task";
     "0)  back"]
    |> print

// TESTING
welcome()


