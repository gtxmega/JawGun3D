using System.Collections;
using UnityEngine;

namespace GameCore
{
    public class Teeth : MonoBehaviour, IRaycastTarget
    {
        [SerializeField] private float timeDelayApplyDamage;
        [SerializeField] private float timePainReduction;

        private float timerDelay;
        private float timerPainReduction;

        private Coroutine m_CoroutineTimerDelay;
        private Coroutine m_CoroutineTimerPain;

        private Patient m_Patient;


        private void Start() 
        {
            m_Patient = FindObjectOfType<Patient>();

            timerDelay = timeDelayApplyDamage;
        }

        public void ApplyDamage(float amount, Vector3 rayDirection)
        {
            if(timerDelay <= 0.0f)
            {
                if(timerPainReduction > 0.0f)
                {
                    m_Patient.ApplyPain(amount);
                    timerPainReduction = timePainReduction;
                }else
                {
                    if(m_CoroutineTimerPain == null)
                        m_CoroutineTimerPain = StartCoroutine(UpdateTimerPain());
                }
                
                
            }else
            {
                if(m_CoroutineTimerDelay == null)
                {
                    m_CoroutineTimerDelay = StartCoroutine(UpdateTimerDelay());
                }                
            }
        }

        private IEnumerator UpdateTimerDelay()
        {
            timerDelay = timeDelayApplyDamage;

            while(timerDelay >= 0.0f)
            {
                timerDelay -= Time.deltaTime;

                yield return null;
            }

            m_CoroutineTimerDelay = null;
        }

        private IEnumerator UpdateTimerPain()
        {
            timerPainReduction = timePainReduction;

            while(timerPainReduction >= 0.0f)
            {
                timerPainReduction -= Time.deltaTime;

                yield return null;
            }

            timerDelay = timeDelayApplyDamage;
            m_CoroutineTimerPain = null;
        }
    }

}
