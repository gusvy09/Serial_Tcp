using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace MyServer
{
    public partial class Form1 : Form
    {
        //ip, port 할당 (local ip, port번호는 임의)
        private string ip = "127.0.0.1";
        private int port = -1;

        // 데이터를 읽고 쓰기 위한 스트림리더,라이터
        bool connected = false;
        StreamReader StreamReader1;
        StreamWriter StreamWriter1;

        // on, off를 위해 전역 변수로 선언
        Thread thread1;
        TcpListener tcpListener1;
        TcpClient tcpClient1;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // 연결 버튼
        {
            if(button1.Text == "Start") // 서버 실행전 On으로 상태 변경
            {
                Boolean bPort = true;
                bPort = int.TryParse(PortText.Text, out port);
                if (bPort && port>0) // 포트 번호가 제대로 입력되어 있는 경우
                {
                    button1.Text = "Stop"; // 실행되는 상태
                    label1.Text = "server on";
                    writeRichText("서버 ON");
                    thread1 = new Thread(connect);
                    thread1.IsBackground = true;
                    thread1.Start();
                }
                else
                {
                    writeRichText("port번호를 다시 확인하세요");
                }
            }
            else // 서버 실행후 Off로 상태 변경
            {
                button1.Text = "Start"; 
                label1.Text = "server off";
                writeRichText("서버 OFF");
                if (tcpClient1 == null) // 연결된 클라이언트 없이 끌때
                {
                    tcpListener1.Server.Close();
                    thread1 = null;
                    GC.Collect();
                }
                else if (tcpClient1.Connected) // 연결된 클라이언트가 있으면
                {
                    // 메모리 해제 및 연결 끊기
                    StreamWriter1.WriteLine("[server]:ServerIsClose");
                    StreamReader1.Close();
                    StreamWriter1.Close();
                    tcpListener1.Server.Close();
                    connected = false; // while문 탈출을 위해서
                    tcpClient1 = null; 
                    thread1 = null;
                }
                
            }
            
        }
        // tcpclient를 이용하여 stream 연결하는 메서드
        private void connect_Stream(TcpClient tcp)
        {
            StreamReader1 = new StreamReader(tcp.GetStream()); // 읽기 스트림 연결
            StreamWriter1 = new StreamWriter(tcp.GetStream()); // 쓰기 스트림 연결
            StreamWriter1.AutoFlush = true; // 쓰기 버퍼 자동으로 처리
        }
        private void connect() // 연결
        {
            try
            {
                tcpListener1 = new TcpListener(IPAddress.Parse(ip), port); //ip와 port번호 이용하여 서버 open
                tcpListener1.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                tcpListener1.Start();
                writeRichText("서버에서 클라이언트 기다리는중");
                
                tcpClient1 = tcpListener1.AcceptTcpClient(); // client가 연결될 때 까지 대기

                ReceiveFromClient(tcpClient1);
            }
            catch {  }
            finally
            {
            }
        }

        private void writeRichText(string msg) // richtextbox 업데이트
        {
            richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.AppendText(msg + "\r\n"); }); // 리치텍스트에 한 줄 입력
            richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.ScrollToCaret(); }); // 스크롤 맨 아래로
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e) // 텍스트박스에서 엔터 누르면 채팅전달
        {

            if (e.KeyCode == Keys.Enter)
            {
                string data = textBox1.Text;
                Send_Data("[chat]:"+data);
            }
        }
        private void Send_Data(string data) // 클라이언트에 데이터를 전송하는 메서드
        {
            if ( tcpClient1 == null || !tcpClient1.Connected) // 텍스트가 비어있거나, 클라이언트에 연결이 되어있지 않다면
            {
                writeRichText("연결된 클라이언트가 없습니다.");
                textBox1.Text = ""; // 초기화
            }
            else if (textBox1.Text.Trim() == "")
            {
                writeRichText("보낼 메세지를 입력해주세요");
                textBox1.Text = ""; // 초기화
            }
            else
            {
                StreamWriter1.WriteLine(data);
                textBox1.Text = ""; // 송신 후 초기화
            }
        }
        private void ReceiveFromClient(TcpClient tcpClient)
        {
            connect_Stream(tcpClient); // client와 연결 후 stream 연결
            connected = true;
            writeRichText("클라이언트 연결 완료");
            while (connected) // clinet와 연결이 되어있는 동안
            {
                string receiveData1 = StreamReader1.ReadLine(); // 데이터 읽기
                if(receiveData1 == "disconnect_server")
                {
                    StreamWriter1.WriteLine("[server]:ServerIsClose");
                    connected = false;
                    StreamReader1.Close();
                    StreamWriter1.Close();
                    tcpListener1.Server.Close();
                    tcpClient1 = null;
                    thread1 = new Thread(connect);
                    thread1.IsBackground = true;
                    thread1.Start();
                    break;
                }
                string[] type = receiveData1.Split(':');  
                if (type[0] == "chat")
                {
                    writeRichText(type[1]); // 읽어온 데이터 richtext에 추가 (채팅)
                }
                else if (type[0] == "data")
                {
                    // data 처리 및 return
                    return_data(type[1]);
                }
            }
        }
        // data 처리 및 리턴 (정상데이터는 OK붙여서 리턴)
        private void return_data(string data)
        {
            writeRichText(data);
            // 받은 데이터를 ; 기준으로 나눔 (현재 FLAG 컬럼의 값이 없기 때문에 substring으로 잘라서 구분, 추후에 값이 생기면 substring하지 않아도 됨)
            string[] check = data.Substring(0,data.Length).Split(';');
            bool flag = true;
            // 나눈 데이터가 정상(빈칸이 없음)인지 아닌지 구별
            for(int i = 0; i < check.Length-1; i++)
            {
                if(check[i] == "" || check[i] == null)
                {
                    flag = false;
                    break;
                }
            }
            // 정상데이터는 OK를 붙여서 반환
            if (flag)
            {
                StreamWriter1.WriteLine("test:" + data + "OK");
            }
            // 빈 문자열 or null이 포함되어 있으면 그냥 반환
            else
            {
                StreamWriter1.WriteLine("test:" + data);
                flag = true;
            }
        }
    }
}
