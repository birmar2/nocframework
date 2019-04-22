using BusinessLayer;
using BusinessLayer.Interfaces;
using BusinessLayer.Support._interfaces;
using Common;
using Common.Interfaces;
using Common.Models;
using LiteDB;
using PresentationLayer.DrawingModule;
using PresentationLayer.DrawingModule._interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using static Common.Helpers;

namespace PresentationLayer
{
    public partial class Form1 : Form
    {
        // switch between MySQL or LiteDB
        private readonly bool isMySQL = false;

        private IUIFactory uiFactory;

        private IFrameWork frameWork;

        private EventMediator eventMediator;

        private IDrawingModulePL drawingModulePL;

        private Random rnd;

        private List<string> rndIp = new List<string>();

        private List<string> rndMac = new List<string>();

        private readonly int maxPortNumber = 6;

        private readonly int maxPcNumber = 2;
        private readonly int networkPCNumber = 4;

        private readonly int networkSize = 500;

        private readonly int pathLength = 20;

        public int NetworkSize { get => networkSize; }

        public Form1()
        {
            eventMediator = new EventMediator();
            // feliratkozás az ErrorMessage Event-re
            // hibaüzenet esetén az OnErrorMessage metódus megjeleníti a hibaüzenetet
            eventMediator.ErrorMessage += OnErrorMessage;

            FactorySupport factorySupport = new FactorySupport();
            Directory.CreateDirectory(@"C:\Log");
            Log.Logger = new LoggerConfiguration().WriteTo.File(@"C:\Log\Log.txt", rollingInterval: RollingInterval.Hour).CreateLogger();
            Directory.CreateDirectory(@"C:\db");
            LiteRepository repo = new LiteRepository(ApplicationConfig.DbConnectionString);

            frameWork = factorySupport.Create(isMySQL, repo, eventMediator);

            drawingModulePL = new DrawingModulePL(uiFactory, frameWork, eventMediator);

            InitializeComponent();

            InitializeRnd();

            // példa: lekéri a GetService() -vel a UIFactory szervízt
            // utána kirajzol egy button-t a felhasználói felületen.

            uiFactory = (IUIFactory)(frameWork.GetService(typeof(IUIFactory)));

            if (uiFactory == null)
            {
                throw new Exception("Hibás UIFactory!");
            }

            // példa lekéri a stílusát egy gombnak
            uiFactory.GetButton(this.button1, "blueButton", "search", "DrawingModule");

            // Betölti az egész adatbázist a memóriába
            //MessageBox.Show("Beolvasom az adatbázist a memóriába");
            frameWork.LoadDatabase();

            if (frameWork.ItemActiveCount < 90)
            {
            // betölti az ábrákat
            LoadSymbols();

            // létrehozza a hálózatot az adatbázisban
            SetupNetwork();
            }

            // példa: kirajzol egy ábrát
            ISymbol exampleSymbol = frameWork.GetSymbol(2);

            pictureBox1.Image = exampleSymbol.GetImage();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            // példa: lekéri a GetService() -vel az IErrorservice szervízt
            // utána megjeleníti a hibaüzenetet

            IErrorService errorService = (IErrorService)(frameWork.GetService(typeof(IErrorService)));

            if (errorService == null)
            {
                throw new Exception("Hibás ErrorService!");
            }

            IError error = new Error(ErrorType.InputError, "Példa a beviteli hibára");
            errorService.Write(error);

            // hibeüzenet 2. példa
            IError errorExample2 = new Error(ErrorType.DatabaseError, "Példa: Adatbázis üzenet a datalayertől");
            errorService.Write(errorExample2);

            this.Focus();
        }

        private void InitializeRnd()
        {
            Random rnd = new Random();
            for (int i = 0; i < 101; i++)
            {
                rndIp.Add(rnd.Next(1, 255).ToString());
                rndMac.Add(rnd.Next(1, 255).ToString("X"));
            }
        }

        private void SetupNetwork()
        {
            rnd = new Random();
            SymbolName symbolName;

            IList<int> itemIdList = new List<int>();
            IList<int> pcList = new List<int>();
            for (int i = 0; i < NetworkSize; i++)
            {
                symbolName = rnd.Next(1, 10) % 2 == 0 ? SymbolName.Router : SymbolName.Switch;

                int itemId = SetupItem(i, symbolName, "router");

                itemIdList.Add(itemId);
            }

            for (int i = 0; i < networkPCNumber; i++)
            {
                int itemId = SetupItem(i, SymbolName.Pc, "PC");

                pcList.Add(itemId);
            }

            int actualPc;
            int nextRouter, actualRouter;
            int? portSource, portDestination;
            for (int i = 0; i < maxPcNumber; i++)
            {
                for (int j = networkPCNumber - 1; j >= maxPcNumber; j--)
                {
                    actualPc = pcList[i];
                    portSource = frameWork.GetFreePortOfActiveItem(actualPc);
                    nextRouter = itemIdList[rnd.Next(0, NetworkSize-1)];
                    portDestination = frameWork.GetFreePortOfActiveItem(nextRouter);
                    if (portSource == null)
                    {
                        break;
                    }
                    else if (portDestination == null)
                    {
                        int index = NetworkSize/2;
                        while(portDestination == null && index > 0)
                        {
                            nextRouter = itemIdList[rnd.Next(0, NetworkSize - 1)];
                            portDestination = frameWork.GetFreePortOfActiveItem(nextRouter);
                            index--;
                        }
                        if (index == 0)
                        {
                            break;
                        }
                    }

                    SetupConnection(actualPc, (int)portSource, nextRouter, (int)portDestination);

                    actualRouter = nextRouter;

                    for (int k = 0; k < pathLength; k++)
                    {
                        portSource = frameWork.GetFreePortOfActiveItem(actualRouter);
                        nextRouter = itemIdList[rnd.Next(0, NetworkSize-1)];
                        portDestination = frameWork.GetFreePortOfActiveItem(nextRouter);
                        if (portSource == null)
                        {
                            break;
                        }
                        else if (portDestination == null)
                        {
                            int index = NetworkSize / 2;
                            while (portDestination == null && index > 0)
                            {
                                nextRouter = itemIdList[rnd.Next(0, NetworkSize - 1)];
                                portDestination = frameWork.GetFreePortOfActiveItem(nextRouter);
                                index--;
                            }
                            if (index == 0)
                            {
                                break;
                            }
                        }

                        SetupConnection(actualRouter, (int)portSource, nextRouter, (int)portDestination);

                        actualRouter = nextRouter;
                    }

                    portSource = frameWork.GetFreePortOfActiveItem(actualRouter);
                    actualPc = pcList[j];
                    portDestination = frameWork.GetFreePortOfActiveItem(actualPc);
                    if (portSource == null)
                    {
                        break;
                    }
                    else if (portDestination == null)
                    {
                        int PCindex = j - 1;
                        while (portDestination == null && PCindex >= maxPcNumber)
                        {
                            actualPc = pcList[PCindex];
                            portDestination = frameWork.GetFreePortOfActiveItem(actualPc);
                            PCindex--;
                        }
                        if (PCindex < maxPcNumber)
                        {
                            break;
                        }
                    }

                    SetupConnection(actualRouter, (int)portSource, actualPc, (int)portDestination);
                }
            }
        }

        private void SetupConnection(int sourceItemId, int sourcePortNumber, 
            int destinationItemId, int destinationPortNumber)
        {
            // létrehoz egy connection-t és egy reverse connection-t
            IConnection connection = new Connection
            {
                Name = "UTP kábel " + sourceItemId + "/" + sourcePortNumber
                + "/" + destinationItemId + "/" + destinationPortNumber,
                SourceItemId = sourceItemId,
                SourcePortNumber = sourcePortNumber,
                DestinationItemId = destinationItemId,
                DestinationPortNumber = destinationPortNumber
            };

            frameWork.AddConnection(connection);
        }

        private IPortActive SetupPort(int itemID, int portNumber)
        {
            int rndIndex = rnd.Next(1,92);
            // példa: létrehoz egy aktív portot
            IPortActive port1 = new PortActive
            {
                MacAddress = rndMac[rndIndex] + "-" + rndMac[rndIndex + 1]
                        + rndMac[rndIndex+2] + "-" + rndMac[rndIndex+3] 
                        + "-" + rndMac[rndIndex+4] + "-" + rndMac[rndIndex+5],
                IPAddress = rndIp[rndIndex]+"."+ rndIp[rndIndex+1] + "." 
                            + rndIp[rndIndex+2] + "." + rndIp[rndIndex+3],
                ItemID = itemID,
                PortNumber = portNumber,
                PortName = "port 0/" + portNumber,
                PhysicalLocation = "ajto mellett",
                PortID = "0/" + portNumber ,
                PortConfig = "switchport mode access; switchport access vlan 1000",
                PortPhysicalType = "UTP",
                SymbolID = 2
            };

            frameWork.AddPortActive(port1.ItemID, port1);
            return port1;
        }

        

        private int SetupItem(int itemNumber, SymbolName itemType, string name)
        {
            int itemId = 0;
            string deviceId = "";
            switch (itemType)
            {
                case SymbolName.Router:
                    deviceId = "router";
                    break;
                case SymbolName.Switch:
                    deviceId = "switch";
                    break;
                case SymbolName.Pc:
                    deviceId = "pc";
                    break;
                default:
                    deviceId = "router";
                    break;
            }

            itemNumber++;

            IItemActive item = new ItemActive
            {
                DeviceName = name + itemNumber,
                DeviceID = deviceId + itemNumber,
                Notes = "ajtó mellett balra",
                SymbolID = (int)itemType
            };
            frameWork.AddItemActive(item);
            itemId = item.Id;

            for (int z = 1; z < maxPortNumber; z++)
            {
            IPortActive port = SetupPort(itemId, z);
            frameWork.AddPortActive(itemId, port);
            }

            return itemId;
        }

        /// <summary>
        /// létrehoz egy ábrát
        /// </summary>
        private void SetSymbol()
        {
            // példa: létrehoz egy ábrát
            ISymbol symbolRouter = new Symbol();
            symbolRouter.Load("router", @".\Resources\router.jpg");
            frameWork.AddSymbol(symbolRouter);
        }

        private void LoadSymbols()
        {
            IList<string> symbolNameList = Helpers.GetSymbolNames();

            string symbolPath = @".\Resources\";
            ISymbol loadSymbol;
            foreach (string itemName in symbolNameList)
            {
                loadSymbol = new Symbol();
                loadSymbol.Load(itemName, symbolPath + itemName + ".jpg");
                frameWork.AddSymbol(loadSymbol);
            }
        }

        // ToDo: ezt a metódust külön osztályba tenni, hogy más UI elemek, Form-ok is fel tudjanak iratkozni
        // az ErrorMessage Event-re
        private void OnErrorMessage(object sender, ErrorMessageEventArgs e)
        {
            // Az errorService által küldött üzenetet egy label-en megjeleníti, 
            // vagy errorType-tól függőn MesseageBox-ban is megjelenítheti a hibaüzenetet

            if (e.errorType == ErrorType.DatabaseError)
            {
                MessageBox.Show(e.message);
            }
            else
            {
            errorLabel.Text = e.message;
            }
        }

        private void ButtonClickOpenDrawingModule(object sender, EventArgs e)
        {
            ((DrawingModulePL)drawingModulePL).ShowDialog();
        }

        /// <summary>
        /// példa létrehoz két aktív eszközt:
        /// </summary>
        private void SetupTwoItems()
        {
            int itemId, itemId2;
            // példa: létrehoz egy új aktív eszközt
            IItemActive item = new ItemActive
            {
                DeviceName = "akármi",
                DeviceID = "router1",
                Notes = "ajtó mellett balra",
                SymbolID = 1
            };

            frameWork.AddItemActive(item);
            itemId = item.Id;
            //MessageBox.Show("adatbázisba elmentve: eszköz id: " + item.Id.ToString());

            // példa: létrehoz egy második aktív eszközt
            IItemActive item2 = new ItemActive
            {
                DeviceName = "teve",
                DeviceID = "switch",
                Notes = "ajtó mellett balra",
                SymbolID = 2
            };

            frameWork.AddItemActive(item2);
            itemId2 = item2.Id;
            //MessageBox.Show("adatbázisba elmentve: eszköz id: " + item2.Id.ToString());

            //példa:  egy eszközt lekér
            IItemActive item3 = frameWork.GetItemActive(itemId);
            //MessageBox.Show("lekérve: eszköz neve: " + item3.DeviceName);
        }
    }
}
