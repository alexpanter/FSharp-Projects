// This is the main file of the game Crate Pusher.
// Here all assets are imported and attached to
// drawing functions and handlers.

open System.Drawing
open System.Windows.Forms

#load "maps/maps.fsx"
#load "visuals.fs"
#load "movement.fs"


let brick = visuals.defaultBrickWall()
let crate = visuals.defaultCrate()
let target = visuals.defaultCrateTarget()

let charUp = visuals.characterUp()
let charDown = visuals.characterDown()
let charLeft = visuals.characterLeft()
let charRight = visuals.characterRight()

let charTargetUp = visuals.characterTargetUp()
let charTargetDown = visuals.characterTargetDown()
let charTargetLeft = visuals.characterTargetLeft()
let charTargetRight = visuals.characterTargetRight()

let direction = ref (1,0)
let coordinate = ref (0,0)
let getDirection() =
    match !direction with
        | (1,0)  -> charRight
        | (0,1)  -> charDown
        | (-1,0) -> charLeft
        | (0,-1) -> charUp
        | _ as v -> failwithf "getDirection received invalid vector: %A" v

let getDirectionWithCharTarget() =
    match !direction with
        | (1,0)  -> charTargetRight
        | (0,1)  -> charTargetDown
        | (-1,0) -> charTargetLeft
        | (0,-1) -> charTargetUp
        | _ as v -> failwithf "getDirection received invalid vector: %A" v

let f = visuals.createMainForm()
let rect(l: Point, s: int) = Rectangle(l,Size(s,s))
let mutable targetSpots = []

let allLevels = maps.levels
let numLevels = List.length allLevels

let levelWon = ref false
let gameOver = ref false

let thisLevelNumber = ref 0
let mutable thisLevel = allLevels.[!thisLevelNumber]()
let mutable pixelSize = f.ClientSize.Width / thisLevel.Size
let gotoNextLevel() =
    if !thisLevelNumber = numLevels - 1 then
        gameOver := true
    else
        thisLevelNumber := !thisLevelNumber + 1
        thisLevel <- allLevels.[!thisLevelNumber]()
        pixelSize <- f.ClientSize.Width / thisLevel.Size
    targetSpots <- []


let updateMap((i,j): int * int, (x,y): int * int) =
    let l = thisLevel.Map
    match (l.[i+y,j+x], l.[i+y+y,j+x+x]) with
        //pushing the crate in the direction of the vector -
        //behind the crate is either an empty space.
        | (2,0) -> l.[i+y,j+x] <- 5
                   l.[i+y+y,j+x+x] <- 2
        //if there is a target behind the crate
        | (2,3) -> l.[i+y,j+x] <- 5
                   l.[i+y+y,j+x+x] <- 2
                   targetSpots <- (i+y+y,j+x+x) :: targetSpots
        | (2,_) -> ()
        //will move the player to an empty space
        | (0,_) -> l.[i+y,j+x] <- 5
        //will move the player to a target
        | (3,_) -> l.[i+y,j+x] <- 4
                   targetSpots <- (i+y,j+x) :: targetSpots
        | _ as k -> failwithf "updateMap received illegal input: %A" k
    //updating the player's spot
    if l.[i,j] = 4 then
        l.[i,j] <- 3
    else 
        l.[i,j] <- 0
    let mutable tempList = []
    for spot in targetSpots do
        if l.[fst spot,snd spot] = 0 then
            l.[fst spot,snd spot] <- 3
        elif l.[fst spot,snd spot] = 5 then
            l.[fst spot,snd spot] <- 4
        else
            tempList <- (fst spot,snd spot) :: tempList
    targetSpots <- tempList
    



// handlers defined for the main form
f.KeyDown.AddHandler(new KeyEventHandler
    (fun s e ->
         let i,j = !coordinate
         let x,y = !direction
         let l = thisLevel
         match e.KeyValue with
             | 38 ->     // up
                 if (x,y) = (0,-1) then
                     if movement.checkMove((i,j), (x,y), l.Map, l.Size) then
                         updateMap((i,j),(x,y))
                 else
                     direction := (0,-1) 
                 f.Refresh()
             | 40 ->     // down
                 if (x,y) = (0,1) then
                     if movement.checkMove((i,j), (x,y), l.Map, l.Size) then
                         updateMap((i,j),(x,y))
                 else
                     direction := (0,1)
                 f.Refresh()
             | 37 ->     // left
                 if (x,y) = (-1,0) then
                     if movement.checkMove((i,j), (x,y), l.Map, l.Size) then
                         updateMap((i,j),(x,y))
                 else
                     direction := (-1,0)
                 f.Refresh()
             | 39 ->     // right
                 if (x,y) = (1,0) then
                     if movement.checkMove((i,j), (x,y), l.Map, l.Size) then
                         updateMap((i,j),(x,y))
                 else
                     direction := (1,0)
                 f.Refresh()
             | 27 ->     // escape
                 f.Close()
             | _ -> ()
     ))




// Paint event handler for the main form
f.Paint.Add(fun e ->
    let mutable targetsLeft = 0
    for i = 0 to (Array2D.length1 <| thisLevel.Map) - 1 do
        for j = 0 to (Array2D.length2 <| thisLevel.Map) - 1 do
            match thisLevel.Map.[i,j] with
            | 0 -> () //empty space
            | 1 -> e.Graphics.DrawImage
                       (brick,
                        rect(Point(j * pixelSize,i * pixelSize), pixelSize))
            
            | 2 -> e.Graphics.DrawImage
                       (crate,
                        rect(Point(j * pixelSize,i * pixelSize), pixelSize))
            
            | 3 -> e.Graphics.DrawImage
                       (target,
                        rect(Point(j * pixelSize,i * pixelSize), pixelSize))
                   targetsLeft <- targetsLeft + 1
            
            | 4 -> e.Graphics.DrawImage
                       (getDirectionWithCharTarget(),
                        rect(Point(j * pixelSize,i * pixelSize), pixelSize))
                   coordinate := (i,j)
                   targetsLeft <- targetsLeft + 1
            
            | 5 -> e.Graphics.DrawImage
                       (getDirection(),
                        rect(Point(j * pixelSize,i * pixelSize), pixelSize))
                   coordinate := (i,j)

            | 10 -> e.Graphics.DrawImage
                       (visuals.endGamePic(),
                        rect(Point(0,0), f.ClientSize.Width))
            
            | _ as k-> failwithf "paint event received illegal integer code: %i" k
    if (targetsLeft = 0) && (not !gameOver) then
        gotoNextLevel()
        f.Refresh()
    )



// Running the application
Application.Run(f)
