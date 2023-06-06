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
        PlayerNameChangeButton

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
        userInputText,

    }
    enum GameObjects
    {
        PlayerNamePannel,
        NameInputField,
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));

        GetText((int)Texts.UserProfileText).text = $"������ : {_username}";
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
   



        GetButton((int)Buttons.UserProfileButton).gameObject.BindEvent
            ((PointerEventData data) => Get<GameObject>((int)GameObjects.PlayerNamePannel).gameObject.SetActive(true));
        GetButton((int)Buttons.PlayerNameChangeButton).gameObject
       .BindEvent((PointerEventData data) => SetPlayerName());


        Get<GameObject>((int)GameObjects.PlayerNamePannel).gameObject.SetActive(false);

    }
    private void SetPlayerName()
    {
        if (Get<GameObject>((int)GameObjects.NameInputField).GetComponent<TMP_InputField>().text != string.Empty && Get<GameObject>((int)GameObjects.NameInputField).GetComponent<TMP_InputField>().text.Length<5)
            {

                _username = Get<GameObject>((int)GameObjects.NameInputField).GetComponent<TMP_InputField>().text;
                GetText((int)Texts.UserProfileText).text = $"������ :{_username}";
            Get<GameObject>((int)GameObjects.PlayerNamePannel).gameObject.SetActive(false);

            }
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
