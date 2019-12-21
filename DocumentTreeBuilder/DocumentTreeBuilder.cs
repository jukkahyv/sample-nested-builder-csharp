using DocumentTreeBuilderSample.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentTreeBuilderSample {

    public class DocumentTreeBuilder {

        private List<Folder> Folders { get; } = new List<Folder>();
        private int _nextId = 1;

        public DocumentTreeBuilder AddFolder(string name, Action<FolderBuilder> action = null) => AddFolder(name, action, out _);

        public DocumentTreeBuilder AddFolder(string name, Action<FolderBuilder> action, out Folder folder) {
            folder = new Folder(GetNextId(), name);
            Folders.Add(folder);
            var childBuilder = new FolderBuilder(this, folder);
            action?.Invoke(childBuilder);
            return this;
        }

        public int GetNextId() => _nextId++;

        public List<Folder> Build() => Folders;

    }

    public class FolderBuilder {

        public FolderBuilder(DocumentTreeBuilder documentTreeBuilder, Folder folder) {
            DocumentTreeBuilder = documentTreeBuilder;
            Folder = folder;
        }

        private DocumentTreeBuilder DocumentTreeBuilder { get; }
        private Folder Folder { get; }

        public FolderBuilder AddDocument(DocumentType documentType, Action<DocumentBuilder> action = null) => AddDocument(documentType, action, out _);

        public FolderBuilder AddDocument(DocumentType documentType, out Document document) => AddDocument(documentType, null, out document);

        public FolderBuilder AddDocument(DocumentType documentType, Action<DocumentBuilder> action, out Document document) {
            document = new Document(DocumentTreeBuilder.GetNextId(), documentType);
            Folder.Documents.Add(document);
            var childBuilder = new DocumentBuilder(this, document);
            action?.Invoke(childBuilder);
            return this;
        }

        public FolderBuilder AddSubFolder(string name, Action<FolderBuilder> action = null) => AddSubFolder(name, action, out _);

        public FolderBuilder AddSubFolder(string name, Action<FolderBuilder> action, out Folder folder) {
            folder = new Folder(DocumentTreeBuilder.GetNextId(), name);
            Folder.SubFolders.Add(folder);
            var childBuilder = new FolderBuilder(DocumentTreeBuilder, folder);
            action?.Invoke(childBuilder);
            return this;
        }

        public FolderBuilder WithIcon(string icon) {
            Folder.Icon = icon;
            return this;
        }

    }

    public class DocumentBuilder {

        public DocumentBuilder(FolderBuilder folderBuilder, Document document) { 
            FolderBuilder = folderBuilder;
            Document = document;
        }

        private Document Document { get; }
        private FolderBuilder FolderBuilder { get; }

        public DocumentBuilder AddLink(Document target) {
            Document.Links.Add(target);
            return this;
        }

    }

}
