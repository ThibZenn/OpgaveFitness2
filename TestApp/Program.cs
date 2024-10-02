using FitnessBL;
using FitnessBL.Models;
using FitnessBL.Manager;

namespace TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // File data
            string filePath = @"C:\Users\thiba\Documents\HOGENT\Semester2\ProgGevorderd1\Labo1\insertRunning\insertRunningTest2.txt";

            // Instantie aanmaken
            FileProcessor processor = new FileProcessor();
            List<Sessie> Data = processor.LeesFile(filePath);
            FitnessManager fitnessManager = new FitnessManager();

            //KeuzeMenu laten verschijnen in console.
            fitnessManager.KeuzeMenu(Data);
            
        }
    }
}
