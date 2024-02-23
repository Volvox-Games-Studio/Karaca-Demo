using System;
using System.Collections;
using UnityEngine;

namespace Games.CollectGame
{
    public class ConfettiController : MonoBehaviour
    {
        [SerializeField] private GameObject[] _confetties;


        private void Start()
        {
            ItemSpawner.OnPhaseCompleted += PhaseCompleted;
        }

        private void PhaseCompleted()
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
            foreach (var confetty in _confetties)
            {
                confetty.SetActive(false);
            }
        }
    }
}