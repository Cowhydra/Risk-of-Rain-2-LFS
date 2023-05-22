using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainUI : UI_Scene
{
    [SerializeField]
    private string _username = "Noname";
    GameObject MouseCursor;


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


        GetText((int)Texts.UserProfileText).text = $"������ :{_username}";
        GetText((int)Texts.GameStartText).text = $"���ӽ���";
        GetText((int)Texts.DicitonaryText).text = $"�α׺�";
        GetText((int)Texts.MusicText).text = $"����";
        GetText((int)Texts.SettingText).text = $"����";
        GetText((int)Texts.QuitText).text = $"����ũ������ ������";

        Debug.Log("���� ���� �ϸ� ������ ����� ������ ��!");
        GetButton((int)Buttons.GameStartButton).gameObject
            .BindEvent((PointerEventData data) => GameStartEvent());
        GetButton((int)Buttons.DicitonaryButton).gameObject
            .BindEvent((PointerEventData data) => ShowLogBook());
    }
    private void Start()
    {
        Init();
    }
    private void GameStartEvent()
    {
        Debug.Log("���� ���� ��ư ������ ���� �Ҹ� �����!");

        Managers.UI.ShowSceneUI<GameStartUI>();

    }
    private void ShowLogBook()
    {
        Debug.Log("�α׺� ��ư ������ ���� �Ҹ� �����!");
        Managers.UI.ShowSceneUI<LogBook>();
    }


}
