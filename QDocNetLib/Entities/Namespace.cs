namespace QDocNetLib.Entities {
    /// <summary>
    /// Represents a C#'s namespace.
    /// </summary>
    public class Namespace: Entity {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:QDocNetLib.Entities.Namespace"/> class.
        /// </summary>
        /// <param name="name">The name of the namespace</param>
        public Namespace(string name)
            :base(name)
        {
			this.Classes = new Container<Class>();
        }
        
        /// <summary>
        /// Gets or modifies the classes in this namespace.
        /// </summary>
        /// <value>The classes.</value>
        public Container<Class> Classes {
            get; private set;
        }
    }
}
