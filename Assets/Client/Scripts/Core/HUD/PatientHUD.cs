using UnityEngine;
using UnityEngine.UI;

namespace GameCore
{
    public class PatientHUD : MonoBehaviour
    {
        [SerializeField] private Image m_ProgressImage;

        private Patient m_Patient;

        private void Start() 
        {
            m_Patient = FindObjectOfType<Patient>();

            if(m_Patient == null)
                new System.Exception(name + ": Patient not found, m_Patient is null");
        }

        private void Update() 
        {
            m_ProgressImage.fillAmount = m_Patient.GetCurrentPrecentPain();
        }
    }
}
