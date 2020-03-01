using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Сообщения бирюзового цвета - входящие. Через табуляцию показан размер сообщения");
            Console.WriteLine("Через пробел введите удалённый IP, локальный порт и удалённый порт\n");

            while (true)
            {

                try
                {
                    string[] input = Console.ReadLine().Split(' ');
                    Client client = new Client(input[0], int.Parse(input[1]), int.Parse(input[2]));

                    client.DoReceive();
                    client.DoSend();
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Некорректный ввод. Пример:   127.0.0.1 4445 4446");
                    continue;
                }

            }
            Console.Clear();
            Console.WriteLine("Чат начат.\n");

        }
    }

    class Client
    {
        UdpClient sender;
        UdpClient receiver;

        Thread listenThread;
        Thread sendThread;

        public Client(string remoteIp, int localPort, int remotePort)
        {
            sender = new UdpClient(remoteIp, remotePort);
            receiver = new UdpClient(localPort);
        }

        public void DoReceive()
        {
            listenThread = new Thread(() =>
            {
                IPEndPoint end = null;

                try
                {

                    while (true)
                    {
                        byte[] incomeData = receiver.Receive(ref end);
                        string msg = Encoding.UTF8.GetString(incomeData);

                        PrintMessage($"{msg} \t[{incomeData.Length} bytes]\n", ConsoleColor.Cyan);
                    }

                }
                catch (Exception ex)
                {
                    PrintMessage("RECEIVE ERROR!\n" + ex.Message, ConsoleColor.Red);
                }
            });

            listenThread.Start();
        }

        public void DoSend()
        {
            sendThread = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        string sendMessage = Console.ReadLine();

                        byte[] sendData = Encoding.UTF8.GetBytes(sendMessage);
                        int sentBytes = sender.Send(sendData, sendData.Length);

                        PrintMessage($"Sent {sentBytes} bytes.\n", ConsoleColor.Green);
                    }
                }
                catch (Exception ex)
                {
                    PrintMessage("SEND ERROR!!\n" + ex.Message, ConsoleColor.Red);
                }
            });

            sendThread.Start();
        }

        void PrintMessage(string msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
    }
}
