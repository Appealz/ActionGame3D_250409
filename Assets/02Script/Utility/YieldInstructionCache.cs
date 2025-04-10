using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ WaitForSecond�� ��ü�� ������ �������� �ʵ��� DP�� ���

// internal
// ���� ����� ������ ���� �����ϰ�, �ٸ� ����������� ������ �����ϴ� ������
// ����� : C++���� ��������Ʈ ����� >> �ַ�� Ž���⿡ �ϳ��ϳ��� ������Ʈ�� ����, �̶� �� ������Ʈ�� C#������ ����������.
// ��, internal�� �ٸ� ��������� ������ �Ұ�����.

// ������ �����ؼ� ��ü�� �������� �ʰ� ����� �� ����.
// ��, ����� ��� static���� ó���� �Ǿ��־����.
internal static class YieldInstructionCache
{
    public static readonly Dictionary<float, WaitForSeconds> waitForSeconds = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds WaitForSeconds(float seconds)
    {
        WaitForSeconds wfs;
        if(!waitForSeconds.TryGetValue(seconds, out wfs)) // ��ü(��ųʸ�)�� ���ٸ� ��ü�� �������ְ�, ��ü�� �ִٸ� out������ wfs�� ���� ����.
        {
            waitForSeconds.Add(seconds, wfs = new WaitForSeconds(seconds)); // ��ųʸ��� �����鼭 wfs�� ������ ����.
        }        
        return wfs;
    }
    
    //// �ڷ�ƾ�� WaitforSecond�� ����Ҷ� 
    //WaitForSeconds wfs1s = new WaitForSeconds(1f);
    //// �̿Ͱ��� ��ü�� �̸� ����� ����ϴ��� �� ��ũ��Ʈ�� ���������� ���Ǹ� ��ü�� ������ ��������Ƿ�
    //// �ش� Ŭ�������� �ѹ� ����� ��� Ŭ�������� ����ϵ��� ����    

    //IEnumerator testCo()
    //{
    //    // 1�ʿ� �ѹ��� Hi�� ��� �ڷ�ƾ.
    //    // �Ʒ��� ���� new WaitForSeceond�� �ϰԵǸ� �� �����Ӹ��� ��ü�� �����ϰԵ�.
    //    while(true)
    //    {
    //        Debug.Log("Hi");
    //        //yield return new WaitForSeconds(1.0f);
    //        yield return wfs1s;
    //    }
    //}
}
