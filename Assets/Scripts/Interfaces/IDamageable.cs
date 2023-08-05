using System.Collections;
using UnityEngine;

namespace Assets
{
    public interface IDamageable
    {
        void TakeDamage(float damage);

        void Die();
    }
}