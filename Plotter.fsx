module Plotter

#r "bin/debug/EDStarMap3DPlotter.exe"
#load "SampleInput.fsx"
#load "SystemsFinder.fsx"

open XPlot.Plotly
open XPlot.Plotly.Graph
open SystemsFinder

// Input
let graphs = Input.graphs
let route = Input.route

// Create the graphs
let graphsWithDetails =
    graphs
    |> Seq.map (fun (group, systemsInGroup, color) -> (group, systemsInGroup |> Seq.map (fun sysToFind -> findSystem sysToFind) |> Seq.choose id, color))

let getPlotForGroup key (values:seq<SystemInfo>) color = 
    let Xs = values |> Seq.map (fun s -> s.coords.x)
    let Ys = values |> Seq.map (fun s -> s.coords.y)
    let Zs = values |> Seq.map (fun s -> s.coords.z)
    let Texts = values |> Seq.map (fun s -> s.name)
    Scatter3d (
        x = Xs,
        y = Ys,
        z = Zs,
        text = Texts,
        mode = "markers",
        marker = Marker(size = 5., color = color, opacity = 0.8),
        name = key) 
    :> XPlot.Plotly.Graph.Trace

let starMapTraces = graphsWithDetails |> Seq.map (fun (k, g, c) -> getPlotForGroup k g c) |> Seq.toList

// Create the route
let routeSystems = route |> Seq.map (fun step -> findSystem step) |> Seq.choose id
let routeTrace =
    Scatter3d (
        x = (routeSystems |> Seq.map (fun s -> s.coords.x)),
        y = (routeSystems |> Seq.map (fun s -> s.coords.y)),
        z = (routeSystems |> Seq.map (fun s -> s.coords.z)),
        mode = "lines",
        marker = Marker(size = 5., color = "rgba(0,255,255,1.0)", opacity = 0.8), name = "Route")
    :> XPlot.Plotly.Graph.Trace

// Render
let layout =
    XPlot.Plotly.Graph.Layout
        (
         paper_bgcolor = "rgba(0,0,0,1.0)", plot_bgcolor = "rgba(0,0,0,1.0)",
         width = 1600., height = 800., showlegend = true, title = Input.title, 
         margin = Margin(b = 50., l = 50., pad = 0., r = 50., t = 50.),
         xaxis = Xaxis(showaxeslabels = true, title = "X", zeroline = true, zerolinecolor = "rgba(0,255,0,0.5)", linecolor="rgba(128,128,128,0.5)"),
         yaxis = Yaxis(showaxeslabels = true, title = "Y", zeroline = true,  zerolinecolor = "rgba(0,255,0,0.5)", linecolor="rgba(128,128,128,0.5)"))

let chart = PlotlyChart()
chart.Plot(routeTrace :: starMapTraces, layout)
chart |> Plotly.Show
