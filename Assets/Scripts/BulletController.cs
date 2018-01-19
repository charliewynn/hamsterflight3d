using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    // Use this for initialization
    Rigidbody rb;
	void Start () {
        rb = GetComponent<Rigidbody>();
        //rb.AddForce(new Vector2(10f, 0), ForceMode.Impulse);
        //Debug.Log("Bullet was made");
    }

    private void OnCollisionEnter(Collision collision)
    {
        var hitObject = collision.gameObject;
        if (hitObject.tag == "Enemy")
            (hitObject.GetComponent<PlayerController>()).getHit(1);
        Destroy(gameObject, .25f);
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        //rb.AddForce(new Vector2(1f, 0), ForceMode.Impulse);
        if(rb.position.x > 20)
        {
            Destroy(gameObject);
        }
        //rb.position
    }
}
