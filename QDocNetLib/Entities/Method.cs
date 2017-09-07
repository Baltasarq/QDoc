namespace QDocNetLib.Entities {
	using System;
	using System.Text;
	using System.Collections.Generic;

    /// <summary>
    /// Represents a method within a class.
    /// </summary>
    public class Method: Member {
        /// <summary>
        /// A formal parameter in a method.
        /// </summary>
        public class Param: Entity {
            /// <summary>
            /// Initializes a new instance of the <see cref="QDocNetLib.Entities.Method.Param"/> class.
            /// </summary>
            /// <param name="name">The name of the parameter.</param>
            /// <param name="type">The type of the parameter.</param>
            public Param(string name, Type type)
                :base(name)
            {
            }

            /// <summary>
            /// Gets or sets the type of the member.
            /// </summary>
            /// <value>The type, as a Type instance.</value>
            public Type Type {
                get; set;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QDocNetLib.Entities.Method"/> class.
        /// </summary>
        /// <param name="name">The name of the method.</param>
        /// <param name="type">The type of the method.</param>
        public Method(string name, Type type)
            :base(name, type)
        {
			this.Parameters = new Container<Param>();
        }
        
        /// <summary>
        /// Gets the parameters in the method.
        /// </summary>
        /// <value>The parameters.</value>
        public Container<Param> Parameters {
            get; private set;
        }

		/// <summary>
		/// Gets or sets the return documentation.
		/// </summary>
		/// <value>The return documentation, as a string.</value>
		public Documentation ReturnDocumentation {
			get; set;
		}

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:QDocNetLib.Entities.Method"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:QDocNetLib.Entities.Method"/>.</returns>
        public override string ToString()
        {
            StringBuilder toret = new StringBuilder();

            toret.Append( base.ToString() );
            toret.Append( '(' );
            toret.Append( this.Parameters.ToString() );
            toret.Append( ')' );


            return toret.ToString();
        }
    }
}
