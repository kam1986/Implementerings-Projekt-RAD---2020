module Tests

// stopwatch lib
open System.Diagnostics

// project code
open Hashing
open HashTable

let rnd = System.Random()
let randomUint64 () =
    let b : byte [] = Array.zeroCreate 8
    rnd.NextBytes ( b )
    Array.fold (fun acc elem -> (acc <<< 8) + uint64 elem) 0UL b

let rec randomUint89m1 () =
    let b : byte [] = Array.zeroCreate 12
    rnd.NextBytes ( b )
    b.[0] <- b.[0] &&& byte 1 // Discard all but the first bit
    let r = bigint b
    if r = (1I <<< 89) - 1I then randomUint89m1 () else r // Try again if we are unlucky. Really shouldn't happen



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
    let msh = multiplyShift a 20
    printfn "Multiply Shift"
    testRunTime n 20 msh

    let ma = randomUint89m1 ()
    let mb = randomUint89m1 ()
    let mmp = multiplyModPrime ma mb 20
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



