using UnityEngine;
using UnityEngine.Networking;
public class PlayerShoot : NetworkBehaviour
{
    public PlayerWeapon weapon;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    void Start()
    {
        if (cam == null)
        {
            Debug.LogError("Player Shoot : No Camera Referenced");
            this.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        void Shoot()
        {
            RaycastHit _hit;
            if (Physics.Raycast(cam.transform.position,cam.transform.forward,out _hit,weapon.range,mask))
            {
                //We hit Something
                Debug.Log("We Hit " + _hit.collider.name);
            }
        }
    }

}
