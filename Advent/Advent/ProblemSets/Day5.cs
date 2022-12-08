using Advent.Helpers.Day5;

namespace Advent.ProblemSets
{
    public class Day5
    {
        readonly StackProcessor _stackProcessor;
        public Day5()
        {
            _stackProcessor = new StackProcessor();
        }

        public async Task<string> SolveProblem_Part1()
        {
            var instructions = await ReadInputFileForInstructions();
            var stackSet = await _stackProcessor.BuildStacks();

            foreach (var ins in instructions)
            {
                var (items, source, target) = ExtractInfoFromInstruction(ins);

                for (int i = 1; i <= items; i++)
                {
                    stackSet[target] = stackSet[source];
                }
            }

            string finalValue = "";
            foreach (var s in stackSet.StackSets)
            {
                finalValue += s.Pop();
            }

            return finalValue;
        }

        public async Task<string> SolveProblem_Part2()
        {
            var instructions = await ReadInputFileForInstructions();
            var stackSet = await _stackProcessor.BuildStacks();

            foreach (var ins in instructions)
            {
                var (items, source, target) = ExtractInfoFromInstruction(ins);

                var tempstack = new Stack<char>();
                for (int i = 1; i <= items; i++)
                {
                    tempstack.Push(stackSet[source]);
                }

                while (tempstack.Count != 0)
                {
                    stackSet[target] = tempstack.Pop();
                }
            }

            string finalValue = "";
            foreach (var s in stackSet.StackSets)
            {
                finalValue += s.Pop();
            }

            return finalValue;
        }

        private async Task<string[]> ReadInputFileForInstructions() =>
           await File.ReadAllLinesAsync(
                        Path.Combine
                        (Environment
                        .GetFolderPath(Environment.SpecialFolder.MyDocuments), "day5instruc.txt"));

        private (int items, int source, int target) ExtractInfoFromInstruction(string ins)
        {
            var subIns = ins.Split("from", StringSplitOptions.RemoveEmptyEntries);

            if (!subIns.Any() || subIns.Length != 2)
            {
                throw new InvalidOperationException("Instruction not in the right format");
            }

            return (Convert.ToInt32(subIns[0].Split(' ')[1]), 
                Convert.ToInt32(subIns[1].Split("to", StringSplitOptions.RemoveEmptyEntries)[0]), 
                Convert.ToInt32(subIns[1].Split("to", StringSplitOptions.RemoveEmptyEntries)[1]));
        }
    }
}
