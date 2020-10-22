using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    Text score_Text;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Sprite[] _liveSprites;

    private bool _gameOver = false;
    


    // Start is called before the first frame update
    void Start()
    {
        
        score_Text.text = "Score: 0";
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameOver) {
            if (Input.GetKeyDown(KeyCode.R)) { 
                Scene currentScene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(currentScene.name);
            }
        }
    }

    public void UpdateScore(int score)
    {
        score_Text.text = "Score: " + score;
    }

    public void UpdateLives(int currentLives) {
        if (currentLives <= 3 && currentLives >= 0)
        {
            _livesImage.sprite = _liveSprites[currentLives];
        }
        else {
            _gameOver = true;
            DisplayGameOver();
        }
    }

    public void DisplayGameOver() {
        StartCoroutine(GameOverFlickerRoutine());
        _restartText.text = "Press 'R' to Restart";
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = "Game Over";
            yield return new WaitForSeconds(2.0f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.1f);
            _gameOverText.text = "Game Over";
            yield return new WaitForSeconds(0.3f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.1f);
            _gameOverText.text = "Game Over";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = ""; 
            yield return new WaitForSeconds(0.2f);
            _gameOverText.text = "Game Over";
        }
    }
}

