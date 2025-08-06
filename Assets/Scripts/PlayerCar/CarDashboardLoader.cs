using UnityEngine;
using UnityEngine.SceneManagement;

public class CarDashboardLoader : MonoBehaviour
{

    void Start()
    {
        SceneManager.LoadScene("IVI", LoadSceneMode.Additive);
    }

}
