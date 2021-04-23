using UnityEngine;

namespace GameCore
{
    public class Disease : MonoBehaviour, IRaycastTarget
    {
        [SerializeField] protected string m_NameDisease;

        [SerializeField] protected float m_CurrentFill;
        [SerializeField] protected float m_MaxFill;

        protected Material m_Material;
        protected Patient m_Patient;


        private void Start() 
        {
            m_Material = GetComponent<MeshRenderer>().material;
        }


#region  Interface IRaycastTarget

        public void ApplyDamage(float damage)
        {
            ChangeFill(damage);
            
            if(m_CurrentFill >= m_MaxFill)
            {
                gameObject.SetActive(false);
            }

        }

#endregion

        private void ChangeFill(float count)
        {
            m_CurrentFill += count;
            m_CurrentFill = Mathf.Clamp(m_CurrentFill, 0.0f, m_MaxFill);

            m_Material.SetFloat("FillDisslove", m_CurrentFill / m_MaxFill);

        }

        public float GetPercentFill()
        {
            return m_CurrentFill / m_MaxFill;
        }

        public void ResetLevelFill()
        {
            m_CurrentFill = 0.0f;
            m_Material.SetFloat("FillDisslove", m_CurrentFill / m_MaxFill);
        }

    }
}
