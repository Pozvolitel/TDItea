using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    private int _life = 20;

    public void AddLife(int life)
    {
        _life += life;
        _scoreText.text = _life.ToString();
        if (_life <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
