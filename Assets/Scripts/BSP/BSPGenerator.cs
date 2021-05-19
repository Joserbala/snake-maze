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
        [SerializeField] private int corridorWidth; //Width of the corridors
        [Tooltip("The number of Rooms to be created = 2^numberIterations")]
        [SerializeField] private int numberIterations;
        [SerializeField] private int offset = 1; //Space for the rooms wrt the walls/partitions
        [SerializeField] private Vector2 mapSize;
        [SerializeField] private Vector2 minimalRoomSize;

        [Header("Flags for Visualization")]
        [SerializeField] private bool drawCorridors;
        [SerializeField] private bool drawPartitions;
        [SerializeField] private bool drawRooms;
        [SerializeField] private bool printCorridorsInConsole;
        [SerializeField] private bool printRoomsInConsole;
        [SerializeField] private bool printTreeInConsole;


        private BSPdata _rootdata;
        private BinaryTree<BSPdata> _tree; //The structure that will stored that whole information of the partitions

        //generation of data thorugh the information stored in te Binary Tree
        private List<Corridor> _corridorList; //List with corridor data
        private List<Room> _roomList;  //A room is defined by a position (center) and a size of the room. There will
                                       // bee a room for each partition. 

        // Start is called before the first frame update
        void Start()
        {
            _rootdata = new BSPdata(Vector2.zero, mapSize);
            _tree = BSP(new BinaryTree<BSPdata>(_rootdata, null, null), 0);  //We put the info related to the whole map in the tree root.

            //Data generation
            //All the generate procedures might be joined to avoid distinct traversals of the same Binary tree (performance optimization)
            //          but for pedagogy purposes it is not done here.                                                    
            _corridorList = GenerateCorridors(_tree);
            _roomList = GenerateRooms(_tree, roomSizePerturbation);

            if (printTreeInConsole)
                Debug.Log(BinaryTreeUtils<BSPdata>.InOrdenHorizontal(_tree, 0));

            if (printCorridorsInConsole)
                foreach (Corridor c in _corridorList)
                    Debug.Log(c);

            if (printRoomsInConsole)
                foreach (Room r in _roomList)
                    Debug.Log(r);


        }

        public BinaryTree<BSPdata> BSP(BinaryTree<BSPdata> tree, int iterations)
        {
            BinaryTree<BSPdata> leftChild = null, righChild = null;
            float cutx, cuty;

            if ((tree == null) || (NoSpaceforOneRoom(tree)))
                return null;
            else
            {
                Vector2 pos = tree.Root.position;
                Vector2 size = tree.Root.size;

                if (ContinueDividing(tree, iterations))
                {
                    if (size.x == size.y)
                    {
                        if (Random.value < 0.5) // Divide Horizontally
                        {
                            cuty = SplitHorizontally(tree.Root);
                            leftChild = new BinaryTree<BSPdata>(
                                            new BSPdata(
                                                new Vector2(pos.x, (float)(pos.y + cuty)),  //Position
                                                new Vector2(size.x, (float)(size.y - cuty))                 //size  
                                            ), null, null
                                        );
                            righChild = new BinaryTree<BSPdata>(
                                            new BSPdata(
                                                pos,  //Position
                                                new Vector2(size.x, (float)cuty)  //size  
                                            ), null, null
                                        );
                        }
                        else //Divide Vertically
                        {
                            cutx = SplitVertically(tree.Root);
                            leftChild = new BinaryTree<BSPdata>(
                                            new BSPdata(
                                                pos,  //Position
                                                new Vector2((float)cutx, size.y) //size  
                                            )
                                        );
                            righChild = new BinaryTree<BSPdata>(
                                            new BSPdata(
                                                new Vector2((float)(pos.x + cutx), pos.y),  //Position
                                                new Vector2((float)(size.x - cutx), size.y)  //size  
                                            )
                                        );
                        }
                    }
                    else
                    {
                        if (tree.Root.size.x > tree.Root.size.y)
                        {   //Divide Vertically
                            cutx = SplitVertically(tree.Root);
                            leftChild = new BinaryTree<BSPdata>(
                                            new BSPdata(
                                                pos,  //Position
                                                new Vector2(cutx, size.y) //size  
                                            )
                                        );
                            righChild = new BinaryTree<BSPdata>(
                                            new BSPdata(
                                                new Vector2(pos.x + cutx, pos.y),  //Position
                                                new Vector2(size.x - cutx, size.y)  //size  
                                            )
                                        );
                        }
                        else
                        {   // Divide Horizontally
                            cuty = SplitHorizontally(tree.Root);
                            leftChild = new BinaryTree<BSPdata>(
                                            new BSPdata(
                                                new Vector2(pos.x, pos.y + cuty),  //Position
                                                new Vector2(size.x, size.y - cuty)                 //size  
                                            ), null, null
                                        );
                            righChild = new BinaryTree<BSPdata>(
                                            new BSPdata(
                                                pos,  //Position
                                                new Vector2(size.x, cuty)  //size  
                                            ), null, null
                                        );
                        }
                    }
                    return new BinaryTree<BSPdata>(tree.Root,
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
        private bool NoSpaceforOneRoom(BinaryTree<BSPdata> tree)
        {
            BSPdata root = tree.Root;
            return (root.size.x < minimalRoomSize.x) || (root.size.y < minimalRoomSize.y);
        }

        /// <summary>
        /// If two rooms can still be added, returns true; otherwise returns false.
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="iterations"></param>
        /// <returns></returns>
        private bool ContinueDividing(BinaryTree<BSPdata> tree, int iterations)
        {
            BSPdata root = tree.Root;

            bool canTwoRoomsFitHorizontally = (root.size.x >= 2 * (minimalRoomSize.x + offset))
                && (root.size.y >= (minimalRoomSize.y + offset));

            bool isHSizeBiggerThanARoom = root.size.x >= (minimalRoomSize.x + offset);

            bool isVSizeBiggerThanTwoRooms = root.size.y >= 2 * (minimalRoomSize.y + offset);

            bool doMoreIterations = iterations < numberIterations;

            return (canTwoRoomsFitHorizontally || isHSizeBiggerThanARoom && isVSizeBiggerThanTwoRooms) && doMoreIterations;
        }

        /// <summary>
        /// It returns a valid value to admit one room in each division of the space. 
        /// It considers an offset (gap with the partition's walls) to avoid exact partitions of the search space.
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private float SplitHorizontally(BSPdata root)
        {
            return Random.Range(minimalRoomSize.y + offset, root.size.y - minimalRoomSize.y - offset);
        }

        /// <summary>
        /// Returns a valid value to admit one room in each division of the space.
        /// It considers an offset (gap with the partition's walls) to avoid exact partitions of the search space.
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private float SplitVertically(BSPdata root)
        {
            return Random.Range(minimalRoomSize.x + offset, root.size.x - minimalRoomSize.x - offset);
        }

        private void OnDrawGizmos()
        {
            if (_tree != null)
                if (drawPartitions)
                    BinaryTreeUtils<BSPdata>.DrawGizmosPartitions(_tree);

            if (_corridorList != null && _corridorList.Count > 0 && drawCorridors)
                BinaryTreeUtils<BSPdata>.DrawGizmosCorridorList(_corridorList);

            if (_roomList != null && _roomList.Count > 0 && drawRooms)
                BinaryTreeUtils<BSPdata>.DrawGizmosRoomList(_roomList);
        }

        private List<Corridor> GenerateCorridors(BinaryTree<BSPdata> tree)
        {
            List<Corridor> list = new List<Corridor>();
            if (tree != null)
            {
                if (tree.hasTwoChilds())
                {
                    list.Add(new Corridor(tree.Left.Root.Center, tree.Right.Root.Center, corridorWidth));
                }

                list = Concat<Corridor>(list, GenerateCorridors(tree.Left));
                list = Concat<Corridor>(list, GenerateCorridors(tree.Right));
            }
            return list;
        }

        /// <summary>
        /// Concats two Lists with elements of data T.
        /// </summary>
        /// <param name="lista1"></param>
        /// <param name="lista2"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private List<T> Concat<T>(List<T> lista1, List<T> lista2)
        {
            foreach (T t in lista2)
                lista1.Add(t);

            return lista1;
        }

        private List<Room> GenerateRooms(BinaryTree<BSPdata> tree, float roomSizePerturbation)
        {
            List<Room> list = new List<Room>();
            if (tree != null)
            {
                if (tree.isAleaf()) // The node has no childs i.e., it is a final room.
                {
                    float roomSizeXPerturbation = Random.Range(roomSizePerturbation, 1.0f);
                    float roomSizeYPerturbation = Random.Range(roomSizePerturbation, 1.0f);
                    list.Add(new Room(tree.Root.Center,
                                      new Vector3(minimalRoomSize.x * roomSizeXPerturbation,
                                                  minimalRoomSize.y * roomSizeYPerturbation,
                                                  0f)));
                }

                list = Concat<Room>(list, GenerateRooms(tree.Left, roomSizePerturbation));
                list = Concat<Room>(list, GenerateRooms(tree.Right, roomSizePerturbation));
            }
            return list;
        }
    }
}