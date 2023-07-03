using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainValues : MonoBehaviour
{
   public int Score;
   public delegate void ScoreHandler(int value);
   public event ScoreHandler OnScoreChanged;

   public void SetScore(int value)
   {
      OnScoreChanged?.Invoke(value);
      Score = value;
   }
}
