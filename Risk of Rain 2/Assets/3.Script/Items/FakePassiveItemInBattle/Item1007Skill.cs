using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Item1007Skill : NewItemPrimitive, IInBattleItem
{
    public int Itemid => 1007;

    public void InExcuteSkillEffect()
    {
        if (Managers.ItemInventory.Items[Itemid].Count.Equals(0))
        {
            return;
        }
        base.Init();
        if (Util.Probability(10))
        {
            GameObject item1007 = Managers.Resource.Instantiate("Item1007Skill");

            Debug.Log("��ġ 2���� ���� �� �̾��ִ� ���� �ʿ�");
            Debug.Log("�������   �Լ� (item1007.transform.position ,item1007.SetRandomPositionSphere(2f, 2f, 5, Player.transform);" +
                "���� SetRandomPosition �������� ���� �ٶ� -KYS ");
            item1007.GetOrAddComponent<Item1007SkillComponent>();
            item1007.SetRandomPositionSphere(2f, 2f, 5, Player.transform);
        }
  
    }
}
