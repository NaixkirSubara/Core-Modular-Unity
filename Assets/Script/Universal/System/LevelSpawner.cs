using System.Collections.Generic;
using UnityEngine;
using MyStudio.Core.Architecture;

namespace MyStudio.Core.Systems
{
    public class LevelSpawner : MonoSingleton<LevelSpawner>
    {
        [Header("Level Collection")]
        public List<GameObject> levelPrefabs; 

        private GameObject _currentLevelObj;
        private int _currentLevelIndex = 0;
        
        public void LoadLevel(int index)
        {
           
            if (_currentLevelObj != null)
            {
                Destroy(_currentLevelObj); 
            }

      
            if (index >= 0 && index < levelPrefabs.Count)
            {
                _currentLevelObj = Instantiate(levelPrefabs[index], Vector3.zero, Quaternion.identity);
                _currentLevelIndex = index;
            }
            else
            {
                Debug.LogError("Level index tidak ditemukan!");
            }
        }

        public void NextLevel()
        {
            LoadLevel(_currentLevelIndex + 1);
        }

        public void RestartLevel()
        {
            LoadLevel(_currentLevelIndex);
        }
    }
}