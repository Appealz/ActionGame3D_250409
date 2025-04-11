using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
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
    private List<InventoryItemData> items = new List<InventoryItemData>(); // �������� �������� ���. => �κ��丮�� ũ�Ⱑ �����Ƿ� ���������� �ϴ� ����Ʈ�� �����.
    // ������ �κ��丮�� �ſ� Ŀ���Եȴٸ� list�� ��ȿ�����̹Ƿ� �ٸ� �ڷᱸ�� ���.

    // ex) ���������� 5�� ������ ������, �������� 10���� �����ϸ�
    // �������� ������ �޾ƿ� �߰����־����.

    /// <summary>
    /// � �������� ��� �����ߴ��� �߰��Ǵ� �������� ����
    /// </summary>
    /// <param name="newItem"></param>
    public void AddItem(InventoryItemData newItem)
    {
        // ������ �������� �̹� �κ��丮�� �ִٸ� �� ���Կ� ��ġ Index�� Ž��
        int index = FindItemIndex(newItem);

        if(TableDataManager.Instance.GetItemData(newItem.itemID, out ItemData_Entity newItemData))
        {
            // ���̺� �����͸� ���������� �ҷ��Դٸ�
            if(newItemData.equip) // ������ �����ϴٸ�(��� �������̶��)
            {
                if(!IsFull()) // ���â�� ���� ���� �ʾҴٸ�
                {
                    // uID ���� ��
                    newItem.uID = 1004; // todo : uID ������ ���� �� ����
                    // ����Ʈ�� �߰�
                    items.Add(newItem);
                    curItemCount++;// ���� ������ ���� ����.
                }
            }
            else if(index < 0) // �� �κ��丮�� ���� �������� ���� ���
            {
                if(!IsFull())
                {
                    newItem.uID = -1; // �������� �������� uID�� ������
                    items.Add(newItem);
                    curItemCount++;
                }
            }
            else // �̹� ������ �������� �ְ�, �������� �������϶�
            {
                items[index].amount += newItem.amount; // ������ �ִ� �������� ������ŭ ���ε��� �������� ������ ������.
            }
        }
        else
        {
            // ���̺� �����͸� ���������� ���� ����
            Debug.Log($"InventoryData.cs - AddItem() - fail. table is not referenced , itemID : {newItem.itemID}");
        }
    }

    // �ܺο��� �������� ����� �����ϱ� ���� �޼ҵ�
    // ���������� ��ȯ.(������ x) => �ܺο��� �����͸� ������ �� �����Ƿ� �����ϱ���.
    public List<InventoryItemData> GetItemList()
    {
        // ���� ���ڵ�� ĸ��ȭ �Ǿ����� �����Ƿ� ������ �ڵ�.
        curItemCount = items.Count;
        return items;
    }


    public int DeleteItem(InventoryItemData deleteItem)
    {
        int index = FindItemIndex(deleteItem);
        if(index < 0)
        {
            Debug.Log($"InventroyData.cs - DeleteItem() - ������ ���� ���� {deleteItem.itemID}");
            return -1001; // ������ ���� �����ڵ� ��ȯ.
        }
        else
        {
            // ���� ������ �����Ϸ��� �������� ���� ���
            if (items[index].amount < deleteItem.amount)
            {
                Debug.Log($"InventoryData.cs - DeleteITem() - ������ ���� ���� {deleteItem.itemID}");
                return -1002; // ������ ���� ���� �ڵ� ��ȯ.
            }
            // �����Ϸ��� �������� ������ �����ϰ� �ִ� �������� �������� ���� ���(amonut�� ����)
            else
            {
                items[index].amount -= deleteItem.amount; // ������ �� �ٿ��ֱ�
                if(items[index].amount <= 0) // ���� ���� �������� ������ 0���϶��
                {
                    items.RemoveAt(index); // ������ ����Ʈ���� ����
                    curItemCount--; // ���� ���� �����۰��� ����
                }
            }
        }
        return 0; // ���������� ���� ����.
    }

    // �̹� �����ϰ� �ִ� �������� ������ ����(����)�ϱ� ���� �޼ҵ�
    // ex) ���������� ���׷��̵�� �������� ���� ������ �����ϴ� Table�� ID���� �������ִ� �޼ҵ�.   
    public void UpdateItemInfo(InventoryItemData data)
    {
        foreach(var item in items)
        {
            if(item.uID == data.uID)
            {
                item.itemID += 1; // 1�� �����ϴ� ��� ���� ��ȭ �������� itemID�� ����.
            }
        }
    }

    // �κ��丮 ���� ���� ����
    public bool IsFull()
    {
        // ���� �����۰����� �ִ� �����۽��԰������� ũ�ų� ������
        return curItemCount >= maxItemSlot;
    }

    // ex) �������� ���������� ���Žÿ� �κ��丮�� ���������� �ִٸ� amount�� �÷��ָ� ������
    // �װ� �ƴ϶�� �̹����� �������־����.
    // �� �κ��丮 ���� ���� tableID�� �������� �ִٸ�, ���° Index�� �ִ��� Ȯ�� �� �������ִ� �޼ҵ�
    private int FindItemIndex(InventoryItemData newItem)
    {
        for(int i = items.Count - 1; i >= 0; i--)
        {
            if (items[i].itemID == newItem.itemID) // ������ ������ �������� ã�����
            {
                return i;
            }
        }
        return -1;
    }

    // �߰������� ���������� ��� : ������ ���� ���, ���� ��ġ ���� ���.
}

