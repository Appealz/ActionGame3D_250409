using System.Collections.Generic;
using UnityEngine;

public class ListTest : MonoBehaviour
{
    public List<int> ints = new List<int>();

    public List<int> GetList()
    {
        return ints;
    }

    public IReadOnlyList<int> GetList2()
    {
        return ints;
    }
}
