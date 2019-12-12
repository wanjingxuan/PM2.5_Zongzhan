using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PM2._5_Project
{
    class ReportData
    {

       
       
        //标准pm2.5设备数据
        private string _timepmStart;
        private string _timepmEnt;
        private string _hpm;
        private string _hpmOther;

      
        public string TimepmStart
        {
            get
            {
                return _timepmStart;
            }

            set
            {
                _timepmStart = value;
            }
        }

        public string TimepmEnt
        {
            get
            {
                return _timepmEnt;
            }

            set
            {
                _timepmEnt = value;
            }
        }
        public string Hpm
        {
            get
            {
                return _hpm;
            }

            set
            {
                _hpm = value;
            }
        }

        public string HpmOther
        {
            get
            {
                return _hpmOther;
            }

            set
            {
                _hpmOther = value;
            }
        }

       
        public static ReportData inserData() {

            ReportData rd = new ReportData();
            rd.TimepmStart = DateTime.Now.ToLongTimeString();
            rd.TimepmEnt = DateTime.Now.ToLongTimeString();
            rd._hpm = "100";
            rd._hpmOther ="200";

            return rd;
        }
    }
}
