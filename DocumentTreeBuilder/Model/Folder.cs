using System.Collections.Generic;

namespace DocumentTreeBuilderSample.Model {

    public class Folder {

        public Folder(int id, string name) {
            Id = id;
            Name = name;
        }

        public List<Document> Documents { get; } = new List<Document>();
        public List<Folder> SubFolders { get; } = new List<Folder>();
        public string Icon { get; set; }
        public int Id { get; }
        public string Name { get; }

    }

    public class Document {

        public Document(int id, DocumentType documentType) {
            Id = id;
            DocumentType = documentType;
        }

        public DocumentType DocumentType { get; }
        public int Id { get; }
        public List<Document> Links { get; } = new List<Document>();

    }

    public enum DocumentType {
        Text,
        Image,
        Audio
    }

}
