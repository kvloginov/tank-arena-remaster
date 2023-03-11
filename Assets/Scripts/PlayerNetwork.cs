using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    private readonly NetworkVariable<PlayerNetworkData> _playerDataState = new(writePerm: NetworkVariableWritePermission.Owner);
    private Vector3 _vel;
    private float _towerRotVel;

    [SerializeField]
    private LevelHandler levelHandler;

    [SerializeField]
    private Transform _tower;

    [SerializeField]
    private float _interpolationTime = 0.1f;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            Destroy(transform.GetComponent<MoveScript>());
        }
        setPositionOnSpawn();
    }

    private void setPositionOnSpawn()
    {
        Debug.Log("Owner: " + ((int)OwnerClientId));

        var spawnPosition = new Vector3(0, 2, 0);
        var spawnPositions = levelHandler.spawnPositions;
        if (((int)OwnerClientId) < spawnPositions.Length)
        {
            spawnPosition = spawnPositions[(int)OwnerClientId].position;
        }

        transform.position = spawnPosition;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            _playerDataState.Value = new PlayerNetworkData()
            {
                Position = transform.position,
                TowerRotation = _tower.eulerAngles,
            };
        } else
        {
            transform.position = Vector3.SmoothDamp(transform.position, _playerDataState.Value.Position, ref _vel, _interpolationTime);
            _tower.rotation = Quaternion.Euler(
                0, 
                Mathf.SmoothDampAngle(_tower.rotation.eulerAngles.y, _playerDataState.Value.TowerRotation.y, ref _towerRotVel, _interpolationTime),
                0);
        }
    }


    struct PlayerNetworkData : INetworkSerializable
    {
        private float _x, _y, _z;
        private float _towerRot;

        internal Vector3 Position
        {
            get => new Vector3(_x, _y, _z);
            set
            {
                _x = value.x;
                _y = value.y;
                _z = value.z;
            }
        }

        internal Vector3 TowerRotation
        {
            get => new Vector3(0, _towerRot, 0);
            set
            {
                _towerRot = value.y;
            }
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _x);
            serializer.SerializeValue(ref _y);
            serializer.SerializeValue(ref _z);
            serializer.SerializeValue(ref _towerRot);
        }
    }
}
