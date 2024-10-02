using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessBL.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FitnessBL.Manager
{
    public class FitnessManager
    {
        public void KeuzeMenu(List<Sessie> Data)
        {
            Console.WriteLine("Welkom!");
            Console.WriteLine("1 - Data klant opzoeken");
            Console.WriteLine("2 - Opzoeken op datum");

            if (int.TryParse(Console.ReadLine(), out int keuze)) // Parse user input
            {
                Console.Clear();

                switch (keuze)
                {
                    case 1:
                        // Option 1: Zoek op KlantNummer
                        ZoekOpKlantNummer(Data);
                        break;

                    case 2:
                        // Option 2: Zoek op datum
                        ZoekOpDatum(Data);
                        break;

                    default:
                        // Invalid option
                        Console.WriteLine("Ongeldige keuze. Kies 1 of 2.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Ongeldige invoer. Voer een numerieke waarde in.");
            }
        }

        static void ZoekOpDatum(List<Sessie> data)
        {
            Console.WriteLine("Voer de datum in (YYYY-mm-dd): ");

            if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime result))
            {
                Console.Clear();

                // Maak een dictionary om de sessies op te slaan volgens datum.
                Dictionary<DateTime, List<Sessie>> lijstOpDatum = new Dictionary<DateTime, List<Sessie>>();
                DateTime inputZonderTijd = result.Date;

                foreach (var item in data)
                {
                    DateTime enkelDatum = item.Datum.Date;

                    if (!lijstOpDatum.ContainsKey(enkelDatum))
                    {
                        lijstOpDatum[enkelDatum] = new List<Sessie>();
                    }
                    lijstOpDatum[enkelDatum].Add(item);
                }

                // Sessie vinden voor de opgegeven datum.
                if (lijstOpDatum.TryGetValue(result.Date, out List<Sessie> gevondenSessies))
                {

                    PrintGevondenSessies(gevondenSessies);
                }
                else
                {
                    Console.WriteLine("Geen sessies gevonden voor de ingevoerde datum.");
                }
            }
            else
            {
                Console.WriteLine("Ongeldige datum. Voer een geldig nummer in.");
            }
        }
        static void ZoekOpKlantNummer(List<Sessie> Data)
        {
            Console.WriteLine("Voer het klantennummer in: ");

            if (int.TryParse(Console.ReadLine(), out int klantenNr))
            {
                Console.Clear();


                Dictionary<int, List<Sessie>> klantenLijst = new Dictionary<int, List<Sessie>>();

                foreach (var item in Data)
                {
                    if (!klantenLijst.ContainsKey(item.KlantNr))
                    {
                        klantenLijst[item.KlantNr] = new List<Sessie>();
                    }
                    klantenLijst[item.KlantNr].Add(item);
                }


                if (klantenLijst.TryGetValue(klantenNr, out List<Sessie> gevondenSessies))
                {

                    PrintGevondenSessies(gevondenSessies);
                }
                else
                {
                    Console.WriteLine("Geen sessies gevonden voor het ingevoerde klantennummer.");
                }
            }
            else
            {
                Console.WriteLine("Ongeldig klantennummer. Voer een geldig nummer in.");
            }
        }
        private static void PrintGevondenSessies(List<Sessie> gevondenSessies)
        {
            // De gevonden sessies op het scherm doen verschijnen.
            foreach (var item in gevondenSessies)
            {
                Console.WriteLine($"SessieNr: {item.SessieNr}, DatumTijd: {item.Datum}, " +
                                  $"KlantNr: {item.KlantNr}, Duur in minuten: {item.TotaleTrainingsduur}, Gem. Snelheid: {item.GemSnelheid}, " +
                                  $"Intervals: {item.LoopIntervallen.Count}");

                foreach (var intervallen in item.LoopIntervallen)
                {
                    Console.WriteLine($"* intervalNr: {intervallen.IntervalNr}, Snelheid: {intervallen.ISnelheid}km/h, " +
                                      $"Duur in seconden: {intervallen.Tijdsduur}s");
                }
            }
        }
    }
}
