
namespace MyClient
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.ComboPort = new System.Windows.Forms.ComboBox();
            this.ComboBaud = new System.Windows.Forms.ComboBox();
            this.SerialStatus = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SocketIP = new System.Windows.Forms.TextBox();
            this.SocketPort = new System.Windows.Forms.TextBox();
            this.SocketStatus = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.RdioSe = new System.Windows.Forms.RadioButton();
            this.RdioSo = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(32, 56);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(484, 231);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(32, 309);
            this.textBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(484, 21);
            this.textBox3.TabIndex = 6;
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("돋움", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button3.Location = new System.Drawing.Point(197, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(81, 22);
            this.button3.TabIndex = 11;
            this.button3.Text = "Open";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("돋움", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button4.Location = new System.Drawing.Point(197, 31);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(81, 21);
            this.button4.TabIndex = 12;
            this.button4.Text = "Close";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("돋움", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button6.Location = new System.Drawing.Point(534, 301);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(79, 34);
            this.button6.TabIndex = 13;
            this.button6.Text = "BitSend";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // ComboPort
            // 
            this.ComboPort.FormattingEnabled = true;
            this.ComboPort.Location = new System.Drawing.Point(68, 4);
            this.ComboPort.Name = "ComboPort";
            this.ComboPort.Size = new System.Drawing.Size(121, 20);
            this.ComboPort.TabIndex = 14;
            this.ComboPort.SelectedIndexChanged += new System.EventHandler(this.ComboPort_SelectedIndexChanged);
            // 
            // ComboBaud
            // 
            this.ComboBaud.FormattingEnabled = true;
            this.ComboBaud.Location = new System.Drawing.Point(68, 32);
            this.ComboBaud.Name = "ComboBaud";
            this.ComboBaud.Size = new System.Drawing.Size(121, 20);
            this.ComboBaud.TabIndex = 15;
            this.ComboBaud.SelectedIndexChanged += new System.EventHandler(this.ComboBaud_SelectedIndexChanged);
            // 
            // SerialStatus
            // 
            this.SerialStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SerialStatus.Location = new System.Drawing.Point(285, 17);
            this.SerialStatus.Name = "SerialStatus";
            this.SerialStatus.Size = new System.Drawing.Size(31, 24);
            this.SerialStatus.TabIndex = 16;
            this.SerialStatus.TabStop = false;
            this.SerialStatus.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(398, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(391, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Port";
            // 
            // SocketIP
            // 
            this.SocketIP.Location = new System.Drawing.Point(424, 4);
            this.SocketIP.Name = "SocketIP";
            this.SocketIP.Size = new System.Drawing.Size(128, 21);
            this.SocketIP.TabIndex = 19;
            // 
            // SocketPort
            // 
            this.SocketPort.Location = new System.Drawing.Point(424, 32);
            this.SocketPort.Name = "SocketPort";
            this.SocketPort.Size = new System.Drawing.Size(128, 21);
            this.SocketPort.TabIndex = 20;
            // 
            // SocketStatus
            // 
            this.SocketStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SocketStatus.Location = new System.Drawing.Point(646, 17);
            this.SocketStatus.Name = "SocketStatus";
            this.SocketStatus.Size = new System.Drawing.Size(31, 24);
            this.SocketStatus.TabIndex = 23;
            this.SocketStatus.TabStop = false;
            this.SocketStatus.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("돋움", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button2.Location = new System.Drawing.Point(558, 31);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(81, 21);
            this.button2.TabIndex = 22;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("돋움", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button5.Location = new System.Drawing.Point(558, 4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(81, 22);
            this.button5.TabIndex = 21;
            this.button5.Text = "Open";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // RdioSe
            // 
            this.RdioSe.AutoSize = true;
            this.RdioSe.Font = new System.Drawing.Font("돋움", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.RdioSe.Location = new System.Drawing.Point(7, 21);
            this.RdioSe.Name = "RdioSe";
            this.RdioSe.Size = new System.Drawing.Size(61, 16);
            this.RdioSe.TabIndex = 24;
            this.RdioSe.TabStop = true;
            this.RdioSe.Text = "Serial";
            this.RdioSe.UseVisualStyleBackColor = true;
            this.RdioSe.CheckedChanged += new System.EventHandler(this.RdioSe_CheckedChanged);
            // 
            // RdioSo
            // 
            this.RdioSo.AutoSize = true;
            this.RdioSo.Font = new System.Drawing.Font("돋움", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.RdioSo.Location = new System.Drawing.Point(322, 21);
            this.RdioSo.Name = "RdioSo";
            this.RdioSo.Size = new System.Drawing.Size(67, 16);
            this.RdioSo.TabIndex = 25;
            this.RdioSo.TabStop = true;
            this.RdioSo.Text = "Socket";
            this.RdioSo.UseVisualStyleBackColor = true;
            this.RdioSo.CheckedChanged += new System.EventHandler(this.RdioSo_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 360);
            this.Controls.Add(this.RdioSo);
            this.Controls.Add(this.RdioSe);
            this.Controls.Add(this.SocketStatus);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.SocketPort);
            this.Controls.Add(this.SocketIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SerialStatus);
            this.Controls.Add(this.ComboBaud);
            this.Controls.Add(this.ComboPort);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.richTextBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox textBox3;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ComboBox ComboPort;
        private System.Windows.Forms.ComboBox ComboBaud;
        private System.Windows.Forms.Button SerialStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SocketIP;
        private System.Windows.Forms.TextBox SocketPort;
        private System.Windows.Forms.Button SocketStatus;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.RadioButton RdioSe;
        private System.Windows.Forms.RadioButton RdioSo;
    }
}

