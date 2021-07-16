using System.Collections;
using UnityEngine;

namespace SnakeMaze.UI
{
    public class DoLerp : MonoBehaviour
    {
        [SerializeField] private float unitsPerSecondInitial = 1.0f;
        [SerializeField] private float unitsPerSecondFinal = 2.0f;
        [SerializeField] private float minTimeToLerp = .5f;
        [SerializeField] private float maxTimeToLerp = 1.0f;
        [SerializeField] private float stoppedTime = .1f;

        private float unitsPerSecond;
        private float timeToLerp;

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
                timeToLerp = Random.Range(minTimeToLerp, maxTimeToLerp);
                while (unitsPerSecond <= unitsPerSecondFinal)
                {
                    Debug.Log("Aumentando");
                    unitsPerSecond = Mathf.Lerp(unitsPerSecond, unitsPerSecondFinal, timeToLerp * Time.deltaTime);

                    yield return null;
                }

                timeToLerp = Random.Range(minTimeToLerp, maxTimeToLerp);
                while (unitsPerSecond >= unitsPerSecondInitial)
                {
                    Debug.Log("Disminuyendo");
                    unitsPerSecond = Mathf.Lerp(unitsPerSecond, unitsPerSecondInitial, timeToLerp * Time.deltaTime);

                    yield return null;
                }

                yield return new WaitForSeconds(stoppedTime);
            }
        }
    }
}