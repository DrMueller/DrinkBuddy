namespace DrinkBuddy.Domain.DrinkRequest.Models
{
    public class DrinkRequest
    {
        public DrinkRequest(
            int profilId,
            string situation,
            string spezialWünsche)
        {
            ProfilId = profilId;
            Situation = situation;
            SpezialWünsche = spezialWünsche;
        }

        public int ProfilId { get; }
        public string Situation { get; }
        public string SpezialWünsche { get; }
    }
}