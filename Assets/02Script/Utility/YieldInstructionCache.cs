using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 동일한 WaitForSecond의 객체가 여러개 생성되지 않도록 DP를 사용

// internal
// 같은 어셈블리 내에서 접근 가능하고, 다른 어셈블리에서는 접근을 제한하는 제한자
// 어셈블리 : C++에서 새프로젝트 만들기 >> 솔루션 탐색기에 하나하나의 프로젝트가 있음, 이때 이 프로젝트를 C#에서는 어셈블리라고함.
// 즉, internal은 다른 어셈블리에서 접근이 불가능함.

// 누구나 접근해서 객체를 생성하지 않고 사용할 수 있음.
// 단, 멤버들 모두 static으로 처리가 되어있어야함.
internal static class YieldInstructionCache
{
    public static readonly Dictionary<float, WaitForSeconds> waitForSeconds = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds WaitForSeconds(float seconds)
    {
        WaitForSeconds wfs;
        if(!waitForSeconds.TryGetValue(seconds, out wfs)) // 객체(딕셔너리)가 없다면 객체를 생성해주고, 객체가 있다면 out을통해 wfs에 값을 참조.
        {
            waitForSeconds.Add(seconds, wfs = new WaitForSeconds(seconds)); // 딕셔너리에 넣으면서 wfs에 참조도 해줌.
        }        
        return wfs;
    }
    
    //// 코루틴의 WaitforSecond를 사용할때 
    //WaitForSeconds wfs1s = new WaitForSeconds(1f);
    //// 이와같이 객체를 미리 만들어 사용하더라도 이 스크립트가 여러곳에서 사용되면 객체가 여러개 만들어지므로
    //// 해당 클래스에서 한번 만들어 모든 클래스에서 사용하도록 진행    

    //IEnumerator testCo()
    //{
    //    // 1초에 한번씩 Hi를 찍는 코루틴.
    //    // 아래와 같이 new WaitForSeceond를 하게되면 매 프레임마다 객체를 생성하게됨.
    //    while(true)
    //    {
    //        Debug.Log("Hi");
    //        //yield return new WaitForSeconds(1.0f);
    //        yield return wfs1s;
    //    }
    //}
}
