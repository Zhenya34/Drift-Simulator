using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnOnFall : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void Update()
    {
        if (_target.position.y < -4)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}