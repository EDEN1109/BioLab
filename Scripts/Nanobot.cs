using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Brinicle;

public class Nanobot : MonoBehaviour
{
    [Header("Nanobot UI Setting")]
    public NanobotUIPanel nanobotUI;

    [Header("Hand Setting")]
    [SerializeField] private Transform botLeftHand;
    [SerializeField] private Transform botRightHand;
    [SerializeField] private Transform leftRayPoint;
    [SerializeField] private Transform rightRayPoint;

    private RaycastHit leftHit;
    private RaycastHit rightHit;
    private ParticleSystem leftParticle;
    private ParticleSystem rightParticle;
    private AudioSource leftAudio;
    private AudioSource rightAudio;
    private LineRenderer leftLaser;
    private LineRenderer rightLaser;
    private Carrier carrier;
    private PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        SetPosition();
        leftLaser = leftRayPoint.GetComponent<LineRenderer>();
        rightLaser = rightRayPoint.GetComponent<LineRenderer>();
        leftParticle = leftRayPoint.GetComponent<ParticleSystem>();
        rightParticle = rightRayPoint.GetComponent<ParticleSystem>();
        leftAudio = leftRayPoint.GetComponent<AudioSource>();
        rightAudio = rightRayPoint.GetComponent<AudioSource>();
        carrier = GameObject.FindGameObjectWithTag("PlayerData").GetComponent<Carrier>();
        playerData = GameObject.FindGameObjectWithTag("PlayerData").GetComponent<PlayerData>();

        carrier.SetNanobotUIPanel(nanobotUI);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerData.IsSetHands)
        {
            RotateGuns();

            if (playerData.LeftTriggerPress())
            {
                float targetDis = RayShoot('L');
                DrawLaser(targetDis, 'L');
            }
            else
            {
                if(leftParticle.isPlaying)
                {
                    leftLaser.enabled = false;
                    leftParticle.Stop();
                }
                if(leftAudio.isPlaying)
                {
                    leftAudio.Stop();
                }
            }

            if (playerData.RightTriggerPress())
            {
                float targetDis = RayShoot('R');
                DrawLaser(targetDis, 'R');
            }
            else
            {
                if (rightParticle.isPlaying)
                {
                    rightLaser.enabled = false;
                    rightParticle.Stop();
                }
                if (rightAudio.isPlaying)
                {
                    rightAudio.Stop();
                }
            }
        }
    }

    private void RotateGuns()
    {
        Vector3 leftPoint = playerData.GetLeftHandRayPoint();
        Vector3 rightPoint = playerData.GetRightHandRayPoint();

        if(leftPoint != null)
        {
            botLeftHand.LookAt(leftPoint);
        }

        if(rightPoint != null)
        {
            botRightHand.LookAt(rightPoint);
        }
    }

    private float RayShoot(char isLeft)
    {
        if(isLeft == 'L')
        {
            Debug.DrawRay(leftRayPoint.position, leftRayPoint.up * 100f, Color.blue, 0.3f);

            if(Physics.Raycast(leftRayPoint.position, leftRayPoint.up, out leftHit))
            {
                if(leftHit.transform.GetComponent<Buble>() != null)
                {
                    leftHit.transform.GetComponent<Buble>().OnRayCutting();
                }

                return Vector3.Distance(leftRayPoint.position, leftHit.point);
            }
        }
        else
        {
            Debug.DrawRay(rightRayPoint.position, rightRayPoint.up * 100f, Color.red, 0.3f);

            if (Physics.Raycast(rightRayPoint.position, rightRayPoint.up, out rightHit))
            {
                if (rightHit.transform.GetComponent<Enemy>() != null)
                {
                    rightHit.transform.GetComponent<Enemy>().OnRayAttack();
                }

                return Vector3.Distance(rightRayPoint.position, rightHit.point);                 
            }
        }

        if(isLeft == 'L')
        {
            return 100f;
        }
        else
        {
            return 100f;
        }
    }

    private void DrawLaser(float targetDis, char isLeft)
    {
        if(isLeft == 'L')
        {
            if(!leftParticle.isPlaying)
            {
                leftLaser.enabled = true;
                leftParticle.Play();
            }
            if(!leftAudio.isPlaying)
            {
                leftAudio.Play();
            }
            leftLaser.SetPosition(0, Vector3.zero);
            leftLaser.SetPosition(1, Vector3.up * targetDis / 100f);
        }
        else
        {
            if (!rightParticle.isPlaying)
            {
                rightLaser.enabled = true;
                rightParticle.Play();
            }
            if (!rightAudio.isPlaying)
            {
                rightAudio.Play();
            }
            rightLaser.SetPosition(0, Vector3.zero);
            rightLaser.SetPosition(1, Vector3.up * targetDis / 100f);
        }
    }

    private void SetPosition()
    {
        Transform mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        transform.parent = mainCamera;
        transform.localPosition = new Vector3(0, -0.15f, 0.015f);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }    

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Enemy>() != null)
        {
            other.GetComponent<Enemy>().IsDie = true;
            carrier.DecreaseAmount();
        }
    }
}
