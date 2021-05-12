using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class GamemanagerX : MonoBehaviour
{
    private PlayerController _playerController;
    
    public bool isGameActive;

    public  TextMeshProUGUI ammoCountText;

    // Start is called before the first frame update
    void Start()
    {
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        isGameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateAmmoCount()
    {
        ammoCountText.text = "AMMO" +
                             "\nCurrent Magazine: " + _playerController.currAmmo + 
                             "\nExtra Ammo: " + _playerController.extraAmmo;
    }
}
