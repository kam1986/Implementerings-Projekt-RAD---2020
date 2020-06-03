module RandomTool

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
