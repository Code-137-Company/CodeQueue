using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CodeQueue.Service.Services.SocketService
{
    public class SocketServer
    {
        private readonly int _port;
        private readonly TcpListener _server;

        public SocketServer()
        {
            _port = 8423;
            _server = new TcpListener(IPAddress.Loopback, _port);
        }

        public void Start() => _server.Start();

        public async Task AwaitReceive()
        {
            var client = _server.AcceptTcpClient();

            var stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            client.Close();
        }
    }
}
