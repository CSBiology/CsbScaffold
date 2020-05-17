// --------------------------------------------------------------------------------------
// FAKE build script
// --------------------------------------------------------------------------------------

#r "paket:
nuget BlackFox.Fake.BuildTask
nuget Fake.Core.Target
nuget Fake.Core.Process
nuget Fake.Core.ReleaseNotes
nuget Fake.IO.FileSystem
nuget Fake.DotNet.Cli
nuget Fake.DotNet.MSBuild
nuget Fake.DotNet.AssemblyInfoFile
nuget Fake.DotNet.Paket
nuget Fake.DotNet.FSFormatting
nuget Fake.DotNet.Fsi
nuget Fake.DotNet.NuGet
nuget Fake.Api.Github
nuget Fake.DotNet.Testing.Expecto //"

#load ".fake/build.fsx/intellisense.fsx"

open BlackFox.Fake
open System.IO
open Fake.Core
open Fake.Core.TargetOperators
open Fake.DotNet
open Fake.IO
open Fake.IO.FileSystemOperators
open Fake.IO.Globbing
open Fake.IO.Globbing.Operators
open Fake.DotNet.Testing
open Fake.Tools
open Fake.Api
open Fake.Tools.Git

Target.initEnvironment ()

[<AutoOpen>]
module Templates =
    
    type ProjectTemplate =
        | CSB
        | DataScience
        | Cloud
        | All

        static member toString = function
            | CSB           ->  "CSB"
            | DataScience   ->  "DataScience"
            | Cloud         ->  "Cloud"
            | All           ->  "All"

        static member ofString = function
            | "CSB"         ->  CSB        
            | "DataScience" ->  DataScience
            | "Cloud"       ->  Cloud      
            | "All"         ->  All        
            | other         ->  failwithf "project template named %s does not exist" other

        static member getLoadScriptPath = function 
            | CSB           ->  [sprintf "#load \"%s\" " ("../../../.paket/load/netstandard2.0/CSB/csb.group.fsx"                  )]
            | DataScience   ->  [sprintf "#load \"%s\" " ("../../../.paket/load/netstandard2.0/DataScience/datascience.group.fsx"  )]
            | Cloud         ->  [sprintf "#load \"%s\" " ("../../../.paket/load/netstandard2.0/Cloud/cloud.group.fsx"    )          ]
            | All           ->  [CSB;DataScience;Cloud] |> List.map ProjectTemplate.getLoadScriptPath |> List.concat                

[<AutoOpen>]
module MessagePrompts =

    let prompt (msg:string) =
        System.Console.WriteLine(msg)
        System.Console.ReadLine().Trim()
        |> function | "" -> None | s -> Some s
        |> Option.map (fun s -> s.Replace ("\"","\\\""))

    let rec promptYesNo msg =
        match prompt (sprintf "%s [Yn]: " msg) with
        | Some "Y" | Some "y" -> true
        | Some "N" | Some "n" -> false
        | _ -> System.Console.WriteLine("Sorry, invalid answer"); promptYesNo msg

    let rec inputFromSelection (msg:string) (allowed:string list) (defaultValue:string) =
        let input = defaultArg (prompt (sprintf "%s [for default:(%s) leave blank]" msg defaultValue)) defaultValue
        let inputSplit =
            input
            |> String.split ','
            |> List.map String.trim
        match inputSplit |> List.tryFind (fun template -> not (List.contains template allowed)) with
        |Some notAllowed -> 
            if promptYesNo (sprintf "%s is no allowed value. Re-enter templates? (y/n)" notAllowed) then
                inputFromSelection msg allowed defaultValue
            else failwith "aborted"
        |None -> inputSplit

let createLoadScript (group: string) =
    let result = 
        Fake.DotNet.DotNet.exec
            (fun p -> { p with WorkingDirectory = __SOURCE_DIRECTORY__ })
            "paket"
            "generate-load-scripts --framework netstandard2.0 --type fsx"
    if not result.OK then 
        failwithf "error generating loadScript for group %s" group 

let createMainLoadScript () =
    createLoadScript "main"

let createLoadScripts (templates:ProjectTemplate list) =
    templates
    |> List.iter (ProjectTemplate.toString >> createLoadScript)



let createLoadScriptRefs (templates:ProjectTemplate list) =

    let mainLoadScript = 
            sprintf "#load \"%s\" " ("../../../.paket/load/netstandard2.0/main.group.fsx"                     )

    templates
    |> List.map ProjectTemplate.getLoadScriptPath
    |> List.concat
    |> fun deps -> mainLoadScript::deps

let createProject =
    BuildTask.create "createProject" [] {
        let projectName =
            defaultArg (prompt "insert name of the new project") "New_Project"

        let templates = 
            inputFromSelection "Choose template [DataScience; CSB; Cloud; All] or provide a comma-separated list of templates."  ["DataScience"; "CSB"; "Cloud"; "All"] "CSB"
            |> List.map ProjectTemplate.ofString

        let folderPath = __SOURCE_DIRECTORY__ @@ "/projects" @@ projectName

        let findFittingName path =
            let rec loop index folder =
                if Directory.Exists(folder) then
                    loop (index+1) (sprintf "%s_%i" folder index)
                else
                    folder
            loop 1 path

        let correctFolder = findFittingName folderPath

        let subs = 
            ["/data";"/scripts";"/results";"/documents";"/presentations"]
            |> List.map (fun sub -> correctFolder @@ sub)

        correctFolder :: subs
        |> List.iter Directory.ensure

        createMainLoadScript ()
        templates |> createLoadScripts

        File.WriteAllLines(
            (correctFolder @@ "/scripts/" @@ "starterScript.fsx" ),
            (createLoadScriptRefs templates)
        )

    }

BuildTask.runOrDefaultWithArguments createProject