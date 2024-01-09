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
using System.Data.OleDb;
using System.Data.SqlClient;


namespace MyClient
{
    public partial class Form1 : Form
    {
        // 전역변수 사용 (stream, tcpclient)
        StreamReader streamReader;
        StreamWriter streamWriter;
        TcpClient tcpClient;

        OleDbConnection OleConn;


        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e) // 연결
        {
            Thread thread = new Thread(connect);
            thread.IsBackground = true;
            thread.Start();
        }

        private void connect()
        {
            if (tcpClient != null && tcpClient.Connected)
            {
                writeRichbox("이미 서버와 연결되어 있습니다.");
                return;
            }
            try
            {
                tcpClient = new TcpClient(); // TcpClient 객체 생성
                IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse(textBox1.Text), int.Parse(textBox2.Text)); // ip, port 할당
                tcpClient.Connect(ipEnd);

                writeRichbox("서버에 연결 되었습니다.");

                streamReader = new StreamReader(tcpClient.GetStream()); // 읽기, 쓰기 스트림 연결
                streamWriter = new StreamWriter(tcpClient.GetStream());
                streamWriter.AutoFlush = true; // 쓰기 버퍼 자동으로 처리

                while (tcpClient.Connected) // 연결되어 있는 동안 데이터 수신
                {
                    string receivedData = streamReader.ReadLine(); // 0번째 문자열로 db에 저장할지, richtext에 추가할지 구분
                    
                    string[] Data = receivedData.Split(':');
                    string str = "";
                    if (Data[1] == "ServerIsClose")
                    {
                        writeRichbox("서버와 연결이 끊겼습니다.");
                        streamReader.Close();
                        streamWriter.Close();
                        tcpClient.Close();
                        break;
                    }
                    for (int i = 1; i < Data.Length; i++)
                    {
                        str += Data[i] + " "; // 문자열 합치기
                    }
                    // 채팅
                    if (Data[0] == "[chat]")
                    {   
                        writeRichbox(str);  
                    }
                    else if (Data[0] == "test") // result.mdb 리턴받은 값
                    {
                        try
                        {
                            ConnectOledb4("RESULT.mdb");
                            // 정상 데이터로 확인 후 return을 받으면 FLAG를 1로 수정 (sql update문)
                            // data [1] : 받은 데이터, Data[1].Split(';')[1] : ENDTIME
                            if (Data[1].Substring(Data[1].Length - 2, 2) == "OK")
                            {
                                string sql = string.Format("update result set FLAG = 1 where ENDTIME = '{0}'", Data[1].Split(';')[1]);
                                ExecSql(sql);
                            }
                        }
                        catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                        finally
                        {
                            OleConn.Close();
                        }
                    }
                }
            }
            catch(Exception ex)
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

        private void writeRichbox(string msg)
        {
            richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.AppendText(msg + "\r\n"); });
            richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.ScrollToCaret(); });
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter) // 엔터 눌렀을때
            {
                
                if(tcpClient == null || !tcpClient.Connected ) // textbox가 비어있거나, client가 null, 연결이 되지 않은 경우
                {
                    writeRichbox("서버와 연결되지 않았습니다.");
                    textBox3.Text = "";
                }
                else if(textBox3.Text == "")
                {
                    writeRichbox("보낼 메세지를 입력해주세요.");
                    textBox3.Text = "";
                }
                else
                {
                    string sendData = textBox3.Text;
                    streamWriter.WriteLine("chat:" + sendData);
                    textBox3.Text = "";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) // 연결 끊기 버튼
        {
            try
            {
                if (tcpClient == null || !tcpClient.Connected) // 연결이 되어있지 않은 경우
                {
                    writeRichbox("연결되어 있는 서버가 없습니다");
                }
                else // 연결이 되어 있는 경우
                {
                    writeRichbox("서버와 연결을 끊었습니다.");
                    streamWriter.WriteLine("disconnect_server");
                    streamReader.Close();
                    streamWriter.Close();
                    tcpClient.Close();
                }
            }
            catch { }
        }

        private void ConnectOledb4(string OleDbName) // 4번과제 mdb에 연결하는 메서드
        {
            OleConn = new OleDbConnection();
            // 대문자, 소문자 구별
            string strDbConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + OleDbName + ";Jet OLEDB:Database Password=veyrontec";
            OleConn.ConnectionString = strDbConn;
            try
            {
                OleConn.Open();
            }
            catch (Exception ex)
            {
                writeRichbox(ex.ToString());
            }
        }
        private int ExecSql(string Sql) // 쿼리문 실행
        {
            OleDbCommand cmd = new OleDbCommand(Sql, OleConn);
            return cmd.ExecuteNonQuery();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 폼 강제종료시 서버 연결을 끊기 위함
            try
            {
                // tcpClient가 null값이거나 연결되어 있지 않음
                if (tcpClient == null || !tcpClient.Connected)
                {
                    return;
                }
                streamWriter.WriteLine("disconnect_server");
                streamReader.Dispose();
                streamWriter.Dispose();
                tcpClient.Dispose();
            }
            catch { }
        }
        private void button5_Click(object sender, EventArgs e) //db조회 및 Data 전송
        {
            string sqlstr;
            string resultData = "";
            OleDbCommand Olecmd;
            OleDbDataReader reader;
            
            // tcpClient의 상태를 확인
            if (tcpClient == null || !tcpClient.Connected)
            {
                writeRichbox("연결된 서버가 없습니다.");
            }
            else
            {
                ConnectOledb4("result.mdb");
                // 전체 데이터 조회
                // sql문으로 startime과 entime의 최신순으로 data를 읽음
                sqlstr = "SELECT * FROM RESULT order by starttime desc, endtime desc;"; 

                try
                {
                    Olecmd = new OleDbCommand(sqlstr, OleConn);
                    reader = Olecmd.ExecuteReader();
                    // 컬럼의 갯수
                    int columnCount = reader.FieldCount;
                    while (reader.Read())
                    {
                        // 컬럼(열)의 갯수만큼 반복 및 데이터 합치기
                        for(int i = 0; i < columnCount; i++)
                        {
                            // column의 값들을 ;을 기준으로 구분하기 위함
                            resultData += (reader[i].ToString() + ";");   
                        }

                        // Data의 Flag값을 확인한 후 전송
                        // Flag의 값이 1이 아니면 전송
                        if (!int.TryParse(resultData.Split(';')[12], out int n) || n != 1)
                        {
                            // 맨 마지막 ; 삭제를 위한 substring
                            streamWriter.WriteLine("data:" + resultData.Substring(0, resultData.Length - 1));
                        }
                        // 보낸 데이터 초기화
                        resultData = "";
                    }

                    // while문 종료 뒤 메모리 해제
                    reader.Close();
                    Olecmd.Dispose();
                    OleConn.Close();
                }
                catch(Exception ex) { writeRichbox(ex.ToString()); }
            }
        }
    }
}
