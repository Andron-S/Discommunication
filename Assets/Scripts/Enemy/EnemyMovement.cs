using System.Collections;
using TMPro.EditorUtilities;
using UnityEngine;

namespace Assets.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private Transform Target;
        [SerializeField] private float Speed;

        private Rigidbody2D Rigidbody2D;

        private void Start()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Target = FindObjectOfType<Player>().transform;

            //Speed = Random.Range(5, 10);
        }

        private void FixedUpdate()
        {
            Move();
        }

        private Vector2 GetDirectionToPlayer()
        {
            Vector2 direction = Target.position - transform.position;
            direction.Normalize();

            return direction;
        }

        private void RotateToPlayer(Vector2 direction)
        {
            float faceAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(faceAngle - 90f, Vector3.forward);
        }

        private void Move()
        {
            if (Target != null)
            {
                Vector2 direction = GetDirectionToPlayer();

                RotateToPlayer(direction);

                Rigidbody2D.velocity = direction * Speed;
            }
        }
    }
}