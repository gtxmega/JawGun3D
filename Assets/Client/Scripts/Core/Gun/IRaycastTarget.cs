using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public interface IRaycastTarget
    {
        void ApplyDamage(float damage);
    }
}
