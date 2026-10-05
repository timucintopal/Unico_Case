using UnityEngine;

namespace Main.Scripts
{
    public static class SaveSystem
    {
        public static int LevelIndex
        {
            get => PlayerPrefs.GetInt(Constants.LevelIndexKey, 0);
            set => PlayerPrefs.SetInt(Constants.LevelIndexKey, value);
        }
    }
}