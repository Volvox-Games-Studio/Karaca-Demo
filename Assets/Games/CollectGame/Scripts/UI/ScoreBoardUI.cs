using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Games.CollectGame.UI
{
    public class ScoreBoardUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private GameObject scoreObject;
        
        private IHaveScore m_Score;

        private void Awake()
        {
            m_Score = scoreObject.GetComponent<IHaveScore>();

            m_Score.OnScoreChanged += OnScoreChanged;
        }

        private void OnDestroy()
        {
            m_Score.OnScoreChanged -= OnScoreChanged;
        }
        
        private void OnScoreChanged(int value)
        {
            scoreText.text = $"Score: {value}";
        }



#if UNITY_EDITOR

        private void OnValidate()
        {
            if (scoreObject && !scoreObject.TryGetComponent(out IHaveScore _))
            {
                scoreObject = null;
                UnityEditor.Undo.RecordObject(this, $"Validate {name} values");
                Debug.LogWarning($"{this} doesn't implement {nameof(IHaveScore)}!");
            }
        }

#endif
    }
}