using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EMSIRails.Models
{
    public class VoyageArret
    {
        public int idvoyage { get; set; }
        public int idtrain { get; set; }
        public string villeDepart { get; set; }
        public string villeArrive { get; set; }
        public Nullable<TimeSpan> heureDepart { get; set; }
        public Nullable<TimeSpan> heureArrive { get; set; }

    }
}