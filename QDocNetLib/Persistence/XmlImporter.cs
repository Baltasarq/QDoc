using System;

namespace QDocNetLib.Persistence {
    /// <summary>
    /// Xml importer. Reads the XML Docs and imports its info.
    /// </summary>
    public class XmlImporter {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:QDocNetLib.Persistence.XmlImporter"/> class.
        /// </summary>
        /// <param name="path">The path of the XML file.</param>
        public XmlImporter(string path)
        {
            this.Path = path;
        }

        /// <summary>
        /// Does the importing work.
        /// </summary>
        /// <returns>The import.</returns>
        public Entity Import()
        {
            Entity toret = null;

            throw new NotImplementedException();
            return toret;
        }
        
        /// <summary>
        /// The path of the XML file.
        /// </summary>
        /// <value>The path.</value>
        public string Path {
            get; private set;
        }
    }
}
