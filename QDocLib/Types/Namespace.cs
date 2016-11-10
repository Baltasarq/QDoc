using System;
using System.Collections.Generic;

namespace QDocLib.Types {
    public class Namespace: Entity {
        public Namespace(string name)
            :base(name)
        {
        }

        public void Add(Type m)
        {
            this.types.Add( m );
        }

        public Type[] Types {
            get {
                var toret = new Type[ this.types.Count ];
                this.types.CopyTo( toret, 0 );
                return toret;
            }
        }

        private List<Type> types;
    }
}
