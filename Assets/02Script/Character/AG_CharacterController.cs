using UnityEngine;

public class AG_CharacterController : MonoBehaviour
{
    #region _SerializeField_
    [SerializeField] private float moveSpeed = 6f;
    #endregion

    #region _Private_
    private Vector3 moveDelta; // �̵� ��ȭ��(����� ���� �ʵ尪)
    private CharacterController controller;
    private Animator anims;
    // ĳ���� �̵��� ī�޶� �����ִ� ���⿡ ���� �̵��ϵ��� ����.
    private Vector3 camForward;
    private Vector3 camRight;

    #endregion

    #region _Public_
    // �� �Ʒ��� ���� ����
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
        
        // ī�޶��� y���� 0���� �����Ұ���.
        camForward = Camera.main.transform.forward; // ī�޶��� forward���� �޾ƿ�.
        camForward.y = 0f;

        camRight = Camera.main.transform.right;
        camRight.y = 0f;

        // �ڵ� �м��غ���
        moveDelta = camForward * moveDelta.z + camRight * moveDelta.x;
        moveDelta.Normalize();

        // ������ȯ
        if(moveDelta.magnitude > 0.001f) // moveDelta�� ũ�Ⱑ 0.001���� ũ�ٸ� ��, �̵��� �߻��ߴٸ�
        {
            transform.forward = moveDelta; // x���̴� z���̴� ������� ĳ���͸� ȸ����Ű�� �۾�.
            Anims.SetBool(animsParams_IsWalk, true); // �ִϸ��̼� ó��
        }
        else
        {
            Anims.SetBool(animsParams_IsWalk, false); // �ִϸ��̼� ó��
        }

        // �̵�
        Controller.Move(moveDelta * (moveSpeed * Time.deltaTime)); // ���� �̵� ó��
    }
}
