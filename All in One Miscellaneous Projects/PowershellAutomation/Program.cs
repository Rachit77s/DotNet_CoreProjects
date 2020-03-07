using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security;
using System.Text;

namespace PowershellAutomation
{

    //Import Nuget Package --> System.Management.Automation
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }


        static void RunPsScriptMethod2()
        {
            StringBuilder sb = new StringBuilder();

            PowerShell psExec = PowerShell.Create();
            psExec.AddCommand(@"C:\Users\d92495j\Desktop\test.ps1");
            psExec.AddArgument(DateTime.Now);

            Collection<PSObject> results;
            Collection<ErrorRecord> errors;
            results = psExec.Invoke();
            errors = psExec.Streams.Error.ReadAll();

            if (errors.Count > 0)
            {
                foreach (ErrorRecord error in errors)
                {
                    sb.AppendLine(error.ToString());
                }
            }
            else
            {
                foreach (PSObject result in results)
                {
                    sb.AppendLine(result.ToString());
                }
            }

            Console.WriteLine(sb.ToString());
        }

        private static string RunScript(string scriptText)
        {
            // create Powershell runspace
            Runspace runspace = RunspaceFactory.CreateRunspace();
            // open it
            runspace.Open();
            // create a pipeline and feed it the script text
            Pipeline pipeline = runspace.CreatePipeline();

            //pipeline.Commands.AddScript(scriptText);
            //pipeline.Commands.AddScript("ipconfig");
            pipeline.Commands.AddScript(@"C: \Users\rachit.srivastava\Downloads\first_script.ps1");


            // add an extra command to transform the script
            // output objects into nicely formatted strings
            // remove this line to get the actual objects
            // that the script returns. For example, the script
            // "Get-Process" returns a collection
            // of System.Diagnostics.Process instances.

            pipeline.Commands.Add("Out-String");
            // pipeline.Commands.AddScript();
            
            // execute the script
            Collection<PSObject> results = pipeline.Invoke();

            // close the runspace
            runspace.Close();

            Console.WriteLine("Results count" + results.Count.ToString());

            Console.WriteLine("Results string" + results.ToString());

            // convert the script result into a single string
            StringBuilder stringBuilder = new StringBuilder();
            foreach (PSObject obj in results)
            {
                //obj.TypeNames
                stringBuilder.AppendLine(obj.ToString());
            }

            Console.WriteLine(stringBuilder.ToString());
            return stringBuilder.ToString();

        }


        private static void RunScriptMethod3(string scriptText)
        {
            // Initialize PowerShell engine
            var shell = PowerShell.Create();

            // Add the script to the PowerShell object
            shell.Commands.AddScript("Get-Service");

            // Execute the script
            var results = shell.Invoke();

            // display results, with BaseObject converted to string
            // Note : use |out-string for console-like output
            if (results.Count > 0)
            {
                // We use a string builder ton create our result text
                var builder = new StringBuilder();

                foreach (var psObject in results)
                {
                    // Convert the Base Object to a string and append it to the string builder.
                    // Add \r\n for line breaks
                    builder.Append(psObject.BaseObject.ToString() + "\r\n");
                }

                // Encode the string in HTML (prevent security issue with 'dangerous' caracters like < >
                //var text = Server.HtmlEncode(builder.ToString());
            }
        }


        private static void RunScriptMethod4(string scriptText)
        {
            //M-II

            //Microsoft.PowerShell.SDK for resolving exception
            PSCredential creds = new PSCredential("myfirstvm", StringToSecureString("tE&sJk8c&gqG"));
            System.Uri uri = new Uri("http://Exchange-Server/powershell?serializationLevel=Full");

            Runspace runspace = RunspaceFactory.CreateRunspace();

            PowerShell powershell = PowerShell.Create();
            PSCommand command = new PSCommand();
            command.AddCommand("New-PSSession");
            command.AddParameter("ConfigurationName", "Microsoft.Exchange");
            command.AddParameter("ConnectionUri", uri);
            command.AddParameter("Credential", creds);
            command.AddParameter("Authentication", "Default");
            powershell.Commands = command;
            runspace.Open(); powershell.Runspace = runspace;
            Collection<PSSession> result = powershell.Invoke<PSSession>();
            if (result.Count > 0)
            {
                // We use a string builder ton create our result text
                var builder = new StringBuilder();

                foreach (var psObject in result)
                {
                    // Convert the Base Object to a string and append it to the string builder.
                    // Add \r\n for line breaks
                    builder.Append(psObject.ToString() + "\r\n");
                }

                // Encode the string in HTML (prevent security issue with 'dangerous' caracters like < >
                //var text = IServer.HtmlEncode(builder.ToString());

            }


            powershell = PowerShell.Create();
            command = new PSCommand();
            command.AddCommand("Set-Variable");
            command.AddParameter("Name", "ra");
            command.AddParameter("Value", result[0]);
            powershell.Commands = command;
            powershell.Runspace = runspace;
            powershell.Invoke();

            powershell = PowerShell.Create();
            command = new PSCommand();
            command.AddScript("Import-PSSession -Session $ra");
            powershell.Commands = command;
            powershell.Runspace = runspace;
            powershell.Invoke();
        }
        

        public static SecureString StringToSecureString(string Password)
        {

            string plaintextpassword;

            //Console.WriteLine("Enter password:");
            //plaintextpassword = Console.ReadLine();

            SecureString securepassword = new SecureString();
            foreach (char c in Password)
            {
                securepassword.AppendChar(c);
            }

            return securepassword;
        }


        //Best Method
        public static void NewPowershellFunction(string vmIpAddress, string userName, string password)
        {
            using (Runspace runspace = RunspaceFactory.CreateRunspace())
            {
                runspace.Open();


                string script = File.ReadAllText(@"C:\Users\rachit.srivastava\Downloads\Invoke-RestartIIS.ps1");


                PowerShell powershell = PowerShell.Create();
                powershell.Runspace = runspace;
                powershell.AddScript(script);

                //invoke iis reset function    to load the Script
                powershell.Invoke();

                try
                {
                    powershell.AddCommand("Invoke-RestartIIS")
                              .AddParameters(new Dictionary<string, string> {
                                                                                { "VMPublicIP", vmIpAddress },
                                                                                { "VMUserName", userName },
                                                                                { "VMPassword", password },
                                                                                { "UIURI", "www.devtenant1.local" },
                                                                                { "APIURI", "www.devtenant1.local" },
                                                                            });
                    Collection<PSObject> results;
                    Collection<ErrorRecord> errors;
                    results = powershell.Invoke();
                    errors = powershell.Streams.Error.ReadAll();
                    StringBuilder stringBuilder = new StringBuilder();


                    if (errors.Count > 0)
                    {
                        foreach (ErrorRecord error in errors)
                        {
                            stringBuilder.AppendLine(error.ToString());
                        }
                    }
                    else
                    {
                        foreach (PSObject result in results)
                        {
                            stringBuilder.AppendLine(result.ToString());
                        }
                    }

                    Console.WriteLine(stringBuilder.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw new Exception($"IIS Restart failed with an error {ex.Message}");
                }
            }
        }

    }
  
}


//private Collection<PSObject> RunPsScript(string psScriptPath)
//{
//    string psScript = string.Empty;
//    if (File.Exists(psScriptPath))
//        psScript = File.ReadAllText(psScriptPath);
//    else
//        throw new FileNotFoundException("Wrong path for the script file");

//    RunspaceConfiguration config = RunspaceConfiguration.Create();
//    PSSnapInException psEx;
//    //add Microsoft SharePoint PowerShell SnapIn
//    PSSnapInInfo pssnap = config.AddPSSnapIn("Microsoft.SharePoint.PowerShell", out psEx);
//    //create powershell runspace
//    Runspace cmdlet = RunspaceFactory.CreateRunspace(config);
//    cmdlet.Open();
//    RunspaceInvoke scriptInvoker = new RunspaceInvoke(cmdlet);
//    // set powershell execution policy to unrestricted
//    scriptInvoker.Invoke("Set-ExecutionPolicy Unrestricted");
//    // create a pipeline and load it with command object
//    Pipeline pipeline = cmdlet.CreatePipeline();
//    try
//    {
//        // Using Get-SPFarm powershell command 
//        pipeline.Commands.AddScript(psScript);
//        pipeline.Commands.AddScript("Out-String");
//        // this will format the output
//        Collection<PSObject> output = pipeline.Invoke();
//        pipeline.Stop();
//        cmdlet.Close();
//        // process each object in the output and append to stringbuilder  
//        StringBuilder results = new StringBuilder();
//        foreach (PSObject obj in output)
//        {
//            results.AppendLine(obj.ToString());
//        }
//        return output;
//    }
//    catch (Exception ex)
//    {
//        return null;
//    }
//}
