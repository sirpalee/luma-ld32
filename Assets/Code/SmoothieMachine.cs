using UnityEngine;
using UnityEngine.SceneManagement;

public class SmoothieMachine : MonoBehaviour {
    private GameObject m_instancedUI;
    public float canBuyFromDistance = 3.0f;

    // Use this for initialization
    private void Start() {
        m_instancedUI = null;
    }

    private void OnMouseOver() {
        if (Vector3.Distance(transform.position, PlayerController.Instance.gameObject.transform.position)
            < canBuyFromDistance) {
            CreateText();
        }
    }

    private void OnMouseExit() {
        DeleteText();
    }

    private void OnMouseUp() {
        if (PlayerInventory.Instance.IsReadyForMagicSmoothie()) {
            SceneManager.LoadScene("Winner");
        }
    }

    private void CreateText() {
        if (m_instancedUI == null) {
            m_instancedUI = (GameObject) Instantiate(Resources.Load("Texts/MakeSmoothieMessage"),
                transform.position + new Vector3(0.0f, 0.6f, 0.0f), Quaternion.Euler(90.0f, 0.0f, 0.0f));
        }
    }

    private void DeleteText() {
        if (m_instancedUI != null) {
            Destroy(m_instancedUI);
        }
    }
}