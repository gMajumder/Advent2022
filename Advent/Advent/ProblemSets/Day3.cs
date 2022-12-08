namespace Advent.ProblemSets
{
    public class Day3
	{
		public async Task<int> SolveProblem(string part)
        {
            string[] instructions = await ReadInputFile();

            var dict = new Dictionary<char, int>();
            int asciiFor_lowercase = Convert.ToInt32('a');
            int asciiFor_Uppercase = Convert.ToInt32('A');

            for (int i = 1; i <= 26; i++)
            {
                dict.Add(Convert.ToChar(asciiFor_lowercase), i);
                asciiFor_lowercase++;
            }

            for (int i = 27; i <= 52; i++)
            {
                dict.Add(Convert.ToChar(asciiFor_Uppercase), i);
                asciiFor_Uppercase++;
            }

            var set = new List<int>();
            int skip = 0;
            int take = 3;

            return part.ToUpper() switch
            {
                "PART1" => Problem_Part1(instructions, dict, set),
                "PART2" => Problem_Part2(instructions, dict, set, skip, take),
                _ => throw new InvalidOperationException("Undesirable Part found"),
            };
        }

        private static async Task<string[]> ReadInputFile() =>
            await File.ReadAllLinesAsync(
                            Path.Combine
                            (Environment
                            .GetFolderPath(Environment.SpecialFolder.MyDocuments), "day3instruc.txt"));

        int Problem_Part1(string[] instructions, Dictionary<char, int> dict, List<int> set)
        {
            foreach (var line in instructions)
            {
                if (line.Length % 2 != 0) throw new Exception("invalid line length");

                var mid = line.Length / 2;

                var first = line.Substring(0, mid);
                var second = line.Substring(mid);

                foreach (var c in first)
                {
                    if (second.Contains(c))
                    {
                        set.Add(dict[c]);
                        break;
                    }
                }
            }

            return set.Sum();
        }

        int Problem_Part2(string[] instructions, Dictionary<char, int> dict, List<int> set, int skip, int take)
        {
            while (skip != instructions.Length)
            {
                var sets = instructions.Skip(skip).Take(take).ToList();

                if (sets.Count != 3) throw new Exception("wrong skip and take");

                foreach (var c in sets[0])
                {
                    if (sets[1].Contains(c) && sets[2].Contains(c))
                    {
                        set.Add(dict[c]);
                        break;
                    }
                }

                skip += 3;
            }

            return set.Sum();
        }
    }
}
