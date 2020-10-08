using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

public class EnemyScriptChaser : MonoBehaviour
{

    private NavMeshAgent agent;

    private float SoundRadius = 30;

    private bool playerNotSeen;

    public AudioClip WalkingClip;

    public AudioClip EnemyNoise;

    public AudioClip EnemyScream;

    public GameObject target;

    private RaycastHit hit;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        InvokeRepeating("RepeatIdle", 0.0f, 10f);
    }

    void RepeatIdle()
    {
        if (Vector3.Distance(GameObject.Find("FPSController(Clone)").transform.position, this.gameObject.transform.position) < SoundRadius && !PauseMenu.gameIsPaused)
        {
            this.gameObject.GetComponent<AudioSource>().volume = this.gameObject.GetComponent<AudioSource>().volume / Vector3.Distance(GameObject.Find("FPSController(Clone)").transform.position, this.gameObject.transform.position);
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(EnemyNoise);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //PATH FINDING
        if(agent.enabled)
        {
            agent.SetDestination(target.transform.position);
        }

        //Activating animator and sounds
            this.gameObject.GetComponent<Animator>().SetBool("isRunning", true);
        

        if (Vector3.Distance(GameObject.Find("FPSController(Clone)").transform.position, this.gameObject.transform.position) < SoundRadius && !this.gameObject.GetComponent<AudioSource>().isPlaying && !PauseMenu.gameIsPaused)
        {
            this.gameObject.GetComponent<AudioSource>().clip = WalkingClip;
            this.gameObject.GetComponent<AudioSource>().pitch = 1;
            this.gameObject.GetComponent<AudioSource>().volume = this.gameObject.GetComponent<AudioSource>().volume / Vector3.Distance(GameObject.Find("FPSController(Clone)").transform.position, this.gameObject.transform.position);
            this.gameObject.GetComponent<AudioSource>().Play();
        }

        if (Vector3.Distance(GameObject.Find("FPSController(Clone)").transform.position, this.gameObject.transform.position) > SoundRadius)
        {
            this.gameObject.GetComponent<AudioSource>().Stop();
            target.GetComponent<FirstPersonController>().setEnemyBack(false);
            this.gameObject.SetActive(false);
        }

        if (this.gameObject.GetComponent<AudioSource>().volume != LevelManager.getEffectVolume() / Vector3.Distance(GameObject.Find("FPSController(Clone)").transform.position, this.gameObject.transform.position))
            this.gameObject.GetComponent<AudioSource>().volume = LevelManager.getEffectVolume() / Vector3.Distance(GameObject.Find("FPSController(Clone)").transform.position, this.gameObject.transform.position);


        //Scare code

        Physics.Raycast(this.transform.position, this.transform.forward, out hit);

        if (Vector3.Angle(this.transform.forward, GameObject.Find("FPSController(Clone)").transform.forward) > 145 &&
            target.GetComponent<FirstPersonController>().getEnemyBack() &&
            hit.collider.tag == target.GetComponent<Collider>().tag)
        {
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(EnemyScream);

            this.transform.parent.GetComponent<Pivot>().enabled = false;
            this.transform.parent = null;
            agent.speed = 3.5f;

            this.GetComponent<BoxCollider>().enabled = true;
            this.GetComponent<Rigidbody>().useGravity = true;
            agent.enabled = true;
            
            GameObject.Find("FPSController(Clone)").GetComponent<FirstPersonController>().resetTurnAraunds();
            GameObject.Find("FPSController(Clone)").GetComponent<FirstPersonController>().setEnemyBack(false);
        }

        if(Vector3.Distance(GameObject.Find("FPSController(Clone)").transform.position, this.transform.position) <= 1.5f)
        {
            this.gameObject.GetComponent<Animator>().SetBool("Attack", true);
        }

        if (Vector3.Distance(GameObject.Find("FPSController(Clone)").transform.position, this.transform.position) > 1.5f)
        {

            this.gameObject.GetComponent<Animator>().SetBool("Attack", false);
        }
    }
}
