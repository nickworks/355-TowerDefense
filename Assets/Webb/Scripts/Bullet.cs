using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Webb {
    public class Bullet : MonoBehaviour
    {
        Vector3 attackTarget;
        public float speed = 50;
        // Start is called before the first frame update
        void Start()
        {
            attackTarget = Tower.enemyPostion;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 dirToPlayer = (attackTarget - transform.position);
            transform.position += dirToPlayer.normalized * speed * Time.deltaTime;
            
        }
        public void OnTriggerEnter(Collider collider)
        {
            if (collider.transform.tag == "Enemy") {
                Destroy(gameObject);
            }

        }
    }
}