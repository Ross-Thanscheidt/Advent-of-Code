using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Advent_of_Code.Year_2023_Day_19;

namespace Advent_of_Code
{
    public partial class Year_2023 : IYear
    {
        [GeneratedRegex("(?<Name>[a-z]+){([xmas][<>]\\d+:(A|R|[a-z]+),?)+}")]
        private static partial Regex Day_19_WorkflowRegex();

        private Dictionary<string, List<WorkflowRule>> _Day_19_Workflows = [];
        private string _Day_19_Categories = "xmas";
        private List<int[]> _Day_19_AcceptedConditions = [];

        private void Day_19_Explore_Workflow(string workflowName, int[] conditionSet)
        {
            foreach (WorkflowRule rule in _Day_19_Workflows[workflowName])
            {
                if (rule.Result == 'A')
                {
                    _Day_19_AcceptedConditions.Add(conditionSet);
                }
                else if (rule.Workflow.Length > 0)
                {
                    Day_19_Explore_Workflow(rule.Workflow, conditionSet);
                }
                else if (rule.Comparison != 'X')
                {
                    int conditionSetIndex = _Day_19_Categories.IndexOf(rule.Category) * 2;

                    if (rule.ResultIfTrue == 'A')
                    {
                        var modifiedConditionSet = new int[conditionSet.Length];
                        Array.Copy(conditionSet, modifiedConditionSet, conditionSet.Length);

                        if (rule.Comparison == '<')
                        {
                            modifiedConditionSet[conditionSetIndex + 1] = Math.Min(modifiedConditionSet[conditionSetIndex + 1], rule.Value - 1);
                        }
                        else if (rule.Comparison == '>')
                        {
                            modifiedConditionSet[conditionSetIndex] = Math.Max(modifiedConditionSet[conditionSetIndex], rule.Value + 1);
                        }

                        if (modifiedConditionSet[conditionSetIndex] <= modifiedConditionSet[conditionSetIndex])
                        {
                            _Day_19_AcceptedConditions.Add(modifiedConditionSet);
                        }
                    }
                    else if (rule.WorkflowIfTrue.Length > 0)
                    {
                        var modifiedConditionSet = new int[conditionSet.Length];
                        Array.Copy(conditionSet, modifiedConditionSet, conditionSet.Length);

                        if (rule.Comparison == '<')
                        {
                            modifiedConditionSet[conditionSetIndex + 1] = Math.Min(modifiedConditionSet[conditionSetIndex + 1], rule.Value - 1);
                        }
                        else if (rule.Comparison == '>')
                        {
                            modifiedConditionSet[conditionSetIndex] = Math.Max(modifiedConditionSet[conditionSetIndex], rule.Value + 1);
                        }

                        if (modifiedConditionSet[conditionSetIndex] <= modifiedConditionSet[conditionSetIndex])
                        {
                            Day_19_Explore_Workflow(rule.WorkflowIfTrue, modifiedConditionSet);
                        }
                    }

                    // The next rule for this workflow is reached when this Comparison is not True
                    if (rule.Comparison == '<')
                    {
                        conditionSet[conditionSetIndex] = Math.Max(conditionSet[conditionSetIndex], rule.Value);
                    }
                    else if (rule.Comparison == '>')
                    {
                        conditionSet[conditionSetIndex + 1] = Math.Min(conditionSet[conditionSetIndex + 1], rule.Value);
                    }

                    if (conditionSet[conditionSetIndex] > conditionSet[conditionSetIndex])
                    {
                        // Abort exploring this workflow further due to impossible conditions
                        return;
                    }
                }
            }
        }

        public string Day_19(StringReader input)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long totalAcceptedRatings = 0;
            ulong combinationsOfRatings = 0;

            for (var line = input.ReadLine(); line != null; line = input.ReadLine())
            {
                if (line.StartsWith('{'))
                {
                    Dictionary<char, long> ratings = [];

                    var ratingsLine = line[1..].Split('}')[0].Split(',');

                    foreach (var rating in ratingsLine)
                    {
                        ratings.Add(rating[0], long.Parse(rating[2..]));
                    }

                    string currentWorkflow = "in";
                    int currentWorkflowIndex = 0;
                    char result = 'X';

                    while (result == 'X')
                    {
                        var rule = _Day_19_Workflows[currentWorkflow][currentWorkflowIndex];
                        if (rule.Result != 'X')
                        {
                            result = rule.Result;
                        }
                        else if (rule.Workflow.Length > 0)
                        {
                            currentWorkflow = rule.Workflow;
                            currentWorkflowIndex = 0;
                        }
                        else if (rule.Comparison != 'X')
                        {
                            var value = ratings[rule.Category];

                            if ((rule.Comparison == '<' && value < rule.Value) ||
                                (rule.Comparison == '>' && value > rule.Value))
                            {
                                if (rule.WorkflowIfTrue.Length > 0)
                                {
                                    currentWorkflow = rule.WorkflowIfTrue;
                                    currentWorkflowIndex = 0;
                                }
                                else if (rule.ResultIfTrue != 'X')
                                {
                                    result = rule.ResultIfTrue;
                                }
                            }
                            else
                            {
                                currentWorkflowIndex++;
                            }
                        }
                    }

                    if (result == 'A')
                    {
                        totalAcceptedRatings += ratings.Sum(r => r.Value);
                    }
                }
                else if (line.Length > 0)
                {
                    var workflowName = line.Split('{')[0];
                    var lineRules = line.Split('{')[1].Split('}')[0].Split(',');
                    List<WorkflowRule> rules = [];

                    foreach (var lineRule in lineRules)
                    {
                        var rule = new WorkflowRule();

                        if (lineRule == "A" || lineRule == "R")
                        {
                            rule.Result = lineRule[0];
                        }
                        else if (lineRule.Contains(':'))
                        {
                            rule.Category = lineRule[0];
                            rule.Comparison = lineRule[1];
                            rule.Value = int.Parse(lineRule.Split(':')[0][2..]);
                            var truePart = lineRule.Split(':')[1];
                            if (truePart == "A" || truePart == "R")
                            {
                                rule.ResultIfTrue = truePart[0];
                            }
                            else
                            {
                                rule.WorkflowIfTrue = truePart;
                            }
                        }
                        else
                        {
                            rule.Workflow = lineRule;
                        }

                        rules.Add(rule);
                    }

                    _Day_19_Workflows.Add(workflowName, rules);
                }
            }

            Day_19_Explore_Workflow("in", [1, 4_000, 1, 4_000, 1, 4_000, 1, 4_000]);

            for (var acceptedConditionsIndex = 0; acceptedConditionsIndex < _Day_19_AcceptedConditions.Count; acceptedConditionsIndex++)
            {
                ulong product = 1;

                for (int categoryIndex = 0; categoryIndex < _Day_19_Categories.Length; categoryIndex++)
                {
                    product *= (ulong)(_Day_19_AcceptedConditions[acceptedConditionsIndex][categoryIndex * 2 + 1] - _Day_19_AcceptedConditions[acceptedConditionsIndex][categoryIndex * 2] + 1);
                }

                combinationsOfRatings += product;
            }

            stopwatch.Stop();

            return $"{totalAcceptedRatings:N0} is the sum of the rating numbers for all of the accepted parts\r\n" +
                   $"{combinationsOfRatings:N0} is the number of distinct combinations of ratings that will be accepted\r\n" +
                   $"({stopwatch.Elapsed.TotalMilliseconds} ms)";
        }
    }
}