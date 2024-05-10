using System;
using System.Collections.Generic;
using UnityEngine;

namespace Descant.Editor
{
    /// <summary>
    /// Serializable class to hold the data for saving and loading Descant groups
    /// </summary>
    [Serializable]
    public class DescantGroupData
    {
        /// <summary>
        /// The group's custom name
        /// </summary>
        public string Name;
        
        /// <summary>
        /// The group's ID
        /// </summary>
        public int ID;
        
        /// <summary>
        /// The group's position
        /// </summary>
        public Vector2 Position;
        
        /// <summary>
        /// The names of the nodes contained within the group
        /// </summary>
        public List<string> Nodes;
        
        /// <summary>
        /// The IDs of the nodes contained within the group
        /// </summary>
        public List<int> NodeIDs;

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="name">The group's custom name</param>
        /// <param name="id">The group's ID</param>
        /// <param name="position">The group's position</param>
        /// <param name="nodes">The names of the nodes contained within the group</param>
        /// <param name="nodeIDs">The IDs of the nodes contained within the group</param>
        public DescantGroupData(string name, int id, Vector2 position, List<string> nodes, List<int> nodeIDs)
        {
            Name = name;
            ID = id;
            Position = position;
            Nodes = nodes;
            NodeIDs = nodeIDs;
        }

        /// <summary>
        /// Overridden Equals method
        /// </summary>
        /// <param name="other">The object being compared against</param>
        /// <returns>Whether the other object has the same data as this one</returns>
        public override bool Equals(object other)
        {
            try
            {
                _ = (DescantGroupData) other;
            }
            catch
            {
                return false;
            }
            
            return Equals((DescantGroupData)other);
        }
        
        #if UNITY_EDITOR
        /// <summary>
        /// Custom Equals method
        /// </summary>
        /// <param name="other">The data object being compared against</param>
        /// <returns>Whether the other DescantGroupData has the same data as this one</returns>
        public bool Equals(DescantGroupData other)
        {
            return
                Name == other.Name &&
                ID == other.ID &&
                Position == other.Position &&
                DescantEditorUtilities.AreListsEqual(Nodes, other.Nodes) &&
                DescantEditorUtilities.AreListsEqual(NodeIDs, other.NodeIDs);
        }
        #endif
        
        /// <summary>
        /// Overridden ToString method
        /// </summary>
        public override string ToString()
        {
            string temp = "";

            for (int i = 0; i < Nodes.Count; i++)
                temp += " " + NodeIDs[i] + Nodes[i];
            
            return GetType() + " (" + ID + Name + " " + Position + ") (" +
                   (temp.Length > 1 ? temp.Substring(1) : "") + ")";
        }
    }
    
    /// <summary>
    /// Serializable class to hold the data for saving and loading Descant node connections
    /// </summary>
    [Serializable]
    public class DescantConnectionData
    {
        /// <summary>
        /// The name of the node the connection is coming from
        /// </summary>
        public string From;
        
        /// <summary>
        /// The ID of the node the connection is coming from
        /// </summary>
        public int FromID;
        
        /// <summary>
        /// The name of the node the connection is going to
        /// </summary>
        public string To;
        
        /// <summary>
        /// The ID of the node the connection is going to
        /// </summary>
        public int ToID;
        
        /// <summary>
        /// The index of the port that this connection is coming from (base 1 for ChoiceNodes and ResponseNodes)
        /// </summary>
        public int ChoiceIndex;

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="from">The name of the node the connection is coming from</param>
        /// <param name="fromID">The ID of the node the connection is coming from</param>
        /// <param name="to">The name of the node the connection is going to</param>
        /// <param name="toID">The ID of the node the connection is going to</param>
        /// <param name="choiceIndex">
        /// The index of the port that this connection is coming from (base 1 for ChoiceNodes and ResponseNodes)
        /// </param>
        public DescantConnectionData(string from, int fromID, string to, int toID, int choiceIndex = 0)
        {
            From = from;
            FromID = fromID;
            To = to;
            ToID = toID;
            ChoiceIndex = choiceIndex;
        }
        
        /// <summary>
        /// Checks to make sure that the connection isn't an illegal one coming from a Choice node's input port
        /// </summary>
        /// <returns>Whether this connection is illegally coming from a Choice node's input port</returns>
        public bool IllegalChoiceFrom()
        {
            return From.Trim() == "Choice" && ChoiceIndex == 0;
        }

        /// <summary>
        /// Determines whether the connection points to itself
        /// </summary>
        /// <returns></returns>
        public bool ToItself()
        {
            return From == To && FromID == ToID;
        }
        
        /// <summary>
        /// Overridden Equals method
        /// </summary>
        /// <param name="other">The object being compared against</param>
        /// <returns>Whether the other object has the same data as this one</returns>
        public override bool Equals(object other)
        {
            try
            {
                _ = (DescantConnectionData) other;
            }
            catch
            {
                return false;
            }
            
            return Equals((DescantConnectionData)other);
        }

        #if UNITY_EDITOR
        /// <summary>
        /// Custom Equals method
        /// </summary>
        /// <param name="other">The data object being compared against</param>
        /// <returns>Whether the other DescantConnectionData has the same data as this one</returns>
        public bool Equals(DescantConnectionData other)
        {
            return
                From == other.From && FromID == other.FromID &&
                To == other.To && ToID == other.ToID &&
                ChoiceIndex == other.ChoiceIndex;
        }
        #endif

        /// <summary>
        /// Overridden ToString method
        /// </summary>
        public override string ToString()
        {
            return GetType() + " (" + FromID + From + " " + ToID + To + " " + ChoiceIndex + ")";
        }
    }
}