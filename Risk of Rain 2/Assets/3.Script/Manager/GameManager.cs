
public class GameManager
{
    private Define.EDifficulty _difficulty = Define.EDifficulty.Easy;
    private bool _isTelePortActive = false;
    private int _gold = 0;
    private bool _isclear = false;



    #region ������ ��踦 ����
    private int _killCount = 0;
    private int _monsterDamaged = 0;
    private int _playerAttackedDamage = 0;
    public int KillCount
    {
        get { return _killCount; }
        set { _killCount = value; }
    }
    public int MonsterDamaged
    {
        get { return _monsterDamaged; }
        set { _monsterDamaged = value; }
    }
    public int PlayerAttackedDamage
    {
        get { return _playerAttackedDamage; }
        set { _playerAttackedDamage = value; }
    }

    //ĳ���Ͱ� ������ Ȥ�� Ŭ���� ������ �޼��ߴٸ� �ѹ��� �������ּ���!
    public bool IsClear
    {
        get { return _isclear; }
        set
        {
            _isclear = value;
            Managers.UI.ShowPopupUI<GameResultUI>();

        }
    }

    #endregion
    public Define.EDifficulty Difficulty
    {
        get { return _difficulty; }
        set
        {
            _difficulty = value;
            Managers.Event.DifficultyChange?.Invoke((int)_difficulty);
        }
    }
    public int Gold
    {
        get { return _gold; }
        set
        {
            _gold = value;
            Managers.Event.GoldChange?.Invoke(_gold);
        }
    }

    public int StageNumber { get; set; } = 1;
    public int StageLevel { get; set; } = 1;

    public int PlayerLevel { get; set; } = 1;
    public bool IsTelePortActive
    {
        get { return _isTelePortActive; }
        set
        {
            _isTelePortActive = value;
            // ���� ��ȯ�� ��Ÿ�� �̺�Ʈ�� �� �ϴ� �ӽ�
            // ���� ���� �� ���ٰ� ���� ���� �̺�Ʈ���� ��� ��� ������������ ���
        }
    }
    public float PlayingTIme { get; set; } = 0;


}