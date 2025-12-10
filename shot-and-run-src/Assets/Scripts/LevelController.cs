using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject Plate;
    public Level level;
    public Camera m_camera;

    public float MinX { get; set; }
    public float MaxX { get; set; }
    public float MinZ { get; set; } = -1f;
    public float MaxZ { get; set; } = 3f;

    public float XLeftShift { get; set; }

    private float timer = 0;
    private int currentWaveNum = 0;

    private bool stop = false;

    private const float bossFightCameraY = 7f;
    private const float bossFightCameraXAng = 30f;
    private bool rotateCamera = false;
    // Start is called before the first frame update

    void Start()
    {
        if (m_camera == null)
        {
            m_camera = Camera.main;
        }

        SetCameraNormal();

        var xScale = level.TrackCount * 0.5f;
        Plate.transform.localScale = new Vector3(xScale, 1, 10);

        MaxX = xScale * 5f - 0.25f;
        MinX = -MaxX;
        XLeftShift = -(xScale * 5f - 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateCamera)
        {
            RotateCamera();
        }

        if (stop)
        {
            return;
        }

        if (timer <= 0)
        {
            timer = level.WaveRate;

            Spawn();

            currentWaveNum++;
            if (currentWaveNum >= level.Waves.Count)
            {
                stop = true;
                SetCameraBossFight();
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    private void RotateCamera()
    {
        int equalValues = 0;
        var newY = m_camera.transform.position.y + Time.deltaTime;

        if (newY > bossFightCameraY)
        {
            newY = bossFightCameraY;
            equalValues += 1;
        }

        var newXAng = m_camera.transform.eulerAngles.x - Time.deltaTime* 10;

        if (newXAng < bossFightCameraXAng)
        {
            newXAng = bossFightCameraXAng;
            equalValues += 1;
        }

        m_camera.transform.position = new Vector3(m_camera.transform.position.x, newY, m_camera.transform.position.z);
        m_camera.transform.eulerAngles = new Vector3(newXAng, m_camera.transform.eulerAngles.y, m_camera.transform.eulerAngles.z);

        if (equalValues == 2)
        {
            rotateCamera = false;
        }
    }

    public void Spawn()
    {
        const float dX = 5;
        var currentWave = level.Waves[currentWaveNum];

        var connectedArchs = new List<Bonus>();
        var archs = new List<GameObject>();

        for (int i = 0; i < level.TrackCount; i++)
        {
            var objective = currentWave.Objectives.ElementAtOrDefault(i);
            if (objective != null)
            {
                var isBoss = objective.tag == "Boss";
                var yPos = isBoss ? objective.transform.position.y : 0.5f;
                var xPos = isBoss 
                    ? XLeftShift + (level.TrackCount / 2.0f - 0.5f) * dX
                    : XLeftShift + i * dX;
                var obj = Instantiate(objective, new Vector3(xPos, yPos, 35), objective.transform.rotation);

                if (obj.tag == "Arch")
                {
                    connectedArchs.Add(obj.GetComponent<Bonus>());
                    archs.Add(obj);
                }

                var objHp = obj.GetComponent<HealthPoints>();
                if (objHp == null)
                {
                    if (obj.transform.childCount > 0)
                    {
                        objHp = obj.transform.GetChild(0).GetComponent<HealthPoints>();
                    }

                    if (objHp == null)
                    {
                        continue;
                    }
                }

                objHp.SetHP(currentWave.Hp);
            }
        }

        archs.ForEach(a => a.GetComponent<ConnectedArch>().ArchBonuses = connectedArchs);
    }

    public void SetCameraNormal()
    {
        m_camera.transform.position = new Vector3(0, 6, -5);
        m_camera.transform.eulerAngles = new Vector3(40f, 0f, 0f);

        rotateCamera = false;
    }

    public void SetCameraBossFight()
    {
        rotateCamera = true;
    }
}


[Serializable]
public class Level
{
    public float WaveRate = 10f;

    public int TrackCount = 2;

    public List<Wave> Waves = new List<Wave>();
}

[Serializable]
public class Wave
{
    public float Hp = 10;
    public List<GameObject> Objectives = new List<GameObject>();
}
