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

    [SerializeField] Animation _halfSphere;
    
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

            if (Managers.Game.GameState == Define.EGameState.NonTelePort)
            {

                Debug.Log("����");

                //UI�̺�Ʈ �߻�
                Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionIn, this);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("�Է� �� ���� ����");

                    Instantiate(_bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);  //���ܿ��� eŰ�� ������ ������ ��ȯ�� ����
                    BossRazer();
                    _halfSphere.Play();
                    //������ �����Ǹ� ������ ���� ���¸� ActiveTelePort�� �ٲپ� ���� UI�� ����!
                    Managers.Game.GameState = Define.EGameState.ActiveTelePort;
                    Managers.Event.PostNotification(Define.EVENT_TYPE.CameraShake, this);
                }
            }
            else if (Managers.Game.GameState == Define.EGameState.CompeleteTelePort && !Managers.Game.IsClear)
            {
                Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerInteractionIn, this);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("���ӽ¸�");

                    Managers.Game.IsClear = true;
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
