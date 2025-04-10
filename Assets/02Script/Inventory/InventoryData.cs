using System.Collections.Generic;
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
    private List<InventoryItemData> items = new List<InventoryItemData>(); // 아이템의 실질적인 목록.

    // ex) 빨간물약을 5개 가지고 있을때, 상점에서 10개를 구매하면
    // 아이템의 정보를 받아와 추가해주어야함.

    /// <summary>
    /// 어떤 아이템을 몇개를 습득했는지 추가되는 아이템의 정보
    /// </summary>
    /// <param name="newItem"></param>
    public void AddItem(InventoryItemData newItem)
    {
        
    }

    // 외부에서 아이템의 목록을 참조하기 위한 메소드
    // 참조형으로 반환.(값복사 x) => 외부에서 데이터를 수정할 수 있으므로 위험하긴함.
    public List<InventoryItemData> GetItemList()
    {
        return items;
    }

    public int DeleteItem(InventoryItemData deleteItem)
    {
        return 0;
    }

    // 이미 소유하고 있는 아이템의 정보를 수정(갱신)하기 위한 메소드
    // ex) 장비아이템의 업그레이드시 아이템의 세부 정보를 참조하는 Table의 ID값을 변경해주는 메소드.
    public void UpdateItemInfo(InventoryItemData data)
    {

    }

    // 인벤토리 여유 공간 여부
    public bool IsFull()
    {
        return false;
    }

    // ex) 상점에서 빨간물약을 구매시에 인벤토리에 빨간물약이 있다면 amount만 늘려주면 되지만
    // 그게 아니라면 이미지를 생성해주어야함.
    // 현 인벤토리 내에 같은 tableID의 아이템이 있다면, 몇번째 Index에 있는지 확인 후 리턴해주는 메소드
    private int FindItemIndex(InventoryItemData newItem)
    {
        return -1;
    }

}

