using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageUI : UI_Base
{
    private float moveSpeed;
    private float alphaSpeed;
    private float destroyTime;
    private Color alpha;
    public int Damage;
    enum Texts
    {
        DamageText,
    }
    public override void Init()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 2.0f;
        alphaSpeed = 2.0f;
        destroyTime = 2.0f;
        Bind<TextMeshProUGUI>(typeof(Texts));
        alpha = GetText((int)Texts.DamageText).color;
        GetText((int)Texts.DamageText).text=$"{Damage}";

        //������ƮǮ �ؾ��� ���߿� ������
        Managers.Resource.Destroy(gameObject, destroyTime);

    }

    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // �ؽ�Ʈ ��ġ

        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // �ؽ�Ʈ ���İ�
        GetText((int)Texts.DamageText).color = alpha;
    }
}
