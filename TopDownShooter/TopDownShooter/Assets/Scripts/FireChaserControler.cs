using UnityEngine;

public class FireChaserControler : MonoBehaviour
{
    [SerializeField] GameObject[] objectChain;
    [SerializeField] Rigidbody2D[] rigidbodyChain;
    [SerializeField] float lagtime = 0.25f;
    float timer = 0;
    float bulletSpeed;
    [HideInInspector] public Vector2 aimInput;
    [HideInInspector] public bool activePlayerControl = true;
    [HideInInspector] public float playerFacingDir;
    void Start()
    {
        bulletSpeed = GetComponent<BulletData>().bulletSpeed;
        if (aimInput == Vector2.zero)
            aimInput = new Vector2 (playerFacingDir, 0);
        Debug.Log(aimInput);
        Debug.Log(bulletSpeed);
        for (int i = rigidbodyChain.Length - 1; i >= 0; i--)
        {
            rigidbodyChain[i].velocity = (aimInput * bulletSpeed);
        }
    }
    void Update()
    {
        if (aimInput != Vector2.zero)
            rigidbodyChain[0].velocity = (aimInput * bulletSpeed);
        timer += Time.deltaTime;
        if (timer > lagtime)
        {
            timer = 0;
            UpdateObjectChainVelocity();
        }
    }
    void UpdateObjectChainVelocity()
    {
        for (int i = rigidbodyChain.Length - 1; i > 0; i--)
        {
            rigidbodyChain[i].velocity = rigidbodyChain[i - 1].velocity;
        }
    }
}