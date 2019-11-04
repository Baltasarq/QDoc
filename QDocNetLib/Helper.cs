// QDocNet - (c) 2017 Baltasar MIT License <baltasarq@gmail.com>

namespace QDocNetLib {
    using System.IO;

    /// <summary>
    /// Facade class with helper functions
    /// </summary>
    public static class Helper {
        /// <summary>
        /// Loads documentation from an XML doc file and its assembly.
        /// </summary>
        /// <returns>The root <see cref="Entity"/>.</returns>
        /// <param name="xmlPath">The path for the assembly.</param>
        public static Unit LoadDocs(string xmlPath)
        {
            if ( !File.Exists( xmlPath ) ) {
                throw new IOException( "'" + xmlPath + "' not found." );
            }
            
            
            if ( !File.Exists( xmlPath ) ) {
                throw new IOException( "'" + xmlPath + "' not found." );
            }
	        
            var importer = new Persistence.XmlImporter( xmlPath );            
            return importer.Import();
        }        
    }
}
