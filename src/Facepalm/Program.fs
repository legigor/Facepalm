module Kernels =

    // http://en.wikipedia.org/wiki/Kernel_(image_processing)#Convolution

    let identity = 
        [|
            [| 0; 0; 0|]
            [| 0; 1; 0|]
            [| 0; 0; 0|]
        |] |> array2D
        
    let edge3 = 
        [[-1;-1;-1]
         [-1; 8;-1]
         [-1;-1;-1]] |> array2D
        
    let sharpen = 
        [|
            [| 0;-1; 0|]
            [|-1; 5;-1|]
            [| 0;-1; 0|]
        |] |> array2D

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


let input = 
    let rnd = System.Random()
    Array2D.init 10 10 (fun x y -> rnd.Next(255))

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


open System.Drawing
open System.Drawing.Imaging

let grayscale (c:Color) =
    let (r, g, b) = (float c.R, float c.G, float c.B)
    let x = int ((0.299 * r + 0.587 * g + 0.114 * b))
    Color.FromArgb(int c.A, x, x, x)

let transformImage(inputImage:string, kernel) = 
    let inputBmp = new Bitmap(inputImage)
    let width = inputBmp.Width
    let height = inputBmp.Height
    let getInput x y = inputBmp.GetPixel(x, y) |> grayscale |> fun c -> int c.R
    let res = convo3d width height getInput kernel
    let outputBmp = new Bitmap(inputBmp.Width, inputBmp.Height)
    let cc c = match c with 
        | c when c < 0 -> 0 
        | c when c > 255 -> 255
        | _ -> c
    Seq.iter (fun (x, y, v) -> outputBmp.SetPixel(x, y, Color.FromArgb(255, cc v, cc v, cc v) ) ) res
    outputBmp

transformImage ("input-600.jpg", Kernels.edge3) |> fun t -> t.Save("output-600.jpg", ImageFormat.Jpeg)