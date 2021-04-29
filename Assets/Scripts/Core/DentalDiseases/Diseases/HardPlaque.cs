using System.Collections;
using UnityEngine;

namespace GameCore
{
    [RequireComponent(typeof(Rigidbody))]
    public class HardPlaque : Disease
    {
        private Transform m_Transform;
        private Vector3 m_StartPosition;
        private Quaternion m_StartRotation;

        private Coroutine m_SelfDesctructionCoroutine;
        private Rigidbody m_RigidBody;


    #region  MonoBehavior Methods

            private void Start() 
            {
                m_Transform = GetComponent<Transform>();
                m_StartPosition = m_Transform.position;
                m_StartRotation = m_Transform.rotation;

                m_RigidBody = GetComponent<Rigidbody>();
                m_RigidBody.isKinematic = true;
            }

    #endregion

    #region Interface IRaycastTarget

            public override void ApplyDamage(float amount)
            {
                m_RigidBody.AddForce(m_Transform.up * 2.0f, ForceMode.Acceleration);

                if(m_SelfDesctructionCoroutine == null)
                    m_SelfDesctructionCoroutine = StartCoroutine(SelfDestruction());
            }

            private IEnumerator SelfDestruction()
            {
                m_RigidBody.isKinematic = false;

                yield return new WaitForSeconds(2.0f);

                m_RigidBody.isKinematic = true;
                gameObject.SetActive(false);

                m_SelfDesctructionCoroutine = null;
            }

    #endregion

    #region  Methods

            public override void ResetState()
            {
                m_Transform.position = m_StartPosition;
                m_Transform.rotation = m_StartRotation;
                m_RigidBody.isKinematic = true;

                base.ResetState();
            }

    #endregion

    }

}
