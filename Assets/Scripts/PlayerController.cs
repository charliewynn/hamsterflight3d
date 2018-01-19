using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Rigidbody rb;
    public ParticleSystem ps;
    public Rigidbody bullet;
    public float bulletSpeed = 5f;
    public float health = 5f;
    bool dead;
    public Material material;

    bool isEnemy;

    private Color originalColor;

	void Start () {
        Debug.Log("Loaded Player");
        rb = GetComponent<Rigidbody>();
        originalColor = material.color;
        if (gameObject.CompareTag("Enemy")) isEnemy = true;
        //ps = GetComponent<ParticleSystem>();
        //ps.emissionRate = 0;//.emission.enabled = false;
        ps.Stop();// Play();
    }

    public void getHit(float damage)
    {
        if (!dead)
        {
            health -= damage;
            if (health <= 0)
            {
                dead = true;
                StartCoroutine(AnimateDamage(1.5f, Color.red, true));

            }
            else
            {
                StartCoroutine(AnimateDamage(.5f, Color.magenta, false));
            }
        }
    }

    IEnumerator AnimateDamage(float timeToFlash, Color flashColor, bool thenDie)
    {
        float progress = 0;
        while (progress <= timeToFlash)
        {
            progress += Time.deltaTime;
            if (Mathf.Round(progress * 10) % 2 == 0)
            {
                material.color = flashColor;
            }
            else
            {
                material.color = originalColor;
            }
            yield return null;
        }
        if (thenDie)
            Destroy(gameObject);
        else
            material.color = originalColor;
    }

    private void FixedUpdate()
    {
        if (isEnemy)
        {

        } else
        {
            applyPlayerMovement();
        }
    }

    void applyPlayerMovement()
    {
        //Debug.Log("(ps: " + ps.isEmitting + " - " + ps.isEmitting);
        bool shouldJetPack = Input.GetKey(KeyCode.UpArrow);
        bool shouldFire = Input.GetKeyDown(KeyCode.Space);
        //Input.touches[0].
        if (Input.touchCount > 0)
        {
            float minTouch = float.MaxValue;
            float maxTouch = float.MinValue;
            bool maxTouchWasDown = false;
            for (int i = 0; i < Input.touchCount; i++)
            {

                if (Input.touches[i].position.x < minTouch)
                {
                    minTouch = Input.touches[i].position.x;
                }
                if (Input.touches[i].position.x > maxTouch)
                {
                    maxTouch = Input.touches[i].position.x;
                    maxTouchWasDown = Input.touches[i].phase == TouchPhase.Began;
                }
            }
            
            if (minTouch < Screen.width/2)
                shouldJetPack = true;
            if (maxTouch > Screen.width/2 && maxTouchWasDown)
                shouldFire = true;
        }
        if (shouldJetPack && transform.position.y <= 10)
        {
            //Debug.Log("boosting: " + transform.position.y);

            rb.AddForce(new Vector2(0, 25f) * Time.fixedDeltaTime, ForceMode.Impulse);
            ps.Play();
            //ps.enableEmission = true;//.Play();//.emissionRate = 10;//.emission.enabled = false;
        }
        else
        {
            ps.Stop();
        }
        //ps.enableEmission = false;//.Stop();// = 0;//.emission.enabled = false;
        if (shouldFire)
        {
            if (bullet != null)
            {
                var b = Instantiate(bullet, transform.position + Vector3.right / 2f, new Quaternion(0, 0, 0, 0));
                b.velocity = transform.right * bulletSpeed;
            }
            //new_bullet.transform.veAddForce(Vector3.forward, ForceMode.Impulse);

        }
    }
}
