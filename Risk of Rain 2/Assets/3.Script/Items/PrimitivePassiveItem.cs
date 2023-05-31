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
    #region ��ų �������� �Ϻ�
    bool Isitem1015Created = false;
    bool item1019Spawned = false;
    #endregion
    //���ʽ� ���� ī��Ʈ...
    public override void Init()
    {
        base.Init();
        Managers.Event.CharacterStateChange -= ExcuteSkill;
        Managers.Event.CharacterStateChange += ExcuteSkill;
        Managers.Event.AddItem -= OnPassiveSkill;
        Managers.Event.AddItem += OnPassiveSkill;
    }
    private void Start()
    {
        Init();
       
    }
    //�⺻������ �÷��̾� ���¿� ���� �ѹ��� ���� ��������ְ�, ���� �������� ȹ���� ��쿡�� �߰������� �ѹ��� ����
    //���� ������ ������ �־�� �ϰ�, �������� ȹ���� ��� ������ Ÿ���� ���� �÷��̾� ���°� ��ϳĿ� ����
    //���� �� ���������� ���� ������ ���°� �ٲ� ���� ��� �нú� ��ų���� ��ȸ�� �����̴ϱ�

    private void ExcuteSkill(Define.WhenItemActivates SKillType)
    {
        switch (SKillType)
        {
            case Define.WhenItemActivates.AfterBattle:
                foreach (var itemkey in Managers.ItemInventory.WhenActivePassiveItem[Define.WhenItemActivates.AfterBattle].Keys)
                {
                   
                }
                break;
            case Define.WhenItemActivates.InBattle:
                foreach (var itemkey in Managers.ItemInventory.WhenActivePassiveItem[Define.WhenItemActivates.InBattle].Keys)
                {
                    OnPassiveSkill(itemkey);
                }
                break;
            case Define.WhenItemActivates.NotBattle:
                foreach (var itemkey in Managers.ItemInventory.WhenActivePassiveItem[Define.WhenItemActivates.NotBattle].Keys)
                {
                    OnPassiveSkill(itemkey);
                }
                break;
        }
        foreach (var itemkey in Managers.ItemInventory.WhenActivePassiveItem[Define.WhenItemActivates.Always].Keys)
        {
            OnPassiveSkill(itemkey);
        }

    }
    private void StopSkill()
    {

    }

   
    //�нú��� �ؼ� ���� �нú갡 �ƴ�, �нú��ε� ��Ƽ������ �� ���� ��ų�� ���� �Ӹ� ����
    //�ϴ�.. ü�踦 ����Ƽ� �ϵ��ڵ� ������ ������ ���߿� ��ų �� ���� ���� �� �� ����ȭ �ؼ� ����?.. ��Ƴ�\
    //���� �׿��� �ߵ� ��Ű�� ��� ���� ��ġ�� ��� ��� �� �ϳ� ->  ���� �غ��鼭 �����غ���  �ܼ��ϰ� �����ε� ���?

    //���� ���� -���� ��
    //�׽� ����
    

    private void OnPassiveSkill(int itemcode)
    {
        switch(itemcode)
        {
            case 1001:
                //���� 15% ��� -> �ϴ� �ӽ÷� �ִ�ü�� �÷����� �ʿ��Ѱ� ���� ������ ����
                _playerStatus.AddMaxHealth(15 * Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[itemcode].WhenItemActive][itemcode].Count);
                    break;  
            case 1002:
                _playerStatus.ChanceBlockDamage= 5 * Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[itemcode].WhenItemActive][itemcode].Count;
                //��� ���������̶� ���� ������Ʈ �ִ��� �ؾ��ҵ�? -> �� �ƴ϶� �׳� �÷��̾� Hit �κп� ������ ���� �Լ� �ϳ� ���� ��
                break;
            case 1003:
                StopCoroutine(nameof(Item1003_co));
                StartCoroutine(nameof(Item1003_co));
                //�÷��̾� ��ġ �������� ������ �� �� �޵�Ŷ ������ �ϳ� �������ְ� �ű⿡ ����� ����  (��)
                break;
            case 1004:
                _playerStatus.CriticalChance = _playerStatus._survivorsData.CriticalChance + 10 * Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[itemcode].WhenItemActive][itemcode].Count;
                break;
            case 1005:
                _playerStatus.MoveSpeed=_playerStatus._survivorsData.MoveSpeed*1.14f* Managers.ItemInventory.WhenActivePassiveItem[Define.WhenItemActivates.Always][itemcode].Count;
                //playerMoveMent._bonusMoveSpeed=1.14f * Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[itemcode].WhenItemActive][itemcode].Count;
                break;
            case 1006:
                //�ǵ� ȹ���ε� �ǵ� ���� �Ӽ��� ���� --> ü�� ����� ������ �����ص�
                //�ϴ� ü�� �������� �־���µ� ü�� ���� 
                _playerStatus.HealthRegen=_playerStatus._survivorsData.HealthRegen+(5 * Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[itemcode].WhenItemActive][itemcode].Count);
                break;
            case 1007:
                StopCoroutine(nameof(Item1007_co));
                StartCoroutine(nameof(Item1007_co));
                //���� �߻� �� �̻��� �߻� -> �켱 �÷��̾� ���� ������ Ÿ�� ��ġ�� ���������� ����
                break;
            case 1008:
                //��� ��� �����ϴ� ��ũ��Ʈ ���� ���� óġ�� ��� 12m �ݰ濡 ��ϱ���� ���� �Ǿ� 350% ����
                //�ϴ� ���ظ� ������ �ڵ�� �ۼ��صξ��µ� ���� ��ġ�� �� ���� ��ġ�� ��ġ ���� ��� ��� ����?  �ƴϸ� ���� �̺�Ʈ�� �ߵ����Ѿ� ���� �𸣰���
                GameObject item1008 = Managers.Resource.Instantiate("Item1008Skill");
                item1008.GetOrAddComponent<Item1008Skill>();
                item1008.transform.position = Player.transform.position;
                break;
            case 1009:
                 _playerStatus.MaxJumpCount = 1+Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[itemcode].WhenItemActive][itemcode].Count;
                break;
            case 1010:
                _playerStatus.OnHeal(1 * Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[itemcode].WhenItemActive][itemcode].Count);
                //���ظ� ������ ���� ü���� 1 ȸ��... ��� ���� ���µ� ���� �ҵ� �ѵ�?.. --> ������Ʈ �����ͼ� ������Ƽ ������ Ȯ���ϸ� �ɵ�?..
                break;
            case 1011:
                _playerStatus.MoveSpeed = 1.3f * Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[itemcode].WhenItemActive][itemcode].Count;
                break;
            case 1012:
                _playerStatus.CriticalChance=_playerStatus._survivorsData.CriticalChance+ 5*Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[itemcode].WhenItemActive][itemcode].Count;
                //ġ��Ÿ Ȯ�� 5%���� ġ��Ÿ ������ ü�� 8  +4 *count ġ��  ===> ��� ���� Ȯ���� ġ��
                _playerStatus.OnHeal(4 * Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[itemcode].WhenItemActive][itemcode].Count);
                break;
            case 1013:
                FindObjectOfType<CommandoSkill>().SkillQColldown-=4+2* Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[itemcode].WhenItemActive][itemcode].Count;
                //���� óġ�ϸ� ��� ��Ÿ�� 4 + 2 * count�� ����
                break;
            case 1014:
                StopCoroutine(nameof(Item1014_co));
                StartCoroutine(nameof(Item1014_co));
                //���� óġ�ϸ� �ܰ� 3�� ���� -> �ܰ��� ���� �Ѿ� ���� ������ ���� ���� ��ŭŸ���� ���� +  �ܰ��� ������ ���� ,
                break;
            case 1015:
                GameObject Item1015=null;
                if (!Isitem1015Created)
                {
                    Item1015 = Managers.Resource.Instantiate("Item1015Skill");
                    Item1015.GetOrAddComponent<Item1015Skill>();
                    Isitem1015Created = true;
                }
                else
                {

                    Item1015.GetOrAddComponent<Item1015Skill>().SetSize(Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1015].WhenItemActive][1015].Count);
                }
                //���� óġ�ϸ� ������ ��� ��ǳ�� ����� �� �ȿ� �ִ� ����  �̼� 80% ���� 
                //�Ź� �����ϴ� ���� �ƴ϶� �ѹ� �����ϸ� ���̻� �������� �ʾƵ� �Ǹ�, �÷��̾� ��ġ�� ��� ����ٴϸ� �� 
                break;
            case 1016:
                GameObject Item1016 =Managers.Resource.Instantiate("Item1016Skill");
                Item1016.GetOrAddComponent<Item1016Skill>();
                Item1016.transform.position = Player.transform.position;
                //���� ���̰� �����Ͽ� ������ �� 5~100m �ݰ��� ������� ���� (�������ִ� ���� ����) ���� ������
                //=>  ���� ������ �� ������ �ִ� ������ ����
                break;
            case 1017:
                //������ ������ �����ϸ� �������� ����

                break;
            case 1018:
                _playerStatus.AddMaxHealth( 40*Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[itemcode].WhenItemActive][itemcode].Count);
                _playerStatus.HealthRegen += _playerStatus._survivorsData.HealthRegen + 1.6f * Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[itemcode].WhenItemActive][itemcode].Count;
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
            for (int i = 0; i < 1; i++)
            {
                GameObject item1003 = Managers.Resource.Instantiate("Item1003Skill");
                item1003.transform.position = Player.transform.position;
                item1003.GetOrAddComponent<Item1003Skill>();
                item1003.SetRandomPositionSphere(5, 2, 5,Player.transform);

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
                    item1014.SetRandomPositionSphere(2f, 2f, 5,Player.transform);

                    item1014.GetOrAddComponent<Item1014Skill>();
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
            for (int i = 0; i < 1; i++)
            {
                GameObject item1007 = Managers.Resource.Instantiate("Item1007Skill");

                Debug.Log("��ġ 2���� ���� �� �̾��ִ� ���� �ʿ�");
                Debug.Log("�������   �Լ� (item1007.transform.position ,item1007.SetRandomPositionSphere(2f, 2f, 5, Player.transform);" +
                    "���� SetRandomPosition �������� ���� �ٶ� -KYS ");
                item1007.GetOrAddComponent<Item1007Skill>();
                item1007.SetRandomPositionSphere(2f, 2f, 5, Player.transform);
            }
            yield return new WaitForSeconds(3.0f);
            item1007Spawned = false;
        }

    }

    private IEnumerator Item1019_co()
    {
        GameObject item1019=null;
        if (!item1019Spawned)
        {
            item1019Spawned = true;
            for (int i = 0; i < 2; i++)
            {
                item1019 = Managers.Resource.Instantiate("Item1019Skill");
                item1019.SetRandomPositionSphere(2, 7, 5,Player.transform);
                item1019.GetOrAddComponent<Item1019Skill>();
            }
            yield return new WaitForSeconds(3.0f);
        }
        item1019.GetComponent<Item1019Skill>().SetStats(Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1019].WhenItemActive][1019].Count);
    }
}
