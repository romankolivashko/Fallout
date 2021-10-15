using System.Collections.Generic;
using System;

namespace Fallout.Models
{
    public class Survivor
    {
        public Survivor()
        {
            this.JoinEntities = new HashSet<ShelterSurvivor>();
            this.Completed = false;
        }

        public int SurvivorId { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public DateTime Arrived {get;set;}

        public virtual ICollection<ShelterSurvivor> JoinEntities { get; set; }
    }
}