namespace ToolManagement.Models
{
    /// <summary>
    /// Klasa reprezentuj¹ca zamówienie.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Identyfikator zamówienia.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identyfikator klienta.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Identyfikator narzêdzia.
        /// </summary>
        public int ToolId { get; set; }

        /// <summary>
        /// Iloœæ zamówionych narzêdzi.
        /// </summary>
        public int Quantity { get; set; }
    }
}
