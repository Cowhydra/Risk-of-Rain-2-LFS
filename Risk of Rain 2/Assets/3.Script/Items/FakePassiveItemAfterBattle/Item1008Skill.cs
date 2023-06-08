using UnityEditor.Rendering;
using UnityEngine;

//���ƺ�� -> ��ϻ���
//Ÿ���� �����ؼ� Ÿ���� ���� �� �ش� ��ġ�� ������ �� ����
public class Item1008Skill : NewItemPrimitive, IAfterBattleItem
{
    public int Itemid => 1008;

    public void AfterExcuteSkillEffect(Transform TargetTransform)
    {
        if (Managers.ItemInventory.Items[Itemid].Count.Equals(0))
        {
            return;
        }
        base.Init();
        GameObject item1008 = Managers.Resource.Instantiate("Item1008Skill");
        //�ݶ��̴� �ִ� ��� ��ġ ����
        if(TargetTransform.TryGetComponent(out Collider coll))
        {
            item1008.transform.position =new Vector3( TargetTransform.position.x,TargetTransform.position.y -coll.bounds.size.y+3f   ,TargetTransform.position.z);
                
        }
        else
        {
            item1008.transform.position = TargetTransform.position;
        }

        item1008.GetOrAddComponent<Item1008SkillComponent>();
    }
}
