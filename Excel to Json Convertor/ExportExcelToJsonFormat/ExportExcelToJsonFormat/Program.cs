using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.XlsIO;
using Newtonsoft.Json;
using System.IO;

//Install the Syncfusion.XlsIO.WinForms NuGet package as reference to your .NET Framework application from NuGet.Org. and Newtonsoft

namespace ExportExcelToJsonFormat
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Instantiate the spreadsheet creation engine.
                using (ExcelEngine excelEngine = new ExcelEngine())
                {
                    IApplication application = excelEngine.Excel;

                    //The workbook is opened.
                    FileStream fileStream = new FileStream("../../Data/Config Keys_Names V2.xlsx", FileMode.Open);

                    IWorkbook workbook = application.Workbooks.Open(fileStream, ExcelOpenType.Automatic);
                    IWorksheet worksheet = workbook.Worksheets[0];

                    //Export worksheet data into CLR Objects
                    IList<ConfigKeyData> customers = worksheet.ExportData<ConfigKeyData>(1, 1, worksheet.UsedRange.LastRow, workbook.Worksheets[0].UsedRange.LastColumn);

                    //open file stream
                    using (StreamWriter file = File.CreateText("../../Output/data.json"))
                    {
                        JsonSerializer serializer = new JsonSerializer();

                        //serialize object directly into file stream
                        serializer.Serialize(file, customers);
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
