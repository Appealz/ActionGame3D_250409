using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // �÷��̾ �����ϵ��� �����

    [SerializeField] private Transform targetTrans;
    // ī�޶�� ���� �Ÿ��� �ΰ� �i�ư�����(offset)
    [SerializeField] private Vector3 offset = new Vector3(0f, 10f, -10f);

    private GameObject targetObj;

    private void Awake()
    {
        if(targetTrans == null) // Inspectorâ���� ���õ��� �ʾ������� ��츦 ���
        {
            targetObj = GameObject.Find("MainPlayer");
            if (targetObj != null)
            {
                targetTrans = targetObj.transform;
            }
        }
    }

    // LateUpdate�� ����ϴ� ������ ĳ���Ͱ� �̵��� �ڿ� ī�޶� ���󰡾��ϱ� ����.
    private void LateUpdate()
    {
        if(targetTrans != null)
        {
            transform.position = targetTrans.position + offset;
        }
    }

}
