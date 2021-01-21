using UnityEngine;
using System.Collections;

public class CubeMovement : MonoBehaviour
{
	public float speed = 10.0f;
	public Rigidbody rb;
	public bool collision = true;
	public GameObject childCamera;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate ()
	{
		float horizonal = Input.GetAxis ("Horizontal") * speed * Time.deltaTime;
		transform.Translate (horizonal, 0, 0);

		if (Input.GetKey (KeyCode.UpArrow) && collision)
			rb.AddForce (0, 10, 0, ForceMode.Impulse);

        if (Input.GetKey(KeyCode.RightArrow) && childCamera.transform.localPosition.x < 4f)
            childCamera.transform.Translate(0.1f, 0, 0);

        if (Input.GetKey(KeyCode.LeftArrow) && childCamera.transform.localPosition.x > -4f)
            childCamera.transform.Translate(-0.1f, 0, 0);

		if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
		{
			if(childCamera.transform.localPosition.x > 0)
				childCamera.transform.Translate(-0.1f, 0, 0);
            
			if(childCamera.transform.localPosition.x < 0)
				childCamera.transform.Translate(0.1f, 0, 0);
		}

        if (transform.position.y <= -20)
        {
            transform.position = new Vector3(-18, 3, 0);
        }
	}

	void OnTriggerExit(Collider other)
	{
		collision = false;
	}

	void OnTriggerEnter(Collider other)
	{
		collision = true;
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (gameObject.transform.position.y <= collision.gameObject.transform.position.y + 0.5)
                gameObject.transform.position = new Vector3(-18, 3, 0);
            else
                Destroy(collision.gameObject);
        }
    }
}
