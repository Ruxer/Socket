using System;



#region 命名空间
using System.Text;//Encoding
using System.Net;//IpAddress、IPEndPoint
using System.Net.Sockets;//Socket
using System.Threading;//Thread
#endregion


namespace SocketServerConsole
{
    class Server
    {


        static void Main(string[] args)//Main方法默认的访问级别为private
        {

            //定义主机的IP信息

            IPEndPoint serverIP = new IPEndPoint(IPAddress.Parse("192.168.2.106"),8685);


            //启动服务器
            TcpServer(serverIP);



        }



        #region TCP连接方式
        
        /// <summary>
        /// 服务器连接方法
        /// </summary>
        ///
        private static void TcpServer(IPEndPoint serverIP)
        {

          // Console.WriteLine("这是TCP连接模式.......");

            Socket tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream,ProtocolType.Tcp);

            tcpSocket.Bind(serverIP);//与本地终端进行绑定
            tcpSocket.Listen(10);//开始进行监听（参数是可以进行监听的最大数量）
            Console.WriteLine("开始进行监听");

            Socket client = tcpSocket.Accept();//为连接到的客户端创建一个socket，用来发送/接收数据，直到close().应该是循环多次来连接客户端（线程）

            Console.WriteLine("客户端成功连接服务器......");

            //这里服务器端

            while(true){

            //发送消息

            	string messStr = Console.ReadLine();

            bool successful = SendData(client,messStr);

          if(successful){

          	Console.WriteLine("发送成功！"); 
          	Console.Write("请继续输入：");

          }

         // Console.ReadLine();

          // //接收消息
          // string reciStr = ReciveData(client);

      	}	


      	

        }

        #endregion


            //服务器端接收客户端信息并对客户端进行反馈
            //定义方法：处理发送/接收

            #region 接收数据方法

            private string ReciveData(Socket serverSocket){

            	//接收
            	//string reciveMsg = "";

            	byte[] data = new byte[1024];

            	int byteLength = serverSocket.Receive(data);




            	return Encoding.UTF8.GetString(data);

            }

            #endregion

            #region 发送数据方法
            private static bool SendData(Socket serverSocket,string sendMessage){

            	Boolean isNull;

            	int bytes =  serverSocket.Send(Encoding.UTF8.GetBytes(sendMessage)); 

            	if(bytes != 0){

            	isNull = false;

            	}else{

            		isNull = true;
            	}
            	return isNull;
            }

			#endregion




    }


}
