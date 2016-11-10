using System;

namespace QDocNetLib.PrimitiveTypes {
    public class String: Entities.Type {
        /// <summary>
        /// The identifier.
        /// </summary>
        public const string Id = "string";

        /// <summary>
        /// Initializes a new instance of this type.
        /// </summary>
        private String()
            :base(Id)
        {
        }

        public override string ToString()
        {
            return Id;
        }

        private static String instance;

        /// <summary>
        /// Get the unique instance.
        /// </summary>
        public static String Get() {
            if (instance == null) {
                instance = new String();
            }

            return instance;
        }
    }
}

