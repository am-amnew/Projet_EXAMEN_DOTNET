namespace WebApplication2.Models
{
    public class Livre
    {
        [System.ComponentModel.DataAnnotations.Key]

        public int Id { get; set; }
        public string Titre { get; set; }
        public string Auteur { get; set; }
        public string Resume { get; set; }
        public bool EstEmprunte { get; set; }
        public List<Emprunt>? Emprunts { get; set; } // Nullable


    }
}
