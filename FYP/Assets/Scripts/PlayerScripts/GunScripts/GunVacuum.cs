using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunVacuum : MonoBehaviour
{
    [SerializeField]
    private Transform gunPoint;

    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private Vector3 scaleChange = new Vector3(-0.01f, -0.01f, -0.01f);

    public GameObject gun;

    Animator anim;
    Animator gunAnim;

    CapsuleCollider _collider;

    //PS
    public ParticleSystem farPointInhale;
    public ParticleSystem nearPoint;
    public ParticleSystem shootPoint;

    //Audio
    public AudioSource inhale;
    public AudioSource shoot;

    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject settingUI;
    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject backPackUI;

    private void Start()
    {
        pauseUI = UIScripts.instance.pauseUI;
        settingUI = UIScripts.instance.settingUI;
        shopUI = UIScripts.instance.shopUI;
        backPackUI = UIScripts.instance.backPackUI;

        gunAnim = gun.GetComponent<Animator>();
    }

    private void Update()
    {
        if ((pauseUI && settingUI) && !pauseUI.activeInHierarchy && !settingUI.activeInHierarchy)
        {
            if (shopUI && !shopUI.activeInHierarchy && !backPackUI.activeInHierarchy)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    gunAnim.SetBool("isInhale", true);
                    gunAnim.SetBool("canShoot", true);
                    //gunAnim.speed = 1f;
                    farPointInhale.Play(true);
                    nearPoint.Play(true);
                    inhale.Play();
                }
                else if (Input.GetMouseButtonUp(1))
                {
                    gunAnim.SetBool("isInhale", false);
                    farPointInhale.Stop(true);
                    nearPoint.Stop(true);
                    inhale.Stop();
                }
                if (Input.GetMouseButtonDown(0))
                {
                    gunAnim.SetBool("canShoot", true);
                    shoot.Play();
                    shootPoint.Play(true);
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    gunAnim.SetBool("canShoot", false);
                    shootPoint.Stop(true);
                }
            }
        }
    }

    float emitTime = 0;
    float emitInterval = 0.5f;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButton(1) && Physics.Raycast(transform.parent.parent.position, transform.parent.parent.forward, out RaycastHit raycastHit, 3f))//inhale item in the crafting table
        {
            if (raycastHit.transform.TryGetComponent(out CraftingTable craftingTable))
            {
                //Debug.Log("raycastHit.transform.TryGetComponent(out CraftingTable craftingTable)");
                //Debug.Log("Time.time: " + Time.time);
                //Debug.Log("emitTime + emitInterval: " + emitTime + emitInterval);
                if(Time.time > emitTime + emitInterval)
                {
                    craftingTable.RemoveInputMaterial();
                    emitTime = Time.time;


                }

            }
        }


        if (other.transform.gameObject.GetComponent<Target>())          //new change
        //if (other.transform.CompareTag("Target"))
        {
            anim = other.transform.gameObject.GetComponentInChildren<Animator>();
            if (other.transform.gameObject.layer == 7)
            {
                if(other.transform.GetComponent<CapsuleCollider>())     //new
                    _collider = other.transform.GetChild(0).gameObject.GetComponentInChildren<CapsuleCollider>();
            }
            if (Input.GetMouseButton(1))
            {
                

                other.transform.gameObject.GetComponent<Target>().isVacuum = true;
                VacuumTarget(other);
            }
            else /*if (Input.GetMouseButtonUp(1))*/
            {
                if(other.transform.childCount > 0 && other.transform.GetComponentInChildren<CapsuleCollider>() && other.transform.GetChild(1).GetComponent<CapsuleCollider>())         //new
                    other.transform.GetChild(1).gameObject.GetComponentInChildren<CapsuleCollider>().enabled = true;
                other.transform.gameObject.GetComponent<Target>().isVacuum = false;

                if (anim)
                {
                    anim.ResetTrigger("Suck");
                    anim.SetTrigger("Release");
                }
                
            }
            if (anim && anim.GetCurrentAnimatorStateInfo(0).IsName("ChickenNormal"))
            {
                anim.speed = 1;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.GetComponent<Target>())      //new change
        //if (other.transform.CompareTag("Target"))
        {
            anim = other.transform.gameObject.GetComponentInChildren<Animator>();
            if(other.transform.childCount > 0 && other.transform.GetComponentInChildren<CapsuleCollider>())         //new
                other.transform.GetChild(1).gameObject.GetComponentInChildren<CapsuleCollider>().enabled = true;
            other.transform.gameObject.GetComponent<Target>().isVacuum = false;
            if (anim)
            {
                anim.ResetTrigger("Suck");
                anim.SetTrigger("Release");
            }
        
        }
    }



    public void VacuumTarget(Collider other)
    {
        //Debug.Log("inhaling");
        if (Vector3.Distance(other.transform.position, gunPoint.position) > 0.3f)
        {
            //old version (Vector3.MoveTowards)
            //float distance = speed * Time.deltaTime;
            //_hit.transform.position = Vector3.MoveTowards(_hit.transform.position, gunPoint.position, distance);

            //new version (change velocity)
            Vector3 direction = (gunPoint.position - other.transform.position).normalized;
            other.attachedRigidbody.velocity = direction * speed;

            if (other.transform.childCount > 0 && other.transform.GetComponentInChildren<CapsuleCollider>())         //new
                other.transform.GetChild(1).gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;
            anim = other.transform.gameObject.GetComponentInChildren<Animator>();
            if (anim)
            {
                anim.speed = 1;
                anim.SetTrigger("Suck");
                anim.ResetTrigger("Release");
            }
           

            
        }

        else
        {
            //Debug.Log("inhaled");
            //inventory system
            var item = other.transform.GetComponent<WorldItem>().item;
            if (item)
                InventoryManager5.instance.AddItem(item);
            


            Destroy(other.transform.gameObject);
            //collision.transform.gameObject.SetActive(false);
        }
        if (Vector3.Distance(other.transform.position, gunPoint.position) < 1.0f)
        {
            other.transform.localScale += scaleChange;
        }
    }
}
