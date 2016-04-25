using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{

    public Text labeLevellName;
    private Interactive.GameManager gameManager;

    void Awake()
    {
        Initialize();
    }

    void Start()
    {
        
    }


    public void Initialize()
    {
        gameManager = FindObjectOfType<Interactive.GameManager>();
        
    }

    public void ReturnToLevelSelection()
    {
        gameManager.ReturnToLevelSelection();
    }

    public void RestartGame()
    {
        gameManager.RestartGame();
    }

    public Color GetLevelUIColor()
    {
        if (gameManager.levelInfo.area == 2)
        {
            return Color.white;
        }
        else
            return Color.black;
    }
}
