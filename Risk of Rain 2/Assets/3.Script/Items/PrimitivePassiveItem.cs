using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PrimitivePassiveItem : ItemPrimitiive
{

    /*
     1. ��� �������� Invoke() ������ ��� ó���� ���ΰ�?
          ==> ���� �÷��ִ� ���� �׳� �ڷ�ƾ �����ϸ� �Ǵµ� �����ϴ� ����?
     */




    //���ʽ� ���� ī��Ʈ...
    PlayerStatus currStartus;
    PlayerMovement playerMoveMent;
    private void Start()
    {
      currStartus=  Player.GetComponent<PlayerStatus>();
        playerMoveMent = Player.GetComponent<PlayerMovement>();
    }
    //�⺻������ �÷��̾� ���¿� ���� �ѹ��� ���� ��������ְ�, ���� �������� ȹ���� ��쿡�� �߰������� �ѹ��� ����
    //���� ������ ������ �־�� �ϰ�, �������� ȹ���� ��� ������ Ÿ���� ���� �÷��̾� ���°� ��ϳĿ� ����
    //���� �� ���������� ���� ������ ���°� �ٲ� ���� ��� �нú� ��ų���� ��ȸ�� �����̴ϱ�

    private void ExcuteSkill()
    {
        foreach (var itemkey in Managers.ItemInventory.WhenActivePassiveItem[Define.WhenItemActivates.Always].Keys)
        {

        }
       
    }
    private void StopSkill()
    {

    }

   
    //�нú��� �ؼ� ���� �нú갡 �ƴ�, �нú��ε� ��Ƽ������ �� ���� ��ų�� ���� �Ӹ� ����
    //�ϴ�.. ü�踦 ����Ƽ� �ϵ��ڵ� ������ ������ ���߿� ��ų �� ���� ���� �� �� ����ȭ �ؼ� ����?.. ��Ƴ�\
    //���� �׿��� �ߵ� ��Ű�� ��� ���� ��ġ�� ��� ��� �� �ϳ� ->  ���� �غ��鼭 �����غ���  �ܼ��ϰ� �����ε� ���?
    private void OnPassiveSkill(int itemcode,Vector3 spawnPos)
    {
        switch(itemcode)
        {
            case 1001:
                //���� 15% ��� -> �ϴ� �ӽ÷� �ִ�ü�� �÷����� �ʿ��Ѱ� ���� ������ ����
                currStartus.MaxHealth = currStartus._survivorsData.MaxHealth + 15 * Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[itemcode].WhenItemActive][itemcode].Count;
                break;  
            case 1002:
                //��� ���������̶� ���� ������Ʈ �ִ��� �ؾ��ҵ�? -> �� �ƴ϶� �׳� �÷��̾� Hit �κп� ������ ���� �Լ� �ϳ� ���� ��
                break;
            case 1003:
                StopCoroutine(nameof(Item1003_co));
                StartCoroutine(nameof(Item1003_co));
                //�÷��̾� ��ġ �������� ������ �� �� �޵�Ŷ ������ �ϳ� �������ְ� �ű⿡ ����� ����  (��)
                break;
            case 1004:
                
                //ġ��Ÿ ���� ���� �����Դϴ�.
                break;
            case 1005:
                // currStartus.MoveSpeed=currStartus._survivorsData.MoveSpeed*1.14* Managers.ItemInventory.WhenActivePassiveItem[Define.WhenItemActivates.Always][itemcode].Count;
                playerMoveMent._bonusMoveSpeed=1.14f * Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[itemcode].WhenItemActive][itemcode].Count;
                break;
            case 1006:
                //�ǵ� ȹ���ε� �ǵ� ���� �Ӽ��� ����
                //�ϴ�  ü�� �������� �־���µ� ü�� ���� 
                currStartus.Health+= 15 * Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[itemcode].WhenItemActive][itemcode].Count;
                break;
            case 1007:
                StopCoroutine(nameof(Item1007_co));
                StartCoroutine(nameof(Item1007_co));
                //���� �߻� �� �̻��� �߻� -> �켱 �÷��̾� �չ������� �������� ������ ����
                break;
            case 1008:
                //��� ��� �����ϴ� ��ũ��Ʈ ���� ���� óġ�� ��� 12m �ݰ濡 ��ϱ���� ���� �Ǿ� 350% ����
                //�ϴ� ���ظ� ������ �ڵ�� �ۼ��صξ��µ� ���� ��ġ�� �� ���� ��ġ�� ��ġ ���� ��� ��� ����?  �ƴϸ� ���� �̺�Ʈ�� �ߵ����Ѿ� ���� �𸣰���
                GameObject item1008= Managers.Resource.Instantiate("Item1008Skill");
                item1008.GetOrAddComponent<Item1008Skill>();
                item1008.transform.position = spawnPos;
                break;
            case 1009:
                playerMoveMent._bonusJumpCount += Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[itemcode].WhenItemActive][itemcode].Count;
                break;
            case 1010:
                currStartus.Health += 1 * Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[itemcode].WhenItemActive][itemcode].Count;
                //���ظ� ������ ���� ü���� 1 ȸ��... ��� ���� ���µ� ���� �ҵ� �ѵ�?.. --> ������Ʈ �����ͼ� ������Ƽ ������ Ȯ���ϸ� �ɵ�?..
                break;
            case 1011:
                playerMoveMent._bonusMoveSpeed = 1.3f * Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[itemcode].WhenItemActive][itemcode].Count;
                break;
            case 1012:
                //ġ��Ÿ Ȯ�� 5%���� ġ��Ÿ ������ ü�� 8  +4 *count ġ��
                break;
            case 1013:
                //���� óġ�ϸ� ��� ��Ÿ�� 4 + 2 * count�� ����
                break;
            case 1014:
                //�ϴ� 1014�� �ľ��� �ܰ� ������ model�̶� ������ ���� ���̶� �Ȱ��Ƽ� addcomponent ���� ������..
                // �ٸ� ������ �ڵ�� �򰥸��� ���ϼ��� ���ؼ� ���Ӱ� prefab�������

                StopCoroutine(nameof(Item1014_co));
                StartCoroutine(nameof(Item1014_co));
                //���� óġ�ϸ� �ܰ� 3�� ���� -> �ܰ��� ���� �Ѿ� ���� ������ ���� ���� ��ŭŸ���� ���� +  �ܰ��� ������ ���� ,
                break;
            case 1015:
                Managers.Resource.Instantiate("Item1015SKill");
                //���� óġ�ϸ� ������ ��� ��ǳ�� ����� �� �ȿ� �ִ� ����  �̼� 80% ���� 
                //�Ź� �����ϴ� ���� �ƴ϶� �ѹ� �����ϸ� ���̻� �������� �ʾƵ� �Ǹ�, �÷��̾� ��ġ�� ��� ����ٴϸ� �� 
                break;
            case 1016:
                bool Isitem1016Created = false;
                if (!Isitem1016Created)
                {
                  GameObject Item1016 =Managers.Resource.Instantiate("Item1016Skill");
                    Isitem1016Created = true;
                }
                //���� ���̰� �����Ͽ� ������ �� 5~100m �ݰ��� ������� ���� (�������ִ� ���� ����) ���� ������
                //=>  ���� ������ �� ������ �ִ� ������ ����
                break;
            case 1017:
                //������ ������ �����ϸ� �������� ����

                break;
            case 1018:
                currStartus.MaxHealth = currStartus._survivorsData.MaxHealth;
                currStartus.MaxHealth +=currStartus._survivorsData.MaxHealth+ 40*Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[itemcode].WhenItemActive][itemcode].Count;
                currStartus.HealthRegen += currStartus._survivorsData.HealthRegen + 1.6f * Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[itemcode].WhenItemActive][itemcode].Count;
                break;
            case 1019:
                StopCoroutine(nameof(Item1019_co));
                StartCoroutine(nameof(Item1019_co));
                //�������� ���� -> ���������� �÷��̾� ü�� ����
                //30�ʸ���, �������� �������� ��ȯ�Ͽ� ���ݷ¿� 300%,ü�¿� 100% ���ʽ��� �ݴϴ�.
                break;
        }
    }

    private IEnumerator Item1003_co()
    {
        bool item1003Spawned = false;
        if (!item1003Spawned)
        {
            item1003Spawned = true;
            for (int i = 0; i < 3; i++)
            {
                GameObject item1003 = Managers.Resource.Instantiate("Item1003Skill");
                item1003.transform.position = Player.transform.position;
                item1003.GetOrAddComponent<Item1003Skill>();
                item1003.SetRandomPositionSphere();
            }
            yield return new WaitForSeconds(1.5f);
            item1003Spawned = false;
        }
    }
    private IEnumerator Item1014_co()
    {
        if (Util.Probability(30))
        {
            bool item1014Spawned = false;
            if (!item1014Spawned)
            {
                item1014Spawned = true;
                for (int i = 0; i < 3; i++)
                {
                    GameObject item1014 = Managers.Resource.Instantiate("Item1014Skill");
                    item1014.transform.position = Player.transform.position;
                    //������
                    item1014.SetRandomPositionSphere(1, 2, 1);

                    item1014.GetOrAddComponent<item1014Skill>();
                }
                yield return new WaitForSeconds(3.0f);
                item1014Spawned = false;
            }
        }
  

    }
    private IEnumerator Item1007_co()
    {
        bool item1007Spawned = false;
        if (!item1007Spawned)
        {
            item1007Spawned = true;
            for (int i = 0; i < 3; i++)
            {
                GameObject item1007 = Managers.Resource.Instantiate("Item1014Skill");
                item1007.transform.position = Player.transform.position;

                item1007.SetRandomPositionSphere(1, 1, 5);
                Debug.Log("��ġ 2���� ���� �� �̾��ִ� ���� �ʿ�");
                Debug.Log("�������   �Լ� (item1007.transform.position ,item1007.SetRandomPositionSphere(1, 1, 5) ");
                item1007.GetOrAddComponent<Item1007Skill>();
                
            }
            yield return new WaitForSeconds(3.0f);
            item1007Spawned = false;
        }

    }

    private IEnumerator Item1019_co()
    {
        bool item1019Spawned = false;
        if (!item1019Spawned)
        {
            
            Item1019Skill[] prev1019skills= GameObject.FindObjectsOfType<Item1019Skill>();
            if(prev1019skills.Length > 0)
            {
                for (int i = 0; i < prev1019skills.Length; i++)
                {
                    Managers.Resource.Destroy(prev1019skills[i].gameObject);
                }
            }



            item1019Spawned = true;
            for (int i = 0; i < (Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1019].WhenItemActive][1019].Count); i++)
            {
                GameObject item1019 = Managers.Resource.Instantiate("Item1019Skill");
                item1019.transform.position = Player.transform.position;
                item1019.SetRandomPositionSphere(1, 10, 5);
                item1019.GetOrAddComponent<Item1007Skill>();
               
            }
            yield return new WaitForSeconds(3.0f);
            item1019Spawned = false;
        }
    }
}
