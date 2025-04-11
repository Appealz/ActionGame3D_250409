using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���̺� ������ �ε�, �ڷᱸ�� �Ľ�
/// </summary>
public class TableDataManager : Singleton<TableDataManager>
{
    // ���̺� �����ʹ� ������ ������ ���� �ʿ� �����Ƿ� �̱������� ����

    private bool loadData = false; // ���� �ε� �ߴ��� ���ߴ��� ���� üũ
    private ActionGame originalTable; // ActionGame ��ũ���ͺ� ������Ʈ. �������� ������
    // ���� �������� �����ʹ� List�� ����.
    // ���� �����ؾ��ϴ� �����͵��� Dictionary�� �籸��.
    private Dictionary<int, ItemData_Entity> dicItemDatas = new Dictionary<int, ItemData_Entity>();
    private Dictionary<int, Monster_Entity> dicMonsterDatas = new Dictionary<int, Monster_Entity>();

    protected override void DoAwake()
    {
        base.DoAwake();
        InitManager();
    }

    // �� �Ŵ����� ��ü�� ó�� ��������� ����
    private void InitManager()
    {
        if(!loadData)
        {
            originalTable = Resources.Load<ActionGame>("ActionGame"); // ���ҽ����Ͽ� �ִ� �����������ϴ� Ÿ�԰� ���ϸ�
            //Resources.Load<Sprite>("���ϸ�"); => �̿Ͱ��� Ÿ�԰� ���ϸ����� �ε尡��.

            // ����Ʈ�� �ִ� �����۵��� ��ųʸ��� �籸��
            foreach(var row in originalTable.ItemData)
            {
                dicItemDatas.Add(row.id, row); 
            }

            foreach(var row in originalTable.MonsterData)
            {
                dicMonsterDatas.Add(row.id, row);
            }

            loadData = true; // ��� ���������� �ε�Ǿ����Ƿ� true�� ����.
        }
    }


    // ĸ��ȭ : �ܺο����� Dictionary�� �����ؼ� ���ο� �����͸� �߰�, ����, ���Ŵ� ���ϰ� �ܺο��� �о �� �� �ֵ��� ����.
    public bool GetItemData(int keyID, out ItemData_Entity itemData)
    {
        return dicItemDatas.TryGetValue(keyID, out itemData);
    }

    public bool GetMonsterData(int keyID, out Monster_Entity monsterData)
    {
        return dicMonsterDatas.TryGetValue(keyID, out monsterData);
    }
}
