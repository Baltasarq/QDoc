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
				var toret = new Entity[ this.entities.Count ];
				this.entities.CopyTo( toret, 0 );
                return toret;
            }
        }

        private List<Entity> entities;
    }
}
