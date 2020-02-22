﻿using Microsoft.Management.Infrastructure;
using Microsoft.Management.Infrastructure.Options;
using System;
using System.Security;

namespace MicrosoftWMIExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //string computer = "ILPT2113";
            //string domain = "ICERTIS.COM";
            //string username = @"upendra.raparti@icertis.com";

            string computer = "myfirstvm";
            string domain = "";
            string username = "myfirstvm";


            string plaintextpassword;

            Console.WriteLine("Enter password:");
            plaintextpassword = Console.ReadLine();

            SecureString securepassword = new SecureString();
            foreach (char c in plaintextpassword)
            {
                securepassword.AppendChar(c);
            }

            // create Credentials
            CimCredential Credentials = new CimCredential(PasswordAuthenticationMechanism.Default,
                                                          domain,
                                                          username,
                                                          securepassword);

            // create SessionOptions using Credentials
            WSManSessionOptions SessionOptions = new WSManSessionOptions();
            SessionOptions.AddDestinationCredentials(Credentials);

            // create Session using computer, SessionOptions
            CimSession Session = CimSession.Create(computer, SessionOptions);

            var allVolumes = Session.QueryInstances(@"root\cimv2", "WQL", "SELECT * FROM Win32_Directory");
            var allPDisks = Session.QueryInstances(@"root\cimv2", "WQL", "SELECT * FROM Win32_DiskDrive");

            // Loop through all volumes
            foreach (CimInstance oneVolume in allVolumes)
            {
                // Show volume information

                if (oneVolume.CimInstanceProperties["DriveLetter"].ToString()[0] > ' ')
                {
                    Console.WriteLine("Volume ‘{0}’ has {1} bytes total, {2} bytes available",
                                      oneVolume.CimInstanceProperties["DriveLetter"],
                                      oneVolume.CimInstanceProperties["Size"],
                                      oneVolume.CimInstanceProperties["SizeRemaining"]);
                }

            }

            // Loop through all physical disks
            foreach (CimInstance onePDisk in allPDisks)
            {
                // Show physical disk information
                Console.WriteLine("Disk {0} is model {1}, serial number {2}",
                                  onePDisk.CimInstanceProperties["DeviceId"],
                                  onePDisk.CimInstanceProperties["Model"].ToString().TrimEnd(),
                                  onePDisk.CimInstanceProperties["SerialNumber"]);
            }



            Console.ReadKey();
        }
    }
}
