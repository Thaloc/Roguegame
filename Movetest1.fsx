open System
printfn "Alas, Hero - the Demon hath not beeneth cleft in twain yet. Thou art up for the task, oh brave soul?"

let mutable x = 0 // spawn
let mutable y = 0 // spawn
let mutable stop = false
while not stop do
        let key = Console.ReadKey true
        let keyString = key.Key.ToString()

        match keyString with
        | "A" ->
            y <- y-1
            printfn "%A" (x,y)
            // felt player replacer 
            stop <- false
        | "W" ->
            x <- x + 1
            printfn "%A" (x,y) 
            stop <- false
        | "D" ->
            y <- y+1
            printfn "%A" (x,y) 
            stop <- false
        | "S" ->
            x <- x-1
            printfn "%A" (x,y) 
            stop <- false
        | "Q" when key.Modifiers = ConsoleModifiers.Shift -> stop <- true
        | _ -> stop <- false