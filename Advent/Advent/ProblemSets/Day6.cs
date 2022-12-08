namespace Advent.ProblemSets
{
    public class Day6
    {
        public int SolveProblem(string inp, int numOfChars)
        {
            var set = Enumerable.Range(0, numOfChars).Select(i => '0').ToArray();
            var targetLength = set.Length - 1;

            for (int i = 0; i < inp.Length; i++)
            {
                bool isMatch = false;
                int matchInx = 0;
                //bool patternMatch = false;

                for (int j = 0; j < set.Length; j++)
                {
                    if (set[j] == inp[i])
                    {
                        isMatch = true;
                        matchInx = j;
                    }
                }

                if (isMatch)
                {
                    //clear
                    for (int m = 0; m <= matchInx; m++)
                    {
                        set[m] = '0';
                    }

                    //shift
                    for (int m = matchInx + 1; m < set.Length; m++)
                    {
                        set[m - (matchInx + 1)] = set[m];
                        set[m] = '0';
                    }

                    //add new element
                    for (int j = 0; j < set.Length; j++)
                    {
                        if (set[j] == '0')
                        {
                            set[j] = inp[i];
                            break;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < set.Length; j++)
                    {
                        if (set[j] == '0')
                        {
                            set[j] = inp[i];
                            if (j == targetLength)
                            {
                                return i + 1;
                                //Console.WriteLine($"the count is : {i + 1}");
                                //patternMatch = true;
                            }
                            break;
                        }
                    }
                }

                //if (patternMatch) break;
            }

            return -1;
        }
    }
}
