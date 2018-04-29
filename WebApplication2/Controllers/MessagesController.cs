using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microsoft.Web.WebSockets;

namespace uppgift_3.Controllers
{
    public class MessagesController : ApiController
    {

        public HttpResponseMessage Get(String username)
        {
            //accepts the web socket request and sets up a class called ChatwsHandler to manage the connection- username parameter for each new connction.
            HttpContext.Current.AcceptWebSocketRequest(new ChatwsHandler(username));

            //sends back a response with HTTP status code 101 telling the client that it has agreed to switch to the WebSocket protocol.
            return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
        }
       

        class ChatwsHandler : WebSocketHandler
        {
            //this class manages a list of chat clients and broadcasts the messages to all clients when it recives a message.

            private static WebSocketCollection chatClients = new WebSocketCollection();
            private string enteredUsername;

            public ChatwsHandler(string username)
            {
                //sets entered username to the username parameter that will be sending and recieving the messages

                enteredUsername = username;
            }

            public override void OnOpen()
            {
                //'override' the normal OnOpen fuction that happens in WebSocketHandler to the code below.
                //have the new chat clients (usernames entered) added to the websocketcollection as they connect.

                chatClients.Add(this);

            }

            public override void OnMessage(string message)
            {
                //'override' the normal OnMessage fuction that happens in WebSocketHandler to the code below.
                //to format what will be pushed from the server to all the clients connected.

                chatClients.Broadcast(enteredUsername + ": " + message);
            }
        }

    }
}
