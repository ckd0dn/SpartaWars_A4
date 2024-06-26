
using System.Collections.Generic;

using UnityEngine;

public class DataManager : Singleton<DataManager>

{    
    public int selectStage;                           // 현재 플레이 중인 스테이지 정보

    public List<UnitData> unitList = new();
    public List<UnitData> selectUnitList = new();

    public bool isGamePlay = false;

    [Header("업그레이드 데이터?")]

    public float maximumMana = 100;
    public int castleHP = 30;
    public float manaRegen = 1f;

    private readonly float increaseMaximumMana = 10;
    private readonly int increaseCastleHP = 30;
    private readonly float increaseManaRegen = 0.5f;

    private readonly float baseMaximumMana = 100;
    private readonly int baseCastleHP = 30;
    private readonly float baseManaRegen = 1f;
        
    [System.Serializable]
    public class UserData
    {
        public List<SlotData> upgradeData = new();

        [SerializeField] private int gold;
        public int Gold
        {
            get
            {
                gold = PlayerPrefs.GetInt("gold", 1000);
                return gold;
            }
            set
            {
                gold = value;
                PlayerPrefs.SetInt("gold", gold);
            }
        }


        [SerializeField]  private int bestUnlockStage;
        public int BestUnlockStage
        {
            get
            {
                bestUnlockStage = PlayerPrefs.GetInt("stageLevel", 1);
                return bestUnlockStage;
            }
            set
            {
                bestUnlockStage = value;
                PlayerPrefs.SetInt("stageLevel", bestUnlockStage);
            }
        }

    }

    public UserData userData = new();

    private void Start()
    {
        InitData();

        UpgradeApply();
       // LoadData();


    }

   /* public void SaveData()
    {
        string toJSON = JsonUtility.ToJson(userData);

        PlayerPrefs.SetString("Data", toJSON);
    }

    public void LoadData()
    {
        string fromJSON = PlayerPrefs.GetString("Data");

        if (fromJSON.Equals("") )
        {           
            InitData();
        }
        else
        {
            userData = JsonUtility.FromJson<UserData>(fromJSON);
        }

        UpgradeApply();
    }*/

    private void InitData()
    {
        userData.BestUnlockStage = 1;

        userData.Gold = 1000;

        //SaveData();

        EditorInit();
    }



    public void UpgradeApply()
    {
        foreach ( SlotData item in userData.upgradeData)
        {
            switch ( item.slotName )
            {
                case "성체력":
                    castleHP = baseCastleHP + ((item.level - 1) * increaseCastleHP);
                    break;
                case "최대 마나":
                    maximumMana = baseMaximumMana + ((item.level - 1) * increaseMaximumMana);
                    break;
                case "마나 재생력":
                    manaRegen = baseManaRegen + ((item.level - 1) * increaseManaRegen);
                    break;
                default: break;

            }
        }
    }

    private void EditorInit()
    {
        foreach (SlotData item in userData.upgradeData)
        {
            item.level = 1;
            item.gold = 100;
        }
    }

    private void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Q ) )
        {
            userData.BestUnlockStage = 4;
        }
    }
}


