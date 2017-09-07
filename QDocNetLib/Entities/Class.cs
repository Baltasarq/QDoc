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
			this.Members = new Container<Member>();
        }
        
        /// <summary>
        /// Gets or modifies the members of the class.
        /// </summary>
        /// <value>The members.</value>
        public Container<Member> Members {
            get; private set;
        }
    }
}
