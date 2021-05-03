using System.Collections;
using UnityEngine;
using UnityEngine.Events;


namespace GameCore
{
    public class CameraPath : MonoBehaviour
    {
        [Header("Events")]
        public UnityEvent EventStartMove = new UnityEvent();
        public UnityEvent EventFinish = new UnityEvent();


        [Header("Path")]
        [SerializeField] private Transform[] m_PathPoints;

        [Header("Params")]
        [SerializeField] private float m_Speed;
        [SerializeField] private Transform m_FollowObject;


        private Transform m_CameraTransform;
        private Quaternion m_StartCameraRotation;


    #region MonoBehavior Methods

        private void Start() 
        {
            m_CameraTransform = Camera.main.transform;
            m_StartCameraRotation = m_CameraTransform.rotation;
        }

    #endregion


        public void StartMove()
        {

            if(m_PathPoints.Length > 0)
            {
                StartCoroutine(ProcessMove());
                EventStartMove.Invoke();
            }else
            {
                new System.Exception(name + ": not set m_PathPoints");
            }
        }

        private IEnumerator ProcessMove()
        {
            int _currentPoint = 0;

            while(_currentPoint < m_PathPoints.Length)
            {
                var direction = m_PathPoints[_currentPoint].position - m_FollowObject.position;
                var distance = direction.magnitude;
                var normalizeDirection = direction / distance;

                if(distance <= 1.0f)
                {
                    _currentPoint++;
                }else
                {
                    m_FollowObject.position += normalizeDirection * m_Speed * Time.deltaTime;
                }

                yield return null;
            }

            EventFinish.Invoke();
        }

        public void ResetPosition()
        {
            m_CameraTransform.rotation = m_StartCameraRotation;
            m_FollowObject.position = m_PathPoints[0].position;
        }

        public void StopMovement()
        {
            StopAllCoroutines();
        }


#if UNITY_EDITOR

        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.blue;

            if(m_PathPoints.Length > 0)
            {               
                for(int i = 0; i < m_PathPoints.Length; ++i)
                {
                    if((i + 1) < m_PathPoints.Length)
                        Gizmos.DrawLine(m_PathPoints[i].position, m_PathPoints[i + 1].position);
                }
            }
        }

#endif


    }


}
