#r @".env\packages\FAKE\tools\FakeLib.dll"
#load @".env\CsbScaffold.fsx"

open Fake
open FSharpAux

let source = __SOURCE_DIRECTORY__

let git = Fake.Tools.Git.CommandHelper.runGitCommand source

///create new working branch "projects" with no history
git "checkout --orphan projects"

///update .gitignore so it only tracks projects
let replaceF text = 
    String.replace      "/* "               ""      text
    |> String.replace   "!.env"            ".env"
    |> String.replace   "!projectName"    "projectName/"
    |> String.replace   ".env/packages/"    ""
    |> String.replace   "!init.cmd"         "init.cmd" 
    |> String.replace   "!init.fsx"         "init.fsx" 
Fake.IO.File.applyReplace replaceF (source + @"\.gitignore")

///commiting made changes
git "rm -r --cached .env"
git "commit -a -m \"Initial Commit\""