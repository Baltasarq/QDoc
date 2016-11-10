using System;

namespace QDocNetLib {
    /// <summary>
    /// Facade class with helper functions
    /// </summary>
    public static class Helper {
        public static Entity LoadFromFile(string path)
        {
            var importer = new Persistence.XmlImporter( path );
            return importer.Import();
        }
    }
}
