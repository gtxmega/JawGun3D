using UnityEngine;

namespace GameCore
{
    public class Gun : MonoBehaviour
    {
        [Header("Gun Attributes")]
        [SerializeField] private float m_Power;
        [SerializeField] private LayerMask m_TargetMask;

        [Header("VFX")]
        [SerializeField] private Transform m_GunDecalPartical;
        [SerializeField] private LineRenderer m_LineLiser;


        private Transform m_Transform;
        private Camera m_Camera;

        private GameMode m_GameMode;


#region  MonoBehavior Methods
        private void Start() 
        {
            m_Transform = GetComponent<Transform>();
            m_Camera = Camera.main;
            m_GameMode = FindObjectOfType<GameMode>();
        }

        private void Update() 
        {
            if(Input.GetMouseButton(0) && m_GameMode.LevelStart)
            {
                Ray _ray = m_Camera.ScreenPointToRay(Input.mousePosition);

                if(Physics.Raycast(_ray, out RaycastHit hitInfo, 30.0f, m_TargetMask))
                {
                    m_Transform.LookAt(hitInfo.point);

                    m_LineLiser.SetPosition(0, m_Transform.position);
                    m_LineLiser.SetPosition(1, hitInfo.point);
                    m_LineLiser.gameObject.SetActive(true);

                    IRaycastTarget target = hitInfo.collider.GetComponent<IRaycastTarget>();
                    
                    if(target != null)
                    {
                        m_GunDecalPartical.position = hitInfo.point;
                        m_GunDecalPartical.LookAt(m_Camera.transform.position);
                        m_GunDecalPartical.gameObject.SetActive(true);

                        target.ApplyDamage(m_Power);
                    }else
                    {
                        m_GunDecalPartical.gameObject.SetActive(false);
                    }

                }
            }else
            {
                m_LineLiser.gameObject.SetActive(false);
                m_GunDecalPartical.gameObject.SetActive(false);
            }
        }

#endregion


    }
}
