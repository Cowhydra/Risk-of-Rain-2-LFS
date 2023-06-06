using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainUI : UI_Scene
{
    [SerializeField]
    private string _username = "Noname";

    enum Buttons
    {
        GameStartButton,
        DicitonaryButton,
        MusicButton,
        SettingButton,
        QuitButton,
        UserProfileButton,

    }
    enum Images
    {
        BackGround,
        MainTitle,

    }
    enum Texts
    {
        UserProfileText,
        GameStartText,
        DicitonaryText,
        MusicText,
        SettingText,
        QuitText,

    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));

        GetText((int)Texts.UserProfileText).text = $"������ :{_username}";
        GetText((int)Texts.GameStartText).text = $"���ӽ���";
        GetText((int)Texts.DicitonaryText).text = $"�α׺�";
        GetText((int)Texts.MusicText).text = $"����";
        GetText((int)Texts.SettingText).text = $"����";
        GetText((int)Texts.QuitText).text = $"����ũ������ ������";

        Debug.Log("���� ���� �ϸ� ������ ����� ������ ��!");
        SoundManager.instance.PlayBGM("MainBgm");
        GetButton((int)Buttons.GameStartButton).gameObject
            .BindEvent((PointerEventData data) => GameStartEvent());
        GetButton((int)Buttons.DicitonaryButton).gameObject
            .BindEvent((PointerEventData data) => ShowLogBook());
        GetImage((int)Images.BackGround).gameObject.SetActive(false);
    }
    private void Start()
    {
        Init();
    }
    private void GameStartEvent()
    {
        Debug.Log("���� ���� ��ư ������ ���� �Ҹ� ����");
        SoundManager.instance.PlaySE("MenuClick");
        TurnOnandOffLog();
        Managers.UI.ShowSceneUI<GameStartUI>();

    }
    private void ShowLogBook()
    {
        Debug.Log("�α׺� ��ư ������ ���� �Ҹ� ����");
        SoundManager.instance.PlaySE("MenuClickLog");
        Managers.UI.ShowSceneUI<LogBook>();
    }
    public void TurnOnandOffLog()
    {
        if (GetImage((int)Images.MainTitle).enabled)
        {
            GetImage((int)Images.MainTitle).enabled = false;
        }
        else
        {
            GetImage((int)Images.MainTitle).enabled = true;
        }
    }


}
