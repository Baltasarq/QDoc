using System;

namespace QDocLib {
    /// <summary>
    /// Facade class with helper functions
    /// </summary>
    public static class QDoc {
        public Entity LoadFromFile(string path)
        {
            var importer = new Persistence.XmlImporter( path );
            return importer.Import();
        }
    }
}
