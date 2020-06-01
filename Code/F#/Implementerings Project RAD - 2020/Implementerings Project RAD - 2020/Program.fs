open System


// project code
open DataStream
open Hashing
open HashTable
open CountSketch
open Tests

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

    let a = randomUint64 () ||| 1UL // set the least significant bit to 1
    let hash x = multiplyShift a l x
    let tablesize = 0
    let table = init tablesize hash

    let stream = createStream n l

    let mean =
        Seq.fold (fun table (x,d) -> increment x d table) table stream


    0 // return an integer exit code

