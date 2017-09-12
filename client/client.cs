
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.

using System;
using System.ServiceModel;

namespace ServiceMultiGame
{

    public class CallbackHandler : IMultiplayerCallback
    {
     
        public void ResultScore(double result, string token)
        {
            Console.WriteLine("Result({0} toke {1})", result, token);
        }

        public void ResultMsg(string eqn)
        {
            Console.WriteLine("MSJ({0})", eqn);
        }

        public void GetPosition(double result)
        {
            Console.WriteLine("Result({0})", result);
        }

        public void KeyUp(string token)
        {
            Console.WriteLine("Tecla arriba " +"usuario "+ token);
        }

        public void KeyDown(string token)
        {
            Console.WriteLine("Tecla Abajo " + "usuario " + token);
        }
    }


    class Client
    {
        static void Main()
        {
            // Construct InstanceContext to handle messages on callback interface
            InstanceContext instanceContext = new InstanceContext(new CallbackHandler());

            // Create a client
            ServiceMultiplayerClient client = new ServiceMultiplayerClient(instanceContext);

            Console.WriteLine("Press <ENTER> to terminate client once the output is displayed.");
            Console.WriteLine();

            // Call the AddTo service operation.

            client.Register();


            string  player1=client.RegisterUser("Bruno");
            
            
            string room=client.GetRoom();
            if (room == "")
            {
                room=client.CreateRoom("banana");
            }

            client.PlayGame(player1, room);

            //string player2Token= client.RegisterUser("Eduardo");

            //client.PlayGame(player2Token, room);


            //client.KeyUp(player2Token);

            // string room = client.GetRoom();

            Console.WriteLine("room" +room);


            
            

            
            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey();
                if (keyinfo.Key == ConsoleKey.UpArrow)
                {
                    client.KeyUp(player1);
                }

                if (keyinfo.Key == ConsoleKey.DownArrow)
                {
                    client.KeyDown(player1);
                }

                if (keyinfo.Key == ConsoleKey.A)
                {
                    client.AddScore(5, player1);
                }

                if (keyinfo.Key == ConsoleKey.R)
                {
                    client.RemoveScore(5, player1);
                }
            }
            while (keyinfo.Key != ConsoleKey.Escape);

            Console.ReadLine();

            //Closing the client gracefully closes the connection and cleans up resources
            client.Close();
        }
    }


    }

