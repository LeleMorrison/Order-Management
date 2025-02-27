using System;

namespace Shared.Models
{
    /// <summary>
    /// Modello per Ordine.
    /// </summary>
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }      // Riferimento all'utente che ha effettuato l'ordine
        public int AddressId { get; set; }   // Riferimento all'indirizzo di spedizione
        
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public List<OrderItem> Items { get; set; } = new();
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
