using System.Collections;
using System.Linq;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    public int Itemprice;
    ItemContainerPriceUI itempriceUI;
    public int _containItemCode;
    private bool _Isuse = false;

    [SerializeField] private Transform itemmodeltrans;
    private void Start()
    {
        Itemprice = Random.Range(25, 50) + (1 + (int)Managers.Game.Difficulty) * Random.Range(1, 10);
        itempriceUI = Managers.UI.MakeWorldSpaceUI<ItemContainerPriceUI>();
        itempriceUI.itemPrice = Itemprice;
        itempriceUI.transform.SetParent(transform);


        _containItemCode = Managers.ItemInventory.Items.Keys.ToList()[Random.Range(0, Managers.ItemInventory.Items.Count)];

        //�𵨰� ���� ������ ������ ���� �սô�.
        //���� �׳� ��Ŀ����� ���ΰ� ���� �����ۿ� �ݸ����� �ٿ��� ȹ�� �� �κ��丮 �߰� �ϴ� �������


        if (itemmodeltrans != null)
        {
            GameObject itemmodel = Managers.Resource.Instantiate($"item{_containItemCode}");
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
        if (other.CompareTag("Player") && !_Isuse)
        {
            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionIn, this); //���� UI
            if (Managers.Game.Gold > Itemprice)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
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
                            itemContainer._Isuse = true;
                            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionOut, itemContainer);
                            itemContainer.itempriceUI.gameObject.SetActive(false);
                        }
                    }
                    #endregion
                    //��� ���� �� �ߺ� ���� ������ ���� �Ұ� Ȯ��
                    Managers.Game.Gold -= Itemprice;
                    _Isuse = true;
                    itempriceUI.gameObject.SetActive(false);
                    //������ ���� => �������� ������ �ڽ��� ���� ��� ���� �ƴ϶� �ʵ忡 ������ �������� �ݴ� ����
                    GameObject _item = Managers.Resource.Instantiate($"Fielditem{_containItemCode}");

                    GameObject _ItemEffect = Managers.Resource.Instantiate("ItemOutEffect");
                    _ItemEffect.transform.position = gameObject.transform.position;
                    _ItemEffect.GetComponent<ItemOutEffect>()._targetPosition = _item.SetRandomPositionSphere(3, 8, 2, gameObject.transform);

                    if (TryGetComponent(out Animator _animoator))
                    {
                        _animoator.SetTrigger("Open");
                    }
                    Debug.Log("������ ���󰡴� �Ҹ� �� ����Ʈ ����1 ");
                    _item.AddComponent<Fielditem>().FieldItemCode = _containItemCode;
                }
            }
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
}