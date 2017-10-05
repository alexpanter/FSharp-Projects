module instance.settings



open System.Windows.Forms
open System.Drawing


// Settings for a MainInstance
type ProgramSettings() = class
    let mutable windowSize = Size(500,500)
    let mutable windowLocation = Unchecked.defaultof<Point>


    member this.WindowSize
        with get() = windowSize
        and set(s: Size) = windowSize <- s


    member this.WindowLocation
        with get() = windowLocation
        and set(p: Point) = windowLocation <- p

    

    (*
    THIS FILE COULD POTENTIALLY CONTAIN THE FOLLOWING DEFINITION :
        - color scheme
        - custom adjusted with and height on panel/windows
    
    *)


end

