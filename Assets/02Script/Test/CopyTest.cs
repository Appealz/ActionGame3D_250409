using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CopyTest : MonoBehaviour
{
    //ListTest testList = new ListTest();

    //private void Awake()
    //{
    //    testList.ints.Add(1);
    //    testList.ints.Add(2);
    //    testList.ints.Add(3);
    //    testList.ints.Add(4);
    //    testList.ints.Add(5);



    //    Debug.Log(testList.ints.Count);


    //    IReadOnlyList<int> readOnlyTest = testList.GetList2();
    //    List<int> test = testList.GetList();

    //    test.Add(9);

    //    Debug.Log($"{readOnlyTest.Count} : 리드온리 갯수");

    //    // 참조형 변수의 사전적 의미만 생각할 뿐 아니라 어디서 어떻게 사용되어야하는지 생각해봐야함.
    //}


    List<int> ints = new List<int>();

    //private void Awake()
    //{
    //    ints.Add(1);
    //    ints.Add(2);
    //    ints.Add(3);
    //    ints.Add(4);
    //    ints.Add(5);

    //    // SQL : 데이터베이스 종류 중 하나.
    //    // 쿼리문 : 질문
    //    // 데이터베이스에게 ~~한 데이터가 있니? => 대답을 해줌
    //    // Where 쿼리문중 한 종류
    //    var holNums = ints.Where(n => n % 2 == 1);
    //    foreach(var n in holNums)
    //    {
    //        Debug.Log(n);
    //    }
    //string[] names = { "kim", "lee", "park" };

    //var upperNames = names.Select(n => n.ToUpper());

    //    foreach (var n in upperNames)
    //    {
    //        Debug.Log(n);
    //    }
    //}

    private void Awake()
    {
        int[] nums = { 3, 5, 1, 9, 8 };
        var sorted = nums.OrderBy(x => x);

        foreach(int i in sorted)
        {
            Debug.Log(i);
        }
    }
}
