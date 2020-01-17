using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mail.MainModule.DTO
{
    public class FBNotificacionDTO
    {
        public string[] registration_ids { get; set; }
        public Data data { get; set; }
        public Notification notification { get; set; }
    }

    public class Data
    {
        //public Data()
        //{
           
        //}
        //public string ShortDesc { get; set; }
        //public string IncidentNo { get; set; }
        //public string Description { get; set; }
        public string Tipo { get; set; }
        public string OrderNo { get; set; }
    }

    public class Notification
    {
        public Notification()
        {
            sound = "default";
        }
        public string title { get; set; }
        public string text { get; set; }
        public string sound { get; set; }
    }
}
