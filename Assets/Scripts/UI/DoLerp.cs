using System.Collections;
using UnityEngine;

namespace SnakeMaze.UI
{
    public class DoLerp : MonoBehaviour
    {
        [SerializeField] private float unitsPerSecondInitial = .0f;
        [SerializeField] private float unitsPerSecondFinal = 2.0f;
        [SerializeField] private float timeToLerp = 1.0f;
        [SerializeField] private float stoppedTime = .1f;

        private float unitsPerSecond;
        private float timeToLerpCounter = 0;

        private void Start()
        {
            unitsPerSecond = unitsPerSecondInitial;
            StartCoroutine(LerpUnitsPerSecondRoutine());
        }

        private void Update() => gameObject.transform.Rotate(0, 0, -unitsPerSecond * Time.deltaTime);

        private IEnumerator LerpUnitsPerSecondRoutine()
        {
            while (true)
            {
                timeToLerpCounter = 0;
                while (timeToLerpCounter <= timeToLerp)
                {
                    Debug.Log("Aumentando");
                    unitsPerSecond = Mathf.Lerp(unitsPerSecond, unitsPerSecondFinal, timeToLerp * Time.deltaTime);
                    timeToLerpCounter += Time.deltaTime;

                    yield return null;
                }

                timeToLerpCounter = 0;
                while (timeToLerpCounter <= timeToLerp)
                {
                    Debug.Log("Disminuyendo");
                    unitsPerSecond = Mathf.Lerp(unitsPerSecond, unitsPerSecondInitial, timeToLerp * Time.deltaTime);
                    timeToLerpCounter += Time.deltaTime;

                    yield return null;
                }

                unitsPerSecond = unitsPerSecondInitial;
                yield return new WaitForSeconds(stoppedTime);
            }
        }
    }
}