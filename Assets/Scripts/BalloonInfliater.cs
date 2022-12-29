using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonInfliater : MonoBehaviour
{
    private Rigidbody rb;

    public float floatStrength = 3.5f;
    public float maxSize = 3;
    [Range(0, 1)] public float riseSize = .5f;
    public float timeToReachMaxSize = 1;
    public float maxDrag = 100;
    public float maxSpeed;

    private float randomSpeed;

    private Vector3 baseScale;
    private Vector3 targetScale;
    private Vector3 sizeToRise;
    //rigidbody'deki drag deðerine eþit
    private float startDrag;  
    private float timeInflating;


    private float speed;


    void Start()
    {
        randomSpeed = UnityEngine.Random.Range(2, maxSpeed);

        rb = GetComponent<Rigidbody>();

        baseScale = transform.localScale;
        targetScale = baseScale * maxSize;

        
        sizeToRise = baseScale * (riseSize * maxSize);
        startDrag = rb.drag;

    }


    private void Update()
    {
        BeginInfliate();

    }

    private void BeginInfliate()
    {
        timeInflating += Time.deltaTime;
        rb.useGravity = false;
        rb.velocity = new Vector3(0, speed, 0);

        //Zaman deðerinin min max arasýnda kalmasýný saðlar.
        timeInflating = Mathf.Clamp(timeInflating, 0, timeToReachMaxSize);

        //Balonun zamanla azalan ivme ile þiþmesini saðlar
        transform.localScale = Vector3.Lerp(baseScale, targetScale, timeInflating / timeToReachMaxSize);

        //Balonun sürüklenmesini belirler. Balon ne kadar büyükse(ne kadar þiþirilirse) o kadar hýzlý yükselir. Bunun tam tersi de geçerlidir.
        rb.drag = Mathf.Lerp(startDrag, maxDrag, InverseLerp(baseScale, sizeToRise, transform.localScale)); 

        speed = Mathf.Lerp(0, randomSpeed, InverseLerp(sizeToRise, targetScale, transform.localScale)); 

        if (transform.localScale.magnitude >= sizeToRise.magnitude)
        {
            rb.useGravity = false;
            rb.velocity = new Vector3(0, speed, 0);
        }
        else
        {
            rb.useGravity = true;
        }        
    }

    //normalize vektörler ayný yönü gösterirlerse 1 ters yönse -1 döndürür. bu hesaplamada lerp kullanýmýnda, a noktasýndan gideceðimiz b noktasýnýn hesaplamalarýný yapýyoruz.
    //balon sabit kalmayacaktýr çünkü
    public static float InverseLerp(Vector3 a, Vector3 b, Vector3 value)
    {
        Vector3 AB = b - a;
        Vector3 AV = value - a;
        return Vector3.Dot(AV, AB) / Vector3.Dot(AB, AB); // vector3.dot vektörel büyüklüðü float döndürür.
    }
}
