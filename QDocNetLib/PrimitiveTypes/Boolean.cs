using System;

namespace QDocNetLib.PrimitiveTypes {
    public class Bool: Entities.Type {
        /// <summary>
        /// The identifier.
        /// </summary>
        public const string Id = "bool";

        /// <summary>
        /// Initializes a new instance of this type.
        /// </summary>
        private Bool()
            :base(Id)
        {
        }

        public override string ToString()
        {
            return Id;
        }

        private static Bool instance;

        /// <summary>
        /// Get the unique instance.
        /// </summary>
        public static Bool Get() {
            if (instance == null) {
                instance = new Bool();
            }

            return instance;
        }
    }
}
