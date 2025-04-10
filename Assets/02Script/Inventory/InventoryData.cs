using System.Collections.Generic;
using UnityEngine;

// �κ��丮�� �ִ� �����۵��� ����� �����ϱ� ���� �����ͷμ��� Ŭ������

// MonoBehaviour�� ��ӹ��� �ʴ� Ŭ������ �ʵ尪���� Inspectorâ���� ���� ���� attribute, �����ų�� �ֵ��� ����ȭ
[System.Serializable]
public class InventoryItemData
{
    // ������ ������ �ִ� ���������μ� ����� ������ֱ� ���� Ŭ����
    // � �������� � �ִ����� �˰������� ��.
    public int itemID; // ���̺� ID => �������� ����.
    public int amount; // ���� �������� ����
    public int uID; // ������ �������� ����(unique) ID => ������ �������� ID : �Ȱ��� ���̺� ID�� ���� �����۵��� �����ϱ� ���� ������ �ڵ带 �ο�.
}


// �κ��丮 ���� Ŭ����
[System.Serializable]
public class InventoryData
{
    private int maxItemSlot = 18; // �κ��丮 �ִ밳��
    // �ܺο��� �����ϱ� ���� public
    public int MaxItemSlot => maxItemSlot; // �б�����

    private int curItemCount; // ���� �����ϰ� �ִ� �������� ����
    public int CurItemCount
    {
        get => curItemCount;
        set => curItemCount = value; // �ܺο��� �����Ͽ� ���� ������ �� �� �����Ƿ� ��, ���� ������ ��ġ������ �̿Ͱ��� ����
    }

    // �κ��丮 ������ �����͸� �����ϱ� ���� ����Ʈ.
    private List<InventoryItemData> items = new List<InventoryItemData>(); // �������� �������� ���.

    // ex) ���������� 5�� ������ ������, �������� 10���� �����ϸ�
    // �������� ������ �޾ƿ� �߰����־����.

    /// <summary>
    /// � �������� ��� �����ߴ��� �߰��Ǵ� �������� ����
    /// </summary>
    /// <param name="newItem"></param>
    public void AddItem(InventoryItemData newItem)
    {
        
    }

    // �ܺο��� �������� ����� �����ϱ� ���� �޼ҵ�
    // ���������� ��ȯ.(������ x) => �ܺο��� �����͸� ������ �� �����Ƿ� �����ϱ���.
    public List<InventoryItemData> GetItemList()
    {
        return items;
    }

    public int DeleteItem(InventoryItemData deleteItem)
    {
        return 0;
    }

    // �̹� �����ϰ� �ִ� �������� ������ ����(����)�ϱ� ���� �޼ҵ�
    // ex) ���������� ���׷��̵�� �������� ���� ������ �����ϴ� Table�� ID���� �������ִ� �޼ҵ�.
    public void UpdateItemInfo(InventoryItemData data)
    {

    }

    // �κ��丮 ���� ���� ����
    public bool IsFull()
    {
        return false;
    }

    // ex) �������� ���������� ���Žÿ� �κ��丮�� ���������� �ִٸ� amount�� �÷��ָ� ������
    // �װ� �ƴ϶�� �̹����� �������־����.
    // �� �κ��丮 ���� ���� tableID�� �������� �ִٸ�, ���° Index�� �ִ��� Ȯ�� �� �������ִ� �޼ҵ�
    private int FindItemIndex(InventoryItemData newItem)
    {
        return -1;
    }

}

