open System
open System.IO

let readfile file =
    File.ReadAllLines(file).[1..]
    |> Array.map (fun line -> line.Split(',') |> Array.map int)

// Load the training sample  

let trainingset = readfile @"\trainingsample.csv"

//  COMPUTING DISTANCES

// We need to compute the euclidean distance between images

let distance (p1: int[]) (p2: int[]) =
    Math.Sqrt (float (Array.sum (Array.map2 ( fun a b -> (pown (a-b) 2)) p1 p2) ))


//  WRITING THE CLASSIFIER FUNCTION

// We are now ready to write a classifier function!
// The classifier should take a set of pixels
// (an array of ints) as an input, search for the
// closest example in our sample, and predict
// the value of that closest element.

let classify (pixels: int[]) =
    (trainingset |> Array.minBy (fun a -> distance a.[1..] pixels)).[0]

// EVALUATING THE MODEL AGAINST VALIDATION DATA

// Now that we have a classifier, we need to check
// how good it is. 
// This is where the 2nd file, validationsample.csv,
// comes in handy. 
// For each Example in the 2nd file,
// we know what the true Label is, so we can compare
// that value with what the classifier says.
// You could now check for each 500 example in that file
// whether your classifier returns the correct answer,
// and compute the % correctly predicted.

let validationsample = readfile @"\validationsample.csv"

let main() = 
    let countcorr = validationsample |> Array.fold (fun acc a -> if classify a.[1..] = a.[0] then acc+1 else acc) 0
    printfn "Accuracy: %f %%" (float countcorr / (float (Array.length validationsample))*100.)
