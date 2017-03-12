open System
open Akka.FSharp
open Akka.FSharp.Spawn
open Akka.Actor
open WinTail

[<EntryPoint>]
let main _ =
    // initialize an actor system
    let myActorSystem =
        System.create "MyActorSystem" (Configuration.parse "akka.suppress-json-serializer-warning = on")

    // make your first actors using the 'spawn' function
    let consoleWriterActor = spawn myActorSystem "consoleWriterActor" <| actorOf (Actors.consoleWriterActor)
    let consoleReaderActor =
        spawn myActorSystem "consoleReaderActor" <| actorOf2 (Actors.consoleReaderActor consoleWriterActor)

    // tell the consoleReader actor to begin
    consoleReaderActor <! Messages.Start

    myActorSystem.WhenTerminated.Wait()
    0 // return an integer exit code
