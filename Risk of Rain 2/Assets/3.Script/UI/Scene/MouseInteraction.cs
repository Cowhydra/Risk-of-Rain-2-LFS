using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MouseInteraction : UI_Scene,IListener
{
    Image MouseCursor;
    public int SpecialCode;

    enum EInteractionType
    {
        Skill,
        Character,
        Difficulty,
    }
    enum EImages
    {
        MouseCursorImage,

    }
    enum EGameObjects
    {
        RightPannel,
        LeftPannel,

    }
    enum ETexts
    {
        RightContentsTitleText,
        RightTitleText,
        LeftTitleText,
        LeftContentsTitleText,
    }
    public override void Init()
    {
        base.Init();
        //���콺 ���� Ŀ�� Ȱ��/��Ȱ��
        Cursor.visible = true;
        GetComponent<Canvas>().sortingOrder = (int)Define.SortingOrder.MouseInteraction;
        Bind<Image>(typeof(EImages));
        Bind<TextMeshProUGUI>(typeof(ETexts));
        Bind<GameObject>(typeof(EGameObjects));
        SetMouseCursor();

        Get<GameObject>((int)EGameObjects.LeftPannel).SetActive(false);
        Get<GameObject>((int)EGameObjects.RightPannel).SetActive(false);
        Managers.Event.AddListener(Define.EVENT_TYPE.MousePointerEnter, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.MousePointerExit, this);

        //�̺�Ʈ ����...

    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    private void SetMouseCursor()
    {
        MouseCursor = GetImage((int)EImages.MouseCursorImage);
        MouseCursor.GetComponent<Image>().raycastTarget = false;
        MouseCursor.transform.localScale = 5* Vector3.one;
        MouseCursor.GetComponent<RectTransform>().pivot = new Vector2(1.8f, 1f);
    }
    // Update is called once per frame
    void Update()
    {
        MouseCursor.transform.position = Input.mousePosition;
    }
    private void ReInit()
    {
        Get<GameObject>((int)EGameObjects.LeftPannel).SetActive(false);
        Get<GameObject>((int)EGameObjects.RightPannel).SetActive(false);
    }
    private void ActivePannel(EInteractionType myInteractionType)
    {
        //������ Ÿ��
        //1. ĳ���� Ȱ�� ����( ������ ���� ) 
        //2. ��ų( ������ ���� )
        //3. ���̵�( ���� ���� )
        switch (myInteractionType)
        {
            case EInteractionType.Skill:
                Get<GameObject>((int)EGameObjects.RightPannel).SetActive(true);
                break;
            case EInteractionType.Character:
                Get<GameObject>((int)EGameObjects.RightPannel).SetActive(true);
                break;
            case EInteractionType.Difficulty:
                Get<GameObject>((int)EGameObjects.LeftPannel).SetActive(true); 
                break;
        }

    }

    public void OnEvent(Define.EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        switch (Event_Type)
        {
            case Define.EVENT_TYPE.MousePointerEnter:
                if (Sender.TryGetComponent(out Diffidculty diff))
                {
                    ActivePannel(EInteractionType.Difficulty);
                    switch (diff.myDifficulty)
                    {
                        case Define.EDifficulty.Easy:
                            GetText((int)ETexts.LeftTitleText).text = "�̽���";
                            GetText((int)ETexts.LeftContentsTitleText).text = "�ʺ� �÷��̾ ���� ���̵��Դϴ�. ���� ���� �̰� ������ ������ �������� �������� �������ϴ�.";
                            break;
                        case Define.EDifficulty.Normal:
                            GetText((int)ETexts.LeftTitleText).text = "��ǳ��";
                            GetText((int)ETexts.LeftContentsTitleText).text = "�� ������ ������ �ǵ���� �÷����մϴ�! ������ ������ ���� �Ƿ��� �����Ͻʽÿ�.";
                            break;
                        case Define.EDifficulty.Hard:
                            GetText((int)ETexts.LeftTitleText).text = "���";
                            GetText((int)ETexts.LeftContentsTitleText).text = "�ϵ��ھ� �÷��̾ ���� ���̵��Դϴ�. ���� ������ ����� ������ ���� �� ���Դϴ�. ������ �����Ͻʽÿ�.";
                            break;
                    }
                }
                else if(Sender.TryGetComponent(out CharacterSelectButton characterSelect))
                {
                    ActivePannel(EInteractionType.Character);
                    GetText((int)ETexts.RightTitleText).text = Managers.Data.CharacterDataDict[characterSelect.Charactercode].Name;
                    GetText((int)ETexts.RightContentsTitleText).text = Managers.Data.CharacterDataDict[characterSelect.Charactercode].unlockscript2;
                }
                else if(Sender.TryGetComponent(out LoadSkillTempo Tempo))
                {
                    ActivePannel(EInteractionType.Skill);
                    GetText((int)ETexts.RightTitleText).text = Tempo.skillTitle;
                    GetText((int)ETexts.RightContentsTitleText).text = Tempo.skillContents;
                }
                break;
            case Define.EVENT_TYPE.MousePointerExit:
                    ReInit();
                break;
        }


    }
}
