namespace Advent.ProblemSets
{
    public class Day9
    {
        public async Task<(int, int)> SolveProblem()
        {
            var instructions = await ReadInputFile();

            var cmds = new List<(char dir, int steps)>();
            foreach (var instruction in instructions)
            {
                var parts = instruction.Split(' ');
                cmds.Add((Convert.ToChar(parts[0]), Convert.ToInt32(parts[1])));
            }


            var tailPostionsPart1 = new List<(int row, int col)>
            {
                (row : 0, col : 0)
            };

            var tailPostionsPart2 = new List<(int row, int col)>
            {
                (row : 0, col : 0),
                (row : 0, col : 0),
                (row : 0, col : 0),
                (row : 0, col : 0),
                (row : 0, col : 0),
                (row : 0, col : 0),
                (row : 0, col : 0),
                (row : 0, col : 0),
                (row : 0, col : 0),
            };

            return (Navigate(tailPostionsPart1, cmds), Navigate(tailPostionsPart2, cmds));

            
        }

        int Navigate(List<(int row, int col)> tailPostions, List<(char dir, int steps)> cmds)
        {
            var headPos = (row: 0, col: 0);

            var set = new HashSet<(int, int)> { tailPostions[0] };
            foreach (var c in cmds)
            {
                for (int i = 1; i <= c.steps; i++)
                {
                    headPos = c.dir switch
                    {
                        'U' => (headPos.row - 1, headPos.col),
                        'D' => (headPos.row + 1, headPos.col),
                        'L' => (headPos.row, headPos.col - 1),
                        'R' => (headPos.row, headPos.col + 1),
                        _ => throw new InvalidOperationException("Unexpected command for direction."),
                    };

                    var tempHead = headPos;

                    for (int p = 0; p < tailPostions.Count; p++)
                    {
                        tailPostions[p] = CheckAndAdjust_TailKnotWrtHeader(tempHead, tailPostions[p]);
                        tempHead = tailPostions[p];
                    }

                    set.Add((tailPostions[^1].row, tailPostions[^1].col));
                }
            }

            return set.Count;
        }

        (int row, int col) CheckAndAdjust_TailKnotWrtHeader((int row, int col) headPos, (int row, int col)tailPos)
        {
            if (headPos == tailPos) return tailPos;

            //check if T is in the periphery 
            var immediatePosns = GetAdjacentLocations(headPos.row, headPos.col);

            //YES : break;
            if (immediatePosns.Contains((tailPos))) return tailPos;

            if (headPos.row == tailPos.row)
            {
                tailPos.col += (headPos.col > tailPos.col ? 1 : -1);
                return tailPos;
            }

            else if (headPos.col == tailPos.col)
            {
                tailPos.row += (headPos.row > tailPos.row ? 1 : -1);
                return tailPos;
            }

            //diagonal : 
            //4 quads : top left / top right / bottom left / bottom right
            else
            {
                if (headPos.row < tailPos.row && headPos.col < tailPos.col)
                {
                    tailPos.row--;
                    tailPos.col--;
                }

                else if (headPos.row < tailPos.row && headPos.col > tailPos.col)
                {
                    tailPos.row--;
                    tailPos.col++;
                }

                else if (headPos.row > tailPos.row && headPos.col > tailPos.col)
                {
                    tailPos.row++;
                    tailPos.col++;
                }

                else
                {
                    tailPos.row++;
                    tailPos.col--;
                }

                return tailPos;
            }
        }

        (int row, int col)[] GetAdjacentLocations(int r, int c)
        {
            return new (int row, int col)[]
                {
                    (r-1, c-1),
                    (r-1, c+1),
                    (r+1, c+1),
                    (r+1, c-1),
                    (r-1, c),
                    (r+1, c),
                    (r, c+1),
                    (r, c-1),
                };
        }

        async Task<string[]> ReadInputFile() =>
            await File.ReadAllLinesAsync(
            Path.Combine
            (Environment
            .GetFolderPath(Environment.SpecialFolder.MyDocuments), "day9instruc.txt"));
    }
}
