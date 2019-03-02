using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {
    public float dayLenght = 180;
    public float rotateTimeLenght = 30;
    public bool night = false;
    private float time;
    private bool isRotating = false;
    private Vector3 originalRotate;
    private float toAddRotate;

	// Use this for initialization
	void Start ()
    {
        time = dayLenght;
        originalRotate = transform.rotation.eulerAngles;
        toAddRotate = (180 / rotateTimeLenght);
	}
	
	// Update is called once per frame
	void Update ()
    {
        time -= Time.deltaTime;
        if (isRotating) {
            transform.Rotate(Vector3.right * toAddRotate * Time.deltaTime);
        }
        if (time <= 0) {
            if (isRotating) {
                time = dayLenght;
                if (!night)
                    transform.localRotation = Quaternion.Euler(originalRotate.x + 180, originalRotate.y, originalRotate.z);
                else
                    transform.localRotation = Quaternion.Euler(originalRotate.x, originalRotate.y, originalRotate.z);
                night = !night;
            } else
                time = rotateTimeLenght;
            isRotating = !isRotating;
        }
	}
}
