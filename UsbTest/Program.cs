// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System;
using System.Management;
using System.Net.Security;

class Program
{
    static void Main()
    {
        ManagementScope scope = new ManagementScope("\\\\.\\ROOT\\CIMV2");
        ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_USBControllerDevice");

        using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
        {
            ManagementObjectCollection collection = searcher.Get();

            foreach (ManagementObject device in collection)
            {
                string dependent = (string)device["Dependent"];
                string deviceID = dependent.Substring(dependent.IndexOf("DeviceID=") + 10).Replace("\"", "");

                ManagementObject usbDevice = new ManagementObject(scope, new ManagementPath(deviceID), null);
                usbDevice.Get();

                Console.WriteLine("Device ID: " + device["DeviceID"]);
                Console.WriteLine("Description: " + device["Description"]);
                //Console.WriteLine("Manufacturer: " + device["Manufacturer"]);
                Console.WriteLine("Test");
            }
        }

        Console.ReadLine();
    }
}