using UnityEngine;

namespace GameCore
{
    public class Gum : MonoBehaviour, IRaycastTarget
    {
        [SerializeField] private string m_NameTeeth;

        private Patient m_Patient;

        private void Start() 
        {
            m_Patient = FindObjectOfType<Patient>();

            if(m_Patient == null)
                new System.Exception(name + ": not found Patient class, m_Patient is null");
        
        }

#region  Interface IRaycastTarget

        public void ApplyDamage(float damage)
        {
            m_Patient.ApplyPain(damage);
        }

#endregion
    }

}
