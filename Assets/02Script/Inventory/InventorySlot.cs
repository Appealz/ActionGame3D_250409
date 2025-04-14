using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private bool isEmpty;

    public bool Empty => isEmpty;

    private int slotIndex;
    public int SlotIndex
    {
        get => slotIndex;
        set => slotIndex = value;
    }

    private Image icon;
    private GameObject SelectedObj;
    private TextMeshProUGUI amountText;
    private Button button;
    private bool isSelect;

    private void Awake()
    {
        // 구조가 결정나있는 프리팹이니까 가능한 구조
        transform.GetChild(0).TryGetComponent<Image>(out icon);
        SelectedObj = transform.GetChild(1).gameObject;
        transform.GetChild(2).TryGetComponent<TextMeshProUGUI>(out amountText);

        if(TryGetComponent<Button>(out button))
        {
            button.onClick.AddListener(OnClick_Select);
        }
    }


    // 외부에서 슬롯에 데이터를 주입해주는 함수
    // 외부에서 정보를 표기해주기 위한 아이템의 정보를 받아서 아이콘 바꿔치기
    // amonut 바꿔치기
    public void DrawItemSlot(InventoryItemData itemData)
    {
        if(TableDataManager.Instance.GetItemData(itemData.itemID, out ItemData_Entity itemInfo))
        {
            icon.sprite = IconLoadManager.Instance.GetIcon(itemData.itemID);
            icon.enabled = true;

            ChangeAmount(itemData.amount);
            isEmpty = false;
        }
        else
        {
            Debug.Log($"InventorySlot.cs - DrawItemSlot() - {itemData.itemID} TableData is not existed");
        }
    }
    // 슬롯의 아이템 제거(판매)
    public void ClearSlot()
    {
        isSelect = false;
        isEmpty = true;
        SelectedObj.SetActive(false);
        amountText.enabled = false;
        icon.enabled=false;
    }

    // 아이템 양 변경
    public void ChangeAmount(int newAmount)
    {
        if(newAmount <2)
        {
            amountText.enabled = false;
        }
        else
        {
            amountText.text = newAmount.ToString();            
        }
    }

    // 슬롯 선택 여부
    public void SetSelectSlot(bool isSelect)
    {
        SelectedObj.SetActive(isSelect);
        this.isSelect = isSelect;
    }

    // 자기자신 클릭여부 받아오기
    public void OnClick_Select()
    {
        if(!isEmpty)
        {
            isSelect = !isSelect;
            SetSelectSlot(isSelect);
        }
    }


}
