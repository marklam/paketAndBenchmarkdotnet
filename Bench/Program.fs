open System
open System.Security.Cryptography
open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Jobs
open BenchmarkDotNet.Running

[<SimpleJob(RuntimeMoniker.Net60, baseline = true)>]
type Md5VsSha256() =
    let sha256 = SHA256.Create()
    let md5 = MD5.Create()
    let mutable data = Array.empty<byte>

    [<Params(1000, 10000)>]
    [<DefaultValue>]
    val mutable N : int

    [<GlobalSetup>]
    member this.Setup() =
        data <- Array.zeroCreate this.N
        Random(42).NextBytes(data)

    [<Benchmark>]
    member _.Sha256() =
        sha256.ComputeHash(data)

    [<Benchmark>]
    member _.Md5() =
        md5.ComputeHash(data)

[<EntryPoint>]
let main _argv =
    BenchmarkRunner.Run<Md5VsSha256>() |> ignore
    0