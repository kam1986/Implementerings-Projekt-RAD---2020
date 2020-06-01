open System


// project code
open DataStream
open Hashing
open HashTable
open CountSketch

// webpage for generation of 89 bit random
// gets it as a string of 0's and 1's.
// handy if we need a new random byte sequence og 11 bytes



[<EntryPoint>]
let main argv =
    (*
    let a = [1 ; 2 ; 3 ; 4]

        printfn "%A" (fourIndependent (List.map (fun (x : int) -> bigint x) a) 2UL) 
    *)
    // set before run, should be altered befor real testing
    let n = 0
    let l = 0
    let a = (int64 << RandomBits) 64 ||| int64 1 // set the least significant bit to 1
    let hash x = multiplyShift a l x
    let tablesize = 0
    let table = init tablesize hash

    let stream = createStream n l

    let mean =
        Seq.fold (fun table x -> increment x 1 table) () table
    
   

    0 // return an integer exit code

