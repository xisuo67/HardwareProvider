using System.Reflection;
using System.Text;

namespace HardwareProvider
{
    public partial class SiteCodeForm : Form
    {
        private static readonly ISecurityServiceLog s_log;

        private MachineInfo m_machineInfo;
        private string m_clipboradInfo;

        static SiteCodeForm()
        {
            ISecurityServiceLogFactory logFactor = new SecurityServiceLogFactory();
            s_log = logFactor.GetCurrentThreadLog();
        }

        public SiteCodeForm()
        {
            InitializeComponent();

            this.Init();
        }

        private void Init()
        {
            this.Text += string.Format(" v{0}", Application.ProductVersion);

            this.Load += MainForm_Load;
            this.FormClosing += MainForm_FormClosing;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.yesNoAnimationCtrl.Visible)
            {
                e.Cancel = true;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.btnCopy.Enabled = this.btnClose.Enabled = false;

            this.dgvMachineInfo.Columns.Add("Project", "项目");
            this.dgvMachineInfo.Columns.Add("Value", "值");
            this.dgvMachineInfo.AllowUserToAddRows = false;
            this.dgvMachineInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvMachineInfo.RowTemplate.Height = 30;

            //this.yesNoAnimationCtrl.Visible = true;
            //this.yesNoAnimationCtrl.Start();

            Task<MachineInfo> task = new Task<MachineInfo>(new Func<MachineInfo>(
                () =>
                {
                    MachineInfo mi = null;
                    try
                    {
                        mi = new MachineInfo();
                    }
                    catch (Exception exp)
                    {
                        s_log.Error(exp);
                    }

                    return mi;
                }));
            task.ContinueWith(new Action<Task<MachineInfo>>(
                (taskEnd) =>
                {
                    this.BeginInvoke(new Action(
                        () =>
                        {
                            try
                            {
                                this.m_machineInfo = taskEnd.Result;
                                Type type = this.m_machineInfo.GetType();
                                PropertyInfo[] propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                                foreach (PropertyInfo propertyInfo in propertyInfos)
                                {
                                    object[] attrs = propertyInfo.GetCustomAttributes(typeof(MachineInfoDescriptionAttribute), true);
                                    if (attrs != null && attrs.Length > 0)
                                    {
                                        MachineInfoDescriptionAttribute attribute = attrs[0] as MachineInfoDescriptionAttribute;
                                        if (attribute != null)
                                        {
                                            int index = this.dgvMachineInfo.Rows.Add();
                                            DataGridViewRow dgvRow = this.dgvMachineInfo.Rows[index];
                                            dgvRow.Cells["Project"].Value = attribute.Description;
                                            dgvRow.Cells["Value"].Value = propertyInfo.GetValue(this.m_machineInfo, null);
                                        }
                                    }
                                }
                                this.dgvMachineInfo.Rows[0].Selected = false;

                                //using (MemoryStream ms = new MemoryStream())
                                //{
                                //    XmlWriterSettings settings = new XmlWriterSettings();
                                //    settings.Indent = true;
                                //    settings.IndentChars = "\t";
                                //    settings.Encoding = new UTF8Encoding(false);
                                //    settings.ConformanceLevel = ConformanceLevel.Auto;
                                //    using (XmlWriter writer = XmlWriter.Create(ms, settings))
                                //    {
                                //        writer.WriteStartDocument();
                                //        writer.WriteStartElement(this.m_machineInfo.GetType().Name);
                                //        writer.WriteAttributeString("SiteCode", this.m_machineInfo.GetSiteCode());
                                //        foreach (DataGridViewRow dgvRow in dgvMachineInfo.Rows)
                                //        {
                                //            string project = dgvRow.Cells["Project"].Value.ToString().Replace(" ", "");
                                //            string value = dgvRow.Cells["Value"].Value.ToString();
                                //            writer.WriteElementString(project, value);
                                //        }
                                //        writer.WriteEndElement();
                                //        writer.WriteEndDocument();
                                //    }
                                //    ms.Position = 0;
                                //    byte[] buffer = new byte[ms.Length];
                                //    ms.Read(buffer, 0, buffer.Length);
                                //    this.m_clipboradInfo = new UTF8Encoding(false).GetString(buffer);
                                //}

                                StringBuilder builder = new StringBuilder();
                                using (StringWriter sw = new StringWriter(builder))
                                {
                                    sw.WriteLine(string.Format("SiteCode：{0}", this.m_machineInfo.GetSiteCode()));
                                    foreach (DataGridViewRow dgvRow in dgvMachineInfo.Rows)
                                    {
                                        string project = dgvRow.Cells["Project"].Value.ToString();
                                        string value = dgvRow.Cells["Value"].Value.ToString();
                                        sw.WriteLine(string.Format("{0}：{1}", project, value));
                                    }
                                }
                                this.m_clipboradInfo = builder.ToString();

                                this.btnCopy.Enabled = this.btnClose.Enabled = true;
                                this.btnCopy.Focus();

                                //this.yesNoAnimationCtrl.Visible = false;
                                //this.yesNoAnimationCtrl.Stop(YesNoWarn.Unknown);
                            }
                            catch (Exception exp)
                            {
                                s_log.Error(exp);
                            }
                        }));
                }));
            task.Start();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.m_clipboradInfo);
            MessageBox.Show("已复制到剪贴板", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}