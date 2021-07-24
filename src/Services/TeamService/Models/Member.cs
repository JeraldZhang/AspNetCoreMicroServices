using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamService.Models
{
    public class Member
    {
        public Member()
        {

        }

        public Member(Guid guid)
        {
            ID = guid;
        }

        public Member(string firstName, string lastName, Guid guid) : this(guid)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public Guid ID { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
