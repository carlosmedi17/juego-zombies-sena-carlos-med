using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public float restartDelay = 2f;

    public bool IsGameOver { get; private set; }

    void Start()
    {
        IsGameOver = false;

        if (gameOverText != null) gameOverText.gameObject.SetActive(false);
    }

    public void LoseGame()
    {
        if (IsGameOver) return;

        IsGameOver = true;

        if (gameOverText != null)
        {
            gameOverText.text = "HAS PERDIDO";
            gameOverText.gameObject.SetActive(true);
            
        }

        StartCoroutine(RestartLevel());
    }
    
    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(restartDelay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
