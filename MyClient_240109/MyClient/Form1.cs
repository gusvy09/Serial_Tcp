using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO.Ports;
// dll import를 위한 using문
using System.Runtime.InteropServices;

namespace MyClient
{
    public partial class Form1 : Form
    {
        //Socket 통신을 위한 변수
        StreamReader streamReader;
        StreamWriter streamWriter;
        TcpClient tcpClient;
        Thread thread;

        bool bIsrun = false;
        // 초기화 확인을 위한 변수
        bool bcheck = false;
        public Form1()
        {
            InitializeComponent();

            string[] port = System.IO.Ports.SerialPort.GetPortNames();

            // 콤보박스에 아이템 추가하는 코드 --
            foreach (string name in port)
            {
                ComboPort.Items.Add(name);
            }
            int[] BaudRate = new int[7] { 4800, 9600, 19200, 38400, 57600, 115200, 230400 };
            foreach (int Baud in BaudRate)
            {
                ComboBaud.Items.Add(Baud);
            }
            // ----------------------------------- 
            // 콤보박스의 초기값
            ComboBaud.SelectedIndex = 0;
            ComboPort.SelectedIndex = 0;
            // 상태를 나타내는 버튼의 테두리 없애기
            SerialStatus.FlatAppearance.BorderSize = 0;
            SocketStatus.FlatAppearance.BorderSize = 0;
            SerialStatus.Enabled = false;
            SocketStatus.Enabled = false;
            DeactivateSocket();
            // 라디오버튼 체크 (시리얼 통신 기본값)
            RdioSe.Checked = true;
            // 포트이름과 Baudrate의 초기값을 이용하여 폼 로딩시 바로 연결
            ConSerial(ComboPort.SelectedItem.ToString(), ComboBaud.SelectedItem.ToString());

            // 초기화 후 check변수를 변경함으로써 combobox 이벤트 활성화
            bcheck = true;
        }

        #region ini read 메소드
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        #endregion
        // RichTextBox수정 
        // 충돌을 피하기 위해서 Invoke사용
        private void writeRichbox(string msg)
        {
            richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.AppendText(msg + "\r\n"); });
            richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.ScrollToCaret(); });
        }

        // 폼 강제종료시 연결을 끊기 위함
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
        }
        // data receive (수신)
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort_DataReceive();
            }
            catch { }
        }

        // 수신 이벤트
        private void SerialPort_DataReceive()
        {
            try
            {
                int bytesToRead = serialPort1.BytesToRead;
                if (bytesToRead > 0)
                {
                    byte[] buffer = new byte[bytesToRead];
                    serialPort1.Read(buffer, 0, bytesToRead);

                    string Data = Encoding.UTF8.GetString(buffer);
                    writeRichbox(Data);
                }
            }
            catch (Exception ex){ writeRichbox(ex.ToString()); }
        }


        // 시리얼포트 연결하는 메서드
        private void ConSerial(string Port,string Baud)
        {
            try
            {
                // 포트가 이미 열려있는 상태라면 닫고 다시 오픈
                if (serialPort1.IsOpen)
                    serialPort1.Close();
                serialPort1.BaudRate = int.Parse(Baud);
                serialPort1.PortName = Port;
                serialPort1.Open();
                SerialStatus.BackColor = Color.Green;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }


        #region combobox 변경시 발생
        private void ComboPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bcheck)
            {
                serialPort1.Close();
                ConSerial(ComboPort.SelectedItem.ToString(), ComboBaud.SelectedItem.ToString());
            }
        }
        private void ComboBaud_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bcheck)
            {
                serialPort1.Close();
                ConSerial(ComboPort.SelectedItem.ToString(), ComboBaud.SelectedItem.ToString());
            }
        }
        #endregion

        #region 체크박스 변경시 발생
        // 시리얼 통신 활성화 됐을 경우엔 Socket통신 비활성화
        private void RdioSe_CheckedChanged(object sender, EventArgs e)
        {
            // 초기화 이후에 체크 됐을 경우
            if(RdioSe.Checked && bcheck)
            {               
                if (!serialPort1.IsOpen)
                    ConSerial(ComboPort.SelectedItem.ToString(), ComboBaud.SelectedItem.ToString());
                DeactivateSocket();
            }
        }

        private void RdioSo_CheckedChanged(object sender, EventArgs e)
        {
            if (RdioSo.Checked)
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                    SerialStatus.BackColor = SystemColors.Control;
                }
                DeactivateSerial();
            }
        }
        #endregion

        #region 버튼
        // 시리얼 포트 오픈
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort1.IsOpen)
                {
                    ConSerial(ComboPort.SelectedItem.ToString(), ComboBaud.SelectedItem.ToString());
                }
                else
                {
                    writeRichbox("열려있음");
                }
            }
            catch (Exception ex)
            {
                writeRichbox(ex.ToString());
            }
        }
        // 송신 Btn
        private void button6_Click(object sender, EventArgs e)
        {

            try
            {
                if (serialPort1.IsOpen)
                {
                    byte[] send = Encoding.UTF8.GetBytes(textBox3.Text);
                    serialPort1.Write(send, 0, send.Length);
                    textBox3.Text = "";
                }
                else
                {
                    writeRichbox("송신 불가능");
                }
            }
            catch (Exception ex) { writeRichbox(ex.ToString()); }

        }
        // 연결 끊기 버튼
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                    SerialStatus.BackColor = SystemColors.Control;
                }
                else
                {
                    writeRichbox("시리얼 포트가 연결되어 있지 않습니다.");
                }
            }
            catch (Exception ex) { writeRichbox(ex.ToString()); }
        }
        // socket통신 연결
        private void button5_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(SocketIP.Text,out int num) && IPAddress.TryParse(SocketIP.Text, out IPAddress Address) && int.TryParse(SocketPort.Text, out int Port))
            {
                thread = new Thread(ConSocket);
                thread.IsBackground = true;
                thread.Start();
                return;
            }
            writeRichbox("아이피 혹은 포트를 다시 확인하세요");
        }
        // socket 통신 끊기
        private void button2_Click(object sender, EventArgs e)
        {
            streamWriter.WriteLine("disconnect_server");
            //DisconSocket();
        }
        #endregion

        // ui구성 요소 비활성화
        private void DeactivateSocket()
        {
            button3.Enabled = true;
            button4.Enabled = true;
            button2.Enabled = false;
            button5.Enabled = false;
            ComboBaud.Enabled = true;
            ComboPort.Enabled = true;
            SocketIP.Enabled = false;
            SocketPort.Enabled = false;
            if (tcpClient != null && tcpClient.Connected)
                streamWriter.WriteLine("disconnect_server");
        }
        private void DeactivateSerial()
        {
            ComboBaud.Enabled = false;
            ComboPort.Enabled = false;
            SocketIP.Enabled = true;
            SocketPort.Enabled = true;
            button3.Enabled = false;
            button4.Enabled = false;
            button2.Enabled = true;
            button5.Enabled = true;
        }
        private void ConSocket()
        {
            if (tcpClient != null && tcpClient.Connected)
            {
                writeRichbox("이미 서버와 연결되어 있습니다.");
                return;
            }
            try
            {
                tcpClient = new TcpClient(); // TcpClient 객체 생성
                IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse(SocketIP.Text), int.Parse(SocketPort.Text)); // ip, port 할당
                tcpClient.Connect(ipEnd);
                bIsrun = true;

                writeRichbox("서버에 연결 되었습니다.");

                streamReader = new StreamReader(tcpClient.GetStream()); // 읽기, 쓰기 스트림 연결
                streamWriter = new StreamWriter(tcpClient.GetStream());
                streamWriter.AutoFlush = true; // 쓰기 버퍼 자동으로 처리

                while (bIsrun) // 연결되어 있는 동안 데이터 수신
                {
                    string receivedData = streamReader.ReadLine();

                    if (receivedData == "[server]:ServerIsClose")
                    {
                        DisconSocket();
                        writeRichbox("서버와 연결이 끊겼습니다.");
                        return;
                    }
                    writeRichbox(receivedData);
                }
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("거부"))
                {
                    writeRichbox("서버 상태를 확인하세요.");
                }
                else if (ex.ToString().Contains("강제"))
                {
                    writeRichbox("서버와 연결이 끊겼습니다.");
                }
                tcpClient = null;
            }
        }

        // 연결 종료하는 메서드
        private void DisconSocket()
        {
            if(tcpClient == null || !tcpClient.Connected)
            {
                return;
            }
            bIsrun = false;
            streamReader.Close();
            streamWriter.Close();
            tcpClient = null;
        }
    }


}
