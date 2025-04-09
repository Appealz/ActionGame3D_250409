using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // 플레이어를 추적하도록 만들것

    [SerializeField] private Transform targetTrans;
    // 카메라는 일정 거리를 두고 쫒아갈것임(offset)
    [SerializeField] private Vector3 offset = new Vector3(0f, 10f, -10f);

    private GameObject targetObj;

    private void Awake()
    {
        if(targetTrans == null) // Inspector창에서 세팅되지 않았을때의 경우를 대비
        {
            targetObj = GameObject.Find("MainPlayer");
            if (targetObj != null)
            {
                targetTrans = targetObj.transform;
            }
        }
    }

    // LateUpdate를 사용하는 이유는 캐릭터가 이동한 뒤에 카메라가 따라가야하기 때문.
    private void LateUpdate()
    {
        if(targetTrans != null)
        {
            transform.position = targetTrans.position + offset;
        }
    }

}
