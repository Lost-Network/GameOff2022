using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopupPlayer : MonoBehaviour
{
    public static DamagePopupPlayer Create(Vector3 position, int damageAmount) {
        Transform damagePopupPlayerTransform = Instantiate(GameAssets.i.pfDamagePopupPlayer, position, Quaternion.identity);

        DamagePopupPlayer damagePopupPlayer = damagePopupPlayerTransform.GetComponent<DamagePopupPlayer>();
        damagePopupPlayer.Setup(damageAmount);

        return damagePopupPlayer;
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
