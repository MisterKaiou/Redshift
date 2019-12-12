using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerActions : MonoBehaviour
{

    private void Start()
    {
        CheckLoadedScenes();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Weapon")) PickWeapon(other);

        if (other.gameObject.name == "Door") SwitchArea(other);

        
    }

    private void PickWeapon(Collider2D other)
    {
        if (gameObject.GetComponent<PlayerMovement>().colliderFront.Distance(other).distance < 1f)
        {
            Debug.Log("Hit");
            Destroy(other.gameObject);
        }
    }

    private void SwitchArea(Collider2D other)
    {
        if (other.gameObject.CompareTag("DoorToOffice"))
        {
            LoadScene("Office");
        }
        else if (other.gameObject.CompareTag("DoorToHouse"))
        {
            LoadScene("House");
        }

        CheckLoadedScenes();
    }

    public IEnumerator LoadScene(string levelToLoad)
    {
        Debug.Log("caiu");

        AsyncOperation load = SceneManager.LoadSceneAsync(levelToLoad);

        yield return null;
    }

    private void CheckLoadedScenes()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).isLoaded && SceneManager.GetSceneAt(i).name != SceneManager.GetActiveScene().name)
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
                Debug.Log("Unloaded Scene:" + SceneManager.GetSceneAt(i).name);
            }
        }
    }

    private void SpawnPlayerAtDoor()
    {
        var rb = gameObject.GetComponent<Rigidbody2D>();

        rb.MovePosition(new Vector2());
    }

}
