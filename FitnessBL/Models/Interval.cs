using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessBL.Exceptions;

namespace FitnessBL.Models
{
    public class Interval
    {
		private int _intervalNr;

		public int IntervalNr
		{
			get { return _intervalNr; }
			set 
			{ 
				if (value < 0)
					throw new DomeinException($"IntervalNr {value} is kleiner dan 0");

				_intervalNr = value; 
			}
		}

		private int _tijdsduur;

		public int Tijdsduur
		{
			get { return _tijdsduur; }
			set 
			{
				if (value < 5 || value > 10800)
					throw new DomeinException($"Tijdsduur {value} ligt niet tussen 5Sec en 10800Sec");

				_tijdsduur = value; 
			}
		}

		private double _ISnelheid;

		public double ISnelheid
		{
			get { return _ISnelheid; }
			set 
			{ 
				if (value < 5 || value > 22)
					throw new DomeinException($"ISnelheid {value} ligt niet tussen 5Km/H en 22 Km/H");
				
				_ISnelheid = value;
			}
		}

        public Interval(int intervalNr, int tijdsduur, double iSnelheid)
        {
            this.IntervalNr = intervalNr;
			this.Tijdsduur = tijdsduur;
			this.ISnelheid = iSnelheid;
        }



    }
}
