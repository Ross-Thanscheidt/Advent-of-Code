using Advent_of_Code.Year_2022_Day_13;

namespace Advent_of_Code
{
    public partial class Year_2022 : IYear
    {
        private (bool RightOrder, bool ContinueChecking) CompareListElements(List<IElement> list1, List<IElement> list2)
        {
            var rightOrder = false;
            var continueChecking = true;
            for (var idx = 0; idx < list1.Count && continueChecking; idx++)
            {
                if (idx < list2.Count)
                {
                    if (list1[idx] is ValueElement &&
                        list2[idx] is ValueElement)
                    {
                        var value1 = (list1[idx] as ValueElement).Value;
                        var value2 = (list2[idx] as ValueElement).Value;
                        if (value1 != value2)
                        {
                            rightOrder = value1 < value2;
                            continueChecking = false;
                        }
                    }
                    else if (list1[idx] is ListElement &&
                             list2[idx] is ListElement)
                    {
                        var list1List = (list1[idx] as ListElement).List;
                        var list2List = (list2[idx] as ListElement).List;
                        (rightOrder, continueChecking) = CompareListElements(list1List, list2List);
                        if (continueChecking && list1List.Count < list2List.Count)
                        {
                            rightOrder = true;
                            continueChecking = false;
                        }
                    }
                    else
                    {
                        if (list1[idx] is ListElement)
                        {
                            var valueList = new List<IElement>
                            {
                                list2[idx] as ValueElement
                            };

                            (rightOrder, continueChecking) = CompareListElements((list1[idx] as ListElement).List, valueList);
                        }
                        else
                        {
                            var valueList = new List<IElement>
                            {
                                list1[idx] as ValueElement
                            };

                            (rightOrder, continueChecking) = CompareListElements(valueList, (list2[idx] as ListElement).List);
                        }
                    }
                }
                else
                {
                    rightOrder = false;
                    continueChecking = false;
                }
            }
            if (continueChecking && list1.Count < list2.Count)
            {
                rightOrder = true;
                continueChecking = false;
            }

            return (rightOrder, continueChecking);
        }
    }
}
