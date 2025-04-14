using System.Collections.Generic;
using UnityEngine;

// 리소스 폴더에서 아이콘을 로드해오는 역할
// 1. 게임 시작시 모든 아이콘 동적 로드
// 2. 사용된 아이콘들만 동적 로드하여 캐싱해서 사용.(DP사용)
public class IconLoadManager : Singleton<IconLoadManager>
{
    // 게임에 따라 유저가 가지고 있을만한 아이콘들을 전부 게임시작할 때 로드(폴더 전체를) => 이렇게 하기도함
    // 여기서는 인벤토리를 열때 처음 아이콘을 불러오고 DP를 사용하여 이전에 가져온 이미지를 캐싱해둔뒤 사용.

    private Dictionary<int, Sprite> iconCache = new Dictionary<int, Sprite>();

    public Sprite GetIcon(int itemID)
    {
        if(iconCache.TryGetValue(itemID, out var cachedIcon))
        {
            return cachedIcon;
        }

        // 딕셔너리에 없다면 동적로딩 진행

        if(TableDataManager.Instance.GetItemData(itemID, out ItemData_Entity itemData))
        {
            Sprite icon = Resources.Load<Sprite>(itemData.iconImg); // iconImg는 테이블에있는 아이콘 경로.
            if(icon != null)
            {
                iconCache[itemID] = icon;
                return icon;
            }
        }
        else
        {
            Debug.Log($"TableInfo is not existed. {itemID}");
        }
        return null;
    }

    // 캐시 정리
    public void ClaerCache()
    {
        iconCache.Clear();
    }


}