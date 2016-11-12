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
            /// Initializes a new instance of the <see cref="QDocNetLib.Entities.Method+Param"/> class.
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
			this.parameters = new List<Param>();
        }

        /// <summary>
        /// Add a new param to this method.
        /// </summary>
        /// <param name="m">The new param, as a Param instance.</param>
        public void Add(Param m)
        {
            this.parameters.Add( m );
        }

        /// <summary>
        /// Gets the params of this method.
        /// </summary>
        /// <value>The parameters, as a primitive array.</value>
        public Param[] Params {
            get {
                var toret = new Param[ this.parameters.Count ];
                this.parameters.CopyTo( toret, 0 );
                return toret;
            }
        }

        public override string ToString()
        {
            StringBuilder toret = new StringBuilder();

            toret.Append( base.ToString() );
            toret.Append( '(' );
            toret.Append( StringFromEntityCollection( this.Params, ", " ) );
            toret.Append( ')' );


            return toret.ToString();
        }

        private List<Param> parameters;
    }
}
