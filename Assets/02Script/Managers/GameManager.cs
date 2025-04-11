using System;
using System.IO;
using UnityEngine;

// 플레이어 데이터 관리
[Serializable]
public class PlayerData
{
    // 유저 캐릭터의 실시간 정보 관리
    public string nickName;
    public int level;
    public int curEXP;
    public int curHP;
    public int MaxHP;
    public int curMP;
    public int maxMP;
    public int gold;
    public int uidCounter; // 유저가 아이템을 습득할때 고유 아이디를 만들어주기 위한 값으로 1씩 증가하도록 구현.
    public InventoryData inventory; // 참조형 변수이기 떄문에 객체 생성후에 사용 가능.
}


// 단일 책임 원칙 지키지 않을 것임.
/// <summary>
/// 1. 유저 데이터를 관리(경험치, 레벨, 골드, 인벤토리 등)
/// 2. 씬 로딩
/// 3. 세이브/로드 => PlayerPrefs를 이용하지 않고 파일로 생성해서 저장.
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
        // => 플레이가 되는 하드웨어
        // PC
        // 모바일
        // 콘솔
        // 어떤 플랫폼에서 실행이 됐는지에 따라서 각각 플랫폼별로 저장소를 접근하기 위해서
        dataPath = Application.persistentDataPath + "/Save"; // Save라는 폴더를 생성해서 경로 생성.
    }

    // 최초의 유저 데이터 생성 메소드
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
        get => ++playerData.uidCounter; // 값을 읽기전에 uidCounter를 증가시킴.(유저가 아이템을 21억개 이상 습득하기 전까지는 중복되는 값이 안만들어짐)
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
        // JSON 포맷을 통해서 데이터를 저장하고 로드
        // 게임 런타임 도중에는 프로그램에서 데이터를 관리하지만
        // 프로그램이 종료되게되면 데이터가 모두 날아가게됨 => 이를 방지하기 위해서 JSON포맷을 통해 저장.
        string data = JsonUtility.ToJson(playerData);

        Debug.Log(data);

        File.WriteAllText(dataPath, data);
    }
    public bool LoadData()
    {
        if(File.Exists(dataPath)) // 해당하는 경로에 데이터가 있는지 없는지 검증해주는 로직
        {
            string data = File.ReadAllText(dataPath); // 읽어들이는 작업.
            playerData = JsonUtility.FromJson<PlayerData>(data);
            return true;
        }
        return false;
    }
    public void DeleteData()
    {
        File.Delete(dataPath); // 해당 파일 삭제
    }

    // 플레이어 데이터 불러오기.
    public bool TrygetPlayerData()
    {
        if(File.Exists(dataPath))
        {
            return LoadData();
        }
        return false;
    }
    #endregion

    // 플레이어가 아이템을 습득.
    #region _ItemLooting_
    public bool LootingItme(InventoryItemData newItem)
    {
        Debug.Log($"{newItem.itemID} 아이템 습득");
        
        if(!PlayerData.inventory.IsFull())
        {
            PlayerData.inventory.AddItem(newItem);
            return true;
        }
        return false;
    }

    #endregion

}
