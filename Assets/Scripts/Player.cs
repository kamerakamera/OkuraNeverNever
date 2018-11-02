using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    Rigidbody rb;
    public Vector3 mousePosition;
    public GameObject jumpCheckObject;
    float changeAmountAxisY;
    public float rotateSensitivityPower = 4;
    float jumpCoolTime = 0;
    bool isJump,deathAccept;
    Vector3 moveDirection;
    bool front, back, right, left, up,jumpAble,vibration,lightCheck, lasttimeLightCheck;
    int straight, side;
    public float movePower,jumpPower;
    AudioSource audioSource;
    public Light playerLight;
    public GameObject enemy;
    public EnemyController enemyController;
    public Camera mainCamera;
    public EnemyManage enemyManage;
    Vector3 cameraPosition;
    [SerializeField]
    private GameManegement gameManegement;


    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        playerLight.enabled = true;
        deathAccept = false;
        vibration = false ;
        enemyController = enemy.GetComponent<EnemyController>();
        lightCheck = false;
        lasttimeLightCheck = false;
        //Death();
        //movePower = 3;
        //jumpPower = 10;
    }

    // Update is called once per frame
    void Update() {
        CheckJumpAble();
        MoveInput();
        PushButton();
    }

    void FixedUpdate() {
        Move();
        lookAround();
        LightUp();
        if (vibration) {
            Vibration();
        }
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
        if (!deathAccept) {
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
            if (jumpCoolTime >= 0.3f) {
                jumpCoolTime = 0;
                isJump = false;
            }
        }
    }

    void CheckJumpAble() {
        Collider[] checkObject;
        jumpAble = false;
        checkObject = Physics.OverlapSphere(jumpCheckObject.transform.position, 0.5f);
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
            //Debug.Log("飛べるよ");
        } else {
            //Debug.Log("飛べないよ");
        }
        return;
    }

    void LightUp() {
        RaycastHit hit;
        lightCheck = false;
        if(Physics.Raycast(this.transform.position, this.transform.forward, out hit)) {
            if(hit.collider.tag == "Enemy") {
                lightCheck = true;
            }
            if(lightCheck && lasttimeLightCheck && enemy.activeSelf) {
                enemyController.Stop();
            }
            if(!lightCheck && lasttimeLightCheck && enemy.activeSelf) {
                enemyController.Restart();
            }
            lasttimeLightCheck = lightCheck;
        }
    }

    void PushButton() {
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0)) {
            if (Physics.SphereCast(this.transform.position + new Vector3(0,-0.52f,0), 0.5f,this.transform.forward * 1.0f, out hit,3.0f)) {
                if(hit.collider.tag == "Button") {
                    hit.collider.gameObject.GetComponent<UnlockButton>().Accept();
                }
            }
        }
    }

    void Death() {
        deathAccept = true;
        audioSource.Play();
        playerLight.enabled = false;
        //enemymanegerからEnemy削除
        enemyManage.PlayerDeath();
        cameraPosition = mainCamera.transform.position;
        StartCoroutine("LoadGameOverScene");
        
    }

    void StartVibration() {
        vibration = true;
    }

    void Vibration() {
        mainCamera.gameObject.transform.position = new Vector3(cameraPosition.x + Random.Range(-0.1f, 0.1f), cameraPosition.y + Random.Range(-0.1f, 0.1f), cameraPosition.z + Random.Range(-0.1f, 0.1f));
    }

    private IEnumerator LoadGameOverScene() {
        StartVibration();
        yield return new WaitForSeconds(7.0f);
        gameManegement.GameEnd();
        yield break;
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.collider.tag == "Enemy") {
            Death();

        }
        if(collision.collider.tag == "ClearObject") {
            gameManegement.GameClear();
        }
    }

}
