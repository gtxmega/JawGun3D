using UnityEngine;

namespace GameCore
{
    public interface IRaycastTarget
    {
        void ApplyDamage(float damage, Vector3 rayDirection);
    }
}
