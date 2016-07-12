using System;


#region
using System.Text;//Encoding
using System.Net;//IpAddress、IPEndPoint
using System.Net.Sockets;//Socket
using System.Threading;//Thread
#endregion




namespace SocketClientConsole
{
    class Client
    {

    public static void Main(string[] args)
        {

            IPEndPoint clientIP = new IPEndPoint(IPAddress.Parse("192.168.2.106"),8685);


            TcpClient(clientIP);


        }    


        #region 客户端连接方法


        private static void TcpClient(IPEndPoint ClientIP)
        {

            Socket tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream,ProtocolType.Tcp);

            bool isExcepition = false;

            try
            {

                Console.WriteLine("正在连接服务器......");

                tcpClient.Connect(ClientIP);

                Console.WriteLine("连接到服务器.........");
            }
            catch (ObjectDisposedException e)
            {
                Console.WriteLine("出现了异常：{0}",e.Message);
                
                
            }
            catch(SocketException ex){

                isExcepition = true;

                Console.WriteLine("出现了异常：{0}",ex.Message);


            }finally{

                if(isExcepition){

                tcpClient.Close();

            }

            }

           

            // //开始发信息给服务器

            // string sendMsg = "客户端已经连接到服务器.......";

            // tcpClient.Send(Encoding.UTF8.GetBytes(sendMsg));

            // Console.WriteLine(string.Format("客户端发送的消息:{0}",sendMsg));


            while(true){

           //接收来自服务器的消息

            Console.WriteLine("接收到服务器端的消息：{0}",ReciveData(tcpClient));

           // Console.ReadLine();
                
         }

        }

        #endregion


        #region 接收消息方法

        private static string ReciveData(Socket client){


            byte[] data = new byte[1024];

            int bytes = client.Receive(data);


            return Encoding.UTF8.GetString(data,0,bytes); 

        }


        #endregion





    }
}
