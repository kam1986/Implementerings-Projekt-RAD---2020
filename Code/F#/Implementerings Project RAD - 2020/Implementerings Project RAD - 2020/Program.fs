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
    
    let ret = TestHashtable 10000000 20 multiplyShift (523432UL, 20)
    printfn "%d" ret

    0 // return an integer exit code

