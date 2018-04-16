using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VendingMachine : MonoBehaviour {
    private uint _numPiesLeft;
    private GameObject _instancedUI;
    private AudioSource _audioSource;

    public uint InitialPieCountMin = 15;
    public uint InitialPieCountMax = 25;
    public float canBuyFromDistance = 3.0f;

    public AudioClip[] coinSounds;

    private void Start() {
        _numPiesLeft = (uint) Random.Range((int) InitialPieCountMin, (int) InitialPieCountMax);
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Update() {
        if (Input.GetButtonUp("Use")) {
            if (_instancedUI != null) {
                if (_numPiesLeft > 0) {
                    if (PlayerInventory.Instance.TryPickingUp("vending")) {
                        --_numPiesLeft;
                        if (_audioSource.isPlaying)
                            _audioSource.Stop();
                        _audioSource.clip = coinSounds[Random.Range(0, coinSounds.Length)];
                        _audioSource.Play();
                    }
                }
            }
        }
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


    private void CreateText() {
        if (_instancedUI == null) {
            _instancedUI = (GameObject) Instantiate(Resources.Load("Texts/BuyPieMessage"),
                transform.position + new Vector3(0.0f, 0.6f, 0.0f), Quaternion.Euler(90.0f, 0.0f, 0.0f));
        }
    }

    private void DeleteText() {
        if (_instancedUI != null) {
            Destroy(_instancedUI);
        }
    }
}