using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamService.Models
{
    public class Team
    {
        public Team()
        {
            Members = new List<Member>();
        }

        public Team(string name) : this()
        {
            Name = name;
        }

        public Team(string name, Guid guid) : this(name)
        {
            ID = guid;
        }

        public string Name { get; set; }
        public Guid ID { get; set; }
        public ICollection<Member> Members { get; set; }
    }
}
