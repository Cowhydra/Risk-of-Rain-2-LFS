using UnityEngine;

public class bonfire : MonoBehaviour
{
    public float jumpForce = 5f; // ���� ��

    private Rigidbody playerRigidbody; // �÷��̾��� Rigidbody ������Ʈ

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("test"))
        {
            JumpToCube2();
        }
    }

    private void JumpToCube2()
    {
        // cube2���� ���� ���� ����
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
