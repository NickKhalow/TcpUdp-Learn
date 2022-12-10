using System.Net;
using System.Net.Sockets;
using System.Text;


namespace Sender;

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
        var client = new UdpClient(
            new IPEndPoint(
                new IPAddress(
                    new byte[] {127, 0, 0, 1}
                ),
                25550
            )
        );
        client.Connect("localhost", 25555);
        client.Send(
            new ReadOnlySpan<byte>(
                Encoding.ASCII.GetBytes(
                    "Hello network!"
                )
            )
        );
    }


    private static void Tcp()
    {
        var client = new TcpClient(
            "localhost",
            25550
        );
        Console.WriteLine(client.GetStream().ReadByte());
    }
}