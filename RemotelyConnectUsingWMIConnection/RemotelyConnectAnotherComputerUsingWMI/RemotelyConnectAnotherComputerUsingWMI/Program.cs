using System;
using System.Management;

namespace RemotelyConnectAnotherComputerUsingWMI
{
    class Program
    {

        //IMPORT SYSTEM.MANAGEMENT NUGET PACKAGE

        static void Main(string[] args)
        {

            Console.WriteLine("Enter IP Address:");
            string ipaddress = Console.ReadLine();

            Console.WriteLine("Enter username:");
            string userName = Console.ReadLine();

            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();

            //IP Address : 52.172.26.219
            //User Name : icertisadmin
            //Password : !c3rt1s@S3aTtl3


            RDCUsingWMI(ipaddress, userName, password, "");

            //Command("", "10.2.6.135", @"ICERTIS\sangameshwar.chanale", "Dec,2019");

        }

        //M-1
        public static void RDCUsingWMI(string machineName, string username, string password, string action)
        {
            ManagementScope Scope = null;
            ConnectionOptions ConnOptions = null;
            ObjectQuery ObjQuery = null;
            ManagementObjectSearcher ObjSearcher = null;

            try
            {
                ConnOptions = new ConnectionOptions();
                ConnOptions.Impersonation = ImpersonationLevel.Impersonate;
                ConnOptions.EnablePrivileges = true;

                //Rachit
                // Packet Privacy means authentication with encrypted connection.
                ConnOptions.Authentication = AuthenticationLevel.PacketPrivacy;

                //Rachit
                //If you connect to a remote computer in a different domain or using a different user name and password, then you must use a ConnectionOptions object in the call to the ManagementScope.


                //local machine
                if (machineName.ToUpper() == Environment.MachineName.ToUpper())
                    //Scope = new ManagementScope(@"\ROOT\CIMV2", ConnOptions);

                    // Connect to IIS WMI namespace \\root\\MicrosoftIISv2
                    Scope = new ManagementScope(@"\ROOT\MicrosoftIISv2", ConnOptions);

                else
                {
                    //remote machine
                    ConnOptions.Username = username;
                    ConnOptions.Password = password;

                    //Rachit commented below
                    //Scope = new ManagementScope(@"\\" + machineName + @"\ROOT\CIMV2", ConnOptions);

                    //NEw code for IIS
                    // Connect to IIS WMI namespace \\root\\MicrosoftIISv2
                    Scope = new ManagementScope(@"\\" + machineName + "\\root\\MicrosoftIISv2", ConnOptions);
                }
                Scope.Connect();


                //Rachit commented below Try 1
                //ObjQuery = new ObjectQuery(@"SELECT * FROM Win32_Directory");

                // Query IIS WMI property IISApplicationPoolSetting
                ObjQuery = new ObjectQuery(@"SELECT * FROM IISApplicationPoolSetting");

                //Try 3 --> IIsWebServerSetting
                //ObjQuery = new ObjectQuery(@"SELECT * FROM IIsWebServerSetting");

                // Search and collect details thru WMI methods
                ObjSearcher = new ManagementObjectSearcher(Scope, ObjQuery);

                foreach (ManagementObject obj in ObjSearcher.Get()) //ERROR HAPPEN HERE
                {                   
                    // Icertis.CLM.AppPool.C7
                    if (obj["Name"].ToString().Split('/')[2] == "RachitTestAppPool")
                    {
                        // InvokeMethod - start, stop, recycle can be passed as parameters as needed.
                        obj.InvokeMethod("stop", null);
                        //obj.InvokeMethod(action, null);
                    }
                }

                //// IIS WMI object IISApplicationPool to perform actions on IIS Application Pool
                //ObjectQuery oQueryIISApplicationPool = new ObjectQuery("SELECT * FROM IISApplicationPool");

                //ManagementObjectSearcher moSearcherIISApplicationPool = new ManagementObjectSearcher(Scope, oQueryIISApplicationPool);

                //ManagementObjectCollection collectionIISApplicationPool = moSearcherIISApplicationPool.Get();

                //foreach (ManagementObject resIISApplicationPool in collectionIISApplicationPool)
                //{
                //    if (resIISApplicationPool["Name"].ToString().Split('/')[2] == "Icertis.CLM.AppPool.C7")
                //    {
                //        // InvokeMethod - start, stop, recycle can be passed as parameters as needed.
                //        resIISApplicationPool.InvokeMethod("stop", null);
                //    }
                //}

                if (ObjSearcher != null)
                {
                    ObjSearcher.Dispose();
                }
            }
            catch (ManagementException exception)
            {
                Console.WriteLine("Please turn ON the Windows Management Instrumentation Service!!!");
                throw exception;
            }
            catch (UnauthorizedAccessException exception)
            {
                Console.WriteLine(" The given credentials are not valid!!!");
                throw exception;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
