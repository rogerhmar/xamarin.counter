namespace xamarin.counter.backend

open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.Logging

module Program =
    open System.Diagnostics

    let exitCode = 0
    let pathToExe = Process.GetCurrentProcess().MainModule.FileName
    let pathToContentRoot = Path.GetDirectoryName(pathToExe)

    let CreateWebHostBuilder args =
        WebHost
            .CreateDefaultBuilder(args)
            .UseContentRoot(pathToContentRoot)
            .UseStartup<Startup>()

    [<EntryPoint>]
    let main args =
        CreateWebHostBuilder(args).Build().Run()

        exitCode
