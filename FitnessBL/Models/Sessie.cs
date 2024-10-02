using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessBL.Exceptions;

namespace FitnessBL.Models
{
    public class Sessie
    {
		private int _sessieNr;

		public int SessieNr
		{
			get { return _sessieNr; }
			set
            {
                if (value <= 0)
                    throw new DomeinException($"SessieNr {value} is kleiner dan 0.");
                
                _sessieNr = value;
            }
        }

        public DateTime Datum { get; set; }

        private int _klantNr;

        public int KlantNr
        {
            get { return _klantNr; }
            set 
            {
                if (value <= 0)
                    throw new DomeinException($"KlantNr {value} is kleiner dan 0.");

                _klantNr = value; 
            }
        }

        private int _totaleTrainingsduur;

        public int TotaleTrainingsduur
        {
            get { return _totaleTrainingsduur; }
            set 
            {
                if (value < 5 || value >= 180)
                    throw new DomeinException($"TotaleTrainingsduur {value} valt niet binnen 5min en 180min");

                _totaleTrainingsduur = value; 
            }
        }

        private double _gemSnelheid;

        public double GemSnelheid
        {
            get { return _gemSnelheid; }
            set 
            { 
                if (value < 5 || value > 22)
                    throw new DomeinException($"GemSnelheid {value} ligt niet binnen de 5km/h en 22km/h");

                _gemSnelheid = value; 
            }
        }




    }
}
