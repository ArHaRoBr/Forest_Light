using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using Unity.Burst.CompilerServices;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    public Interactable focus;

    public LayerMask movementMask;
    Camera cam;
    PlayerMotor motor;

    //--------
    public GameObject enemyHealthBar;
    //-------
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                //Debug.Log("We hit " + hit.collider.name + " " + hit.point);
                motor.MoveToPoint(hit.point);
                RemoveFocus();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {

                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }

        enemyCheck();

        //--------------
      
        //--------
            /*if (Input.GetKeyDown(KeyCode.I))
            {
                Inventory.instance.ShowOrHide();
            }*/
        }
    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDeFocused();
            }

            focus = newFocus;
            motor.FollowTarget(newFocus);
        }

        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDeFocused();
        }

        focus = null;
        motor.StopFollowingTarget();
    }

    void enemyCheck()
    {
        Ray ray1 = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit1;

        if (Physics.Raycast(ray1, out hit1, 100))
        {
            try
            {
                if (hit1.collider.gameObject.GetComponent<Enemy>() != null)
                {
                   enemyHealthBar.SetActive(true);
                }
                else if (hit1.collider.gameObject.GetComponent<Enemy>() == null)
                {
                   enemyHealthBar.SetActive(false);

                }

            }
            catch
            {
                enemyHealthBar.SetActive(false);
            }
        }
    }

}
