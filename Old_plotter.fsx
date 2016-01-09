
#r "packages/FSharp.Data/lib/net40/FSharp.Data.dll"
#r "packages/"

open FSharp.Data
open System.Linq

type Systems = JsonProvider<"./systems.json">
let systems = Systems.Load("./systems.json")

type Input = CsvProvider<"./input.csv">
let input = Input.Load("./input.csv")

let selectedSystemsAndState = input.Rows |> Seq.map (fun i -> (i.System, i.State))
let enriched = selectedSystemsAndState |> Seq.map (fun sel -> systems |> Seq.find (fun sys -> sys.Name = fst sel)) 
let enrichedCoords = enriched |> Seq.map (fun s -> (s.X, s.Y, s.Z))  
let Xs = enriched |> Seq.map (fun s -> double s.X)
let Ys = enriched |> Seq.map (fun s -> double s.Y)
let Zs = enriched |> Seq.map (fun s -> double s.Z)

let prms = new System.Collections.Generic.Dictionary<string, obj>()
prms.["x"] <- Xs
prms.["y"] <- Ys
prms.["z"] <- Zs
prms.["xlab"] <- "X"
prms.["ylab"] <- "Y"
prms.["zlab"] <- "Z"
prms.["highlight.3d"] <- true

// R.scatterplot3d prms

