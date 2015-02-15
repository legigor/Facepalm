// [<EntryPoint>]
// let main argv =
//     printfn "%A" argv
//     0

open System.Drawing
open System.Drawing.Imaging;

let inputFile = "face1.jpg"
let outputFile = "face1-result.jpg"

// Load image, get the some matrix for processing
let inputBmp = new Bitmap(inputFile)
let inputData = Array2D.init inputBmp.Width inputBmp.Height (fun x y -> inputBmp.GetPixel(x, y))
    

// Write the result
let outputBmp = new Bitmap(inputBmp.Width, inputBmp.Height)
Array2D.iteri (fun x y c -> outputBmp.SetPixel(x, y, c)) inputData
outputBmp.Save(outputFile, ImageFormat.Jpeg)
