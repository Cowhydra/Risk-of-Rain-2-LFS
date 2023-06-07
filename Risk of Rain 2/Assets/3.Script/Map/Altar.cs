using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    [SerializeField] Material _outline;
    [SerializeField] Renderer _renderer;
    public List<Material> materialList = new List<Material>();

    [SerializeField] GameObject _bossPrefab;
    [SerializeField] Transform _spawnPoint;
    [SerializeField] ParticleSystem _bossRazer;

    private bool _isSpawn = false;

    private void Awake()
    {
        _renderer = this.GetComponent<Renderer>();
        SoundManager.instance.PlayBGM("Stage1Bgm");
    }

    private void Start()
    {
        if (_bossRazer.isPlaying)
        {
            _bossRazer.Stop();
        }
    }

    private void OnTriggerEnter(Collider other) // �ܰ��� ����
    {
        if (other.CompareTag("Player"))
        {
            materialList.Clear();
            materialList.AddRange(_renderer.sharedMaterials);
            materialList.Add(_outline);

            _renderer.materials = materialList.ToArray();
        }
    }
    private void OnTriggerStay(Collider other) 
    {
        Transform bossSpawnPoint = _spawnPoint;

        if (other.CompareTag("Player"))
        {
            Debug.Log("����");

            //UI�̺�Ʈ �߻�
            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionIn, this);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("�Է� �� ���� ����");

                if(!_isSpawn)
                {
                    Instantiate(_bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);  //���ܿ��� eŰ�� ������ ������ ��ȯ�� ����
                    BossRazer();

                    //������ �����Ǹ� ������ ���� ���¸� ActiveTelePort�� �ٲپ� ���� UI�� ����!
                    Managers.Game.GameState = Define.EGameState.ActiveTelePort;
                    _isSpawn = true;
                }

            }
        }
    }

    private void OnTriggerExit(Collider other) // �ܰ��� ����
    {
        if (other.CompareTag("Player"))
        {
            materialList.Clear();
            materialList.AddRange(_renderer.sharedMaterials);
            materialList.Remove(_outline);

            _renderer.materials = materialList.ToArray();

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

}
