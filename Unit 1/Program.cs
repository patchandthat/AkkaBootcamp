using System;
﻿using Akka.Actor;

namespace WinTail
{
    #region Program
    class Program
    {
        public static ActorSystem MyActorSystem;

        static void Main(string[] args)
        {
            MyActorSystem = ActorSystem.Create("MyActorSystem");

            IActorRef consoleWriterActor = MyActorSystem.ActorOf(Props.Create(() => new ConsoleWriterActor()), "ConsoleWriter");

            IActorRef tailCoordinatorActor = MyActorSystem.ActorOf(Props.Create(() => new TailCoordinatorActor()), "TailCoordinatorActor");
           
            IActorRef validationActor = MyActorSystem.ActorOf(Props.Create(() => new FileValidationActor(consoleWriterActor)), "ValidationActor");

            IActorRef consoleReaderActor = MyActorSystem.ActorOf(Props.Create(() => new ConsoleReaderActor()), "ConsoleReader");
            
            consoleReaderActor.Tell(ConsoleReaderActor.StartCommand);

            MyActorSystem.AwaitTermination();
        }
    }
    #endregion
}
