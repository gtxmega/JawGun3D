using UnityEngine;

namespace GameCore
{
    public class SoftPlaque : Disease
    {
        [SerializeField] private float m_CurrentFill;
        [SerializeField] private float m_MaxFill;

        private Material m_Material;
        private Patient m_Patient;


        private void Start() 
        {
            m_Material = GetComponent<MeshRenderer>().material;
        }


#region  Interface IRaycastTarget

        public override void ApplyDamage(float damage, Vector3 rayDirection)
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

        public override void ResetState()
        {
            m_CurrentFill = 0.0f;
            m_Material.SetFloat("FillDisslove", m_CurrentFill / m_MaxFill);

            base.ResetState();
        }

    }

}
