// QDocNet - (c) 2017 Baltasar MIT License <baltasarq@gmail.com>

namespace QDocNetLib {
    using System;
    using System.Collections.Generic;
    
	/// <summary>
	/// An entity to be documented.
	/// </summary>
    public class Entity: IEquatable<Entity> {
		/// <summary>
		/// Initializes a new <see cref="Entity"/>.
		/// </summary>
        /// <param name="unit">The <see cref="Unit"/> this entity pertains to.</param>
        public Entity(Unit unit)
        {
            this.Unit = unit;
            this.entities = new List<Entity>();
        }
        
        /// <summary>
        /// Serves as a hash function for a <see cref="T:QDocNetLib.Entity"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
        public override int GetHashCode()
        {
            return ( 13 * this.Id.GetHashCode() )+ ( 17 * this.Unit.GetHashCode() );
        }
        
        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:QDocNetLib.Entity"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:QDocNetLib.Entity"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current <see cref="T:QDocNetLib.Entity"/>;
        /// otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            bool toret = ReferenceEquals( this, obj );
            
            if ( !toret
              && obj is Entity entity )
            {
                toret = this.Equals( entity );
            }
            
            return toret;
        }
        
        /// <summary>
        /// Determines whether the specified <see cref="QDocNetLib.Entity"/> is equal to the current <see cref="T:QDocNetLib.Entity"/>.
        /// </summary>
        /// <param name="entity">The <see cref="QDocNetLib.Entity"/> to compare with the current <see cref="T:QDocNetLib.Entity"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="QDocNetLib.Entity"/> is equal to the current
        /// <see cref="T:QDocNetLib.Entity"/>; otherwise, <c>false</c>.</returns>
        public bool Equals(Entity entity)
        {
            bool toret = ReferenceEquals( this, entity );
            
            if ( !toret
              && !ReferenceEquals( entity, null ) )
            {
                toret = this.Unit == entity.Unit && this.Id == entity.Id;
            }
            
            return toret;
        }
        
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="ent">The <see cref="Entity"/>.</param>
        public void Add(Entity ent)
        {
            this.entities.Add( ent );
            this.Id.SetTypeIfUnknown( ent.Id );
        }
        
        /// <summary>Clear all sub entities.</summary>
        public void Clear()
        {
            this.entities.Clear();
        }
    
        /// <summary>The number of subentities.</summary>   
        public int Count {
            get {
                return this.entities.Count;
            }
        }
        
        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:QDocNetLib.Entity"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:QDocNetLib.Entity"/>.</returns>
        public override string ToString()
        {
            string toret = string.Format( "[Entity: Type={0}: Id=({1})"
                                    + "\n\tDocs=({2})]",
                                    this.Type,
                                    this.Id,
                                    this.Docs );
                                    
            if ( this.entities.Count > 0 ) {
                var names = new List<string>();
                
                this.entities.ForEach( (e) => names.Add( e.Id.Name ) );
                toret += "\n\t" + string.Join( ", ", names );
            }
            
            return toret;
        }
        
        /// <summary>
        /// Gets the unit for this entity.
        /// </summary>
        /// <value>The <see cref="Unit"/>.</value>
        public Unit Unit {
            get; private set;
        }
        
        /// <summary>
        /// Sets the structural type.
        /// </summary>
        /// <value>The type of the entity, as a <see cref="System.Type"/> instance.</value>
        public Id.AType Type {
            get {
                return this.Id.Type;
            }
        }

        /// <summary>
        /// Gets or sets the documentation.
        /// </summary>
        /// <value>The documentation, as a string.</value>
        public Documentation Docs {
            get; set;
        }
        
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier, as an <see cref="Id"/>.</value>
        public Id Id {
            get {
                Id toret = null;
                
                if ( this.Docs != null ) {
                    toret = this.Docs.Id;
                } else {
                    throw new ArgumentException( "Docs not assigned yet." );
                }
                
                return toret;
            }
        }
        
        /// <summary>All subentities.</summary>   
        public Entity[] Entries => this.entities.ToArray();

        readonly List<Entity> entities;
    }
}
