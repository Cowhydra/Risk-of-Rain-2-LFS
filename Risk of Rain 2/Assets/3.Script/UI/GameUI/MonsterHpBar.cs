using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpBar : UI_Base
{
    enum Sliders
    {
        HpBar,
    }
    public override void Init()
    {
       

    }

    // Start is called before the first frame update
    void Start()
    {
        Bind<Slider>(typeof(Sliders));
        Debug.Log("���� ü�� ������Ʈ �����ͼ� value ���������");
    }

    void Update()
    {
        Transform parent = transform.parent;
        transform.position=parent.position+Vector3.up*(parent.GetComponent<Collider>().bounds.size.y);

        //�θ��� ü���� ����ͼ� ������Ʈ ���ָ��
    }
}
