using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace ChatBot_Demo
{
    public class TwitchChatBot
    {
        //Props
        public TcpClient Client;
        public StreamReader Reader;
        public StreamWriter Writer;
        private string Login_Name { get; set; }
        private string Token { get; set; }
        private string Channel_To_Join { get; set; }

        //constructor
        public TwitchChatBot(string l, string t, string c)
        {
            this.Login_Name = l;
            this.Token = t;
            this.Channel_To_Join = c;
        }

        #region IRC
        /// <summary>
        /// Makes an IRC connection to Twitch
        /// </summary>
        /// <returns>void</returns>
        public void Connect()
        {
            Client = new TcpClient("irc.twitch.tv", 6667);
            Reader = new StreamReader(Client.GetStream());
            Writer = new StreamWriter(Client.GetStream());

            Writer.WriteLine("PASS " + Token);
            Writer.WriteLine("NICK " + Login_Name);
            Writer.WriteLine("USER " + Login_Name + " 8 * :" + Login_Name);
            Writer.WriteLine("JOIN #" + Channel_To_Join);
            Writer.Flush();
        }

        /// <summary>
        /// Reads a message from the chat of the channel you're joined to
        /// </summary>
        /// <returns>string</returns>
        public string ReadMessage()
        {
            string chat_message = Reader.ReadLine();
            return chat_message;
        }

        /// <summary>
        /// Sends a message in the chat of the channel you're joined to
        /// </summary>
        /// <param name="message"></param>
        /// <returns>void</returns>
        public void SendMessage(string message)
        {
            string toSend = (":" + Login_Name + "!" + Login_Name + "@" + Login_Name +
            ".tmi.twitch.tv PRIVMSG #" + Channel_To_Join + " :" + message);
            Writer.WriteLine(toSend);
            Writer.Flush();
        }

        /// <summary>
        /// Sends a ping periodically to keep the connection alive
        /// </summary>
        /// <returns>void</returns>
        public void SendPing()
        {
            Console.WriteLine("Sending PING....");
            Writer.WriteLine("PING :irc.twitch.tv");
            Writer.Flush();
        }

        /// <summary>
        /// Sends a pong in response to a PING to keep the connection alive
        /// </summary>
        /// <returns>void</returns>
        public void SendPong()
        {
            Console.WriteLine("Sending PONG....");
            Writer.WriteLine("PONG :irc.twitch.tv");
            Writer.Flush();
        }

        #endregion
    }
}
