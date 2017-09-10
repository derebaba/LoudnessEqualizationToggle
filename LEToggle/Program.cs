using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEToggle
{
    class Program
    {
        static void Main(string[] args)
        {
            //var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\MMDevices\\Audio\\Render\\{104cea38-3c3a-499c-9546-f2f441f7a3b8}\\FxProperties", true);
            if (myKey != null)
            {
                byte[] loudnessEq = (byte[])myKey.GetValue("{fc52a749-4be9-4510-896e-966ba6525980},3");
                if (loudnessEq[9] == 0xff)
                {
                    myKey.SetValue("{fc52a749-4be9-4510-896e-966ba6525980},3", new Byte[] { 0x0b, 0, 0x35, 0xa1, 0x01, 0, 0, 0, 0, 0, 0, 0 });
                } else
                {
                    myKey.SetValue("{fc52a749-4be9-4510-896e-966ba6525980},3", new Byte[] { 0x0b, 0, 0x35, 0xa1, 0x01, 0, 0, 0, 0xff, 0xff, 0, 0 });
                }
                myKey.Close();
            } else
            {
                Console.WriteLine("Can't read value from registry.");
            }
        }
    }
}
