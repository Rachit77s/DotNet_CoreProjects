using Microsoft.EntityFrameworkCore;
using System;

namespace SOLIDPrinciples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }


    #region Open Close Principle

    #region Violating Open Close Principle
    public class SocialMediaPost
    {
        public void CreateSocialMediaPost(DbContext dbContext, string postMessage)
        {
            if (postMessage.StartsWith("#"))
            {
                dbContext.Add(postMessage);
            }
            else
            {
                dbContext.AddAsync(postMessage);
            }

            //What if someone wants to save message starting with @, then we need to add another condition
        }
    }
    #endregion

    #region Correct Implementation of Open Close Principle
    public class SocialMediaPostImplementation
    {
        public virtual void CreateSocialMediaPost(DbContext dbContext, string postMessage)
        {
            dbContext.Add(postMessage);
        }
    }

    public class TagPost : SocialMediaPostImplementation
    {
        public override void CreateSocialMediaPost(DbContext dbContext, string postMessage)
        {
            //AddAsTag hypothetical method name
            dbContext.Add(postMessage);
        }
    }

    public class AtTheRatePost : SocialMediaPostImplementation
    {
        public override void CreateSocialMediaPost(DbContext dbContext, string postMessage)
        {
            //AddAsAtTheRate(@) hypothetical method name
            dbContext.Add(postMessage);
        }
    }

    #endregion

    #endregion


    #region Liskov Principle

    // Not following the Liskov Substitution Principle  
    public class AccessDataFile
    {
        public string FilePath { get; set; }
        public virtual void ReadFile()
        {
            // Read File logic  
            Console.WriteLine($"Base File {FilePath} has been read");
        }
        public virtual void WriteFile()
        {
            //Write File Logic  
            Console.WriteLine($"Base File {FilePath} has been written");
        }
    }

    public class AdminDataFileUser : AccessDataFile
    {
        public override void ReadFile()
        {
            // Read File logic  
            Console.WriteLine($"File {FilePath} has been read");
        }

        public override void WriteFile()
        {
            //Write File Logic  
            Console.WriteLine($"File {FilePath} has been written");
        }
    }


    public class RegularDataFileUser : AccessDataFile
    {
        public override void ReadFile()
        {
            // Read File logic  
            Console.WriteLine($"File {FilePath} has been read");
        }

        public override void WriteFile()
        {
            //Write File Logic  
            throw new NotImplementedException();
        }
    }

    //Calling class not following Liskov Substitution Principle  

    //AccessDataFile accessDataFile = new AdminDataFileUser();
    //accessDataFile.FilePath = @"c:\temp\a.txt";  
    //accessDataFile.ReadFile();  
    //accessDataFile.WriteFile();  

    //AccessDataFile accessDataFileR = new RegularDataFileUser();
    //accessDataFileR.FilePath = @"c:\temp\a.txt";  
    //accessDataFileR.ReadFile();  
    //accessDataFileR.WriteFile();  // Throws exception  

    #endregion


    #region Interface Segregation Principle



    #endregion
}
