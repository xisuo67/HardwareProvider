namespace HardwareProvider
{
    partial class SiteCodeForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SiteCodeForm));
            dgvMachineInfo = new DataGridView();
            Project = new DataGridViewTextBoxColumn();
            Value = new DataGridViewTextBoxColumn();
            label1 = new Label();
            btnCopy = new Button();
            btnClose = new Button();
            yesNoAnimationCtrl = new YesNoAnimationCtrl();
            ((System.ComponentModel.ISupportInitialize)dgvMachineInfo).BeginInit();
            SuspendLayout();
            // 
            // dgvMachineInfo
            // 
            dgvMachineInfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvMachineInfo.BackgroundColor = SystemColors.Control;
            dgvMachineInfo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMachineInfo.Columns.AddRange(new DataGridViewColumn[] { Project, Value });
            dgvMachineInfo.Location = new Point(16, 37);
            dgvMachineInfo.Name = "dgvMachineInfo";
            dgvMachineInfo.ReadOnly = true;
            dgvMachineInfo.RowTemplate.Height = 23;
            dgvMachineInfo.Size = new Size(454, 197);
            dgvMachineInfo.TabIndex = 4;
            // 
            // Project
            // 
            Project.FillWeight = 140F;
            Project.HeaderText = "Project";
            Project.Name = "Project";
            Project.ReadOnly = true;
            Project.Width = 88;
            // 
            // Value
            // 
            Value.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Value.FillWeight = 300F;
            Value.HeaderText = "Value";
            Value.Name = "Value";
            Value.ReadOnly = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 12);
            label1.Name = "label1";
            label1.Size = new Size(106, 21);
            label1.TabIndex = 1;
            label1.Text = "硬件信息列表";
            // 
            // btnCopy
            // 
            btnCopy.Location = new Point(150, 263);
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new Size(88, 30);
            btnCopy.TabIndex = 17;
            btnCopy.Text = "复制";
            btnCopy.UseVisualStyleBackColor = true;
            btnCopy.Click += btnCopy_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(244, 263);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(88, 30);
            btnClose.TabIndex = 18;
            btnClose.Text = "关闭";
            btnClose.UseVisualStyleBackColor = true;
            // 
            // yesNoAnimationCtrl
            // 
            yesNoAnimationCtrl.Location = new Point(412, 245);
            yesNoAnimationCtrl.Name = "yesNoAnimationCtrl";
            yesNoAnimationCtrl.Size = new Size(58, 58);
            yesNoAnimationCtrl.TabIndex = 19;
            yesNoAnimationCtrl.Text = "yesNoAnimationCtrl";
            // 
            // SiteCodeForm
            // 
            AutoScaleDimensions = new SizeF(10F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(482, 309);
            Controls.Add(yesNoAnimationCtrl);
            Controls.Add(btnClose);
            Controls.Add(btnCopy);
            Controls.Add(dgvMachineInfo);
            Controls.Add(label1);
            Font = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SiteCodeForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "计算机硬件信息收集工具";
            ((System.ComponentModel.ISupportInitialize)dgvMachineInfo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.DataGridView dgvMachineInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnClose;
        private DataGridViewTextBoxColumn Project;
        private DataGridViewTextBoxColumn Value;
        private YesNoAnimationCtrl yesNoAnimationCtrl;
    }
}