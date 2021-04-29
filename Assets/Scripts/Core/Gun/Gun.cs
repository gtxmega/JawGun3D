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
                RaycastHit hitInfo;
                IRaycastTarget target = GetRaycastTarget(out hitInfo);
                
                if(target != null)
                {
                    m_Transform.LookAt(hitInfo.point);
                    
                    ShowVFX(hitInfo.point);

                    target.ApplyDamage(m_Power);
                }else
                {
                    HideVFX();
                }
            }else
            {
                HideVFX();
            }

        }

#endregion

        private IRaycastTarget GetRaycastTarget(out RaycastHit hitInfo)
        {
            Ray _ray = m_Camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(_ray, out hitInfo, 30.0f, m_TargetMask))
            {
                IRaycastTarget target = hitInfo.collider.GetComponent<IRaycastTarget>();
                if(target != null)
                {
                    return target;
                }
            }

            return null;
        }

        private void ShowVFX(Vector3 positionVFX)
        {
            m_LineLiser.SetPosition(0, m_Transform.position);
            m_LineLiser.SetPosition(1, positionVFX);
            m_LineLiser.gameObject.SetActive(true);

            m_GunDecalPartical.position = positionVFX;
            m_GunDecalPartical.LookAt(m_Camera.transform.position);
            m_GunDecalPartical.gameObject.SetActive(true);
        }

        private void HideVFX()
        {
            m_LineLiser.gameObject.SetActive(false);
            m_GunDecalPartical.gameObject.SetActive(false);
        }


    }
}
