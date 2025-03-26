namespace ToolManagement.Models
{
    /// <summary>
    /// Klasa reprezentująca klienta.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Identyfikator klienta.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Imię klienta.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Nazwisko klienta.
        /// </summary>
        public string LastName { get; set; }
    }
}