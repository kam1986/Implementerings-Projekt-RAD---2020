open System


// project code
open DataStream
open Hashing
open HashTable
open CountSketch
open Tests
open RandomTool
// webpage for generation of 89 bit random
// gets it as a string of 0's and 1's.
// handy if we need a new random byte sequence og 11 bytes

[<EntryPoint>]
let main argv =
    // the last tuple are the argument (minus x) given to the hashfunciton and the form depend on the function.
    presentExperiment 10000 15 10
    0 // return an integer exit code

