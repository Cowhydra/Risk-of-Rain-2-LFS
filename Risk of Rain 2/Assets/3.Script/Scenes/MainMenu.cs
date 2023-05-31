using UnityEngine;

public class MainMenu : BaseScene
{
    void Start()
    {

        //��.. ��� Addserable�� ���� Asynchronous_Load �� ���ʿ� ����,
        //���� ������ �Ѿ �� ��,, �� �̷������� ���ҽ� �׶����� �ε� �ص� �Ǵµ�
        //������Ʈ�� �۱⵵ �ϰ� �ϴϱ� �ϴ� ���⼭�� �ѹ��� ��� ���� �ε� 
        Managers.Resource.LoadAllAsync<Object>("Asynchronous_Load", (key, count, totalCount) =>
        {
            //  Debug.Log($"{key} {count}/{totalCount}");

            if (count == totalCount)
            {
                Debug.Log("������ �ε� �Ϸ�!");
                StartLoaded();
            }
        });

    }

    void StartLoaded()
    {
        Init();
        Managers.Data.Init();
        Managers.ItemInventory.init();
        Managers.ItemApply.Init();
        Managers.UI.ShowSceneUI<MainUI>();
        Managers.UI.ShowSceneUI<MouseInteraction>();
    }


    protected override void Init()
    {
        base.Init();


    }
    public override void Clear()
    {
        Managers.Event.ClearEventList();
    }
}
