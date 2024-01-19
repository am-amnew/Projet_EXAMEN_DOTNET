namespace WebApplication2.Models
{
    public class Emprunt
    {
        public int Id { get; set; }
        public int LivreId { get; set; }
        public int AbonneId { get; set; }
        public DateTime DateEmprunt { get; set; }
        public DateTime DateRetour { get; set; }
        public Livre? Livre { get; set; } // Navigation property
        public Abonne? Abonne { get; set; } // Navigation propert
    }
}
