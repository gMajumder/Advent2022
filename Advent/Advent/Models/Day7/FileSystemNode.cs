namespace Advent.Models.Day7
{
    public class FileSystemNode
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public double Size { get; set; }

        public FileSystemNode ParentNode { get; set; }
        public List<FileSystemNode>? ChildNodes { get; set; }

        public FileSystemNode(string name, string type, double? size, FileSystemNode parent)
        {
            ParentNode = parent;
            Name = name;
            Type = Enum.Parse<Type>(type.ToUpper());

            if (Type == Type.FOLDER)
            {
                ChildNodes = new List<FileSystemNode>();
            }
            else
            {
                Size = size.GetValueOrDefault();
                ChildNodes = null;
            }
        }
    }

    public enum Type
    {
        FOLDER,
        FILE
    }
}
