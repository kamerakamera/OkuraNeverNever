using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    Rigidbody rb;
    public Camera mainCamera;
    public Vector3 mousePosition;
    public GameObject jumpCheckObject;
    float changeAmountAxisY;
    public static float rotateSensitivityPower = 4;
    float jumpCoolTime = 0;
    bool isJump;
    Vector3 moveDirection;
    bool front, back, right, left, up,jumpAble;
    int straight, side;
    public float movePower,jumpPower;
    AudioSource audioSource;
    public AudioClip audioClip;


    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        //movePower = 3;
        //jumpPower = 10;
    }

    // Update is called once per frame
    void Update() {
        CheckJumpAble();
        MoveInput();
    }

    void FixedUpdate() {
        Move();
        lookAround();
    }

    void lookAround() {
        mousePosition = Input.mousePosition;
        float mousePositionX = Input.GetAxis("Mouse X");
        float mousePositionY = Input.GetAxis("Mouse Y");
        //縦回転
        if (transform.localEulerAngles.x - mousePositionY <= 290 && transform.localEulerAngles.x - mousePositionY >= 180) {
            transform.Rotate(290 - transform.localEulerAngles.x, 0, 0);
        } else if (transform.localEulerAngles.x - mousePositionY >= 70 && transform.localEulerAngles.x - mousePositionY <= 180) {
            transform.Rotate(70 - transform.localEulerAngles.x, 0, 0);
        } else transform.Rotate(-mousePositionY * rotateSensitivityPower, 0, 0);
        //横回転
        transform.Rotate(0, mousePositionX * rotateSensitivityPower, 0, Space.World);
    }

    void MoveInput() {
        if (Input.GetKey("w")) {
            front = true;
        } else front = false;

        if (Input.GetKey("s")) {
            back = true;
        } else back = false;

        if (Input.GetKey("a")) {
            left = true;
        } else left = false;

        if (Input.GetKey("d")) {
            right = true;
        } else right = false;

        if (Input.GetKeyDown(KeyCode.Space) && up == false && jumpAble) {
            up = true;
        } else up = false;

        if (front) {
            straight = 1;
        }
        if (back) {
            straight = -1;
        }
        if ((front && back) || (!front && !back)) straight = 0;

        if (left) {
            side = -1;
        }
        if (right) {
            side = 1;
        }
        if ((left && right) || (!left && !right)) side = 0;
    }

    void Move() {
        moveDirection = new Vector3(transform.forward.x * straight + transform.right.x * side, rb.velocity.y, transform.forward.z * straight + transform.right.z * side).normalized;
        rb.velocity = new Vector3(moveDirection.x * movePower, rb.velocity.y, moveDirection.z * movePower);

        if (up && isJump == false) {
            rb.velocity = new Vector3(rb.velocity.x, jumpPower, rb.velocity.z);
            isJump = true;
        }
        if (isJump) {
            jumpCoolTime += Time.fixedDeltaTime;
            if (jumpCoolTime >= 2) {
                jumpCoolTime = 0;
                isJump = false;
            }
        }
    }

    void CheckJumpAble() {
        Collider[] checkObject;
        jumpAble = false;
        checkObject = Physics.OverlapSphere(jumpCheckObject.transform.position, 1f);
        if(checkObject == null) {
            jumpAble = false;
            return;
        }
        foreach(Collider col in checkObject) {
            if (col.tag == "Player") {
                continue;
            }
            if (col.isTrigger == true) {
                jumpAble = false;
                return;
            }
            jumpAble = true;

        }
        if(jumpAble == true) {
            Debug.Log("飛べるよ");
        } else {
            Debug.Log("飛べないよ");
        }
        return;
    }

}
