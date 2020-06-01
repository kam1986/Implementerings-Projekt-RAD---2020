module CountSketch

open Hashing


// Opgave 4
let fourIndependent coef (x : int64) =
    let p = (bigint 1 <<< 89) - bigint 1

    let rec eval coef x =
        match coef with 
        | c :: cs -> 
            c + x * (eval cs x)                
            |> fun x -> (x &&& p) + (x >>> 89)      // computing x mod p 
        | [] -> bigint 0

    eval coef (bigint x)



// Opgave 5
let CountSketchHash t x =
    let m = bigint 1 <<< t
    // RandomBits 88 can return 2^89-1 as result hence we subtract 1 to eliminate this. 
    let coef = [ for c in 0 .. 3 -> RandomBits 88 - bigint 1 ] 
    let g = fourIndependent coef x
    let s = bigint 1 - bigint 2 * (g >>> 89)
    let h =
        (g &&& m) + (g >>> t)
        |> fun y -> if y < m then y else y - m
    (h, s)


// Opgave 6
// Seq takes any Ienumerable i.e. list, arrays, sets with many more.
let inline S C = Seq.sumBy (fun cy -> cy * cy) C
