using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace GameCore
{
    public class GameMode : MonoBehaviour
    {
    #region  UNITY EVENTS
        public UnityEvent EventLevelStart = new UnityEvent();
        public UnityEvent EventLevelWin = new UnityEvent();
        public UnityEvent EventLevelLose = new UnityEvent();
    #endregion

        public bool LevelCompliteStatus {get; private set;}
        public bool LevelStart {get; private set;}


        [Header("UI")]
        [SerializeField] private GameObject[] m_StarsForLevel;
        [SerializeField] private GameObject m_FinalPanel;
        [SerializeField] private Text m_TextFinishLevel;


        [Header("Start UI")]
        [SerializeField] private GameObject m_StartPanel;


        private CameraPath m_CameraPath;
        private Patient m_Patient;
        private Disease[] m_AllDisease;


#region  MonoBehavior Methods
        private void Start() 
        {
            m_CameraPath = FindObjectOfType<CameraPath>();
            m_Patient = FindObjectOfType<Patient>();

            m_AllDisease = FindObjectsOfType<Disease>();


            m_CameraPath.EventFinish.AddListener(GameWin);
            m_Patient.EventDeathPatient.AddListener(GameOver);
        }

        private void OnEnable() 
        {
            if(m_Patient != null)
                m_Patient.EventDeathPatient.AddListener(GameOver);

            if(m_CameraPath != null)
                m_CameraPath.EventFinish.AddListener(GameWin);
        }

        private void OnDisable() 
        {
            if(m_Patient != null)
                m_Patient.EventDeathPatient.RemoveListener(GameOver);

            if(m_CameraPath != null)
                m_CameraPath.EventFinish.RemoveListener(GameWin);
        }


#endregion

        public void StartLevel()
        {
            m_CameraPath.ResetPosition();

            m_StartPanel.SetActive(false);

            m_CameraPath.StartMove();

            LevelCompliteStatus = false;
            LevelStart = true;

            EventLevelStart.Invoke();
        }

        public void RestartLevel()
        {
            foreach(var item in m_AllDisease)
            {
                item.ResetState();
            }

            m_Patient.ResetCurrentLevelPain();

            m_CameraPath.ResetPosition();
            m_CameraPath.StartMove();

            m_FinalPanel.SetActive(false);

            LevelCompliteStatus = false;
            LevelStart = true;

            EventLevelStart.Invoke();
        }

        private void GameWin()
        {
            ShowGameEndInfo("Level Completed");
            EventLevelWin.Invoke();
        }

        private void GameOver()
        {
            m_CameraPath.StopMovement();

            ShowGameEndInfo("Lose");
            EventLevelLose.Invoke();
        }

        private void ShowStarsForLevel()
        {
            var countStars = 0;
            var startDiseases = m_Patient.GetStartCountDiseases();
            var currentDiseases = m_Patient.GetCurrentCountDiseases();

            if(currentDiseases == 0)
                countStars = 3;

            if(currentDiseases ==  startDiseases)
            {
                countStars = 0;
            }else if(currentDiseases < (startDiseases / 2) && currentDiseases > 0)
            {
                countStars = 2;
            }else if(currentDiseases >=  (startDiseases / 2))
            {
                countStars = 1;
            }

            for(int i = 0; i < m_StarsForLevel.Length; ++i)
            {
                if(i < countStars)
                {
                    m_StarsForLevel[i].SetActive(true);
                }else
                {
                    m_StarsForLevel[i].SetActive(false);
                }
            }
        }

        public void ShowGameEndInfo(string textEndLevel)
        {
            ShowStarsForLevel();

            m_TextFinishLevel.text = textEndLevel;
            
            SetLevelStatus();

            m_FinalPanel.SetActive(true);
        }

        private void SetLevelStatus()
        {
            LevelCompliteStatus = true;
            LevelStart = false;
        }

    }

}
