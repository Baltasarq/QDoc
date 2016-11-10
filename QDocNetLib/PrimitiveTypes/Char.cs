using System;

namespace QDocNetLib.PrimitiveTypes {
    public class Char: Entities.Type {
        /// <summary>
        /// The identifier.
        /// </summary>
        public const string Id = "char";

        /// <summary>
        /// Initializes a new instance of this type.
        /// </summary>
        private Char()
            :base(Id)
        {
        }

        public override string ToString()
        {
            return Id;
        }

        private static Char instance;

        /// <summary>
        /// Get the unique instance.
        /// </summary>
        public static Char Get() {
            if (instance == null) {
                instance = new Char();
            }

            return instance;
        }
    }
}

