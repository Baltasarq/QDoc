using System;

namespace QDocLib.Persistence {
    public class HtmlExporter {
        public HtmlExporter(Entity entity)
        {
            this.Entity = entity;
        }

        public void SaveTo(string path)
        {
        }

        public Entity Entity {
            get; set;
        }
    }
}
