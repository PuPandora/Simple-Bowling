using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public int ForcePower;
    public float timer;

    Rigidbody rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    void FixedUpdate()
    {
        // 랜덤 각도 방향
        float minAngle = -25f;
        float maxAngle = 25f;
        float randomAngle = Random.Range(minAngle, maxAngle);
        Vector3 randomDirection = Quaternion.Euler(randomAngle, randomAngle, randomAngle) * Vector3.forward;
        Vector3 force = randomDirection.normalized * ForcePower;
        // 초반 부스터
        if (timer < 2f)
        {
            rigid.AddForce(force * Time.fixedDeltaTime, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pin")
        {
            StartCoroutine(GameManager.instance.CollidePin());
        }
    }
}
