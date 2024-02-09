using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;

namespace Integracion.Adapter
{
    public abstract class BasculaColganteDibal500Adapter
    {

        #region [Dibalscop Métodos Importados]
        [DllImport("Dibalscop.dll")]
        static extern IntPtr ItemsSend(DibalScale[] myScales,
                                        int numberScales,
                                        DibalItem[] myItems,
                                        int numberItems,
                                        int showWindow, int closeTime);

        [DllImport("Dibalscop.dll")]
        static extern IntPtr ItemsSend2(DibalScale[] myScales,
                                        int numberScales,
                                        DibalItem2[] myItems,
                                        int numberItems,
                                        int showWindow, int closeTime);

        [DllImport("Dibalscop.dll")]
        static extern IntPtr DataSend();


        [DllImport("Dibalscop.dll")]
        static extern IntPtr DataSend2();


        [DllImport("Dibalscop.dll")]
        static extern IntPtr RegistersSend(DibalScale[] myScales,
                                        int numberScales,
                                        DibalRegister[] myRegisters,
                                        int numberRegisters,
                                        int showWindow, int closeTime);

        [DllImport("Dibalscop.dll")]
        static extern int ReadRegister(ref int serverHandle,
                                        byte[] register,
                                        string scaleIpAddress,
                                        int scalePortTx,
                                        string pcIpAddress,
                                        int pcPorRx,
                                        int timeOut,
                                        string pathLogs);


        [DllImport("Dibalscop.dll")]
        static extern int CancelReadRegister(ref int serverHandle, string pathLogs);
        #endregion

        #region [Estructuras]
        public struct DibalScale
        {
            public int masterAddress;
            public string IpAddress;
            public int txPort;
            public int rxPort;
            public string model;
            public string display;
            public string section;
            public int group;
            public string logsPath;

            public DibalScale(int _masterAddress, string _IpAddress, int _txPort, int _rxPort, string _model, string _display, string _section, int _group, string _logsPath)
            {
                this.masterAddress = _masterAddress;
                this.IpAddress = _IpAddress;
                this.txPort = _txPort;
                this.rxPort = _rxPort;
                this.model = _model;
                this.display = _display;
                this.section = _section;
                this.group = _group;
                this.logsPath = _logsPath;

            }
        }

        public struct DibalItem
        {
            public int code;
            public int directKey;
            public double price;
            public string itemName;
            public int type;
            public int section;
            public string expiryDate;
            public int alterPrice;
            public int number;
            public int priceFactor;
            public string textG;

            public DibalItem(int _code, int _directKey, double _price, string _name, int _type, int _section, string _expiryDate, int _alterPrice, int _number, int _priceFactor, string _textG)
            {
                this.code = _code;
                this.directKey = _directKey;
                this.price = _price;
                this.itemName = _name;
                this.type = _type;
                this.section = _section;
                this.expiryDate = _expiryDate;
                this.alterPrice = _alterPrice;
                this.number = _number;
                this.priceFactor = _priceFactor;
                this.textG = _textG;
            }
        }

        public struct DibalItem2
        {
            public char action;
            public int code;
            public int directKey;
            public double price;
            public string itemName;
            public string itemName2;
            public int type;
            public int section;
            public int labelFormat;
            public int EAN13Format;
            public int VATType;
            public double offerPrice;
            public string expiryDate;
            public string extraDate;
            public double tare;
            public string EANScanner;
            public int productClass;
            public int productDirectNumber;
            public int alterPrice;
            public string textG;

            public DibalItem2(char _action, int _code, int _directKey, double _price, string _name, string _name2, int _type, int _section, int _labelFormat, int _EAN13Format, int _VATType, double _offerPrice, string _expiryDate, string _extraDate, double _tare, string _EANScanner, int _productClass, int _productDirectNumber, int _alterPrice, string _textG)
            {
                this.action = _action;
                this.code = _code;
                this.directKey = _directKey;
                this.price = _price;
                this.itemName = _name;
                this.itemName2 = _name2;
                this.type = _type;
                this.section = _section;
                this.labelFormat = _labelFormat;
                this.EAN13Format = _EAN13Format;
                this.VATType = _VATType;
                this.offerPrice = _offerPrice;
                this.expiryDate = _expiryDate;
                this.extraDate = _extraDate;
                this.tare = _tare;
                this.EANScanner = _EANScanner;
                this.productClass = _productClass;
                this.productDirectNumber = _productDirectNumber;
                this.alterPrice = _alterPrice;
                this.textG = _textG;
            }
        }
        public struct DibalRegister
        {

            public string register;
            int sendCompleted;

            public DibalRegister(string _register, int _sendCompleted)
            {
                this.register = _register;
                this.sendCompleted = _sendCompleted;
            }
        }

        public struct DibalImageStruct
        {
            public string path;
            public int type;
            public int id;
            public int assignPlu;

            public DibalImageStruct(string _path, int _type, int _id, int _assignPlu)
            {
                this.path = _path;
                this.type = _type;
                this.id = _id;
                this.assignPlu = _assignPlu;
            }
        }


        #endregion

        #region [Variables]
        const string MODEL500RANGE = "500RANGE";
        const string MODELLSERIES = "LSERIES";
        #endregion

        #region [Variables Exportadas]
        public const int TIMEOUT = 10;	//10secons is the minimum time out for the socket

        bool cancel = false;
        bool finish = false;
        IPAddress[] Addresses;			//PC Ip addresses
        Thread[] oExportScaleThread;	//Scale threads
        string PcIp = string.Empty;		//PC Ip address
        Int32 totalRegisters = 0;
        bool exportContinuous;
        string pathLogs = string.Empty;
        #endregion

        #region [Invoke]
        delegate void SetText(string text);
        delegate void SetRegisterDs(int a, string b, string c);
        delegate void SetResultDs(string a, int b);
        #endregion
    }
}
