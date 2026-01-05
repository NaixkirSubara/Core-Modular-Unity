using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MyStudio.Core.Architecture;

namespace MyStudio.Core.Systems
{
    public class SceneLoader : MonoSingleton<SceneLoader>
    {
        [Header("UI References")]
        [SerializeField] private GameObject _loadingScreen; 
        [SerializeField] private Slider _progressBar;      

        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
         
            if (_loadingScreen != null) _loadingScreen.SetActive(true);

     
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            
            //===========================================
            // Mencegah scene langsung muncul sebelum selesai 100% (opsional)
            //untuk sistem press to start
            //===========================================
            // operation.allowSceneActivation = false; 

            while (!operation.isDone)
            {
                // Update Progress Bar (Nilai 0.0 sampai 0.9)
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                
                if (_progressBar != null)
                    _progressBar.value = progress;

                yield return null;
            }

            // 3. Sembunyikan Loading Screen setelah selesai
            if (_loadingScreen != null) _loadingScreen.SetActive(false);
        }
    }
}