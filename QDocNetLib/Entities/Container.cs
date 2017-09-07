using System.Collections.Generic;

namespace QDocNetLib.Entities {
    /// <summary>
    /// A collection of Entities, used in at least namespaces and classes.
    /// </summary>
    public class Container<T> where T: Entity {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:QDocNetLib.Entities.Container`1"/> class.
        /// </summary>
        public Container()
        {
            this.entities = new List<T>();
        }

        /// <summary>
        /// Add the specified <see cref="Entity"/> to this namespace.
        /// </summary>
        /// <param name="e">The entity (a class?) to add.</param>
        public void Add(T e)
        {
            this.entities.Add( e );
        }

        /// <summary>
        /// Gets or sets the types inside this namespace.
        /// </summary>
        /// <value>The types.</value>
        public T[] Entities {
            get {
                return this.entities.ToArray();
            }
            set {
                this.entities.Clear();
                this.entities.AddRange( value );
            }
        }
        
        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:QDocNetLib.Entities.Container`1"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:QDocNetLib.Entities.Container`1"/>.</returns>
        public override string ToString()
        {
            string toret = string.Join<Entity>( ", ", this.Entities );
            return string.Format("[Container: Types={0}]", toret );
        }

        private List<T> entities;
    }
}
