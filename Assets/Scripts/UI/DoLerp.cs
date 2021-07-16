using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeMaze.UI
{
    public class DoLerp : MonoBehaviour
    {
        [SerializeField] private float timeToLerp = 1.0f;

        private void Update()
        {
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.identity, timeToLerp * Time.deltaTime);
        }
    }
}