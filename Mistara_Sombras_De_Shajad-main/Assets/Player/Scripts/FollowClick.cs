using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;


public class FollowClick : MonoBehaviour
{
    //[SerializeField] private Transform target;
    NavMeshAgent _agent;
    private GameObject _gameObject;
    private Vector3? target = null;
    

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Convert mouse position to world coordinates
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
           
            // Check if something was hit
            if (hit.collider != null)
            {
                //Debug.Log("Clicked on " + hit.collider.gameObject.name);               
                // hit.collider.gameObject.GetComponent<YourComponent>().YourFunction();
                _gameObject = hit.collider.gameObject;
                target = hit.collider.transform.position;

                _agent.SetDestination(Camera.main.ScreenToWorldPoint(Input.mousePosition));                
            }
            else
            {                
                _agent.SetDestination(Camera.main.ScreenToWorldPoint(Input.mousePosition));                
            }
        }
        else if (Input.GetMouseButton(0))
        {
            _agent.SetDestination(Camera.main.ScreenToWorldPoint(Input.mousePosition));            
        }

        

        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.Value);

            // Check if the distance is less than 0.5 units
            if (distance < 1.5f)
            {
                //Debug.Log("Distance is less than 1.5 units");
                // Add additional actions here
                target = null;
                _agent.Stop();
            }
        }


        
        if (_agent.isStopped == true && _gameObject != null)
        {
            _gameObject.GetComponent<IsClickable>().ManageInteraction();

            _agent.isStopped = false;
            _gameObject = null;
        }

    }
}
