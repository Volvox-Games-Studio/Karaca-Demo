using System;
using System.Collections;
using UnityEngine;

namespace Games.CollectGame
{
    public class ConfettiController : MonoBehaviour
    {
        [SerializeField] private GameObject[] _confetties;
        [SerializeField] private Countdown _countdown;
        


        private void Start()
        {
            ItemSpawner.OnPhaseCompleted += PhaseCompleted;
        }

        private void OnDestroy()
        {
            ItemSpawner.OnPhaseCompleted -= PhaseCompleted;
        }

        private void PhaseCompleted(int phase)
        {
            foreach (var confetty in _confetties)
            {
                confetty.SetActive(true);
            }

            StartCoroutine(CloseConfety());
        }

        private IEnumerator CloseConfety()
        {
            yield return new WaitForSeconds(2f);
            StartCoroutine(_countdown.GeriSayimiBaslat());
            foreach (var confetty in _confetties)
            {
                confetty.SetActive(false);
            }
        }
    }
}