module HashTable


// A hash table which is a table of list containing tuples of key, value pairs
// and a hash function
type HashTable = Table of (uint64 * int) list array * (uint64 -> uint64)


// init table of size size and with hash function h. 
let init size h = 
    let table = [| for i in 1 .. int (2. ** float size) -> [] |]
    Table (table, h)

let get x (Table (table, h)) =
    let rec find x lst =
        match lst with
        | [] -> 0
        | (key, d) :: xs when x = key -> d
        | _ :: xs -> find x xs
    // get x from the 'h x' list of the table
    find x table.[int (h x)]


let set x v ((Table (table, h)) as tb) =
    // function which traversale the a list and update the tuple if it exists
    let rec update x v lst =
        match lst with
        | [] -> [x, v] // add the new tuple (x,v) to the end of the list
        | (key, _) :: table when key = x -> (key, v) :: table
        | hash :: table -> hash :: update x v table
    
    h x    // finding the right list index    
    |> fun index -> table.[int index] <- update x v table.[int index] // updating the list
    tb // returning the table


let increment x d (Table (table, h) as tb) =
    // function which traversale the list and increment the value corresponding 
    // to the key x with d
    let rec update x v lst =
        match lst with
        | [] -> [x, v] // add the new tuple (x,v) to the end of the list
        | (key, v') :: table when key = x -> (key, v + v') :: table
        | hash :: table -> hash :: update x v table

    h x    // finding the right list index    
    |> fun index -> table.[int index] <- update x d table.[int index] // updating the list
    tb // returning the table

