using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 테이블 데이터 로드, 자료구조 파싱
/// </summary>
public class TableDataManager : Singleton<TableDataManager>
{
    // 테이블 데이터는 여러개 가지고 있을 필요 없으므로 싱글톤으로 구현

    private bool loadData = false; // 동적 로딩 했는지 안했는지 여부 체크
    private ActionGame originalTable; // ActionGame 스크립터블 오브젝트. 오리지널 데이터
    // 현재 오리지널 데이터는 List로 구성.
    // 자주 접근해야하는 데이터들을 Dictionary로 재구성.
    private Dictionary<int, ItemData_Entity> dicItemDatas = new Dictionary<int, ItemData_Entity>();
    private Dictionary<int, Monster_Entity> dicMonsterDatas = new Dictionary<int, Monster_Entity>();

    protected override void DoAwake()
    {
        base.DoAwake();
        InitManager();
    }

    // 이 매니저는 객체가 처음 만들어질때 실행
    private void InitManager()
    {
        if(!loadData)
        {
            originalTable = Resources.Load<ActionGame>("ActionGame"); // 리소스파일에 있는 가져오고자하는 타입과 파일명
            //Resources.Load<Sprite>("파일명"); => 이와같이 타입과 파일명으로 로드가능.

            // 리스트에 있는 아이템들을 딕셔너리로 재구성
            foreach(var row in originalTable.ItemData)
            {
                dicItemDatas.Add(row.id, row); 
            }

            foreach(var row in originalTable.MonsterData)
            {
                dicMonsterDatas.Add(row.id, row);
            }

            loadData = true; // 모두 정상적으로 로드되었으므로 true로 변경.
        }
    }


    // 캡슐화 : 외부에서는 Dictionary에 접근해서 새로운 데이터를 추가, 수정, 제거는 못하고 외부에서 읽어만 갈 수 있도록 구성.
    public bool GetItemData(int keyID, out ItemData_Entity itemData)
    {
        return dicItemDatas.TryGetValue(keyID, out itemData);
    }

    public bool GetMonsterData(int keyID, out Monster_Entity monsterData)
    {
        return dicMonsterDatas.TryGetValue(keyID, out monsterData);
    }
}
