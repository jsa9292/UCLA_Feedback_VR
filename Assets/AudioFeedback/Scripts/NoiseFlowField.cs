using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class NoiseFlowField : MonoBehaviour
{
    FastNoise _fastNoise;
    public Vector3Int _gridSize;
    public float _cellSize;
    public Vector3[,,] _flowfieldDirection;
    public float _increment;
    public Vector3 _offset, _offsetSpeed;

    //particles
    public GameObject _particlePrefab;
    public int _amountOfParticles;
    [HideInInspector]
    public List <FlowFieldParticle> _particles;
    public List<MeshRenderer> _particleMeshRenderer;
    public float _spawnRadius;
    public float _particleScale, _particleMoveSpeed, _particleRotateSpeed;

    bool _particleSpawnValidation(Vector3 position)
    {
        bool valid = true;
        foreach (FlowFieldParticle particle in _particles)
        {
            if (Vector3.Distance(position, particle.transform.position) < _spawnRadius)
            {
                valid = false;
                break;
            }
        }
        if (valid)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool baked;
    public bool record;
    public bool endRecording;
    GameObjectRecorder _recorder;
    string _clipName;
    public AnimationClip _currentClip;
    public string _newClipName;


    // Start is called before the first frame update
    private void Awake()
    {
        _flowfieldDirection = new Vector3[_gridSize.x, _gridSize.y, _gridSize.z];
        _fastNoise = new FastNoise();
        if (baked)
        {
            for (int i = 0; i < transform.childCount; i++) {
                _particles.Add(transform.GetChild(i).GetComponent<FlowFieldParticle>());
                _particleMeshRenderer.Add(transform.GetChild(i).GetComponent<MeshRenderer>());
            }
            this.enabled = false;
        }
        else if (record) {
            //Spawn();
            _recorder = new GameObjectRecorder(gameObject);
            _recorder.BindComponentsOfType<Transform>(gameObject, true);

        }

        else Spawn();
    }
    private void StartRecording() { 
    
    }
    private void StopRecording() {
        if (!record) return;
        record = false;
        _recorder.SaveToClip(_currentClip);
        //AssetDatabase.CreateAsset(_currentClip, _saveFolderLocation + _newClipName);
        AssetDatabase.SaveAssets();
    }
    void Spawn()
    {
        _particles = new List<FlowFieldParticle>();
        _particleMeshRenderer = new List<MeshRenderer>();
        for (int i = 0; i < _amountOfParticles; i++)
        {
            int attempt = 0;

            while (attempt < 100)
            {
                Vector3 randomPos = new Vector3(
                    Random.Range(this.transform.position.x, this.transform.position.x + _gridSize.x * _cellSize),
                    Random.Range(this.transform.position.y, this.transform.position.y + _gridSize.y * _cellSize),
                    Random.Range(this.transform.position.z, this.transform.position.z + _gridSize.z * _cellSize));
                bool isValid = true;//_particleSpawnValidation(randomPos);

                if (isValid)
                {
                    GameObject particleInstance = (GameObject)Instantiate(_particlePrefab);
                    particleInstance.name = "particle" + i;
                    particleInstance.transform.position = randomPos;
                    particleInstance.transform.parent = this.transform;
                    particleInstance.transform.localScale = new Vector3(_particleScale, _particleScale, _particleScale);
                    _particles.Add(particleInstance.GetComponent<FlowFieldParticle>());
                    _particleMeshRenderer.Add(particleInstance.GetComponent<MeshRenderer>());
                    break;
                }
                if (!isValid)
                {
                    attempt++;
                }
            }
        }
        Debug.Log(_particles.Count);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateFlowFieldDirections();
        ParticleBehavior();
    }

    private void LateUpdate()
    {
        if (record) {
            _recorder.TakeSnapshot(Time.deltaTime);
        }

        if (endRecording)
        {
            StopRecording();
        }

    }

    void CalculateFlowFieldDirections()
    {
        _offset = new Vector3(_offset.x + (_offsetSpeed.x * Time.deltaTime), _offset.y + (_offsetSpeed.y * Time.deltaTime), _offset.z + (_offsetSpeed.z * Time.deltaTime));


        float xOff = 0f;

        for (int x = 0; x < _gridSize.x; x++)
        {
            float yOff = 0f;
            for (int y = 0; y < _gridSize.y; y++)
            {
                float zOff = 0f;
                for (int z = 0; z < _gridSize.z; z++)
                {
                    float noise = _fastNoise.GetSimplex(xOff + _offset.x, yOff + _offset.y, zOff + _offset.z) + 1;
                    Vector3 noiseDirection = new Vector3(Mathf.Cos(noise * Mathf.PI), Mathf.Sin(noise * Mathf.PI), Mathf.Cos(noise * Mathf.PI));
                    _flowfieldDirection[x, y, z] = Vector3.Normalize(noiseDirection);
                    zOff += _increment;
                }
                yOff += _increment;
            }
            xOff += _increment;
        }
    }

    void ParticleBehavior()
    {
        foreach(FlowFieldParticle p in _particles)
        {
            // X Edges
            if (p.transform.position.x > this.transform.position.x + (_gridSize.x * _cellSize))
            {
                p.transform.position = new Vector3(this.transform.position.x, p.transform.position.y, p.transform.position.z);
            }
            if (p.transform.position.x < this.transform.position.x)
            {
                p.transform.position = new Vector3(this.transform.position.x + (_gridSize.x * _cellSize), p.transform.position.y, p.transform.position.z);
            }

            // Y Edges
            if (p.transform.position.y > this.transform.position.y + (_gridSize.y * _cellSize))
            {
                p.transform.position = new Vector3(p.transform.position.x, this.transform.position.y, p.transform.position.z);
            }
            if (p.transform.position.y < this.transform.position.y)
            {
                p.transform.position = new Vector3(p.transform.position.x, this.transform.position.y + (_gridSize.y * _cellSize), p.transform.position.z);
            }

            // Z Edges
            if (p.transform.position.z > this.transform.position.z + (_gridSize.z * _cellSize))
            {
                p.transform.position = new Vector3(p.transform.position.x, p.transform.position.y, this.transform.position.z);
            }
            if (p.transform.position.z < this.transform.position.z)
            {
                p.transform.position = new Vector3(p.transform.position.x, p.transform.position.y, this.transform.position.z + (_gridSize.z * _cellSize));
            }


            Vector3Int particlePos = new Vector3Int(
                Mathf.FloorToInt(Mathf.Clamp((p.transform.position.x - this.transform.position.x) / _cellSize, 0, _gridSize.x - 1)),
                Mathf.FloorToInt(Mathf.Clamp((p.transform.position.y - this.transform.position.x) / _cellSize, 0, _gridSize.y - 1)),
                Mathf.FloorToInt(Mathf.Clamp((p.transform.position.z - this.transform.position.x) / _cellSize, 0, _gridSize.z - 1))
                );
            p.ApplyRotation(_flowfieldDirection[particlePos.x, particlePos.y, particlePos.z], _particleRotateSpeed);
            p._movespeed = _particleMoveSpeed;
            //p.transform.localScale = new Vector3(_particleScale, _particleScale, _particleScale);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(this.transform.position + new Vector3((_gridSize.x * _cellSize) * 0.5f, (_gridSize.y * _cellSize) * 0.5f, (_gridSize.z * _cellSize) * 0.5f),
            new Vector3(_gridSize.x * _cellSize, _gridSize.y * _cellSize, _gridSize.z * _cellSize));

    }
}
