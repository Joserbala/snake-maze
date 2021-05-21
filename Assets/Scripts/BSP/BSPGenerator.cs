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
        private BinaryTree<BSPData> _tree; //The structure that will stored that whole information of the partitions

        //generation of data thorugh the information stored in te Binary Tree
        private List<Corridor> _corridorList; //List with corridor data
        private List<Room> _roomList;  //A room is defined by a position (center) and a size of the room. There will
                                       // bee a room for each partition. 

        // Start is called before the first frame update
        void Start()
        {
            _rootdata = new BSPData(Vector2.zero, mapSize);
            _tree = BSP(new BinaryTree<BSPData>(_rootdata, null, null), 0);  //We put the info related to the whole map in the tree root.

            //Data generation
            //All the generate procedures might be joined to avoid distinct traversals of the same Binary tree (performance optimization)
            //          but for pedagogy purposes it is not done here.                                                    
            _corridorList = GenerateCorridors(_tree);
            _roomList = GenerateRooms(_tree, roomSizePerturbation);

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
            float cutx, cuty;

            if ((tree == null) || (NoSpaceforOneRoom(tree)))
                return null;
            else
            {
                Vector2 pos = tree.Root.Position;
                Vector2 size = tree.Root.Size;

                if (ContinueDividing(tree, iterations))
                {
                    if (size.x == size.y)
                    {
                        if (Random.value < 0.5) // Divide Horizontally
                        {
                            cuty = SplitHorizontally(tree.Root);
                            leftChild = new BinaryTree<BSPData>(
                                            new BSPData(
                                                new Vector2(pos.x, (float)(pos.y + cuty)),  //Position
                                                new Vector2(size.x, (float)(size.y - cuty))                 //size  
                                            ), null, null
                                        );
                            righChild = new BinaryTree<BSPData>(
                                            new BSPData(
                                                pos,  //Position
                                                new Vector2(size.x, (float)cuty)  //size  
                                            ), null, null
                                        );
                        }
                        else //Divide Vertically
                        {
                            cutx = SplitVertically(tree.Root);
                            leftChild = new BinaryTree<BSPData>(
                                            new BSPData(
                                                pos,  //Position
                                                new Vector2((float)cutx, size.y) //size  
                                            )
                                        );
                            righChild = new BinaryTree<BSPData>(
                                            new BSPData(
                                                new Vector2((float)(pos.x + cutx), pos.y),  //Position
                                                new Vector2((float)(size.x - cutx), size.y)  //size  
                                            )
                                        );
                        }
                    }
                    else
                    {
                        if (tree.Root.Size.x > tree.Root.Size.y)
                        {   //Divide Vertically
                            cutx = SplitVertically(tree.Root);
                            leftChild = new BinaryTree<BSPData>(
                                            new BSPData(
                                                pos,  //Position
                                                new Vector2(cutx, size.y) //size  
                                            )
                                        );
                            righChild = new BinaryTree<BSPData>(
                                            new BSPData(
                                                new Vector2(pos.x + cutx, pos.y),  //Position
                                                new Vector2(size.x - cutx, size.y)  //size  
                                            )
                                        );
                        }
                        else
                        {   // Divide Horizontally
                            cuty = SplitHorizontally(tree.Root);
                            leftChild = new BinaryTree<BSPData>(
                                            new BSPData(
                                                new Vector2(pos.x, pos.y + cuty),  //Position
                                                new Vector2(size.x, size.y - cuty)                 //size  
                                            ), null, null
                                        );
                            righChild = new BinaryTree<BSPData>(
                                            new BSPData(
                                                pos,  //Position
                                                new Vector2(size.x, cuty)  //size  
                                            ), null, null
                                        );
                        }
                    }
                    return new BinaryTree<BSPData>(tree.Root,
                                            //(BinaryTree<BSPdata>)
                                            BSP(leftChild, iterations + 1),
                                            //(BinaryTree<BSPdata>)
                                            BSP(righChild, iterations + 1)
                                          );
                }
                else  // No dividing anymore
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
        private bool NoSpaceforOneRoom(BinaryTree<BSPData> tree)
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

            bool canTwoRoomsFitHorizontally = (root.Size.x >= 2 * (minimalRoomSize.x + offset))
                && (root.Size.y >= (minimalRoomSize.y + offset));

            bool isHSizeBiggerThanARoom = root.Size.x >= (minimalRoomSize.x + offset);

            bool isVSizeBiggerThanTwoRooms = root.Size.y >= 2 * (minimalRoomSize.y + offset);

            bool doMoreIterations = iterations < numberIterations;

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
            List<Corridor> corridorList = new List<Corridor>();
            if (tree != null)
            {
                if (tree.HasTwoChilds())
                {
                    corridorList.Add(new Corridor(tree.Left.Root.Center, tree.Right.Root.Center, corridorWidth));
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

        private List<Room> GenerateRooms(BinaryTree<BSPData> tree, float roomSizePerturbation)
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

                    GameObject corridor = Instantiate(roomPrefab, tree.Root.Center, Quaternion.identity, roomParentT);
                    corridor.transform.localScale = new Vector2(actualRoomSizeX, actualRoomSizeY);

                    roomList.Add(new Room(tree.Root.Center,
                                      new Vector2(actualRoomSizeX,
                                                  actualRoomSizeY),
                                      corridor));
                }

                roomList = Concat(roomList, GenerateRooms(tree.Left, roomSizePerturbation));
                roomList = Concat(roomList, GenerateRooms(tree.Right, roomSizePerturbation));
            }

            return roomList;
        }
    }
}