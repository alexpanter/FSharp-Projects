module graphics.graphics

// The file 'settings_globals' containing the module 'globals'
// is included compile time.

open System.Windows.Forms
open System.Drawing



// >>>>>>>>> PROGRAM WINDOW PROPERTIES <<<<<<<<<
(* Difference between a form's Size and the form's ClientSize is
                  {Width=16, Height=59}
*)
let public menuHeight = 39  // the default height of a main menu strip
let public mainSize = (627,462 + menuHeight) // should be made mutable in
                                             // implementation file.
let public sidePanelWidth = 200
let public msgBoxHeight = 60
let internal shapesPanelHeight = 300

// the whole program window
let mainFormSize() = Size(fst mainSize, snd mainSize)

// left-side panel
let mainPanelPoint = Point(0,0)
let mainPanelSize() = Size(mainFormSize().Width - sidePanelWidth,
                           mainFormSize().Height)

// right-side panel
let sidePanelPoint() = Point(mainPanelSize().Width, 0)
let sidePanelSize = Size(mainFormSize().Width - mainPanelSize().Width,
                         mainFormSize().Height - 59)

// subpanels for the left-side panel
let drawingPanelPoint = Point(0, 0)
let drawingPanelSize() = Size(mainPanelSize().Width,
                              mainPanelSize().Height - msgBoxHeight - menuHeight)

let messageBoxPoint() = Point(0, drawingPanelSize().Height)
let messageBoxSize() = Size(mainPanelSize().Width, msgBoxHeight)

// subpanels for the right-side panel
let shapesPanelPoint() = Point(0,0)
let shapesPanelSize = Size(sidePanelSize.Width, shapesPanelHeight)

let toolPanelPoint() = Point(0, shapesPanelSize.Height)
let toolPanelSize = Size(sidePanelWidth,
                         sidePanelSize.Height - shapesPanelSize.Height)










let createMainWindow() =
    new Form
        ( Size = mainFormSize()
        , MinimumSize = mainFormSize()
        , Text = globals.programName
        , Icon = new Icon( globals.rootFolder
                         + globals.pathSeparator
                         + "resources"
                         + globals.pathSeparator
                         + "logo_small.ico"
                         )
        , ShowIcon = true
        )

let createMainPanel(parent: Form) =
    new Panel
        ( Parent = parent
        , Location = mainPanelPoint
        , Size = mainPanelSize()
        )

let createSidePanel(parent: Form) =
    new Panel
        ( Parent = parent
        , Location = sidePanelPoint()
        , Size = sidePanelSize
        )

let createDrawingPanel(parent: Panel) =
    new Panel
        ( Parent = parent
        , Location = drawingPanelPoint
        , Size = drawingPanelSize()
        , BackColor = Color.Blue
        )

let createMessagePanel(parent: Panel) =
    new Panel
        ( Parent = parent
        , Location = messageBoxPoint()
        , Size = messageBoxSize()
        , BackColor = Color.Yellow
        )

let createShapesPanel(parent: Panel) =
    new Panel
        ( Parent = parent
        , Location = shapesPanelPoint()
        , Size = shapesPanelSize
        , BackColor = Color.Green
        )

let createToolsPanel(parent: Panel) =
    new Panel
        ( Parent = parent
        , Location = toolPanelPoint()
        , Size = toolPanelSize
        , BackColor = Color.Lime
        )




