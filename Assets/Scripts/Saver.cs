using UnityEngine;

namespace Yuuta.Halloween
{
    public static class Saver
    {
        private const string BEST_SCORE_KEY = "BEST_SCORE";
        private const string SCORE_KEY = "SCORE";

        public static int GetBestScore()
            => PlayerPrefs.GetInt(BEST_SCORE_KEY, 0);

        public static int GetCurrentScore()
            => PlayerPrefs.GetInt(SCORE_KEY, 0);

        public static void SetScore(int score)
        {
            PlayerPrefs.SetInt(SCORE_KEY, score);
            
            var bestScore = GetBestScore();
            if (score > bestScore)
                PlayerPrefs.SetInt(BEST_SCORE_KEY, score);
        }
    }
}