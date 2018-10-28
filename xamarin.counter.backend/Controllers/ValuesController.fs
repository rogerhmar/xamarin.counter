﻿namespace xamarin.counter.backend.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc

[<Route("api/[controller]")>]
[<ApiController>]
type ValuesController () =
    inherit ControllerBase()

    [<HttpGet("{id}")>]
    member this.Get(id:int) =
        let value = (id+1).ToString()
        ActionResult<string>(value)
