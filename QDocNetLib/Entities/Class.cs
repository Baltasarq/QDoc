using System;
using System.Collections.Generic;

namespace QDocNetLib.Entities {
    public class Class: Type {
        public Class(string name)
            :base(name)
        {
        }

        public void Add(Member m)
        {
            this.members.Add( m );
        }

        public Member[] Members {
            get {
                var toret = new Member[ this.members.Count ];
                this.members.CopyTo( toret, 0 );
                return toret;
            }
        }

        private List<Member> members;
    }
}
