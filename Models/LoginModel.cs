namespace WebApplication2.Models
{
    public class LoginModel
    {
        [System.ComponentModel.DataAnnotations.Key]

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
