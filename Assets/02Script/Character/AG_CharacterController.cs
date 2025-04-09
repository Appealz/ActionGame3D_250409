using UnityEngine;

public class AG_CharacterController : MonoBehaviour
{
    #region _SerializeField_
    [SerializeField] private float moveSpeed = 6f;
    #endregion

    #region _Private_
    private Vector3 moveDelta; // 이동 변화량(계산을 위한 필드값)
    private CharacterController controller;
    private Animator anims;
    // 캐릭터 이동은 카메라가 보고있는 방향에 따라 이동하도록 설정.
    private Vector3 camForward;
    private Vector3 camRight;

    #endregion

    #region _Public_
    // 위 아래는 같은 문법
    public Animator Anims => anims ??= transform.GetComponentInChildren<Animator>();
    public CharacterController Controller => controller ??= transform.GetComponentInChildren<CharacterController>();
    //public Animator Anims
    //{
    //    get
    //    {
    //        if(anims == null)
    //        {
    //            TryGetComponent<Animator>(out anims);                
    //        }
    //        return anims;
    //    }
    //}

    //public CharacterController Controller
    //{
    //    get
    //    {
    //        if(controller == null)
    //        {
    //            TryGetComponent<CharacterController>(out controller);
    //        }
    //        return controller;
    //    }
    //}    
    #endregion

    #region _AnimHash_
    public static int animsParams_IsWalk = Animator.StringToHash("IsMove");
    public static int animsParams_Attack01 = Animator.StringToHash("FlyAttack01");
    public static int animsParams_Attack02 = Animator.StringToHash("FlyAttack02");
    public static int animsParams_Attack03 = Animator.StringToHash("FlyAttack03");
    #endregion

    private void Update()
    {
        moveDelta.x = Input.GetAxisRaw("Horizontal");
        moveDelta.y = 0f;
        moveDelta.z = Input.GetAxisRaw("Vertical");
        
        // 카메라의 y값은 0으로 무시할것임.
        camForward = Camera.main.transform.forward; // 카메라의 forward값을 받아옴.
        camForward.y = 0f;

        camRight = Camera.main.transform.right;
        camRight.y = 0f;

        // 코드 분석해보기
        moveDelta = camForward * moveDelta.z + camRight * moveDelta.x;
        moveDelta.Normalize();

        // 방향전환
        if(moveDelta.magnitude > 0.001f) // moveDelta의 크기가 0.001보다 크다면 즉, 이동이 발생했다면
        {
            transform.forward = moveDelta; // x축이던 z축이던 상관없이 캐릭터를 회전시키는 작업.
            Anims.SetBool(animsParams_IsWalk, true); // 애니메이션 처리
        }
        else
        {
            Anims.SetBool(animsParams_IsWalk, false); // 애니메이션 처리
        }

        // 이동
        Controller.Move(moveDelta * (moveSpeed * Time.deltaTime)); // 실제 이동 처리
    }
}
