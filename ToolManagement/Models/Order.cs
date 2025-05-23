namespace ToolManagement.Models
{
    /// <summary>
    /// Klasa reprezentująca zamówienie.
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
        /// Identyfikator narzędzia.
        /// </summary>
        public int ToolId { get; set; }

        /// <summary>
        /// Ilość zamówionych narzędzi.
        /// </summary>
        public int Quantity { get; set; }
    }
}
