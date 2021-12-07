using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 3;
    public float sense = 3;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        var movementVertical = Input.GetAxis("Vertical");
        var movementHozizontal = Input.GetAxis("Horizontal");
        transform.position += transform.forward * movementVertical * Time.fixedDeltaTime * speed;
        transform.position += transform.right * movementHozizontal * Time.fixedDeltaTime * speed;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        Cursor.visible = false; //Oculta o cursor do mouse
        Cursor.lockState = CursorLockMode.Locked; //Trava o cursor do centro

        var mouseX = Input.GetAxis("Mouse X") * sense;
        var mouseY = Input.GetAxis("Mouse Y") * sense;

        transform.eulerAngles += new Vector3(-mouseY, mouseX);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
    }
}
