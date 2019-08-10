using System;
using System.Collections.Generic;

namespace CC.Models
{
	public class Cinema
	{
		public int ID { get; set; }

        public string Name { get; set; }

        public double Lontitude { get; set; }

        public double Latitude { get; set; }

        public string OpeningHour { get; set; }

        public string ClosingHour { get; set; }
	}
}