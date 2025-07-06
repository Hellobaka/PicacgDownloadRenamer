namespace PicacgDownloadRenamer
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            PathPreviews = new TextBox();
            BrowserButton = new Button();
            ActionDisplay = new ListBox();
            DryRunSelector = new CheckBox();
            StartButton = new Button();
            DBSelectButton = new Button();
            DBPathPreview = new TextBox();
            label1 = new Label();
            label2 = new Label();
            MoveProgressBar = new ProgressBar();
            ProgressDisplay = new Label();
            CopyModeSelector = new CheckBox();
            label3 = new Label();
            PicacgVersionSelector = new ComboBox();
            label4 = new Label();
            BrowserOutputButton = new Button();
            OutputPathPreview = new TextBox();
            KeepEPNameSelector = new CheckBox();
            SuspendLayout();
            // 
            // PathPreviews
            // 
            PathPreviews.Location = new Point(98, 41);
            PathPreviews.Name = "PathPreviews";
            PathPreviews.ReadOnly = true;
            PathPreviews.Size = new Size(489, 23);
            PathPreviews.TabIndex = 0;
            // 
            // BrowserButton
            // 
            BrowserButton.Location = new Point(593, 41);
            BrowserButton.Name = "BrowserButton";
            BrowserButton.Size = new Size(75, 23);
            BrowserButton.TabIndex = 1;
            BrowserButton.Text = "浏览";
            BrowserButton.UseVisualStyleBackColor = true;
            BrowserButton.Click += BrowserButton_Click;
            // 
            // ActionDisplay
            // 
            ActionDisplay.FormattingEnabled = true;
            ActionDisplay.HorizontalScrollbar = true;
            ActionDisplay.Location = new Point(12, 126);
            ActionDisplay.Name = "ActionDisplay";
            ActionDisplay.ScrollAlwaysVisible = true;
            ActionDisplay.Size = new Size(656, 395);
            ActionDisplay.TabIndex = 2;
            // 
            // DryRunSelector
            // 
            DryRunSelector.AutoSize = true;
            DryRunSelector.Checked = true;
            DryRunSelector.CheckState = CheckState.Checked;
            DryRunSelector.Location = new Point(513, 101);
            DryRunSelector.Name = "DryRunSelector";
            DryRunSelector.Size = new Size(74, 21);
            DryRunSelector.TabIndex = 3;
            DryRunSelector.Text = "Dry-Run";
            DryRunSelector.UseVisualStyleBackColor = true;
            // 
            // StartButton
            // 
            StartButton.Location = new Point(593, 99);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(75, 23);
            StartButton.TabIndex = 4;
            StartButton.Text = "开始";
            StartButton.UseVisualStyleBackColor = true;
            StartButton.Click += StartButton_Click;
            // 
            // DBSelectButton
            // 
            DBSelectButton.Location = new Point(593, 12);
            DBSelectButton.Name = "DBSelectButton";
            DBSelectButton.Size = new Size(75, 23);
            DBSelectButton.TabIndex = 6;
            DBSelectButton.Text = "浏览";
            DBSelectButton.UseVisualStyleBackColor = true;
            DBSelectButton.Click += DBSelectButton_Click;
            // 
            // DBPathPreview
            // 
            DBPathPreview.Location = new Point(98, 12);
            DBPathPreview.Name = "DBPathPreview";
            DBPathPreview.ReadOnly = true;
            DBPathPreview.Size = new Size(489, 23);
            DBPathPreview.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(80, 17);
            label1.TabIndex = 7;
            label1.Text = "缓存数据库：";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 44);
            label2.Name = "label2";
            label2.Size = new Size(80, 17);
            label2.TabIndex = 8;
            label2.Text = "重命名缓存：";
            // 
            // MoveProgressBar
            // 
            MoveProgressBar.Location = new Point(12, 527);
            MoveProgressBar.Name = "MoveProgressBar";
            MoveProgressBar.Size = new Size(517, 23);
            MoveProgressBar.Style = ProgressBarStyle.Continuous;
            MoveProgressBar.TabIndex = 9;
            // 
            // ProgressDisplay
            // 
            ProgressDisplay.AutoSize = true;
            ProgressDisplay.Location = new Point(535, 530);
            ProgressDisplay.Name = "ProgressDisplay";
            ProgressDisplay.Size = new Size(55, 17);
            ProgressDisplay.TabIndex = 10;
            ProgressDisplay.Text = "100/100";
            // 
            // CopyModeSelector
            // 
            CopyModeSelector.AutoSize = true;
            CopyModeSelector.Location = new Point(418, 101);
            CopyModeSelector.Name = "CopyModeSelector";
            CopyModeSelector.Size = new Size(87, 21);
            CopyModeSelector.TabIndex = 11;
            CopyModeSelector.Text = "保留原文件";
            CopyModeSelector.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 102);
            label3.Name = "label3";
            label3.Size = new Size(92, 17);
            label3.TabIndex = 12;
            label3.Text = "哔咔缓存版本：";
            // 
            // PicacgVersionSelector
            // 
            PicacgVersionSelector.DropDownStyle = ComboBoxStyle.DropDownList;
            PicacgVersionSelector.FormattingEnabled = true;
            PicacgVersionSelector.Items.AddRange(new object[] { "V1", "V2" });
            PicacgVersionSelector.Location = new Point(98, 98);
            PicacgVersionSelector.Name = "PicacgVersionSelector";
            PicacgVersionSelector.Size = new Size(121, 25);
            PicacgVersionSelector.TabIndex = 13;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 73);
            label4.Name = "label4";
            label4.Size = new Size(68, 17);
            label4.TabIndex = 16;
            label4.Text = "输出目录：";
            // 
            // BrowserOutputButton
            // 
            BrowserOutputButton.Location = new Point(593, 70);
            BrowserOutputButton.Name = "BrowserOutputButton";
            BrowserOutputButton.RightToLeft = RightToLeft.Yes;
            BrowserOutputButton.Size = new Size(75, 23);
            BrowserOutputButton.TabIndex = 15;
            BrowserOutputButton.Text = "浏览";
            BrowserOutputButton.UseVisualStyleBackColor = true;
            BrowserOutputButton.Click += BrowserOutputButton_Click;
            // 
            // OutputPathPreview
            // 
            OutputPathPreview.Location = new Point(98, 70);
            OutputPathPreview.Name = "OutputPathPreview";
            OutputPathPreview.Size = new Size(489, 23);
            OutputPathPreview.TabIndex = 14;
            // 
            // KeepEPNameSelector
            // 
            KeepEPNameSelector.AutoSize = true;
            KeepEPNameSelector.Location = new Point(289, 101);
            KeepEPNameSelector.Name = "KeepEPNameSelector";
            KeepEPNameSelector.Size = new Size(123, 21);
            KeepEPNameSelector.TabIndex = 17;
            KeepEPNameSelector.Text = "保留原始章节标题";
            KeepEPNameSelector.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(680, 565);
            Controls.Add(KeepEPNameSelector);
            Controls.Add(label4);
            Controls.Add(BrowserOutputButton);
            Controls.Add(OutputPathPreview);
            Controls.Add(PicacgVersionSelector);
            Controls.Add(label3);
            Controls.Add(CopyModeSelector);
            Controls.Add(ProgressDisplay);
            Controls.Add(MoveProgressBar);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(DBSelectButton);
            Controls.Add(DBPathPreview);
            Controls.Add(StartButton);
            Controls.Add(DryRunSelector);
            Controls.Add(ActionDisplay);
            Controls.Add(BrowserButton);
            Controls.Add(PathPreviews);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "哔咔漫画缓存重命名器";
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox PathPreviews;
        private Button BrowserButton;
        private ListBox ActionDisplay;
        private CheckBox DryRunSelector;
        private Button StartButton;
        private Button DBSelectButton;
        private TextBox DBPathPreview;
        private Label label1;
        private Label label2;
        private ProgressBar MoveProgressBar;
        private Label ProgressDisplay;
        private CheckBox CopyModeSelector;
        private Label label3;
        private ComboBox PicacgVersionSelector;
        private Label label4;
        private Button BrowserOutputButton;
        private TextBox OutputPathPreview;
        private CheckBox KeepEPNameSelector;
    }
}
