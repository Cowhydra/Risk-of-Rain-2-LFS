using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LogBook : UI_Scene,IListener
{
    //���� ����� ������Ƽ public���� ������.. ���� ���谡 ..��!
    //LogBook�� DetailLogBook�� ������?.. �ּ�ȭ �����
    private int Iteminfocode { get; set; } = -1;
    private int enivinfocode { get; set; } = -1;
    private int monsterinfocode { get; set; } = -1;
    private int Characterinfocode { get; set; } = -1;
    private bool iseverclicked ;
    private DetailInLogBook detailInLogBook;
    public  static Define.ECurrentClickType ClickType { get; private set; }=Define.ECurrentClickType.None;
    private Color subMenuImagePrevColor;
    private Color subMenuButtonPrevColor;
    enum ETexts
    {
        ItemAndEquipText,
        MonsterText,
        EnvironmentText,
        CharacterText,
        LogBookTitleText,
        LogBookSubText,

        IneventoryDescirbeTitleBackGroundText,
        IneventoryDescirbeTitleText,
        IneventoryDescirbeText,
        IneventoryIsAquireText,

    }
    enum EButtons
    {
        ItemAndEquip,
        Monster,
        Environment,
        Character,
        BackButton,
    }
    enum EGameObjects
    {
        ItemIneventoryPannel,
        MonsterIneventoryPannel,
        EnvionmentIneventoryPannel,
        CharacterIneventoryPannel,

    }
    enum EImages
    {
        ItemAndEquipColor,
        MonsterColor,
        EnvironmentColor,
        CharacterColor,

    }
    public override void Init()
    {
        base.Init();
      
        gameObject.GetComponent<Canvas>().sortingOrder = (int)Define.SortingOrder.LogBookUI;
        Bind<TextMeshProUGUI>(typeof(ETexts));
        Bind<Button>(typeof(EButtons));
        Bind<GameObject>(typeof(EGameObjects));
        Bind<Image>(typeof(EImages));

        subMenuImagePrevColor = GetImage((int)EImages.CharacterColor).color;
        subMenuButtonPrevColor = GetButton((int)EButtons.ItemAndEquip).GetComponent<Image>().color;
        Managers.Event.AddListener(Define.EVENT_TYPE.LogBookItem, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.ClickLogBookDetail, this);

        //�� ��ư �� Ŭ�� �̺�Ʈ
        GetButton((int)EButtons.ItemAndEquip).gameObject
            .BindEvent((PointerEventData data) => ItemAndEquipClickEvent());
        GetButton((int)EButtons.Character).gameObject
            .BindEvent((PointerEventData data) => CharacterClickEvent());
        GetButton((int)EButtons.Environment).gameObject
            .BindEvent((PointerEventData data) => EnvironmentClickEvent());
        GetButton((int)EButtons.Monster).gameObject
            .BindEvent((PointerEventData data) => MonsterClickEvent());
        GetButton((int)EButtons.BackButton).gameObject
            .BindEvent((PointerEventData data) => BackButtonEvent());
        //�� ��ư�� ������ In Out Į�� ���� �̺�Ʈ
        GetButton((int)EButtons.ItemAndEquip).gameObject
            .BindEvent((PointerEventData data) => GetImage((int)EImages.ItemAndEquipColor).color=Color.yellow,Define.UIEvent.PointerEnter);
        GetButton((int)EButtons.ItemAndEquip).gameObject
           .BindEvent((PointerEventData data) => GetImage((int)EImages.ItemAndEquipColor).color = subMenuImagePrevColor, Define.UIEvent.PointerExit);
        GetButton((int)EButtons.Monster).gameObject
         .BindEvent((PointerEventData data) => GetImage((int)EImages.MonsterColor).color = Color.yellow, Define.UIEvent.PointerEnter);
        GetButton((int)EButtons.Monster).gameObject
           .BindEvent((PointerEventData data) => GetImage((int)EImages.MonsterColor).color = subMenuImagePrevColor, Define.UIEvent.PointerExit);
        GetButton((int)EButtons.Environment).gameObject
                 .BindEvent((PointerEventData data) => GetImage((int)EImages.EnvironmentColor).color = Color.yellow, Define.UIEvent.PointerEnter);
        GetButton((int)EButtons.Environment).gameObject
           .BindEvent((PointerEventData data) => GetImage((int)EImages.EnvironmentColor).color = subMenuImagePrevColor, Define.UIEvent.PointerExit);
        GetButton((int)EButtons.Character).gameObject
                 .BindEvent((PointerEventData data) => GetImage((int)EImages.CharacterColor).color = Color.yellow, Define.UIEvent.PointerEnter);
        GetButton((int)EButtons.Character).gameObject
           .BindEvent((PointerEventData data) => GetImage((int)EImages.CharacterColor).color = subMenuImagePrevColor, Define.UIEvent.PointerExit);




        UIInit();


        #region �˾� �α׺�
        {
            detailInLogBook = Managers.UI.ShowPopupUI<DetailInLogBook>();
            detailInLogBook.gameObject.SetActive(false);
        }
        #endregion

    }
    private void BackButtonEvent()
    {
        Managers.UI.ClosePopupUI(detailInLogBook);
        Managers.Resource.Destroy(gameObject);
        ClickType = Define.ECurrentClickType.None;
    }
    private void UIInit()
    {
        GetText((int)ETexts.ItemAndEquipText).text = "�����۰� ���";
        GetText((int)ETexts.MonsterText).text = "����";
        GetText((int)ETexts.EnvironmentText).text = "ȯ��";
        GetText((int)ETexts.CharacterText).text = "������";
        GetText((int)ETexts.LogBookSubText).text = "���ϴ� �޴��� ������ �ּ���!";
        GetText((int)ETexts.IneventoryDescirbeTitleBackGroundText).text = "";
        GetText((int)ETexts.IneventoryDescirbeTitleText).text = "";
        GetText((int)ETexts.IneventoryDescirbeText).text = "";
        GetText((int)ETexts.IneventoryIsAquireText).text = "";

        Get<GameObject>((int)EGameObjects.ItemIneventoryPannel).SetActive(false);
        Get<GameObject>((int)EGameObjects.EnvionmentIneventoryPannel).SetActive(false);
        Get<GameObject>((int)EGameObjects.CharacterIneventoryPannel).SetActive(false);
        Get<GameObject>((int)EGameObjects.MonsterIneventoryPannel).SetActive(false);
    }
    void Start()
    {
        Init();
    }
    private void ItemAndEquipClickEvent()
    {
        SelectMenu(Define.ECurrentClickType.ItemAndEquip);
        GetText((int)ETexts.LogBookSubText).text = "�����۰� ���";

    }
    private void MonsterClickEvent()
    {
        SelectMenu(Define.ECurrentClickType.Monster);
        GetText((int)ETexts.LogBookSubText).text = "����";
    }
    private void EnvironmentClickEvent()
    {
        SelectMenu(Define.ECurrentClickType.Enviroment);
        GetText((int)ETexts.LogBookSubText).text = "ȯ��";

    }
    private void CharacterClickEvent()
    {
        SelectMenu(Define.ECurrentClickType.Character);
        GetText((int)ETexts.LogBookSubText).text = "������";

    }




    private void SetText()
    {
        switch (ClickType)
        {
            case Define.ECurrentClickType.ItemAndEquip:
                GetText((int)ETexts.IneventoryDescirbeTitleBackGroundText).text
                    = $"<mark =#8A2BE2>[{Managers.Data.ItemDataDict[Iteminfocode].itemname}]</mark>";
                GetText((int)ETexts.IneventoryDescirbeTitleText).text
                    = $"[{Managers.Data.ItemDataDict[Iteminfocode].itemname}]";
                GetText((int)ETexts.IneventoryDescirbeText).text
                    = $"{Managers.Data.ItemDataDict[Iteminfocode].explanation}";
             
                GetText((int)ETexts.IneventoryIsAquireText).text = $"ȹ�� ���� : {ConvertToSTring(Managers.Data.ItemDataDict[Iteminfocode].isHaveHad)}";
                break;
            case Define.ECurrentClickType.Monster:

               //�� ������ �ڵ忡 ���� ������ Ȯ��
                break;
            case Define.ECurrentClickType.Character:
                GetText((int)ETexts.IneventoryDescirbeTitleBackGroundText).text
                  = $"<mark =#8A2BE2>[{Managers.Data.CharacterDataDict[Characterinfocode].Name}]</mark>";
                GetText((int)ETexts.IneventoryDescirbeTitleText).text
                    = $"[{Managers.Data.CharacterDataDict[Characterinfocode].Name}]";
                GetText((int)ETexts.IneventoryDescirbeText).text
                    = $"{Managers.Data.CharacterDataDict[Characterinfocode].unlockscript2}";
                GetText((int)ETexts.IneventoryIsAquireText).text = $"ȹ�� ���� : {ConvertToSTring(Managers.Data.CharacterDataDict[Characterinfocode].isActive)}";
                //�� ������ �ڵ忡 ���� ������ Ȯ��
                break;
            case Define.ECurrentClickType.Enviroment:

                //�� ������ �ڵ忡 ���� ������ Ȯ��
                break;
        }
    }
    private void SelectMenu(Define.ECurrentClickType _clickType)
    {
        Get<GameObject>((int)EGameObjects.ItemIneventoryPannel).SetActive(false);
        Get<GameObject>((int)EGameObjects.EnvionmentIneventoryPannel).SetActive(false);
        Get<GameObject>((int)EGameObjects.CharacterIneventoryPannel).SetActive(false);
        Get<GameObject>((int)EGameObjects.MonsterIneventoryPannel).SetActive(false);
        GetButton((int)EButtons.ItemAndEquip).GetComponent<Image>().color = subMenuButtonPrevColor;
        GetButton((int)EButtons.Environment).GetComponent<Image>().color = subMenuButtonPrevColor;
        GetButton((int)EButtons.Character).GetComponent<Image>().color = subMenuButtonPrevColor;
        GetButton((int)EButtons.Monster).GetComponent<Image>().color = subMenuButtonPrevColor;

        switch (_clickType)
        {
            case Define.ECurrentClickType.ItemAndEquip:
                ClickType= Define.ECurrentClickType.ItemAndEquip;
                Get<GameObject>((int)EGameObjects.ItemIneventoryPannel).SetActive(true);
                GetButton((int)EButtons.ItemAndEquip).GetComponent<Image>().color = Color.yellow;
                break;
            case Define.ECurrentClickType.Monster:
                ClickType= Define.ECurrentClickType.Monster;
                Get<GameObject>((int)EGameObjects.MonsterIneventoryPannel).SetActive(true);
                GetButton((int)EButtons.Monster).GetComponent<Image>().color = Color.yellow;
                break;
            case Define.ECurrentClickType.Character:
                ClickType= Define.ECurrentClickType.Character;
                Get<GameObject>((int)EGameObjects.CharacterIneventoryPannel).SetActive(true);
                GetButton((int)EButtons.Character).GetComponent<Image>().color = Color.yellow;
                break;
            case Define.ECurrentClickType.Enviroment:
                ClickType= Define.ECurrentClickType.Enviroment;
                Get<GameObject>((int)EGameObjects.EnvionmentIneventoryPannel).SetActive(true);
                GetButton((int)EButtons.Environment).GetComponent<Image>().color = Color.yellow;
                break;
        }
    }

    public void OnEvent(Define.EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        //�̺�Ʈ ó�� ���� �̺�Ʈ Ÿ�Կ� ���� ó��
        switch (Event_Type)
        {
            case Define.EVENT_TYPE.LogBookItem:
                iseverclicked = true;
                if (Sender.TryGetComponent(out ItemButton itemButton))
                {
                    Iteminfocode = itemButton.Itemcode;
                }
                else if (Sender.TryGetComponent(out InvenCharacterButton CharButton))
                {
                    Characterinfocode = CharButton.Charactercode;
                }

                SetText();
                break;
            case Define.EVENT_TYPE.ClickLogBookDetail:
                Debug.Log("ClickLogBookDetail �̺�Ʈ �߼�");
                detailInLogBook.gameObject.SetActive(true);
                if (Sender.TryGetComponent(out ItemButton itemButtons))
                {
                    detailInLogBook.SpecialCode = itemButtons.Itemcode;
                }
                else if (Sender.TryGetComponent(out InvenCharacterButton CharButton))
                {
                    detailInLogBook.SpecialCode = CharButton.Charactercode;
                }
       
                break;
        }
   
    }

    private string ConvertToSTring(bool result)
    {
        return result ? "����" : "�̺���";
    }



}
