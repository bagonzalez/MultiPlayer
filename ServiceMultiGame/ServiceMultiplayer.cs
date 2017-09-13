using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;

namespace ServiceMultiGame
{

    [ServiceContract(Namespace = "http://ServiceMultiGame", SessionMode = SessionMode.Required,
                    CallbackContract = typeof(IMultiplayerCallback))]
    public interface IMultiplayer
    {
        [OperationContract(IsOneWay = false)]
        string RegisterUser(string user);
        [OperationContract(IsOneWay = false)]
        string CreateRoom(string room);
        [OperationContract(IsOneWay = true)]
        void AddScore(double n, string token);
        [OperationContract(IsOneWay = true)]
        void RemoveScore(double n, string token);
        [OperationContract(IsOneWay = true)]
        void GetScore(string token);
        [OperationContract(IsOneWay = true)]
        void PlayGame(string token, string room);
        [OperationContract(IsOneWay = false)]
        string GetRoom();
        [OperationContract(IsOneWay = true)]
        void KeyUp(string token);
        [OperationContract(IsOneWay = true)]
        void KeyDown(string token);
        [OperationContract(IsOneWay = true)]
        void Register();

    }

    public interface IMultiplayerCallback
    {
        [OperationContract(IsOneWay = true)]
        void ResultScore(double result, string token);
        [OperationContract(IsOneWay = true)]
        void ResultMsg(string eqn);
        [OperationContract(IsOneWay = true)]
        void GetPosition(double result);
        [OperationContract(IsOneWay = true)]
        void KeyUp(string token);
        [OperationContract(IsOneWay = true)]
        void KeyDown(string token);
    }


    public class ServiceMultiplayer : IMultiplayer
    {
        private static List<Jugador> listJugadores = new List<Jugador>();
        private static List<Room> listRoom = new List<Room>();
        private static List<IMultiplayerCallback> clientList = new List<IMultiplayerCallback>();


        ServiceMultiplayer()
        {
            string fileName = @"C:\temp\Jugadores.xml";

            /*if (File.Exists(fileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Jugador>));
                using (FileStream stream = File.OpenRead(fileName))
                {
                    List<Jugador> dezerializedList = (List<Jugador>)serializer.Deserialize(stream);
                    ServiceMultiplayer.listJugadores = dezerializedList;
                }
            }*/            
        }

        public void AddScore(double n, string token)
        {
            foreach (Jugador jugador in ServiceMultiplayer.listJugadores)
            {
                if (jugador.id == token)
                {
                    jugador.score += n;
                    MSGScoreAllClient(jugador.score, token);
                }
            }
        }

        public string CreateRoom(string room)
        {
            Room newRoom = new Room();

            Random rnd = new Random();
            string posibles = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            int longitud = posibles.Length;
            char letra;
            int longitudnuevacadena = 5;
            string nuevacadena = "";
            for (int i = 0; i < longitudnuevacadena; i++)
            {
                letra = posibles[rnd.Next(longitud)];
                nuevacadena += letra.ToString();
            }

            newRoom.id = nuevacadena;
            newRoom.name = room;
            newRoom.player1 = "";
            newRoom.player2 = "";
            newRoom.gameStart = false;

            ServiceMultiplayer.listRoom.Add(newRoom);
            Callback.ResultMsg("Room creado: " + newRoom.name);

            return newRoom.id;
        }

        public void GetScore(string token)
        {
            foreach (Jugador jugador in ServiceMultiplayer.listJugadores)
            {
                if (jugador.id == token)
                {                    
                    Callback.ResultScore(jugador.score, token);
                }
            }
        }

        public string GetJugador(string name)
        {
            foreach (Jugador jugador in ServiceMultiplayer.listJugadores)
            {
                if (jugador.name == name)
                {
                    return jugador.name;
                }
            }

            return null;
        }

        public void PlayGame(string token, string room)
        {
            foreach (Room roomPlay in ServiceMultiplayer.listRoom)
            {
                if (!roomPlay.gameStart)
                {
                    if (roomPlay.id == room)
                    {
                        if (roomPlay.player1 == "")
                        {
                            roomPlay.player1 = token;
                            Callback.ResultMsg("Player 1 Jugador " + token + " añadido al room " + roomPlay.id);
                        }
                        else if (roomPlay.player2 == "")
                        {
                            roomPlay.player2 = token;
                            Callback.ResultMsg("Player 2 Jugador " + token + " añadido al room " + roomPlay.id);
                        }

                        if (roomPlay.player1 != "" && roomPlay.player2 != "")
                        {
                            roomPlay.gameStart = true;
                            Callback.ResultMsg(" Game start ");
                        }
                            
                    }

                }                
            }
        }

        public string RegisterUser(string user)
        {
            string idUser = GetJugador(user);

            if (idUser == null)
            {
                Jugador jugador = new Jugador();

                Random rnd = new Random();
                string posibles = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                int longitud = posibles.Length;
                char letra;
                int longitudnuevacadena = 5;
                string nuevacadena = "";
                for (int i = 0; i < longitudnuevacadena; i++)
                {
                    letra = posibles[rnd.Next(longitud)];
                    nuevacadena += letra.ToString();
                }

                jugador.id = nuevacadena;
                jugador.name = user;
                jugador.score = 0;

                ServiceMultiplayer.listJugadores.Add(jugador);
                Callback.ResultMsg("Jugador creado: " + jugador.name);

                XDocument xml = new XDocument(new XElement("Root",
                                from p in ServiceMultiplayer.listJugadores
                                select new XElement("Jugador", (XElement)p)));

                xml.Save(@"C:\temp\Jugadores.xml");
                return jugador.id;

            }
            else
            {
                return idUser;
            }            
        }

        public void RemoveScore(double n, string token)
        {
            foreach (Jugador jugador in ServiceMultiplayer.listJugadores)
            {
                if (jugador.id == token)
                {
                    jugador.score -= n;
                    MSGScoreAllClient(jugador.score, token);
                }
            }
        }

        public void MSGScoreAllClient(double n, string token)
        {
            foreach (IMultiplayerCallback callback in ServiceMultiplayer.clientList)
            {
                callback.ResultScore(n, token);
            }
        }

        public string GetRoom()
        {
            foreach (Room room in ServiceMultiplayer.listRoom)
            {
                if (room.player1 == "")
                {
                    Callback.ResultMsg("Obteniendo room "+ room.name);
                    return room.id;
                }

                if (room.player2 == "")
                {
                    Callback.ResultMsg("Obteniendo room " + room.name);
                    return room.id;
                }
            }

            return "";
        }

        public void KeyUp(string token)
        {
            foreach (IMultiplayerCallback callback in ServiceMultiplayer.clientList)
            {               
                callback.KeyUp(token);
            }
        }     

        public void KeyDown(string token)
        {
            foreach (IMultiplayerCallback callback in ServiceMultiplayer.clientList)
            {
                
                callback.KeyDown(token);
            }            
        }

        public void Register()
        {
            IMultiplayerCallback callback=OperationContext.Current.GetCallbackChannel<IMultiplayerCallback>();
            ServiceMultiplayer.clientList.Add(callback);
        }

        IMultiplayerCallback Callback
        {
            get
            {
                return OperationContext.Current.GetCallbackChannel<IMultiplayerCallback>();
            }
        }
    }

    public class Jugador
    {
        public string id { get; set; }
        public string name { get; set; }
        public double score { get; set; }

        public Jugador() { }

        public static explicit operator XElement(Jugador v)
        {
            XElement xml = new XElement("Jugador",
              new XAttribute("id", v.id),
                new XAttribute("name", v.name),
                new XAttribute("score", v.score));
            return xml;
        }
    }

    public class Room
    {
        public string id { get; set; }
        public string name { get; set; }
        public string player1 { get; set; }
        public string player2 { get; set; }
        public bool gameStart { get; set; }
        public Room() { }

    }
}
