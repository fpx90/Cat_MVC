using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Cat_SignalR.Hubs
{
    public class CatHub : Hub
    {
        private readonly static Dictionary<string, string> connections = new Dictionary<string, string>();

        public static void SendToOne(string username,string content)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<CatHub>();
            string userId = "";
            if (connections.ContainsKey(username))
            {
                userId = connections[username];
                context.Clients.Client(userId).SendMessage(content == "" ? "服务器给你推送消息了" : content);
            }
        }

        public void SendLogin(string username)
        {
            if (!connections.ContainsKey(username))
            {
                connections.Add(username, Context.ConnectionId);
            }
            else
            {
                connections[username] = Context.ConnectionId;
            }
        }
    }
}