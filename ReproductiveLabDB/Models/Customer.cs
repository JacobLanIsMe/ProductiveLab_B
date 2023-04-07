using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class Customer
    {
        public Customer()
        {
            CourseOfTreatments = new HashSet<CourseOfTreatment>();
            InverseSpouseNavigation = new HashSet<Customer>();
        }

        public int SqlId { get; set; }
        public Guid CustomerId { get; set; }
        public string Name { get; set; } = null!;
        public int GenderId { get; set; }
        public DateTime Birthday { get; set; }
        public Guid Spouse { get; set; }

        public virtual Gender Gender { get; set; } = null!;
        public virtual Customer SpouseNavigation { get; set; } = null!;
        public virtual ICollection<CourseOfTreatment> CourseOfTreatments { get; set; }
        public virtual ICollection<Customer> InverseSpouseNavigation { get; set; }
    }
}
