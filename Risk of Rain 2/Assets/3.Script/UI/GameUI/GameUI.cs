using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class GameUI : UI_Game, IListener
{

    Color PrevSkillFillImageColor;
    Color FullChargeSkillFillImageColor;


    public float RunTime = 0f;

    private CommandoSkill characterSkill;
    private PlayerStatus playerStatus;
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


        ItemInformationImage,

        TeleCheckTrue1,
        TeleCheckTrue2,

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

        ObjectContents1,
        ObjectContents2,

        PlayerLevelText,

        PlayerHpText,
        BossHpText,
        InteractionKeyText,
        InteractionContentsText,
        ItemInformationText,


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
        ItemInformationPannel,



        NoneTelePort,
        ActiveTelePort,


    }
    enum Buttons
    {
        ContinueButton,
        ReturnToMenuButton,
        QuitButton,
    }
    #endregion

    private void OnDisable()
    {
        Managers.Event.DifficultyChange -= DifficultyImageChagngeEvent;
        Managers.Event.GoldChange -= GoldChangeEvent;
        Managers.Event.EquipItemChange -= EquipChangeEvent;
        Managers.Event.GameStateChange -= GameGoalEvent;
        Managers.Event.AddItem -= ItemGainPannelEvent;
        Managers.Event.EquipItemChange -= ItemGainPannelEvent;
        Managers.Event.BossProgress -= BossEvent;
    }
    public override void Init()
    {
        base.Init();
        Managers.Game.PlayingTime = Time.time;
        Bind<GameObject>(typeof(GameObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Slider>(typeof(Sliders));
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));

        #region ��ų �� ĳ����
        characterSkill = FindObjectOfType<CommandoSkill>();
        PrevSkillFillImageColor = GetImage((int)Images.SkillRFillImage).color;
        FullChargeSkillFillImageColor = Color.clear;
        playerStatus = FindObjectOfType<PlayerStatus>();
        #endregion
        #region  �̺�Ʈ ����
        Managers.Event.DifficultyChange -= DifficultyImageChagngeEvent;
        Managers.Event.DifficultyChange += DifficultyImageChagngeEvent;
        Managers.Event.GoldChange -= GoldChangeEvent;
        Managers.Event.GoldChange += GoldChangeEvent;
        Managers.Event.EquipItemChange -= EquipChangeEvent;
        Managers.Event.EquipItemChange += EquipChangeEvent;
        Managers.Event.GameStateChange -= GameGoalEvent;
        Managers.Event.GameStateChange += GameGoalEvent;
        Managers.Event.AddItem -= ItemGainPannelEvent;
        Managers.Event.AddItem += ItemGainPannelEvent;
        Managers.Event.EquipItemChange -= ItemGainPannelEvent;
        Managers.Event.EquipItemChange += ItemGainPannelEvent;
        Managers.Event.BossProgress -= BossEvent;
        Managers.Event.BossProgress += BossEvent;

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

        EventOfPlayerHp(FindObjectOfType<PlayerStatus>().Health, FindObjectOfType<PlayerStatus>().MaxHealth);
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
        Get<GameObject>((int)GameObjects.ItemInformationPannel).SetActive(false);
        Get<GameObject>((int)GameObjects.ActiveTelePort).SetActive(false);

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
        GetText((int)Texts.ObjectContents1).text = $"{"<b><color=#FF0000>�ڷ�����<u>(_)</u></color></b>�� ã�Ƽ� �����Ͻʽÿ�"}";
        GetText((int)Texts.PlayerLevelText).text = $"{1}";
    }
    private void FixedUpdate()
    {
        RunTime = Time.time - Managers.Game.PlayingTime;
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

        EventOfSkill();
    }

    // ��ȣ�ۿ��� ������ �Ұ��ΰ��� ���� �̺�Ʈ ������� �ٸ��� �ҿ���?..
    //�ƹ����� �÷��̾�� ���� �ϴ°� ������

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
        Get<Slider>((int)Sliders.ExpSlider).value = 0;
        Get<Slider>((int)Sliders.BossHpSlider).value = 1;


    }
    private void InitImage()
    {
        GetImage((int)Images.SkillM1FillImage).color = FullChargeSkillFillImageColor;
        GetImage((int)Images.SkillM2FillImage).color = FullChargeSkillFillImageColor;
        GetImage((int)Images.SkillShiftFillImage).color = FullChargeSkillFillImageColor;
        GetImage((int)Images.SkillRFillImage).color = FullChargeSkillFillImageColor;
        GetImage((int)Images.SkillQFillImage).color = FullChargeSkillFillImageColor;
        GetImage((int)Images.TeleCheckTrue1).enabled = false;
        GetImage((int)Images.TeleCheckTrue2).enabled = false;
    }
    private void DifficultyImageChagngeEvent(int _)
    {
        GetImage((int)Images.StageImage).sprite = Managers.Resource.LoadSprte($"Difficultyicon{Mathf.Min((int)Managers.Game.Difficulty + 1,7)}");
        Debug.Log("���� ���̵� �� ���� ���� Sprite�� �ٸ��� �ϰ� ������ ���⿡ �߰����μ���");

    }
    private void EventOfSkill()
    {

        Get<Slider>((int)Sliders.SkillM2).value = characterSkill.GetPhaseRoundCooldownRemain() / characterSkill.PhaseRoundCooldown;
        if (Get<Slider>((int)Sliders.SkillM2).value.Equals(0))
        {
            GetImage((int)Images.SkillM2FillImage).color = FullChargeSkillFillImageColor;
            GetText((int)Texts.SkillM2CoolTime).text = string.Empty;
        }
        else
        {
            GetImage((int)Images.SkillM2FillImage).color = PrevSkillFillImageColor;
            GetText((int)Texts.SkillM2CoolTime).text = $"{characterSkill.GetPhaseRoundCooldownRemain():0.0}";
        }
        Get<Slider>((int)Sliders.SkillShift).value = characterSkill.GetTacticalDiveCooldownRemain() / characterSkill.TacticalDiveCooldown;
        if (Get<Slider>((int)Sliders.SkillShift).value.Equals(0))
        {
            GetImage((int)Images.SkillShiftFillImage).color = FullChargeSkillFillImageColor;

            GetText((int)Texts.SkillShiftCoolTime).text = "";
        }
        else
        {
            GetImage((int)Images.SkillShiftFillImage).color = PrevSkillFillImageColor;

            GetText((int)Texts.SkillShiftCoolTime).text = $"{characterSkill.GetTacticalDiveCooldownRemain():0.0}";
        }

        Get<Slider>((int)Sliders.SkillR).value = characterSkill.GetSuppressiveFireCooldownRemain() / characterSkill.SuppressiveFireCooldown;
        if (Get<Slider>((int)Sliders.SkillR).value.Equals(0))
        {
            GetImage((int)Images.SkillRFillImage).color = FullChargeSkillFillImageColor;

            GetText((int)Texts.SkillRCoolTime).text = "";
        }
        else
        {
            GetImage((int)Images.SkillRFillImage).color = PrevSkillFillImageColor;

            GetText((int)Texts.SkillRCoolTime).text = $"{characterSkill.GetSuppressiveFireCooldownRemain():0.0}";
        }
        Get<Slider>((int)Sliders.SkillQ).value = characterSkill.GetSkillQCooldownRemain() / characterSkill.SuppressiveFireCooldown;
        if (Get<Slider>((int)Sliders.SkillQ).value.Equals(0))
        {
            GetImage((int)Images.SkillQFillImage).color = FullChargeSkillFillImageColor;

            GetText((int)Texts.SkillQCoolTime).text = "";
        }
        else
        {
            GetImage((int)Images.SkillQFillImage).color = PrevSkillFillImageColor;

            GetText((int)Texts.SkillQCoolTime).text = $"{characterSkill.GetSkillQCooldownRemain():0.0}";
        }



        //R ��ų ��Ÿ�� ��ų �߰��ؾ� �� -> �÷��̾� ħ�� ����

    }

    //Player Hp �� ��� Entity ���� �������� �Ἥ set ������Ƽ ������ ���� �� ��� ��¿ �� ���� ACtion ���
    //
    private void EventOfPlayerHp(float currHp, float MaxHp)
    {
        Get<Slider>((int)Sliders.PlayerHpSlider).value =(int)currHp / MaxHp;
        GetText((int)Texts.PlayerHpText).text = $"{currHp}/{MaxHp}";

    }
    private void EventOfPlayerExp(float currentExp, float MaxExp, int level)
    {
        Get<Slider>((int)Sliders.ExpSlider).value = currentExp / MaxExp;
        GetText((int)Texts.PlayerLevelText).text = $"{level}";
    }
    private void EventOfBossHp(float currentHp, float MaxHp)
    {
        Get<Slider>((int)Sliders.BossHpSlider).value = currentHp / MaxHp;
        GetText((int)Texts.BossHpText).text = $"{currentHp}/{MaxHp}";
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
    //ȿ�� �������� �ڷ�ƾ? ���� �ؾ��ҵ�
    private void GameGoalEvent()
    {
        Get<GameObject>((int)GameObjects.ActiveTelePort).SetActive(false);
        Get<GameObject>((int)GameObjects.NoneTelePort).SetActive(false);
        GetImage((int)Images.TeleCheckTrue1).enabled = false;
        GetImage((int)Images.TeleCheckTrue2).enabled = false;

        switch (Managers.Game.GameState)
        {
            case Define.EGameState.NonTelePort:
                Get<GameObject>((int)GameObjects.NoneTelePort).SetActive(true);
                GetText((int)Texts.ObjectContents1).text = "<b><color=#FF0000>�ڷ�����<u>(_)</u></color></b>�� ã�Ƽ� �����Ͻʽÿ�";
                GetText((int)Texts.ObjectContents2).text = "";
                break;
            case Define.EGameState.ActiveTelePort:
                GetText((int)Texts.ObjectContents2).text = "<b>������ óġ�Ͻʽÿ�!</b>";
                GetText((int)Texts.ObjectContents1).text = $"<b><color=#FF0000>�ڷ�����<u>(_)</u></color></b>�� ���� �Ͻʽÿ�.({Managers.Game.ProgressBoss:00}%)";
                Get<GameObject>((int)GameObjects.BossPannel).SetActive(true);
                Get<GameObject>((int)GameObjects.ActiveTelePort).SetActive(true);
                break;
            case Define.EGameState.CompeleteTelePort:
                GetText((int)Texts.ObjectContents1).text = "�ڸ���Ʈ�� ���ʽÿ�";
                Get<GameObject>((int)GameObjects.ActiveTelePort).SetActive(false);
                Get<GameObject>((int)GameObjects.BossPannel).SetActive(false);
                break;
        }

    }

    private void BossEvent()
    {
        GetText((int)Texts.ObjectContents1).text = $"<b><color=#FF0000>�ڷ�����<u>(_)</u></color></b>�� ���� �Ͻʽÿ�.({Managers.Game.ProgressBoss:00}%)";
        if (Managers.Game.ProgressBoss > 100)
        {
            GetImage((int)Images.TeleCheckTrue1).enabled = true;
        }

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
        else if (_Sender.TryGetComponent(out Altar aitar))
        {
            GetText((int)Texts.InteractionKeyText).text = "E";
            switch (Managers.Game.GameState)
            {
                case Define.EGameState.NonTelePort:
                    GetText((int)Texts.InteractionContentsText).text = $"�ڷ����� ����...?";
                    break;
                case Define.EGameState.ActiveTelePort:
                    Get<GameObject>((int)GameObjects.InteractionPannel).SetActive(false);
                    break;
                case Define.EGameState.CompeleteTelePort:
                    GetText((int)Texts.InteractionContentsText).text = $"�ڷ����Ϳ� ���ʽÿ�";
                    break;
            }
        }
        else if (_Sender.TryGetComponent(out Item1025Skill item2025skill))
        {
            GetText((int)Texts.InteractionKeyText).text = "E";
            GetText((int)Texts.InteractionContentsText).text = $"�����ͳ� �̵�!";
        }
        else if( _Sender.TryGetComponent(out StartShip startship))
        {
            GetText((int)Texts.InteractionKeyText).text = "E";
            GetText((int)Texts.InteractionContentsText).text = $"Ż���� Ż��...";
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

            case Define.EVENT_TYPE.BossHpChange:
                if (Sender.TryGetComponent(out BeetleQueen boss))
                {
                    EventOfBossHp(boss.Health, boss.MaxHealth);
                }
                else
                {
                    Debug.Log(Sender.gameObject + "Not Found");
                }
                break;
            case Define.EVENT_TYPE.PlayerExpChange:
                if (Sender.TryGetComponent(out PlayerStatus _PlayerStatusExp))
                {
                    EventOfPlayerExp(_PlayerStatusExp.CurrentExp, _PlayerStatusExp.Exp, _PlayerStatusExp.Level);
                }
                else
                {
                    Debug.Log(Sender.gameObject + "Not Found");
                }
                //��ü�� ��ȭ SLider�� ���� ���ٴ�... �׳� �� ��ũ��Ʈ���� ó���ϵ��� �սô�.
                break;
            case Define.EVENT_TYPE.PlayerHpChange:
                if (Sender.TryGetComponent(out PlayerStatus _PlayerStatusHp))
                {
                    EventOfPlayerHp(_PlayerStatusHp.Health, _PlayerStatusHp.MaxHealth);
                }
                else
                {
                    Debug.Log(Sender.gameObject + "Not Found");
                }
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

    private void ItemGainPannelEvent(int n)
    {
        Get<GameObject>((int)GameObjects.ItemInformationPannel).SetActive(true);
        GetText((int)Texts.ItemInformationText).text = $"{Managers.Data.ItemDataDict[n].explanation}";
        GetImage((int)Images.ItemInformationImage).sprite = Managers.Resource.LoadSprte(Managers.Data.ItemDataDict[n].iconkey);

        StartCoroutine(nameof(ItemPannelFadeInout_co), Get<GameObject>((int)GameObjects.ItemInformationPannel).GetComponent<Image>());

    }


    private IEnumerator ItemPannelFadeInout_co(Image image)
    {
        float FameTime = 2.2f;
        image.color = Color.white;

        float currentTime = 0.0f;
        float percent = 0.0f;
        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / FameTime;
            Color color = image.color;
            color.a = Mathf.Lerp(1, 0, percent);
            image.color = color;
            yield return null;
        }

        Get<GameObject>((int)GameObjects.ItemInformationPannel).SetActive(false);
    }
}


    