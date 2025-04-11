using System;
using System.IO;
using UnityEngine;

// �÷��̾� ������ ����
[Serializable]
public class PlayerData
{
    // ���� ĳ������ �ǽð� ���� ����
    public string nickName;
    public int level;
    public int curEXP;
    public int curHP;
    public int MaxHP;
    public int curMP;
    public int maxMP;
    public int gold;
    public int uidCounter; // ������ �������� �����Ҷ� ���� ���̵� ������ֱ� ���� ������ 1�� �����ϵ��� ����.
    public InventoryData inventory; // ������ �����̱� ������ ��ü �����Ŀ� ��� ����.
}


// ���� å�� ��Ģ ��Ű�� ���� ����.
/// <summary>
/// 1. ���� �����͸� ����(����ġ, ����, ���, �κ��丮 ��)
/// 2. �� �ε�
/// 3. ���̺�/�ε� => PlayerPrefs�� �̿����� �ʰ� ���Ϸ� �����ؼ� ����.
/// </summary>
public class GameManager : Singleton<GameManager>
{
    private PlayerData playerData;
    public PlayerData PlayerData
    {
        get => playerData;
    }

    private string dataPath;

    protected override void DoAwake()
    {
        base.DoAwake();
        // => �÷��̰� �Ǵ� �ϵ����
        // PC
        // �����
        // �ܼ�
        // � �÷������� ������ �ƴ����� ���� ���� �÷������� ����Ҹ� �����ϱ� ���ؼ�
        dataPath = Application.persistentDataPath + "/Save"; // Save��� ������ �����ؼ� ��� ����.
    }

    // ������ ���� ������ ���� �޼ҵ�
    public void CreateUserData(string newNickName)
    {
        playerData = new PlayerData();
        playerData.nickName = newNickName;
        playerData.level = 1;
        playerData.curEXP = 0;
        playerData.curHP = playerData.MaxHP = 100;
        playerData.curMP = playerData.maxMP = 50;
        playerData.gold = 10000;
        playerData.uidCounter = 0;
        playerData.inventory = new InventoryData();        
    }
    
    public int ItemUIDMaker
    {
        get => ++playerData.uidCounter; // ���� �б����� uidCounter�� ������Ŵ.(������ �������� 21�ﰳ �̻� �����ϱ� �������� �ߺ��Ǵ� ���� �ȸ������)
    }


    public void Test()
    {
        CreateUserData("HIPlayer");
        SaveData();

        InventoryItemData testItem = new InventoryItemData();
        testItem.itemID = 1001;
        testItem.amount = 3;
        LootingItme(testItem);

        Debug.Log(playerData.inventory.CurItemCount);
    }


    #region _Save&Load_
    public void SaveData()
    {
        // JSON ������ ���ؼ� �����͸� �����ϰ� �ε�
        // ���� ��Ÿ�� ���߿��� ���α׷����� �����͸� ����������
        // ���α׷��� ����ǰԵǸ� �����Ͱ� ��� ���ư��Ե� => �̸� �����ϱ� ���ؼ� JSON������ ���� ����.
        string data = JsonUtility.ToJson(playerData);

        Debug.Log(data);

        File.WriteAllText(dataPath, data);
    }
    public bool LoadData()
    {
        if(File.Exists(dataPath)) // �ش��ϴ� ��ο� �����Ͱ� �ִ��� ������ �������ִ� ����
        {
            string data = File.ReadAllText(dataPath); // �о���̴� �۾�.
            playerData = JsonUtility.FromJson<PlayerData>(data);
            return true;
        }
        return false;
    }
    public void DeleteData()
    {
        File.Delete(dataPath); // �ش� ���� ����
    }

    // �÷��̾� ������ �ҷ�����.
    public bool TrygetPlayerData()
    {
        if(File.Exists(dataPath))
        {
            return LoadData();
        }
        return false;
    }
    #endregion

    // �÷��̾ �������� ����.
    #region _ItemLooting_
    public bool LootingItme(InventoryItemData newItem)
    {
        Debug.Log($"{newItem.itemID} ������ ����");
        
        if(!PlayerData.inventory.IsFull())
        {
            PlayerData.inventory.AddItem(newItem);
            return true;
        }
        return false;
    }

    #endregion

}
