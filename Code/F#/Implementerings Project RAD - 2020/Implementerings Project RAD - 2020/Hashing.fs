module Hashing


// ----------------------------------- part 1.1 ------------------------------------ //


// Del 1 (a)
// The inline force generic types on a and x, hence
// makes 
let multiplyShift a l x = (a * x) >>> (64 - l)

// Del 1 (b)
// here we use the result of assignment 3  exercise 2.7 
// where we rewrite x to a more suitable size
let multiplyModPrime a b l (x : uint64) =
    let m = bigint 1 <<< 20
    let w = bigint 1 <<< 89
    let p = w - bigint 1

    let r = 
        bigint x 
        |> fun x -> (a * x + b)                 
        |> fun x -> (x &&& p) + (x >>> 89)      // computing x mod p 
        |> fun y -> if y < p then y else y - p  // correcting depending on result

    let modl = (bigint ((1 <<< l) - 1)) &&& r
    uint64 modl

let randomUint64 (rnd : System.Random) =
    let b : byte [] = Array.zeroCreate 8
    rnd.NextBytes ( b )
    Array.fold (fun acc elem -> (acc <<< 8) + uint64 elem) 0UL b

let rec randomUint89m1 (rnd : System.Random) =
    let b : byte [] = Array.zeroCreate 12
    rnd.NextBytes ( b )
    b.[0] <- b.[0] &&& byte 1 // Discard all but the first bit
    let r = bigint b
    if r = (bigint 2 <<< 89) - (bigint 1) then randomUint89m1 rnd else r // Try again if we are unlucky. Really shouldn't happen

// Del 1 (c)
let testRunTime n l (h : (uint64 -> uint64)) = 
    let stream = DataStream.createStream n l
    let stopWatch = System.Diagnostics.Stopwatch.StartNew()
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