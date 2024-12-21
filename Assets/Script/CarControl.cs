using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{
    public float speed = 10f;
    public Transform carMesh;
    float carRotation = 0f;
    Vector3 camRotation;
    public Animator carAni;
    public GameObject cam;
    public GameObject camPanel;
    bool camModeActive = false;
    Vector3 camPrevRotation;
    Transform camPrevTrans;
    bool objectPicked = false;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        camPrevRotation = new Vector3(cam.transform.rotation.x,cam.transform.rotation.y,cam.transform.rotation.z);
        camPrevTrans = cam.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(camModeActive==false)
        {
        if(Input.GetKey(KeyCode.UpArrow)){
            transform.position += new Vector3(0f,0.1f,0f);
        }
        
        if(Input.GetKey(KeyCode.DownArrow)&& transform.position.y>1.5){
            transform.position -= new Vector3(0f,0.1f,0f);
        }

        if(Input.GetKey(KeyCode.W)){
            transform.position += transform.forward*speed*Time.deltaTime;
           
        }

        if(Input.GetKey(KeyCode.S) ){
            transform.position -= transform.forward*speed*Time.deltaTime;
           
        }

        if(Input.GetKey(KeyCode.D)){
            transform.position += transform.right*speed*Time.deltaTime;
           //carAni.Play("MoveRight");
             }

        if(Input.GetKey(KeyCode.A)){
            transform.position -= transform.right*speed*Time.deltaTime;
             //carAni.Play("MoveLeft");
        }


        RotateCar();
        Magenet();
        }
        
         if(Input.GetKeyDown(KeyCode.C)){
            if(camModeActive==false){
                camModeActive = true;
                camPanel.SetActive(true);
            }
            else{
                camModeActive = false;
                cam.transform.localRotation = Quaternion.Euler(new Vector3(15,0f,0f));
                cam.transform.localPosition = new Vector3(0f,2.22f,-4.73f);
                camPanel.SetActive(false);
            }
        }

       if(camModeActive){
        CameraMode();
       }
    }

    void RotateCar(){
         if(Input.GetAxis("Mouse X")!=0){
            carRotation+=Input.GetAxis("Mouse X");
            transform.eulerAngles = new Vector3(0f,carRotation*5,0f);
        }
       

    }

    void Magenet(){
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -transform.up, out hit))
        { 
            if(hit.transform.tag=="Pickable"){
                if(hit.distance<2.5f){
                Debug.Log(hit.transform.name);
                if(Input.GetKeyDown(KeyCode.P)){
                    if(objectPicked==false){
                        hit.transform.parent = transform;
                        objectPicked = true;
                        hit.transform.GetComponent<Rigidbody>().useGravity = false;
                    }
                    else{
                        hit.transform.parent = null;
                        objectPicked = false;
                        hit.transform.GetComponent<Rigidbody>().useGravity = true;
                    }
                }
            }
            }
        }
    }

    void CameraMode(){
        camRotation.x += Input.GetAxis("Mouse Y");
        camRotation.y += Input.GetAxis("Mouse X");
        camRotation.x = Mathf.Clamp(camRotation.x,-10,10);
        cam.transform.eulerAngles = new Vector3(camRotation.x,camRotation.y,0f)*3;
    }
}
