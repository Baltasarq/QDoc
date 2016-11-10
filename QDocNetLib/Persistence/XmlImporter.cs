using System;

namespace QDocNetLib.Persistence {
    public class XmlImporter
    {
        public XmlImporter(string path)
        {
            this.Path = path;
        }

        public Entity Import()
        {
            Entity toret = null;

            throw new NotImplementedException();
            return toret;
        }

        public string Path {
            get; set;
        }
    }
}
