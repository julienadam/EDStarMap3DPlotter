// labels are not working, I think there's a bug in XPlot.Plotly, text should accept an array, but it expects a string only
// let labels = systemsAndColor |> Seq.map (fun s -> string s.Name) |> Seq.toArray
module Plotter

#r "packages/FSharp.data/lib/net40/FSharp.Data.dll"
#r "packages/XPlot.Plotly/lib/net45/Xplot.plotly.dll"
#r "packages/Newtonsoft.Json/lib/net45/Newtonsoft.Json.dll"

#load "coordinates.fsx"

open FSharp.Data
open XPlot.Plotly
open XPlot.Plotly.Graph
open Newtonsoft.Json

type Systems = JsonProvider< "Data/systems.json" >
type SystemInfo = Systems.Root
let allSystems = Systems.Load("Data/systems.json")

let indexedSystems = allSystems |> Seq.map (fun s -> s.Name, s) |> dict

// Input

let selectedSystems = 
    [
    "Shell", Coordinates.shell
    "Closed", ["Harma"; "Warkushanui"; "49 Arietis"; "64 Arietis"; "Arawere"; "Pic Tok"; "Rhea"]
    "Affected", ["Harma"; "Warkushanui"; "Varati"; "Ngobe"; "Iapodes"; "Sol"; "Diaguandri"; "Nganji"; "Peregrina"]
    "Source", ["Merope"]
    ]

let route = ["Sol"; "Merope"]

let colorMap input = 
    match input with
    | "Closed" -> "rgba(255, 0, 0, 1.0)"
    | "Affected" -> "rgba(255, 255, 0, 1.0)"
    | "Source" -> "rgba(0, 0, 255, 1.0)"
    | "Shell" -> "rgba(0, 255, 0, 1.0)"
    | _ -> failwith "unknown group"

type Point = { x : decimal; y : decimal; z : decimal }
type EdsmSystem = { name : string; coords: Point} // {"name":"Sol","coords":{"x":0,"y":0,"z":0}}

let edsmSearch sysName =
    let result = Http.Request ( sprintf "http://www.edsm.net/api-v1/system?sysname=%s&coords=1&known=1" (System.Uri.EscapeUriString(sysName)))
    match result.StatusCode with
    | 200 -> 
        match result.Body with
        | Text (bodyText) -> 
            match bodyText with
            | "-1" -> None
            | _ -> Some((JsonConvert.DeserializeObject<EdsmSystem> bodyText).coords)
        | _ -> None
    | _ -> None

let findSystemInLocalCache sysName = 
    if indexedSystems.ContainsKey sysName
    then 
        let sysFound = indexedSystems.[sysName]
        Some({x = sysFound.X; y = sysFound.Y; z = sysFound.Z })
    else
        None

let findSystem sysName =
    match findSystemInLocalCache sysName with
    | Some (coords) -> 
        printfn "Found system '%s' in local EDDB.io cache @ {x=%f, y=%f, z=%f" sysName coords.x coords.y coords.z
        Some(coords)
    | None -> 
        match edsmSearch sysName with
        | Some (coords) ->
            printfn "Found system '%s' with EDSM query @ {x=%f, y=%f, z=%f" sysName coords.x coords.y coords.z
            Some (coords)
        | None -> None

// Create the systems maps (one for each group, with one color per group)
let selectedSystemsWithDetails = 
    selectedSystems 
    |> Seq.map (fun (group, systemsInGroup) -> (group, systemsInGroup |> Seq.map (fun sysToFind -> findSystem sysToFind) |> Seq.choose id))

let getPlotForGroup key (values:seq<Point>) = 
    let Xs = values |> Seq.map (fun s -> s.x)
    let Ys = values |> Seq.map (fun s -> s.y)
    let Zs = values |> Seq.map (fun s -> s.z)
    Scatter3d (
        x = Xs, 
        y = Ys, 
        z = Zs, 
        mode = "markers", 
        marker = Marker(size = 5., color = colorMap key, opacity = 0.8),
        name = key) 
    :> XPlot.Plotly.Graph.Trace

let starMapTraces = selectedSystemsWithDetails |> Seq.map (fun (k, c) -> getPlotForGroup k c) |> Seq.toList

// Create the route trace
let routeSystems = 
    route
    |> Seq.map (fun sel -> allSystems |> Seq.find (fun sys -> sys.Name = sel))
    |> Seq.map (fun stuff -> { x = stuff.X; y = stuff.Y; z = stuff.Z})

let routeTrace = 
    Scatter3d (
        x = (routeSystems |> Seq.map (fun s -> s.x)), 
        y = (routeSystems |> Seq.map (fun s -> s.y)), 
        z = (routeSystems |> Seq.map (fun s -> s.z)), 
        mode = "lines", 
        marker = Marker(size = 5., color = "rgba(0,255,255,1.0)", opacity = 0.8), name = "Route") 
    :> XPlot.Plotly.Graph.Trace


// Render
let layout = 
    XPlot.Plotly.Graph.Layout
        (
         paper_bgcolor = "rgba(0,0,0,1.0)", plot_bgcolor = "rgba(0,0,0,1.0)",
         width = 1600., height = 800., showlegend = true, title = "Elite: Dangerous star map", 
         margin = Margin(b = 50., l = 50., pad = 0., r = 50., t = 50.),
         xaxis = Xaxis(showaxeslabels = true, title = "X", zeroline = true, zerolinecolor = "rgba(0,255,0,0.5)", linecolor="rgba(128,128,128,0.5)"),
         yaxis = Yaxis(showaxeslabels = true, title = "Y", zeroline = true,  zerolinecolor = "rgba(0,255,0,0.5)", linecolor="rgba(128,128,128,0.5)"))

let chart = PlotlyChart()
chart.Plot(routeTrace :: starMapTraces, layout)
chart |> Plotly.Show
