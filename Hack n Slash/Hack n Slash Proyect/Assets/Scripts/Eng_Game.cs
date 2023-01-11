using UnityEngine;

public class Eng_Game : MonoBehaviour
{
    
    public GameManger gameManager;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.CompleteLevel();
        }
        
    }

}
