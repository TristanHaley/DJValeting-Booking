namespace Domain.Models
{
    public class Customer
    {
        public string Firstname     { get; set; }
        public string Surname       { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail  { get; set; }
        public int    CustomerId    { get; set; }
    }
}