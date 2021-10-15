using System.Collections.Generic;

namespace Fallout.Models
{
  public class Shelter
  {
    public Shelter()
    {
        this.JoinEntities = new HashSet<ShelterSurvivor>();
    }

    public int ShelterId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<ShelterSurvivor> JoinEntities { get; set; }
  }
}