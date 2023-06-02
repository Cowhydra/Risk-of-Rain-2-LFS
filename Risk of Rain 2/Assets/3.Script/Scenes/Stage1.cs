using UnityEngine;

public class Stage1 : BaseScene
{
   
    // Start is called before the first frame update
    void Start()
    {
        Managers.Resource.LoadAllAsync<Object>("Asynchronous_Load", (key, count, totalCount) =>
        {
            //  Debug.Log($"{key} {count}/{totalCount}");

            if (count == totalCount)
            {
                Debug.Log("�ӽ� �ڵ� !");
                Debug.Log("���� �̺κ� StartLoaded �����ؾ���");
                Debug.Log("        Managers.UI.ShowGameUI<GameUI>(); �־������");
                StartLoaded();
            }
        });
        SceneType = Define.Scene.Stage1;

    }
    void StartLoaded()
    {
        Init();
        Managers.Data.Init();
        Managers.ItemInventory.init();
        Managers.ItemApply.Init();
        Managers.UI.ShowGameUI<GameUI>();

        Managers.Game.Gold = 9900;
    }
    protected override void Init()
    {
        base.Init();


    }

    public override void Clear()
    {
    }
}
