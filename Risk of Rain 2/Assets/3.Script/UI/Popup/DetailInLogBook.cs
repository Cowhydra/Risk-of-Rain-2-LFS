using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DetailInLogBook : UI_Popup
{

    private int _specialCode;
    public int SpecialCode
    {
        get
        {
            return _specialCode;
        }
        set
        {
            _specialCode = value;
            Setting();
        }
    }
    private void Awake()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(EGameObjects));
        Bind<Button>(typeof(EButtons));
        Bind<TextMeshProUGUI>(typeof(ETexts));
        GetButton((int)EButtons.BackButton).gameObject
            .BindEvent((PointerEventData data) => gameObject.SetActive(false));

        GetComponent<Canvas>().sortingOrder = (int)Define.SortingOrder.DetailInLogBook;
        Setting();
    }
    enum EGameObjects
    {
        ObjectSpawnPosition,

    }
    enum ETexts
    {
        TItileTitleText,
        TitlieContentsText,
        ScriptTitleText,
        ScriptContentsText,
        FindCountText,
        FindMaxCountTitleText,
        InformationTitleText,
        InformationContentsText,


    }
    enum EButtons
    {
        BackButton
    }
    private void Setting()
    {

        if (SpecialCode.Equals(-1))
        {
            return;
        }
        if (LogBook.ClickType.Equals(Define.ECurrentClickType.None))
        {
            return;
        }

        foreach (Transform transforom in Get<GameObject>((int)EGameObjects.ObjectSpawnPosition).GetComponentInChildren<Transform>())
        {
            Managers.Resource.Destroy(transforom.gameObject);
        }
        //����ȭ ������ ���� �� �˾� â�� �̸� �����صΰ�, Enable�Ҷ� ���� �̺�Ʈ�� �ؽ�Ʈ  ����..
        //�ٸ� UI�鵵 ���۰� ���ÿ� �̸� ����� ������ ����ȭ �����ѵ�.. FPS ������ �ʾƼ� �ϴ� ���ξ����ϴ�.
        SetText();
        SetModel();
    }

    private void SetText()
    {

        switch (LogBook.ClickType)
        {
            case Define.ECurrentClickType.ItemAndEquip:
                GetText((int)ETexts.TItileTitleText).text = $"{Managers.Data.ItemDataDict[SpecialCode].itemname}";
                GetText((int)ETexts.TitlieContentsText).text = "�����۰� ���";
                GetText((int)ETexts.ScriptTitleText).text = $"���� : ";
                GetText((int)ETexts.ScriptContentsText).text = $" {Managers.Data.ItemDataDict[SpecialCode].explanation}";
                GetText((int)ETexts.FindCountText).text = $"�߰��� : {0}";
                GetText((int)ETexts.FindMaxCountTitleText).text = $"�ְ���ø : {0}";
                GetText((int)ETexts.InformationTitleText).text = "����";
                GetText((int)ETexts.InformationContentsText).text = $"{Managers.Data.ItemDataDict[SpecialCode].explanation}\n" +
                    $"�߰� ���� ���� : {Managers.Data.EnvDataDict[Random.Range(100, 107)].enviromentname}\n";
        
                break;
            case Define.ECurrentClickType.Monster:
                GetText((int)ETexts.TItileTitleText).text = $"{Managers.Data.MonData[SpecialCode].name}";
                GetText((int)ETexts.TitlieContentsText).text = "����";
                GetText((int)ETexts.ScriptTitleText).text = $"���� : ";
                GetText((int)ETexts.ScriptContentsText).text = $" ü�� : {Managers.Data.MonData[SpecialCode].maxhealth} \n���� : {Managers.Data.MonData[SpecialCode].attack}\n �ӵ� : {Managers.Data.MonData[SpecialCode].speed}\n ��� : {Managers.Data.MonData[SpecialCode].armor}" +
                    $"";
                GetText((int)ETexts.FindCountText).text = $"�÷��̾� óġ : {0}";
                GetText((int)ETexts.FindMaxCountTitleText).text = $"óġ ���� Ƚ�� : {0}";
                GetText((int)ETexts.InformationTitleText).text = "������Ű ����";
                GetText((int)ETexts.InformationContentsText).text = $"{Managers.Data.MonData[SpecialCode].script}";
                break;
            case Define.ECurrentClickType.Character:
                GetText((int)ETexts.TItileTitleText).text = $"{Managers.Data.CharacterDataDict[SpecialCode].Name}";
                GetText((int)ETexts.TitlieContentsText).text = "������";
                GetText((int)ETexts.ScriptTitleText).text = $"���� : ";
                GetText((int)ETexts.ScriptContentsText).text = $" {Managers.Data.CharacterDataDict[SpecialCode].script1}";
                GetText((int)ETexts.FindCountText).text = $"���� óġ : {0}";
                GetText((int)ETexts.FindMaxCountTitleText).text = $"�ִ� ���� óġ : {0}";
                GetText((int)ETexts.InformationTitleText).text = "����";
                GetText((int)ETexts.InformationContentsText).text = $"{Managers.Data.CharacterDataDict[SpecialCode].script1}\n{Managers.Data.CharacterDataDict[SpecialCode].script2}\n{Managers.Data.CharacterDataDict[SpecialCode].script3}\n{Managers.Data.CharacterDataDict[SpecialCode].script4}";
                break;
            case Define.ECurrentClickType.Enviroment:
                break;
        }
    }
    private void SetModel()
    {
        switch (LogBook.ClickType)
        {
            case Define.ECurrentClickType.ItemAndEquip:
                GameObject item = Managers.Resource.Instantiate($"item{SpecialCode}", Get<GameObject>((int)EGameObjects.ObjectSpawnPosition).transform);
                item.GetOrAddComponent<UIItemController>();
                break;
            case Define.ECurrentClickType.Monster:
                GameObject monster = Managers.Resource.Instantiate($"{SpecialCode}Model", Get<GameObject>((int)EGameObjects.ObjectSpawnPosition).transform);
                monster.GetOrAddComponent<UIItemController>();
                break;
            case Define.ECurrentClickType.Character:
                if (!(SpecialCode.Equals(7) || SpecialCode.Equals(1)))
                {
                    return;
                }
                if (SpecialCode.Equals(1))
                {
                    GameObject character = Managers.Resource.Instantiate($"Commando", Get<GameObject>((int)EGameObjects.ObjectSpawnPosition).transform);
                    character.GetOrAddComponent<UIItemController>();

                }
                else if (SpecialCode.Equals(7))
                {
                    GameObject character = Managers.Resource.Instantiate($"Merc", Get<GameObject>((int)EGameObjects.ObjectSpawnPosition).transform);
                    character.GetOrAddComponent<UIItemController>();
                }
                break;
            case Define.ECurrentClickType.Enviroment:
                break;
            case Define.ECurrentClickType.None:
                break;
        }
    }



}
