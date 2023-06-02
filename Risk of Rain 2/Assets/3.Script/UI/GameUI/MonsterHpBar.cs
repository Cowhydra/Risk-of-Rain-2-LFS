using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpBar : UI_Base
{
    Entity _myentity;
    enum Sliders
    {
        HpBar,
    }

    private void Awake()
    {
        Init();   
    }
    public override void Init()
    {
        Bind<Slider>(typeof(Sliders));
        Debug.Log("���� ü�� ������Ʈ �����ͼ� value ���������");

    }

    private void OnEnable()
    {
       if(gameObject.transform.root.TryGetComponent(out Entity entity))
        {
            _myentity = entity;
        }
        else
        {
            Debug.Log($"{gameObject.transform.root} ���� Entity�� ������ �� ����");
        }
    
    }

    void Update()
    {
        Transform parent = transform.parent;
        transform.position=parent.position+Vector3.up*(parent.GetComponent<Collider>().bounds.size.y);
        Get<Slider>((int)Sliders.HpBar).value = _myentity.Health / _myentity.MaxHealth;
        //�θ��� ü���� ����ͼ� ������Ʈ ���ָ��
    }
}
