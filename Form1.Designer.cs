namespace NCTool
{
    partial class NCTool
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NCTool));
            this.connectBtn = new System.Windows.Forms.Button();
            this.logText = new System.Windows.Forms.TextBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.sendText = new System.Windows.Forms.TextBox();
            this.sendBtn = new System.Windows.Forms.Button();
            this.frontBtn = new System.Windows.Forms.Button();
            this.homeBtn = new System.Windows.Forms.Button();
            this.rearBtn = new System.Windows.Forms.Button();
            this.leftBtn = new System.Windows.Forms.Button();
            this.rightBtn = new System.Windows.Forms.Button();
            this.upBtn = new System.Windows.Forms.Button();
            this.downBtn = new System.Windows.Forms.Button();
            this.hCombo = new System.Windows.Forms.ComboBox();
            this.vCombo = new System.Windows.Forms.ComboBox();
            this.yPos = new System.Windows.Forms.TextBox();
            this.zPos = new System.Windows.Forms.TextBox();
            this.xPos = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.xyZeroBtn = new System.Windows.Forms.Button();
            this.zZeroBtn = new System.Windows.Forms.Button();
            this.materialCombo = new System.Windows.Forms.ComboBox();
            this.thickness = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.speed = new System.Windows.Forms.TextBox();
            this.step = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.targetX = new System.Windows.Forms.TextBox();
            this.targetR = new System.Windows.Forms.TextBox();
            this.targetY = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.holeBtn = new System.Windows.Forms.Button();
            this.offsetX = new System.Windows.Forms.TextBox();
            this.offsetY = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.flipXY = new System.Windows.Forms.CheckBox();
            this.testMode = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.resumeBtn = new System.Windows.Forms.Button();
            this.abortBtn = new System.Windows.Forms.Button();
            this.execBtn = new System.Windows.Forms.Button();
            this.filename = new System.Windows.Forms.TextBox();
            this.fileBtn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.leaserMode = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.spindle = new System.Windows.Forms.TextBox();
            this.readBtn = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.comCombo = new System.Windows.Forms.ComboBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(140, 41);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(75, 23);
            this.connectBtn.TabIndex = 1;
            this.connectBtn.Text = "Connect";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // logText
            // 
            this.logText.Location = new System.Drawing.Point(25, 86);
            this.logText.Multiline = true;
            this.logText.Name = "logText";
            this.logText.Size = new System.Drawing.Size(206, 256);
            this.logText.TabIndex = 2;
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // sendText
            // 
            this.sendText.Location = new System.Drawing.Point(7, 19);
            this.sendText.Name = "sendText";
            this.sendText.Size = new System.Drawing.Size(206, 19);
            this.sendText.TabIndex = 3;
            // 
            // sendBtn
            // 
            this.sendBtn.Location = new System.Drawing.Point(230, 17);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(91, 23);
            this.sendBtn.TabIndex = 4;
            this.sendBtn.Text = "Send command";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // frontBtn
            // 
            this.frontBtn.Location = new System.Drawing.Point(112, 83);
            this.frontBtn.Name = "frontBtn";
            this.frontBtn.Size = new System.Drawing.Size(75, 23);
            this.frontBtn.TabIndex = 5;
            this.frontBtn.Text = "FRONT";
            this.frontBtn.UseVisualStyleBackColor = true;
            this.frontBtn.Click += new System.EventHandler(this.frontBtn_Click);
            // 
            // homeBtn
            // 
            this.homeBtn.Location = new System.Drawing.Point(112, 49);
            this.homeBtn.Name = "homeBtn";
            this.homeBtn.Size = new System.Drawing.Size(75, 23);
            this.homeBtn.TabIndex = 6;
            this.homeBtn.Text = "HOME";
            this.homeBtn.UseVisualStyleBackColor = true;
            this.homeBtn.Click += new System.EventHandler(this.homeBtn_Click);
            // 
            // rearBtn
            // 
            this.rearBtn.Location = new System.Drawing.Point(112, 15);
            this.rearBtn.Name = "rearBtn";
            this.rearBtn.Size = new System.Drawing.Size(75, 23);
            this.rearBtn.TabIndex = 7;
            this.rearBtn.Text = "REAR";
            this.rearBtn.UseVisualStyleBackColor = true;
            this.rearBtn.Click += new System.EventHandler(this.rearBtn_Click);
            // 
            // leftBtn
            // 
            this.leftBtn.Location = new System.Drawing.Point(8, 49);
            this.leftBtn.Name = "leftBtn";
            this.leftBtn.Size = new System.Drawing.Size(75, 23);
            this.leftBtn.TabIndex = 8;
            this.leftBtn.Text = "LEFT";
            this.leftBtn.UseVisualStyleBackColor = true;
            this.leftBtn.Click += new System.EventHandler(this.leftBtn_Click);
            // 
            // rightBtn
            // 
            this.rightBtn.Location = new System.Drawing.Point(211, 49);
            this.rightBtn.Name = "rightBtn";
            this.rightBtn.Size = new System.Drawing.Size(75, 23);
            this.rightBtn.TabIndex = 9;
            this.rightBtn.Text = "RIGHT";
            this.rightBtn.UseVisualStyleBackColor = true;
            this.rightBtn.Click += new System.EventHandler(this.rightBtn_Click);
            // 
            // upBtn
            // 
            this.upBtn.Location = new System.Drawing.Point(47, 15);
            this.upBtn.Name = "upBtn";
            this.upBtn.Size = new System.Drawing.Size(75, 23);
            this.upBtn.TabIndex = 11;
            this.upBtn.Text = "UP";
            this.upBtn.UseVisualStyleBackColor = true;
            this.upBtn.Click += new System.EventHandler(this.upBtn_Click);
            // 
            // downBtn
            // 
            this.downBtn.Location = new System.Drawing.Point(47, 83);
            this.downBtn.Name = "downBtn";
            this.downBtn.Size = new System.Drawing.Size(75, 23);
            this.downBtn.TabIndex = 12;
            this.downBtn.Text = "DOWN";
            this.downBtn.UseVisualStyleBackColor = true;
            this.downBtn.Click += new System.EventHandler(this.downBtn_Click);
            // 
            // hCombo
            // 
            this.hCombo.FormattingEnabled = true;
            this.hCombo.Location = new System.Drawing.Point(121, 118);
            this.hCombo.Name = "hCombo";
            this.hCombo.Size = new System.Drawing.Size(42, 20);
            this.hCombo.TabIndex = 13;
            // 
            // vCombo
            // 
            this.vCombo.FormattingEnabled = true;
            this.vCombo.Location = new System.Drawing.Point(62, 118);
            this.vCombo.Name = "vCombo";
            this.vCombo.Size = new System.Drawing.Size(40, 20);
            this.vCombo.TabIndex = 14;
            // 
            // yPos
            // 
            this.yPos.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.yPos.Location = new System.Drawing.Point(141, 12);
            this.yPos.Name = "yPos";
            this.yPos.ReadOnly = true;
            this.yPos.Size = new System.Drawing.Size(92, 26);
            this.yPos.TabIndex = 15;
            this.yPos.TabStop = false;
            this.yPos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // zPos
            // 
            this.zPos.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.zPos.Location = new System.Drawing.Point(13, 13);
            this.zPos.Name = "zPos";
            this.zPos.ReadOnly = true;
            this.zPos.Size = new System.Drawing.Size(83, 26);
            this.zPos.TabIndex = 16;
            this.zPos.TabStop = false;
            this.zPos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // xPos
            // 
            this.xPos.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.xPos.Location = new System.Drawing.Point(26, 12);
            this.xPos.Name = "xPos";
            this.xPos.ReadOnly = true;
            this.xPos.Size = new System.Drawing.Size(88, 26);
            this.xPos.TabIndex = 17;
            this.xPos.TabStop = false;
            this.xPos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(125, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 12);
            this.label2.TabIndex = 19;
            this.label2.Text = "Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(604, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "Z";
            // 
            // xyZeroBtn
            // 
            this.xyZeroBtn.Location = new System.Drawing.Point(243, 13);
            this.xyZeroBtn.Name = "xyZeroBtn";
            this.xyZeroBtn.Size = new System.Drawing.Size(52, 23);
            this.xyZeroBtn.TabIndex = 21;
            this.xyZeroBtn.Text = "ZERO";
            this.xyZeroBtn.UseVisualStyleBackColor = true;
            this.xyZeroBtn.Click += new System.EventHandler(this.xyZeroBtn_Click);
            // 
            // zZeroBtn
            // 
            this.zZeroBtn.Location = new System.Drawing.Point(108, 14);
            this.zZeroBtn.Name = "zZeroBtn";
            this.zZeroBtn.Size = new System.Drawing.Size(48, 23);
            this.zZeroBtn.TabIndex = 22;
            this.zZeroBtn.Text = "ZERO";
            this.zZeroBtn.UseVisualStyleBackColor = true;
            this.zZeroBtn.Click += new System.EventHandler(this.zZeroBtn_Click);
            // 
            // materialCombo
            // 
            this.materialCombo.FormattingEnabled = true;
            this.materialCombo.Location = new System.Drawing.Point(73, 13);
            this.materialCombo.Name = "materialCombo";
            this.materialCombo.Size = new System.Drawing.Size(121, 20);
            this.materialCombo.TabIndex = 23;
            this.materialCombo.SelectedIndexChanged += new System.EventHandler(this.materialCombo_SelectedIndexChanged);
            // 
            // thickness
            // 
            this.thickness.Location = new System.Drawing.Point(73, 37);
            this.thickness.Name = "thickness";
            this.thickness.Size = new System.Drawing.Size(43, 19);
            this.thickness.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 12);
            this.label4.TabIndex = 25;
            this.label4.Text = "Material";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 12);
            this.label5.TabIndex = 26;
            this.label5.Text = "Thick";
            // 
            // speed
            // 
            this.speed.Location = new System.Drawing.Point(73, 61);
            this.speed.Name = "speed";
            this.speed.Size = new System.Drawing.Size(74, 19);
            this.speed.TabIndex = 27;
            // 
            // step
            // 
            this.step.Location = new System.Drawing.Point(73, 84);
            this.step.Name = "step";
            this.step.Size = new System.Drawing.Size(43, 19);
            this.step.TabIndex = 28;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 12);
            this.label6.TabIndex = 29;
            this.label6.Text = "Speed";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 87);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 12);
            this.label7.TabIndex = 30;
            this.label7.Text = "Step";
            // 
            // targetX
            // 
            this.targetX.Location = new System.Drawing.Point(277, 229);
            this.targetX.Name = "targetX";
            this.targetX.Size = new System.Drawing.Size(62, 19);
            this.targetX.TabIndex = 31;
            // 
            // targetR
            // 
            this.targetR.Location = new System.Drawing.Point(277, 279);
            this.targetR.Name = "targetR";
            this.targetR.Size = new System.Drawing.Size(62, 19);
            this.targetR.TabIndex = 31;
            // 
            // targetY
            // 
            this.targetY.Location = new System.Drawing.Point(277, 254);
            this.targetY.Name = "targetY";
            this.targetY.Size = new System.Drawing.Size(62, 19);
            this.targetY.TabIndex = 31;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(261, 233);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(12, 12);
            this.label8.TabIndex = 25;
            this.label8.Text = "X";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(261, 258);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(12, 12);
            this.label9.TabIndex = 25;
            this.label9.Text = "Y";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(258, 282);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 25;
            this.label10.Text = "φ";
            // 
            // holeBtn
            // 
            this.holeBtn.Location = new System.Drawing.Point(263, 305);
            this.holeBtn.Name = "holeBtn";
            this.holeBtn.Size = new System.Drawing.Size(75, 23);
            this.holeBtn.TabIndex = 32;
            this.holeBtn.Text = "Dril";
            this.holeBtn.UseVisualStyleBackColor = true;
            this.holeBtn.Click += new System.EventHandler(this.holeBtn_Click);
            // 
            // offsetX
            // 
            this.offsetX.Location = new System.Drawing.Point(269, 13);
            this.offsetX.Name = "offsetX";
            this.offsetX.Size = new System.Drawing.Size(100, 19);
            this.offsetX.TabIndex = 36;
            // 
            // offsetY
            // 
            this.offsetY.Location = new System.Drawing.Point(269, 37);
            this.offsetY.Name = "offsetY";
            this.offsetY.Size = new System.Drawing.Size(100, 19);
            this.offsetY.TabIndex = 36;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(218, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 12);
            this.label11.TabIndex = 37;
            this.label11.Text = "Offset X";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(218, 41);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(48, 12);
            this.label12.TabIndex = 37;
            this.label12.Text = "Offset Y";
            // 
            // flipXY
            // 
            this.flipXY.AutoSize = true;
            this.flipXY.Location = new System.Drawing.Point(224, 88);
            this.flipXY.Name = "flipXY";
            this.flipXY.Size = new System.Drawing.Size(67, 16);
            this.flipXY.TabIndex = 38;
            this.flipXY.Text = "Flip X-Y";
            this.flipXY.UseVisualStyleBackColor = true;
            // 
            // testMode
            // 
            this.testMode.AutoSize = true;
            this.testMode.Location = new System.Drawing.Point(293, 88);
            this.testMode.Name = "testMode";
            this.testMode.Size = new System.Drawing.Size(78, 16);
            this.testMode.TabIndex = 39;
            this.testMode.Text = "Test Mode";
            this.testMode.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.hCombo);
            this.groupBox1.Controls.Add(this.rightBtn);
            this.groupBox1.Controls.Add(this.leftBtn);
            this.groupBox1.Controls.Add(this.rearBtn);
            this.groupBox1.Controls.Add(this.homeBtn);
            this.groupBox1.Controls.Add(this.frontBtn);
            this.groupBox1.Location = new System.Drawing.Point(263, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 150);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.vCombo);
            this.groupBox2.Controls.Add(this.downBtn);
            this.groupBox2.Controls.Add(this.upBtn);
            this.groupBox2.Location = new System.Drawing.Point(583, 52);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(171, 149);
            this.groupBox2.TabIndex = 43;
            this.groupBox2.TabStop = false;
            // 
            // resumeBtn
            // 
            this.resumeBtn.Location = new System.Drawing.Point(125, 136);
            this.resumeBtn.Name = "resumeBtn";
            this.resumeBtn.Size = new System.Drawing.Size(75, 23);
            this.resumeBtn.TabIndex = 48;
            this.resumeBtn.Text = "Resume";
            this.resumeBtn.UseVisualStyleBackColor = true;
            this.resumeBtn.Click += new System.EventHandler(this.resumeBtn_Click);
            // 
            // abortBtn
            // 
            this.abortBtn.Location = new System.Drawing.Point(211, 136);
            this.abortBtn.Name = "abortBtn";
            this.abortBtn.Size = new System.Drawing.Size(75, 23);
            this.abortBtn.TabIndex = 47;
            this.abortBtn.Text = "Abort";
            this.abortBtn.UseVisualStyleBackColor = true;
            this.abortBtn.Click += new System.EventHandler(this.abortBtn_Click);
            // 
            // execBtn
            // 
            this.execBtn.Location = new System.Drawing.Point(41, 136);
            this.execBtn.Name = "execBtn";
            this.execBtn.Size = new System.Drawing.Size(75, 23);
            this.execBtn.TabIndex = 46;
            this.execBtn.Text = "Execute";
            this.execBtn.UseVisualStyleBackColor = true;
            this.execBtn.Click += new System.EventHandler(this.execBtn_Click);
            // 
            // filename
            // 
            this.filename.Location = new System.Drawing.Point(11, 165);
            this.filename.Name = "filename";
            this.filename.Size = new System.Drawing.Size(275, 19);
            this.filename.TabIndex = 45;
            // 
            // fileBtn
            // 
            this.fileBtn.Location = new System.Drawing.Point(304, 163);
            this.fileBtn.Name = "fileBtn";
            this.fileBtn.Size = new System.Drawing.Size(75, 23);
            this.fileBtn.TabIndex = 44;
            this.fileBtn.Text = "File";
            this.fileBtn.UseVisualStyleBackColor = true;
            this.fileBtn.Click += new System.EventHandler(this.fileBtn_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.leaserMode);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.spindle);
            this.groupBox3.Controls.Add(this.readBtn);
            this.groupBox3.Controls.Add(this.resumeBtn);
            this.groupBox3.Controls.Add(this.abortBtn);
            this.groupBox3.Controls.Add(this.execBtn);
            this.groupBox3.Controls.Add(this.filename);
            this.groupBox3.Controls.Add(this.fileBtn);
            this.groupBox3.Controls.Add(this.testMode);
            this.groupBox3.Controls.Add(this.flipXY);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.offsetY);
            this.groupBox3.Controls.Add(this.offsetX);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.step);
            this.groupBox3.Controls.Add(this.speed);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.thickness);
            this.groupBox3.Controls.Add(this.materialCombo);
            this.groupBox3.Location = new System.Drawing.Point(363, 204);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(390, 193);
            this.groupBox3.TabIndex = 49;
            this.groupBox3.TabStop = false;
            // 
            // leaserMode
            // 
            this.leaserMode.AutoSize = true;
            this.leaserMode.Location = new System.Drawing.Point(224, 112);
            this.leaserMode.Name = "leaserMode";
            this.leaserMode.Size = new System.Drawing.Size(89, 16);
            this.leaserMode.TabIndex = 52;
            this.leaserMode.Text = "Leaser Mode";
            this.leaserMode.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(218, 64);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(42, 12);
            this.label13.TabIndex = 51;
            this.label13.Text = "Spindle";
            // 
            // spindle
            // 
            this.spindle.Location = new System.Drawing.Point(269, 61);
            this.spindle.Name = "spindle";
            this.spindle.Size = new System.Drawing.Size(100, 19);
            this.spindle.TabIndex = 50;
            // 
            // readBtn
            // 
            this.readBtn.Location = new System.Drawing.Point(300, 136);
            this.readBtn.Name = "readBtn";
            this.readBtn.Size = new System.Drawing.Size(75, 23);
            this.readBtn.TabIndex = 49;
            this.readBtn.Text = "File Read";
            this.readBtn.UseVisualStyleBackColor = true;
            this.readBtn.Click += new System.EventHandler(this.readBtn_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.xyZeroBtn);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.xPos);
            this.groupBox4.Controls.Add(this.yPos);
            this.groupBox4.Location = new System.Drawing.Point(263, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(308, 47);
            this.groupBox4.TabIndex = 50;
            this.groupBox4.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.zZeroBtn);
            this.groupBox5.Controls.Add(this.zPos);
            this.groupBox5.Location = new System.Drawing.Point(583, 4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(170, 47);
            this.groupBox5.TabIndex = 51;
            this.groupBox5.TabStop = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.sendBtn);
            this.groupBox6.Controls.Add(this.sendText);
            this.groupBox6.Location = new System.Drawing.Point(18, 348);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(328, 49);
            this.groupBox6.TabIndex = 52;
            this.groupBox6.TabStop = false;
            // 
            // comCombo
            // 
            this.comCombo.FormattingEnabled = true;
            this.comCombo.Location = new System.Drawing.Point(9, 15);
            this.comCombo.Name = "comCombo";
            this.comCombo.Size = new System.Drawing.Size(206, 20);
            this.comCombo.TabIndex = 53;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.comCombo);
            this.groupBox7.Controls.Add(this.connectBtn);
            this.groupBox7.Location = new System.Drawing.Point(16, 4);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(232, 72);
            this.groupBox7.TabIndex = 54;
            this.groupBox7.TabStop = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label14.Location = new System.Drawing.Point(526, 403);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(226, 15);
            this.label14.TabIndex = 55;
            this.label14.Text = "Presented by Crytus (c) 2020";
            this.label14.Click += new System.EventHandler(this.label14_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Location = new System.Drawing.Point(248, 205);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(105, 137);
            this.groupBox8.TabIndex = 56;
            this.groupBox8.TabStop = false;
            // 
            // NCTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 427);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.holeBtn);
            this.Controls.Add(this.targetY);
            this.Controls.Add(this.targetR);
            this.Controls.Add(this.targetX);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.logText);
            this.Controls.Add(this.groupBox8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "NCTool";
            this.Text = "NCTool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Leave += new System.EventHandler(this.Form1_Leave);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.TextBox logText;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TextBox sendText;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.Button frontBtn;
        private System.Windows.Forms.Button homeBtn;
        private System.Windows.Forms.Button rearBtn;
        private System.Windows.Forms.Button leftBtn;
        private System.Windows.Forms.Button rightBtn;
        private System.Windows.Forms.Button upBtn;
        private System.Windows.Forms.Button downBtn;
        private System.Windows.Forms.ComboBox hCombo;
        private System.Windows.Forms.ComboBox vCombo;
        private System.Windows.Forms.TextBox yPos;
        private System.Windows.Forms.TextBox zPos;
        private System.Windows.Forms.TextBox xPos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button xyZeroBtn;
        private System.Windows.Forms.Button zZeroBtn;
        private System.Windows.Forms.ComboBox materialCombo;
        private System.Windows.Forms.TextBox thickness;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox speed;
        private System.Windows.Forms.TextBox step;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox targetX;
        private System.Windows.Forms.TextBox targetR;
        private System.Windows.Forms.TextBox targetY;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button holeBtn;
        private System.Windows.Forms.TextBox offsetX;
        private System.Windows.Forms.TextBox offsetY;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox flipXY;
        private System.Windows.Forms.CheckBox testMode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button resumeBtn;
        private System.Windows.Forms.Button abortBtn;
        private System.Windows.Forms.Button execBtn;
        private System.Windows.Forms.TextBox filename;
        private System.Windows.Forms.Button fileBtn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox comCombo;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button readBtn;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox spindle;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox leaserMode;
        private System.Windows.Forms.GroupBox groupBox8;
    }
}

