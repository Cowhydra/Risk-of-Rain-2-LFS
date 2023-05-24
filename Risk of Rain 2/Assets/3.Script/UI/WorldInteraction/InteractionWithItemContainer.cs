using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionWithItemContainer : UI_Scene
{

    private void Start()
    {
        gameObject.transform.GetChild(0).localPosition= Vector3.zero;
        Debug.Log("���ϴ� ��ġ�� UI �Űܳ��� ��� ");
        Debug.Log(" �� ������ ��ȣ�ۿ��� ĳ���� ������ ��Ÿ���Ƿ�, ĳ���� �������� Bownder+ �ϵ��ڵ� �� A ���ؼ� ��Ÿ���� �ɵ�? ");

    }

    void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y);
        transform.rotation = Camera.main.transform.rotation;

        gameObject.transform.GetChild(0).transform.localScale = 5*Vector3.one * (1 / parent.localScale.x);
    }
}
