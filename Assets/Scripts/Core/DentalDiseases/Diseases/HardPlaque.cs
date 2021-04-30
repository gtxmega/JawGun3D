using UnityEngine;

namespace GameCore
{
    [RequireComponent(typeof(ShardController))]
    public class HardPlaque : Disease
    {
        [SerializeField] private Rigidbody[] m_Shards;


        private ShardController m_ShardController;

        private Coroutine m_SelfDesctructionCoroutine;


    #region  MonoBehavior Methods

        private void Start() 
        {
            m_ShardController = GetComponent<ShardController>();
            m_ShardController.Initialize(m_Shards);
        }

    #endregion

    #region Interface IRaycastTarget

            public override void ApplyDamage(float amount, Vector3 rayDirection)
            {
                if(m_SelfDesctructionCoroutine == null)
                {
                    m_ShardController.ShowShards(rayDirection);
                    gameObject.SetActive(false);
                }
            }

    #endregion

    #region  Methods

            public override void ResetState()
            {
                m_ShardController.ResetState();
                base.ResetState();
            }

            public void SetShards(Rigidbody[] rigidbodies)
            {
                m_Shards = rigidbodies;
            }

    #endregion

    }

}
