using TMPro;
using UnityEngine;

public class ScoreSaveManager : MonoBehaviour
{
    
    public void SaveScore(int score)
    {
      if(PlayerPrefs.HasKey("Score"))
        {
            if(PlayerPrefs.GetInt("Score")<score)
            {
                PlayerPrefs.SetInt("Score", score);
            }
        }
        else
        {
            PlayerPrefs.SetInt("Score", score);
        }
    }

    
    public int LoadScore()
    {

        return PlayerPrefs.GetInt("Score", 0);



    }
  
}
