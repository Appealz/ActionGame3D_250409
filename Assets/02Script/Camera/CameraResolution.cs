using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    // viewport rect를 조정

    private void Awake()
    {
        if(TryGetComponent<Camera>(out Camera cam))
        {
            // rect가 뷰포트 rect의 정보임.
            Rect rt = cam.rect;
            float scale_Height = ((float)Screen.width / Screen.height) / ((float)16 / 9);
            float scale_Width = 1f / scale_Height;

            if(scale_Height < 1f)
            {
                rt.height = scale_Height;
                rt.y = (1f - scale_Height) / 2f;
            }
            else
            {
                rt.width = scale_Width;
                rt.x = (1f - scale_Width) / 2f;
            }
            cam.rect = rt;
        }
    }
}
