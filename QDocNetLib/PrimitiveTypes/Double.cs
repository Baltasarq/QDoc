using System;

namespace QDocNetLib.PrimitiveTypes {
    public class Double: QDocNetLib.Entities.Type {
        /// <summary>
        /// The identifier.
        /// </summary>
        public const string Id = "double";

        /// <summary>
        /// Initializes a new instance of this type.
        /// </summary>
        private Double()
            :base(Id)
        {
        }

        public override string ToString()
        {
            return Id;
        }

        private static Double instance;

        /// <summary>
        /// Get the unique instance.
        /// </summary>
        public static Double Get() {
            if (instance == null) {
                instance = new Double();
            }

            return instance;
        }
    }
}

