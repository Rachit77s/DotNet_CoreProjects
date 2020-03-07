using ExcelDataReader;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;



namespace ExcelToJson
{

    //Install below 2 packages
    //Install-Package ExcelDataReader
    //Install-Package ExcelDataReader.DataSet
    //Newtonsoft Json

    //Pass arguments as 
    //The runtime splits the arguments given at the console at each space.If you call
    //myApp.exe arg1 arg2 arg3
    //The Main Method gets an array of var args = new string[] { "arg1", "arg2", "arg3" }

    class Program
    {
        static void Main(string[] args)
        {
            //var inputFilePath = args[0];
            //var outputFilePath = args[1];
            //var sheetName = args[2];

            var inputFilePath = @"C:\Users\rachit.srivastava\Downloads\Config Keys_Names V2.xlsx";
            var outputFilePath = @"C:\Users\rachit.srivastava\Downloads\Config Keys_Names VRachit22333.json";
            //var sheetName = "Sheet2";
            string exceptionFilePath = @"C: \Users\rachit.srivastava\Downloads\ICMToolsException.txt";

            var curDir = Directory.GetCurrentDirectory();
            Console.WriteLine(curDir);

            //Console.WriteLine(Directory.GetDirectoryRoot(curDir));

            try
            {
                using (var inFile = File.Open(inputFilePath, FileMode.Open, FileAccess.Read))
                {
                    var excelContent = ParseExcel(inFile);
                    string json = JsonConvert.SerializeObject(excelContent);

                    // File.WriteAllText(outputFilePath, json, Encoding.UTF8);

                    //write string to file
                    //System.IO.File.WriteAllText(outputFilePath, json);

                    if (File.Exists(outputFilePath))
                    {
                        File.Delete(outputFilePath);
                        //using (var tw = new StreamWriter(inputFilePath, true))
                        //{
                        //    tw.WriteLine(json.ToString());
                        //    tw.Close();
                        //}

                        //write string to file
                        System.IO.File.WriteAllText(outputFilePath, json);
                    }
                    else if (!File.Exists(outputFilePath))
                    {
                        //using (var tw = new StreamWriter(inputFilePath, true))
                        //{
                        //    tw.WriteLine(json.ToString());
                        //    tw.Close();
                        //}

                        //write string to file
                        System.IO.File.WriteAllText(outputFilePath, json);
                    }

                }
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(exceptionFilePath, ex.Message);
            }


        }



        public static IEnumerable<Dictionary<string, object>> ParseExcel(Stream document)
        {
            try
            {
                //using (var reader = ExcelReaderFactory.CreateReader(document, new ExcelReaderConfiguration { FallbackEncoding = Encoding.GetEncoding(1252) }))

                //https://stackoverflow.com/questions/49215791/vs-code-c-sharp-system-notsupportedexception-no-data-is-available-for-encodin
                //VS Code C# - System.NotSupportedException: No data is available for encoding 1252

                //Install System.Text.Encoding.CodePages Nuget Package for below line
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                using (var reader = ExcelReaderFactory.CreateReader(document))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        UseColumnDataType = true,
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                        }
                    });
                    return MapDatasetData(result.Tables.Cast<DataTable>().First());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static IEnumerable<Dictionary<string, object>> MapDatasetData(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                var row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                yield return row;
            }

        }



    }
}
