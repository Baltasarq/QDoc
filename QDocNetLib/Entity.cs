using System.Text;
using System.Collections.Generic;
using QDocLib;

namespace QDocNetLib {
    public class Entity {
        public Entity(string name)
        {
            this.Name = name;
			this.crossReferences = new List<Entity>();
        }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
        public string Name {
            get; set;
        }

		/// <summary>
		/// Gets or sets the documentation.
		/// </summary>
		/// <value>The documentation, as a string.</value>
		public Documentation SummaryDocumentation {
			get; set;
		}

		public Entity[] CrossReferences {
			get {
				return this.crossReferences.ToArray();
			}
			set {
				this.crossReferences.Clear();
				this.crossReferences.AddRange( value );
			}
		}

		/// <summary>
		/// Builds a descriptive string from an entity collection.
		/// </summary>
		/// <returns>The entity collection to describe.</returns>
		/// <param name="entities">The entities themselves, as a primitive array.</param>
		/// <param name="separator">The separator after each entity, as a string.</param>
        public static string StringFromEntityCollection(Entity[] entities, string separator)
        {
            var toret = new StringBuilder();

            foreach(Entity entity in entities) {
                toret.Append( entity.ToString() );
                toret.Append( separator );
            }

            return toret.ToString();
        }

		private List<Entity> crossReferences;
    }
}

