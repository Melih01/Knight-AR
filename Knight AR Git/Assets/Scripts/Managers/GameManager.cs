using GoogleARCore.Examples.HelloAR;
using UnityEngine;

public enum GamePlayMode
{
    Normal = 0,
    AR = 1
}

public class GameManager : CustomMonoBehaviour
{
    [Space]
    public ScreenUIController screenUIController;
    [Space]
    public ObjectPoolManager objectPoolManager;
    [Space]
    [SerializeField]
    float localScaleForAR = 0.5f;

    public float characterLocalScaleForAR
    {
        get
        {
            return gamePlayMode == GamePlayMode.AR ? localScaleForAR : 1;
        }
    }

    public static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public PlayerController playerController { get; set; }
    public EnemyController enemyController { get; set; }

    public GamePlayMode gamePlayMode { get; private set; }

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
    }

    void OnEnable()
    {
        if (!instance)
        {
            instance = this;
        }

        if (!screenUIController) screenUIController = FindObjectOfType<ScreenUIController>();
        if (!objectPoolManager) objectPoolManager = FindObjectOfType<ObjectPoolManager>();

        gamePlayMode = FindObjectOfType<GameController>() == null ? GamePlayMode.Normal : GamePlayMode.AR;
    }

    public void PlayAgain()
    {
        ReviveAllCharacters();
    }

    public void ReviveAllCharacters()
    {
        playerController.Revive();
        enemyController.Revive();
    }
}