namespace Advent.Models.Day5
{
    public class StackSet
    {
        public List<Stack<char>> StackSets { get; set; }

        public StackSet(List<Stack<char>> stackSets)
        {
            StackSets = stackSets;
        }

        public char this[int stackNum]
        {
            get => StackSets[stackNum - 1].Pop();

            set => StackSets[stackNum - 1].Push(value);
        }
    }
}
