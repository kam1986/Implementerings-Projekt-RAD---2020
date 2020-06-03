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

    let n = 500000
    let l = 27
    //TestHashtable n |> ignore
    let ms = [9;18;27]
    let stream = DataStream.createStream n l
    List.iter (presentExperiment stream) ms
    //presentExperiment stream 6
    0 // return an integer exit code

