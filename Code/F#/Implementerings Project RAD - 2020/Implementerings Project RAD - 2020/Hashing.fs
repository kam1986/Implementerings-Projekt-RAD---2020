module Hashing


// ----------------------------------- part 1.1 ------------------------------------ //


// Del 1 (a)
// The inline force generic types on a and x, hence
// makes 
let inline multiplyShift a l x = (a * x) >>> (64 - l)

// Del 1 (b)
// here we use the result of assignment 3  exercise 2.7 
// where we rewrite x to a more suitable size
let inline multiplyModPrime a b l (x : int64) =
    let m = bigint 2 <<< 20
    let w = bigint 2 <<< 89
    let p = w - bigint 1
    
    bigint x
    |> fun x -> (a * x + b)                 
    |> fun x -> (x &&& p) + (x >>> 89)      // computing x mod p 
    |> fun y -> if y < p then y else y - p  // correcting depending on result


