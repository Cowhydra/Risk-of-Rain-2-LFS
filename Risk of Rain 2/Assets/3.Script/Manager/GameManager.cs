using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager 
{
    private Define.EDifficulty _difficulty = Define.EDifficulty.Easy;
    private bool _isTelePortActive = false;
    private int _gold = 0;





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
        get { return _isTelePortActive;}
        set
        {
            _isTelePortActive = value;
           // ���� ��ȯ�� ��Ÿ�� �̺�Ʈ�� �� �ϴ� �ӽ�
           // ���� ���� �� ���ٰ� ���� ���� �̺�Ʈ���� ��� ��� ������������ ���
        }
    }
    public float PlayingTIme { get; set; } = 0;


}
