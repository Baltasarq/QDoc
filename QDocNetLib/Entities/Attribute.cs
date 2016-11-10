using System;

namespace QDocNetLib.Entities {
    /// <summary>
    /// Represents an attribute within a class.
    /// </summary>
    public class Attribute: Member {
        /// <summary>
        /// Initializes a new instance of the <see cref="QDocNetLib.Entities.Attribute"/> class.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="type">The type of the attribute.</param>
        public Attribute(string name, Type type)
            :base(name, type)
        {
        }
    }
}
