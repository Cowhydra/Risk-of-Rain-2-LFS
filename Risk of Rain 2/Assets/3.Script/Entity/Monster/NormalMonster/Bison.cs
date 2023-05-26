using System.Collections;
using UnityEngine;

public class Bison : MonoBehaviour
{
    Transform targets;
    Animator animator;
    float enemyMoveSpeed = 10f;

    private Rigidbody _enemyRigidbody;
    private Vector3 previousPosition;

    private void Awake()
    {
        TryGetComponent(out _enemyRigidbody);
        TryGetComponent(out animator);
    }

    private void Start()
    {
        StartCoroutine(UpdatetargetCoroutine());
        previousPosition = transform.position;

    }
    private IEnumerator UpdatetargetCoroutine()
    {
        while (true)
        {
            UpdateTarget();
            yield return new WaitForSeconds(0.25f);
        }
    }

    private void UpdateTarget()
    {
        Collider[] Cols = Physics.OverlapSphere(transform.position, 10f);
        Debug.Log(Physics.OverlapSphere(transform.position, 10f, 1 << 8));
        if (Cols.Length > 0)
        {
            float distance = Mathf.Infinity;
            for (int i = 0; i < Cols.Length; i++)
            {
                if (Cols[i].tag == "Player")
                {
                    Debug.Log("Physic Enemy : Target found");
                    float newDistance = Vector3.Distance(transform.position, Cols[i].transform.position);
                    if (newDistance < distance)
                    {
                        distance = newDistance;
                        targets = Cols[i].gameObject.transform;
                    }
                }
            }
        }
    }
    private void Update()
    {
        if (targets != null)
        {
            targets.position = new Vector3(targets.position.x, transform.position.y, targets.position.z);
            Vector3 dir = targets.position - transform.position;

            // ���� ��� ������ �Ÿ� ���
            float distanceToTarget = Vector3.Distance(transform.position, targets.position);

            if (distanceToTarget <= 2f) // �÷��̾ ���� ���� ���� �ִ��� Ȯ��
            {
                animator.SetBool("Run", false);
                animator.SetBool("Attack", true);
            }
            else if (distanceToTarget <= 10f) // �÷��̾ �ν� ���� ���� �ִ��� Ȯ��
            {
                animator.SetBool("Run", true);
                animator.SetBool("Attack", false);
            }
            else
            {
                animator.SetBool("Run", false);
                animator.SetBool("Attack", false);
            }


            animator.SetBool("Run", true);

            // SmoothDamp�� ����� ��ġ ����
            Vector3 targetPosition = previousPosition + dir.normalized * enemyMoveSpeed * Time.deltaTime;
            //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);

            _enemyRigidbody.MovePosition(transform.position);

            // ������ ȸ��
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothTime);

            // ��ġ ������ ���� ���� ��ġ ������Ʈ
            previousPosition = transform.position;

        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            targets.position = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);
            transform.LookAt(targets.position);
        }
    }

}
