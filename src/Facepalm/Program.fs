module Kernels =

    let private toArray<'T> (d:'T[][]) =
        Array2D.init 3 3 (fun x y -> d.[x].[y])

    // http://en.wikipedia.org/wiki/Kernel_(image_processing)#Convolution

    let identity = 
        [|
            [| 0; 0; 0|]
            [| 0; 1; 0|]
            [| 0; 0; 0|]
        |] |> toArray
        
    let edge3 = 
        [|
            [|-1;-1;-1|]
            [|-1; 8;-1|]
            [|-1;-1;-1|]
        |] |> toArray
        
    let sharpen = 
        [|
            [| 0;-1; 0|]
            [|-1; 5;-1|]
            [| 0;-1; 0|]
        |] |> toArray

let input = 
    let rnd = System.Random()
    Array2D.init 10 10 (fun x y -> rnd.Next(255))

let convo3d width height getInput kernel =
    let edged l x =
        match x with
            | x when x < 0 -> 0
            | x when x >= l -> (l - 1)  
            | _ -> x
    let getk x y = Array2D.get kernel x y
    let c x y = 
        Seq.sum (
            seq {
                for dx in -1..1 do
                for dy in -1..1 
                    ->  getk (dx + 1) (dy + 1) * getInput (edged width (x + dx)) (edged height (y + dy))
        })
    seq { for x in 0..width - 1 do 
          for y in 0..height - 1 -> x, y, c x y }

let output k =
    printfn "Input of %s\n%A" (input.GetType().ToString()) input
    printfn "Kernel of %s\n%A" (k.GetType().ToString()) (k)
    let width = Array2D.length1 input
    let height = Array2D.length2 input
    let getInput x y = input.[x, y]
    convo3d width height getInput k

let res k = 
    let a = Array2D.copy input
    Seq.iter (fun (x, y, v) -> Array2D.set a x y v ) (output k)
    a

printfn "Result\n%A" (res Kernels.sharpen)