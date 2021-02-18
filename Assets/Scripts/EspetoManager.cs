using UnityEngine;

public class EspetoManager : MonoBehaviour
{
    private SliderJoint2D espeto;
    private JointMotor2D motor;

    // Start is called before the first frame update
    void Start()
    {
        espeto = GetComponent<SliderJoint2D>();
        motor = espeto.motor;
    }

    // Update is called once per frame
    void Update()
    {
        if (espeto.limitState == JointLimitState2D.UpperLimit)
        {
            motor.motorSpeed = Random.Range(-1, -5);
        }
        else if (espeto.limitState == JointLimitState2D.LowerLimit)
        {
            motor.motorSpeed = Random.Range(1, 5);
        }
        // A alteração no motor não é por referência
        espeto.motor = motor;
    }
}
