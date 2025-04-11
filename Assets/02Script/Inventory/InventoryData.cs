using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

// 인벤토리에 있는 아이템들의 목록을 관리하기 위한 데이터로서의 클래스들

// MonoBehaviour를 상속받지 않는 클래스의 필드값들을 Inspector창에서 보기 위한 attribute, 노출시킬수 있도록 직렬화
[System.Serializable]
public class InventoryItemData
{
    // 유저가 가지고 있는 아이템으로서 목록을 만들어주기 위한 클래스
    // 어떤 아이템이 몇개 있는지만 알고있으면 됨.
    public int itemID; // 테이블 ID => 아이템의 종류.
    public int amount; // 보유 아이템의 개수
    public int uID; // 각각의 아이템의 고유(unique) ID => 구분을 짓기위한 ID : 똑같은 테이블 ID를 갖는 아이템들을 구분하기 위해 고유한 코드를 부여.
}


// 인벤토리 정보 클래스
[System.Serializable]
public class InventoryData
{
    private int maxItemSlot = 18; // 인벤토리 최대개수
    // 외부에서 접근하기 위해 public
    public int MaxItemSlot => maxItemSlot; // 읽기전용

    private int curItemCount; // 현재 보유하고 있는 아이템의 개수
    public int CurItemCount
    {
        get => curItemCount;
        set => curItemCount = value; // 외부에서 접근하여 값을 변경할 수 도 있으므로 단, 검증 과정을 거치기위해 이와같이 설계
    }

    // 인벤토리 아이템 데이터를 저장하기 위한 리스트.
    private List<InventoryItemData> items = new List<InventoryItemData>(); // 아이템의 실질적인 목록. => 인벤토리의 크기가 작으므로 순차접근을 하는 리스트를 사용함.
    // 하지만 인벤토리가 매우 커지게된다면 list는 비효율적이므로 다른 자료구조 사용.

    // ex) 빨간물약을 5개 가지고 있을때, 상점에서 10개를 구매하면
    // 아이템의 정보를 받아와 추가해주어야함.

    /// <summary>
    /// 어떤 아이템을 몇개를 습득했는지 추가되는 아이템의 정보
    /// </summary>
    /// <param name="newItem"></param>
    public void AddItem(InventoryItemData newItem)
    {
        // 습득한 아이템이 이미 인벤토리에 있다면 그 슬롯에 위치 Index를 탐색
        int index = FindItemIndex(newItem);

        if(TableDataManager.Instance.GetItemData(newItem.itemID, out ItemData_Entity newItemData))
        {
            // 테이블 데이터를 정상적으로 불러왔다면
            if(newItemData.equip) // 장착이 가능하다면(장비 아이템이라면)
            {
                if(!IsFull()) // 장비창이 가득 차지 않았다면
                {
                    // uID 생성 후
                    newItem.uID = 1004; // todo : uID 생성기 만든 후 수정
                    // 리스트에 추가
                    items.Add(newItem);
                    curItemCount++;// 현재 아이템 개수 증가.
                }
            }
            else if(index < 0) // 현 인벤토리에 같은 아이템이 없는 경우
            {
                if(!IsFull())
                {
                    newItem.uID = -1; // 겹쳐지는 아이템은 uID가 없도록
                    items.Add(newItem);
                    curItemCount++;
                }
            }
            else // 이미 동일한 아이템이 있고, 겹쳐지는 아이템일때
            {
                items[index].amount += newItem.amount; // 기존에 있는 아이템의 개수만큼 새로들어온 아이템의 개수를 더해줌.
            }
        }
        else
        {
            // 테이블 데이터를 정상적으로 참조 실패
            Debug.Log($"InventoryData.cs - AddItem() - fail. table is not referenced , itemID : {newItem.itemID}");
        }
    }

    // 외부에서 아이템의 목록을 참조하기 위한 메소드
    // 참조형으로 반환.(값복사 x) => 외부에서 데이터를 수정할 수 있으므로 위험하긴함.
    public List<InventoryItemData> GetItemList()
    {
        // 현재 이코드는 캡슐화 되어있지 않으므로 위험한 코드.
        curItemCount = items.Count;
        return items;
    }


    public int DeleteItem(InventoryItemData deleteItem)
    {
        int index = FindItemIndex(deleteItem);
        if(index < 0)
        {
            Debug.Log($"InventroyData.cs - DeleteItem() - 아이템 제거 실패 {deleteItem.itemID}");
            return -1001; // 아이템 없는 에러코드 반환.
        }
        else
        {
            // 보유 개수가 삭제하려는 개수보다 적은 경우
            if (items[index].amount < deleteItem.amount)
            {
                Debug.Log($"InventoryData.cs - DeleteITem() - 아이템 제거 실패 {deleteItem.itemID}");
                return -1002; // 아이템 개수 에러 코드 반환.
            }
            // 삭제하려는 아이템의 개수가 보유하고 있는 아이템의 개수보다 적은 경우(amonut만 수정)
            else
            {
                items[index].amount -= deleteItem.amount; // 아이템 양 줄여주기
                if(items[index].amount <= 0) // 만약 남은 아이템의 개수가 0이하라면
                {
                    items.RemoveAt(index); // 아이템 리스트에서 제거
                    curItemCount--; // 현재 보유 아이템개수 감소
                }
            }
        }
        return 0; // 정상적으로 삭제 성공.
    }

    // 이미 소유하고 있는 아이템의 정보를 수정(갱신)하기 위한 메소드
    // ex) 장비아이템의 업그레이드시 아이템의 세부 정보를 참조하는 Table의 ID값을 변경해주는 메소드.   
    public void UpdateItemInfo(InventoryItemData data)
    {
        foreach(var item in items)
        {
            if(item.uID == data.uID)
            {
                item.itemID += 1; // 1강 성공하는 경우 다음 강화 아이템의 itemID로 변경.
            }
        }
    }

    // 인벤토리 여유 공간 여부
    public bool IsFull()
    {
        // 현재 아이템개수가 최대 아이템슬롯개수보다 크거나 같을때
        return curItemCount >= maxItemSlot;
    }

    // ex) 상점에서 빨간물약을 구매시에 인벤토리에 빨간물약이 있다면 amount만 늘려주면 되지만
    // 그게 아니라면 이미지를 생성해주어야함.
    // 현 인벤토리 내에 같은 tableID의 아이템이 있다면, 몇번째 Index에 있는지 확인 후 리턴해주는 메소드
    private int FindItemIndex(InventoryItemData newItem)
    {
        for(int i = items.Count - 1; i >= 0; i--)
        {
            if (items[i].itemID == newItem.itemID) // 동일한 종류의 아이템을 찾은경우
            {
                return i;
            }
        }
        return -1;
    }

    // 추가적으로 구현가능한 기능 : 아이템 정렬 기능, 슬롯 위치 변경 기능.
}

