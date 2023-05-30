using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameUI : UI_Game, IListener
{

    Color PrevSkillFillImageColor;
    string isnotActiveTeleport = "<b><color=#FF0000>�ڷ�����<u>(_)</u></color></b>�� ã�Ƽ� �����Ͻʽÿ�";
    string isActtiveTeleport = "������ óġ�Ͻʽÿ�!";
    string doneTeleporyEvent = "�ڸ���Ʈ�� ���ֽʽÿ�";
    public float RunTime = 0f;
    #region UI�⺻ ��ҵ� Bind
    enum Sliders
    {
        SkillM1,
        SkillM2,
        SkillShift,
        SkillR,
        SkillQ,

        ExpSlider,
        PlayerHpSlider,
        BossHpSlider,

    }
    enum Images
    {
        SkillM1FillImage,
        SkillM2FillImage,
        SkillShiftFillImage,
        SkillRFillImage,
        SkillQFillImage,

        StageImage,

        TeleCheckFalse,
        TeleCheckTrue,

    }
    enum Texts
    {
        SkillM1CoolTime,
        SkillM2CoolTime,
        SkillShiftCoolTime,
        SkillRCoolTime,
        SkillQCoolTime,


        GoldText,
        TimeText,
        StageNumber,
        StageLevel,

        ObjectContents,
        PlayerLevelText,

        PlayerHpText,
        BossHpText,
        InteractionKeyText,
        InteractionContentsText

    }
    enum GameObjects
    {
        GameItemPannel,
        DifficultyBackground,

        DifficultyCompass,

        BagItemPannel,
        EquipPannel,
        BossPannel,
        BagPannel,
        EscPannel,
        InteractionPannel,

    }
    enum Buttons
    {
        ContinueButton,
        ReturnToMenuButton,
        QuitButton,
    }
    #endregion
    public override void Init()
    {
        base.Init();
        Managers.Game.PlayingTIme = Time.time;
        Bind<GameObject>(typeof(GameObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Slider>(typeof(Sliders));
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));

        PrevSkillFillImageColor = GetImage((int)Images.SkillRFillImage).color;

        #region  �̺�Ʈ ����
        Managers.Event.DifficultyChange -= DifficultyImageChagngeEvent;
        Managers.Event.DifficultyChange += DifficultyImageChagngeEvent;
        Managers.Event.GoldChange -= GoldChangeEvent;
        Managers.Event.GoldChange += GoldChangeEvent;
        Managers.Event.EquipItemChange -= EquipChangeEvent;
        Managers.Event.EquipItemChange += EquipChangeEvent;


        Managers.Event.AddListener(Define.EVENT_TYPE.PlayerHpChange, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.BossHpChange, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.PlayerExpChange, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.PlayerUseSkill, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.PlayerInteractionIn, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.PlayerInteractionOut, this);

        #endregion
        InitTexts();
        InitImage();
        InitSlider();

        InitButton();
        InitGameObjects();

    }
    void Start()
    {
        Init();

    }

    public void InitGameObjects()
    {
        Get<GameObject>((int)GameObjects.DifficultyBackground).GetComponent<Rigidbody2D>()
       .velocity = 5 * Vector2.left;

        Get<GameObject>((int)GameObjects.BagItemPannel).SetActive(true);
        Get<GameObject>((int)GameObjects.BossPannel).SetActive(false);
        Get<GameObject>((int)GameObjects.EscPannel).SetActive(false);
        Get<GameObject>((int)GameObjects.InteractionPannel).SetActive(false);
        Get<GameObject>((int)GameObjects.BagPannel).SetActive(false);
    }
    private void InitTexts()
    {
        GetText((int)Texts.SkillM1CoolTime).text = "";
        GetText((int)Texts.SkillM2CoolTime).text = "";
        GetText((int)Texts.SkillShiftCoolTime).text = "";
        GetText((int)Texts.SkillRCoolTime).text = "";
        GetText((int)Texts.SkillQCoolTime).text = "";

        GetText((int)Texts.GoldText).text = $"{0}";
        GetText((int)Texts.StageNumber).text = $"�������� {1}";
        GetText((int)Texts.StageLevel).text = $"����. {1}";
        GetText((int)Texts.ObjectContents).text = $"{isnotActiveTeleport}";
        GetText((int)Texts.PlayerLevelText).text = $"{1}";


    }
    private void FixedUpdate()
    {
        RunTime = Time.time - Managers.Game.PlayingTIme;
        int minutes = (int)(RunTime / 60); // ��
        int seconds = (int)(RunTime % 60); // ��

        GetText((int)Texts.TimeText).text = $"{minutes:00}:{seconds:00}";


    }
    private void Update()
    {


        //E ��ư ������ Ȱ��/��Ȱ��
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Get<GameObject>((int)GameObjects.BagPannel).SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            Get<GameObject>((int)GameObjects.BagPannel).SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("���콺 ��ǲ ���� ī�޶� �������̰�");
            Get<GameObject>((int)GameObjects.EscPannel).SetActive(true);
            Time.timeScale = 0f;
        }
    }

    // ��ȣ�ۿ��� ������ �Ұ��ΰ��� ���� �̺�Ʈ ������� �ٸ��� �ҿ���?..
    //�ƹ����� �÷��̾�� ���� �ϴ°� ������
    private void SetCoolTime()
    {
    }
    private void InitButton()
    {
        GetButton((int)Buttons.ReturnToMenuButton).gameObject
            .BindEvent((PointerEventData data) => Debug.Log("�� ��ȯ"));
        GetButton((int)Buttons.ContinueButton).gameObject
           .BindEvent((PointerEventData data) => ResumeGame());
        GetButton((int)Buttons.QuitButton).gameObject
          .BindEvent((PointerEventData data) => Debug.Log("��������"));
    }
    private void ResumeGame()
    {
        Debug.Log("���콺 ��ǲ �ٽ� �ֱ� ī�޶� �����̰�");
        Get<GameObject>((int)GameObjects.EscPannel).SetActive(false);
        Time.timeScale = 1f;
    }
    private void InitSlider()
    {

        Get<Slider>((int)Sliders.SkillM1).value = 1;
        Get<Slider>((int)Sliders.SkillM2).value = 1;
        Get<Slider>((int)Sliders.SkillShift).value = 1;
        Get<Slider>((int)Sliders.SkillQ).value = 1;
        Get<Slider>((int)Sliders.SkillR).value = 1;
        Get<Slider>((int)Sliders.PlayerHpSlider).value = 1;
        Get<Slider>((int)Sliders.ExpSlider).value = 1;
        Get<Slider>((int)Sliders.BossHpSlider).value = 1;
    }
    private void InitImage()
    {
        GetImage((int)Images.SkillM1FillImage).color = PrevSkillFillImageColor;
        GetImage((int)Images.SkillM2FillImage).color = PrevSkillFillImageColor;
        GetImage((int)Images.SkillShiftFillImage).color = PrevSkillFillImageColor;
        GetImage((int)Images.SkillRFillImage).color = PrevSkillFillImageColor;
        GetImage((int)Images.SkillQFillImage).color = PrevSkillFillImageColor;

        GetImage((int)Images.TeleCheckTrue).enabled = false;
    }
    private void DifficultyImageChagngeEvent(int _)
    {
        GetImage((int)Images.StageImage).sprite = Managers.Resource.LoadSprte($"Difficultyicon{(int)Managers.Game.Difficulty + 1}");
        Debug.Log("���� ���̵� �� ���� ���� Sprite�� �ٸ��� �ϰ� ������ ���⿡ �߰����μ���");
        // (int)Manager.Game.Difficulty++1 + [ ���̵� ������] �ϸ� �ɵ�
    }
    private void EventOfSkill()
    {
        //Get<Slider>((int)Sliders.SkillM1).value = (float)���� �����ð�/ ��ų��Ÿ��;
        //Get<Slider>((int)Sliders.SkillM2).value = (float)���� �����ð�/ ��ų��Ÿ��;
        //Get<Slider>((int)Sliders.SkillShift).value = (float)���� �����ð�/ ��ų��Ÿ��;

        //Get<Slider>((int)Sliders.SkillR).value = (float)���� �����ð�/ ��ų��Ÿ��;
        //if (Get<Slider>((int)Sliders.SkillQ).value.CompareTo(0.95) > 0)
        //{
        //    Color color = Color.white;
        //    color.a = 0;
        //    GetImage((int)Images.SkillQFillImage).color = color;
        //}
    }
    //����� �ѹ� �� ����  ������ (�÷��̾� ����ġ, Hp �� ���� �̺�Ʈ�� ���������� ����) 
    private void EventOfPlayerHp()
    {
        // Get<Slider>((int)Sliders.PlayerHpSlider).value = 1;
        // Get<Slider>((int)Sliders.ExpSlider).value = 1;
    }
    private void EventOfPlayerExp()
    {

    }
    private void EventOfBossHp()
    {
        if (!Get<GameObject>((int)GameObjects.BossPannel).activeSelf)
        {
            Get<GameObject>((int)GameObjects.BossPannel).SetActive(true);
        }

    }
    private void GoldChangeEvent(int gold)
    {
        GetText((int)Texts.GoldText).text = $"{gold}";
    }
    private void EquipChangeEvent(int itemcode)
    {
        //���� ��ų Ŭ���� Ȥ�� ��ų���� �����ð� -> ��ų ��Ÿ������ �ʱ�ȭ
        // ��ü�ð� - ����ִ� �ð� -> ���� �ð� ǥ���� �� ������
        // //Get<Slider>((int)Sliders.SkillQ).value = (float)���� �����ð�/ ��ų��Ÿ��;

        //��ų ��Ÿ���� FixedUpadte���� ó���� ����

        Get<Slider>((int)Sliders.SkillQ).GetComponent<Image>()
             .sprite = Managers.Resource.LoadSprte($"{Managers.Data.ItemDataDict[itemcode].iconkey}");
        Get<GameObject>((int)GameObjects.EquipPannel).GetComponent<Image>()
            .sprite = Managers.Resource.LoadSprte($"{Managers.Data.ItemDataDict[itemcode].iconkey}");

    }
    private void InteractionInTextChangeEvent(Component _Sender)
    {
        Get<GameObject>((int)GameObjects.InteractionPannel).SetActive(true);
        if (_Sender.TryGetComponent(out ItemContainer _itemCOntainer))
        {
            GetText((int)Texts.InteractionKeyText).text = "E";
            if (Managers.Game.Gold > _itemCOntainer.Itemprice)
            {
                GetText((int)Texts.InteractionContentsText).text = $"���� ����($<color=#FFD700>{_itemCOntainer.Itemprice}</color>)";
            }
            else
            {
                GetText((int)Texts.InteractionContentsText).text = $"���� ����($<color=#FF0000>{_itemCOntainer.Itemprice}</color>)";
            }
            //�̷������� ó�� ������ �ᱹ �� ���ݿ� ���� �������� ��� (���ڸ� ���� ����) ��ü�� ���� ItemContainer���� ����
            // �ٸ� ��ȣ�ۿ� Ű�鵵 �������� ����� UI�� �����ϰ� ���۵��� �ش� class ������ ó���սô�.!!
        }
    }
    private void InteractionOutEvent()
    {
        Get<GameObject>((int)GameObjects.InteractionPannel).SetActive(false);
    }
    public void OnEvent(Define.EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        switch (Event_Type)
        {
            case Define.EVENT_TYPE.PlayerHpChange:
                EventOfPlayerHp();
                break;
            case Define.EVENT_TYPE.BossHpChange:
                EventOfBossHp();
                break;
            case Define.EVENT_TYPE.PlayerExpChange:
                EventOfPlayerExp();
                break;
            case Define.EVENT_TYPE.PlayerUseSkill:
                EventOfSkill();
                break;
            case Define.EVENT_TYPE.EnemyHpChange:
                //��ü�� ��ȭ SLider�� ���� ���ٴ�... �׳� �� ��ũ��Ʈ���� ó���ϵ��� �սô�.
                break;
            case Define.EVENT_TYPE.PlayerInteractionIn:
                InteractionInTextChangeEvent(Sender);
                //���� ���� ������� Sender�� ������Ʈ ( ������, ����� Ż��, �ڷ���Ʈ ) � ���� Text �ٸ��� ��� 
                break;
            case Define.EVENT_TYPE.PlayerInteractionOut:
                InteractionOutEvent();
                //���� ���� ������� Sender�� ������Ʈ ( ������, ����� Ż��, �ڷ���Ʈ ) � ���� Text �ٸ��� ��� 
                break;

        }
    }

}
