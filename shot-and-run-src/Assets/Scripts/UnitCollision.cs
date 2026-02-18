using UnityEngine;

public class UnitCollision : MonoBehaviour
{
    private UnitController _unitController;

    // Start is called before the first frame update
    void Start()
    {
        _unitController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<UnitController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Barrel" || other.gameObject.tag == "Boss")
        {
            _unitController.RemoveUnit(gameObject);
        }

        if (other.gameObject.tag == "Arch")
        {
            other.gameObject.GetComponent<BonusGain>().GetAward();
        }
    }
}
