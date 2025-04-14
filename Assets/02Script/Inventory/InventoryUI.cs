using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 슬롯을 생성해주고, 팝업이 열릴 때 슬롯을 갱신해주는 역할.
public class InventoryUI : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GameObject slotPrefabs;
    [SerializeField] private RectTransform contentTrans;

    private List<InventorySlot> slots = new List<InventorySlot>();
    private InventorySlot slot;

    private int currentCount;
    private int maxCount;

    // 읽기전용 리스트
    private IReadOnlyList<InventoryItemData> dataList;

    private void Awake()
    {
        InitSlot();
    }



    private void InitSlot()
    {
        maxCount = 18;

        for(int i = 0; i< maxCount; i++)
        {
            if(Instantiate(slotPrefabs, contentTrans).TryGetComponent<InventorySlot>(out slot))
            {
                slot.SlotIndex = i;
                slots.Add(slot);
            }
        }
    }

    // 인벤토리 정보 최신정보로 갱신 (팝업창이 열릴때 호출)
    public void RefreshInventoryUI()
    {
        // 목록 받아오기
        dataList = GameManager.Instance.INVEN.GetItemList();
        currentCount = GameManager.Instance.INVEN.CurItemCount;
        
        for(int i = 0; i < maxCount; i++)
        {
            if(i < currentCount && dataList[i].itemID > -1)
            {
                // 아이템 데이터 정보를 제대로 가지고있으면서 현재 카운트보다 i가 작다면
                slots[i].DrawItemSlot(dataList[i]); // 아이템 그려주기
            }
            else
            {
                slots[i].ClearSlot(); // 그게 아니라면 클리어
            }

            slots[i].SetSelectSlot(false);
        }

    }
}
