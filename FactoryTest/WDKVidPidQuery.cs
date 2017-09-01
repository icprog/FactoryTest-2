using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace USB
{
    public struct HIDD_VIDPID
    {
        public UInt16 VendorID;     // 供应商标识
        // public UInt16 ProductID;    // 产品编号
        public String ProductID;// 产品编号
        public String ProductName;// 产品编号
        public String PortName;// 产品编号
    }
    
    /// <summary>
    /// 基于WDK获取系统设备的VIDPID
    /// </summary>
    partial class EZUSB
    {
        /// <summary>
        /// 获取系统所有设备的VIDPID
        /// </summary>
        public static HIDD_VIDPID[] AllVidPid
        {
            get
            {
                //return WhoVidPid(Guid.Empty, Guid.Empty, null, 0x1a86, "7523");
                return WhoVidPid(Guid.Empty, Guid.Empty, null,0x0e8d,"0003");
                //return WhoVidPid(Guid.Empty, Guid.Empty, null);
            }
        }

        /// <summary>
        /// 结合设备安装类GUID和设备接口类GUID获取设备VIDPID
        /// </summary>
        /// <param name="setupClassGuid">设备安装类GUID，Empty忽视</param>
        /// <param name="interfaceClassGuid">设备接口类GUID，Empty忽视</param>
        /// <returns>设备VIDPID列表</returns>
        /// <remarks>
        /// 优点：直接通过设备实例ID提取VIDPID，从而无需获取设备路径来读写IO
        /// </remarks>
        public static HIDD_VIDPID[] WhoVidPid(Guid setupClassGuid, Guid interfaceClassGuid, String Enumerator)
        {
            // 根据设备安装类GUID创建空的设备信息集合
            IntPtr DeviceInfoSet;
            if (setupClassGuid == Guid.Empty)
            {
                DeviceInfoSet = SetupDiCreateDeviceInfoList(IntPtr.Zero, IntPtr.Zero);
            }
            else
            {
                DeviceInfoSet = SetupDiCreateDeviceInfoList(ref setupClassGuid, IntPtr.Zero);
            }

            if (DeviceInfoSet == new IntPtr(-1)) return null;

            // 根据设备接口类GUID创建新的设备信息集合
            IntPtr hDevInfo;
            if (interfaceClassGuid == Guid.Empty)
            {
                hDevInfo = SetupDiGetClassDevsEx(
                    IntPtr.Zero,
                    Enumerator,
                    IntPtr.Zero,
                    DIGCF.DIGCF_ALLCLASSES  | DIGCF.DIGCF_PRESENT,
                    DeviceInfoSet,
                    null,
                    IntPtr.Zero);
            }
            else
            {
                hDevInfo = SetupDiGetClassDevsEx(
                    ref interfaceClassGuid,
                    null,
                    IntPtr.Zero,
                    DIGCF.DIGCF_DEVICEINTERFACE | DIGCF.DIGCF_PRESENT,
                    DeviceInfoSet,
                    null,
                    IntPtr.Zero);
            }

            if (hDevInfo == new IntPtr(-1)) return null;

            // 枚举所有设备
            List<HIDD_VIDPID> DeviceList = new List<HIDD_VIDPID>();

            // 存储设备实例ID
            StringBuilder DeviceInstanceId = new StringBuilder(256);

            // 获取设备信息数据
            UInt32 DeviceIndex = 0;
            uint MAX_DEV_LEN = 1000;
            StringBuilder DeviceName = new StringBuilder("");
            SP_DEVINFO_DATA DeviceInfoData = SP_DEVINFO_DATA.Empty;
            while (SetupDiEnumDeviceInfo(hDevInfo, DeviceIndex++, ref DeviceInfoData))
            {   // 获取设备实例ID             
               
                    if (SetupDiGetDeviceInstanceId(hDevInfo, ref DeviceInfoData, DeviceInstanceId, DeviceInstanceId.Capacity, IntPtr.Zero))
                {
                    String PNPDeviceID = DeviceInstanceId.ToString();
                    Match match = Regex.Match(PNPDeviceID, "VID_[0-9|A-F]{4}&PID_[0-9|A-F]{4}");
                    if (match.Success)
                    {
                        HIDD_VIDPID Entity=new HIDD_VIDPID();
                        Entity.VendorID = Convert.ToUInt16(match.Value.Substring(4, 4), 16);    // 供应商标识                 
                        //Entity.ProductID = Convert.ToUInt16(match.Value.Substring(13, 4), 16);  // 产品编号
                        Entity.ProductID = match.Value.Substring(13, 4);// 产品编号
                        if (SetupDiGetDeviceRegistryProperty(hDevInfo, DeviceInfoData, SPDRP_FRIENDLYNAME, 0, DeviceName, MAX_DEV_LEN, IntPtr.Zero))
                        {
                            Entity.ProductName = DeviceName.ToString();

                        }
                        //if(Regex.IsMatch(Entity.ProductID,"7806|781E|781e|600E|600e"))
                        if (!DeviceList.Contains(Entity))
                                DeviceList.Add(Entity);
                    }
                }
            }

            SetupDiDestroyDeviceInfoList(hDevInfo);
            if (DeviceList.Count == 0)
                return null;
            else
                return DeviceList.ToArray();
        }

        /// <summary>
        /// 结合设备安装类GUID和设备接口类GUID获取设备VIDPID
        /// </summary>
        /// <param name="setupClassGuid">设备安装类GUID，Empty忽视</param>
        /// <param name="interfaceClassGuid">设备接口类GUID，Empty忽视</param>
        /// <returns>设备VIDPID列表</returns>
        /// <remarks>
        /// 优点：直接通过设备实例ID提取VIDPID，从而无需获取设备路径来读写IO
        /// </remarks>
        public static HIDD_VIDPID[] WhoVidPid(Guid setupClassGuid, Guid interfaceClassGuid, String Enumerator, UInt16 VendorID, String ProductID)
        {
            // 根据设备安装类GUID创建空的设备信息集合
            IntPtr DeviceInfoSet;
            if (setupClassGuid == Guid.Empty)
            {
                DeviceInfoSet = SetupDiCreateDeviceInfoList(IntPtr.Zero, IntPtr.Zero);
            }
            else
            {
                DeviceInfoSet = SetupDiCreateDeviceInfoList(ref setupClassGuid, IntPtr.Zero);
            }

            if (DeviceInfoSet == new IntPtr(-1)) return null;

            // 根据设备接口类GUID创建新的设备信息集合
            IntPtr hDevInfo;
            if (interfaceClassGuid == Guid.Empty)
            {
                hDevInfo = SetupDiGetClassDevsEx(
                    IntPtr.Zero,
                    Enumerator,
                    IntPtr.Zero,
                    DIGCF.DIGCF_ALLCLASSES | DIGCF.DIGCF_PRESENT,
                    DeviceInfoSet,
                    null,
                    IntPtr.Zero);
            }
            else
            {
                hDevInfo = SetupDiGetClassDevsEx(
                    ref interfaceClassGuid,
                    null,
                    IntPtr.Zero,
                    DIGCF.DIGCF_DEVICEINTERFACE | DIGCF.DIGCF_PRESENT,
                    DeviceInfoSet,
                    null,
                    IntPtr.Zero);
            }

            if (hDevInfo == new IntPtr(-1)) return null;

            // 枚举所有设备
            List<HIDD_VIDPID> DeviceList = new List<HIDD_VIDPID>();

            // 存储设备实例ID
            StringBuilder DeviceInstanceId = new StringBuilder(256);

            // 获取设备信息数据
            UInt32 DeviceIndex = 0;
            uint MAX_DEV_LEN = 1000;
            StringBuilder DeviceName = new StringBuilder("");
            SP_DEVINFO_DATA DeviceInfoData = SP_DEVINFO_DATA.Empty;
            while (SetupDiEnumDeviceInfo(hDevInfo, DeviceIndex++, ref DeviceInfoData))
            {   // 获取设备实例ID             

                if (SetupDiGetDeviceInstanceId(hDevInfo, ref DeviceInfoData, DeviceInstanceId, DeviceInstanceId.Capacity, IntPtr.Zero))
                {
                    String PNPDeviceID = DeviceInstanceId.ToString();
                    Match match = Regex.Match(PNPDeviceID, "VID_[0-9|A-F]{4}&PID_[0-9|A-F]{4}");
                    if (match.Success)
                    {
                        HIDD_VIDPID Entity = new HIDD_VIDPID();
                        Entity.VendorID = Convert.ToUInt16(match.Value.Substring(4, 4), 16);    // 供应商标识                 
                        //Entity.ProductID = Convert.ToUInt16(match.Value.Substring(13, 4), 16);  // 产品编号
                        Entity.ProductID = match.Value.Substring(13, 4);// 产品编号
                        if((Entity.VendorID==VendorID)&&(Entity.ProductID.Contains(ProductID)))
                        {
                            if (SetupDiGetDeviceRegistryProperty(hDevInfo, DeviceInfoData, SPDRP_FRIENDLYNAME, 0, DeviceName, MAX_DEV_LEN, IntPtr.Zero))
                            {
                                Entity.ProductName = DeviceName.ToString();
                                Match MatchName = Regex.Match(PNPDeviceID, "COM[0-9]");
                                if (match.Success)
                                {
                                    Entity.PortName = Entity.ProductName.Remove(0, Entity.ProductName.IndexOf('(')+1);
                                    Entity.PortName = Entity.PortName.Remove(Entity.PortName.IndexOf(')'),1);
                                }
                            }
                            if (!DeviceList.Contains(Entity))
                                DeviceList.Add(Entity);
                        }
                       
                        //if(Regex.IsMatch(Entity.ProductID,"7806|781E|781e|600E|600e"))
                        
                    }
                }
            }

            SetupDiDestroyDeviceInfoList(hDevInfo);
            if (DeviceList.Count == 0)
                return null;
            else
                return DeviceList.ToArray();
        }

        /// <summary>
        /// 从搜索列表中提取出存在于系统中的VIDPID
        /// </summary>
        /// <param name="searchList">搜索列表</param>
        /// <returns>系统中存在的VIDPID列表</returns>
        public static HIDD_VIDPID[] WhoVidPid(HIDD_VIDPID[] searchList)
        {
            // 获取系统所有设备的VIDPID集合
            HIDD_VIDPID[] DeviceCollection = AllVidPid;
            if (DeviceCollection == null) return null;

            List<HIDD_VIDPID> ValidList = new List<HIDD_VIDPID>();
            foreach (HIDD_VIDPID VidPid in searchList)
            {
                if (Array.IndexOf(DeviceCollection, VidPid) != -1)
                {
                    ValidList.Add(VidPid);
                }
            }

            if (ValidList.Count == 0)
                return null;
            else
                return ValidList.ToArray();
        }
    }
}
