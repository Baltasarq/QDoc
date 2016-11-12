namespace QDocNetLib.Entities {
	using System;

    /// <summary>
    /// Represents any member, including attributes,
    /// properties and methods.
    /// </summary>
    public class Member: Entity {
        /// <summary>
        /// Initializes a new instance of the <see cref="QDocNetLib.Entities.Member"/> class.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="name">The type of the member.</param>
        public Member(string name, Type type)
            :base(name)
        {
            this.Type = type;
        }

        /// <summary>
        /// Gets or sets the type of the member.
        /// </summary>
        /// <value>The type, as a Type instance.</value>
        public Type Type {
            get; set;
        }

        public override string ToString()
        {
            return string.Format(
                "[Member: Name={0}, Type={0}]",
                this.Name,
                this.Type );
        }
    }
}
