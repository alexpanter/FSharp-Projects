module instance.file

#r "drawing.dll"  // contains the modules 'shapes' and 'math'
//#r "memory.dll"  //containing classes for memory stack (used in 'undo' and 'redo')

open System.Runtime.Serialization.Formatters
open System.Collections.Generic
open System.IO
open System.Drawing
open System.Windows.Forms




// Instance of a Shadow Draw file
type FileInstance() = class
    
    // All basic attributes
    let createdShapes = List<shapes.allShapes>(4)
    let selectedShapes = List<shapes.allShapes>(4)
    let mutable instanceName = "Untitled"

    // global drawing settings
    let mutable rotationVelocity = System.Math.PI / 16.0
    let mutable moveVelocity = 5
    let mutable scaleVelocity = 0.1

    // save properties
    let mutable isChangesUnsaved = false    // any new changes since last save
    let mutable isFileSaved = false         // has the FileInstance been saved

    // Changing name of the drawing instance (used as "save")
    member this.InstanceName
        with get() = instanceName
        and set(n: string) = instanceName <- n

    // save properties
    member this.IsChangesUnsaved
        with get() = isChangesUnsaved
        and set(b: bool) = isChangesUnsaved <- b
    
    member this.IsFileSaved
        with get() = isFileSaved
        and set(b: bool) = isFileSaved <- b


    // Adding new shapes to the program instance
    member this.AddTriangle(p: Point) =
        createdShapes.Add(shapes.allShapes.Tri(new shapes.Triangle(p)))

    member this.AddSquare(p: Point) =
        createdShapes.Add(shapes.allShapes.Sq(new shapes.Square(p)))

    member this.AddRectangle(p: Point) =
        createdShapes.Add(shapes.allShapes.Rect(new shapes.Rectangle(p)))

    member this.AddPentagon(p: Point) =
        createdShapes.Add(shapes.allShapes.Pent(new shapes.Pentagon(p)))

    member this.AddPolygon(p: Point, n: int) =
        // should probably be more complex, e.g. with an array of Points
        // from which to calculate a center Point.
        createdShapes.Add(shapes.allShapes.Pol(new shapes.Polygon(p,n)))


    //
    //
    // All member functions related to manipulating and drawing shapes 
    member this.CreatedShapes = createdShapes

    member this.SelectedShapes = selectedShapes

    member this.ClearSelection() = selectedShapes.Clear()

    member this.AddToSelection(s: shapes.allShapes) = selectedShapes.Add(s)


    (* MOVE TO MAININSTANCE ??? *)
    member this.DrawShape(f: Form, s: shapes.Shape) =
        f.Paint.Add(fun g -> g.Graphics.FillPolygon(s.Brush,s.Points))
        f.Refresh()
        

    // shape scaling
    member private this.ScaleUpShape(s: shapes.allShapes) =
        let r = shapes.getShape(s).Radius
        shapes.getShape(s).Radius <- r + scaleVelocity

    member private this.ScaleDownShape(s: shapes.allShapes) =
        let r = shapes.getShape(s).Radius
        shapes.getShape(s).Radius <- r - scaleVelocity

    member this.ScaleUpSelection() =
        for s in selectedShapes do
            this.ScaleUpShape(s)

    member this.ScaleDownSelection() =
        for s in selectedShapes do
            this.ScaleDownShape(s)

    // shape moving
    member private this.MoveLeftShape(s: shapes.allShapes) =
        let p = new Point(shapes.getShape(s).Center.X - moveVelocity,
                          shapes.getShape(s).Center.Y)
        shapes.getShape(s).Center <- p

    member private this.MoveRightShape(s: shapes.allShapes) =
        let p = new Point(shapes.getShape(s).Center.X + moveVelocity,
                          shapes.getShape(s).Center.Y)
        shapes.getShape(s).Center <- p

    member private this.MoveUpShape(s: shapes.allShapes) =
        let p = new Point(shapes.getShape(s).Center.X,
                          shapes.getShape(s).Center.Y - moveVelocity)
        shapes.getShape(s).Center <- p

    member private this.MoveDownShape(s: shapes.allShapes) =
        let p = new Point(shapes.getShape(s).Center.X,
                          shapes.getShape(s).Center.Y + moveVelocity)
        shapes.getShape(s).Center <- p

    member this.MoveLeftSelection() =
        for s in selectedShapes do
            this.MoveLeftShape(s)

    member this.MoveRightSelection() =
        for s in selectedShapes do
            this.MoveRightShape(s)

    member this.MoveUpSelection() =
        for s in selectedShapes do
            this.MoveUpShape(s)

    member this.MoveDownSelection() =
        for s in selectedShapes do
            this.MoveDownShape(s)

    // shape rotation
    member private this.RotateLeftShape(s: shapes.allShapes) =
        let mutable i = 0
        let sToShape = shapes.getShape(s)
        for v in sToShape.Vectors do
            sToShape.Item(i) <- math.Vector.RotateVector (v) (rotationVelocity)  
            i <- i + 1
            
    member private this.RotateRightShape(s: shapes.allShapes) =
        let mutable i = 0
        let sToShape = shapes.getShape(s)
        for v in sToShape.Vectors do
            sToShape.Item(i) <- math.Vector.RotateVector (v) (-rotationVelocity)
            i <- i + 1  

    member this.RotateLeftSelection() =
        for s in selectedShapes do
            this.RotateLeftShape(s)

    member this.RotateRightSelection() =
        for s in selectedShapes do
            this.RotateRightShape(s)


end



