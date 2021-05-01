using UnityEngine;

namespace GameCore
{
    public class ShardController : MonoBehaviour
    {
        [SerializeField] private float m_ShardsForceExplosions = 200.0f;

        private Transform[] m_ShardTransforms;
        private Vector3[] m_ShardStartPositions;
        private Quaternion[] m_ShardStartRotations;

        private Rigidbody[] m_ShardRigidBodies;

        public void Initialize(Rigidbody[] shards)
        {
            m_ShardTransforms = new Transform[shards.Length];
            m_ShardStartPositions = new Vector3[shards.Length];
            m_ShardStartRotations = new Quaternion[shards.Length];
            m_ShardRigidBodies = new Rigidbody[shards.Length];

            for(int i = 0; i < shards.Length; ++i)
            {
                m_ShardTransforms[i] = shards[i].transform;

                m_ShardStartPositions[i] = m_ShardTransforms[i].position;
                m_ShardStartRotations[i] = m_ShardTransforms[i].rotation;

                m_ShardRigidBodies[i] = shards[i];
            }
        }

        public void ShowShards(Vector3 applyForce)
        {
            for(int i = 0; i < m_ShardRigidBodies.Length; ++i)
            {
                m_ShardRigidBodies[i].isKinematic = false;
                m_ShardTransforms[i].gameObject.SetActive(true);

                m_ShardRigidBodies[i].AddExplosionForce(m_ShardsForceExplosions, transform.position, 0.0f, 1.0f);
            }
        }

        public void ResetState()
        {
            for(int i = 0; i < m_ShardTransforms.Length; ++i)
            {
                m_ShardTransforms[i].gameObject.SetActive(false);

                m_ShardRigidBodies[i].isKinematic = true;

                m_ShardTransforms[i].position = m_ShardStartPositions[i];
                m_ShardTransforms[i].rotation = m_ShardStartRotations[i];

            }
        }
    }

}
