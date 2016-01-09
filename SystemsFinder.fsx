module SystemsFinder

#r "packages/Newtonsoft.Json/lib/net45/Newtonsoft.Json.dll"
#r "packages/FSharp.data/lib/net40/FSharp.Data.dll"

open Newtonsoft.Json
open FSharp.Data

type Systems = JsonProvider<"paket-files/eddb.io/Data/systems.json">
type EddbSystemInfo = Systems.Root
let allSystems = Systems.Load("paket-files/eddb.io/Data/systems.json")
let indexedSystems = allSystems |> Seq.map (fun s -> s.Name, s) |> dict

type Point = { x : decimal; y : decimal; z : decimal }
type SystemInfo = { name : string; coords: Point}

let edsmSearch sysName =
    let result = Http.Request ( sprintf "http://www.edsm.net/api-v1/system?sysname=%s&coords=1&known=1" (System.Uri.EscapeUriString(sysName)))
    match result.StatusCode with
    | 200 -> 
        match result.Body with
        | Text (bodyText) -> 
            match bodyText with
            | "-1" -> None
            | _ -> Some((JsonConvert.DeserializeObject<SystemInfo> bodyText).coords)
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
        printfn "Found system '%s' in local EDDB.io cache @ (x=%f, y=%f, z=%f)" sysName coords.x coords.y coords.z
        Some({ name = sysName; coords = coords})
    | None -> 
        match edsmSearch sysName with
        | Some (coords) ->
            printfn "Found system '%s' with EDSM query @ (x=%f, y=%f, z=%f)" sysName coords.x coords.y coords.z
            Some ({ name = sysName; coords = coords})
        | None -> None