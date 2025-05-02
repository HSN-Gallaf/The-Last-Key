using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoorLevelLoader : MonoBehaviour
{
    public int levelToLoad = 1;                     // Set this per door
    public string sceneName;                        // Name of the scene to load
    public GameObject interactionUI;                // Optional UI prompt (e.g., "Press E to Enter")

    private bool isPlayerInRange = false;

    void Start()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        // Lock the door if it's above the unlocked level
        if (levelToLoad > unlockedLevel)
        {
            gameObject.SetActive(false);  // Hide or disable the door
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(sceneName);  // Load the scene assigned to this door
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (interactionUI != null) interactionUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (interactionUI != null) interactionUI.SetActive(false);
        }
    }
}
