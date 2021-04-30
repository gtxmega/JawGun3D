using UnityEngine;

namespace GameCore
{
    public abstract class Disease : MonoBehaviour, IRaycastTarget
    {
        [SerializeField] protected string m_NameDisease;


    #region  Interface IRaycastTarget

            public virtual void ApplyDamage(float damage, Vector3 rayDirection)
            {

            }

    #endregion

    #region  Methods

            public virtual void ResetState()
            {
                gameObject.SetActive(true);
            }

    #endregion


    }
}
