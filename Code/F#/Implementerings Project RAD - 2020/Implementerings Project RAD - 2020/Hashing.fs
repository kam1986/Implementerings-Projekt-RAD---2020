module Hashing

open System.Net
open System.Diagnostics



// getting random bytes from  www.random.org/bytes given in the assignment.
let RandomBits bits =
    use wb = new WebClient() 
    let bytes = bits / 8 + 1           // computing number of bytes 
    let correction = bytes * 8 - bits  // computing number of bits we have to correct by right shifting 
    // getting random bytes
    wb.DownloadString(@"https://www.random.org/cgi-bin/randbyte?nbytes=" + string bytes + "&format=b")
    // filter noise
    |> fun str -> Array.filter (fun c -> c = '1' || c = '0' ) [| for c in str -> c |]
    // casting char to byte for every char in the array
    |> Array.map byte 
    // converting to bigint
    |> bigint
    // correcting number of bits
    |> fun bint -> bint >>> correction



// ----------------------------------- part 1 -------------------------------------- //

// Del 1 (a)
// The inline force generic types on a and x, hence
// makes 
let inline multiplyShift (a, l) x = (a * x) >>> (64 - l)

// Del 1 (b)
// here we use the result of assignment 3  exercise 2.7 
// where we rewrite x to a more suitable size
let multiplyModPrime (a, b, l) (x : uint64) =
    let w = bigint 1 <<< 89
    let p = w - bigint 1

    bigint x 
    |> fun x -> (a * x + b)                 
    |> fun x -> (x &&& p) + (x >>> 89)      // computing x mod p 
    |> fun y -> if y < p then y else y - p  // correcting depending on result
    |> fun r -> (bigint ((1 <<< l) - 1)) &&& r
    |> uint64


// ----------------------------------- Part 2 ---------------------------------------- //

