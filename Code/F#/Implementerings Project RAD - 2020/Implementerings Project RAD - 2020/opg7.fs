open System
open FSharp.Plotly	//for plotting the estimates X

// project code
open DataStream
open Hashing
open HashTable
open CountSketch
open Tests

// n: number of tokens in the stream 
// l: the interval of values that elements of the stream can take.
// m: an exponent of 2
let experiments (n : int) l (m : int) =
	let sigma = DataStream.createStream n l

	let X = []*100
	let randomNumbers = [0, 0, 0, 0]
	let CSsigma = 0
	for i in 1 .. 100:
		randomNumbers = [0, 0, 0, 0] // generate four random numbers here
		CSsigma = CountSketch.CountSketch m CountSketch.fourIndependent sigma
		let X[i] = Tests.S sigma l CountSketch.CountSketch randomNumbers
	let retval = Array.sort X
	retval

let presentExperiment (n : int) l (m : int) =
	X = experiments n l m
	for i in 0 .. 100-1:
		xAxis[i] = i
	
	S = 0 // calculate S here
	
	let diagram =
		[
			Chart.Point(xAxis,X,Name="Estimates")
			|> Chart.withgAxisAnchor(Y=1)
			Chart.Line(xAxis, [S], Name="True S value")
			|> Chart.withAxisAnchor(Y=2);
		]
		|> Chart.Combine
	diagram |> Chart.Show

let MSE S (n : int) l (m : int)=
	X = experiments n l m
	let acc = 0
	for i in 1 .. 100:
		acc += (X[i]-S)^2
	acc=acc/100
	