using Microsoft.AspNet.SignalR;
using System;
using System.Threading.Tasks;

namespace MVC5Course.Hubs
{
    public class ModbusHub : Hub
    {
        private static readonly IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<ModbusHub>();

        public static void BroadcastData(string address, ushort value)
        {
            Console.WriteLine($"📡 廣播數據: {address} = {value}"); // 記錄數據
            hubContext.Clients.All.updateModbusData(address, value);
        }

        public override Task OnConnected()
        {
            Console.WriteLine($"✅ 用戶已連線: {Context.ConnectionId}");
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Console.WriteLine($"✅ 用戶已離開: {Context.ConnectionId}");
            return base.OnDisconnected(stopCalled);
        }
    }
}