using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    /// <summary>
    /// Modello per Indirizzo (Rubrica).
    /// </summary>
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }    // Riferimento all'utente proprietario dell'indirizzo
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
    }
}
