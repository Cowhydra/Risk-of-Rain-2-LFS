using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : UI_Scene,IListener
{

    Color PrevSkillFillImageColor;
    string isnotActiveTeleport = "<b><color=#FF0000>�ڷ�����<u>(_)</u></color></b>�� ã�Ƽ� �����Ͻʽÿ�";
    string isActtiveTeleport = "������ óġ�Ͻʽÿ�!";
    string doneTeleporyEvent = "�ڸ���Ʈ�� ���ֽʽÿ�";

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

    }
    enum GameObjects
    {
        GameItemPannel,
        DifficultyBackground,

        DifficultyCompass,

        BossPannel,
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

        PrevSkillFillImageColor = GetImage((int)Images.SkillRFillImage).color;
        Get<GameObject>((int)GameObjects.DifficultyBackground).GetComponent<Rigidbody2D>()
            .AddForce(new Vector2(-50f, 0));
        Get<GameObject>((int)GameObjects.BossPannel).SetActive(false);

        #region  �̺�Ʈ ����
        Managers.Event.DifficultyChange -= DifficultyImageChagngeEvent;
        Managers.Event.DifficultyChange += DifficultyImageChagngeEvent;
        Managers.Event.GoldChange -= GoldChangeEvent;
        Managers.Event.GoldChange += GoldChangeEvent;



        Managers.Event.AddListener(Define.EVENT_TYPE.PlayerHpChange, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.BossHpChange, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.PlayerExpChange, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.PlayerUseSkill, this);

        #endregion
        InitTexts();
        InitImage();
        InitSlider();

    }
    void Start()
    {
        Init();
        
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
        float time = Time.time - Managers.Game.PlayingTIme;
        int minutes = (int)(time / 60); // ��
        int seconds = (int)(time % 60); // ��

        GetText((int)Texts.TimeText).text = $"{minutes:00}:{seconds:00}";
    }


    // ��ȣ�ۿ��� ������ �Ұ��ΰ��� ���� �̺�Ʈ ������� �ٸ��� �ҿ���?..
    //�ƹ����� �÷��̾�� ���� �ϴ°� ������
    private void SetCoolTime()
    {
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
    private void DifficultyImageChagngeEvent()
    {
        GetImage((int)Images.StageImage).sprite = Managers.Resource.LoadSprte($"Difficultyicon{(int)Managers.Game.Difficulty+1}");
    }
    private void EventOfSkill()
    {
        //Get<Slider>((int)Sliders.SkillM1).value = (float)���� �����ð�/ ��ų��Ÿ��;
        //Get<Slider>((int)Sliders.SkillM2).value = (float)���� �����ð�/ ��ų��Ÿ��;
        //Get<Slider>((int)Sliders.SkillShift).value = (float)���� �����ð�/ ��ų��Ÿ��;
        //Get<Slider>((int)Sliders.SkillQ).value = (float)���� �����ð�/ ��ų��Ÿ��;
        //Get<Slider>((int)Sliders.SkillR).value = (float)���� �����ð�/ ��ų��Ÿ��;

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

    public void OnEvent(Define.EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
       switch(Event_Type)
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

        }
    }
}
