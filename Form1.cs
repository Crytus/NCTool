//
// NCTool by Crytus Project
//    Copyright (c) 2020,2021 Crytus Project All right reserved.
//
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using System.Reflection;
using System.Collections.Generic;

namespace NCTool
{

    public partial class NCTool : Form
    {
        static string VersionInfo = "NCTool Version 0.9 2020/07/20\nPresented by Crytus (https://crytus.info)";

        static string InitCmd = "G0 G54 G17 G21 G90 M5 M9 T0 F500 S1000";
        static double x = 0.0, y = 0.0;
        static double vi = 0.0, vj = 0.0, vr = 0.0;
        static int gc = 0;
        //static int digit1 = 4, digit2 = 3;	// 整数4桁、小数3桁
        static int xdigit1 = 4, xdigit2 = 3;	// 整数4桁、小数3桁
        static int ydigit1 = 4, ydigit2 = 3;	// 整数4桁、小数3桁
        static bool idle = true;
        static double cur_x, cur_y;
        StreamReader sr;
        Thread fileThread;
        static bool fileAbort = false;
        static string unit;
        static bool leaser = false;	// レーザーモード

        static Dictionary<string, string> tools;
        static List<action> actions;
        static List<material> materials;

        static double workarea_x = 0;
        static double workarea_y = 0;

        static double getNumb(string str, int digit1, int digit2)
        {
            double fv = 0.0;
            int minus = 1;
            if (str[0] == '-')
            {
                minus = -1;
                str = str.Substring(1);
            }
            if (str.Length > digit2)
            {
                double v1 = int.Parse(str.Substring(0, str.Length - digit2));
                double v2 = int.Parse(str.Substring(str.Length - digit2));
                if (digit2 == 1)
                {
                    fv = v1 + v2 / 10.0;
                }
                if (digit2 == 2)
                {
                    fv = v1 + v2 / 100.0;
                }
                if (digit2 == 3)
                {
                    fv = v1 + v2 / 1000.0;
                }
                if (digit2 == 4)
                {
                    fv = v1 + v2 / 10000.0;
                }
            }
            else
            {
                fv = int.Parse(str);
            }
            return fv * minus;
        }
        static string getCmd(string str)
        {
            int len;
            for (len = 1; len < str.Length; len++)
            {
                if ((str[len] != '-')&&((str[len] < '0') || (str[len] > '9')))
                {
                    break;
                }
            }
            return str.Substring(0, len);

        }

        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileStringW", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);
        private string status = "";
        StreamWriter writer;
        string logFilename;

        public NCTool()
        {
            InitializeComponent();
        }
        public string GetIniValue(string path, string section, string key)
        {
            StringBuilder sb = new StringBuilder(256);
            GetPrivateProfileString(section, key, string.Empty, sb, (uint)sb.Capacity, path);
            Console.WriteLine(section + ":" + key + "=" + sb);
            return sb.ToString();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            materials = new List<material>();

            string[] ports = GetDeviceNames();
            if (ports != null)
            {
                foreach (string port in ports)
                {
                    Console.WriteLine(port);
                    comCombo.Items.Add(port);
                }
            }
            Assembly myAssembly = Assembly.GetEntryAssembly();
            string path = myAssembly.Location;
            path = path.Replace(".exe", ".ini");
            Console.WriteLine(path);
            //
            hCombo.Items.Add("0.1");
            hCombo.Items.Add("0.5");
            hCombo.Items.Add("1");
            hCombo.Items.Add("5");
            hCombo.Items.Add("10");
            hCombo.Items.Add("50");
            hCombo.SelectedIndex = 4;
            //
            vCombo.Items.Add("0.1");
            vCombo.Items.Add("0.5");
            vCombo.Items.Add("1");
            vCombo.Items.Add("5");
            vCombo.Items.Add("10");
            //vCombo.Items.Add("50");
            vCombo.SelectedIndex = 3;
            //
            int num = 0;
            for (int i = 1; i <= 20; i++) {
                string str = GetIniValue(path, "material", Convert.ToString(i));
                if (str.Length == 0) break;
                materialCombo.Items.Add(str);
                string step = GetIniValue(path, "step", Convert.ToString(i));
                string speed = GetIniValue(path, "speed", Convert.ToString(i));
                materials.Add(new material() { name = str, step = step, speed = speed});
                num++;
            }
            if (num == 0)
            {
                MessageBox.Show("設定ファイルが読み込めませんでした。\r\n" + "(" + path + ")\r\n" + "プログラムを終了します", "エラー");
                this.Close();
                return;
            }
            materialCombo.SelectedIndex = 1;
            //
            thickness.Text = "1.0";
            speed.Text = "400";
            step.Text = "0.2";
            offsetX.Text = "0.0";
            offsetY.Text = "0.0";
            spindle.Text = "5000";
            //
            string max_x = GetIniValue(path, "workarea", "x");
            if (max_x.Length > 0) {
                workarea_x = double.Parse(max_x);
	        }
            string max_y = GetIniValue(path, "workarea", "y");
            if (max_y.Length > 0) {
                workarea_y = double.Parse(max_y);
            }
            string s = GetIniValue(path, "setup", "spindle");
            if (s.Length > 0) {
                spindle.Text = s;
            }
            s = GetIniValue(path, "setup", "speed");
            if (s.Length > 0) {
                speed.Text = s;
            }
            s = GetIniValue(path, "setup", "step");
            if (s.Length > 0) {
                step.Text = s;
            }
            s = GetIniValue(path, "setup", "thickness");
            if (s.Length > 0) {
                thickness.Text = s;
            }
            //str = "G0 G54 G17 G21 G90 M5 M9 T0 F500 S1000";
            s = GetIniValue(path, "setup", "initcmd");
            if (s.Length > 0)
            {
                InitCmd = s;
            }
            //
            disableAll();
            // ログファイル
            DateTime dt = DateTime.Now;
            logFilename = string.Format("{0:0000}{1:00}{2:00}{3:00}{4:00}{5:00}.log", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
            //Encoding enc = Encoding.GetEncoding("Shift_JIS");
            // ファイルを空に
            //writer = new StreamWriter(logFilename, false, enc);
            //writer.Close();
            resumeBtn.Enabled = false;
            abortBtn.Enabled = false;
            execBtn.Enabled = false;
            readBtn.Enabled = false;
        }
        private void putLog(string str)
        {
            try
            {
                Encoding enc = Encoding.GetEncoding("Shift_JIS");
                // ファイルを開く
                writer = new StreamWriter(logFilename, true, enc);
                writer.WriteLine(str);
                // ファイルを閉じる
                writer.Close();
            }
            catch
            {
                MessageBox.Show("ログ出力が出来ません", "エラー");
                return;
            }
        }
        public static string[] GetDeviceNames()
        {
            var deviceNameList = new System.Collections.ArrayList();
            var check = new System.Text.RegularExpressions.Regex("(COM[1-9][0-9]?[0-9]?)");

            ManagementClass mcPnPEntity = new ManagementClass("Win32_PnPEntity");
            ManagementObjectCollection manageObjCol = mcPnPEntity.GetInstances();

            //全てのPnPデバイスを探索しシリアル通信が行われるデバイスを随時追加する
            foreach (ManagementObject manageObj in manageObjCol)
            {
                //Nameプロパティを取得
                var namePropertyValue = manageObj.GetPropertyValue("Name");
                if (namePropertyValue == null)
                {
                    continue;
                }

                //Nameプロパティ文字列の一部が"(COM1)～(COM999)"と一致するときリストに追加"
                string name = namePropertyValue.ToString();
                if (check.IsMatch(name))
                {
                    deviceNameList.Add(name);
                }
            }

            //戻り値作成
            if (deviceNameList.Count > 0)
            {
                string[] deviceNames = new string[deviceNameList.Count];
                int index = 0;
                foreach (var name in deviceNameList)
                {
                    deviceNames[index++] = name.ToString();
                }
                return deviceNames;
            }
            else
            {
                return null;
            }
        }

        private void disableAll()
        {
            upBtn.Enabled = false;
            downBtn.Enabled = false;
            leftBtn.Enabled = false;
            rightBtn.Enabled = false;
            rearBtn.Enabled = false;
            frontBtn.Enabled = false;
            homeBtn.Enabled = false;
            sendBtn.Enabled = false;
            xyZeroBtn.Enabled = false;
            zZeroBtn.Enabled = false;
            holeBtn.Enabled = false;
        }
        private void enableAll()
        {
            upBtn.Enabled = true;
            downBtn.Enabled = true;
            leftBtn.Enabled = true;
            rightBtn.Enabled = true;
            rearBtn.Enabled = true;
            frontBtn.Enabled = true;
            homeBtn.Enabled = true;
            sendBtn.Enabled = true;
            xyZeroBtn.Enabled = true;
            zZeroBtn.Enabled = true;
            holeBtn.Enabled = true;
        }
        private void connectBtn_Click(object sender, EventArgs e)
        {

            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                connectBtn.Text = "Connect";
                disableAll();
            }
            else
            {
                if (comCombo.SelectedIndex < 0)
                {
                    MessageBox.Show("COMポートを選択してください", "エラー");
                    return;
                }
                string str = comCombo.Text;
                Regex regex = new Regex(@"\(.*\)");
                Match match = regex.Match(str);
                string port = match.Value.Substring(1, match.Value.Length - 2);
                //
                serialPort1.BaudRate = 115200;
                serialPort1.Parity = Parity.None;
                serialPort1.DataBits = 8;
                serialPort1.StopBits = StopBits.One;
                serialPort1.Handshake = Handshake.None;
                serialPort1.PortName = port;
                try
                {
                    serialPort1.Open();
                } catch
                {
                    MessageBox.Show("ポートが存在しません", "エラー");
                    return;
                }
                putLog("; " + str);
                connectBtn.Text = "Close";
                enableAll();
                // 初期設定
                Task.Delay(500);
                //serialPort1.Write(InitCmd + "\r\n");
                sendData(InitCmd);
                //sendData("G0");
                getPos();
            }
        }

        delegate void SetTextCallback(string text);
        private void Response(string text)
        {
            if (logText.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(Response);
                BeginInvoke(d, new object[] { text });

            }
            else
            {
                if (text[0] == '<')
                {
                    //Console.WriteLine(text);
                    string[] ary = text.Substring(1, text.Length - 3).Split('|');
                    if (ary.Length > 2)
                    {
                        //<Idle|MPos:0.000,0.000,0.000|FS:0,0|Ov:100,100,100>
                        status = ary[0];
                        string[] ary2 = ary[1].Split(':');
                        string[] ary3 = ary2[1].Split(',');
                        xPos.Text = ary3[0];
                        yPos.Text = ary3[1];
                        zPos.Text = ary3[2];
                    }
                    else
                    {
                        //<Idle,MPos:100.600,99.900,0.000,WPos:100.000,100.000,0.100>
                        ary = text.Substring(1, text.Length - 3).Split(',');
                        if (ary.Length > 5) { 
                            status = ary[0];
                            string[] ary2 = ary[1].Split(':');
                            ary2 = ary[4].Split(':');
                            xPos.Text = ary2[1];
                            yPos.Text = ary[5];
                            zPos.Text = ary[6];
                        }
                    }
                    //logText.AppendText(text + "\n");

                    if (status.Equals("Run"))
                    {
                        serialPort1.Write("?");
                        //idle = false;
                    } else
                    {
                        //idle = true;
                    }
                    if (status.Equals("Idle"))
                    {
                        //idle = true;
                    }
                }
                else {
                    logText.Update();
                    logText.AppendText(text + "\n");
                    // okを受け取ったら送信テキストをクリア
                    Regex regex = new Regex(@"ok");
                    Match match = regex.Match(text);
                    if (match.Value.Length > 0) {
                        sendText.Text = "";
                        getPos();
                        idle = true;
                    }
                    else
                    {
                        text = text.Trim();
                        if (text.Length > 0)
                        {
                            MessageBox.Show("[" + text + "]", "応答");
                            Console.WriteLine(text);
                            if (text.StartsWith("Grbl"))
                            {
                                //string str = "G0 G54 G17 G21 G90 M5 M9 T0 F500 S500";
                                serialPort1.Write(InitCmd + "\r\n");
                                putLog(InitCmd);
                            }
                        }
                        getPos();
                        idle = true;
                    }
                }
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string str = serialPort1.ReadLine();
            Response(str);
        }

        private void Form1_Leave(object sender, EventArgs e)
        {
            serialPort1.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            serialPort1.Close();
        }

        private void sendData(string str, bool flag = false)
        {
            if (fileAbort)
            {
                return;
            }
            //logText.Update();
            //logText.AppendText(str + "\n");

            if (flag) {
                idle = false;
            }
            serialPort1.Write(str + "\n");
            Console.WriteLine("Send:" + str);
            putLog(str);
            if (flag)
            {
                waitIdle();
            }
        }
        // 現在位置を得る
        private void getPos()
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Write("?");
            }
        }
        private void sendBtn_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                disableAll();
                logText.AppendText(sendText.Text + "\r\n");
                serialPort1.Write(sendText.Text + "\n");
                enableAll();
            }
        }

        private void leftBtn_Click(object sender, EventArgs e)
        {
            if (hCombo.SelectedIndex >= 0) {
                disableAll();
                float v = float.Parse(hCombo.Text);
                string str = string.Format("G91 G00 X-{0}", v);
                sendData(str);
                enableAll();
            }
        }

        private void homeBtn_Click(object sender, EventArgs e)
        {
            disableAll();
            sendData("G90 X0 Y0");
            enableAll();
        }

        private void rightBtn_Click(object sender, EventArgs e)
        {
            if (hCombo.SelectedIndex >= 0)
            {
                disableAll();
                float v = float.Parse(hCombo.Text);
                string str = string.Format("G91 G00 X{0}", v);
                sendData(str);
                enableAll();
            }
        }

        private void rearBtn_Click(object sender, EventArgs e)
        {
            if (hCombo.SelectedIndex >= 0)
            {
                disableAll();
                float v = float.Parse(hCombo.Text);
                string str = string.Format("G91 G00 Y{0}", v);
                sendData(str);
                enableAll();
            }
        }

        private void frontBtn_Click(object sender, EventArgs e)
        {
            if (hCombo.SelectedIndex >= 0)
            {
                disableAll();
                float v = float.Parse(hCombo.Text);
                string str = string.Format("G91 G00 Y-{0}", v);
                sendData(str);
                enableAll();
            }
        }

        private void upBtn_Click(object sender, EventArgs e)
        {
            if (vCombo.SelectedIndex >= 0)
            {
                disableAll();
                float v = float.Parse(vCombo.Text);
                string str = string.Format("G91 G00 Z{0}", v);
                sendData(str);
                enableAll();
            }
        }

        private void downBtn_Click(object sender, EventArgs e)
        {
            if (vCombo.SelectedIndex >= 0)
            {
                disableAll();
                float v = float.Parse(vCombo.Text);
                string str = string.Format("G91 G00 Z-{0}", v);
                sendData(str);
                enableAll();
            }
        }
		// 現在のXY位置をホームポジションにする
        private void xyZeroBtn_Click(object sender, EventArgs e)
        {
            sendData("G92 X0 Y0");
        }
		// 現在のZ位置をホームポジションにする
        private void zZeroBtn_Click(object sender, EventArgs e)
        {
            sendData("G92 Z0");

        }
        private void waitIdle()
        {
            while (!idle)
            {
                Task.Delay(100);
            }
        }
        // ファイル選択
        private void fileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.FileName = filename.Text;
            ofd.InitialDirectory = @"";
            ofd.Filter = "Gaberファイル(*.grb;*.text)|*.grb;*.text|すべてのファイル(*.*)|*.*";
            ofd.FilterIndex = 2;
            ofd.Title = "開くファイルを選択してください";
            ofd.RestoreDirectory = true;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;

            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき、選択されたファイル名を表示する
                Console.WriteLine(ofd.FileName);
                filename.Text = ofd.FileName;
                this.Text = "NCTool : " + ofd.FileName;
                readBtn.Enabled = true;
            }
        }
		// スピンドルモーターON/OFF
        private void motorCtrl(bool flag)
        {
        	if (leaser) {
        		return;
        	}
            if (flag)
            {
                string str = string.Format("S{0}", spindle.Text);
                sendData(str, true);
                // 回転開始
                sendData("M03", true);
            }
            else
            {
                // 回転停止
                sendData("M05", true);
            }
        }
        // レーザーON/OFF
        private void leaserCtrl(bool flag)
        {
        	if (leaser == false) {
        		return;
        	}
            if (flag)
            {
                string str = string.Format("S{0}", spindle.Text);
                sendData(str, true);
                // 照射開始
                sendData("M03", true);
            }
            else
            {
                // 照射停止
                sendData("M05", true);
            }
        }

        // 穴あけ実行
        private void doHole(double cx, double cy)
        {
        	string str;

            if ((cx < 0) || (cy < 0) || (cx > workarea_x) || (cy > workarea_y))
            {
                return;
            }
            str = "HOLE: " + cx + "," + cy;
            Console.WriteLine(str);
            putLog("; " + str);
            //
            sendData("G90", true);    // 絶対モード
            // ヘッドを上げる
            sendData("G0 Z5", true); // 5ミリ
            // 目的の場所へ移動
            str = string.Format("G00 X{0} Y{1}", correction(cx), correction(cy));
            sendData(str, true);
            if (!testMode.Checked)
            {
                // 穴をあける
                double z = double.Parse(thickness.Text) + 0.5;
                str = string.Format("F{1} G01 Z-{0}", z, speed.Text);
                sendData(str, true);
                // ヘッドを上げる
                sendData("G0 Z5", true); // 5ミリ
            }
            cur_x = cx;
            cur_y = cy;
        }
        // 穴（円周切削）実行
        private void doCircle(double cx, double cy, double r)
        {
        	string str;
            double thick = 0.0;
            bool flag;
            //
        	double ex, ey;
        	double rx;
        	int steps = 64;
        	//
            if ((cx < 0) || (cy < 0) || (cx > workarea_x) || (cy > workarea_y) || (r < 0))
            {
                return;
            }
            str = "CIRCLE: " + cx + "," + cy + " " + r;
            Console.WriteLine(str);
            putLog("; " + str);
            if (r > 10.0)
            {
            	steps = 128;
            }
            //
            sendData("G90", true);    // 絶対モード
            //
            while (thick <= double.Parse(thickness.Text))
            {
                Console.WriteLine(thick + ":" + double.Parse(thickness.Text));
	            rx = 0.0;
            	ex = Math.Sin(rx) * r + cx;
            	ey = Math.Cos(rx) * r + cy;
	            // ヘッドを上げる
   		        //sendData("G0 Z5", true); // 5ミリ
       		    // 目的の場所へ移動
       		    str = string.Format("G00 X{0} Y{1}", correction(ex), correction(ey));
       		    sendData(str, true);
                // ヘッドを下げる
                if (!testMode.Checked)
                {
                    str = string.Format("F{1} G01 Z-{0}", thick, speed.Text);
                    sendData(str, true);
					leaserCtrl(true);
                }
                flag = false;
	            while (flag != true) {
	            	rx += (Math.PI * 2) / steps;
	            	if (rx > (Math.PI * 2)) {
	            		flag = true;
	            		rx = 0;
	            	}
	            	ex = Math.Sin(rx) * r + cx;
	            	ey = Math.Cos(rx) * r + cy;
	            	//
	                // 終点へ移動
                	str = string.Format("F{2} G01 X{0} Y{1}", correction(ex), correction(ey), speed.Text);
                	sendData(str, true);
	            }
                thick += double.Parse(step.Text);

                // ヘッドを下げる
                if (!testMode.Checked)
                {
                    str = string.Format("F{1} G01 Z-{0}", thick, speed.Text);
                    sendData(str, true);
                }
            }
            // ヘッドを上げる
			leaserCtrl(false);
            sendData("G0 Z5", true); // 5ミリ
            cur_x = cx;
            cur_y = cy;
        }
        // 移動
        private void doMove(double cx, double cy)
        {
        	string str;

            if ((cx < 0) || (cy < 0) || (cx > workarea_x) || (cy > workarea_y))
            {
                return;
            }
            str = "MOVE: " + cx + "," + cy;
            Console.WriteLine(str);
            putLog("; " + str);
            //
            sendData("G90", true);    // 絶対モード
            // 目的の場所へ移動
            str = string.Format("G00 X{0} Y{1}", correction(cx), correction(cy));
            sendData(str, true);
            //
            cur_x = cx;
            cur_y = cy;
        }
        // 切削実行
        private void doCut(double sx, double sy, double cx, double cy)
        {
            double thick = 0.0;
            string str;
            bool flag = true;

            if ((cx < 0) || (cy < 0) || (cx > workarea_x) || (cy > workarea_y))
            {
                return;
            }
            str = "CUT: " + sx + "," + sy + " - " + cx + "," + cy;
            Console.WriteLine(str);
            putLog("; " + str);
            //
            //sendData("G90", true);    // 絶対モード
            //doMove(sx, sy);
            // ヘッドを下げる
            if (!testMode.Checked)
            {
            //    str = string.Format("F{1} G01 Z-{0}", thick, speed.Text);
            //    sendData(str, true);
            }
            while (thick <= double.Parse(thickness.Text))
            {
                Console.WriteLine(thick + ":" + double.Parse(thickness.Text));
                if (flag) {
                    // 始点から
                    doMove(sx, sy);
                } else
                {
                    // 終点から
                    doMove(cx, cy);
                }
                // ヘッドを下げる
                if (!testMode.Checked)
                {
                    str = string.Format("F{1} G01 Z-{0}", thick, speed.Text);
                    sendData(str, true);
					leaserCtrl(true);
                }
                if (flag)
                {
                    // 終点へ移動
                    str = string.Format("F{2} G01 X{0} Y{1}", correction(cx), correction(cy), speed.Text);
                } else
                {
                    // 始点へ移動
                    str = string.Format("F{2} G01 X{0} Y{1}", correction(sx), correction(sy), speed.Text);
                }
                sendData(str, true);
                thick += double.Parse(step.Text);
                flag = !flag;   // 方向を変える
            }
            // ヘッドを上げる
			leaserCtrl(false);
            sendData("G0 Z5", true); // 5ミリ
            cur_x = cx;
            cur_y = cy;
        }
        // 円弧処理
        private void doArc(double sx, double sy, double ex, double ey, double vi, double vj, int gc)
        {
            double thick = 0.0;
            bool flag = true;
            int cmd;
            string str;
            double ii, jj;

            if ((sx < 0) || (sy < 0) || (sx > workarea_x) || (sy > workarea_y)) {
                return;
            }
        	if ((vi == 0.0) && (vj == 0.0) && (vr == 0.0)) {
        		return;
        	}
        	str = "ARC: " + sx + "," + sy + " " + ex + "," + ey + "  " + vi + "," + vj + " " + gc;
            Console.WriteLine(str);
            putLog("; " + str);

            sendData("G90", true);    // 絶対モード
            cmd = gc;
			//
			// 円弧切削
            while (thick <= double.Parse(thickness.Text))
            {
                Console.WriteLine(thick + ":" + double.Parse(thickness.Text));
	            // ヘッドを上げる
	            //sendData("G0 Z5", true); // 5ミリ
                if (flag)
                {
                    // 先頭へ移動
                    str = string.Format("G00 X{0} Y{1}", correction(sx), correction(sy));
                }
                else
                {
                    // 先頭へ移動
                    str = string.Format("G00 X{0} Y{1}", correction(ex), correction(ey));
                }
                sendData(str, true);

                // ヘッドを下げる
                if (!testMode.Checked)
                {
                    str = string.Format("F{1} G01 Z-{0}", thick, speed.Text);
                    sendData(str, true);
					leaserCtrl(true);
                }
                if (flag)
                {
                    ii = vi;
                    jj = vj;
                    // 終点へ移動
                    str = string.Format("F{2} G{5} X{0} Y{1} I{3} J{4}", correction(ex), correction(ey), speed.Text, correction(ii), correction(jj), cmd);
                } else {
                    ii = sx + vi;
                    jj = sy + vj;
                    ii = ii - ex;
                    jj = jj - ey;
                    // 終点へ移動
                    str = string.Format("F{2} G{5} X{0} Y{1} I{3} J{4}", correction(sx), correction(sy), speed.Text, correction(ii), correction(jj), cmd);
                }
                sendData(str, true);
                Console.WriteLine("Arc: " + str);
                //
                thick += double.Parse(step.Text);
                flag = !flag;   // 方向を変える
                if (cmd == 2)
                {
                    cmd = 3;
                }
                else
                {
                    cmd = 2;
                }
            }
            // ヘッドを上げる
			leaserCtrl(false);
            sendData("G0 Z5", true); // 5ミリ
            cur_x = ex;
            cur_y = ey;
        }
		// 処理実行開始
        private void execBtn_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                MessageBox.Show("ポートがオープンされていません", "エラー");
                return;
            }
            resumeBtn.Enabled = false;
            abortBtn.Enabled = true;

            leaser = leaserMode.Checked;

            fileThread = new Thread(new ThreadStart(() =>
            {
                execStart();
            }));
            
            resumeBtn.Enabled = true;
            abortBtn.Enabled = true;
            execBtn.Enabled = false;

            fileThread.Start();
        }
        private double arctan(double x, double y)
        {
            Console.WriteLine("atan: " + x + "," + y);
            if (x == 0.0)
            {
                if (y > 0.0)
                {
                    return Math.PI / 2;
                }
                return -(Math.PI / 2);
            } else if (y == 0.0)
            {
                if (x > 0.0)
                {
                    return 0;
                }
                return Math.PI;
            } else if (x < 0.0)
            {
                return Math.Atan(x / y);
            }
            else
            {
                return Math.Atan(x / y) + Math.PI;
            }
            return 0.0;
        }
		// 処理実行スレッド
        private void execStart()
        {
            double ofx = double.Parse(offsetX.Text);
            double ofy = double.Parse(offsetY.Text);
            double target_x = 0.0;
            double target_y = 0.0;
            int target_item = 0;
            double target_dist = 0.0;

            DateTime dt = DateTime.Now;
            string time = string.Format("{0:0000}{1:00}{2:00}{3:00}{4:00}{5:00}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
	        putLog("% ; Start " + time);

            foreach (KeyValuePair<string, string> t in tools)
            {
                if (fileAbort)
                {
                    break;
                }
                Console.WriteLine("{0} : {1}", t.Key, t.Value);
                DialogResult result = MessageBox.Show(t.Value + "ツールを実行しますか？",
                	"ツール", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                	string str = "; TOOL " + t.Key + " " + t.Value;
       		        putLog(str);

                    Console.WriteLine("「はい」が選択されました");
                    motorCtrl(false);
                    doMove(0.0, 0.0);
                    //MessageBox.Show(string.Format("ツールの準備ができたらresumeを押してください: " + t.Key));
                    Invoke((MethodInvoker)delegate {
                        resumeBtn.Enabled = true;
                        abortBtn.Enabled = false;
                    });
                    // 実行待ち
                    fileThread.Suspend();
                    // 実行開始
                    Invoke((MethodInvoker)delegate {
                        resumeBtn.Enabled = false;
                        abortBtn.Enabled = true;
                    });
                    if (!testMode.Checked)
                    {
                        motorCtrl(true);
                    }
                    // 対象を未実行に
                    // 一番小さいXY位置を得る、現在位置にする
                    //target_x = 0.0;
                    //target_y = 0.0;
                    target_x = double.Parse(offsetX.Text);  // 開始点をオフセット位置にする
                    target_y = double.Parse(offsetY.Text);
                    target_item = -1;
                    target_dist = 1000.0;
                    // 全てを未実行にする
                    for (int i = 0; i < actions.Count; i++)
                    {
                        if (actions[i].tool == t.Key.Substring(0, 3))
                        {
                            actions[i].done = false;
                            double dist = Math.Sqrt(actions[i].x * actions[i].x + actions[i].y * actions[i].y);
                            if (dist < target_dist)
                            {
                                target_item = i;
                                target_dist = dist;
                            }
                        }
                    }
                    // ターゲットがなくなるまで実行する
                    while (true)
                    {
                        // 現在のターゲットに最も近い次のターゲットを見つける
                        //target_x = actions[i].x;
                        //target_y = actions[i].y;
                        // 原点に最も近い次のターゲットを見つける
                        //target_x = double.Parse(offsetX.Text);  // 原点をオフセット位置にする
                        //target_y = double.Parse(offsetY.Text);
                        target_dist = 10000.0;
                        target_item = -1;
                        for (int j = 0; j < actions.Count; j++)
                        {
                            if ((actions[j].tool == t.Key.Substring(0, 3)) && (actions[j].done != true))
                            {
                                double x = actions[j].x - target_x;
                                double y = actions[j].y - target_y;
                                double dist = Math.Sqrt(x * x + y * y);
                                if (dist < target_dist)
                                {
                                    target_item = j;
                                    target_dist = dist;
                                }
                            }
                        }
                        if  (target_item < 0) {
                            break;
                        }

                        int i = target_item;
                        // 処理の実行
                        if (actions[i].act == "move")
                        {
                        	string s = actions[i].act + " = " + actions[i].x + "," + actions[i].y + ":" + actions[i].tool + "(" + actions[i].gcode + ")";
                            Console.WriteLine(s);
					        putLog("; " + s);
                            if ((actions[i].x == 0) && (actions[i].y == 0.0))
                            {
                                doMove(actions[i].y, actions[i].x);
                            }
                            else if (flipXY.Checked)
                            {
                            //    doMove(actions[i].y - ofy, actions[i].x - ofx);
                            }
                            else
                            {
                            //    doMove(actions[i].x - ofx, actions[i].x - ofy);
                            }
                        }
                        else if (actions[i].act == "cut")
                        {
                        	string s = actions[i].act + " = " + actions[i].sx + "," + actions[i].sy + "-" + actions[i].x + "," + actions[i].y + ":" + actions[i].tool + "(" + actions[i].gcode + ")";
                            Console.WriteLine(s);
					        putLog("; " + s);
                        	double w = double.Parse(t.Value);	// 幅
                            if (leaser ||(w >= 2.0)) {		// 幅2ミリ以上は周囲切削
                            	double p1x, p1y;
                            	double p2x, p2y;
                            	double p3x, p3y;
                            	double p4x, p4y;
								double dx, dy;
								double l;
								dx = actions[i].sx - actions[i].x;
								dy = actions[i].sy - actions[i].y;
								l = Math.Sqrt((dx * dx) + (dy * dy));
								p1x = actions[i].sx - ((dy / l) * (w / 2));
								p1y = actions[i].sy + ((dx / l) * (w / 2));
								p2x = actions[i].sx + ((dy / l) * (w / 2));
								p2y = actions[i].sy - ((dx / l) * (w / 2));
								p3x = actions[i].x - ((dy / l) * (w / 2));
								p3y = actions[i].y + ((dx / l) * (w / 2));
								p4x = actions[i].x + ((dy / l) * (w / 2));
								p4y = actions[i].y - ((dx / l) * (w / 2));
	                            if (flipXY.Checked)
	                            {
	                                doCut(p1y - ofy, p1x - ofx, p3y - ofy, p3x - ofx);
	                                //doCut(p3y - ofy, p3x - ofx, p4y - ofy, p4x - ofx);  // ARC
                                    doArc(p3y - ofy, p3x - ofx, p4y - ofy, p4x - ofx, actions[i].y - p3y, actions[i].x - p3x, 2);
                                    doCut(p4y - ofy, p4x - ofx, p2y - ofy, p2x - ofx);
	                                //doCut(p2y - ofy, p2x - ofx, p1y - ofy, p1x - ofx);	// ARC
                                    doArc(p2y - ofy, p2x - ofx, p1y - ofy, p1x - ofx, actions[i].sy - p2y, actions[i].sx - p2x, 2);
                                }
                                else
	                            {
	                                doCut(p1x - ofx, p1y - ofy, p3x - ofx, p3y - ofy);
	                                //doCut(p3x - ofx, p3y - ofy, p4x - ofx, p4y - ofy);	// ARC
                                    doArc(p3x - ofx, p3y - ofy, p4x - ofx, p4y - ofy, actions[i].x - p3x, actions[i].y - p3y, 3);
                                    doCut(p4x - ofx, p4y - ofy, p2x - ofx, p2y - ofy);
	                                //doCut(p2x - ofx, p2y - ofy, p1x - ofx, p1y - ofy);	// ARC
                                    doArc(p2x - ofx, p2y - ofy, p1x - ofx, p1y - ofy, actions[i].sx - p2x, actions[i].sy - p2y, 3);
                                }
                            } else {
	                            if (flipXY.Checked)
	                            {
	                                doCut(actions[i].sy - ofy, actions[i].sx - ofx, actions[i].y - ofy, actions[i].x - ofx);
	                            }
	                            else
	                            {
	                                doCut(actions[i].sx - ofx, actions[i].sy - ofy, actions[i].x - ofx, actions[i].y - ofy);
	                            }
	                        }
                        }
                        else if (actions[i].act == "dril")
                        {
                        	string s = actions[i].act + " = " + actions[i].x + "," + actions[i].y + ":" + actions[i].tool + "(" + actions[i].gcode + ")";
                            Console.WriteLine(s);
					        putLog("; " + s);
                        	double r = double.Parse(t.Value);
                            if (leaser ||(r >= 2.0)) {		// 直径2ミリ以上は円周切削、レーザーモードの場合も
                            	// 円周切削
	                            if (flipXY.Checked)
	                            {
	                                doCircle(actions[i].y - ofy, actions[i].x - ofx, r / 2.0);
	                            }
	                            else
	                            {
	                                doCircle(actions[i].x - ofx, actions[i].y - ofy, r / 2.0);
	                            }
                            } else {	// 穴あけ
	                            if (flipXY.Checked)
	                            {
	                                doHole(actions[i].y - ofy, actions[i].x - ofx);
	                            }
	                            else
	                            {
	                                doHole(actions[i].x - ofx, actions[i].y - ofy);
	                            }
	                        }
                        }
                        else if (actions[i].act == "arc")
                        {
                        	string s = actions[i].act + " = " + actions[i].sx + "," + actions[i].sy + " : " + actions[i].x + "," + actions[i].y + " : " + actions[i].i + "," + actions[i].j + "-" + actions[i].g + ":" + actions[i].tool + "(" + actions[i].gcode + ")";
                            Console.WriteLine(s);
					        putLog("; " + s);
                        	double w = double.Parse(t.Value);	// 幅
                            //if (leaser ||(w >= 2.0)) {      // 幅2ミリ以上は周囲切削
                            if (w >= 2.0) {       // 幅2ミリ以上は周囲切削
                                double cx, cy;  // 中心
                                double sr, er;  // 角度
                                double r;   // 半径
                                double p1x, p1y;
                                double p2x, p2y;
                                double p3x, p3y;
                                double p4x, p4y;
								double sx, sy, x, y;
                                int g, g1, g2;
                                Console.WriteLine("開始: " + actions[i].sx + "," + actions[i].sy);
                                Console.WriteLine("終了: " + actions[i].x + "," + actions[i].y);
	                            if (flipXY.Checked)
	                            {
	                            	sx = actions[i].sy;
	                            	sy = actions[i].sx;
	                            	x = actions[i].y;
	                            	y = actions[i].x;
                                	cx = sx + actions[i].j;
                                	cy = sy + actions[i].i;
                                	r = Math.Sqrt(actions[i].i * actions[i].i + actions[i].j * actions[i].j);
                                	sr = arctan(sx - cx, sy - cy);
                                	er = arctan(x - cx, y - cy);
                                    g = (actions[i].g == 2) ? 3 : 2;
                                    g1 = 3;
                                    g2 = 2;
                                } else {
	                            	sx = actions[i].sx;
	                            	sy = actions[i].sy;
	                            	x = actions[i].x;
	                            	y = actions[i].y;
                                	cx = actions[i].sx + actions[i].i;
                                	cy = actions[i].sy + actions[i].j;
                                    r = Math.Sqrt(actions[i].i * actions[i].i + actions[i].j * actions[i].j);
                                    sr = arctan(sx - cx, sy - cy);
                                	er = arctan(x - cx, y - cy);
                                    g = actions[i].g;
                                    g1 = 2;
                                    g2 = 3;
                                }
                                Console.WriteLine("中心: " + cx + "," + cy + "　半径: " + r);
                                Console.WriteLine("角度: " + sr + "  " + er);

                                p1x = Math.Cos(sr) * (r + w / 2.0) + cx;
                                p1y = Math.Sin(sr) * (r + w / 2.0) + cy;
                                Console.WriteLine("P1: " + p1x + "," + p1y);
                                p2x = Math.Cos(sr) * (r - w / 2.0) + cx;
                                p2y = Math.Sin(sr) * (r - w / 2.0) + cy;
                                Console.WriteLine("P2: " + p2x + "," + p2y);
                                p3x = Math.Cos(er) * (r + w / 2.0) + cx;
                                p3y = Math.Sin(er) * (r + w / 2.0) + cy;
                                Console.WriteLine("P3: " + p3x + "," + p3y);
                                p4x = Math.Cos(er) * (r - w / 2.0) + cx;
                                p4y = Math.Sin(er) * (r - w / 2.0) + cy;
                                Console.WriteLine("P4: " + p4x + "," + p4y);
                                //
                                doArc(p1x - ofx, p1y - ofy, p3x - ofx, p3y - ofy, cx - p1x, cy - p1y, g);
                                doArc(p1x - ofx, p1y - ofy, p2x - ofx, p2y - ofy, sx - p1x, sy - p1y, g1);
                                doArc(p2x - ofx, p2y - ofy, p4x - ofx, p4y - ofy, cx - p2x, cy - p2y, g);
                                doArc(p3x - ofx, p3y - ofy, p4x - ofx, p4y - ofy, x - p3x, y - p3y, g2);
                            }
                            else {
	                            // 円弧・円
	                            if (flipXY.Checked)
	                            {
		                        	doArc(actions[i].sy - ofy, actions[i].sx - ofx, actions[i].y - ofx, actions[i].x - ofy, actions[i].j, actions[i].i, (actions[i].g == 2) ? 3 : 2);
		                        } else {
		                        	doArc(actions[i].sx - ofx, actions[i].sy - ofy, actions[i].x - ofx, actions[i].y - ofy, actions[i].i, actions[i].j, actions[i].g);
		                        }
	                        }
                        }
                        // 実行済みに
                        actions[i].done = true;
                        // 次の開始点を探す起点
                        target_x = actions[i].x;
                        target_y = actions[i].y;
                    }
                    if (!testMode.Checked)
                    {
                        motorCtrl(false);
                    }
                    doMove(0.0, 0.0);   // 原点へ移動
                }
                else if (result == DialogResult.No)
                {
                    Console.WriteLine("「いいえ」が選択されました");
                }
                else if (result == DialogResult.Cancel)
                {
                    Console.WriteLine("「キャンセル」が選択されました");
                    break;
                }
            }
            dt = DateTime.Now;
            time = string.Format("{0:0000}{1:00}{2:00}{3:00}{4:00}{5:00}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
	        putLog("% ; End " + time);
            Invoke((MethodInvoker)delegate {
                resumeBtn.Enabled = false;
                abortBtn.Enabled = false;
                execBtn.Enabled = true;
            });
        }

        private void abortBtn_Click(object sender, EventArgs e)
        {
            fileAbort = true;
            serialPort1.Write("\x18$X");
        }

        private void resumeBtn_Click(object sender, EventArgs e)
        {
            fileThread.Resume();
        }

        private void readBtn_Click(object sender, EventArgs e)
        {
        	if (filename.Text == "") {
        		MessageBox.Show("ファイルを選択してください", "エラー");
                return;
			}
            try
            {
	            sr = new StreamReader(filename.Text, Encoding.GetEncoding("UTF-8"));
            } catch
            {
                MessageBox.Show("ファイルがオープンできません", "エラー");
                return;
            }

            fileExcecute();

            sr.Close();
            MessageBox.Show("ファイルの読み込みが終わりました。");
            resumeBtn.Enabled = false;
            abortBtn.Enabled = false;
            execBtn.Enabled = true;
        }
		// About dialog
        private void label14_Click(object sender, EventArgs e)
        {
            MessageBox.Show(VersionInfo, "About NCTool");
        }

        // 補正処理（演算誤差吸収）
        private double correction(double v)
        {
            // 一定値以下なら0とする
            if (Math.Abs(v) < 0.00001)
            {
                v = 0.0;
            }
            return v;
        }
        private void materialCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < materials.Count; i++) {
                if (materials[i].name == materialCombo.Text) {
                    speed.Text = materials[i].speed;
                    step.Text = materials[i].step;
                    break;
                }
            }
        }
		// G-Codeファイルを読み込む
        private void fileExcecute()
        {
            double ofx, ofy;
            tools = new Dictionary<string, string>();
            actions = new List<action>();
            string tool = "";

            ofx = double.Parse(offsetX.Text);
            ofy = double.Parse(offsetY.Text);
            fileAbort = false;

            while (sr.EndOfStream == false)
            {
                if (fileAbort)
                {
                    break;
                }
                string line = sr.ReadLine().Trim();
                //Console.WriteLine(line);
                if (line.StartsWith("%"))
                {
                    // %ASAXBY*OFA0B0*MOMM*FSLAX43Y43*IPPOS*%
                    string[] ary = line.Substring(1, line.Length - 2).Split('*');
                    for (int i = 0; i < ary.Length; i++)
                    {
                        if (ary[i].Length == 0)
                        {
                            continue;
                        }
                        Console.WriteLine(ary[i]);
                        if (ary[i].StartsWith("AS"))
                        {
                            // ASAXBY
                        }
                        else if (ary[i].StartsWith("OF"))
                        {
                        	// OFA0B0 オフセット
                        }
                        else if (ary[i].StartsWith("MO"))
                        {
                        	// MOMM 単位 MM/IN
							unit = ary[i].Substring(2, 2);
                        }
                        else if (ary[i].StartsWith("FS"))
                        {
                        	// FSLAX43Y43 数値表記(整数4桁、小数3桁)
                            string s = ary[i].Substring(4,3);
                            Console.WriteLine(s);
                            if (s.StartsWith("X"))
                            {
                                xdigit1 = int.Parse(s.Substring(1, 1));
                                xdigit2 = int.Parse(s.Substring(2, 1));
                            }
                            s = ary[i].Substring(7,3);
                            Console.WriteLine(s);
                            if (s.StartsWith("Y"))
                            {
                                ydigit1 = int.Parse(s.Substring(1, 1));
                                ydigit2 = int.Parse(s.Substring(2, 1));
                            }
                        }
                        else if (ary[i].StartsWith("IP"))
                        {
                        	// IPPOS
                        }
                        else if (ary[i].StartsWith("AD"))
                        {
                        	// ADD11C,0.100 ツール
                            string key;
                            string size;
                            string[] t = ary[i].Split(',');
                            key = t[0].Substring(2);
                            size = t[1];
                            tools.Add(key, size);
                        }
                        else
                        {
                            Console.WriteLine("Unknown: [" + ary[i] + "]");
                        }
                    }
                }
                else
                {
                    string[] ary = line.Split('*');
                    for (int i = 0; i < ary.Length; i++)
                    {
                        string str = ary[i];
                        string cmd = "";
                        while (str.Length > 0)
                        {
                            if (str.StartsWith("G"))
                            {
                                cmd = getCmd(str);
                                str = str.Substring(cmd.Length);
                                Console.WriteLine(cmd);
                                if (cmd == "G54")
                                {
                                    // 現在のツールを設定
                                    tool = str.Trim();
                                }
                                if (cmd == "G00")
                                {
                                	gc = 0;		// move
                                }
                                if (cmd == "G01")
                                {
                                	gc = 1;		// line
                                }
                                if (cmd == "G02")
                                {
                                	gc = 2;		// arc clockwise
                                }
                                if (cmd == "G03")
                                {
                                	gc = 3;		// arc counterclockwise
                                }
                            }
                            else if (str.StartsWith("D"))
                            {
                                cmd = getCmd(str);
                                str = str.Substring(cmd.Length);
                                Console.WriteLine(cmd);
                                if (cmd == "D03")
                                {
                                    Console.WriteLine(string.Format("Dril {0:f4}-{1:f4}", x, y));
                                    actions.Add(new action() {tool = tool, act = "dril", x = x, y = y , gcode = line});
                                    cur_x = x;
                                    cur_y = y;

                                }
                                if (cmd == "D02")
                                {
                                    Console.WriteLine(string.Format("Move {0:f4}-{1:f4}", x, y));
                                    actions.Add(new action() { tool = tool, act = "move", x = x, y = y , gcode = line });
                                    cur_x = x;
                                    cur_y = y;
                                }
                                if (cmd == "D01")
                                {
                                	if (gc < 2) {
	                                    Console.WriteLine(string.Format("Cut {0:f4}-{1:f4}", x, y));
	                                    actions.Add(new action() { tool = tool, act = "cut", x = x, y = y, sx = cur_x, sy = cur_y , gcode = line });
	                                } else {
	                                    Console.WriteLine(string.Format("Arc {0:f4}-{1:f4}", x, y));
	                                    actions.Add(new action() { tool = tool, act = "arc", x = x, y = y, sx = cur_x, sy = cur_y, i = vi, j = vj, r = vr, g = gc , gcode = line });
	                                }
                                    cur_x = x;
                                    cur_y = y;
                                }
                            }
                            else if (str.StartsWith("X"))
                            {
                                cmd = getCmd(str);
                                str = str.Substring(cmd.Length);
                                //Console.WriteLine(cmd);
                                x = getNumb(cmd.Substring(1), xdigit1, xdigit2);
                            }
                            else if (str.StartsWith("Y"))
                            {
                                cmd = getCmd(str);
                                str = str.Substring(cmd.Length);
                                //Console.WriteLine(cmd);
                                y = getNumb(cmd.Substring(1), ydigit1, ydigit2);
                            }
                            else if (str.StartsWith("M"))
                            {
                                cmd = getCmd(str);
                                str = str.Substring(cmd.Length);
                                //Console.WriteLine(cmd);
                            }
                            else if (str.StartsWith("I"))
                            {
                                cmd = getCmd(str);
                                str = str.Substring(cmd.Length);
                                Console.WriteLine(cmd);
                                vi = getNumb(cmd.Substring(1), xdigit1, xdigit2);
                            }
                            else if (str.StartsWith("J"))
                            {
                                cmd = getCmd(str);
                                str = str.Substring(cmd.Length);
                                //Console.WriteLine(cmd);
                                vj = getNumb(cmd.Substring(1), ydigit1, ydigit2);
                            }
                            else if (str.StartsWith("R"))
                            {
                                cmd = getCmd(str);
                                str = str.Substring(cmd.Length);
                                //Console.WriteLine(cmd);
                                vr = getNumb(cmd.Substring(1), xdigit1, xdigit2);
                            }
                            else
                            {
                                Console.WriteLine("Unknown: [" + str + "]");
                                str = "";
                                //Console.WriteLine(cmd);
                                MessageBox.Show("不明のデータ：" + str);
                                MessageBox.Show("フォーマットが不明のため、ファイルの処理を中止しました。");
                                return;
                            }
                        }
                    }
                }
            }
        }
        // 手動穴あけ
        private void holeProc()
        {
            double x, y, r;
            double ofx;
            double ofy;

            try {
                ofx = double.Parse(offsetX.Text);
                ofy = double.Parse(offsetY.Text);
                x = float.Parse(targetX.Text);
                y = float.Parse(targetY.Text);
                r = float.Parse(targetR.Text);
            }
            catch
            {
                MessageBox.Show("入力に誤りがあります", "エラー");
                return;
            }

            idle = true;
	        if (!testMode.Checked)
	        {
                motorCtrl(true);
                //string str = string.Format("S{0}", spindle.Text);
                //sendData(str);
                //sendData("M3");
            }
            if (leaser ||(r >= 2.0)) {		// 直径2ミリを超える場合は円周切削、レーザーモードの場合も
	           	// 円周切削
                if (flipXY.Checked)
	            {
	                doCircle(y - ofy, x - ofx, r / 2.0);
	            }
	            else
	            {
	            	doCircle(x - ofx, y - ofy, r / 2.0);
	            }
            } else {	// 穴あけ
	        	if (flipXY.Checked)
	            {
	            	doHole(y - ofy, x - ofx);
	            }
	            else
	            {
	            	doHole(x - ofx, y - ofy);
	            }
	        }
            motorCtrl(false);
            //sendData("M5");
            doMove(0.0, 0.0);   // 原点へ移動
        }

        private void holeBtn_Click(object sender, EventArgs e)
        {
        	Thread holeThread;
            if (!serialPort1.IsOpen)
            {
                MessageBox.Show("ポートがオープンされていません", "エラー");
                return;
            }
            leaser = leaserMode.Checked;

            holeThread = new Thread(new ThreadStart(() =>
            {
                holeProc();
            }));

            holeThread.Start();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //writer.Close();
        }
    }

    public class action
    {
        public string tool { get; set; }
        public string act { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public double sx { get; set; }
        public double sy { get; set; }
        public bool done { get; set; }
        public double i { get; set; }
        public double j { get; set; }
        public double r { get; set; }
        public int g { get; set; }
        public string gcode { get; set; }
    }
    public class material
    {
        public string name { get; set; }
        public string speed { get; set; }
        public string step { get; set; }
    }
}
