// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp
(*

Steps

1. On launch, Fetch 10 pokemons and stop at max of 300. 
2. Persist the fetched pokemons.
3. Allow to search for pokemon by name or any other attributes like abilities. 
4. Search for pokemon and display it
5. List its name, number, an image of it, and some abilities. 
6. Add option to view all data about it.
7. There should also be a way to sort the Pokemon results by name or number (a-z, 1-N).

8. Make sure the interface is user friendly.

https://pokeapi.co/api/v2/pokemon/?offset=0&limit=20

- Concurrency
- Infinte scrolling

*)

open System
open System.Net
open System.Text.Json
open System.IO


type FetchRecord =
    {
        count: int
        next: string
        previous: string
        results: Pokemon[]
    }
and Pokemon =
    {
        name: string
        url: string
    }

let mutable pokemons: Pokemon array = Array.empty

let fetch (offset: int) =
    async {
        let url = $"https://pokeapi.co/api/v2/pokemon?offset={offset}&limit=10"
        use client = new System.Net.Http.HttpClient()
        
        let! response = client.GetAsync(url) |> Async.AwaitTask

        response.EnsureSuccessStatusCode() |> ignore

        return! response.Content.ReadAsStringAsync() |> Async.AwaitTask
    }


let cast (jsonString: String) =
    let record = JsonSerializer.Deserialize<FetchRecord> jsonString
    record.results


let loadFromFile =
    let path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
    let path = path + "/.pokezone"

    if File.Exists(path) then
        let storedJson = File.ReadAllText(path)
        JsonSerializer.Deserialize<Pokemon[]> storedJson
    else
        Array.empty


let storeToFile (pokemons: Pokemon[]) =
    let path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
    let path = path + "/.pokezone"

    let serializedRecord = JsonSerializer.Serialize<Pokemon[]> pokemons
    File.WriteAllText(path, serializedRecord)


let printAll (pokemons: Pokemon[]) =
    for pokemon in pokemons do
        printfn "%s" pokemon.name


let setup =
    // 1. Load data from file
    pokemons <- loadFromFile
    
    // 2. Check threshold and Get new records
    if pokemons.Length <= 300 then
        let newPokemons = fetch pokemons.Length |> Async.RunSynchronously |> cast
        pokemons <- Array.append pokemons newPokemons

    // 3. Store new records
    storeToFile pokemons    


let console =
    let mutable search = ""
    printfn "Console Mode"
    printfn "1. Search (s)"
    printfn "2. Order by Name (o)"
    printfn "3. Exit (q)"

    while true do
        printf "> " ;
        search <- Console.ReadLine()
        if search.Equals "s" || search.Equals "1"  then
            printf "Search keyword: "
            let keyword = Console.ReadLine()
            let filtered = pokemons |> Array.filter (fun t -> t.name.Contains keyword)
            printAll filtered
        elif search.Equals "o" || search.Equals "2" then
            pokemons <- pokemons |> Array.sortBy (fun t -> t.name)
            printAll pokemons
        elif search.Equals "q" || search.Equals "3" then
            storeToFile pokemons
            exit 0
        else
            printfn "[!] Invalid Command"



[<EntryPoint>]
let main _ =

    setup
    printfn "Pokemons present: %i" pokemons.Length
    console

    0 // return an integer exit code