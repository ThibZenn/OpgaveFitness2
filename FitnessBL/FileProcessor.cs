using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessBL.Models;

namespace FitnessBL
{
    public class FileProcessor
    {
        public List<Sessie> LeesFile(string FilePath)
        {
            //lijst met loopsessies aanmaken om daar de gemaakte sessies in te verzamelen.
            List<Sessie> sessies = new List<Sessie>();
            //Dictionary aanmaken om makkelijk te kunnen checken op dubbels.
            Dictionary<int, Sessie> sessieDictionary = new Dictionary<int, Sessie>();
            string logFilePath = @"C:\Users\thiba\Documents\HOGENT\Semester2\ProgGevorderd1\Labo1\OpgaveFitness2\Errors";
            //ErrorFile leegmaken bij het begin van uitlezen.
            if (File.Exists(logFilePath)) File.WriteAllText(logFilePath, string.Empty);

            using (StreamReader sr = new StreamReader(FilePath))
            {
                //Lijn die gelezen wordt.
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    try
                    {
                        List<string> lijnInStukjes = SplitInput(line);

                        int sessieNr = int.Parse(lijnInStukjes[0]);

                        // Check if the LoopSessie already exists 
                        if (!sessieDictionary.ContainsKey(sessieNr))
                        {
                            DateTime datum = DateTime.ParseExact(lijnInStukjes[1], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                            int klantNr = int.Parse(lijnInStukjes[2]);
                            int totaleTrainingsduur = int.Parse(lijnInStukjes[3]);
                            double gemSnelheid = double.Parse(lijnInStukjes[4]);

                            Sessie nieuweSessie = new Sessie(sessieNr, datum, klantNr, totaleTrainingsduur, gemSnelheid);
                            nieuweSessie.LoopIntervallen = new List<Interval>(); // Initialize the list of intervals
                            sessieDictionary[sessieNr] = nieuweSessie; // Add to dictionary
                        }

                        // Now read interval details
                        Sessie huidigeSessie = sessieDictionary[sessieNr]; // Get the existing session

                        // Read the interval details only
                        int Snr = int.Parse(lijnInStukjes[5]);
                        int intervalTijd = int.Parse(lijnInStukjes[6]);
                        double intervalSnelheid = double.Parse(lijnInStukjes[7]);
                        Interval huidigeInterval = new Interval(Snr, intervalTijd, intervalSnelheid);

                        // Add the interval to the existing LoopSessie
                        huidigeSessie.LoopIntervallen.Add(huidigeInterval);
                    }
                    catch (Exception ex)
                    {
                        File.AppendAllText(logFilePath, ex.Message + Environment.NewLine);
                    }
                }
            }

            // Transfer all LoopSessie objects from the dictionary to the list
            sessies.AddRange(sessieDictionary.Values);

            return sessies;
        }


        private List<string> SplitInput(string input)
        {
            List<string> losseStukjes;

            int startindex = input.IndexOf('(') + 1;
            int eindindex = input.IndexOf(")");

            string waardes = input.Substring(startindex, eindindex - startindex);

            losseStukjes = new List<string>(waardes.Split(','));
            losseStukjes[1] = losseStukjes[1].Trim('\'');
            return losseStukjes;
        }

    }
}
