
#r "packages/FSharp.data/lib/net40/FSharp.Data.dll"
#r "packages/XPlot.Plotly/lib/net45/Xplot.plotly.dll"
#r "packages/Newtonsoft.Json/lib/net45/Newtonsoft.Json.dll"

open FSharp.Data
open XPlot.Plotly
open XPlot.Plotly.Graph
open Newtonsoft.Json

let plot = 
    Scatter3d (
        x = [0m; 100m; -100m], 
        y = [50m; 25m; -37m], 
        z = [-52m; 18m; 68m], 
        text = ["foo"; "bar"; "baz"], 
        mode = "lines", 
        marker = Marker(size = 5., color = "rgba(0,255,255,1.0)", opacity = 0.8), name = "Route") 
    :> XPlot.Plotly.Graph.Trace

// Render
let layout = 
    XPlot.Plotly.Graph.Layout
        (
         paper_bgcolor = "rgba(0,0,0,1.0)", plot_bgcolor = "rgba(0,0,0,1.0)",
         width = 1600., height = 800., showlegend = true, title = "Test scatter plot", 
         margin = Margin(b = 50., l = 50., pad = 0., r = 50., t = 50.),
         xaxis = Xaxis(showaxeslabels = true, title = "X", zeroline = true, zerolinecolor = "rgba(0,255,0,0.5)", linecolor="rgba(128,128,128,0.5)"),
         yaxis = Yaxis(showaxeslabels = true, title = "Y", zeroline = true,  zerolinecolor = "rgba(0,255,0,0.5)", linecolor="rgba(128,128,128,0.5)"))

let chart = PlotlyChart()
chart.Plot([plot], layout)
chart |> Plotly.Show

