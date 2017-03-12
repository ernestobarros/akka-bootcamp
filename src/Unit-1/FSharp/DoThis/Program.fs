﻿open System
open Akka.FSharp
open Akka.FSharp.Spawn
open Akka.Actor
open WinTail

let printInstructions() = 
    Console.WriteLine "Write whatever you want into the console!"
    Console.Write "Some lines will appear as"
    Console.ForegroundColor <- ConsoleColor.Red
    Console.Write " red"
    Console.ResetColor()
    Console.Write " and others will appear as"
    Console.ForegroundColor <- ConsoleColor.Green
    Console.Write " green! "
    Console.ResetColor()
    Console.WriteLine()
    Console.WriteLine()
    Console.WriteLine "Type 'exit' to quit this application at any time.\n"

[<EntryPoint>]
let main _ = 
    // initialize an actor system
    // YOU NEED TO FILL IN HERE
    let myActorSystem = 
        System.create "MyActorSystem" (Configuration.parse "akka.suppress-json-serializer-warning = on")

    printInstructions()

    // make your first actors using the 'spawn' function
    // YOU NEED TO FILL IN HERE
    let consoleWriterActor = spawn myActorSystem "consoleWriterActor" <| actorOf (Actors.consoleWriterActor)
    let consoleReaderActor = 
        spawn myActorSystem "consoleReaderActor" <| actorOf2 (Actors.consoleReaderActor consoleWriterActor)

    // tell the consoleReader actor to begin
    // YOU NEED TO FILL IN HERE
    consoleReaderActor <! Actors.Start
    myActorSystem.WhenTerminated.Wait()
    0 // return an integer exit code
