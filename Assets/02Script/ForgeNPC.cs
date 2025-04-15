using System.Collections.Generic;
using System.Linq; // << 링크 : C#에서 제공해주는 쿼리문
using UnityEngine;


public class ForgeNPC : MonoBehaviour
{
    // 플레이어랑 오버랩이 발생하면
    // 플레이어가 가지고 있는 아이템의 목록을 복사하고
    // 복사된 목록에서 일반 아이템들을 제거한 뒤에
    // 장착이 가능한 아이템의 목록을 만들어서
    // 디버그로 표시.


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            printForgeItemList();
        }
    }

    List<InventoryItemData> copyList = new List<InventoryItemData>();
    // 사용자가 가지고 있던 아이템 중 재련소에 표기 가능한 아이템 목록 생성
    private void printForgeItemList()
    {
        //copyList = GameManager.Instance.INVEN.GetItemList2(); // 얕은복사를 하게되면 인벤토리내 아이템이 사라지게됨.
        copyList = GameManager.Instance.INVEN.GetItemList2().ToList<InventoryItemData>(); // 깊은복사(값복사)

        // 일반적인 정방향의 순서로 진행되면 인덱스가 꼬이기 때문에 역방향으로 설계
        for (int i = copyList.Count - 1; i>= 0; i--)
        {
            if (TableDataManager.Instance.GetItemData(copyList[i].itemID, out ItemData_Entity tableData))
            {
                if(!tableData.equip) // 장착템이 아닌경우
                {
                    copyList.RemoveAt(i); // 리스트에서 제거
                }
            }
            // 풀 강화가 끝난 아이템의 경우는 제거.
            // 강화가 불가능한 아이템 제거.
        }

        Debug.Log("---------------------- 인벤토리내 아이템 --------------------------------");
        foreach(InventoryItemData itemData in copyList)
        {
            Debug.Log(itemData.itemID);
        }

        // copyList[0].uID; // uID를 통해서 업그레이드 된 아이템을 적용해줌.
        Debug.Log("-------------------------------------------------------------------------");
    }

    


}
