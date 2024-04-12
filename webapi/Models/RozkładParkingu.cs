using System;
using System.Collections.Generic;

namespace webapi.Models
{
    public class RozkładParkingu
    {
        public long Id { get; set; }
        public long? IdParkingu { get; set; }
        public long? StanMiejsca { get; set; }

        public virtual Parking Parking { get; set; } = null!;
        public virtual ICollection<KodParkingu> KodyParkingu { get; set; } = new List<KodParkingu>();
    }
}
