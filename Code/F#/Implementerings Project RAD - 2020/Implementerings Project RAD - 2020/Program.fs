open System
open System.Net

// project code
open DataStream
open Hashing
open HashTable
open CountSketch

// webpage for generation of 89 bit random
// gets it as a string of 0's and 1's.
// handy if we need a new random byte sequence og 11 bytes

// getting random bytes from  www.random.org/bytes given in the assignment.
let RandomBits bits =
    use wb = new WebClient() 
    let bytes = bits / 8 + 1           // computing number of bytes 
    let correction = bytes * 8 - bits  // computing number of bits we have to correct by right shifting 
    wb.DownloadString(@"https://www.random.org/cgi-bin/randbyte?nbytes=" + string bytes + "&format=b")
    |> fun str -> Array.filter (fun c -> c = '1' || c = '0' ) [| for c in str -> c |]
    |> Array.map byte
    |> bigint
    |> fun bint -> bint >>> correction

[<EntryPoint>]
let main argv =
    let a = [1 ; 2 ; 3 ; 4]

    printfn "%A" (fourIndependent (List.map (fun (x : int) -> bigint x) a) 2UL) 
    0 // return an integer exit code

