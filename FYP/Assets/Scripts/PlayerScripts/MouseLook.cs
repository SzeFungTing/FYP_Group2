using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;

    public bool lockCursor = true;
    bool lockViewMoving = false;

    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject settingUI;
    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject backPackUI;
    GameObject craftingSystemUI;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //it make ui button not work!!!!!!!
        Cursor.visible = false;

        pauseUI = UIScripts.instance.pauseUI;
        settingUI = UIScripts.instance.settingUI;
        shopUI = UIScripts.instance.shopUI;
        backPackUI = UIScripts.instance.backPackUI;
        craftingSystemUI = UIScripts.instance.CraftingUI;
    }

    // Update is called once per frame
    void Update()
    {
        if (!lockViewMoving)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
        

        UpdateCursorLock();
    }

    public void SetCursorLock(bool value)
    {
        lockCursor = value;
        if (!lockCursor)
        {//we force unlock the cursor if the user disable the cursor locking helper
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void UpdateCursorLock()
    {
        //if the user set "lockCursor" we check & properly lock the cursos
        if (lockCursor)
            InternalLockUpdate();
    }

    public void InternalLockUpdate()
    {
        //if (Input.GetKeyUp(KeyCode.Escape))
        //{
        //    Debug.Log("1");

        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;
        //}
        //else if (pauseUI.activeInHierarchy || settingUI.activeInHierarchy)
        //{
        //    if (Input.GetKeyUp(KeyCode.Escape))
        //    {
        //        Debug.Log("2");

        //        Cursor.lockState = CursorLockMode.Locked;
        //        Cursor.visible = false;
        //    }
        //}

        if((shopUI|| backPackUI || craftingSystemUI) &&(shopUI.activeInHierarchy || backPackUI.activeInHierarchy || craftingSystemUI.activeInHierarchy /*|| pauseUI.activeInHierarchy || settingUI.activeInHierarchy*/))
        {
            //Debug.Log("11");

            lockViewMoving = true;
        }
        else if ((shopUI || backPackUI || craftingSystemUI || pauseUI || settingUI) && lockViewMoving && (!shopUI.activeInHierarchy || !backPackUI.activeInHierarchy || !craftingSystemUI.activeInHierarchy|| !pauseUI.activeInHierarchy || !settingUI.activeInHierarchy))
        {
            //Debug.Log("12");

            lockViewMoving = false;
        }

        //if (Input.GetKeyUp(KeyCode.C))            //shop system  
        //{
        //    Debug.Log("3");

        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;
        //    lockViewMoving = true;
        //}
        //else if (shopUI.activeInHierarchy)
        //{
        //    if (Input.GetKeyUp(KeyCode.C))
        //    {
        //        Debug.Log("4");

        //        Cursor.lockState = CursorLockMode.Locked;
        //        Cursor.visible = false;
        //        lockViewMoving = false;
        //    }
        //}

        //if (Input.GetKeyUp(KeyCode.I) && !backPackUI.activeInHierarchy)          //backpack system
        //{
        //    Cursor.lockState = CursorLockMode.None;
        //    lockViewMoving = false;
        //    Cursor.visible = true;
        //}
        //else if (backPackUI.activeInHierarchy)
        //{
        //    if (Input.GetKeyUp(KeyCode.I))
        //    {
        //        Cursor.lockState = CursorLockMode.Locked;
        //        lockViewMoving = true;
        //        Cursor.visible = false;
        //    }
        //}

        //if (Input.GetKeyUp(KeyCode.E))          //Crafting system
        //{
        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;
        //    lockViewMoving = true;

        //}
        //else if (craftingSystemUI.activeInHierarchy)
        //{
        //    if (Input.GetKeyUp(KeyCode.E))
        //    {
        //        Cursor.lockState = CursorLockMode.Locked;
        //        Cursor.visible = false;
        //        lockViewMoving = false;
        //    }
        //}
    }
}
