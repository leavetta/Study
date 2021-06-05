using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	public float offset;
	public GameObject bullet;
	public Joystick joystick;
	public Transform shotPoint;

	private float timeBtwShots;
	public float startTimeBtwShots;
	private float rotZ;
	private Vector3 difference;

	private Character player;

	// Start is called before the first frame update
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
		
	}

	// Update is called once per frame
	void Update()
	{
		
		if (Mathf.Abs(joystick.Horizontal) > 0.3f || Mathf.Abs(joystick.Vertical) > 0.3f)
		{

			rotZ = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;
		}

		transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

		if (timeBtwShots <= 0)
		{
			
				if (joystick.Horizontal != 0 || joystick.Vertical != 0)
				{
					Shoot();
				}
			
		}
		else
		{
			timeBtwShots -= Time.deltaTime;
		}
	}

	public void Shoot()
	{
		Instantiate(bullet, shotPoint.position, transform.rotation);
		timeBtwShots = startTimeBtwShots;
	}
}
