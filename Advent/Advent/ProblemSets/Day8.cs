namespace Advent.ProblemSets
{
    public class Day8
    {
        public async Task<(int part1Result, int part2Result)> SolveProblem()
        {
            var inputLines = await ReadInputFile();

            var matrix = new List<List<int>>();

            foreach (var line in inputLines)
            {
                matrix.Add(
                    line.Select(c => Convert.ToInt32(c)).ToList()
                );
            }

            return (Problem_Part1_TotalVisibility(matrix), Problem_Part2_HighestScenicScore(matrix));
        }


        int Problem_Part2_HighestScenicScore(List<List<int>> matrix)
        {
            var firstRow = 0;
            var lastRow = matrix.Count - 1;
            var firstCol = 0;
            var lastCol = matrix[0].Count - 1;

            var maxScenicScore = 0;
            for (int i = firstRow + 1; i < lastRow; i++)
            {
                for (int j = firstCol + 1; j < lastCol; j++)
                {
                    var leftCount = LoopDirectionallyAndCheck(matrix, "left", i, j).visibleTreeCount;
                    var rightCount = LoopDirectionallyAndCheck(matrix, "right", i, j).visibleTreeCount;
                    var upCount = LoopDirectionallyAndCheck(matrix, "up", i, j).visibleTreeCount;
                    var downCount = LoopDirectionallyAndCheck(matrix, "down", i, j).visibleTreeCount;

                    maxScenicScore = Math.Max(maxScenicScore, (leftCount * rightCount * upCount * downCount));
                }
            }

            return maxScenicScore;
        }

        int Problem_Part1_TotalVisibility(List<List<int>> matrix)
        {
            var firstRow = 0;
            var lastRow = matrix.Count - 1;
            var firstCol = 0;
            var lastCol = matrix[0].Count - 1;

            var result = (2 * matrix.Count) + (2 * (matrix.Count - 2));
            for (int i = firstRow + 1; i < lastRow; i++)
            {
                for (int j = firstCol + 1; j < lastCol; j++)
                {
                    var isVisible = LoopDirectionallyAndCheck(matrix, "left", i, j).isVisibleTillEdge
                        || LoopDirectionallyAndCheck(matrix, "right", i, j).isVisibleTillEdge
                        || LoopDirectionallyAndCheck(matrix, "up", i, j).isVisibleTillEdge
                        || LoopDirectionallyAndCheck(matrix, "down", i, j).isVisibleTillEdge;

                    result += (isVisible ? 1 : 0);
                }
            }

            return result;
        }

        (bool isVisibleTillEdge, int visibleTreeCount) LoopDirectionallyAndCheck(
            List<List<int>> matrix, 
            string direction, int row, int col)
        {
            int value = matrix[row][col];
            int treeCount = 0;
            switch (direction)
            {
                case "left" :
                    for (int i = col - 1; i >= 0; i--)
                    {
                        treeCount++;

                        if (value <= matrix[row][i])
                        {
                            return (false, treeCount);
                        }
                    }
                    return (true, treeCount); 

                case "right" :
                    for (int i = col + 1; i < matrix[0].Count; i++)
                    {
                        treeCount++;

                        if (value <= matrix[row][i])
                        {
                            return (false, treeCount);
                        }
                    }

                    return (true, treeCount); 

                case "up" :
                    for (int i = row - 1; i >= 0; i--)
                    {
                        treeCount++;

                        if (value <= matrix[i][col])
                        {
                            return (false, treeCount);
                        }
                    }

                    return (true, treeCount); 

                case "down" :
                    for (int i = row + 1; i < matrix.Count; i++)
                    {
                        treeCount++;

                        if (value <= matrix[i][col])
                        {
                            return (false, treeCount);
                        }
                    }

                    return (true, treeCount); 

                default: throw new InvalidOperationException("Invalid Traversal attempted");
            }
        }

        async Task<string[]> ReadInputFile() =>
            await File.ReadAllLinesAsync(
            Path.Combine
            (Environment
            .GetFolderPath(Environment.SpecialFolder.MyDocuments), "day8instruc.txt"));
    }
}
