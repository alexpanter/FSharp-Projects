module calender.classes

open System
open System.Collections.Generic
open System.Runtime.Serialization.Formatters

type Time =
    { mutable hour: int;
      mutable minute: int }
with
    override this.ToString() =
        let h = if this.hour = 0 then "00" else (string this.hour)
        let m = if this.minute = 0 then "00" else (string this.minute)
        String.Format("{0}:{1}",h,m)


type Task(start: int * int, stop: int * int, description: string) = class
    let startTime = { hour = fst start; minute = snd start }
    let stopTime = { hour = fst stop; minute = snd stop }
    let mutable description = description

    member this.StartTime = startTime
    member this.StopTime = stopTime
    member this.Description = description
end



type Day(dayText: string) = class
    let tasks = List<Task>()

    member this.DayText = dayText

    member this.Tasks() = tasks.AsReadOnly()

    member this.AddTask(t: Task) =
        try
            tasks.Add(t)
            true
        with
            | _ -> false

    member this.RemoveTask(id: int) =
        try
            tasks.RemoveAt(id - 1)
            true
        with
            | _ -> false

    member this.getPrintFormat() =
        [ for i = 1 to tasks.Count do yield (i,tasks.[i-1]) ]
end



type Week(weekNumber: int, yearNumber: int) = class

    let path =
        (__SOURCE_DIRECTORY__,"data",string yearNumber)
        |> IO.Path.Combine
        
    let file = string weekNumber
    
    let monday = new Day("Monday")
    let tuesday = new Day("Tuesday")
    let wednesday = new Day("Wednesday")
    let thursday = new Day("Thursday")
    let friday = new Day("Friday")
    let saturday = new Day("Saturday")
    let sunday = new Day("Sunday")

    member this.Monday = monday
    member this.Tuesday = tuesday
    member this.Wednesday = wednesday
    member this.Thursday = thursday
    member this.Friday = friday
    member this.Saturday = saturday
    member this.Sunday = sunday
    
    member this.getDay(day: string) =
        match day with
            | "monday"    | "Monday"    | "1" -> monday
            | "tuesday"   | "Tuesday"   | "2" -> tuesday
            | "wednesday" | "Wednesday" | "3" -> wednesday
            | "thursday"  | "Thursday"  | "4" -> thursday
            | "friday"    | "Friday"    | "5" -> friday
            | "saturday"  | "Saturday"  | "6" -> saturday
            | "sunday"    | "Sunday"    | "0" | "7" -> sunday
            | _ as k ->
                failwithf "Week.getDay(): The day %s does not exist" k
    
    member this.Path = path
    member this.File = file         
end



// The CalenderSerializer is used to serialize and deserialize all
// data being used in the application
type CalenderSezializer() = class
    static member Serialize(w: Week) =
        let formatter = Binary.BinaryFormatter()
        if not (IO.Directory.Exists(w.Path)) then
            IO.Directory.CreateDirectory(w.Path)
            |> ignore
        let target = IO.Path.Combine(w.Path,w.File)
        use stream = new IO.FileStream(target, IO.FileMode.Create)
        formatter.Serialize(stream, w |> box)

    static member internal Deserialize(path,file) =
        try
            let target = IO.Path.Combine(path,file)
            use stream = new IO.FileStream(target, IO.FileMode.Open)
            let formatter = Binary.BinaryFormatter()
            Some(formatter.Deserialize(stream))
        with
            | _ -> None
end

