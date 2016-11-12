namespace QDocNetLib.Entities {
	using System.Collections.Generic;

    /// <summary>
    /// Represents a class.
    /// </summary>
    public class Class: Entity {
        /// <summary>
        /// Initializes a new instance of the <see cref="QDocNetLib.Entities.Class"/> class.
        /// </summary>
        /// <param name="name">The name of the class.</param>
        public Class(string name)
            :base(name)
        {
			this.members = new List<Member>();
        }

        /// <summary>
        /// Add a new member to this class.
        /// </summary>
        /// <param name="m">The new member, as a Member instance.</param>
        public void Add(Member m)
        {
            this.members.Add( m );
        }

        /// <summary>
        /// Gets the members of this class.
        /// </summary>
        /// <value>The members, as a primitive array.</value>
        public Member[] Members {
            get {
                var toret = new Member[ this.members.Count ];
                this.members.CopyTo( toret, 0 );
                return toret;
            }
        }

        private List<Member> members;
    }
}
