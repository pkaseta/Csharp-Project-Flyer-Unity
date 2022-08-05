using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {

    [SerializeField] float sceneDelay = 1f;
    [SerializeField] AudioClip crashMedia;
    [SerializeField] AudioClip successMedia;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;


    //audio Source Component
    AudioSource audioSource;

    //state
    bool isTransitioning = false;
    bool collisionsEnabled = true;
    //Detect when you use the toggle, ensures collision controls arent cuplicated
    // bool m_ToggleChange;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

        // Update is called once per frame
    void Update() {
        CheatLoadNextLevel();
         if(Input.GetKey("c")){
                collisionsEnabled = false;
        }
    }

    void OnCollisionEnter(Collision other) {
        if(isTransitioning) {
            return;
        }
        switch (other.gameObject.tag) {
            case "Friendly":
                Debug.Log("Home Base");
                 break;
            case "Finish":
                StartWinSequence();
                break;
            case "Fuel sphere":
                 Debug.Log("Add fuel");
                 break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void playAudio(UnityEngine.AudioClip media) {
        if(!audioSource.isPlaying) {
                    audioSource.PlayOneShot(media);
                }
    }

    void ReloadLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
         SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        //Plays success audio
        
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        
        SceneManager.LoadScene(nextSceneIndex);
    }

    void StartCrashSequence() {
        if(collisionsEnabled){
            isTransitioning = true;
            audioSource.Stop();
            stopPlayerControls();
            playAudio(crashMedia);
            crashParticles.Play();
            Invoke("ReloadLevel", sceneDelay);
        }
    }

    void StartWinSequence() {
        isTransitioning = true;
        audioSource.Stop();
        stopPlayerControls();
        playAudio(successMedia);
        successParticles.Play();
        Invoke("LoadNextLevel", sceneDelay);
    }

    void stopPlayerControls() {
        GetComponent<Movement>().enabled = false;
    }

    void CheatLoadNextLevel() {
        if(Input.GetKey("l")){
            LoadNextLevel();
        }
    }

        void OnGUI()
    {
        //Switch this toggle to activate and deactivate the parent GameObject
        collisionsEnabled = GUI.Toggle(new Rect(10, 10, 100, 30), collisionsEnabled, "Collision");
    }
}
