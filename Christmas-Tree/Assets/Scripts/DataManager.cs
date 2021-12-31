using UnityEngine;

public static class DataManager
{
    private const string BestScoreKey = "best_score";

    public static float BestScore
    {
        get => PlayerPrefs.GetFloat(BestScoreKey);
        
        set
        {
            PlayerPrefs.SetFloat(BestScoreKey, value);
            PlayerPrefs.Save();
        }
    }

}