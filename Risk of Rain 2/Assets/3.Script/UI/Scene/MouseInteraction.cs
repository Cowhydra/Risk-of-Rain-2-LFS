using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MouseInteraction : UI_Scene, IListener
{
    public int SpecialCode;
    [SerializeField] private Texture2D mouseCursorImage;
    RectTransform MouseFakeImage;
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
        RightBackGroundPannel,
        LeftBackGroundPannel,

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
        MouseFakeImage = GetImage((int)EImages.MouseCursorImage).GetComponent<RectTransform>();
        Get<GameObject>((int)EGameObjects.LeftPannel).SetActive(false);
        Get<GameObject>((int)EGameObjects.RightPannel).SetActive(false);

        //�̺�Ʈ ����...
        Managers.Event.AddListener(Define.EVENT_TYPE.MousePointerEnter, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.MousePointerExit, this);


        Cursor.SetCursor(mouseCursorImage, Vector2.zero, CursorMode.Auto);
        GetImage((int)EImages.MouseCursorImage).enabled = false;

    }
    void Start()
    {
        Init();
    }

    void Update()
    {
        MouseFakeImage.anchoredPosition = Input.mousePosition;
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
                            GetText((int)ETexts.LeftTitleText).text = "<b><color=#006400>�̽���</color></b>";
                            GetText((int)ETexts.LeftContentsTitleText).text = "�ʺ� �÷��̾ ���� ���̵��Դϴ�. ���� ���� �̰� ������ ������ �������� �������� �������ϴ�.";
                            Get<GameObject>((int)EGameObjects.LeftBackGroundPannel).GetComponent<Image>().color = Color.green;
                            break;
                        case Define.EDifficulty.Normal:
                            GetText((int)ETexts.LeftTitleText).text = "<b><color=#FF4500>��ǳ��</color></b>";
                            GetText((int)ETexts.LeftContentsTitleText).text = "�� ������ ������ �ǵ���� �÷����մϴ�! ������ ������ ���� �Ƿ��� �����Ͻʽÿ�.";
                            Get<GameObject>((int)EGameObjects.LeftBackGroundPannel).GetComponent<Image>().color = Color.yellow;
                            break;
                        case Define.EDifficulty.Hard:
                            GetText((int)ETexts.LeftTitleText).text = "<b><color=#FF1493>���</color></b>";
                            GetText((int)ETexts.LeftContentsTitleText).text = "�ϵ��ھ� �÷��̾ ���� ���̵��Դϴ�. ���� ������ ����� ������ ���� �� ���Դϴ�. ������ �����Ͻʽÿ�.";
                            Get<GameObject>((int)EGameObjects.LeftBackGroundPannel).GetComponent<Image>().color = Color.magenta;
                            break;
                    }
                }
                else if (Sender.TryGetComponent(out CharacterSelectButton characterSelect))
                {
                    ActivePannel(EInteractionType.Character);
                    GetText((int)ETexts.RightTitleText).text = Managers.Data.CharacterDataDict[characterSelect.Charactercode].Name;
                    //ĳ���� ���� �̺��� ���ο� ���� �޸��� ���
                    if (Managers.Data.CharacterDataDict[characterSelect.Charactercode].isActive)
                    {
                        GetText((int)ETexts.RightContentsTitleText).text = Managers.Data.CharacterDataDict[characterSelect.Charactercode].script1;
                    }
                    else
                    {
                        GetText((int)ETexts.RightContentsTitleText).text = Managers.Data.CharacterDataDict[characterSelect.Charactercode].unlockscript2;
                    }

                }
                else if (Sender.TryGetComponent(out LoadSkillTempo Tempo))
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
