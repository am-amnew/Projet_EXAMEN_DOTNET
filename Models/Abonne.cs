namespace WebApplication2.Models
{
    public class Abonne
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public List<Emprunt>? Emprunts { get; set; }

    }
}
