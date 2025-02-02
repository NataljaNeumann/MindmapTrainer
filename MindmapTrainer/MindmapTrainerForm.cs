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
            /// Constructs a new node
            /// </summary>
            /// <param name="tr">The training form</param>
            /// <param name="text">The text of the node</param>
            //===============================================================================================
            public MindMapNode(MindmapTrainerForm oTrainerForm, string strText)
            {
                m_strText = strText;
                m_oTrainerForm = oTrainerForm;
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
                            yield return new MindMapNode(m_oTrainerForm, text);
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

            // init random with current time
            m_oRandomGenerator = new Random(((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 +
                DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond);
            m_oRandomGenerator2 = new Random((((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + 
                DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);

            m_btnHiddenAcceptButton.Location = 
                new Point(-m_btnHiddenAcceptButton.Size.Width,
                          -m_btnHiddenAcceptButton.Size.Height);

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
                m_ctlMindmapNodeView.Node = new MindMapNode(this, m_strRootNodeName);
                m_ctlMindmapNodeView.Show();

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


                m_ctlMindmapNodeView.Node = new MindMapNode(this, m_strRootNodeName);
                m_ctlMindmapNodeView.Show();

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

                System.Windows.Forms.MessageBox.Show(this, ex.Message, "Fehler beim Speichern der Datei", 
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

    }
}
