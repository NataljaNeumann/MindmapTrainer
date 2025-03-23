// Vokabel-Trainer v1.0
// Copyright (C) 2019 NataljaNeumann@gmx.de
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MindmapTrainer
{

    //*******************************************************************************************************
    /// <summary>
    /// This is the main form of the application
    /// </summary>
    //*******************************************************************************************************
    public partial class MindmapTrainerForm : Form
    {
        //===================================================================================================
        /// <summary>
        /// The mind map
        /// </summary>
        SortedDictionary<string, Dictionary<String, bool>> m_oMindMap;

        //===================================================================================================
        /// <summary>
        /// The training results
        /// </summary>
        SortedDictionary<string, string> m_oTrainingResults;


        //===================================================================================================
        /// <summary>
        /// The count of correct answers for each node
        /// </summary>
        SortedDictionary<string, int> m_oCorrectAnswers;

        //===================================================================================================
        /// <summary>
        /// The recently trained subjects, see alsso m_bSkipLastTrained
        /// </summary>
        LinkedList<string> m_oLastTrainedSubjects = new LinkedList<string>();

        //===================================================================================================
        /// <summary>
        /// The total number of errors
        /// </summary>
        int m_nTotalErrors;

        //===================================================================================================
        /// <summary>
        /// Holds information, if recently trained subjects can be skipped
        /// </summary>
        bool m_bSkipLastTrained;

        //===================================================================================================
        /// <summary>
        /// A random number generator for randomization
        /// </summary>
        Random m_oRandomGenerator;

        //===================================================================================================
        /// <summary>
        /// A second random number generator for randomization
        /// </summary>
        Random m_oRandomGenerator2;


        //===================================================================================================
        /// <summary>
        /// The name of the root node of the min map graph
        /// </summary>
        string m_strRootNodeName;

        //===================================================================================================
        /// <summary>
        /// The file name of the mind map
        /// </summary>
        string m_strFilePath;


        //===================================================================================================
        /// <summary>
        /// Indicates, that the tree view started an edit operation (not insertion)
        /// </summary>
        bool m_bTreeViewInEditNodeMode;

        //***************************************************************************************************
        /// <summary>
        /// Represents a node of the mind map
        /// </summary>
        //***************************************************************************************************
        class MindMapNode : IMindmapNode
        {
            //===============================================================================================
            /// <summary>
            /// Holds the text of the node
            /// </summary>
            string m_strText;

            //===============================================================================================
            /// <summary>
            /// The pointer to the form
            /// </summary>
            MindmapTrainerForm m_oTrainerForm;

            //===============================================================================================
            /// <summary>
            /// The parent node of this node, or null
            /// </summary>
            MindMapNode m_oParentNode;

            //===============================================================================================
            /// <summary>
            /// Constructs a new node
            /// </summary>
            /// <param name="tr">The training form</param>
            /// <param name="text">The text of the node</param>
            //===============================================================================================
            public MindMapNode(MindmapTrainerForm oTrainerForm, MindMapNode oParentNode, string strText)
            {
                m_strText = strText;
                m_oTrainerForm = oTrainerForm;
                m_oParentNode = oParentNode;
            }

            #region IMindmapNode Member


            //===============================================================================================
            /// <summary>
            /// Gets or sets the text of the node
            /// </summary>
            public string Text
            {
                get
                {
                    return m_strText;
                }
                set
                {
                    m_strText = value;
                }
            }

            //===============================================================================================
            /// <summary>
            /// Gets the sub-elements of this node
            /// </summary>
            public IEnumerable<IMindmapNode> Elements
            {
                get {
                    if (m_oTrainerForm != null && m_strText != null && m_oTrainerForm.m_oMindMap.ContainsKey(m_strText))
                    {
                        foreach (string text in m_oTrainerForm.m_oMindMap[m_strText].Keys)
                        {
                            yield return new MindMapNode(m_oTrainerForm, this, text);
                        }
                    }
                    yield break;
                }
            }

            //===============================================================================================
            /// <summary>
            /// Add an element to sub-element
            /// </summary>
            /// <param name="text">The name(text) of the element</param>
            public void AddElement(string text)
            {
                if (m_oTrainerForm != null && m_strText != null)
                {
                    if (!m_oTrainerForm.m_oMindMap.ContainsKey(m_strText))
                        m_oTrainerForm.m_oMindMap[m_strText] = new Dictionary<string,bool>();

                    if (!m_oTrainerForm.m_oTrainingResults.ContainsKey(m_strText))
                    {
                        m_oTrainerForm.m_oTrainingResults[m_strText] = "111011";
                        m_oTrainerForm.m_nTotalErrors += 1;
                    }

                    if (!m_oTrainerForm.m_oCorrectAnswers.ContainsKey(m_strText))
                        m_oTrainerForm.m_oCorrectAnswers[m_strText] = 0;

                    if (!m_oTrainerForm.m_oMindMap[m_strText].ContainsKey(text))
                        m_oTrainerForm.m_oMindMap[m_strText][text] = false;

                    m_oTrainerForm.Save();
                }
            }

            //===================================================================================================
            /// <summary>
            /// Tests, if there are sub-elements in this node
            /// </summary>
            public bool HasElements
            {
                get { 
                    return (m_oTrainerForm != null && m_strText != null && m_oTrainerForm.m_oMindMap.ContainsKey(m_strText) && m_oTrainerForm.m_oMindMap[m_strText].Count>0);
                }
            }

            #endregion
        }

        //===================================================================================================
        /// <summary>
        /// Constructs a new mind map trainer form
        /// </summary>
        //===================================================================================================
        public MindmapTrainerForm()
        {
            InitializeComponent();

            m_ctlTreeView.RightToLeft = RightToLeft;
            m_ctlTreeView.RightToLeftLayout = RightToLeftLayout;
            m_tbxEditNodeText.RightToLeft = RightToLeft;

            // init random with current time
            m_oRandomGenerator = new Random(((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 +
                DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond);
            m_oRandomGenerator2 = new Random((((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);

            m_btnHiddenAcceptButton.Location = 
                new Point(-m_btnHiddenAcceptButton.Size.Width,
                          -m_btnHiddenAcceptButton.Size.Height);


            // for right to left cultures switch to the tree view per default
            if (this.RightToLeftLayout)
            {
                m_ctlTreeView.Show();
                m_ctlTreeView.Enabled = false;
                m_ctlMindmapNodeView.Hide();
            }
            else
            {
                m_ctlTreeView.Hide();
            }

            m_ctlTreeView.RightToLeft = RightToLeftLayout ? RightToLeft.Yes : RightToLeft.No;

            EnableDisableMenu();
        }

        
        //===================================================================================================
        /// <summary>
        /// This is execuded then the hidden accept button is triggered
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event args</param>
        //===================================================================================================
        private void hiddenAcceptButton_Click(object sender, EventArgs e)
        {

            if (m_bTreeViewInEditNodeMode)
            {
            }
            else
            {

                TreeNode currentNode = (TreeNode)m_tbxEditNodeText.Tag;
                MindMapNode currentNodeData = (MindMapNode)currentNode.Tag;
                currentNodeData.Text = m_tbxEditNodeText.Text;
                currentNode.Text = m_tbxEditNodeText.Text;
                m_tbxEditNodeText.Visible = false;

                if (currentNode.Parent != null)
                {
                    ((MindMapNode)currentNode.Parent.Tag).AddElement(m_tbxEditNodeText.Text);

                    /*
                    MindMapNode newNodeData = new Node("");

                    TreeNode newNode = new TreeNode() { Tag = newNodeData };
                    currentNode.Parent.Nodes.Add(newNode);
                    StartEditingNode(newNode);
                     */
                }
            }


        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user clicks 'New'
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event args</param>
        //===================================================================================================
        private void newMindmapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_dlgSaveFileDialog1.FileName = "";
            m_dlgSaveFileDialog1.DefaultExt = ".MindMap.xml";
            if (m_dlgSaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                m_ctlToggleGUI.Enabled = true;
                m_oMindMap = new SortedDictionary<string,Dictionary<string,bool>>();
                m_oTrainingResults = new SortedDictionary<string, string>();
                m_oCorrectAnswers = new SortedDictionary<string, int>();

                m_strRootNodeName = m_dlgSaveFileDialog1.FileName.Substring(
                    m_dlgSaveFileDialog1.FileName.LastIndexOf('\\')+1).Replace(".MindMap.xml","");
                m_strFilePath = m_dlgSaveFileDialog1.FileName;
                m_oMindMap[m_strRootNodeName] = new Dictionary<string, bool>();
                m_oTrainingResults[m_strRootNodeName] = "111011";
                m_nTotalErrors = 1;
                m_oCorrectAnswers[m_strRootNodeName] = 0;

                if (m_ctlTreeView.Visible)
                {
                    PopulateTreeView();
                }
                else
                {
                    m_ctlMindmapNodeView.Node = new MindMapNode(this, null, m_strRootNodeName);
                    m_ctlMindmapNodeView.Show();
                }

                EnableDisableMenu();
            }
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user clicks on 'open'
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event args</param>
        //===================================================================================================
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_dlgOpenFileDialog1.FileName = "";
            m_dlgOpenFileDialog1.DefaultExt = ".MindMap.xml";

            if (m_dlgOpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                m_ctlToggleGUI.Enabled = true;

                m_oMindMap = new SortedDictionary<string, Dictionary<string, bool>>();
                m_oTrainingResults = new SortedDictionary<string, string>();
                m_oCorrectAnswers = new SortedDictionary<string, int>();

                m_strFilePath = m_dlgOpenFileDialog1.FileName;
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(m_dlgOpenFileDialog1.FileName);
                foreach (System.Xml.XmlElement e5 in doc.SelectNodes("/mindmap/start"))
                {
                    m_strRootNodeName = e5.InnerText;
                };

                m_oMindMap[m_strRootNodeName]=new Dictionary<string,bool>();

                foreach (System.Xml.XmlElement e4 in doc.SelectNodes("/mindmap/subject"))
                {
                    foreach (System.Xml.XmlNode e2 in e4.SelectNodes("@name"))
                    {
                        Dictionary<string,bool> elements = new Dictionary<string, bool>();
                        foreach (System.Xml.XmlNode e3 in e4.SelectNodes("element"))
                        {
                            elements[e3.InnerText] = false;
                        }
                        m_oMindMap[e2.InnerText] = elements;

                        m_oTrainingResults[e2.InnerText] = "111011";
                        foreach (System.Xml.XmlNode e5 in e4.SelectNodes("@training"))
                        {
                            string s = e5.InnerText;
                            while (s.Length < 6)
                                s = "1" + s;
                            if (s.Length > 6)
                                s = s.Substring(0, 6);
                            m_oTrainingResults[e2.InnerText] = s;
                        }

                        // count errors
                        string s2 = m_oTrainingResults[e2.InnerText];
                        m_nTotalErrors += s2.Length - s2.Replace("0", "").Length;

                        // load correct answers for the subject
                        int res = 0;
                        foreach (System.Xml.XmlNode e6 in e4.SelectNodes("@correct"))
                        {
                            if (!int.TryParse(e6.InnerText, out res))
                            {
                                res = 0;
                            }
                        }
                        m_oCorrectAnswers[e2.InnerText] = res;
                    }
                }

                if (m_ctlTreeView.Visible)
                {
                    PopulateTreeView();
                }
                else
                {
                    m_ctlMindmapNodeView.Node = new MindMapNode(this, null, m_strRootNodeName);
                    m_ctlMindmapNodeView.Show();
                }
                 

                EnableDisableMenu();

            }
        }

        //===================================================================================================
        /// <summary>
        /// Saves the mind map and training results
        /// </summary>
        //===================================================================================================
        private void Save()
        {
            System.IO.FileInfo fi2 = new System.IO.FileInfo(m_strFilePath);
            if (fi2.Exists)
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(m_strFilePath + ".bak");
                if (fi.Exists)
                {
                    fi.Delete();
                }

                fi2.MoveTo(fi2.FullName+".bak");
            }

            try
            {
                using (System.IO.StreamWriter w = 
                    new System.IO.StreamWriter(m_strFilePath, false, Encoding.UTF8))
                {
                    w.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                    w.WriteLine("<mindmap>");
                    w.WriteLine("  <start>{0}</start>", PrepareForXml(m_strRootNodeName));
                    foreach (KeyValuePair<string, Dictionary<string, bool>> pair in m_oMindMap)
                    {
                        w.Write("  <subject name=\"{0}\" training=\"{1}\" correct=\"{2}\">", 
                            PrepareForXml(pair.Key), m_oTrainingResults[pair.Key], m_oCorrectAnswers[pair.Key]);
                        foreach (string strElement in pair.Value.Keys)
                        {
                            w.WriteLine();
                            w.Write("    <element>{0}</element>", PrepareForXml(strElement));
                        }
                        w.WriteLine("</subject>");
                    }
                    w.WriteLine("</mindmap>");
                }
            }
            catch (Exception ex)
            {
                try
                {
                    System.IO.FileInfo fi3 = new System.IO.FileInfo(m_strFilePath);
                    if (fi3.Exists)
                        fi3.Delete();
                }
                catch
                {
                }

                try
                {
                    System.IO.FileInfo fi4 = new System.IO.FileInfo(m_strFilePath + ".bak");
                    if (fi4.Exists)
                        fi4.MoveTo(m_strFilePath);
                }
                catch
                {
                }

                System.Windows.Forms.MessageBox.Show(this, ex.Message, Properties.Resources.ErrorSavingFile, 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //===================================================================================================
        /// <summary>
        /// Prepares a text for XML
        /// </summary>
        /// <param name="text">Text to convert to XML representation</param>
        /// <returns>Converted text</returns>
        //===================================================================================================
        string PrepareForXml(string text)
        {
            return text.Replace("&", "&amp;").Replace("\"","&quot;")
                .Replace("<", "&lt;").Replace(">", "&gt;");
        }


        //===================================================================================================
        /// <summary>
        /// This is executed when mouse moves over the window for making more random numbers
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event args</param>
        //===================================================================================================
        private void MindmapTrainerForm_MouseMove(object sender, MouseEventArgs e)
        {
            // make randoms less deterministic, whenever possible
            if (m_oRandomGenerator != null)
                m_oRandomGenerator = new Random(m_oRandomGenerator.Next() + 
                    ((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                    DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond + (e.X & 3) * 256);
            if (m_oRandomGenerator2 != null)
                m_oRandomGenerator2 = new Random(m_oRandomGenerator2.Next() + 
                    (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                    DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + 
                    DateTime.UtcNow.DayOfYear + (e.Y & 3) * 256);
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user clicks the 'Training' button
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event args</param>
        //===================================================================================================
        private void trainingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                m_ctlMindmapNodeView.Hide();

                bool bRepeat = true;
                while (bRepeat)
                {
                    bRepeat = false;
                    // there we train one of the words randomly. Words with errors get higher weight
                    int rnd2 = m_oRandomGenerator2.Next();
                    m_oRandomGenerator2 = new Random(rnd2 + 
                        (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + DateTime.UtcNow.Second) * 1000 + 
                        DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);

                    int selectedError = rnd2 % (m_nTotalErrors + m_oMindMap.Count);

                    m_bSkipLastTrained = m_oMindMap.Count > 5;

                    int wordIndex = -1;
                    using (SortedDictionary<string, string>.ValueCollection.Enumerator 
                        values = m_oTrainingResults.Values.GetEnumerator())
                    {
                        while (selectedError >= 0 && values.MoveNext())
                        {
                            wordIndex += 1;
                            if (values.Current.Contains("0"))
                            {
                                selectedError -= values.Current.Length - 
                                    values.Current.Replace("0", "").Length + 1;
                            }
                            else
                                selectedError -= 1;
                        }

                        bRepeat = TrainSubject(wordIndex);
                    };
                }
                Save();
            }
            finally
            {
                m_ctlMindmapNodeView.Show();
            }
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user clicks the 'Intensive training' menu item
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event args</param>
        //===================================================================================================
        private void intensivelyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                m_ctlMindmapNodeView.Hide();

                bool bRepeat = true;
                while (bRepeat)
                {
                    bRepeat = false;
                    // decide, if we will train one subject randomly, or one that needs additional training
                    int rnd = m_oRandomGenerator.Next();
                    m_oRandomGenerator = new Random(rnd + ((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                        DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond);

                    if ((m_nTotalErrors > 0) && (rnd % 100 < 50))
                    {
                        // there we train one of the subjects that need additional training
                        int rnd2 = m_oRandomGenerator2.Next();
                        m_oRandomGenerator2 = new Random(rnd + rnd2 + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                            DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);

                        int selectedError = rnd2 % m_nTotalErrors;

                        m_bSkipLastTrained = m_oMindMap.Count > 10;

                        int wordIndex = -1;
                        using (SortedDictionary<string, string>.ValueCollection.Enumerator 
                            values = m_oTrainingResults.Values.GetEnumerator())
                        {
                            while (selectedError >= 0 && values.MoveNext())
                            {
                                wordIndex += 1;
                                if (values.Current.Contains("0"))
                                {
                                    selectedError -= values.Current.Length - values.Current.Replace("0", "").Length + 1;
                                }
                            }

                            bRepeat = TrainSubject(wordIndex);
                        }
                    }
                    else
                    {
                        // there we train one of the words
                        int rnd2 = m_oRandomGenerator2.Next();
                        m_oRandomGenerator2 = new Random(rnd2 + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                            DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);


                        // calculate mean of correct answers
                        long lTotal = 0;
                        foreach (int i in m_oCorrectAnswers.Values)
                            lTotal += i;

                        int mean = (int)(lTotal / m_oCorrectAnswers.Count);

                        // now calculate the sum of weights of all words
                        int iTotalWeights = 0;
                        foreach (int i in m_oCorrectAnswers.Values)
                        {
                            int weight = mean + 3 - i;
                            if (weight <= 0)
                                weight = 1;
                            iTotalWeights += weight;
                        };

                        int selectedWeight = rnd2 % iTotalWeights;

                        int wordIndex = -1;
                        using (SortedDictionary<string, int>.ValueCollection.Enumerator 
                            values = m_oCorrectAnswers.Values.GetEnumerator())
                        {
                            while (selectedWeight >= 0 && values.MoveNext())
                            {
                                wordIndex += 1;

                                int weight = mean + 3 - values.Current;

                                if (weight <= 0)
                                    weight = 1;

                                selectedWeight -= weight;
                            }

                            m_bSkipLastTrained = m_oTrainingResults.Count > 5;

                            bRepeat = TrainSubject(wordIndex);
                        }
                    }
                }
                Save();
            }
            finally
            {
                m_ctlMindmapNodeView.Show();
            }

        }

        //===================================================================================================
        /// <summary>
        /// This is executed for training the user on a specific subject and interpret the results
        /// </summary>
        /// <param name="index">Index of the subject</param>
        /// <returns>true iff training shall continue</returns>
        //===================================================================================================
        private bool TrainSubject(int index)
        {

            bool bContinue = false;
            bool bCorrect = false;
            foreach (KeyValuePair<string, string> pair in m_oTrainingResults)
            {
                if (0 == index--)
                {
                    if (m_bSkipLastTrained)
                    {
                        foreach (string s in m_oLastTrainedSubjects)
                        {
                            // if we trained this subject recently, then try to skip it
                            if (s.Equals(pair.Key))
                                if (m_oRandomGenerator2.Next(100) > 0)
                                    return true;
                        };
                    }

                    // add the subject to the list of recently trained rotate the list
                    m_oLastTrainedSubjects.AddFirst(pair.Key); 
                    if (m_oLastTrainedSubjects.Count > 5)
                        m_oLastTrainedSubjects.RemoveLast();


                    string subject = pair.Key;
                    using (SubjectTestForm test = new SubjectTestForm() )
                    {
                        test.MouseMove += new System.Windows.Forms.MouseEventHandler(
                            this.MindmapTrainerForm_MouseMove);

                        test.m_lblSubject.Text = subject + ":";
                        StringBuilder b = new StringBuilder();
                        foreach (string s in m_oMindMap[pair.Key].Keys)
                            if (b.Length>0)
                                b.AppendFormat("\r\n> {0}",s);
                            else
                                b.AppendFormat("> {0}",s);

                        test.m_lblElements.Text = b.ToString();

                        switch (test.ShowDialog())
                        {
                            case DialogResult.Yes:
                                bContinue = true;
                                bCorrect = true;
                                break;
                            case DialogResult.No:
                                bContinue = true;
                                bCorrect = false;
                                break;
                            default:
                                bContinue = false;
                                break;
                        }

                        if (bContinue)
                        {
                            string prevResults = m_oTrainingResults[subject];
                            string newResults = (bCorrect ? "1" : "0") + prevResults.Substring(0, 
                                prevResults.Length<5?prevResults.Length:5);
                            m_oTrainingResults[subject] = newResults;
                            m_nTotalErrors += newResults.Length - newResults.Replace("0", "").Length - 
                                (prevResults.Length - prevResults.Replace("0", "").Length);

                            // if the result was correct and we didn't repeat it because of 
                            // earlier mistakes, then increment the number of correct answers
                            if (bCorrect && prevResults.IndexOf('0')<0)
                                m_oCorrectAnswers[subject]++;

                            EnableDisableMenu();                        
                        }
                    }

                    return bContinue;
                }
            }
            return bContinue;
        }

        //===================================================================================================
        /// <summary>
        /// Enables or disables menu items
        /// </summary>
        //===================================================================================================
        private void EnableDisableMenu()
        {
            m_ctlTrainingToolStripMenuItem.Enabled = 
                m_ctlIntensiveToolStripMenuItem.Enabled = 
                    m_oMindMap != null && m_oMindMap.Count > 0;
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user clicks the 'About mind map trainer' menu item
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event args</param>
        //===================================================================================================
        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutForm form = new AboutForm())
            {
                form.ShowDialog(this);
            }
        }

        //===================================================================================================
        /// <summary>
        /// This is executed when user clicks the 'Show licence' menu item
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event args</param>
        //===================================================================================================
        private void licenseMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.gnu.org/licenses/gpl-2.0.html");
        }

        private void m_ctlTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void m_ctlTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu contextMenu = new ContextMenu();
                MenuItem addNodeMenuItem = new MenuItem(Properties.Resources.AddNode);
                addNodeMenuItem.Click += (s, args) => StartEditingNewNode(e.Node);
                contextMenu.MenuItems.Add(addNodeMenuItem);

                MenuItem detachNodeMenuItem = new MenuItem(Properties.Resources.DetachNode);
                detachNodeMenuItem.Click += (s, args) => DetachNode(e.Node);
                contextMenu.MenuItems.Add(detachNodeMenuItem);

                MenuItem editNodeMenuItem = new MenuItem(Properties.Resources.EditNode);
                editNodeMenuItem.Click += (s, args) => EditNode(e.Node);
                contextMenu.MenuItems.Add(editNodeMenuItem);

                contextMenu.Show(m_ctlTreeView, e.Location);
            }
        }

        private void m_ctlTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {

            TreeNode node = e.Node;
            if (node.Nodes[0].Text == "Loading...")
            {
                node.Nodes.Clear(); // Remove the placeholder node
                LoadChildNodes(node);
            }

            /*
            foreach (TreeNode node in m_ctlTreeView.Nodes)
            {
                CollapseAllNodes(node);
            }*/
        }

        private void LoadChildNodes(TreeNode parentNode)
        {
            if (m_oMindMap.ContainsKey(parentNode.Text))
            {
                foreach (string text in m_oMindMap[parentNode.Text].Keys)
                {
                    TreeNode childNode = new TreeNode(text) { Tag = new MindMapNode(this, (MindMapNode)parentNode.Tag, text) };
                    if (m_oMindMap.ContainsKey(text) && m_oMindMap[text].Keys.Count>0)
                    {
                        childNode.Nodes.Add(new TreeNode("Loading...")); // Add placeholder node for child nodes
                    }
                    parentNode.Nodes.Add(childNode);
                }
            }
        }



        private void CollapseAllNodes(TreeNode node)
        {
            foreach (TreeNode subNode in node.Nodes)
            {
                subNode.Collapse();
                CollapseAllNodes(subNode);
            }
        }


        private void StartEditingNewNode(TreeNode parentNode)
        {
            m_bTreeViewInEditNodeMode = false;
            MindMapNode parentNodeData = (MindMapNode)parentNode.Tag;
            MindMapNode newNodeData = new MindMapNode(this, parentNodeData, "");
            parentNode.Expand();

            TreeNode newNode = new TreeNode() { Tag = newNodeData };
            parentNode.Nodes.Add(newNode);
            parentNode.Expand();

            /*
            if (RightToLeftLayout)
            {
                m_tbxEditNodeText.Bounds = new Rectangle(
                    newNode.Bounds.Right - 200,
                    newNode.Bounds.Top,
                    200, newNode.Bounds.Height);
            }
            else
            {
                m_tbxEditNodeText.Bounds = new Rectangle(newNode.Bounds.Location,
                    new Size(200, newNode.Bounds.Height));
            }
             */
            ShowTextBoxForNode(newNode);
            m_tbxEditNodeText.Text = "";
            m_tbxEditNodeText.Tag = newNode;
        }

        private void EditNode(TreeNode node)
        {
            m_bTreeViewInEditNodeMode = true;
            ShowTextBoxForNode(node);

            /*
            if (RightToLeftLayout)
            {
                m_tbxEditNodeText.Bounds = new Rectangle(
                    node.Bounds.Right - Math.Max(node.Bounds.Width * 11 / 10, 200),
                    node.Bounds.Top,
                    Math.Max(node.Bounds.Width * 11 / 10, 200), 
                    node.Bounds.Height);
            }
            else
            {
                m_tbxEditNodeText.Bounds = new Rectangle(node.Bounds.Location,
                    new Size(Math.Max(node.Bounds.Width * 11 / 10, 200), node.Bounds.Height));
            }*/
            m_tbxEditNodeText.Text = node.Text;
            m_tbxEditNodeText.Tag = node;
        }

        private void ShowTextBoxForNode(TreeNode node)
        {
            if (node == null) return;

            // Get the bounds of the node
            Rectangle nodeBounds = node.Bounds;

            // Determine the width of the textbox
            int textBoxWidth = Math.Max(nodeBounds.Width, 200); // Enforce minimum width of 200

            // Adjust the position based on Right-to-Left layout
            int xPosition;
            if (this.RightToLeft == RightToLeft.Yes)
            {
                // Right-to-Left: Align the textbox to the right side of the node
                xPosition = m_ctlTreeView.Width - nodeBounds.Left - textBoxWidth;
            }
            else
            {
                // Left-to-Right: Use the left side of the node
                xPosition = nodeBounds.X;
            }

            // Position and size the textbox
            m_tbxEditNodeText.Location = new Point(xPosition, nodeBounds.Y);
            m_tbxEditNodeText.Size = new Size(textBoxWidth, nodeBounds.Height);

            // Show and focus the textbox
            m_tbxEditNodeText.Visible = true;
            m_tbxEditNodeText.BringToFront();
            m_tbxEditNodeText.Focus();
        }

        private void DetachNode(TreeNode node)
        {
            if (node.Parent != null)
            {
                node.Parent.Nodes.Remove(node);
                m_ctlTreeView.Nodes.Add(node);
            }
        }
        private void m_ctlTreeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && m_ctlTreeView.SelectedNode != null)
            {
                EditNode(m_ctlTreeView.SelectedNode);
            }
        }

        private void m_tbxEditNodeText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TreeNode currentNode = (TreeNode)m_tbxEditNodeText.Tag;
                MindMapNode currentNodeData = (MindMapNode)currentNode.Tag;
                currentNodeData.Text = m_tbxEditNodeText.Text;
                currentNode.Text = m_tbxEditNodeText.Text;
                m_tbxEditNodeText.Visible = false;

                if (currentNode.Parent != null)
                {
                    ((MindMapNode)currentNode.Parent.Tag).AddElement(m_tbxEditNodeText.Text);

                    /*
                    MindMapNode newNodeData = new Node("");

                    TreeNode newNode = new TreeNode() { Tag = newNodeData };
                    currentNode.Parent.Nodes.Add(newNode);
                    StartEditingNode(newNode);
                     */
                }
                //PopulateTreeView();
            }
        }

        private void PopulateTreeView()
        {
            m_ctlTreeView.Nodes.Clear();
            if (m_oMindMap!=null)
            {
                m_ctlTreeView.Enabled = true;

                TreeNode oTreeNode = new TreeNode(m_strRootNodeName);
                oTreeNode.Tag = new MindMapNode(this, null, m_strRootNodeName);
                m_ctlTreeView.Nodes.Add(oTreeNode);
                if (m_oMindMap.ContainsKey(m_strRootNodeName) && m_oMindMap[m_strRootNodeName].Keys.Count > 0)
                {
                    oTreeNode.Nodes.Add(new TreeNode("Loading...")); // Add placeholder node for child nodes
                }
            }
            else
            {
                m_ctlTreeView.Enabled = false;
            }
        }

        private void AddSubNodes(TreeNode treeNode, MindMapNode node)
        {
            foreach (var subNode in node.Elements)
            {
                TreeNode subTreeNode = new TreeNode(subNode.Text);
                subTreeNode.Tag = subNode;
                treeNode.Nodes.Add(subTreeNode);
            }
        }

        private void OnToggleGUIClick(object sender, EventArgs e)
        {
            if (m_ctlTreeView.Visible)
            {
                m_ctlTreeView.Hide();
                if (!string.IsNullOrEmpty(m_strFilePath))
                {
                    m_ctlMindmapNodeView.Node = new MindMapNode(this, null, m_strRootNodeName);
                    m_ctlMindmapNodeView.Show();
                }
            }
            else
            {
                m_ctlTreeView.Show();
                m_ctlTreeView.Enabled = true;
                PopulateTreeView();

                m_ctlMindmapNodeView.Hide();
            }

        }

        private void OnNodeEditTextBoxFocusLeft(object oSender, EventArgs e)
        {
            m_tbxEditNodeText.Visible = false;
        }

    }
}
