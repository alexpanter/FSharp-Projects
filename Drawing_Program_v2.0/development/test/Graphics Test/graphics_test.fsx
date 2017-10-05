//module graphics

// This test file will test drawing the program window and its attached panels.
// Furthermore, it will test how resizing the main window should function.



open System.Windows.Forms
open System.Drawing


// configurable attributes
(* Difference between a forms Size and the forms ClientSize is
                  {Width=16, Height=59}
*)
let internal menuHeight = 39  // the default height of a main menu strip
let mutable internal mainSize = (627,462 + menuHeight)
let internal sidePanelWidth = 200
let internal msgBoxHeight = 60
let internal shapesPanelHeight = 300


// the whole program window
let internal mainFormSize() = Size(fst mainSize, snd mainSize)

// left-side panel
let internal mainPanelPoint = Point(0,0)
let internal mainPanelSize() = Size(mainFormSize().Width - sidePanelWidth,
                                    mainFormSize().Height)

// right-side panel
let internal sidePanelPoint() = Point(mainPanelSize().Width, 0)
let internal sidePanelSize = Size(mainFormSize().Width - mainPanelSize().Width,
                                  mainFormSize().Height - 59)


// subpanels for the left-side panel
let internal drawingPanelPoint = Point(0, 0)
let internal drawingPanelSize() = Size(mainPanelSize().Width,
                                       mainPanelSize().Height - msgBoxHeight - menuHeight)

let internal messageBoxPoint() = Point(0, drawingPanelSize().Height)
let internal messageBoxSize() = Size(mainPanelSize().Width, msgBoxHeight)


// subpanels for the right-side panel
let internal shapesPanelPoint() = Point(0,0)
let internal shapesPanelSize = Size(sidePanelSize.Width, shapesPanelHeight)

let internal toolPanelPoint() = Point(0, shapesPanelSize.Height)
let internal toolPanelSize = Size(sidePanelWidth,
                                  sidePanelSize.Height - shapesPanelSize.Height)



// creating all forms
let form = new Form( Text = "Graphics Test"
                   , Size = mainFormSize()
                   , MinimumSize = mainFormSize()
                   )

let mainPanel = new Panel( Parent = form
                         , Location = mainPanelPoint
                         , Size = mainPanelSize()
                         )

let sidePanel = new Panel(Parent = form
                         , Location = sidePanelPoint()
                         , Size = sidePanelSize
                         )

let drawPanel = new Panel( Parent = mainPanel
                         , Location = drawingPanelPoint
                         , Size = drawingPanelSize()
                         , BackColor = Color.Blue
                         )

let msgBox = new Panel( Parent = mainPanel
                      , Location = messageBoxPoint()
                      , Size = messageBoxSize()
                      , BackColor = Color.Yellow
                      )

let shapePanel = new Panel( Parent = sidePanel
                          , Location = shapesPanelPoint()
                          , Size = shapesPanelSize
                          , BackColor = Color.Green
                          )

let toolPanel = new Panel( Parent = sidePanel
                         , Location = toolPanelPoint()
                         , Size = toolPanelSize
                         , BackColor = Color.Lime
                         )


//
form.Resize.AddHandler(new System.EventHandler
    (fun s e ->
        mainSize <- (form.Size.Width, form.Size.Height)
        
        mainPanel.Size <- mainPanelSize()
        sidePanel.Location <- sidePanelPoint()

        drawPanel.Size <- drawingPanelSize()
        msgBox.Location <- messageBoxPoint()
        msgBox.Size <- messageBoxSize()
    ))

form.ResizeEnd.AddHandler(new System.EventHandler
    (fun s e ->
        printfn ""
        printfn "Size:       %A" form.Size
        printfn "ClientSize: %A" form.ClientSize
        printfn "Difference: {Width=%i, Height=%i}"
          (form.Size.Width-form.ClientSize.Width)
          (form.Size.Height-form.ClientSize.Height)
    ))



//

let m1 = new MenuItem("Files")
let m2 = new MenuItem("Tools")
let m3 = new MenuItem("Help")
let mainMenu = new MainMenu [|m1;m2;m3|]
form.Menu <- mainMenu
form.ShowInTaskbar <- true

//
form.Controls.AddRange ([|mainPanel;sidePanel|])
mainPanel.Controls.AddRange ([|drawPanel;msgBox|])
sidePanel.Controls.AddRange ([|shapePanel;toolPanel|])
form.Show ()

//
[<System.STAThreadAttribute>]
Application.Run (form)
