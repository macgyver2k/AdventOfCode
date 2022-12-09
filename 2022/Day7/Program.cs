using System.Text.Json;
using System.Text.Json.Serialization;

var inputLines = await File.ReadAllLinesAsync("input.txt");

var root = ReadTree(inputLines);
var bigDirectories = new List<(DirectoryNode Node, Int64 Size)>();

var result = GetFileSize(root, 100000, bigDirectories);
var sum = bigDirectories.Where(_ => _.Size <= 100000).Sum(_ => _.Size);

Console.WriteLine( $"The sum is {sum}" );

static Int64 GetFileSize( DirectoryNode node, Int64 minimumSize, List<(DirectoryNode Node, Int64 Size)> bigDirectories )
{
    var fileSizeSum = node.FileNodes.Sum(_ => _.Size);
    var subDirSizeSum = node.DirectoryNodes.Select(_ => GetFileSize(_, minimumSize,bigDirectories)).Sum();
    var sum = fileSizeSum + subDirSizeSum;

    bigDirectories.Add((node, sum));
    return sum;
}

static DirectoryNode? ReadTree(string[] inputLines)
{
    DirectoryNode? root = null;
    DirectoryNode? current = null;

    var isListCommand = false;

    foreach (var line in inputLines)
    {
        if (line[0] == '$')
        {
            isListCommand = false;
            var command = line[2..];

            if (command.StartsWith("cd"))
            {
                var path = command[3..];

                if (path == "/")
                {
                    root = new DirectoryNode("/", null);
                    current = root;
                    continue;
                }

                if (path == "..")
                {
                    current = current?.Parent;
                    continue;
                }

                current = current?.GetDirectory(path);
            }

            if (command.StartsWith("ls"))
            {
                isListCommand = true;
                continue;
            }
        }

        if (isListCommand)
        {
            if (line.StartsWith("dir"))
            {
                var dirName = line[4..];
                current?.AddDirectoryNode(dirName);
                continue;
            }

            var parts = line.Split(' ');
            var size = Convert.ToInt64(parts[0]);
            var name = parts[1];

            current?.AddFileNode(name, size);
        }
    }

    return root;
}

public class DirectoryNode
{
    private readonly List<DirectoryNode> directoryNodes = new List<DirectoryNode>();
    private readonly List<FileNode> fileNodes = new List<FileNode>();

    public String Name { get; }
    [JsonIgnore]
    public DirectoryNode? Parent { get; }
    public List<DirectoryNode> DirectoryNodes => directoryNodes;
    public List<FileNode> FileNodes => fileNodes;

    public DirectoryNode(String name, DirectoryNode? parent = null)
    {
        Name = name;
        Parent = parent;
    }

    public void AddFileNode(String name, Int64 size)
    {
        this.FileNodes.Add(new FileNode(name, size, this));
    }

    public void AddDirectoryNode(String name)
    {
        this.directoryNodes.Add(new DirectoryNode(name, this));
    }

    public DirectoryNode? GetDirectory(string path)
    {
        return this.directoryNodes.First(_ => _.Name == path);
    }
}

public record FileNode(String Name, Int64 Size, [property: JsonIgnore] DirectoryNode Parent);

