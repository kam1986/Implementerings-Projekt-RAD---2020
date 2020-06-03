module Tests

// stopwatch lib
open System.Diagnostics
// project code
open Hashing
open HashTable
open DataStream
open RandomTool
open System.IO

// Del 1 (1.c)
let testRunTime n l (h : (uint64 -> uint64)) = 
    let stream = DataStream.createStream n l
    let stopWatch = Stopwatch.StartNew()
    let multShiftSum = Seq.fold (+) 0UL (Seq.map (fun (x, s) -> h x) stream)
    stopWatch.Stop()
    printfn "sum: %A time: %A" multShiftSum stopWatch.Elapsed.TotalMilliseconds


let testHash () =
    let a = randomUint64 ()
    let n = 1000000
    let msh = multiplyShift (a, 20)
    printfn "Multiply Shift"
    testRunTime n 20 msh

    let ma = randomUint89m1 ()
    let mb = randomUint89m1 ()
    let mmp = multiplyModPrime (ma, mb, 20)
    printfn "Multiply Mod Prime"
    testRunTime n 20 mmp


// Del 2 hashtable testing

// for reuse of the S value, it takes a stream of tokens (uint64 * int) 
// a hash function and the parameters for that hashfunction
let S stream l hash hashparams =
    // make new table
    let table = init l (hash hashparams)
    // adding each token from the stream into the table
    // Seq. are really slow ............. -_-
    Seq.fold (fun table (x, d) -> increment x d table) table stream
    |> fun (Table (table, _)) -> 
        Array.fold // for all list in the array
            ( fun sum lst -> 
                List.sumBy // sum each entry with the function (_, d) -> d * d
                    ( fun (_, d) -> 
                         int64 (d * d)
                    ) lst // argument to List.sumBy
                    + sum // adding to accumulator
            ) (int64 0) table
  

let TestHashtable n l hash hashparams =
    let stream = DataStream.createStream n l
    S stream l hash hashparams


// m: Size of Count Sketch container 
let experiments (m : int) stream =
    let evaluateCountSketch i =
        printfn "%A" i
        (CountSketch.CountSketch m (CountSketch.getCountSketchHash m) stream)

    List.init 100 evaluateCountSketch

let meanSquare X S =
    let res = List.sumBy (fun x -> (double (S-x)) ** 2.0) X
    (double res) / double (List.length X)

let presentExperiment (n : int) l (m : int) =
    let stream = DataStream.createStream n l
    let X = experiments m stream
    let Xsort = List.sort X
    let xAxis = List.init 100 id 
    let realS = S stream m multiplyShift (randomUint64 (), m)  // calculate S here
    printfn "Resulting estimators"
    printfn "%A" Xsort
    printfn "Actual Value"
    printfn "%A" realS
    printfn "Mean Square (estimated varians): %A" (meanSquare Xsort (int realS))
    printfn "Calculated Varians: %A" (2.0 * (double realS) ** 2.0 / (double (1 <<< m)))
    let Xres = String.concat " " (List.map (fun x -> string x) Xsort) 

    // Grouping into size 11

    let groupedX = List.init 9 (fun x -> X.[x*11..x*11+10])
    let medianX = List.map (List.sort >> (List.item 5)) groupedX
    let sortedmedianX = List.sort medianX
    printfn "Resulting estimators with grouping"
    printfn "%A" sortedmedianX

    let XmedRes = String.concat " " (List.map (fun x -> string x) sortedmedianX) 

    let fileName = "./" + (String.concat "_" (List.map (fun x -> string x) [n;m;l])) + ".txt"
    let res = String.concat "\n" [Xres; XmedRes; string realS]
    File.WriteAllText(fileName,res) 

