using Advent.Models.Day7;

namespace Advent.Helpers.Day7
{
    public class FileSystemCommandProcessor
    {
        public double CalculateDirectorySize(FileSystemNode node)
        {
            if (node.Type == Models.Day7.Type.FILE) return node.Size;

            double result = 0;
            foreach (var item in node.ChildNodes)
            {
                result += CalculateDirectorySize(item);
            }

            node.Size = result;
            return result;
        }

        public void ProcessCommandSet(int lineNum, FileSystemNode rootNode, string[] instructions)
        {
            if (rootNode is null || lineNum >= instructions.Length) return;

            var line = instructions[lineNum];

            if (line.StartsWith('$'))
            {
                var sub = line.Split(' ');

                switch (sub.Length)
                {
                    //$ cd <>
                    case 3:
                        var dirName = sub[2];

                        if (!dirName.Contains('.'))
                        {
                            var cdNode = rootNode.ChildNodes.First(n => n.Name == dirName);
                            ProcessCommandSet(lineNum + 1, cdNode, instructions);
                            return;
                        }

                        //$ cd ..
                        var parentNode = rootNode.ParentNode;
                        ProcessCommandSet(lineNum + 1, parentNode, instructions);
                        return;

                    //$ ls
                    case 2:
                        ProcessCommandSet(lineNum + 1, rootNode, instructions);
                        return;

                    default: throw new InvalidOperationException("unable to parse INSTRUCTION");
                }
            }

            int newlineNum = ProcessLsStatements(lineNum, rootNode, instructions);

            ProcessCommandSet(newlineNum, rootNode, instructions);
        }

        private int ProcessLsStatements(int lineNum, FileSystemNode directoryNode, string[] instructions)
        {
            if (lineNum >= instructions.Length) return lineNum;

            var line = instructions[lineNum];

            if (line.StartsWith('$'))
            {
                return lineNum;
            }

            //add file nodes
            if (char.IsDigit(line[0]))
            {
                var cmdParts = line.Split(' ');
                var fileSize = Convert.ToDouble(cmdParts[0]);
                var fileName = cmdParts[1];
                var fileNode = new FileSystemNode(fileName, "file", fileSize, directoryNode);
                directoryNode.ChildNodes.Add(fileNode);

                return ProcessLsStatements(lineNum + 1, directoryNode, instructions);

            }

            //add folder nodes
            var dirCmdParts = line.Split(' ');
            var folderName = dirCmdParts[1];
            var folderNode = new FileSystemNode(folderName, "folder", null, directoryNode);
            directoryNode.ChildNodes.Add(folderNode);

            return ProcessLsStatements(lineNum + 1, directoryNode, instructions);
        }
    }
}
