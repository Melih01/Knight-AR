using UnityEngine;

public class GameResultUIController : CustomMonoBehaviour
{
    [Space]
    public UnityEngine.UI.Button playAgainButton;
    [Space]
    public GameObject victory;
    public GameObject defeat;

    void Awake()
    {
        HideGameResultUI();
    }

    void OnEnable()
    {
        playAgainButton.onClick.AddListener(PlayAgainUIButton);
    }

    void OnDisable()
    {
        playAgainButton.onClick.RemoveAllListeners();
    }

    public void ShowGameResultUI(bool isVictory)
    {
        victory.SetActive(isVictory);
        defeat.SetActive(!isVictory);

        playAgainButton.gameObject.SetActive(true);
    }

    public void HideGameResultUI()
    {
        victory.SetActive(false);
        defeat.SetActive(false);

        playAgainButton.gameObject.SetActive(false);
    }

    public void PlayAgainUIButton()
    {
        GameManager.instance.PlayAgain();
        HideGameResultUI();
    }
}
