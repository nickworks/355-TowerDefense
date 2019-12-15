using UnityEngine;
namespace Webb
{/// <summary>
/// moves object towards object and damges enemys if they toch collider is destroyed after a coupple seconds
/// </summary>
    public class AreaAffectBullet : MonoBehaviour
    {
        Vector3 attackTarget; // stores dir to player
        public float speed = 50;// how fast bullet moves
        float timer = .2f; //how long bullet is alive
        // Start is called before the first frame update
        // gets dir of player
        void Start()
        {
            attackTarget = Tower.enemyPostion;
        }

        // Update is called once per frame
        /// <summary>
        /// moves object towards player
        /// destroys after timer hits zero
        /// </summary>
        void Update()
        {
            timer -= Time.deltaTime;
            Vector3 dirToPlayer = (attackTarget - transform.position);
            transform.position += dirToPlayer.normalized * speed * Time.deltaTime;
          if (timer <= 0) Destroy(gameObject);
        }
      
    }
}