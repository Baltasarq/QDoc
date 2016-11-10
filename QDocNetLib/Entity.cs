using System;
using System.Text;

namespace QDocNetLib {
    public class Entity {
        public Entity(string name)
        {
            this.Name = name;
        }

        public string Name {
            get; set;
        }

        public static string StringFromEntityCollection(Entity[] entities, string separator)
        {
            StringBuilder toret = new StringBuilder();

            foreach(Entity entity in entities) {
                toret.Append( entity.ToString() );
                toret.Append( separator );
            }

            return toret.ToString();
        }
    }
}

