using System.Collections.Generic;
using SnakeMaze.Structures;
using SnakeMaze.Utils;
using UnityEngine;

namespace SnakeMaze.BSP
{
    public class BSPGenerator : MonoBehaviour
    {
        [Header("Parameter Configuration")]
        [Range(0.25f, 1.0f)]
        [SerializeField] private float roomSizePerturbation;
        [Tooltip("Width of the corridors.")]
        [SerializeField] private int corridorWidth;
        [Tooltip("The number of Rooms to be created = 2^numberIterations")]
        [SerializeField] private int numberIterations;
        [Tooltip("Space for the rooms with reference to the walls / partitions.")]
        [SerializeField] private int offset = 1;
        [SerializeField] private Vector2 mapSize;
        [SerializeField] private Vector2 minimalRoomSize;

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
        /// <summary>Structure that will store the whole information of the partitions.</summary>
        private BinaryTree<BSPData> _tree;

        // Data generated with the information stored in the Binary Tree.
        /// <summary>List with Corridor data.</summary>
        private List<Corridor> _corridorList;
        /// <summary>
        /// A room is defined by a position (center) and a size of the room. There will be a room for each partition.
        /// </summary>
        private List<Room> _roomList;

        void Start()
        {
            // Putting the information related to the whole map in the tree root.
            _rootdata = new BSPData(Vector2.zero, mapSize);
            _tree = BSP(new BinaryTree<BSPData>(_rootdata, null, null), 0);

            // Data generation.
            // REVIEW: All the Generate methodsd might be joined to avoid different traversals of the same Binary tree (performance optimization).                                               
            _corridorList = GenerateCorridors(_tree);
            _roomList = GenerateRooms(_tree);

            if (printTreeInConsole)
                Debug.Log(BinaryTreeUtils<BSPData>.InOrderHorizontal(_tree, 0));

            if (printCorridorsInConsole)
                foreach (Corridor c in _corridorList)
                    Debug.Log(c);

            if (printRoomsInConsole)
                foreach (Room r in _roomList)
                    Debug.Log(r);
        }

        public BinaryTree<BSPData> BSP(BinaryTree<BSPData> tree, int iterations)
        {
            BinaryTree<BSPData> leftChild, righChild;
            float cutx, cutY;

            if ((tree == null) || NoSpaceForOneRoom(tree))
            {
                return null;
            }
            else
            {
                var positionVector = tree.Root.Position;
                var sizeVector = tree.Root.Size;

                if (ContinueDividing(tree, iterations))
                {
                    if (sizeVector.x == sizeVector.y)
                    {
                        if (Random.value < 0.5) // Divide horizontally.
                        {
                            cutY = SplitHorizontally(tree.Root);
                            leftChild = new BinaryTree<BSPData>(
                                            new BSPData(
                                                new Vector2(positionVector.x, (float)(positionVector.y + cutY)), // Position.
                                                new Vector2(sizeVector.x, (float)(sizeVector.y - cutY)) // Size.  
                                            ), null, null
                                        );
                            righChild = new BinaryTree<BSPData>(
                                            new BSPData(
                                                positionVector, // Position.
                                                new Vector2(sizeVector.x, (float)cutY) // Size.  
                                            ), null, null
                                        );
                        }
                        else // Divide vertically.
                        {
                            cutx = SplitVertically(tree.Root);
                            leftChild = new BinaryTree<BSPData>(
                                            new BSPData(
                                                positionVector, // Position.
                                                new Vector2((float)cutx, sizeVector.y) // Size.  
                                            )
                                        );
                            righChild = new BinaryTree<BSPData>(
                                            new BSPData(
                                                new Vector2((float)(positionVector.x + cutx), positionVector.y), // Position.
                                                new Vector2((float)(sizeVector.x - cutx), sizeVector.y) // Size.
                                            )
                                        );
                        }
                    }
                    else
                    {
                        if (tree.Root.Size.x > tree.Root.Size.y) // Divide vertically.
                        {
                            cutx = SplitVertically(tree.Root);
                            leftChild = new BinaryTree<BSPData>(
                                            new BSPData(
                                                positionVector, // Position.
                                                new Vector2(cutx, sizeVector.y) // Size. 
                                            )
                                        );
                            righChild = new BinaryTree<BSPData>(
                                            new BSPData(
                                                new Vector2(positionVector.x + cutx, positionVector.y), // Position.
                                                new Vector2(sizeVector.x - cutx, sizeVector.y) // Size.
                                            )
                                        );
                        }
                        else // Divide horizontally.
                        {
                            cutY = SplitHorizontally(tree.Root);
                            leftChild = new BinaryTree<BSPData>(
                                            new BSPData(
                                                new Vector2(positionVector.x, positionVector.y + cutY), // Position.
                                                new Vector2(sizeVector.x, sizeVector.y - cutY) // Size.
                                            ), null, null
                                        );
                            righChild = new BinaryTree<BSPData>(
                                            new BSPData(
                                                positionVector, // Position.
                                                new Vector2(sizeVector.x, cutY) // Size.  
                                            ), null, null
                                        );
                        }
                    }
                    return new BinaryTree<BSPData>(tree.Root,
                                            // (BinaryTree<BSPdata>)
                                            BSP(leftChild, iterations + 1),
                                            // (BinaryTree<BSPdata>)
                                            BSP(righChild, iterations + 1)
                                          );
                }
                else  // No more dividing.
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
            return (root.Size.x < minimalRoomSize.x) || (root.Size.y < minimalRoomSize.y);
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

            var canTwoRoomsFitHorizontally = (root.Size.x >= 2 * (minimalRoomSize.x + offset))
                && (root.Size.y >= (minimalRoomSize.y + offset));

            var isHSizeBiggerThanARoom = root.Size.x >= (minimalRoomSize.x + offset);

            var isVSizeBiggerThanTwoRooms = root.Size.y >= 2 * (minimalRoomSize.y + offset);

            var doMoreIterations = iterations < numberIterations;

            return (canTwoRoomsFitHorizontally || isHSizeBiggerThanARoom && isVSizeBiggerThanTwoRooms) && doMoreIterations;
        }

        /// <summary>
        /// It returns a valid value to admit one room in each division of the space. 
        /// It considers an offset (gap with the partition's walls) to avoid exact partitions of the search space.
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private float SplitHorizontally(BSPData root)
        {
            return Random.Range(minimalRoomSize.y + offset, root.Size.y - minimalRoomSize.y - offset);
        }

        /// <summary>
        /// Returns a valid value to admit one room in each division of the space.
        /// It considers an offset (gap with the partition's walls) to avoid exact partitions of the search space.
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private float SplitVertically(BSPData root)
        {
            return Random.Range(minimalRoomSize.x + offset, root.Size.x - minimalRoomSize.x - offset);
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

        private List<Corridor> GenerateCorridors(BinaryTree<BSPData> tree)
        {
            var corridorList = new List<Corridor>();

            if (tree != null)
            {
                if (tree.HasTwoChilds())
                {
                    var corridorCenter = (tree.Left.Root.Center + tree.Right.Root.Center) / 2;
                    var corridorGO = Instantiate(corridorPrefab, corridorCenter, Quaternion.identity, corridorParentT);

                    if (tree.Left.Root.Center.x == tree.Right.Root.Center.x)
                    {
                        corridorGO.transform.localScale = new Vector2(corridorWidth, Vector2.Distance(tree.Left.Root.Center, tree.Right.Root.Center));
                    }
                    else
                    {
                        corridorGO.transform.localScale = new Vector2(Vector2.Distance(tree.Left.Root.Center, tree.Right.Root.Center), corridorWidth);
                    }

                    corridorList.Add(new Corridor(tree.Left.Root.Center, tree.Right.Root.Center, corridorWidth, corridorGO));
                }

                corridorList = Concat(corridorList, GenerateCorridors(tree.Left));
                corridorList = Concat(corridorList, GenerateCorridors(tree.Right));
            }

            return corridorList;
        }

        /// <summary>
        /// Concats two Lists with elements of data T.
        /// </summary>
        /// <param name="firstList"></param>
        /// <param name="secondList"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private List<T> Concat<T>(List<T> firstList, List<T> secondList)
        {
            foreach (T t in secondList)
            {
                firstList.Add(t);
            }

            return firstList;
        }

        /// <summary>
        /// Generates and instantiates the different rooms of the dungeon.
        /// </summary>
        /// <remarks>
        /// The size of the room generated will be an integer since we are working with tiles. 
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

                    var actualRoomSizeX = Mathf.FloorToInt(minimalRoomSize.x * roomSizeXPerturbation);
                    var actualRoomSizeY = Mathf.FloorToInt(minimalRoomSize.y * roomSizeYPerturbation);

                    var roomGO = Instantiate(roomPrefab, tree.Root.Center, Quaternion.identity, roomParentT);
                    roomGO.transform.localScale = new Vector2(actualRoomSizeX, actualRoomSizeY);

                    roomList.Add(new Room(tree.Root.Center,
                                      new Vector2(actualRoomSizeX,
                                                  actualRoomSizeY),
                                      roomGO));
                }

                roomList = Concat(roomList, GenerateRooms(tree.Left));
                roomList = Concat(roomList, GenerateRooms(tree.Right));
            }

            return roomList;
        }
    }
}