// [<EntryPoint>]
// let main argv =
//     printfn "%A" argv
//     0

// open System.Drawing
// open System.Drawing.Imaging;
// 
// let inputFile = "face1.jpg"
// let outputFile = "face1-result.jpg"
// 
// let inputBmp = new Bitmap(inputFile)
// 
// 
// let output = 
//     seq { for x in 0..inputBmp.Height do 
//             for y in 0..inputBmp.Width
//                 -> (x, y, inputBmp.GetPixel(x, y))
//     }
// 
// let outputBmp = new Bitmap(inputBmp.Width, inputBmp.Height)
// for c in output do
//     printfn "%A" c
// 
// 
// Array2D.iteri (fun x y c -> outputBmp.SetPixel(x, y, c)) output

// http://en.wikipedia.org/wiki/Kernel_(image_processing)
// let grayscale (c:Color) =
//     let (r, g, b) = (float c.R, float c.G, float c.B)
//     let x = int ((0.299 * r + 0.587 * g + 0.114 * b))
//     Color.FromArgb(int c.A, x, x, x)
// 
// // Load image, get the some matrix for processing
// let inputData =
//     Array2D.init inputBmp.Width inputBmp.Height
//         (fun x y -> grayscale(inputBmp.GetPixel(x, y)))
// 
// 
// // Write the result
// let outputBmp = new Bitmap(inputBmp.Width, inputBmp.Height)
// Array2D.iteri (fun x y c -> outputBmp.SetPixel(x, y, c)) inputData
// outputBmp.Save(outputFile, ImageFormat.Jpeg)


