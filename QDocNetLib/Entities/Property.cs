using System;

namespace QDocNetLib.Entities {
    /// <summary>
    /// Represents a property.
    /// </summary>
    public class Property: Member {
        /// <summary>
        /// Initializes a new instance of the <see cref="QDocLib.Entities.Property"/> class.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="type">The type of the property.</param>
        public Property(string name, Type type)
            :base(name, type)
        {
        }
    }
}
