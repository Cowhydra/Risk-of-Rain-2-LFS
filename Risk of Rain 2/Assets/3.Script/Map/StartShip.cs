using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartShip : MonoBehaviour
{
    [Header("������ �÷��̾�")]
    [SerializeField] GameObject _PlayerPrefab;
    [Header("���� ��ġ")]
    [SerializeField] Transform _spawnPoint;

    private Animation _doorOpen;
    private bool isStart = false;

    void Awake()
    {
        _doorOpen = GetComponent<Animation>();
    }


    void Update()
    {
        if (isStart == false && Input.GetKeyDown(KeyCode.E))
        {
            _doorOpen.Play();
            //Instantiate(_PlayerPrefab, _spawnPoint.position, _spawnPoint.rotation);
            isStart = true;
        }
    }
}
