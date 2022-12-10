using System.Net;
using System.Net.Sockets;
using System.Text;


namespace Receiver;

public static class Program
{
    public static void Main(string[] args)
    {
        switch (args.First())
        {
            case "udp":
                Udp();
                break;
            case "tcp":
                Tcp();
                break;
            default:
                throw new ArgumentException();
        }
    }


    private static void Udp()
    {
        var localIP = new IPAddress(
            new byte[] {127, 0, 0, 1}
        );
        var client = new UdpClient(
            new IPEndPoint(
                localIP,
                25555
            )
        );
        var endPoint = new IPEndPoint(
            localIP,
            25550
        );
        Console.WriteLine(
            Encoding.ASCII.GetString(
                client.Receive(ref endPoint)
            )
        );
    }


    private static void Tcp()
    {
        var listener = new TcpListener(
            new IPAddress(new byte[] {127, 0, 0, 1}),
            25550
        );
        Console.WriteLine("Create listener");
        listener.Start();
        Console.WriteLine("Listener started");
        var connected = listener.AcceptTcpClient();
        Console.WriteLine("Connection received");
        connected.GetStream().WriteByte(166);
    }
}