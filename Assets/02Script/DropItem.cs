using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class DropItem : MonoBehaviour
{
    private SphereCollider col;
    private Rigidbody rb;
    private bool isDrop;
    private Vector3 pos;
    private Transform rotTrans;
    private float dropPosY;
    private float valueA;

    private void Awake()
    {
        if(TryGetComponent<SphereCollider>(out col))
        {
            col.radius = 0.5f;
            col.isTrigger = true;
        }
        if(TryGetComponent<Rigidbody>(out rb))
        {
            rb.useGravity = true;
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }

        rotTrans = transform.GetChild(0);
        valueA = 0f;
        
        isDrop = false;
    }

    private void Update()
    {
        if(isDrop)
        {
            rotTrans.Rotate(Vector3.up * (90f * Time.deltaTime));
            pos = rotTrans.position;
            valueA += Time.deltaTime;
            pos.y = dropPosY + 0.3f * Mathf.Sin(valueA);
            rotTrans.position = pos;
        }
    }

    [SerializeField] int dropID = 2002;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ground"))
        {
            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero;
            isDrop = true;
            dropPosY = rotTrans.position.y;
        }

        if(isDrop && other.CompareTag("Player"))
        {
            InventoryItemData newData = new InventoryItemData();

            // todo : 몬스터가 드랍할 때 아이템의 종류가 결정되도록 개선.
            newData.itemID = dropID;
            newData.amount = 1;

            if(GameManager.Instance.LootingItme(newData))
            {
                Destroy(gameObject);
            }
        }
    }

}
