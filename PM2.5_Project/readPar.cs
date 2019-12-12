using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;

namespace PM2._5_Project
{
	class readPar
	{
		
		private static string _airT;

		public static string AirT
		{
			get {
				if (_airT == null) {
					_airT ="0";
				
				}
				return _airT; }
			set { 
			
				_airT =  Math.Round(((double.Parse(value)/65530*150)),2).ToString();
			
			 
			}


		}
		private static System.DateTime _date;

		public static System.DateTime Date
		{
			get {
				
				
				return _date; }
			set { _date = value; }
		}
		private static string _detectionPM;

		public static string DetectionPM
		{
			get {
				if (_detectionPM == null)
				{
					_detectionPM = "0";

				}
				
				return _detectionPM; }
			set { _detectionPM = value; }
		}
		private static string _diluteAirF;

		public static string DiluteAirF
		{
			get {
                if (_diluteAirF == null)
				{
					_diluteAirF = "0";

				}
				
				return _diluteAirF; }
            set { _diluteAirF = Math.Round(((double.Parse(value) / 65530 * 30)), 2).ToString(); }
		}

		private static int _setTime;

		public static int  SetTime
		{
			get {
				return _setTime; }
			set { _setTime = value; }
		}
		private static string _ean581F;

		public static string Ean581F
		{
			get {

                if (_ean581F == null)
                {
                    _ean581F = "0";

                }
               
				return _ean581F; }
            set {

                if (Double.Parse(DiluteAirF)>2)
                {
                    _ean581F = (GlobalParameter.eanvalue + new Random().NextDouble()).ToString("0.00");

                }
                else {
                    _ean581F = Math.Round(((double.Parse(value) / 65530 * 100)), 2).ToString();
                }

            }
        }
		private static string _mixT;

		public static string MixT
		{
			get {
				if (_mixT == null)
				{
					_mixT = "0";

				}
				
				return _mixT; }
			set { _mixT = Math.Round(((double.Parse(value) / 65535 * 100)), 2).ToString(); }
		}
		private static string _p;

		public static string P
		{
			get {
				if (_p == null)
				{
					_p = "0";

				}
				
				return _p; }
			set { _p = Math.Round(((double.Parse(value) / 65535 *2000-1000)), 0).ToString(); 
			}
		}
		private static string _standardPM;

		public static string StandardPM
		{
			get {
				if (_standardPM == null)
				{
					_standardPM = "0";

				}
				
				return _standardPM; }
			set { _standardPM = value; }
		}
		private static string _airValve;

		public static string AirValve
		{
			get { return _airValve; }
			set {

				_airValve = value;
			}
		}
		private static string _dustValve;

		public static string DustValve
		{
			get { return _dustValve; }
			set {

				_dustValve = value;
			
			}
		}

		private static DateTime _recordTime;

		public static DateTime RecordTime
		{
			get { return _recordTime; }
			set
			{

				_recordTime = value;

			}
		}
		private static Color _airValueColor;

		public static Color AirValueColor
		{
			get { return _airValueColor; }
			set
			{
			
				_airValueColor = value;

			}
		}

		private static Color _dustValueColor;

		public static Color DustValueColor
		{
			get { return _dustValueColor; }
			set
			{

				_dustValueColor = value;

			}
		}


		public static void SetData(string [] data ){

            //总站修改顺序
           
            AirT  = data[2];
			 P = data[1];
			 MixT = data[2];
			 Ean581F = data[4];
            DiluteAirF = data[5];
           
           
			//StandardPM = data[5];
			//DetectionPM = data[6];
			StandardPM = "0";
			DetectionPM = "0";
		   
		}
	}

    class readParPM {

        private static string _loading;
        private static string _frequency;
        private static string _temperature;
        private static string _humidity;
        private static string _pressure;
        private static string _oneHour;

        public static string Loading
        {
            get
            {
                return _loading;
            }

            set
            {
                _loading = value;
            }
        }

        public static string Frequency
        {
            get
            {
                return _frequency;
            }

            set
            {
                _frequency = value;
            }
        }

        public static string Temperature
        {
            get
            {
                return _temperature;
            }

            set
            {
                _temperature = value;
            }
        }

        public static string Pressure
        {
            get
            {
                return _pressure;
            }

            set
            {
                _pressure = value;
            }
        }

        public static string OneHour
        {
            get
            {
                return _oneHour;
            }

            set
            {
                _oneHour = value;
            }
        }

        public static string Humidity
        {
            get
            {
                return _humidity;
            }

            set
            {
                _humidity = value;
            }
        }

        public static void SetData(string[] data)
        {

            OneHour = data[0];
            Loading = data[1];
            Frequency = data[2];
            Temperature = data[3];
            Humidity = data[4];
            Pressure = data[5];
        
         
        }
    }
}
