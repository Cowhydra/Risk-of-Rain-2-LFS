using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    private MeshRenderer _objectMesh;
    private Material _mat;
    [SerializeField] Material _highlightMaterial;

    [SerializeField] GameObject _bossPrefab;
    [SerializeField] Transform _spawnPoint;
    [SerializeField] ParticleSystem _bossRazer;

    private void Awake()
    {
        //bossRazer = GetComponent<ParticleSystem>();
        _objectMesh = GetComponent<MeshRenderer>();
        _mat = _objectMesh.material;
    }
    private void Start()
    {
        if (_bossRazer.isPlaying)
        {
            _bossRazer.Stop();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Transform bossSpawnPoint = _spawnPoint;

        if (other.CompareTag("Player"))
        {
            Debug.Log("����");
            Highlight();

            //UI�̺�Ʈ �߻�
            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionIn, this);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("�Է�");
                
                Instantiate(_bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);  //���ܿ��� eŰ�� ������ ������ ��ȯ�� ����
                BossRazer();

                //������ �����Ǹ� ������ ���� ���¸� ActiveTelePort�� �ٲپ� ���� UI�� ����!
                Managers.Game.GameState = Define.EGameState.ActiveTelePort;

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ResetHighlight();
            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionOut, this);
        }
    }

    private void BossRazer()
    {
        if (!_bossRazer.isPlaying)
        {
            _bossRazer.Play();
        }
    }

    private void Highlight()
    {
        _objectMesh.material = _highlightMaterial;
    }

    private void ResetHighlight()
    {
        _objectMesh.material = _mat;
    }
}