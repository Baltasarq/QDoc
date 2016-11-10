using System;

namespace QDocLib.Types {
    public class Member: Entity {
        public Member(string name)
            :base(name)
        {
        }

        public Type Type {
            get; set;
        }
    }
}
