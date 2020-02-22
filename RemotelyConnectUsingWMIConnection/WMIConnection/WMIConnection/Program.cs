using System;
using System.Collections.Generic; //M-5

using System.Linq;  //M-5

using System.Management; //For ManagementScope
using System.Text;
using System.Text.RegularExpressions; //For Regex   M-5

namespace WMIConnection
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Console.WriteLine("Enter IP Address:");
            string ipaddress = Console.ReadLine();

            Console.WriteLine("Enter username:");
            string userName = Console.ReadLine();

            //Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();

            //IP Address : 52.172.26.219

            //User Name : icertisadmin
            
            //Password : !c3rt1s@S3aTtl3


            Command("", ipaddress, userName, password);

            //Command("", "10.2.6.135", @"ICERTIS\sangameshwar.chanale", "Dec,2019");
            //GetWebSitesRunningOnApplicationPool(new ManagementScope(), "ICM UI Website C7");

            //var flag = IsApplicationPoolRunning("10.2.6.135", "ICM UI Website C7");

            // performRequestedAction("10.2.6.135", "ICM UI Website C7", "stop");

            //ApplicationPoolWMI("10.2.6.135", @"ICERTIS\sangameshwar.chanale", "Dec,2019", "ICM UI Website C7", "stop");

          // var v = ListWebServers("10.2.6.135");

            Console.ReadKey();
        }

        //M-1
        public static void Command(string vCommand, string machineName, string username, string password)
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
                    //code here
                    // Icertis.CLM.AppPool.C7
                    if (obj["Name"].ToString().Split('/')[2] == "RachitTestAppPool")
                    {
                        // InvokeMethod - start, stop, recycle can be passed as parameters as needed.
                        obj.InvokeMethod("stop", null);
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


        //M-2
        private static bool IsApplicationPoolRunning(string servername, string strAppPool)
        {
            string sb = ""; // String to store return value

            // Connection options for WMI object
            ConnectionOptions options = new ConnectionOptions();

            // Packet Privacy means authentication with encrypted connection.
            options.Authentication = AuthenticationLevel.PacketPrivacy;

            // EnablePrivileges : Value indicating whether user privileges 
            // need to be enabled for the connection operation. 
            // This property should only be used when the operation performed 
            // requires a certain user privilege to be enabled.
            options.EnablePrivileges = true;

            // Connect to IIS WMI namespace \\root\\MicrosoftIISv2
            ManagementScope scope = new ManagementScope(@"\\" + servername + "\\root\\MicrosoftIISv2", options);

            // Query IIS WMI property IISApplicationPoolSetting
            ObjectQuery oQueryIISApplicationPoolSetting = new ObjectQuery("SELECT * FROM IISApplicationPoolSetting");

            // Search and collect details thru WMI methods
            ManagementObjectSearcher moSearcherIISApplicationPoolSetting = new ManagementObjectSearcher(scope, oQueryIISApplicationPoolSetting);
            ManagementObjectCollection collectionIISApplicationPoolSetting = moSearcherIISApplicationPoolSetting.Get();

            // Loop thru every object
            foreach (ManagementObject resIISApplicationPoolSetting in collectionIISApplicationPoolSetting)
            {
                // IISApplicationPoolSetting has a property called Name which will 
                // return Application Pool full name /W3SVC/AppPools/DefaultAppPool
                // Extract Application Pool Name alone using Split()
                if (resIISApplicationPoolSetting["Name"].ToString().Split('/')[2] == strAppPool)
                {
                    // IISApplicationPoolSetting has a property 
                    // called AppPoolState which has following values
                    // 2 = started 4 = stopped 1 = starting 3 = stopping
                    if (resIISApplicationPoolSetting["AppPoolState"].ToString() != "2")
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        //M-3
        public static void performRequestedAction(String servername, String AppPoolName, String action)
        {
            StringBuilder sb = new StringBuilder();
            ConnectionOptions options = new ConnectionOptions();
            options.Authentication = AuthenticationLevel.PacketPrivacy;
            options.EnablePrivileges = true;
            ManagementScope scope = new ManagementScope(@"\\" + servername + "\\root\\MicrosoftIISv2", options);

            // IIS WMI object IISApplicationPool to perform actions on IIS Application Pool
            ObjectQuery oQueryIISApplicationPool = new ObjectQuery("SELECT * FROM IISApplicationPool");

            ManagementObjectSearcher moSearcherIISApplicationPool = new ManagementObjectSearcher(scope, oQueryIISApplicationPool);

            ManagementObjectCollection collectionIISApplicationPool = moSearcherIISApplicationPool.Get();

            foreach (ManagementObject resIISApplicationPool in collectionIISApplicationPool)
            {
                if (resIISApplicationPool["Name"].ToString().Split('/')[2] == AppPoolName)
                {
                    // InvokeMethod - start, stop, recycle can be passed as parameters as needed.
                    resIISApplicationPool.InvokeMethod(action, null);
                }
            }
        }


        //M-4
        public static void ApplicationPoolWMI(string server, string username, string password, string appPool, string act)
        {
            ConnectionOptions co = new ConnectionOptions();
            co.Username = username;
            co.Password = password;
            co.Impersonation = ImpersonationLevel.Impersonate;
            co.Authentication = AuthenticationLevel.PacketPrivacy;
            string objPath = "IISApplicationPool.Name='W3SVC/AppPools/" + appPool + "'";

            ManagementScope scope = new ManagementScope(@"\\" + server + @"\root\MicrosoftIISV2", co);
            using (ManagementObject mc = new ManagementObject(objPath))
            {
                mc.Scope = scope;
                mc.InvokeMethod("Stop", null, null);
                //switch (act)
                //{

                //    //case act.Start:
                //    //    mc.InvokeMethod("Start", null, null);
                //    //    break;
                //    //case eAction.Stop:
                //    //    mc.InvokeMethod("Stop", null, null);
                //    //    break;
                //    //case eAction.Recycle:
                //    //    mc.InvokeMethod("Recycle", null, null);
                //    //    break;
                //}
            }

        }


        //M-5
        public static IEnumerable<string> GetWebSitesRunningOnApplicationPool(ManagementScope scope, string applicationPoolName)
        {
            //get application names from application pool
            string path = string.Format("IIsApplicationPool.Name='W3SVC/APPPOOLS/{0}'", applicationPoolName);
            ManagementPath managementPath = new ManagementPath(path);
            ManagementObject classInstance = new ManagementObject(scope, managementPath, null);
            ManagementBaseObject outParams = classInstance.InvokeMethod("EnumAppsInPool", null, null);
         
            //get web server names from application names
            IEnumerable<string> nameList = (outParams.Properties["Applications"].Value as string[]) //no null reference exception even there is no application running
                                           .Where(item => !String.IsNullOrEmpty(item)) //but you get empty strings so they are filtered
                                           .ToList() //you get something like /LM/W3SVC/1/ROOT
                                           .Select(item => item.Slice(item.NthIndexOf("/", 2) + 1, item.NthIndexOf("/", 4))); //your WebServer.Name is between 2nd and 4th slahes


            //get server comments from names
            List<string> serverCommentList = new List<string>();
            foreach (string name in nameList)
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, new ObjectQuery(string.Format("SELECT ServerComment FROM IIsWebServerSetting WHERE Name = '{0}'", name)));

                serverCommentList.AddRange(from ManagementObject queryObj in searcher.Get() select queryObj["ServerComment"].ToString());
            }

            return serverCommentList;
        }


        //M-6
        public static List<string> ListWebServers(string serverName)
        {
            ConnectionOptions options = new ConnectionOptions();

            options.Authentication = AuthenticationLevel.PacketPrivacy;
            options.Impersonation = ImpersonationLevel.Impersonate;
            options.EnablePrivileges = true;

            string path = string.Format("\\\\{0}\\root\\MicrosoftIISv2", serverName);

            ManagementScope scope = new ManagementScope(path, options);

            //Rachit Try1
            //ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM IIsWebServer");

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT* FROM IIsWebServerSetting");
            searcher.Scope = scope;

            ManagementObjectCollection result = searcher.Get();

            List<string> list = new List<string>();

            foreach (ManagementObject obj in result)
            {
                //Rachit try 1
                //string name = obj.Properties["Name"].Value.ToString();
                string name = obj.Properties["ServerComment"].Value.ToString();
                list.Add(name);
            }
            return list;
        }



    }// Class End
}
