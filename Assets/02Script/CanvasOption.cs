using UnityEngine;
using UnityEngine.UI;

public class CanvasOption : MonoBehaviour
{
    private void Awake()
    {
        if(TryGetComponent<Canvas>(out Canvas canvas))
        {
            canvas.renderMode = RenderMode.ScreenSpaceCamera; // ���۽� ��ũ�������̽��� ī�޶�� ����
            canvas.worldCamera = Camera.main; // ������ ī�޶�� ����ī�޶��
            canvas.planeDistance = 0.4f;
        }

        if(TryGetComponent<CanvasScaler>(out CanvasScaler scaler))
        {
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);
        }
    }
}