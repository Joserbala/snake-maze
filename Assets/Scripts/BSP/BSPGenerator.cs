using System.Collections.Generic;
using SnakeMaze.Maze;
using SnakeMaze.Structures;
using SnakeMaze.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SnakeMaze.BSP
{
    public class BSPGenerator : MonoBehaviour
    {
        [Header("Parameter Configuration")]
        [Range(0.25f, 1.0f)]
        [Tooltip("Variation of size for rooms. The smaller this value the smaller rooms will be generated.")]
        [SerializeField] private float roomSizePerturbation;
        [Tooltip("Width of the corridors.")]
        [SerializeField] private int corridorWidth;
        [Tooltip("The maximum number of Rooms to be created = 2^numberIterations.")]
        [SerializeField] private int numberIterations;
        [Tooltip("Space for the rooms with reference to the walls / partitions.")]
        [SerializeField] private int offset = 1;
        [SerializeField] private Vector2 mapSize;
        [SerializeField] private Vector2 maxRoomSize;

        [Header("Flags for Visualization")]
        [SerializeField] private bool drawCorridors;
        [SerializeField] private bool drawPartitions;
        [SerializeField] private bool drawRooms;
        [SerializeField] private bool printCorridorsInConsole;
        [SerializeField] private bool printRoomsInConsole;
        [SerializeField] private bool printTreeInConsole;

        [Header("Prefabs")]
        [SerializeField] private GameObject corridorPrefab;
        [SerializeField] private GameObject roomPrefab;
        [SerializeField] private Transform corridorParentT;
        [SerializeField] private Transform roomParentT;

        private BSPData _rootdata;

        /// <summary>
        /// Structure that will store the whole information about the partitions.
        /// </summary>
        private BinaryTree<BSPData> _tree;

        // Data generated with the information stored in the Binary Tree.
        /// <summary>
        /// List with Corridor data.
        /// </summary>
        private List<Corridor> _corridorList;

        /// <summary>
        /// A room is defined by a position (center) and a size of the room. There will be a room for each partition.
        /// </summary>
        private List<Room> _roomList;

        public List<Room> RoomList
        {
            get => _roomList;
        }

        private void Start()
        {
            GenerateDungeon();

            if (printCorridorsInConsole)
                foreach (Corridor c in _corridorList)
                    Debug.Log(c);

            if (printRoomsInConsole)
                foreach (Room r in _roomList)
                    Debug.Log(r);
        }

        /// <summary>
        /// Destroys all rooms and corridors contained in <see cref="roomParentT"/> and <see cref="corridorParentT"/>.
        /// </summary>
        public void DeleteDungeon()
        {
            foreach (Transform corridor in corridorParentT)
            {
                Destroy(corridor.gameObject);
            }

            foreach (Transform room in roomParentT)
            {
                Destroy(room.gameObject);
            }
        }

        /// <summary>
        /// Generates a new dungeon.
        /// </summary>
        /// <remarks>
        /// You may want to call <see cref="DeleteDungeon"/> before.
        /// </remarks>
        public void GenerateDungeon()
        {
            if (printTreeInConsole)
                Debug.Log(BinaryTreeUtils<BSPData>.InOrderHorizontal(_tree, 0));

            // Putting the information related to the whole map in the tree root.
            _rootdata = new BSPData(new Bounds(Vector2.zero, new Vector3(mapSize.x, mapSize.y, 0)));
            _tree = BSP(new BinaryTree<BSPData>(_rootdata, null, null), 0);

            // Data generation.
            // REVIEW: All the Generate methods might be joined to avoid different traversals of the same Binary tree (performance optimization).                                               
            _corridorList = GenerateCorridors(_tree);
            // _corridorList = GenerateCorridorsGood(_tree);
            _roomList = GenerateRooms(_tree);
        }

        private BinaryTree<BSPData> BSP(BinaryTree<BSPData> tree, int iterations)
        {
            BinaryTree<BSPData> leftChild, rightChild;
            float cutX, cutY;

            if ((tree == null) || NoSpaceForOneRoom(tree))
            {
                return null;
            }
            else
            {
                var positionVector = tree.Root.PartitionBounds.center;
                var sizeVector = tree.Root.PartitionBounds.size;

                if (ContinueDividing(tree, iterations))
                {
                    if (sizeVector.x == sizeVector.y)
                    {
                        if (Random.value < 0.5) // Divide horizontally.
                        {
                            cutY = SplitHorizontally(tree.Root);
                            leftChild = new BinaryTree<BSPData>(new BSPData(new Bounds(new Vector2(positionVector.x, (float)(positionVector.y + cutY)),
                                                                                       new Vector2(sizeVector.x, (float)(sizeVector.y - cutY)))),
                                                                null,
                                                                null);
                            rightChild = new BinaryTree<BSPData>(new BSPData(new Bounds(positionVector,
                                                                                        new Vector2(sizeVector.x, (float)cutY))),
                                                                 null,
                                                                 null);
                        }
                        else // Divide vertically.
                        {
                            cutX = SplitVertically(tree.Root);
                            leftChild = new BinaryTree<BSPData>(new BSPData(new Bounds(positionVector,
                                                                                       new Vector2((float)cutX, sizeVector.y))));
                            rightChild = new BinaryTree<BSPData>(new BSPData(new Bounds(new Vector2((float)(positionVector.x + cutX), positionVector.y),
                                                                                        new Vector2((float)(sizeVector.x - cutX), sizeVector.y))));
                        }
                    }
                    else
                    {
                        if (tree.Root.PartitionBounds.size.x > tree.Root.PartitionBounds.size.y) // Divide vertically.
                        {
                            cutX = SplitVertically(tree.Root);
                            leftChild = new BinaryTree<BSPData>(new BSPData(new Bounds(positionVector,
                                                                                       new Vector2(cutX, sizeVector.y))));
                            rightChild = new BinaryTree<BSPData>(new BSPData(new Bounds(new Vector2(positionVector.x + cutX, positionVector.y),
                                                                                        new Vector2(sizeVector.x - cutX, sizeVector.y))));
                        }
                        else // Divide horizontally.
                        {
                            cutY = SplitHorizontally(tree.Root);
                            leftChild = new BinaryTree<BSPData>(new BSPData(new Bounds(new Vector2(positionVector.x, positionVector.y + cutY),
                                                                                       new Vector2(sizeVector.x, sizeVector.y - cutY))),
                                                                null,
                                                                null);
                            rightChild = new BinaryTree<BSPData>(new BSPData(new Bounds(positionVector,
                                                                                        new Vector2(sizeVector.x, cutY))),
                                                                 null,
                                                                 null);
                        }
                    }

                    return new BinaryTree<BSPData>(tree.Root,
                        // (BinaryTree<BSPdata>)
                        BSP(leftChild, iterations + 1),
                        // (BinaryTree<BSPdata>)
                        BSP(rightChild, iterations + 1)
                    );
                }
                else // No more dividing.
                {
                    return tree;
                }
            }
        }

        /// <summary>
        /// If no single room can be adjusted returns true, false otherwise.
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        private bool NoSpaceForOneRoom(BinaryTree<BSPData> tree)
        {
            BSPData root = tree.Root;
            return (root.PartitionBounds.size.x < maxRoomSize.x) || (root.PartitionBounds.size.y < maxRoomSize.y);
        }

        /// <summary>
        /// If two rooms can still be added, returns true; otherwise returns false.
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="iterations"></param>
        /// <returns></returns>
        private bool ContinueDividing(BinaryTree<BSPData> tree, int iterations)
        {
            BSPData root = tree.Root;

            var canTwoRoomsFitHorizontally = (root.PartitionBounds.size.x >= 2 * (maxRoomSize.x + offset))
                                             && (root.PartitionBounds.size.y >= (maxRoomSize.y + offset));

            var isHSizeBiggerThanARoom = root.PartitionBounds.size.x >= (maxRoomSize.x + offset);

            var isVSizeBiggerThanTwoRooms = root.PartitionBounds.size.y >= 2 * (maxRoomSize.y + offset);

            var doMoreIterations = iterations < numberIterations;

            return (canTwoRoomsFitHorizontally || isHSizeBiggerThanARoom && isVSizeBiggerThanTwoRooms) &&
                   doMoreIterations;
        }

        /// <summary>
        /// It returns a valid value to admit one room in each division of the space. 
        /// It considers an offset (gap with the partition's walls) to avoid exact partitions of the search space.
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private float SplitHorizontally(BSPData root)
        {
            return Random.Range(maxRoomSize.y + offset, root.PartitionBounds.size.y - maxRoomSize.y - offset);
        }

        /// <summary>
        /// Returns a valid value to admit one room in each division of the space.
        /// It considers an offset (gap with the partition's walls) to avoid exact partitions of the search space.
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private float SplitVertically(BSPData root)
        {
            return Random.Range(maxRoomSize.x + offset, root.PartitionBounds.size.x - maxRoomSize.x - offset);
        }

        private void OnDrawGizmos()
        {
            if (_tree != null)
                if (drawPartitions)
                    BinaryTreeUtils<BSPData>.DrawGizmosPartitions(_tree);

            if (_corridorList != null && _corridorList.Count > 0 && drawCorridors)
                BinaryTreeUtils<BSPData>.DrawGizmosCorridorList(_corridorList);

            if (_roomList != null && _roomList.Count > 0 && drawRooms)
                BinaryTreeUtils<BSPData>.DrawGizmosRoomList(_roomList);
        }

        // TODO: Colocar los pasillos en posiciones de tiles en lugar del centro: cuando el tamaño de las salas es par, el pasillo no se colocará correctamente.
        // Creo que me daría igual si los centros son una sala o un pasillo.
        private List<Corridor> GenerateCorridors(BinaryTree<BSPData> tree)
        {
            var corridorList = new List<Corridor>();

            if (tree != null)
            {
                if (tree.HasTwoChilds())
                {
                    // REVIEW: Con la siguiente línea solo se generan ciertos pasillos.
                    if ((tree.Left.IsALeaf() && tree.Right.IsALeaf()) ||
                        (tree.Left.IsALeaf() && !tree.Right.IsALeaf()) ||
                        (!tree.Left.IsALeaf() && tree.Right.IsALeaf()))
                    {
                        var corridorCenter = (tree.Left.Root.Center + tree.Right.Root.Center) / 2;
                        var corridorGO = Instantiate(corridorPrefab, corridorCenter, Quaternion.identity,
                            corridorParentT);
                        if (tree.Left.Root.Center.x == tree.Right.Root.Center.x)
                        {
                            // if (tree.Left.Root.Center.y < tree.Right.Root.Center.y)
                            // {
                            //     corridorGO.transform.localScale = new Vector2(corridorWidth, Vector2.Distance(tree.Left.Root.TopCenterPosition, tree.Right.Root.BottomCenterPosition));
                            // }
                            // else
                            // {
                            //     corridorGO.transform.localScale = new Vector2(corridorWidth, Vector2.Distance(tree.Left.Root.BottomCenterPosition, tree.Right.Root.TopCenterPosition));
                            // }
                            corridorGO.transform.localScale = new Vector2(corridorWidth,
                                Vector2.Distance(tree.Left.Root.Center, tree.Right.Root.Center));
                        }
                        else
                        {
                            // if (tree.Left.Root.Center.x < tree.Right.Root.Center.x)
                            // {
                            //     corridorGO.transform.localScale = new Vector2(Vector2.Distance(tree.Left.Root.RightCenterPosition, tree.Right.Root.LeftCenterPosition), corridorWidth);
                            // }
                            // else
                            // {
                            //     corridorGO.transform.localScale = new Vector2(Vector2.Distance(tree.Left.Root.LeftCenterPosition, tree.Right.Root.RightCenterPosition), corridorWidth);
                            // }
                            corridorGO.transform.localScale = new Vector2(
                                Vector2.Distance(tree.Left.Root.Center, tree.Right.Root.Center), corridorWidth);
                        }

                        corridorList.Add(new Corridor(tree.Left.Root.Center, tree.Right.Root.Center, corridorWidth,
                            corridorGO));
                    }
                }

                corridorList = ListUtils.Concat(corridorList, GenerateCorridors(tree.Left));
                corridorList = ListUtils.Concat(corridorList, GenerateCorridors(tree.Right));
            }

            return corridorList;
        }

        private List<Corridor> GenerateCorridorsGood(BinaryTree<BSPData> tree)
        {
            var corridorList = new List<Corridor>();
            var rightNodeList = new List<BSPData>();
            var leftNodeList = new List<BSPData>();
            var rightNode = new BSPData();
            var leftNode = new BSPData();

            if (tree != null)
            {
                if (tree.HasTwoChilds())
                {
                    BinaryTreeUtils<BSPData>.GetAllChildren(tree.Left, ref leftNodeList);
                    BinaryTreeUtils<BSPData>.GetAllChildren(tree.Right, ref rightNodeList);
                    GetNearestNodes(leftNodeList, rightNodeList, out leftNode, out rightNode);
                    GenerateCorridor(leftNode, rightNode, ref corridorList);

                    GenerateCorridorsGood(tree.Left);
                    GenerateCorridorsGood(tree.Right);
                }

                if (tree.Left == null)
                {
                    GenerateCorridorsGood(tree.Right);
                }

                if (tree.Right == null)
                {
                    GenerateCorridorsGood(tree.Left);
                }
            }

            return corridorList;
        }

        private void GetNearestNodes(List<BSPData> leftList, List<BSPData> rightList, out BSPData finalLeftNode,
            out BSPData finalRightNode)
        {
            var minDistance = Mathf.Infinity;
            var currentRightNode = new BSPData();
            var currentLeftNode = new BSPData();
            foreach (var leftNode in leftList)
            {
                foreach (var rightNode in rightList)
                {
                    var distance = Vector2.Distance(leftNode.Center, rightNode.Center);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        currentLeftNode = leftNode;
                        currentRightNode = rightNode;
                    }
                }
            }

            finalLeftNode = currentLeftNode;
            finalRightNode = currentRightNode;
        }

        private bool GenerateCorridor(BSPData roomOne, BSPData roomTwo, ref List<Corridor> corridorList)
        {
            var roomOnePosition = roomOne.Center;
            var roomTwoPosition = roomTwo.Center;
            var minDistanceX = roomOne.PartitionBounds.size.x / 2 + roomTwo.PartitionBounds.size.x / 2;
            var minDistanceY = roomOne.PartitionBounds.size.y / 2 + roomTwo.PartitionBounds.size.y / 2;
            var relativeDistanceX = roomTwoPosition.x - roomOnePosition.x;
            var relativeDistanceY = roomTwoPosition.y - roomOnePosition.y;


            Directions currentDirection;

            if (minDistanceX < Mathf.Abs(relativeDistanceX) && minDistanceY < Mathf.Abs(relativeDistanceY))
            {
                // Rooms don't overlap.
                return false;
            }

            if (minDistanceX > Mathf.Abs(relativeDistanceX))
            {
                // Rooms overlap in X axis.
                currentDirection = relativeDistanceY > 0 ? Directions.Up : Directions.Down;
            }
            else
            {
                // Rooms overlap in Y axis.
                currentDirection = relativeDistanceX > 0 ? Directions.Right : Directions.Left;
            }

            var corridorCenter = CoordinateOfCorridorCenter();
            var corridorGO = Instantiate(corridorPrefab, corridorCenter, Quaternion.identity,
                corridorParentT);

            switch (currentDirection)
            {
                case Directions.Up:
                    corridorGO.transform.localScale = new Vector3(corridorWidth,
                        Mathf.Abs(roomTwo.BottomCenterPosition.y - roomOne.TopCenterPosition.y), 1);
                    break;
                case Directions.Down:

                    corridorGO.transform.localScale = new Vector3(corridorWidth,
                        Mathf.Abs(roomTwo.TopCenterPosition.y - roomOne.BottomCenterPosition.y), 1);
                    break;
                case Directions.Right:
                    corridorGO.transform.localScale =
                        new Vector3(Mathf.Abs(roomTwo.LeftCenterPosition.x - roomOne.RightCenterPosition.x), corridorWidth, 1);
                    break;
                case Directions.Left:
                    corridorGO.transform.localScale =
                        new Vector3(Mathf.Abs(roomTwo.RightCenterPosition.x - roomOne.LeftCenterPosition.x), corridorWidth, 1);
                    break;
            }

            corridorList.Add(new Corridor(roomOne.Center, roomTwo.Center, corridorWidth,
                corridorGO));

            return true;

            Vector2 CoordinateOfCorridorStart()
            {
                float lower;
                float higher;
                float coordinateX = 0, coordinateY = 0;

                switch (currentDirection)
                {
                    case Directions.Left:
                    case Directions.Right:
                        lower = Mathf.Max(roomOne.BottomLeftCorner.y, roomTwo.BottomLeftCorner.y);
                        higher = Mathf.Max(roomOne.TopLeftCorner.y, roomTwo.TopLeftCorner.y);


                        coordinateX = roomOne.Center.x + Mathf.Sign((int)currentDirection) * roomOne.PartitionBounds.size.x;
                        coordinateY = Random.Range(lower, higher);
                        break;
                    case Directions.Up:
                    case Directions.Down:
                        lower = Mathf.Max(roomOne.BottomLeftCorner.x, roomTwo.BottomLeftCorner.x);
                        higher = Mathf.Max(roomOne.BottomRightCorner.x, roomTwo.BottomRightCorner.x);


                        coordinateX = Random.Range(lower, higher);
                        coordinateY = roomOne.Center.y + Mathf.Sign((int)currentDirection) * roomOne.PartitionBounds.size.y;
                        break;
                }

                return new Vector2(coordinateX, coordinateY);
            }

            Vector2 CoordinateOfCorridorCenter()
            {
                float lower;
                float higher;
                float coordinateX = 0, coordinateY = 0;

                switch (currentDirection)
                {
                    case Directions.Left:
                    case Directions.Right:
                        lower = Mathf.Max(roomOne.BottomCenterPosition.y, roomTwo.BottomCenterPosition.y);
                        higher = Mathf.Max(roomOne.TopCenterPosition.y, roomTwo.TopCenterPosition.y);


                        coordinateX = (roomOne.Center.x + Mathf.Sign((int)currentDirection) * roomOne.PartitionBounds.size.x +
                            roomTwo.Center.x - Mathf.Sign((int)currentDirection) * roomTwo.PartitionBounds.size.x) / 2f;
                        coordinateY = Random.Range(lower, higher);
                        break;
                    case Directions.Up:
                    case Directions.Down:
                        lower = Mathf.Max(roomOne.LeftCenterPosition.x, roomTwo.LeftCenterPosition.x);
                        higher = Mathf.Max(roomOne.RightCenterPosition.x, roomTwo.RightCenterPosition.x);


                        coordinateX = Random.Range(lower, higher);
                        coordinateY = (roomOne.Center.y + Mathf.Sign((int)currentDirection) * roomOne.PartitionBounds.size.y +
                            roomOne.Center.y - Mathf.Sign((int)currentDirection) * roomOne.PartitionBounds.size.y) / 2f;
                        break;
                }

                return new Vector2(coordinateX, coordinateY);
            }
        }

        /// <summary>
        /// Generates and instantiates the different rooms of the dungeon.
        /// </summary>
        /// <remarks>
        /// The size of the rooms generated will be an integer since we are working with tiles. 
        /// </remarks>
        /// <param name="tree"></param>
        /// <returns></returns>
        private List<Room> GenerateRooms(BinaryTree<BSPData> tree)
        {
            var roomList = new List<Room>();

            if (tree != null)
            {
                if (tree.IsALeaf()) // The node has no childs i.e., it is a final room.
                {
                    var roomSizeXPerturbation = Random.Range(roomSizePerturbation, 1.0f);
                    var roomSizeYPerturbation = Random.Range(roomSizePerturbation, 1.0f);

                    var actualRoomSizeX = Mathf.FloorToInt(maxRoomSize.x * roomSizeXPerturbation);
                    var actualRoomSizeY = Mathf.FloorToInt(maxRoomSize.y * roomSizeYPerturbation);

                    Vector2 actualCenter;

                    if (actualRoomSizeX % 2 == 0)
                    {
                        actualCenter.x = Mathf.RoundToInt(tree.Root.Center.x);
                    }
                    else
                    {
                        // Since the size in x is odd, generate a center position which is unit and a half position to fit the tilemap perfectly.
                        actualCenter.x = UnitAndHalfPosition(tree.Root.Center.x);
                    }

                    if (actualRoomSizeY % 2 == 0)
                    {
                        actualCenter.y = Mathf.RoundToInt(tree.Root.Center.y);
                    }
                    else
                    {
                        // Since the size in y is odd, generate a center position which is unit and a half position to fit the tilemap perfectly.
                        actualCenter.y = UnitAndHalfPosition(tree.Root.Center.y);
                    }

                    var roomGO = Instantiate(roomPrefab, actualCenter, Quaternion.identity, roomParentT);
                    roomGO.transform.localScale = new Vector2(actualRoomSizeX, actualRoomSizeY);

                    var room = new Room(actualCenter, new Vector2(actualRoomSizeX, actualRoomSizeY), roomGO);

                    tree.Root.StoredRoom = room;

                    roomList.Add(room);
                }

                roomList = ListUtils.Concat(roomList, GenerateRooms(tree.Left));
                roomList = ListUtils.Concat(roomList, GenerateRooms(tree.Right));
            }

            return roomList;

            static float UnitAndHalfPosition(float number)
            {
                var rounded = Mathf.RoundToInt(number);
                return rounded + .5f;
            }
        }
    }
}