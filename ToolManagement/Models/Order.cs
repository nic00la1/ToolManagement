namespace ToolManagement.Models
{
    /// <summary>
    /// Klasa reprezentuj�ca zam�wienie.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Identyfikator zam�wienia.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identyfikator klienta.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Identyfikator narz�dzia.
        /// </summary>
        public int ToolId { get; set; }

        /// <summary>
        /// Ilo�� zam�wionych narz�dzi.
        /// </summary>
        public int Quantity { get; set; }
    }
}
