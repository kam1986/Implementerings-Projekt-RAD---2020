module CountSketch

open Hashing
open RandomTool
// Opgave 4
let fourIndependent coef (x : uint64) =
    let p = (1I <<< 89) - 1I

    let rec eval coef x =
        match coef with 
        | c :: cs -> 
            c + x * (eval cs x)              
            |> fun x -> (x &&& p) + (x >>> 89)      // computing x mod p 
        | [] -> 0I

    let r = eval coef (bigint x)
    if r >= p then r - p else r



// Opgave 5
let CSHash (g : (uint64 -> bigint)) t x =
    let m = 1I <<< t 
    let gx = g x
    let s = 1 - 2 * int (gx >>> 88)
    let h = int (gx &&& (m - 1I))
    (h, s)

let getCountSketchHash t =
    let coef = [ for c in 0 .. 3 -> randomUint89m1 () ] 
    let g = fourIndependent coef
    CSHash g t 
    

// Opgave 6
// 
let CountSketch m (g : uint64 -> (int*int)) (stream : seq<uint64*int>) =
    let C = Array.zeroCreate (1 <<< m)
    let evaluate (x,d) =
        let (h,s) = g x
        C.[h] <- C.[h] + s * d
    Seq.iter evaluate stream
    Array.fold (fun acc elem -> acc + elem * elem) 0 C