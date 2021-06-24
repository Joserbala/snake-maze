using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SnakeMaze.Utils
{
    public class AsyncSceneLoad : MonoBehaviour
    {
        [SerializeField] private Image fillImage;

        private void Start()
        {
            StartCoroutine(AsyncLoadScene());
        }

        private IEnumerator AsyncLoadScene()
        {
            yield return new WaitForSecondsRealtime(1f);
            AsyncOperation gameLevel = SceneManager.LoadSceneAsync("Gonzalo");
            while (!gameLevel.isDone)
            {
                var progress = gameLevel.progress / 0.9f;
                fillImage.fillAmount = progress;
                yield return null;
            }


        }
        
    }
}
