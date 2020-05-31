module CountSketch

let fourIndependent coef (x : uint64) =
    let p = (bigint 1 <<< 89) - bigint 1

    let rec eval coef x =
        match coef with 
        | c :: cs -> 
            c + x * (eval cs x)                
            |> fun x -> (x &&& p) + (x >>> 89)      // computing x mod p 
        | [] -> bigint 0

    eval coef (bigint x)
