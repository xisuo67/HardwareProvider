using Microsoft.Win32;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Management;

namespace SecurityServiceProvider
{
    [Serializable]
    public class MachineInfo
    {
        private static readonly string s_cpuId;
        private static readonly string s_mainboardSn;
        //private static readonly string s_systemDiskSn;
        //private static readonly string s_hardDiskSn;
        private static readonly string s_hardDiskId;
        private static readonly string s_macAddress;
        private static readonly string s_systemName;
        private static readonly string s_systemInstallDate;

        private string m_cpuId;
        private string m_mainboardSn;
        //private string m_systemDiskSn;
        //private string m_hardDiskSn;
        private string m_hardDiskId;
        private string m_macAddress;
        private string m_systemName;
        private string m_systemInstallDate;

        static MachineInfo()
        {
            try
            {
                //获取 CPU ID
                {
                    string cpuId = "";
                    ManagementClass mc = new ManagementClass(WMIPath.Win32_Processor.ToString());
                    ManagementObjectCollection moc = mc.GetInstances();
                    foreach (ManagementObject mo in moc)
                    {
                        object idObj = mo.Properties["ProcessorId"].Value;
                        if (idObj != null)
                        {
                            cpuId += idObj.ToString().Trim() + "|";
                            break;//只取第一个 CPU ID
                        }
                    }
                    int lastIndex = cpuId.LastIndexOf('|');
                    if (lastIndex >= 0)
                    {
                        cpuId = cpuId.Substring(0, lastIndex);
                    }
                    s_cpuId = cpuId;
                }

                //获取主板序列号
                {
                    string mainboardSn = "";
                    ManagementClass mc = new ManagementClass(WMIPath.Win32_BaseBoard.ToString());
                    ManagementObjectCollection moc = mc.GetInstances();
                    foreach (ManagementObject mo in moc)
                    {
                        object snObj = mo.Properties["SerialNumber"].Value;
                        if (snObj != null)
                        {
                            mainboardSn += snObj.ToString().Trim() + "|";
                        }
                    }
                    int lastIndex = mainboardSn.LastIndexOf('|');
                    if (lastIndex >= 0)
                    {
                        mainboardSn = mainboardSn.Substring(0, lastIndex);
                    }
                    s_mainboardSn = mainboardSn;
                }

                //获取操作系统运行盘序列号
                {
                    //string systemDiskSn = "";
                    //ManagementClass mc = new ManagementClass("Win32_PhysicalMedia");
                    //ManagementObjectCollection moc = mc.GetInstances();
                    //Dictionary<string, string> dict = new Dictionary<string, string>();
                    //foreach (ManagementObject mo in moc)
                    //{
                    //    string tag = mo.Properties["Tag"].Value.ToString().ToLower().Trim();
                    //    object snObj = mo.Properties["SerialNumber"].Value;
                    //    if (snObj != null)
                    //    {
                    //        dict.Add(tag, snObj.ToString().Trim());
                    //    }
                    //}
                    //mc = new ManagementClass("Win32_OperatingSystem");
                    //moc = mc.GetInstances();
                    //string currentSystemRunDisk = string.Empty;
                    //foreach (ManagementObject mo in moc)
                    //{
                    //    currentSystemRunDisk = Regex.Match(mo.Properties["Name"].Value.ToString().ToLower(), @"harddisk\d+").Value;
                    //}
                    //systemDiskSn = dict
                    //    .Where(x => Regex.IsMatch(x.Key, @"physicaldrive" + Regex.Match(currentSystemRunDisk, @"\d+$").Value))
                    //    .FirstOrDefault().Value;
                    //s_systemDiskSn = systemDiskSn;
                }

                //获取硬盘序列号
                {
                    //string hardDiskSn = "";
                    //ManagementClass mc = new ManagementClass(WMIPath.Win32_DiskDrive.ToString());
                    //ManagementObjectCollection moc = mc.GetInstances();
                    //foreach (ManagementObject mo in moc)
                    //{
                    //    object itObj = mo.Properties["InterfaceType"].Value;
                    //    if (itObj != null)
                    //    {
                    //        if (itObj.ToString().ToLower() != "usb")
                    //        {
                    //            object snObj = mo.Properties["SerialNumber"].Value;
                    //            if (snObj != null)
                    //            {
                    //                hardDiskSn += snObj.ToString().Trim() + "|";
                    //            }
                    //        }
                    //    }
                    //}
                    //int lastIndex = hardDiskSn.LastIndexOf('|');
                    //if (lastIndex >= 0)
                    //{
                    //    hardDiskSn = hardDiskSn.Substring(0, lastIndex);
                    //}
                    //s_hardDiskSn = hardDiskSn;
                }

                //获取逻辑分区(C:) ID
                {
                    string hardDiskId = "";
                    ManagementObject mo = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
                    hardDiskId = mo.Properties["VolumeSerialNumber"].Value.ToString();
                    s_hardDiskId = hardDiskId;
                }

                //获取网卡 MAC 地址
                {
                    string macAddress = "";
                    List<NetworkAdapter> networkAdapters = GetNetAdapterInfo();
                    foreach (NetworkAdapter networkAdapter in networkAdapters)
                    {
                        if (networkAdapter.Type == NetworkAdapterType.PCIPhysical
                            || networkAdapter.Type == NetworkAdapterType.PCIWireless)
                        {
                            macAddress += networkAdapter.MacAddress + "|";
                        }
                    }
                    int lastIndex = macAddress.LastIndexOf('|');
                    if (lastIndex >= 0)
                    {
                        macAddress = macAddress.Substring(0, lastIndex);
                    }
                    s_macAddress = macAddress;
                }

                //获取主机名
                {
                    string systemName = "";
                    ManagementClass mc = new ManagementClass(WMIPath.Win32_Processor.ToString());
                    ManagementObjectCollection moc = mc.GetInstances();
                    foreach (ManagementObject mo in moc)
                    {
                        object snObj = mo.Properties["SystemName"].Value;
                        if (snObj != null)
                        {
                            systemName += snObj.ToString().Trim() + "|";
                        }
                    }
                    int lastIndex = systemName.LastIndexOf('|');
                    if (lastIndex >= 0)
                    {
                        systemName = systemName.Substring(0, lastIndex);
                    }
                    s_systemName = systemName;
                }

                //获取操作系统安装日期
                {
                    string systemInstallDate = "";
                    System.Management.ObjectQuery oq = new System.Management.ObjectQuery("SELECT * FROM Win32_OperatingSystem");
                    System.Management.ManagementScope ms = new System.Management.ManagementScope();
                    ManagementObjectSearcher mos = new ManagementObjectSearcher(ms, oq);
                    ManagementObjectCollection moc = mos.Get();
                    string strInfo = "";
                    foreach (ManagementObject MyObject in moc)
                    {
                        strInfo = MyObject.GetText(TextFormat.Mof);
                    }
                    systemInstallDate = strInfo.Substring(strInfo.LastIndexOf("InstallDate") + 15, 14);
                    s_systemInstallDate = systemInstallDate;
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public MachineInfo()
        {
            this.m_cpuId = s_cpuId;
            this.m_mainboardSn = s_mainboardSn;
            //this.m_systemDiskSn = s_systemDiskSn;
            //this.m_hardDiskSn = s_hardDiskSn;
            this.m_hardDiskId = s_hardDiskId;
            this.m_macAddress = s_macAddress;
            this.m_systemName = s_systemName;
            this.m_systemInstallDate = s_systemInstallDate;
        }

        public MachineInfo(string cpuId, string mainboardSn, string hardDiskId)
        {
            this.m_cpuId = cpuId;
            this.m_mainboardSn = mainboardSn;
            this.m_hardDiskId = hardDiskId;
        }

        [MachineInfoDescription("CPU ID")]
        public string CpuId
        {
            get
            {
                return this.m_cpuId;
            }
            set { }
        }

        [MachineInfoDescription("Mainboard")]
        public string MainboardSn
        {
            get
            {
                return this.m_mainboardSn;
            }
        }

        //public string SystemDiskSn
        //{
        //    get
        //    {
        //        return this.m_systemDiskSn;
        //    }
        //}

        //public string HardDiskSn
        //{
        //    get
        //    {
        //        return this.m_hardDiskSn;
        //    }
        //}

        [MachineInfoDescription("HardDisk ID")]
        public string HardDiskId
        {
            get
            {
                return this.m_hardDiskId;
            }
            set { }
        }

        [MachineInfoDescription("MAC Address")]
        public string MacAddress
        {
            get
            {
                return this.m_macAddress;
            }
        }

        [MachineInfoDescription("Host Name")]
        public string SystemName
        {
            get
            {
                return this.m_systemName;
            }
        }

        public string SystemInstallDate
        {
            get
            {
                return this.m_systemInstallDate;
            }
        }

        public static List<NetworkAdapter> GetNetAdapterInfo()
        {
            try
            {
                List<NetworkAdapter> networkAdapters = new List<NetworkAdapter>();
                NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
                if (adapters != null)
                {
                    foreach (NetworkInterface ni in adapters)
                    {
                        NetworkAdapterType naType = NetworkAdapterType.Unknown;
                        IPInterfaceProperties ips = ni.GetIPProperties();

                        PhysicalAddress physicalAddress = ni.GetPhysicalAddress();
                        if (physicalAddress == null)
                        {
                            continue;
                        }
                        string macAddress = physicalAddress.ToString();
                        if (macAddress.Length < 7)
                        {
                            continue;
                        }
                        if (macAddress.Substring(0, 6) == "000000")
                        {
                            continue;
                        }
                        if (ni.Name.ToLower().IndexOf("vmware") > -1)
                        {
                            continue;
                        }
                        string key = "SYSTEM\\CurrentControlSet\\Control\\Network\\{4D36E972-E325-11CE-BFC1-08002BE10318}\\" + ni.Id + "\\Connection";
                        RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(key, false);
                        if (registryKey != null)
                        {
                            // 区分 PnpInstanceID     
                            // 如果前面有 PCI 就是本机的真实网卡    
                            // MediaSubType 01 为是虚拟网卡，02为无线网卡。    
                            string pnpInstanceID = registryKey.GetValue("PnpInstanceID", "").ToString();
                            int mediaSubType = Convert.ToInt32(registryKey.GetValue("MediaSubType", 0));
                            if (pnpInstanceID.Length > 3 && pnpInstanceID.Substring(0, 3) == "PCI")
                            {
                                if (ni.NetworkInterfaceType.ToString().ToLower().IndexOf("wireless") >= 0)
                                {
                                    naType = NetworkAdapterType.PCIWireless;
                                }
                                else
                                {
                                    naType = NetworkAdapterType.PCIPhysical;
                                }
                            }
                            else if (mediaSubType == 1 || mediaSubType == 0)
                            {
                                naType = NetworkAdapterType.Virtual;
                            }
                            else if (mediaSubType == 2 || ni.NetworkInterfaceType.ToString().ToLower().IndexOf("wireless") >= 0)
                            {
                                naType = NetworkAdapterType.Wireless;
                            }
                            else if (mediaSubType == 7)
                            {
                                naType = NetworkAdapterType.Bluetooth;
                            }
                        }
                        StringBuilder ipStrBuilder = new StringBuilder();
                        UnicastIPAddressInformationCollection UnicastIPAddressInformationCollection = ips.UnicastAddresses;
                        foreach (UnicastIPAddressInformation UnicastIPAddressInformation in UnicastIPAddressInformationCollection)
                        {
                            if (UnicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                ipStrBuilder.Append(UnicastIPAddressInformation.Address + "\r\n");
                            }
                        }
                        string ipAddress = ipStrBuilder.ToString();
                        int lastIndex = ipAddress.LastIndexOf("\r\n");
                        if (lastIndex >= 0)
                        {
                            ipAddress = ipAddress.Substring(0, lastIndex);
                        }

                        networkAdapters.Add(new NetworkAdapter()
                        {
                            Type = naType,
                            Description = ni.Name.Trim(),
                            Speed = ni.Speed / 1024 / 1024,
                            MacAddress = macAddress.Trim(),
                            IpAddress = ipAddress.Trim()
                        });
                    }
                }
                return networkAdapters;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public string GetSiteCode()
        {
            string siteCode;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;
                    settings.ConformanceLevel = ConformanceLevel.Auto;
                    settings.IndentChars = "\t";
                    settings.Encoding = Encoding.UTF8;
                    using (XmlWriter writer = XmlWriter.Create(ms, settings))
                    {
                        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                        ns.Add("", "");
                        XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
                        xmlSerializer.Serialize(writer, this, ns);
                        ms.Position = 0;
                    }
                    byte[] buffer = new byte[ms.Length];
                    ms.Read(buffer, 0, buffer.Length);
                    siteCode = HashServiceHelper.GetSHA256HashCode(ByteConversion.BytesToBase64String(buffer), Encoding.UTF8);
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return siteCode;
        }
    }

    public enum WMIPath
    {
        // 硬件
        Win32_Processor,     // CPU 处理器
        Win32_PhysicalMemory,  // 物理内存条
        Win32_Keyboard,     // 键盘
        Win32_PointingDevice,  // 点输入设备，包括鼠标。
        Win32_FloppyDrive,    // 软盘驱动器
        Win32_DiskDrive,     // 硬盘驱动器
        Win32_CDROMDrive,    // 光盘驱动器
        Win32_BaseBoard,     // 主板
        Win32_BIOS,       // BIOS 芯片
        Win32_ParallelPort,   // 并口
        Win32_SerialPort,    // 串口
        Win32_SerialPortConfiguration, // 串口配置
        Win32_SoundDevice,    // 多媒体设置，一般指声卡。
        Win32_SystemSlot,    // 主板插槽 (ISA & PCI & AGP)
        Win32_USBController,   // USB 控制器
        Win32_NetworkAdapter,  // 网络适配器
        Win32_NetworkAdapterConfiguration, // 网络适配器设置
        Win32_Printer,      // 打印机
        Win32_PrinterConfiguration, // 打印机设置
        Win32_PrintJob,     // 打印机任务
        Win32_TCPIPPrinterPort, // 打印机端口
        Win32_POTSModem,     // MODEM
        Win32_POTSModemToSerialPort, // MODEM 端口
        Win32_DesktopMonitor,  // 显示器
        Win32_DisplayConfiguration, // 显卡
        Win32_DisplayControllerConfiguration, // 显卡设置
        Win32_VideoController, // 显卡细节。
        Win32_VideoSettings,  // 显卡支持的显示模式。
                              // 操作系统
        Win32_TimeZone,     // 时区
        Win32_SystemDriver,   // 驱动程序
        Win32_DiskPartition,  // 磁盘分区
        Win32_LogicalDisk,   // 逻辑磁盘
        Win32_LogicalDiskToPartition,   // 逻辑磁盘所在分区及始末位置。
        Win32_LogicalMemoryConfiguration, // 逻辑内存配置
        Win32_PageFile,     // 系统页文件信息
        Win32_PageFileSetting, // 页文件设置
        Win32_BootConfiguration, // 系统启动配置
        Win32_ComputerSystem,  // 计算机信息简要
        Win32_OperatingSystem, // 操作系统信息
        Win32_StartupCommand,  // 系统自动启动程序
        Win32_Service,     // 系统安装的服务
        Win32_Group,      // 系统管理组
        Win32_GroupUser,    // 系统组帐号
        Win32_UserAccount,   // 用户帐号
        Win32_Process,     // 系统进程
        Win32_Thread,      // 系统线程
        Win32_Share,      // 共享
        Win32_NetworkClient,  // 已安装的网络客户端
        Win32_NetworkProtocol // 已安装的网络协议
    }

    public enum NetworkAdapterType
    {
        Unknown,
        PCIPhysical,
        PCIWireless,
        Physical,
        Wireless,
        Virtual,
        Bluetooth
    }

    public class NetworkAdapter
    {
        public NetworkAdapterType Type { get; set; }

        public string Description { get; set; }

        public long Speed { get; set; }

        public string MacAddress { get; set; }

        public string IpAddress { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class MachineInfoDescriptionAttribute : Attribute
    {
        private string m_description;

        public MachineInfoDescriptionAttribute(string description)
        {
            this.m_description = description;
        }

        public string Description
        {
            get { return this.m_description; }
        }
    }
}