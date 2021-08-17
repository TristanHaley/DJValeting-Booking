using System.Collections;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Customer
    {
        public Customer()
        {
            Bookings = new HashSet<Booking>();
        }
        
        public         string               Firstname     { get; set; }
        public         string               Surname       { get; set; }
        public         string               ContactNumber { get; set; }
        public         string               ContactEmail  { get; set; }
        public         int                  CustomerId    { get; set; }
        public virtual ICollection<Booking> Bookings      { get; set; }
    }
}