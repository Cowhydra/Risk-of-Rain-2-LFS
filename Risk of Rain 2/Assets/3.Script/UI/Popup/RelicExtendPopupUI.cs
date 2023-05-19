using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class RelicExtendPopupUI : UI_Popup
{
    public EClickType MyType { get; set; }

    public enum EGameObjects
    {
        ClosePannel,
    }
   public enum EClickType
    {
        Relic,
        Extend,

    }
    enum ETexts
    {
        TitleText,
        TitleContentsText,
        DescribeText,

    }
    enum EButtons
    {
        ClosePopupButton,

    }
    public void SetText(EClickType clickType)
    {
        switch (clickType)
        {
            case EClickType.Relic:
                GetText((int)ETexts.TitleText).text = "����";
                GetText((int)ETexts.TitleContentsText).text = "<color=#8A2BE2>����</color>�� ���� �����̸� ũ�� �ڹٲ� ���� ���� ������ �Դϴ�. <color=#8A2BE2>����</color>�� ������� <color=#8A2BE2>��� ���</color>�� ������ �ֽ��ϴ�.\n ������ Ȱ��ȭ�Ǿ� �־ ������ ��� ������ �� �ֽ��ϴ�.";
                GetText((int)ETexts.DescribeText).text = "�̿� ������ ������ �����ϴ�.";
                break;
            case EClickType.Extend:
                GetText((int)ETexts.TitleText).text = "Ȯ����";
                GetText((int)ETexts.TitleContentsText).text = "�̹� ���ӿ��� ���� Ȯ������ ���˴ϴ�.";
                GetText((int)ETexts.DescribeText).text = "�̿� ������ Ȯ������ �����ϴ�.";
                break;
        }
    }
    public override void Init()
    {
        base.Init();
        Bind<TextMeshProUGUI>(typeof(ETexts));
        Bind<Button>(typeof(EButtons));
        Bind<GameObject>(typeof(EGameObjects));
        Get<GameObject>((int)EGameObjects.ClosePannel).BindEvent((PointerEventData data) => Managers.UI.ClosePopupUI());
        GetButton((int)EButtons.ClosePopupButton).gameObject
            .BindEvent((PointerEventData data) => Managers.UI.ClosePopupUI());
        SetText(MyType);

    }
    private void Start()
    {
        Init();
    }




}
