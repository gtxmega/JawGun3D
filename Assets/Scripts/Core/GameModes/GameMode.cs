using UnityEngine;

namespace GameCore
{
    public class GameMode : MonoBehaviour
    {
        public bool LevelCompliteStatus {get; private set;}
        public bool LevelStart {get; private set;}


        [Header("UI")]
        [SerializeField] private GameObject[] m_StarsForLevel;
        [SerializeField] private GameObject m_FinalPanel;
        [SerializeField] private GameObject m_TextWonLevel;
        [SerializeField] private GameObject m_TextLoseLevel;

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
        }

        public void RestartLevel()
        {
            foreach(var item in m_AllDisease)
            {
                item.ResetLevelFill();
                item.gameObject.SetActive(true);
            }

            m_Patient.ResetCurrentLevelPain();

            m_CameraPath.ResetPosition();
            m_CameraPath.StartMove();

            m_FinalPanel.SetActive(false);

            LevelCompliteStatus = false;
            LevelStart = true;
        }

        private void GameWin()
        {
            ShowStarsForLevel();

            m_TextWonLevel.SetActive(true);
            m_TextLoseLevel.SetActive(false);

            m_FinalPanel.SetActive(true);
            LevelCompliteStatus = true;
        }

        private void GameOver()
        {
            m_CameraPath.StopMovement();

            ShowStarsForLevel();

            m_TextLoseLevel.SetActive(true);
            m_TextWonLevel.SetActive(false);

            m_FinalPanel.SetActive(true);
            LevelCompliteStatus = true;
            LevelStart = false;
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

    }

}
