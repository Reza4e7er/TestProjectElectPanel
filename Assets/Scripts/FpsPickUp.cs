using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class FpsPickUp : MonoBehaviour
{
    public GameObject fpscam;
    private Rigidbody rb;
    public Transform Player, Container;
    public float Range;
    public bool Equipped;
    public static bool slotfull;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 DistanceToPlayer = Player.position - transform.position;
        //Equip

        if (!Equipped && DistanceToPlayer.magnitude <= Range && Keyboard.current.eKey.wasPressedThisFrame && !slotfull)
        {
            equip();
            print("XD");
        }
        //Drop
        if (Equipped && Keyboard.current.qKey.wasPressedThisFrame)
        {
            drop();
        }
    }
    void equip()
    {
        rb.isKinematic = true;
        Equipped = true;
        slotfull = true;

        transform.SetParent(Container);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;
        fpscam.active = true;
        
    }
    void drop()
    {
        rb.isKinematic = false;
        Equipped = false;
        slotfull = false;
        transform.SetParent(null);
        fpscam.active = false;

    }
    
}
