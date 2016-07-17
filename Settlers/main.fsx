#load "drawingPanel.fsx"
#load "applicationForm.fsx"


// main function
let main() =

    let f = new ApplicationForm.MainForm()
    let p = new DrawingPanel.MainPanel(f)

    System.Windows.Forms.Application.Run(f)


// running the application
main()
