using System.Collections;
using System.Linq;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    public int Itemprice;
    ItemContainerPriceUI itempriceUI;
    public int ContainItemCode;
    private bool isUse = false;

    [SerializeField] private Transform itemmodeltrans;
    private void Start()
    {
        Itemprice = Random.Range(25, 50) + (1 + (int)Managers.Game.Difficulty) * Random.Range(1, 10);
        itempriceUI = Managers.UI.MakeWorldSpaceUI<ItemContainerPriceUI>();
        itempriceUI.itemPrice = Itemprice;
        itempriceUI.transform.SetParent(transform);


        ContainItemCode = Managers.ItemInventory.Items.Keys.ToList()[Random.Range(0, Managers.ItemInventory.Items.Count)];

        //�𵨰� ���� ������ ������ ���� �սô�.
        //���� �׳� ��Ŀ����� ���ΰ� ���� �����ۿ� �ݸ����� �ٿ��� ȹ�� �� �κ��丮 �߰� �ϴ� �������


        if (itemmodeltrans != null)
        {
            GameObject itemmodel = Managers.Resource.Instantiate($"item{ContainItemCode}");
            itemmodel.transform.parent = itemmodeltrans.transform;
            itemmodel.transform.localPosition = Vector3.up * 0.5f;
            itemmodel.AddComponent<UIItemController>();
        }



    }

    //������ �����̳�
    // �÷��̾ ���� -> ���� ��尡 ���ٸ� E Ű�� ������
    // ������ ���� -> �ݶ��̴� ���� -> �̺�Ʈ �߼�
    // ���� �������� �ʰ� Triger Exit �ص� �ټ�
    //�ٵ� Trigger Vs COllsion

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !isUse)
        {
            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionIn, this); //���� UI
            if (Managers.Game.Gold > Itemprice)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //��� ���� �� �ߺ� ���� ������ ���� �Ұ� Ȯ��
                    Managers.Game.Gold -= Itemprice;
                    isUse = true;
                    itempriceUI.gameObject.SetActive(false);

                    //���ӿ� �����ϴ� 3����  ���ϴ� �� ���°�..

                    #region 3��¥�� �����̳� �ϴ� 1���� ȹ�� �����ϰ� ����
                    if (transform.parent == null)
                    {
                        Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionOut, this);
                    }
                    else
                    {
                        ItemContainer[] itemContainers = transform.parent.GetComponentsInChildren<ItemContainer>();
                        foreach (ItemContainer itemContainer in itemContainers)
                        {
                            itemContainer.isUse = true;
                            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionOut, itemContainer);
                            itemContainer.itempriceUI.gameObject.SetActive(false);
                        }
                    }

                    #endregion

                    //������ ���� => �������� ������ �ڽ��� ���� ��� ���� �ƴ϶� �ʵ忡 ������ �������� �ݴ� ����
                    GameObject item = Managers.Resource.Instantiate($"Fielditem{ContainItemCode}", gameObject.SetItemSpawnPosition());
                    item.AddComponent<Fielditem>().FieldItemCode = ContainItemCode;
                }
            }
            Debug.Log("��ȣ�ۿ� Ű�� ������ �������� ȹ�� �� �ƴ϶� ���� ");
            Debug.Log("���ڷκ��� ���� ��ġ ���� �ε�, ���߿� �����̰� ����Ʈ�� ������ ���� �� �ڽ� ������Ʈ ���� ��" +
                "���������� �����ؼ� �� ������ ������? ");
            Debug.Log("�������� �����ǰ�, �������� Collider�� ������ �־ �ش� ������ Collider�� ���� �������� ȹ��");
        }
    }


    private void OnTriggerExit(Collider other)
    {
        //StartCoroutine(nameof(CollisionExitEvent_co));
        Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionOut, this);
    }

    IEnumerator CollisionExitEvent_co()
    {
        yield return new WaitForSeconds(0.5f);
        Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionOut, this);
    }


    //���� ���� ��ġ �������ִ� ����ε� ���� ����� ���ʿ� ���� Ȯ��޼���ȭ ������ GameObject���� ��� ����� �����ϵ���
    //private Vector3 SetItemSpawnPosition()
    //{
    //    Debug.Log("������ ���󰡴� �Ҹ� �� ����Ʈ ����");
    //    Collider collider = GetComponent<Collider>();
    //    Bounds colliderBounds = collider.bounds;
    //    Vector3 colliderSize = colliderBounds.size;
    //    Vector3 colliderCenter = colliderBounds.center;


    //    float angle = Random.Range(0f, Mathf.PI * 2f);

    //    // ������ �Ÿ��� ����
    //    float distance = Random.Range(5f, 10f);

    //    // �ﰢ�Լ��� ����Ͽ� ��ġ ���
    //    float xPos = transform.position.x + Mathf.Cos(angle) * distance;
    //    float zPos = transform.position.z + Mathf.Sin(angle) * distance;
    //    Vector3 CirclePos = new Vector3(xPos, transform.position.y, zPos);

    //    float additionalHeight = 5.0f;

    //    //���Ӱ� ���� ��������, X,Z ��ǥ���� ������ �ݰ�, ������ ���� ���� ��
    //    //Y ��(����)�� ���� ��ġ ( �ݸ��� �߽��� + ���� ��*(�ݸ��� ���� ���ڸ�  + �߰����� �������� �ö󰥳���)
    //    Vector3 newPosition = colliderCenter + CirclePos + Vector3.up * (colliderSize.y * 0.5f + additionalHeight);
    //    return newPosition;
    //}
}
