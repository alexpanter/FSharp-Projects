module calender.control

open System
open System.Globalization
#load "classes.fs"



let year() =
    DateTime.Now.Year


let today() =
    DateTime.Now.DayOfWeek.ToString()



let internal week(value: float) =
    let dateTime = DateTime.Now.AddDays(value)
    let calender = GregorianCalendar()
    let weekRule = CalendarWeekRule.FirstFourDayWeek
    let firstDay = DayOfWeek.Monday
    
    calender.GetWeekOfYear(dateTime, weekRule, firstDay)


let thisWeek() = week(0.0)
let nextWeek() = week(7.0)



let internal getWeekNumber(value: System.DateTime) =
    let calender = GregorianCalendar()   
    let weekRule = CalendarWeekRule.FirstFourDayWeek
    let firstDay = DayOfWeek.Monday
    calender.GetWeekOfYear(value, weekRule, firstDay)




let getMaxNumberOfWeeks(y: int) =
    let rec run = function
        | (d: int) ->
            let t = DateTime(y,12,d)
            if t.DayOfWeek = DayOfWeek.Thursday then
                getWeekNumber(t)
            else
                run(d - 1)

    run(31)




let getWeekFromBinary(w: int) =
    let path = IO.Path.Combine("data", year() |> string, w |> string)
    let data = calender.classes.CalenderSezializer.Deserialize(path)
    match data with
        | Some(x) -> Some((x |> unbox): calender.classes.Week)
        | None -> None

let a = System.DateTime(2016,12,31)


let isWeekNumValid(weekNum: int) =
    not (weekNum > getMaxNumberOfWeeks(year()) || weekNum < 0)





let getWeek() = getWeekFromBinary(week(0.0))

let getNextWeek() = getWeekFromBinary(week(7.0))


