using UnityEngine;

public class Fielditem : MonoBehaviour
{
    public int FieldItemCode;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("������ ȹ�� �Ҹ� �����!");

            if (!Managers.ItemInventory.AddItem(FieldItemCode, gameObject.transform))
            {
                GameObject item = Managers.Resource.Instantiate($"Fielditem{Managers.ItemInventory.TempItemCode}");
                item.SetRandomPositionSphere();
                Debug.Log("������ ���󰡴� �Ҹ� �� ����Ʈ ����2");
                item.AddComponent<Fielditem>().FieldItemCode = Managers.ItemInventory.TempItemCode;
            }
            Managers.Resource.Destroy(gameObject);
        }
    }


}
