using UnityEngine;
namespace Webb
{
    public class AreaAffectBullet : MonoBehaviour
    {
        Vector3 attackTarget;
        public float speed = 50;
        float timer = .2f;
        // Start is called before the first frame update
        void Start()
        {
            attackTarget = Tower.enemyPostion;
        }

        // Update is called once per frame
        void Update()
        {
            timer -= Time.deltaTime;
            Vector3 dirToPlayer = (attackTarget - transform.position);
            transform.position += dirToPlayer.normalized * speed * Time.deltaTime;
          if (timer <= 0) Destroy(gameObject);
        }
      
    }
}