using Advent.Helpers.Day7;
using Advent.Models.Day7;

namespace Advent.ProblemSets
{
    public class Day7
    {
        FileSystemCommandProcessor _fileSystemCommandProcessor;
        const double TOTAL_MEMORY = 70000000;
        const double MINIMUM_MEMORY_FOR_UPDATE = 30000000;

        public Day7()
        {
            _fileSystemCommandProcessor = new FileSystemCommandProcessor();
        }

        public (double problemOneResult, double problemTwoResult) SolveProblem()
        {
            string[] instructions = ReadInputFile();

            var rootNode = new FileSystemNode("/", "folder", null, null);

            _fileSystemCommandProcessor.ProcessCommandSet(1, rootNode, instructions);

            _fileSystemCommandProcessor.CalculateDirectorySize(rootNode);

            double problemOneResult = 0;
            Problem_Part1_SumAtMost1000(rootNode, ref problemOneResult);

            var usedSpace = rootNode.Size;
            var unUsedSpace = TOTAL_MEMORY - usedSpace;
            var minSpaceReqd = MINIMUM_MEMORY_FOR_UPDATE - unUsedSpace;

            double leastMemoryToBeDeleted = Int32.MaxValue;
            Problem_Part2_MemoryCleanUp(rootNode, ref leastMemoryToBeDeleted, minSpaceReqd);

            return (problemOneResult: problemOneResult, problemTwoResult: leastMemoryToBeDeleted);
        }

        string[] ReadInputFile() =>
            File.ReadAllLines(
            Path.Combine
            (Environment
            .GetFolderPath(Environment.SpecialFolder.MyDocuments), "day7instruc.txt"));

        void Problem_Part1_SumAtMost1000(FileSystemNode node, ref double result)
        {
            if (node.Type == Models.Day7.Type.FILE) return;

            if (node.Size <= 100000)
            {
                result += node.Size;
            }

            foreach (var n in node.ChildNodes)
            {
                Problem_Part1_SumAtMost1000(n, ref result);
            }
        }

        void Problem_Part2_MemoryCleanUp(FileSystemNode node, ref double leastMemDir, double minSpaceReqd)
        {
            if (node.Type == Models.Day7.Type.FILE) return;

            if (node.Size >= minSpaceReqd)
                leastMemDir = Math.Min(leastMemDir, node.Size);

            foreach (var n in node.ChildNodes)
            {
                Problem_Part2_MemoryCleanUp(n, ref leastMemDir, minSpaceReqd);
            }
        }
    }
}
