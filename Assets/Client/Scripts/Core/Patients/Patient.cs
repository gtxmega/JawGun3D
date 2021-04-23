using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    public class Patient : MonoBehaviour
    {
        [HideInInspector] public UnityEvent EventDeathPatient = new UnityEvent();

        public bool Death {get; private set;}

        [Header("Description")]
        [SerializeField] private string m_PatientName;

        [Header("Pain attributes")]
        [SerializeField] private float m_MaxLevelPain;
        [SerializeField] private float m_CurrentLevelPain;
        [SerializeField] private float m_AmountDecreasePain;

        [SerializeField] private float m_TimeDelayDecreasePain;
        private float currentTimerDelayDecrease;


        private int m_StartCountDiseases;
        private Disease[] m_CurrentDiseases;


#region  MonoBehavior Methods
        private void Start() 
        {
            m_CurrentDiseases = FindObjectsOfType<Disease>();
            m_StartCountDiseases = m_CurrentDiseases.Length;
        }


        private void Update() 
        {
            if(m_CurrentLevelPain > 0.0f)
            {
                if(currentTimerDelayDecrease > 0.0f)
                {
                    currentTimerDelayDecrease -= Time.deltaTime;
                }else
                {
                    m_CurrentLevelPain -= m_AmountDecreasePain * Time.deltaTime;
                }
            }
        }

#endregion


        public void ApplyPain(float amount)
        {   
            if(Death)
                return;

            m_CurrentLevelPain += amount;
            m_CurrentLevelPain = Mathf.Clamp(m_CurrentLevelPain, 0.0f, m_MaxLevelPain);

            RefreshTimerDelayDecrease();

            if(m_CurrentLevelPain >= m_MaxLevelPain)
            {
                Death = true;
                EventDeathPatient.Invoke();
            }


        }

        private void RefreshTimerDelayDecrease()
        {
            currentTimerDelayDecrease = m_TimeDelayDecreasePain;
        }

        public float GetCurrentPrecentPain()
        {
            return m_CurrentLevelPain / m_MaxLevelPain;
        }

        public int GetCurrentCountDiseases()
        {
            var count = 0;

            for(int i = 0; i < m_CurrentDiseases.Length; ++i)
            {
                if(m_CurrentDiseases[i].isActiveAndEnabled)
                    count++;
            }

            return count;
        }

        public int GetStartCountDiseases()
        {
            return m_StartCountDiseases;
        }

        public void ResetCurrentLevelPain()
        {
            m_CurrentLevelPain = 0.0f;
            currentTimerDelayDecrease = 0.0f;
            Death = false;
        }

    }

}
