using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
   [SerializeField] Transform highScoreEntry;
    
   [SerializeField] Transform highScoreTemplate;
    [SerializeField] float offset;
    // Start is called before the first frame update
    void Start()
    {
        highScoreTemplate.gameObject.SetActive(false);
        for (int i = 0; i < 10; i++)
        {
            Transform entry = Instantiate(highScoreTemplate, highScoreEntry);
            RectTransform entryRect = entry.GetComponent<RectTransform>();
            entryRect.anchoredPosition = new Vector2(0, -offset * i);
            highScoreTemplate.gameObject.SetActive(true);

            int position = i + 1;
            entry.Find("Position Text").GetComponent<Text>().text = position.ToString();
            int score = 1199;
            entry.Find("Score Text").GetComponent<Text>().text = score.ToString();
            string name = "AAA";
            entry.Find("Name Text").GetComponent<Text>().text = name;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

class HighScore
{
    public int score;
    public string name;
}
