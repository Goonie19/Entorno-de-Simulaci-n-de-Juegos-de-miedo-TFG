using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

public class EnemyScriptChaser : MonoBehaviour
{

    private NavMeshAgent agent;

    private float SoundRadius = 30;

    private bool audioIsNotPlaying = true;

    private bool playerNotSeen;

    public AudioClip WalkingClip;

    public AudioClip EnemyNoise;

    public AudioClip EnemyScream;

    public GameObject target;

    private Quaternion rotation;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rotation = this.transform.rotation;

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
        if(agent.enabled)
        {
            agent.SetDestination(target.transform.position);
        }

        if (this.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 0)
        {
            this.gameObject.GetComponent<Animator>().SetBool("isRunning", true);
        } else
            this.gameObject.GetComponent<Animator>().SetBool("isRunning", false);

        if (Vector3.Distance(GameObject.Find("FPSController(Clone)").transform.position, this.gameObject.transform.position) < SoundRadius && audioIsNotPlaying && !PauseMenu.gameIsPaused)
        {
            this.gameObject.GetComponent<AudioSource>().clip = WalkingClip;
            this.gameObject.GetComponent<AudioSource>().volume = this.gameObject.GetComponent<AudioSource>().volume / Vector3.Distance(GameObject.Find("FPSController(Clone)").transform.position, this.gameObject.transform.position);
            this.gameObject.GetComponent<AudioSource>().loop = true;
            this.gameObject.GetComponent<AudioSource>().Play();
            audioIsNotPlaying = false;
        }

        if (Vector3.Distance(GameObject.Find("FPSController(Clone)").transform.position, this.gameObject.transform.position) > SoundRadius)
        {
            this.gameObject.GetComponent<AudioSource>().Stop();
            audioIsNotPlaying = true;
        }

        if (this.gameObject.GetComponent<AudioSource>().volume != LevelManager.getEffectVolume())
            this.gameObject.GetComponent<AudioSource>().volume = LevelManager.getEffectVolume() / Vector3.Distance(GameObject.Find("FPSController(Clone)").transform.position, this.gameObject.transform.position);
        
        if (target.GetComponent<FirstPersonController>().getTurnAraunds() == 5)
        {
            
        }
    }
}
