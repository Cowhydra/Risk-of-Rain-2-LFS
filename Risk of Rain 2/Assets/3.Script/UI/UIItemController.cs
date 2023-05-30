using UnityEngine;

public class UIItemController : MonoBehaviour
{
    private GameObject tagertObject; // ť��
    [SerializeField] private float rotateSpeed;

    private void Start()
    {
        tagertObject = gameObject.transform.parent.gameObject;
        // ť���� ũ�� ����
        Vector3 cubeSize = GetObjectSize(tagertObject);

        // ���� ��ü�� ũ�� ����
        SetObjectSize(gameObject, cubeSize);
        rotateSpeed = 50.0f;
        Debug.DrawRay(gameObject.transform.position, 15000 * Vector3.forward, Color.red);
        gameObject.transform.GetChild(0).transform.localPosition = Vector3.zero;
    }
    private void Update()
    {

        transform.RotateAround(transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
        // gameObject.transform.Rotate(Vector3.up,Time.deltaTime*rotateSpeed); 
    }
    // ��ü�� ũ�� ����
    private Vector3 GetObjectSize(GameObject obj)
    {
        Renderer renderer = obj.GetComponentInChildren<Renderer>();
        Vector3 size = renderer.bounds.size;
        return size;
    }

    // ��ü�� ũ�� ����
    private void SetObjectSize(GameObject obj, Vector3 size)
    {
        Vector3 originalSize = GetObjectSize(obj);
        Vector3 scale = obj.transform.localScale;

        scale.x *= size.x / originalSize.x;
        scale.y *= size.y / originalSize.y;
        scale.z *= size.z / originalSize.z;

        obj.transform.localScale = scale;

        // Collider ������Ʈ ��������
        Collider collider = obj.GetComponent<Collider>();
        if (collider != null)
        {
            // Collider�� ũ�� ����
            if (collider is BoxCollider boxCollider)
            {
                boxCollider.size = size;
            }
            // �ٸ� ������ Collider�� �߰����� ó�� �ʿ�
            else if (collider is SphereCollider sphereCollider)
            {
                // SphereCollider�� ũ�� ����
            }
            else if (collider is CapsuleCollider capsuleCollider)
            {
                // CapsuleCollider�� ũ�� ����
            }
            // ...
        }
    }

}
