using UnityEngine;
using UnityEngine.InputSystem;
using EPOOutline;
using TMPro;
using UnityEngine.Playables;
using Ignis;
public class TheOutline : MonoBehaviour
{
    private GameObject nowEquppied=null,TargetPickUp=null,TargetInteractable=null;
    public GameObject fpscam,parent,Wire1,Wire2,WireFixed,PlayerCapsule,fire,box,extinguisherVFX;
    public Transform Player, Container;
    public bool Equipped,IsOpened,Onfire,Fuse;
    public static bool slotfull;
    public TextMeshProUGUI myText,myText1;
    public Animator animTablo,Wrench,Pilers;
    private void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        RaycastHit hitInfo;

        // Limit the raycast distance to prevent going through walls
        float maxRaycastDistance = 5f; 

        if (Physics.Raycast(ray, out hitInfo, maxRaycastDistance))
        {
            GameObject hitObject = hitInfo.collider.gameObject;

            // PickUp Object

            if (hitObject.CompareTag("PickUp") == true)
            {
                
                hitObject.GetComponent<Outlinable>().enabled = true;
                TargetPickUp = hitObject;
                if (!Equipped && Keyboard.current.eKey.wasPressedThisFrame && !slotfull)
                {
                    TargetPickUp.GetComponent<Rigidbody>().isKinematic = true;
                    Equipped = true;
                    slotfull = true;

                    TargetPickUp.transform.SetParent(Container);
                    TargetPickUp.transform.localPosition = Vector3.zero;
                    TargetPickUp.transform.localRotation = Quaternion.Euler(Vector3.zero);
                    TargetPickUp.transform.localScale = Vector3.one;
                    fpscam.active = true;
                    TargetPickUp.tag = "nonxd";
                    TargetPickUp.layer = LayerMask.NameToLayer("Outlinable");
                    print("XD");
                    nowEquppied = TargetPickUp;
                }
               

                
            }
            // if player looks at somethingelse then the outline component will be turned off
            else
            {
               
                TargetPickUp.GetComponent<Outlinable>().enabled = false;
            }

            if (hitObject.CompareTag("Interactable") == true)
            {
                hitObject.GetComponent<Outlinable>().enabled = true;
                TargetInteractable = hitObject;
                if (!IsOpened)
                {
                    if (parent.transform.childCount == 1)
                    {
                        if (parent.transform.GetChild(0).name == "PB114_wrench_Low_max")
                        {
                            myText.text = "Press F";
                            if (Keyboard.current.fKey.wasPressedThisFrame)
                            {
                                animTablo.SetBool("Open", true);
                                Wrench.SetBool("Wrench", true);
                                IsOpened = true;
                                TargetInteractable.GetComponent<BoxCollider>().enabled = false;
                            }
                        }
                        else
                        {
                            myText.text = "U need Wrench to open the door";
                        }
                    }
                    else
                    {
                        myText.text = "U need Wrench to open the door";
                    }

                }
                else
                {
                    if (TargetInteractable.name == "Wire")
                    {
                        if (parent.transform.childCount == 1)
                        {
                            if (parent.transform.GetChild(0).name == "PB59_pliers_SM_max")
                            {
                                myText.text = "Press F";
                                if (Keyboard.current.fKey.wasPressedThisFrame)
                                {
                                    Invoke("WireFixing", 1f);
                                    Pilers.SetBool("Fixed", true);

                                }
                            }
                            else
                            {
                                myText.text = "u need Pliers to fix the wires!";
                            }

                        }
                        else
                        {
                            myText.text = "u need Pliers to fix the wires!";
                        }
                    }
                 
                    if(TargetInteractable.name == "TabloFirst")
                    {
                        TargetInteractable.GetComponent<Outlinable>().enabled = true;
                        if (Fuse)
                        {
                            myText.text = "Turn Off the Fuse!!!";
                        }
                        else
                        {
                            if (parent.transform.childCount == 1)
                            {
                                if (parent.transform.GetChild(0).name == "PB20_vognegasnyk_sm_max")
                                {
                                    myText.text = "Press F";
                                    if (Keyboard.current.fKey.wasPressedThisFrame)
                                    {

                                        print("WP");
                                        Invoke("FireOff", 2f);
                                        TargetInteractable.tag = "nonxd";
                                        extinguisherVFX.active = true;

                                    }
                                }
                                else
                                {
                                    myText.text = "Use FireExtinguisher to put out fire!";
                                }
                            }
                            else
                            {
                                myText.text = "Use FireExtinguisher to put out fire!";
                            }

                        }
                        
                    }
                    if(TargetInteractable.name == "Fuse")
                    {
                        TargetInteractable.GetComponent<Outlinable>().enabled = true;
                        myText.text = "Press F";
                        if (Keyboard.current.fKey.wasPressedThisFrame)
                        {

                            Fuse = false;

                        }
                    }
                }
            }
            // if player looks at somethingelse then the outline component will be turned off
            else
            {
                myText.text = "";
                TargetInteractable.GetComponent<Outlinable>().enabled = false;
            }
        }
        //Dropping the Object
        if (Equipped && Keyboard.current.qKey.wasPressedThisFrame)
        {
            Drop();
        }
        
    }
    void WireFixing()
    {
        Wire1.active = false;
        Wire2.active = false;
        WireFixed.active = true;
        PlayerCapsule.GetComponent<PlayableDirector>().enabled = true;
        PlayerCapsule.GetComponent<PlayerInput>().enabled = false;
        Invoke("Playerinputback", ((float)PlayerCapsule.GetComponent<PlayableDirector>().duration));
        Onfire = true;
        fire.GetComponent<FlammableObject>().enabled = true;
        box.GetComponent<BoxCollider>().enabled = true;
        Drop();

    }
    void Drop()
    {
        nowEquppied.GetComponent<Rigidbody>().isKinematic = false;
        Equipped = false;
        slotfull = false;
        nowEquppied.transform.SetParent(null);
        fpscam.active = false;
        nowEquppied.tag = "PickUp";
        nowEquppied.layer = 0;

    }
    void Playerinputback()
    {
        PlayerCapsule.GetComponent<PlayerInput>().enabled = true;
        myText1.enabled = false;
    }
    void FireOff()
    {
        fire.GetComponent<FlammableObject>().enabled = false;
        extinguisherVFX.active = false;
    }
}
