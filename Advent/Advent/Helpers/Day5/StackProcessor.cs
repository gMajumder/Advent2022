using Advent.Models.Day5;

namespace Advent.Helpers.Day5
{
    public class StackProcessor
    {
        public async Task<StackSet> BuildStacks()
        {
            var stacks = await File.ReadAllLinesAsync(Path.Combine
                            (Environment
                            .GetFolderPath(Environment.SpecialFolder.MyDocuments), "stackset.txt"));

            var listOfStacks = new List<Stack<char>>();
            foreach (var line in stacks)
            {
                var stack = new Stack<char>();

                for (int i = line.Length - 1; i >= 0; i--)
                {
                    stack.Push(line[i]);
                }

                listOfStacks.Add(stack);
            }

            return new StackSet(listOfStacks);
        }
    }
}
