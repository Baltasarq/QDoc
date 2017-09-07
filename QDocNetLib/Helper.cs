using System;
using System.IO;

namespace QDocNetLib {
    /// <summary>
    /// Facade class with helper functions
    /// </summary>
    public static class Helper {
        /// <summary>
        /// Loads from an XML doc file.
        /// </summary>
        /// <returns>The root Entity (a namespace).</returns>
        /// <param name="path">The PATH in which the XML lies.</param>
        public static Entity LoadFromFile(string path)
        {
            var importer = new Persistence.XmlImporter( path );
			Console.WriteLine( "Reading " + path );
            return importer.Import();
        }
    }
}
