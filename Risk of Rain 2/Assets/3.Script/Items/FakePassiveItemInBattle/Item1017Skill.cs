public class Item1017Skill : NewItemPrimitive, IInBattleItem
{
    public int Itemid => 1017;

    public void InExcuteSkillEffect()
    {
        if (Managers.ItemInventory.Items[Itemid].Count.Equals(0))
        {
            return;
        }
        //������ ������ �����ϸ� �������� ����
    }
}
