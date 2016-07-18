module calender.control

open System
open System.Globalization
#load "classes.fs"



let private year(daysToAdd: float) =
    DateTime.Now.AddDays(daysToAdd).Year


let private week1(dateTime: DateTime) =
    let calendar = GregorianCalendar()
    let weekRule = CalendarWeekRule.FirstFourDayWeek
    let firstDay = DayOfWeek.Monday
    calendar.GetWeekOfYear(dateTime, weekRule, firstDay)


let private week2(daysToAdd: float) =
    let dateTime = DateTime.Now.AddDays(daysToAdd)
    let calendar = GregorianCalendar()
    let weekRule = CalendarWeekRule.FirstFourDayWeek
    let firstDay = DayOfWeek.Monday
    calendar.GetWeekOfYear(dateTime, weekRule, firstDay)




// calculate the number of weeks in the input year y.
let private getMaxNumberOfWeeks(y: int) =
    let rec run = function
        | (d: int) ->
            let t = DateTime(y,12,d)
            if t.DayOfWeek = DayOfWeek.Thursday then
                week1(t)
            else
                run(d - 1)

    run(31)



// getting serialized data and converting it to a classes.Week object.
// Input arguments are week number (w) and year (y).
let private getWeekFromBinary(w: int, y: int) =
    let path = IO.Path.Combine(__SOURCE_DIRECTORY__,"data", string y)
    let data = calender.classes.CalenderSezializer.Deserialize(path,string w)
    match data with
        | Some(x) -> Some((x |> unbox): calender.classes.Week)
        | None -> None









// API-functions to facade this library file. Nothing above this
// comment should be accessible from outside this module.
let public today() = DateTime.Now.DayOfWeek.ToString()


let public thisWeek() = week2(0.0)


let public nextWeek() = week2(7.0)


let public weekFromDateTime(dateTime: DateTime) = week1(dateTime)


let public thisYear() = year(0.0)


let public nextWeeksYear() =
    let y = thisYear()
    match nextWeek() with
        | 1 -> y + 1
        | _ -> y


let public getNumberWeek(n: int) = getWeekFromBinary(n,year(0.0))


let public getThisWeek() = getWeekFromBinary(week2(0.0),year(0.0))


let public getNextWeek() = getWeekFromBinary(week2(7.0),year(0.0))


let public getWeekFromDateTime(dateTime: DateTime) =
    getWeekFromBinary(weekFromDateTime(dateTime),dateTime.Year)


let public isWeekNumberValid(weekNum: int) =
    not (weekNum > getMaxNumberOfWeeks(year(0.0)) || weekNum < 0)
