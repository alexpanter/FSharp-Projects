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



type Week() = class

    let mutable path = ""
    let monday = new Day("monday")
    let tuesday = new Day("tuesday")
    let wednesday = new Day("wednesday")
    let thursday = new Day("thursday")
    let friday = new Day("friday")
    let saturday = new Day("saturday")
    let sunday = new Day("sunday")

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
            | "thursday"  | "Thursday"    | "4" -> thursday
            | "friday"    | "Friday"    | "5" -> friday
            | "saturday"  | "Saturday"  | "6" -> saturday
            | "sunday"    | "Sunday"    | "0" | "7" -> sunday
            | _ as k ->
                failwithf "Week.getDay(): The day %s does not exist" k

    member this.Path
        with get() = path
        and set(s: string) = path <- s
            
end



// The CalenderSerializer is used to serialize and deserialize all
// data being used in the application
type CalenderSezializer() = class
    static member Serialize(input: obj, path: string) =
        let formatter = Binary.BinaryFormatter()
        use stream = new IO.FileStream(path, IO.FileMode.Create)
        formatter.Serialize(stream, input)

    static member Deserialize(path: string) =
        try
            use stream = new IO.FileStream(path, IO.FileMode.Open)
            let formatter = Binary.BinaryFormatter()
            Some(formatter.Deserialize(stream))
        with
            | _ -> None
end

