// QDocNet - (c) 2017 Baltasar MIT License <baltasarq@gmail.com>

namespace QDocNetLib {
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
        
    /// <summary>This unit represents a DLL (library) or EXE (program).</summary>
    public class Unit: System.IEquatable<Unit> {
        /// <summary>The default name of the assembly.</summary>
        public const string DefaultAssemblyName = "assembly";
    
        /// <summary>
        /// Initializes a new <see cref="T:QDocNetLib.Unit"/>.
        /// </summary>
        /// <param name="name">The name of the assembly.</param>
        public Unit(string name = DefaultAssemblyName)
        {
            if ( !string.IsNullOrWhiteSpace( name ) ) {
                this.Name = name;
            }
            
            this.allEntities = new Dictionary<Id, Entity>();
            this.rootEntities = new HashSet<Entity>();
        }
        
        /// <summary>
        /// Serves as a hash function for a <see cref="T:QDocNetLib.Unit"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
        
        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:QDocNetLib.Unit"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:QDocNetLib.Unit"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current <see cref="T:QDocNetLib.Unit"/>;
        /// otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            bool toret = false;
            
            if ( obj is Unit unit ) {
                toret = this.Equals( unit );
            }
            
            return toret;
        }
        
        /// <summary>
        /// Determines whether the specified <see cref="QDocNetLib.Unit"/> is equal to the current <see cref="T:QDocNetLib.Unit"/>.
        /// </summary>
        /// <param name="unit">The <see cref="QDocNetLib.Unit"/> to compare with the current <see cref="T:QDocNetLib.Unit"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="QDocNetLib.Unit"/> is equal to the current
        /// <see cref="T:QDocNetLib.Unit"/>; otherwise, <c>false</c>.</returns>
        public bool Equals(Unit unit)
        {
            bool toret = false;
            
            if ( unit != null ) {
                toret = ( this.Name == unit.Name );
            }
            
            return toret;
        }
        
        ///<summary>Converts the info to a string.</summary>
        ///<returns>A string with all the info.</returns>
        public override string ToString()
        {
            var toret = new StringBuilder();
            
            toret.Append( "Assembly: " );
            toret.AppendLine( this.Name );
            
            foreach(Entity entity in this.AllEntities)
            {
                toret.AppendLine( entity.ToString() );
            }
            
            return toret.ToString();
        }

        /// <summary>
        /// Loads the unit with the given rootEntities and allEntities.
        /// </summary>
        /// <returns>The <see cref="Unit"/> created.</returns>
        /// <param name="rootEntities">The root entities, probably a set of classes.</param>
        /// <param name="allEntities">All entities.</param>
        public void Load(  ISet<Entity> rootEntities,
                           IDictionary<Id, Entity> allEntities)
        {
            // Insert all entities
            foreach(Id id in allEntities.Keys) {
                this.allEntities.Add( id, allEntities[ id ] );
            }

            // Insert the root entities
            foreach(Entity entity in rootEntities) {
                this.rootEntities.Add( entity );
            }
        }
        
        /// <summary>
        /// Gets the root entities, probably a namespace or a collection
        /// of classes.
        /// </summary>
        /// <value>The root entities, as a
        /// <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection"/>.</value>
        public ReadOnlyCollection<Entity> AllEntities {
            get {
                return new ReadOnlyCollection<Entity>( this.allEntities.Values.ToArray() );
            }
        }
        
        /// <summary>
        /// Gets the classes immediately inside this assembly.
        /// </summary>
        /// <value>The classes, as an <see cref="Entity"/> array.</value>
        public Entity[] Classes {
            get {
                return this.rootEntities.ToArray();
            }
        }
        
        /// <summary>
        /// Gets or sets the name of the assembly.
        /// </summary>
        /// <value>The name, as a string.</value>
        public string Name {
            get; set;
        }
        
        readonly Dictionary<Id, Entity> allEntities;
        readonly ISet<Entity> rootEntities;
    }
}
