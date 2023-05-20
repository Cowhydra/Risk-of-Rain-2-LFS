using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DetailInLogBook : UI_Popup
{
  
    public int specialCode = -1;
    private void Start()
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
            .BindEvent((PointerEventData data) => { gameObject.SetActive(false); });

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
    private void OnEnable()
    {
        
        Setting();
    }
    private void Setting()
    {
       
        if (specialCode.Equals(-1))
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
                GetText((int)ETexts.TItileTitleText).text = $"{Managers.Data.ItemDataDict[specialCode].itemname}";
                GetText((int)ETexts.TitlieContentsText).text = "�����۰� ���";
                GetText((int)ETexts.ScriptTitleText).text = $"���� : ";
                GetText((int)ETexts.ScriptContentsText).text = $" {Managers.Data.ItemDataDict[specialCode].explanation}";
                GetText((int)ETexts.FindCountText).text = $"�߰��� : {1}";
                GetText((int)ETexts.FindMaxCountTitleText).text = $"�ְ���ø : {1}";
                GetText((int)ETexts.InformationTitleText).text = "����";
                GetText((int)ETexts.InformationContentsText).text = "�����ۿ� ���� �������� ����!";
                break;
            case Define.ECurrentClickType.Monster:

                break;
            case Define.ECurrentClickType.Character:
                GetText((int)ETexts.TItileTitleText).text = $"{Managers.Data.CharacterDataDict[specialCode].Name}";
                GetText((int)ETexts.TitlieContentsText).text = "������";
                GetText((int)ETexts.ScriptTitleText).text = $"���� : ";
                GetText((int)ETexts.ScriptContentsText).text = $" {Managers.Data.CharacterDataDict[specialCode].script1}";
                GetText((int)ETexts.FindCountText).text = $"���� óġ : {1}";
                GetText((int)ETexts.FindMaxCountTitleText).text = $"�ִ� ���� óġ : {1}";
                GetText((int)ETexts.InformationTitleText).text = "����";
                GetText((int)ETexts.InformationContentsText).text = "ĳ���Ϳ� ���� �������� ����!";
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
               GameObject go= Managers.Resource.Instantiate($"item{Managers.Data.ItemDataDict[specialCode].itemcode}", Get<GameObject>((int)EGameObjects.ObjectSpawnPosition).transform);
                go.transform.localScale = Vector3.one * 22000f;
                go.transform.localPosition = new Vector3(0, 0 ,- 100);
                break;
            case Define.ECurrentClickType.Monster:
                break;
            case Define.ECurrentClickType.Character:
                Debug.Log("���� ĳ���� �𵨸� �ϼ��Ǹ� �ϼ��� ĳ���͸� �� �߰�");
                //Managers.Resource.Instantiate($"item{Managers.Data.CharacterDataDict[specialCode].}", Get<GameObject>((int)EGameObjects.ObjectSpawnPosition).transform);

                break;
            case Define.ECurrentClickType.Enviroment:
                break;
            case Define.ECurrentClickType.None:
                break;
        }
    }



}
