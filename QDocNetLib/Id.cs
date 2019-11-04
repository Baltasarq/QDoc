// QDocNet - (c) 2018 Baltasar MIT License <baltasarq@gmail.com>

namespace QDocNetLib {
    using System;
    
    /// <summary>
    /// Represents and id, such as T:Cars.Car.
    /// </summary>
    public class Id: IEquatable<Id> {
        /// <summary>Each char corresponding a type in the docs.</summary>
        public char[] ReprType = { 'T', 'N', 'E', 'C', 'M', 'P', 'F' };
        
        /// <summary>Corresponding enum types to the char types.</summary>
        public enum AType {
            /// <summary>Default value: typically, a Class, or an Enum.</summary>
            Type,
            /// <summary>A namespace.</summary>
            Namespace,
            /// <summary>An enum.</summary>
            Enum,
            /// <summary>A class.</summary>
            Class,
            /// <summary>Method in a class.</summary>
            Method,
            /// <summary>A Property in a class.</summary>
            Property,
            /// <summary>An id inside an enum.</summary>
            EnumValue };
        
        /// <summary>
        /// Creates a new <see cref="T:QDocNetLib.Id"/>, parsing an id like:
        ///     "T:Cars.Car.Model" or "Car.Maker".
        /// </summary>
        /// <param name="id">Identifier.</param>
        public Id(string id)
        {
            this.Parameters = new string[ 0 ];
            this.ParseName( id );
        }
        
        /// <summary>
        /// Serves as a hash function for a <see cref="T:QDocNetLib.Id"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
        public override int GetHashCode()
        {
            return this.FullId.GetHashCode();
        }
        
        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:QDocNetLib.Id"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:QDocNetLib.Id"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current <see cref="T:QDocNetLib.Id"/>;
        /// otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            bool toret = false;
            
            if ( obj is Id id ) {
                toret = this.Equals( id );
            }
            
            return toret;
        }
        
        /// <summary>
        /// Determines whether the specified <see cref="QDocNetLib.Id"/> is equal to the current <see cref="T:QDocNetLib.Id"/>.
        /// </summary>
        /// <param name="id">The <see cref="QDocNetLib.Id"/> to compare with the current <see cref="T:QDocNetLib.Id"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="QDocNetLib.Id"/> is equal to the current
        /// <see cref="T:QDocNetLib.Id"/>; otherwise, <c>false</c>.</returns>
        public bool Equals(Id id)
        {
            bool toret = false;
            
            if ( !ReferenceEquals( id, null ) ) {
                toret = ( this.FullId == id.FullId );
            }
            
            return toret;
        }
        
        /// <summary>
        /// Determines whether a specified instance of <see cref="QDocNetLib.Id"/> is equal to another specified <see cref="QDocNetLib.Id"/>.
        /// </summary>
        /// <param name="one">The first <see cref="QDocNetLib.Id"/> to compare.</param>
        /// <param name="other">The second <see cref="QDocNetLib.Id"/> to compare.</param>
        /// <returns><c>true</c> if <c>one</c> and <c>other</c> are equal; otherwise, <c>false</c>.</returns>
        public static bool operator==(Id one, Id other)
        {
            bool toret = false;
            
            if ( !ReferenceEquals( one, null ) ) {
                toret = one.Equals( other );
            }
            
            return toret;
        }
        
        /// <summary>
        /// Determines whether a specified instance of <see cref="QDocNetLib.Id"/> is not equal to another specified <see cref="QDocNetLib.Id"/>.
        /// </summary>
        /// <param name="one">The first <see cref="QDocNetLib.Id"/> to compare.</param>
        /// <param name="other">The second <see cref="QDocNetLib.Id"/> to compare.</param>
        /// <returns><c>true</c> if <c>one</c> and <c>other</c> are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator!=(Id one, Id other)
        {
            return !( one == other );
        }
        
        void ParseDotSeparatedPath(string path)
        {
            string[] pathParts = path.Split( new char[]{ '.' } );
            
            if ( pathParts.Length > 1 ) {
                this.Name = pathParts[ pathParts.Length - 1 ];
                this.Path = new string[ pathParts.Length - 1 ];
                Array.Copy( pathParts, this.Path, pathParts.Length - 1 );
            } else {
                throw new ArgumentException( "not enough parts: '" + path + '\'' );
            }
            
            return;
        }
        
        void ParseNamePart(string namePart)
        {
            string parameters = "";
            int posLPar = namePart.LastIndexOf( '(' );
            
            if ( posLPar >= 0 ) {
                // Take parameters removing the brackets
                parameters = namePart.Substring( posLPar + 1 );
                parameters = parameters.Remove( parameters.Length - 1 );
                this.Parameters = parameters.Split( ',' );
                
                this.Type = AType.Method;
                namePart = namePart.Substring( 0, posLPar );
            }
            
            this.ParseDotSeparatedPath( namePart );
            
            return;
        }
        
        void ParseName(string name)
        {
            string[] mainParts = name.Split( new char[]{ ':' } );
            
            // Extract the type
            if ( mainParts.Length < 2 ) {
                mainParts = new string[]{ "T", mainParts[ 0 ] };
            }
            
            if ( string.IsNullOrWhiteSpace( mainParts[ 0 ] ) ) {
                mainParts[ 0 ] = char.ToString( ReprType[ 0 ] );
            }
            
            this.CharType = mainParts[ 0 ].Trim()[ 0 ];
            
            // Extract the path
            mainParts[ 1 ] = mainParts[ 1 ].Trim();
            
            if ( mainParts[ 1 ].Length > 0 ) {
                this.ParseNamePart( mainParts[ 1 ] );
            } else {
                throw new ArgumentException( "malformed id: '" + name + '\'' );
            }
            
            return;
        }
        
        /// <summary>
        /// Sets the type if unknown.
        /// The type is set given an <see cref="Id"/> from an <see cref="Entity"/>,
        /// which has been or will be soon added as a subentity.
        /// </summary>
        /// <param name="subEntityId">An <see cref="Id"/> from which to decide the type.</param>
        public void SetTypeIfUnknown(Id subEntityId)
        {
            // Try to deduce this type
            if ( subEntityId.Type == Id.AType.Method ) {
                this.Type = Id.AType.Class;
            }
            else
            if ( subEntityId.Type == Id.AType.Type
              && subEntityId.Type == Id.AType.EnumValue )
            {
                this.Type = Id.AType.Enum;
            }
            
            return;
        }
        
        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:QDocNetLib.Id"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:QDocNetLib.Id"/>.</returns>
        public override string ToString()
        {
            string parameters = "";
            
            if ( this.Type == AType.Method ) {
                parameters = "(" + string.Join( ", ", this.Parameters ) + ")";
            }
            
            return string.Format("{0}:{1}.{2}{3}",
                                    this.CharType,
                                    string.Join( "::", this.Path ),
                                    this.Name,
                                    parameters );
        }
        
        /// <summary>
        /// Gets the path, as in [ "Cars", "Car", "ctor" ]
        /// </summary>
        /// <value>The path, as a string array.</value>
        public string[] Path {
            get; private set;
        }
        
        /// <summary>
        /// Gets the fully qualified identifier.
        /// Parameters are included if this is a method.
        /// </summary>
        /// <value>The full identifier, as a string.</value>
        public string FullId {
            get {
                string toret;
                string path = string.Join( ".", this.Path );
                string parameters = string.Join( ",", this.Parameters );
                
                // Add the complete path, or at least just the name
                if ( string.IsNullOrWhiteSpace( path ) ) {
                    toret = this.Name;
                } else {
                    toret = path + "." + this.Name;
                }
                
                // Add the parameters
                if ( !string.IsNullOrWhiteSpace( parameters ) ) {
                    toret += "(" + parameters + ")";
                }
                
                return toret;
             }
        }
        
        /// <summary>
        /// Gets the parameters
        /// (provided it is a method, empty array otherwise).
        /// </summary>
        /// <value>The parameters.</value>
        public string[] Parameters {
            get; private set;
        }
        
        /// <summary>
        /// Gets or sets the char type for this id.
        /// </summary>
        /// <value>The type of the id, as a char.</value>
        public char CharType {
            get {
                return ReprType[ (int) this.Type ];
            }
            set {
                int pos = Math.Max( 0, Array.IndexOf( ReprType, value ) );
                
                this.Type = (AType) pos;
            }
        }
        
        /// <summary>
        /// Sets the structural type.
        /// </summary>
        /// <value>The type of the entity, as a <see cref="System.Type"/> instance.</value>
        public AType Type {
            get; set;
        }
        
        /// <summary>
        /// Gets or sets the name (last entry in the original path).
        /// </summary>
        /// <value>The name.</value>
        public string Name {
            get; set;
        }
        
        /// <summary>
        /// Gets the parent for the object of this id.
        /// (The last entry in the Path).
        /// </summary>
        /// <value>The parent, as a string.</value>
        /// <seealso cref="Path"/>
        public string ParentName {
            get {
                string toret = "";
                
                if ( this.Path.Length > 0 ) {
                    toret = this.Path[ this.Path.Length - 1 ];
                }

                return toret;
            }
        }
        
        /// <summary>
        /// Gets the parent for this id.
        /// </summary>
        /// <value>The parent, as an <see cref="Id"/>.</value>
        public Id Parent {
            get {
                return new Id( string.Join( ".", this.Path ) );
            }
        }
    }
}
