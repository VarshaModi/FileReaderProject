using System;
using System.IO;
using System.Text;

namespace FileReader
{
    class Program
    {

        public static void Main(string[] args)
        {
            try
            {
                string inputClaimFilePath = @"D:\inpClaims.txt";
                string inputProviderFilePath = @"D:\inpProvider.txt";
                if (!File.Exists(inputClaimFilePath) || !File.Exists(inputProviderFilePath))
                {
                    Console.WriteLine("Either or both of the files do not exist in the specified path.");
                    Console.ReadLine();
                }
                else
                {
                    string folder = @"C:\Temp";
                    bool folderExists = Directory.Exists(folder);
                    if (!folderExists)
                        Directory.CreateDirectory(folder);
                    string fileName = folder + "\\outAdmit.txt";
                    FileProcessor fileprocessor = new FileProcessor(inputClaimFilePath, inputProviderFilePath, fileName);
                    fileprocessor.ReadAllClaims();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}