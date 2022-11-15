using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public static DamagePopup Create(Vector3 position, int damageAmount) {
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);

        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount);

        return damagePopup;
    }
    private TextMeshPro textMesh;

    private float timer = 2f;
    
    private void Awake() {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount) {
        textMesh.SetText(damageAmount.ToString());
    }

    private void Update() {
        timer -= Time.deltaTime;
        if (0 < timer) {
            float moveYSpeed = .5f;
            transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
        } else {
            Destroy(gameObject);
        }
    }
}
