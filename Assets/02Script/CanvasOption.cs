using UnityEngine;
using UnityEngine.UI;

public class CanvasOption : MonoBehaviour
{
    private void Awake()
    {
        if(TryGetComponent<Canvas>(out Canvas canvas))
        {
            canvas.renderMode = RenderMode.ScreenSpaceCamera; // 시작시 스크린스페이스의 카메라로 변경
            canvas.worldCamera = Camera.main; // 지정된 카메라는 메인카메라로
            canvas.planeDistance = 0.4f;
        }

        if(TryGetComponent<CanvasScaler>(out CanvasScaler scaler))
        {
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);
        }
    }
}