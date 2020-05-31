module Tests

// stopwatch lib
open System.Diagnostics

// project code
open Hashing


// Del 1 (c)
let testRunTime n l (h : (uint64 -> uint64)) = 
    let stream = DataStream.createStream n l
    let stopWatch = Stopwatch.StartNew()
    let multShiftSum = Seq.fold (+) 0UL (Seq.map (fun (x,s) -> h x) stream)
    stopWatch.Stop()
    printfn "sum: %A time: %A" multShiftSum stopWatch.Elapsed.TotalMilliseconds


let test () =
    let rnd = System.Random()

    let a = randomUint64 rnd
    let n = 1000000
    let msh = multiplyShift a 20
    printfn "Multiply Shift"
    testRunTime n 20 msh

    let ma = randomUint89m1 rnd
    let mb = randomUint89m1 rnd
    let mmp = multiplyModPrime ma mb 20
    printfn "Multiply Mod Prime"
    testRunTime n 20 mmp

