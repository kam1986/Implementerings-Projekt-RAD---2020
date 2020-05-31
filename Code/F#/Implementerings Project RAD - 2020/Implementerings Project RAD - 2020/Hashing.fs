module Hashing

open System.Net
open System.Diagnostics



// getting random bytes from  www.random.org/bytes given in the assignment.
let RandomBits bits =
    use wb = new WebClient() 
    let bytes = bits / 8 + 1           // computing number of bytes 
    let correction = bytes * 8 - bits  // computing number of bits we have to correct by right shifting 
    wb.DownloadString(@"https://www.random.org/cgi-bin/randbyte?nbytes=" + string bytes + "&format=b")
    |> fun str -> Array.filter (fun c -> c = '1' || c = '0' ) [| for c in str -> c |]
    |> Array.map byte
    |> bigint
    |> fun bint -> bint >>> correction



// ----------------------------------- part 1.1 ------------------------------------ //

// Del 1 (a)
// The inline force generic types on a and x, hence
// makes 
let inline multiplyShift a l x = (a * x) >>> (64 - l)

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
    if r = (bigint 1 <<< 89) - (bigint 1) then randomUint89m1 rnd else r // Try again if we are unlucky. Really shouldn't happen

