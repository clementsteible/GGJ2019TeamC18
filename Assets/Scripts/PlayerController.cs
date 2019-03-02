using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public bool joystick = false;
    public float playerSpeed = 3;
    public GameObject[] weapons = new GameObject[2];
    public float QTETime = 1;
    [HideInInspector]
    public bool inQTE = false;
    private string currentQTE;
    private float timerQTE = 0;
    private bool first = true;
    private bool QTETmp = false;
    private GameObject currentCow;
    //private Animator anim;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        weapons[0].SetActive(true);
        weapons[1].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (inQTE) {
            inQTE = QTE();
            if (!inQTE) {
                currentCow.GetComponent<VacheIA>().GoHome();
                Debug.Log("fail");
            }
            return;
        }
        if (first) {
            weapons[0].SetActive(true);
            weapons[1].SetActive(false);
            first = false;
        }
        DetectJoystick();
        transform.position = ((new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"))) * playerSpeed) + transform.position;
        if (!joystick)
            LookAtMouse();
        else
            LookAtJoystick();
        if (!weapons[0].activeInHierarchy && (Input.GetButton("Fire1") || Input.GetAxis("Fire1") > 0)) {
            weapons[0].SetActive(true);
            weapons[1].SetActive(false);
        } else if (!weapons[1].activeInHierarchy && (Input.GetButton("Fire2") || Input.GetAxis("Fire2") > 0)) {
            weapons[1].SetActive(true);
            weapons[0].SetActive(false);
        }
        /*if (Input.GetButton("Use")) {
            currentQTE = (Random.Range(0, 10) > 5) ? "QTELeft" : "QTERight";
            Debug.Log(currentQTE);
            timerQTE = QTETime;
            inQTE = true;
        }*/
    }

    void DetectJoystick()
    {
        string[] temp = Input.GetJoystickNames();
        if (temp.Length > 0) {
            for (int i = 0; i < temp.Length; ++i) {
                if (!string.IsNullOrEmpty(temp[i])) {
                    joystick = true;
                } else {
                    joystick = false;
                }
            }
        } else {
            joystick = false;
        }
    }

    void LookAtJoystick()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0f, Mathf.Atan2(Input.GetAxis("OtherVertical"), Input.GetAxis("OtherHorizontal")) * Mathf.Rad2Deg, 0f));
    }

    void LookAtMouse()
    {
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        transform.rotation = Quaternion.Euler(new Vector3(0f, angle * -1, 0f));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    bool QTE()
    {
        timerQTE -= Time.deltaTime;
        if (!QTETmp) {
            if (timerQTE <= 0)
                return false;
            else if (Input.GetButton("QTELeft") || Input.GetAxis("QTELeft") > 0) {
                if (currentQTE == "QTELeft") {
                    timerQTE = 0.5f;
                    QTETmp = true;
                    return true;
                } else
                    return false;
            } else if (Input.GetButton("QTERight") || Input.GetAxis("QTERight") > 0) {
                if (currentQTE == "QTERight") {
                    timerQTE = 0.5f;
                    QTETmp = true;
                    return true;
                } else
                    return false;
            }
            return true;
        } else if (timerQTE <= 0) {
            currentQTE = (Random.Range(0, 10) > 5) ? "QTELeft" : "QTERight";
            Debug.Log(currentQTE);
            timerQTE = QTETime;
            QTETmp = false;
        }
        return true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Vache" && Input.GetButton("Use")) {
            currentQTE = (Random.Range(0, 10) > 5) ? "QTELeft" : "QTERight";
            Debug.Log(currentQTE);
            timerQTE = QTETime;
            inQTE = true;
            currentCow = collision.gameObject;
            currentCow.GetComponent<VacheIA>().GoToFrom();
        }
    }
}