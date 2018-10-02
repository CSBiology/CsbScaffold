#r @".env\packages\FAKE\tools\FakeLib.dll"
#load @".env\CsbScaffold.fsx"

open Fake
open FSharpAux


try 

let source = __SOURCE_DIRECTORY__

let git = Fake.Tools.Git.CommandHelper.runGitCommand source
    
let replaceF text = 
    String.replace      "/* "               ""      text
    |> String.replace   "!.env"            ".env"
    |> String.replace   "!projectName"    "projectName/"
    |> String.replace   ".env/packages/"    ""
    |> String.replace   "!init.cmd"         "init.cmd" 
    |> String.replace   "!init.fsx"         "init.fsx" 

let branches = Fake.Tools.Git.Branches.getLocalBranches source

if not (List.contains "projects" branches) then       
    ///create new working branch "projects" with no history
    git "checkout --orphan projects" |> ignore
    ///update .gitignore so it only tracks projects
    Fake.IO.File.applyReplace replaceF (source + @"\.gitignore")
    ///commiting made changes
    git "rm -r --cached .env" |> ignore
    git "commit -a -m \"Initial Commit\"" |> ignore

with 
| err -> printfn "%s" err.Message