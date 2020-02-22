using Microsoft.Web.Administration; //For ServerManager()
using System;
using System.DirectoryServices; // For DirectoryEntries

//RUN THE PROGRAM IN ADMINISTRATIVE MODE


namespace IterateAppPools
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryEntries appPools = new DirectoryEntry("IIS://localhost/W3SVC/AppPools").Children;

            foreach (DirectoryEntry appPool in appPools)
            {
                Console.WriteLine(appPool.Name);
            }

            //M-II
            foreach (var appPool in new ServerManager().ApplicationPools)
            {
                Console.WriteLine(appPool.Name);
            }


            //M-III
            ServerManager server = new ServerManager();

            ApplicationPoolCollection applicationPools = server.ApplicationPools;
            foreach (ApplicationPool pool in applicationPools)
            {
                //get the AutoStart boolean value
                bool autoStart = pool.AutoStart;

                //get the name of the ManagedRuntimeVersion
                string runtime = pool.ManagedRuntimeVersion;

                //get the name of the ApplicationPool
                string appPoolName = pool.Name;

                //get the identity type
                ProcessModelIdentityType identityType = pool.ProcessModel.IdentityType;

                //get the username for the identity under which the pool runs
                string userName = pool.ProcessModel.UserName;

                //get the password for the identity under which the pool runs
                string password = pool.ProcessModel.Password;
            }

            Console.ReadKey();

        }
    }
}
