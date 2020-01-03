# Sample for nested builder pattern in C#

Making use of Action<T> callbacks and "out" parameters, with optional discards.

```csharp
var docTree = new DocumentTreeBuilder()
    .AddFolder("My files", folder => folder
        .SetIcon("myicon.png")
        .AddDocument(Model.DocumentType.Text)
        .AddDocument(Model.DocumentType.Text)
        .AddDocument(Model.DocumentType.Text, out var textDocument)
        .AddSubFolder("Images", imgFolder => imgFolder
            .AddDocument(Model.DocumentType.Image, doc => doc.AddLink(textDocument))
        )
    , out var myFiles)
    .AddFolder("Shared files")
    .Build();

Console.WriteLine($"MyFiles ID: {myFiles.Id}");
```

## Some explanation

One of the rules I follow is that the builder methods (such as ```AddDocument```) should always return the builder itself, and
the child builder, if necessary, should be accessed in a callback. This makes it clear when we are configuring a child object, and when parent object, and avoids the need to use special GetParent or Build calls.
The children can be chained easily.

Additionally, all created objects can be exported as "out" parameters, if they need to be accessed later.
