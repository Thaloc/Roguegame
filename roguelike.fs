/// Få farve ét specifikt sted 
/// Få det ud af et array 
open System
type Color = ConsoleColor

type Canvas (rows:int, cols:int) = 

    let mutable spillerflade = Array2D.create rows cols (' ', Color.Red, Color.White) 

    member this.Set (x:int, y:int, c:char, fg: Color, bg: Color) = 
        spillerflade.[x,y] <- (c, fg, bg)  


    member this.ShowSF() =  
        for i in 0..rows-1 do
            for j in 0..cols-1 do 
                let (c, fg, bg) = spillerflade.[i, j]
                System.Console.ResetColor()
                System.Console.ForegroundColor <- fg
                System.Console.BackgroundColor <- bg
                printf "%c" c
                System.Console.ResetColor()
            printf "\n"
        System.Console.ResetColor()

let test = Canvas(25, 75)
test.Set (0, 0, '@', Color.Red, Color.White)


[<AbstractClass>]
type Entity() = 
    abstract member RenderOn: Canvas * int * int -> unit // ULOVLIG TYPE??? SKAL VÆRE CANVAS -> UNIT, OG IKKE CANVAS * INT * INT -> UNIT
    default this.RenderOn (canvas, x,y) = () // ULOVLIG TYPE, SE linje 32

type Player() = 
    inherit Entity()
    
    // let mutable xpos = x
    // let mutable ypos = y 

    let mutable CHP = 10 // Current HitPoints

    

    override this.RenderOn (canvas,x,y) =                   // PROBLEMER MED x,y 
        canvas.Set (x, y, '=', Color.White, Color.Black)
        canvas.Set (x, y+1, '^', Color.White, Color.Black) 
        canvas.Set (x, y+2, '.', Color.White, Color.Black) 
        canvas.Set (x, y+3, '.', Color.White, Color.Black) 
        canvas.Set (x, y+4, '^', Color.White, Color.Black)
        canvas.Set (x, y+5, '=', Color.White, Color.Black)  
        canvas.ShowSF()

    member this.HitPoints = CHP 

    member this.Damage(dmg) =
        CHP <- max 0 (CHP - dmg) 

    member this.Heal (h:int) =
        CHP <- min 15 (CHP + h)
    
    // member this.MoveTo (x,y) =

        printfn "Alas, Hero - the Demon hath not beeneth cleft in twain yet. Thou art up for the task, oh brave soul?"  // LOOP SKAL GØRE
        let mutable x = 0 // spawn
        let mutable y = 0 // spawn
        let mutable stop = false
        while not stop do
            let key = Console.ReadKey true
            let keyString = key.Key.ToString()
            match keyString with
            | "A" ->
                y <- y-1
                Player().RenderOn (test, x, y) 
                stop <- false
            | "W" ->
                x <- x + 1
                Player().RenderOn (test, x, y) 
                stop <- false
            | "D" ->
                y <- y+1
                Player().RenderOn (test, x, y) 
                stop <- false
            | "S" ->
                x <- x-1
                Player().RenderOn (test, x, y) 
                stop <- false
            | "Q" when key.Modifiers = ConsoleModifiers.Shift -> stop <- true
            | _ -> stop <- false


    member this.IsDead = 
        CHP <= 0



let Thaloc = Player()

// Thaloc.Damage(3)
// printfn "%A" Thaloc.HitPoints
// Thaloc.Heal(10) 
// printfn "%A" Thaloc.HitPoints
// Thaloc.Damage(18)
// printfn "%A" Thaloc.HitPoints
// printfn "%A" Thaloc.IsDead

Thaloc.RenderOn (test, 5, 10)

// type Item() =
//     inherit Entity() 
//     abstract member InteractWith (Player()) =  // eksekvér mulig handling (giv item, print på skærm)
//     member FullyOccupy = ///hvor stort er det specifikke item - kan man gå igennem

// type Wall =
//     inherit Item

// type Water = 
//     inherit Item

// type Fire =
//     inherit Item

// type FleshEatingPlant = 
//     inherit Item

// type Exit =
//     inherit item