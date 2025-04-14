using System;
using UnityEngine;
using UnityEngine.UI;


// 각각의 버튼을 통해서 유니티 이벤트 발생
// 커맨더 패턴 사용
// 중재자 패턴


public enum ButtonType
{
    BT_AttackBTN,
    BT_SkillBTN01,
    BT_SkillBTN02,
    BT_SkillBTN03,
    BT_MenuBTN,
    BT_InventoryBTN,
    BT_SkillInfoBTN,
}

public class UIManager : MonoBehaviour
{
    // 버튼바인딩은 이름으로
    private GameObject obj;
    private Button btn;

    public static event Action OnAttackButtonPressed;
    public static event Action OnSkill01ButtonPressed;
    public static event Action OnSkill02ButtonPressed;
    public static event Action OnSkill03ButtonPressed;

    private GameObject inventoryObj;
    private InventoryUI inventoryUI;
    private bool isOpenInventory;

    // 현재는 버튼이 4개이지만
    // 가변적으로 만드려면 Action의 타입을 int타입으로해서 넘버링을 넘겨주는 방식으로 진행.

    private void Awake()
    {
        InitManager();
    }

    public void InitManager()
    {
        obj = GameObject.Find("MenuBTN");
        if (obj != null )
        {
            if(obj.TryGetComponent<Button>(out btn))
            {
                btn.onClick.AddListener(() => HandleButtonClick(ButtonType.BT_MenuBTN));
            }
        }

        obj = GameObject.Find("InventoryBTN");
        if (obj != null)
        {
            if (obj.TryGetComponent<Button>(out btn))
            {
                btn.onClick.AddListener(() => HandleButtonClick(ButtonType.BT_InventoryBTN));
            }
        }

        obj = GameObject.Find("SkillInfoBTN");
        if (obj != null)
        {
            if (obj.TryGetComponent<Button>(out btn))
            {
                btn.onClick.AddListener(() => HandleButtonClick(ButtonType.BT_SkillInfoBTN));
            }
        }

        obj = GameObject.Find("AttackBTN");
        if (obj != null)
        {
            if (obj.TryGetComponent<Button>(out btn))
            {
                btn.onClick.AddListener(() => HandleButtonClick(ButtonType.BT_AttackBTN));
            }
        }

        obj = GameObject.Find("SkillBTN01");
        if (obj != null)
        {
            if (obj.TryGetComponent<Button>(out btn))
            {
                btn.onClick.AddListener(() => HandleButtonClick(ButtonType.BT_SkillBTN01));
            }
        }

        obj = GameObject.Find("SkillBTN02");
        if (obj != null)
        {
            if (obj.TryGetComponent<Button>(out btn))
            {
                btn.onClick.AddListener(() => HandleButtonClick(ButtonType.BT_SkillBTN02));
            }
        }

        obj = GameObject.Find("SkillBTN03");
        if (obj != null)
        {
            if (obj.TryGetComponent<Button>(out btn))
            {
                btn.onClick.AddListener(() => HandleButtonClick(ButtonType.BT_SkillBTN03));
            }
        }

        // 인벤토리 팝업 탐색.
        if(inventoryObj == null)
        {
            inventoryObj = GameObject.Find("InventoryPopup");
            if(inventoryObj && !inventoryObj.TryGetComponent<InventoryUI>(out inventoryUI))
            {
                Debug.Log("UIManager.cs - InitManager() - inventoryUI is not referenced");
            }

            // UI 오브젝트를 잘 찾았다면 scale을 0으로 변경
            // rectTransform에서 스케일값을 조정하면 안되는 이유 : UI를 끄는경우 스케일을 0으로만드는 방법을 자주 사용하는데 스케일을 조정하게되면 기존 스케일 
            inventoryObj.LeanScale(Vector3.zero, 0.1f);
            isOpenInventory = false;
        }
    }

    private void HandleButtonClick(ButtonType type)
    {
        switch(type)
        {
            case ButtonType.BT_AttackBTN:
                Debug.Log(ButtonType.BT_AttackBTN);
                OnAttackButtonPressed?.Invoke();
                break;
            case ButtonType.BT_SkillBTN01:
                Debug.Log(ButtonType.BT_SkillBTN01);
                break;
            case ButtonType.BT_SkillBTN02:
                Debug.Log(ButtonType.BT_SkillBTN02);
                break;
            case ButtonType.BT_SkillBTN03:
                Debug.Log(ButtonType.BT_SkillBTN03);
                break;
            case ButtonType.BT_MenuBTN:                
                Debug.Log(ButtonType.BT_MenuBTN);
                break;
            case ButtonType.BT_InventoryBTN:
                ShowInventory();
                Debug.Log(ButtonType.BT_InventoryBTN);
                break;
            case ButtonType.BT_SkillInfoBTN:
                Debug.Log(ButtonType.BT_SkillInfoBTN);
                break;
        }
    }

    public void ShowInventory()
    {
        isOpenInventory = !isOpenInventory;
        if(isOpenInventory)
        {
            inventoryObj.LeanScale(Vector3.one, 0.7f).setEase(LeanTweenType.easeInElastic);
            inventoryUI.RefreshInventoryUI();
        }
        else
        {
            inventoryObj.LeanScale(Vector3.zero, 0.7f).setEase(LeanTweenType.easeInElastic);
        }
    }

}
