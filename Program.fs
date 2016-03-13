open System
open System.IO

let re = System.Text.RegularExpressions.Regex("\w+")
let twoWords str =
    seq {for m in re.Matches(str) -> m.Value}

let fileWords fileName =
    fileName
    |> File.ReadLines
    |> Seq.collect twoWords
    |> Seq.map (fun w -> w.ToLowerInvariant())
    |> Seq.filter (fun w -> w.IndexOfAny([|'a'..'z'|]) > -1)
    |> Seq.filter (fun w -> w.IndexOfAny([|'0'..'9'|]) > -1)
    |> Seq.filter (fun w -> w.IndexOfAny([|'_'|]) = -1)
   
let dirUniqueWords dirName =
    dirName
    |> Directory.EnumerateFiles
    |> Seq.collect fileWords
    |> Set.ofSeq

let bashrc = dirUniqueWords @"/home/jacqueline/Documents/bashrc"
let bashrc2 = dirUniqueWords @"/home/jacqueline/Documents/bashrc2"

//printfn "%A %A" bashrc bashrc2



[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    0 // return an integer exit code

