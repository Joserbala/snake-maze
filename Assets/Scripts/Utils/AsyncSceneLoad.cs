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
            AsyncOperation gameLevel = SceneManager.LoadSceneAsync("Gonzalo");
            while (gameLevel.progress<1)
            {
                fillImage.fillAmount = gameLevel.progress;
                yield return new WaitForEndOfFrame();
            }


        }
        
    }
}
