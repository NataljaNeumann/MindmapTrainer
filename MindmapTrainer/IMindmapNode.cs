/*  Mindmap-Trainer aims to help people in training for exams
    Copyright (C) 2024-2025 NataljaNeumann@gmx.de

    This program is free software; you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation; either version 2 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License along
    with this program; if not, write to the Free Software Foundation, Inc.,
    51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace MindmapTrainer
{
    //*******************************************************************************************************
    /// <summary>
    /// Objects that implement this interface provide information about single node of the graph
    /// </summary>
    //*******************************************************************************************************
    public interface IMindmapNode
    {
        //===================================================================================================
        /// <summary>
        /// Gets or sets the text of the node
        /// </summary>
        string Text { get; set; }

        //===================================================================================================
        /// <summary>
        /// Gets the sub-elements of this node
        /// </summary>
        IEnumerable<IMindmapNode> Elements { get; }

        //===================================================================================================
        /// <summary>
        /// Add an element to sub-element
        /// </summary>
        /// <param name="text">The name(text) of the element</param>
        void AddElement(string text);

        //===================================================================================================
        /// <summary>
        /// Tests, if there are sub-elements in this node
        /// </summary>
        bool HasElements { get; }
    }
}
