using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField] private Rigidbody projectile;
    [SerializeField] private Transform shootPoint;
    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }


    private void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            Shoot();
        }
    }
    public void Shoot(){

        var _cameraCenterRay = mainCam.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0f));
        var _shootGo = Instantiate(projectile,shootPoint.position,shootPoint.rotation);
        if(Physics.Raycast(_cameraCenterRay,out var hitinfo)){
            var _dir = (hitinfo.point - shootPoint.position);
            shootPoint.rotation = Quaternion.LookRotation(_dir);
            _shootGo.AddForce(_dir.normalized * 20f,ForceMode.Impulse);
        }
        else{
            shootPoint.rotation = Quaternion.LookRotation(shootPoint.forward);
            _shootGo.AddForce(shootPoint.forward * 20f,ForceMode.Impulse);
        }
    }

}
