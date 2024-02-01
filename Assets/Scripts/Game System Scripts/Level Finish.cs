using UnityEngine;
public class LevelFinish : MonoBehaviour
{
    [SerializeField] private string sceneName;
    private void OnTriggerEnter2D(Collider2D other)
    {  
        if (other.tag == "Player")
            MenuManager.LoadScene(sceneName);  
    }
}