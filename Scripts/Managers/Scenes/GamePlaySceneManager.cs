using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum PlayerType { PlayerPack, MachinGunPlayerPack, SniperPlayerPack, PistolPlayerPack }

public class GamePlaySceneManager : BaseSceneManager
{
    public GameObject Player { get; private set; }
    public Player PlayerClass { get; private set; }
    public PlayerConditions PlayerConditions { get; private set; }
    public GameObject Egg{ get; private set; }
    public EggEnemy EggEnemy { get; private set; }
    public int MonstersCount;
    private bool BossSeen = false;
    public bool pause = false;
    public GameObject inventoryManager { get; private set; }
    public GameObject tooltip { get; private set; }
    public ItemBoxPositions itemBoxPositions;
    public ItemBoxPositions monsterPositions;
    public ItemBoxPositions EggPositions;
    public GameObject itemBox { get; private set; }

    private Coroutine MonsterSpawnCoroutine;
    const int MONSTER_MAX_COUNT = 40;
    public List<int> avoidancePriorityList;

    public GetPlayerStatUICanvas checkPlayerStatUIActivation { get; private set; }

    private static GamePlaySceneManager _instance;

    public static GamePlaySceneManager Instance
    {
        get
        {
            if (_instance != null) { return _instance; }

            _instance = FindObjectOfType<GamePlaySceneManager>();
            if (_instance != null) { return _instance; }

            _instance = new GameObject(nameof(GamePlaySceneManager)).AddComponent<GamePlaySceneManager>();
            return _instance;
        }
    }

    private void Awake()
    {
        for(int i = 1; i < MONSTER_MAX_COUNT + 1; i++)
        {
            avoidancePriorityList.Add(i);
        }
    }

    protected override bool Init()
    {
        if (!base.Init()) return false;

        SceneType = Scenes.GamePlayScene;
        AudioManager.Instance.PlayMusic(SoundType.PlaySceneTheme);       
        SpawnObjects();
        UIManager.Instance.ShowSceneUI<Canv_InGameMenu>();
        //PlayerConditions.onDie += LoadEndingScene;
        EggEnemy.OnDestroy += SpawnBoss;

        return true;
    }

    private void SpawnObjects()
    {
        Player = Instantiate(Resources.Load<GameObject>($"Prefabs/Character/{Enum.GetName(typeof(PlayerType), GameManager.Instance.CharacterIdx)}"));
        PlayerConditions = Player.GetComponentInChildren<PlayerConditions>();
        PlayerClass = Player.GetComponentInChildren<Player>();
        int random = UnityEngine.Random.Range(0,EggPositions.boxDatas.Length);
        Egg = Instantiate(Resources.Load<GameObject>($"Prefabs/Monster/{MonsterDB.EggEnemy.ToString()}"), EggPositions.boxDatas[random].Positions, Quaternion.identity);
        EggEnemy = Egg.GetComponentInChildren<EggEnemy>();
        inventoryManager = Instantiate(Resources.Load<GameObject>("Prefabs/ItemBox/InventoryManager"));
        tooltip = Instantiate(Resources.Load<GameObject>("Prefabs/ItemBox/Tooltip Canvas"));

        itemBox = Resources.Load<GameObject>("Prefabs/ItemBox/Crate1");
        SpawnBox();
        MonsterSpawnCoroutine = StartCoroutine(MonsterSpawn());
        AudioManager.Instance.PlayMusic(SoundType.PlaySceneTheme);

        checkPlayerStatUIActivation = Player.GetComponentInChildren<GetPlayerStatUICanvas>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (checkPlayerStatUIActivation.isActivePlayerUICanvas)
        {
            checkPlayerStatUIActivation.ClosePlayerMenu();
        }

        else
        {
            if (!pause)
            {
                ShowGameMenu();
            }
            else
            {
                CloseGameMenu();
            }
        }
    }

    public void ShowGameMenu()
    {
        UIManager.Instance.ShowSceneUI<Canv_InGameMenu>();
        AudioManager.Instance.PlaySFX(SoundType.ButtonSound);
        Player.GetComponentInChildren<Player>()._playerInput.enabled = false;
        pause = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;

    }

    public void CloseGameMenu()
    {
        AudioManager.Instance.PlaySFX(SoundType.EndBtnSound);
        Player.GetComponentInChildren<Player>()._playerInput.enabled = true;
        Cursor.visible = false; // 커서를 숨김
        Cursor.lockState = CursorLockMode.Locked;
        pause = false;
        Time.timeScale = 1f;
        UIManager.Instance.CloseUI();

    }
    public void SpawnMonster(Vector3 spawnPosition, MonsterDB monsterName)
    {
        if (MonstersCount < MONSTER_MAX_COUNT)
        {
            MonstersCount++;
            NavMeshHit hit;
            GameObject monster = Resources.Load<GameObject>($@"Prefabs/Monster/{monsterName.ToString()}");
            if (NavMesh.SamplePosition(spawnPosition, out hit, 0.1f, NavMesh.AllAreas))
            {
                monster = ObjectPoolManager.Instance.GetObjectFromPool(monster, spawnPosition, Quaternion.identity);
            }
            else
            {
                bool isInstantiating = false;
                for (int i = 0; i < 30; i++)
                {
                    if (NavMesh.SamplePosition(spawnPosition, out hit, 10.0f, NavMesh.AllAreas))
                    {
                        isInstantiating = true;
                        Vector3 closestPoint = hit.position;
                        monster = ObjectPoolManager.Instance.GetObjectFromPool(monster, spawnPosition, Quaternion.identity);
                        break;
                    }
                }
                if(!isInstantiating) monster = ObjectPoolManager.Instance.GetObjectFromPool(monster, spawnPosition, Quaternion.identity);
            }
            if (!BossSeen)
            {
                NormalEnemy normalEnemy = monster.GetComponentInChildren<NormalEnemy>();
                if (normalEnemy != null)
                {
                    normalEnemy.SetAvoidancePriority(avoidancePriorityList[0]);
                    avoidancePriorityList.RemoveAt(0);
                }
            }
        }
    }
    public void SpawnMonsterPositions()
    {
        SpawnMonster(monsterPositions.boxDatas[UnityEngine.Random.Range(0, monsterPositions.boxDatas.Length)].Positions, (MonsterDB)(UnityEngine.Random.Range(4, 7)));
        for (int i = 0; i < monsterPositions.boxDatas.Length; i++)
        {
            SpawnMonster(monsterPositions.boxDatas[i].Positions, (MonsterDB)(UnityEngine.Random.Range(0, 4)));
        }
    }

    private IEnumerator MonsterSpawn()
    {
        WaitForSeconds wait = new WaitForSeconds(10);
        while (true)
        {
            if (MonstersCount < MONSTER_MAX_COUNT)
                SpawnMonsterPositions();

            yield return wait;
        }
    }

    private void SpawnBoss()
    {
        if (MonsterSpawnCoroutine != null)
        {
            StopCoroutine(MonsterSpawnCoroutine);
        }
        for(int i = 0; i < 7; i++)
        {
            GameObject monster = Resources.Load<GameObject>($@"Prefabs/Monster/{((MonsterDB)i).ToString()}");
            ObjectPoolManager.Instance.ReleaseAllObjectsInPool(monster);
        }
        MonstersCount = 0;
        Instantiate(Resources.Load<GameObject>($"Prefabs/Monster/{MonsterDB.Spider_Queen.ToString()}"), new Vector3(-10, 0, 10.5f), Quaternion.identity);
        BossSeen = true;
    }

    public void SpawnBox()
    {
        for (int i = 0; i < itemBoxPositions.boxDatas.Length; i++)
        {
            Instantiate(itemBox, itemBoxPositions.boxDatas[i].Positions, Quaternion.Euler(itemBoxPositions.boxDatas[i].Rotations));
        }

    }

    public override void Clear()
    {
        Time.timeScale = 1;
        pause = false;
        AudioManager.Instance.StopMusic();
        UIManager.Instance.CloseSceneUI();
        Cursor.lockState = CursorLockMode.None;

        if (MonsterSpawnCoroutine != null)
        {
            StopCoroutine(MonsterSpawnCoroutine);
        }
    }

    private void LoadEndingScene()
    {
        SceneLoadManager.Instance.LoadScene(Scenes.EndingCredit);
    }

    //esc키 누르면 뜨는 세팅창도 구현해야함
}
