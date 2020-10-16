using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    Text score_Text;


    // Start is called before the first frame update
    void Start()
    {
        score_Text.text = "Score: 0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int score)
    {
        score_Text.text = "Score: " + score;
    }
}
