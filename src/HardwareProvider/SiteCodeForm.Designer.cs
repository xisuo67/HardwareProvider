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
            this.dgvMachineInfo = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            //this.yesNoAnimationCtrl = new LicenseRequestTools.YesNoAnimationCtrl();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMachineInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMachineInfo
            // 
            this.dgvMachineInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMachineInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMachineInfo.Location = new System.Drawing.Point(16, 37);
            this.dgvMachineInfo.Name = "dgvMachineInfo";
            this.dgvMachineInfo.ReadOnly = true;
            this.dgvMachineInfo.RowTemplate.Height = 23;
            this.dgvMachineInfo.Size = new System.Drawing.Size(454, 450);
            this.dgvMachineInfo.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 41);
            this.label1.TabIndex = 1;
            this.label1.Text = "硬件信息列表";
            // 
            // yesNoAnimationCtrl
            // 
            //this.yesNoAnimationCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            //this.yesNoAnimationCtrl.Location = new System.Drawing.Point(422, 493);
            //this.yesNoAnimationCtrl.Name = "yesNoAnimationCtrl";
            //this.yesNoAnimationCtrl.Size = new System.Drawing.Size(48, 48);
            //this.yesNoAnimationCtrl.TabIndex = 16;
            //this.yesNoAnimationCtrl.Text = "yesNoAnimationCtrl1";
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(150, 511);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(88, 30);
            this.btnCopy.TabIndex = 17;
            this.btnCopy.Text = "复制";
            this.btnCopy.UseVisualStyleBackColor = true;
            //this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(244, 511);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 30);
            this.btnClose.TabIndex = 18;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            //this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // SiteCodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(19F, 41F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 553);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCopy);
            //this.Controls.Add(this.yesNoAnimationCtrl);
            this.Controls.Add(this.dgvMachineInfo);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SiteCodeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "计算机硬件信息收集工具";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMachineInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvMachineInfo;
        //private YesNoAnimationCtrl yesNoAnimationCtrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnClose;
    }
}