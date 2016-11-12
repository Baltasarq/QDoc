namespace QDocNetLib.Entities {
	using System.Collections.Generic;

    public class Namespace: Entity {
        public Namespace(string name)
            :base(name)
        {
			this.entities = new List<Entity>();
        }

		public void Add(Entity m)
        {
			this.entities.Add( m );
        }

		public Entity[] Types {
            get {
				return this.entities.ToArray();
            }
			set {
				this.entities.Clear();
				this.entities.AddRange( value );
			}
        }

        private List<Entity> entities;
    }
}
