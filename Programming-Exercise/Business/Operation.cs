using Newtonsoft.Json;
using Programming_Exercise.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Programming_Exercise.Business.File;

namespace Programming_Exercise.Business
{
    public class Operation
    {
        List<EndPoint> localEndpoints = new List<EndPoint>();
        FileManager fileManager = new FileManager();

        public Operation()
        {
            var endPoints = fileManager.GetFileContent();

            if (endPoints != null && endPoints.Any())
            {
                localEndpoints = endPoints;
            }
        }

        public void DeleteEndPoint(string serialNumber = null)
        {
            string searchSerialNumber = string.Empty;

            if (!string.IsNullOrEmpty(serialNumber))
            {
                searchSerialNumber = serialNumber;
            }
            else
            {
                Console.WriteLine("Before delete, let's verify if serial number already exists, please input 'Endpoint Serial Number': ");
                Console.ReadLine();
            }

            if (!string.IsNullOrEmpty(searchSerialNumber))
            {
                Console.WriteLine("Searching Endpoint by Serial Number... ");
                Thread.Sleep(3000);

                if (SearchEndPoint(searchSerialNumber) != null)
                {
                    localEndpoints.Remove(localEndpoints.Where(x => x.SerialNumber == searchSerialNumber).FirstOrDefault());

                    fileManager.DeleteFile();
                    if (localEndpoints.Any())
                    {
                        string jsonString = JsonConvert.SerializeObject(localEndpoints);
                        fileManager.CreateFile(content: jsonString, newFile: false);
                    }
                    Console.WriteLine("This serial number was removed of collection!");
                    Thread.Sleep(3000);

                }
                else
                {
                    Console.WriteLine("This serial number doesn't exists in collection!");
                    Thread.Sleep(3000);
                }
            }
        }

        public void ListAllEndPoint()
        {
            var endPoints = fileManager.GetFileContent();
            if (endPoints != null && endPoints.Any())
            {
                foreach (var item in endPoints)
                {
                    PrintItem(item);
                }
                Thread.Sleep(3000);
            }
            else
            {
                Console.WriteLine("The collection is empty");
                Thread.Sleep(3000);
            }
        }

        public EndPoint SearchEndPoint(string serialNumber = null, bool mainMenu = false)
        {
            if (mainMenu)
            {
                Console.WriteLine("Input 'Endpoint Serial Number': ");
                string searchSerialNumber = Console.ReadLine();

                if (!string.IsNullOrEmpty(searchSerialNumber))
                {
                    Console.WriteLine("Searching Endpoint by Serial Number... ");
                    Thread.Sleep(3000);

                    var item = localEndpoints.Where(x => x.SerialNumber == searchSerialNumber).FirstOrDefault();

                    if (item != null)
                    {
                        PrintItem(item);
                        Thread.Sleep(3000);
                        return item;
                    }
                    else
                    {
                        Console.WriteLine("Serial number doesn't exists in collection");
                        Thread.Sleep(3000);
                    }
                }
                return null;
            }
            return localEndpoints.Where(x => x.SerialNumber == serialNumber).FirstOrDefault();
        }

        public void CreateEndPoint(bool edit = false)
        {
            try
            {
                var returnCreate = ValidateCreate(edit);
                if (returnCreate != null && !edit)
                {
                    localEndpoints.Add(returnCreate);

                    string jsonString = JsonConvert.SerializeObject(localEndpoints);
                    fileManager.CreateFile(content: jsonString, newFile: false);

                    Console.WriteLine("The new Endpoint was created sucessfully");
                    Thread.Sleep(3000);

                }
                else if (returnCreate != null && edit)
                {
                    var updateItem = localEndpoints.Where(x => x.SerialNumber == returnCreate.SerialNumber).FirstOrDefault();
                    if (updateItem != null)
                    {
                        localEndpoints.Remove(localEndpoints.Where(x => x.SerialNumber == returnCreate.SerialNumber).FirstOrDefault());
                        localEndpoints.Add(returnCreate);
                    }

                    string jsonString = JsonConvert.SerializeObject(localEndpoints);
                    fileManager.CreateFile(content: jsonString, newFile: false);

                    Console.WriteLine("The new Endpoint was updated sucessfully");
                    Thread.Sleep(3000);

                }
                else
                    Console.WriteLine("Error to create a new Endpoint");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        private EndPoint ValidateCreate(bool edit = false)
        {
            EndPoint endPointAttributes;

            Console.WriteLine("Input 'Endpoint Serial Number': ");

            string searchSerialNumber = Console.ReadLine();

            if (!string.IsNullOrEmpty(searchSerialNumber))
            {
                Console.WriteLine("Searching Endpoint by Serial Number... ");
                Thread.Sleep(3000);

                var endPoint = SearchEndPoint(searchSerialNumber);

                if (endPoint != null && !edit)
                {
                    Console.WriteLine("This serial number already exists in collection!");
                    return null;
                }
                else if (endPoint != null && edit)
                {
                    Console.WriteLine("Let's edit the item in collection!");
                    SetSwitchState(endPoint);
                    return endPoint;
                }
                else
                {
                    Console.WriteLine("No one serial number created with this id, let's create a new...");

                    int meterNumber;
                    int meterModel;

                    endPointAttributes = new EndPoint();
                    endPointAttributes.SerialNumber = searchSerialNumber;

                    SetSwitchState(endPointAttributes);

                    Console.WriteLine("Input the Firmware Version: ");
                    var inputFirmware = Console.ReadLine().ToString();
                    endPointAttributes.FirmwareVersion = !string.IsNullOrEmpty(inputFirmware) ? inputFirmware : "";

                    Console.WriteLine("Input the 'Meter Number': ");
                    while (!int.TryParse(Console.ReadLine(), out meterNumber))
                    {
                        Console.Write("This is not valid input. Please enter an integer value: ");
                    }
                    endPointAttributes.MeterNumber = meterNumber;


                    Console.WriteLine("Input the 'Meter Model' - 1)NSX1P2W 2)NSX1P3W 3)NSX2P3W 4)NSX3P4W: ");
                    while (!int.TryParse(Console.ReadLine(), out meterModel))
                    {
                        Console.Write("This is not valid input. Please enter a valid option value: ");
                    }
                    switch (meterModel)
                    {
                        case 1:
                            endPointAttributes.MeterModel = (int)MeterModelEnum.NSX1P2W;
                            break;
                        case 2:
                            endPointAttributes.MeterModel = (int)MeterModelEnum.NSX1P3W;
                            break;
                        case 3:
                            endPointAttributes.MeterModel = (int)MeterModelEnum.NSX2P3W;
                            break;
                        case 4:
                            endPointAttributes.MeterModel = (int)MeterModelEnum.NSX3P4W;
                            break;
                        default:
                            Console.WriteLine("Error, invalid option selected");
                            break;
                    }

                    return new EndPoint
                    {
                        SerialNumber = endPointAttributes.SerialNumber,
                        SwitchState = endPointAttributes.SwitchState,
                        FirmwareVersion = endPointAttributes.FirmwareVersion,
                        MeterModel = endPointAttributes.MeterModel,
                        MeterNumber = endPointAttributes.MeterNumber
                    };
                }
            }
            else
                return null;
        }

        private static int SetSwitchState(EndPoint endPointAttributes)
        {
            int switchState;
            Console.WriteLine("Input the 'Switch State' - 0)Disconnected 1)Connected 2)Armed:");
            while ((!int.TryParse(Console.ReadLine(), out switchState)))
            {
                Console.WriteLine("Error, invalid option selected, try again!");
            }
            switch (switchState)
            {
                case 0:
                    endPointAttributes.SwitchState = (int)StateEnum.Disconnected;
                    break;
                case 1:
                    endPointAttributes.SwitchState = (int)StateEnum.Connected;
                    break;
                case 2:
                    endPointAttributes.SwitchState = (int)StateEnum.Armed;
                    break;
                default:
                    Console.WriteLine("Error, invalid option selected, try again!");
                    break;
            }

            return switchState;
        }

        private static void PrintItem(EndPoint item)
        {
            Console.WriteLine("------------- Printing item -----------");
            Console.WriteLine("Serial Number: " + item.SerialNumber);
            Console.WriteLine("Meter Model: " + Enum.GetName(typeof(MeterModelEnum), item.MeterModel));
            Console.WriteLine("Meter Number: " + item.MeterNumber);
            Console.WriteLine("Firmware Version: " + item.FirmwareVersion);
            Console.WriteLine("Switch State: " + Enum.GetName(typeof(StateEnum), item.SwitchState));
            Console.WriteLine("---------------------------------------\r\n");
        }
    }
}
