# Sample for nested builder pattern in C#

```csharp
var docTree = new DocumentTreeBuilder()
    .AddFolder("My files", folder => folder
        .AddDocument(Model.DocumentType.Text, out var textDocument)
        .AddSubFolder("Images", imgFolder => imgFolder
            .AddDocument(Model.DocumentType.Image, doc => doc.AddLink(textDocument))
        )
    , out var myFiles)
    .AddFolder("Shared files")
    .Build();

Console.WriteLine($"MyFiles ID: {myFiles.Id}");
```
