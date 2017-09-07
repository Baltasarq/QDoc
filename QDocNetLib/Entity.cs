using System.Text;
using System.Collections.Generic;
using System;
using QDocNetLib.Entities;

namespace QDocNetLib {
	/// <summary>
	/// An entity to be documented.
	/// </summary>
    public class Entity {
		/// <summary>
		/// Initializes a new instance of the <see cref="QDocNetLib.Entity"/> class.
		/// </summary>
		/// <param name="name">The name of the Entity.</param>
        public Entity(string name)
        {
            this.Name = name;
			this.SysType = null;
        }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
        public string Name {
            get; set;
        }

		/// <summary>
		/// Sets the structural type.
		/// </summary>
		/// <value>The type of the entity, as a <see cref="System.Type"/> instance.</value>
		public Type SysType {
			get; set;
		}

		/// <summary>
		/// Gets or sets the documentation.
		/// </summary>
		/// <value>The documentation, as a string.</value>
		public Documentation SummaryDocumentation {
			get; set;
		}

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:QDocNetLib.Entity"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:QDocNetLib.Entity"/>.</returns>
        public override string ToString()
        {
            return string.Format("[Entity: Name={0}, SysType={1}, SummaryDocumentation={2}]", Name, SysType, SummaryDocumentation);
        }

		/// <summary>
		/// Creates the entity corresponding to the specified type.
		/// </summary>
		/// <param name="type">Type.</param>
		public static Entity Create(Type type)
		{
			Entity toret = null;

			if ( type.BaseType == typeof(Object) ) {
				toret = new Class( type.Name );
				toret.SysType = type;
			}

			return toret;
		}
    }
}

