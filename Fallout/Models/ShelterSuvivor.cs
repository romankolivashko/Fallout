namespace Fallout.Models
{
  public class ShelterSurvivor
  {       
    public int ShelterSurvivorId { get; set; }
    public int SurvivorId { get; set; }
    public int ShelterId { get; set; }
    public virtual Survivor Survivor { get; set; }
    public virtual Shelter Shelter { get; set; }
  }
}