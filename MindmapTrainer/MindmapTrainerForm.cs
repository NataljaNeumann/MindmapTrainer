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
    public partial class MindmapTrainerForm : Form
    {
        SortedDictionary<string, Dictionary<String, bool>> _mindMap;
        SortedDictionary<string, string> _training;
        SortedDictionary<string, int> _correctAnswers;
        LinkedList<string> _trainedSubjects = new LinkedList<string>();
        int _totalErrors;
        bool _skipLast;
        Random _rnd;
        Random _rnd2;


        string _root;
        string _fileName;

        class MindMapNode : IMindmapNode
        {
            string _text;
            MindmapTrainerForm _tr;

            public MindMapNode(MindmapTrainerForm tr, string text)
            {
                _text = text;
                _tr = tr;
            }

            #region IMindmapNode Member

            public string Text
            {
                get
                {
                    return _text;
                }
                set
                {
                    _text = value;
                }
            }

            public IEnumerable<IMindmapNode> Elements
            {
                get {
                    if (_tr != null && _text != null && _tr._mindMap.ContainsKey(_text))
                    {
                        foreach (string text in _tr._mindMap[_text].Keys)
                        {
                            yield return new MindMapNode(_tr, text);
                        }
                    }
                    yield break;
                }
            }

            public void AddElement(string text)
            {
                if (_tr != null && _text != null)
                {
                    if (!_tr._mindMap.ContainsKey(_text))
                        _tr._mindMap[_text] = new Dictionary<string,bool>();

                    if (!_tr._training.ContainsKey(_text))
                    {
                        _tr._training[_text] = "111011";
                        _tr._totalErrors += 1;
                    }

                    if (!_tr._correctAnswers.ContainsKey(_text))
                        _tr._correctAnswers[_text] = 0;

                    if (!_tr._mindMap[_text].ContainsKey(text))
                        _tr._mindMap[_text][text] = false;

                    _tr.Save();
                }
            }

            public bool HasElements
            {
                get { 
                    return (_tr != null && _text != null && _tr._mindMap.ContainsKey(_text) && _tr._mindMap[_text].Count>0);
                }
            }

            #endregion
        }

        public MindmapTrainerForm()
        {
            InitializeComponent();

            // init random with current time
            _rnd = new Random(((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond);
            _rnd2 = new Random((((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);

            hiddenAcceptButton.Location = new Point(-hiddenAcceptButton.Size.Width, -hiddenAcceptButton.Size.Height);

            EnableDisableMenu();
        }

        
        private void hiddenAcceptButton_Click(object sender, EventArgs e)
        {

        }

        private void neueMindmapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _mindMap = new SortedDictionary<string,Dictionary<string,bool>>();
                _training = new SortedDictionary<string, string>();
                _correctAnswers = new SortedDictionary<string, int>();

                _root = saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.LastIndexOf('\\')+1).Replace(".MindMap.xml","");
                _fileName = saveFileDialog1.FileName;
                _mindMap[_root] = new Dictionary<string, bool>();
                _training[_root] = "111011";
                _totalErrors = 1;
                _correctAnswers[_root] = 0;
                mindmapNodeView1.Node = new MindMapNode(this, _root);
                mindmapNodeView1.Show();

                EnableDisableMenu();
            }
        }

        private void öffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _mindMap = new SortedDictionary<string, Dictionary<string, bool>>();
                _training = new SortedDictionary<string, string>();
                _correctAnswers = new SortedDictionary<string, int>();

                _fileName = openFileDialog1.FileName;
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(openFileDialog1.FileName);
                foreach (System.Xml.XmlElement e5 in doc.SelectNodes("/mindmap/start"))
                {
                    _root = e5.InnerText;
                };

                _mindMap[_root]=new Dictionary<string,bool>();

                foreach (System.Xml.XmlElement e4 in doc.SelectNodes("/mindmap/subject"))
                {
                    foreach (System.Xml.XmlNode e2 in e4.SelectNodes("@name"))
                    {
                        Dictionary<string,bool> elements = new Dictionary<string, bool>();
                        foreach (System.Xml.XmlNode e3 in e4.SelectNodes("element"))
                        {
                            elements[e3.InnerText] = false;
                        }
                        _mindMap[e2.InnerText] = elements;

                        _training[e2.InnerText] = "111011";
                        foreach (System.Xml.XmlNode e5 in e4.SelectNodes("@training"))
                        {
                            string s = e5.InnerText;
                            while (s.Length < 6)
                                s = "1" + s;
                            if (s.Length > 6)
                                s = s.Substring(0, 6);
                            _training[e2.InnerText] = s;
                        }

                        // count errors
                        string s2 = _training[e2.InnerText];
                        _totalErrors += s2.Length - s2.Replace("0", "").Length;

                        // load correct answers for the subject
                        int res = 0;
                        foreach (System.Xml.XmlNode e6 in e4.SelectNodes("@correct"))
                        {
                            if (!int.TryParse(e6.InnerText, out res))
                            {
                                res = 0;
                            }
                        }
                        _correctAnswers[e2.InnerText] = res;
                    }
                }


                mindmapNodeView1.Node = new MindMapNode(this, _root);
                mindmapNodeView1.Show();

                EnableDisableMenu();

            }
        }

        private void Save()
        {
            System.IO.FileInfo fi2 = new System.IO.FileInfo(_fileName);
            if (fi2.Exists)
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(_fileName + ".bak");
                if (fi.Exists)
                {
                    fi.Delete();
                }

                fi2.MoveTo(fi2.FullName+".bak");
            }

            try
            {
                using (System.IO.StreamWriter w = new System.IO.StreamWriter(_fileName, false, Encoding.UTF8))
                {
                    w.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                    w.WriteLine("<mindmap>");
                    w.WriteLine("  <start>{0}</start>", PrepareForXml(_root));
                    foreach (KeyValuePair<string, Dictionary<string, bool>> pair in _mindMap)
                    {
                        w.Write("  <subject name=\"{0}\" training=\"{1}\" correct=\"{2}\">", PrepareForXml(pair.Key), _training[pair.Key], _correctAnswers[pair.Key]);
                        foreach (string element in pair.Value.Keys)
                        {
                            w.WriteLine();
                            w.Write("    <element>{0}</element>", PrepareForXml(element));
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
                    System.IO.FileInfo fi3 = new System.IO.FileInfo(_fileName);
                    if (fi3.Exists)
                        fi3.Delete();
                }
                catch
                {
                }

                try
                {
                    System.IO.FileInfo fi4 = new System.IO.FileInfo(_fileName + ".bak");
                    if (fi4.Exists)
                        fi4.MoveTo(_fileName);
                }
                catch
                {
                }

                System.Windows.Forms.MessageBox.Show(this, ex.Message, "Fehler beim Speichern der Datei", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        string PrepareForXml(string text)
        {
            return text.Replace("&", "&amp;").Replace("\"","&quot;").Replace("<", "&lt;").Replace(">", "&gt;");
        }


        private void MindmapTrainerForm_MouseMove(object sender, MouseEventArgs e)
        {
            // make randoms less deterministic, whenever possible
            if (_rnd != null)
                _rnd = new Random(_rnd.Next() + ((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond + (e.X & 3) * 256);
            if (_rnd2 != null)
                _rnd2 = new Random(_rnd2.Next() + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear + (e.Y & 3) * 256);
        }

        private void trainierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                mindmapNodeView1.Hide();

                bool bRepeat = true;
                while (bRepeat)
                {
                    bRepeat = false;
                    // there we train one of the words randomly. Words with errors get higher weight
                    int rnd2 = _rnd2.Next();
                    _rnd2 = new Random(rnd2 + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);

                    int selectedError = rnd2 % (_totalErrors + _mindMap.Count);

                    _skipLast = _mindMap.Count > 5;

                    int wordIndex = -1;
                    using (SortedDictionary<string, string>.ValueCollection.Enumerator values = _training.Values.GetEnumerator())
                    {
                        while (selectedError >= 0 && values.MoveNext())
                        {
                            wordIndex += 1;
                            if (values.Current.Contains("0"))
                            {
                                selectedError -= values.Current.Length - values.Current.Replace("0", "").Length + 1;
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
                mindmapNodeView1.Show();
            }
        }

        private void intensivToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                mindmapNodeView1.Hide();

                bool bRepeat = true;
                while (bRepeat)
                {
                    bRepeat = false;
                    // decide, if we will train one subject randomly, or one that needs additional training
                    int rnd = _rnd.Next();
                    _rnd = new Random(rnd + ((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond);

                    if ((_totalErrors > 0) && (rnd % 100 < 50))
                    {
                        // there we train one of the subjects that need additional training
                        int rnd2 = _rnd2.Next();
                        _rnd2 = new Random(rnd + rnd2 + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);

                        int selectedError = rnd2 % _totalErrors;

                        _skipLast = _mindMap.Count > 10;

                        int wordIndex = -1;
                        using (SortedDictionary<string, string>.ValueCollection.Enumerator values = _training.Values.GetEnumerator())
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
                        int rnd2 = _rnd2.Next();
                        _rnd2 = new Random(rnd2 + (((DateTime.UtcNow.Hour * 60 + DateTime.UtcNow.Minute) * 60 + DateTime.UtcNow.Second) * 1000 + DateTime.UtcNow.Millisecond) * 365 + DateTime.UtcNow.DayOfYear);


                        // calculate mean of correct answers
                        long lTotal = 0;
                        foreach (int i in _correctAnswers.Values)
                            lTotal += i;

                        int mean = (int)(lTotal / _correctAnswers.Count);

                        // now calculate the sum of weights of all words
                        int iTotalWeights = 0;
                        foreach (int i in _correctAnswers.Values)
                        {
                            int weight = mean + 3 - i;
                            if (weight <= 0)
                                weight = 1;
                            iTotalWeights += weight;
                        };

                        int selectedWeight = rnd2 % iTotalWeights;

                        int wordIndex = -1;
                        using (SortedDictionary<string, int>.ValueCollection.Enumerator values = _correctAnswers.Values.GetEnumerator())
                        {
                            while (selectedWeight >= 0 && values.MoveNext())
                            {
                                wordIndex += 1;

                                int weight = mean + 3 - values.Current;

                                if (weight <= 0)
                                    weight = 1;

                                selectedWeight -= weight;
                            }

                            _skipLast = _training.Count > 5;

                            bRepeat = TrainSubject(wordIndex);
                        }
                    }
                }
                Save();
            }
            finally
            {
                mindmapNodeView1.Show();
            }

        }

        private bool TrainSubject(int index)
        {

            bool bContinue = false;
            bool bCorrect = false;
            foreach (KeyValuePair<string, string> pair in _training)
            {
                if (0 == index--)
                {
                    if (_skipLast)
                    {
                        foreach (string s in _trainedSubjects)
                        {
                            // if we trained this subject recently, then try to skip it
                            if (s.Equals(pair.Key))
                                if (_rnd2.Next(100) > 0)
                                    return true;
                        };
                    }

                    // add the subject to the list of recently trained rotate the list
                    _trainedSubjects.AddFirst(pair.Key); 
                    if (_trainedSubjects.Count > 5)
                        _trainedSubjects.RemoveLast();


                    string subject = pair.Key;
                    using (SubjectTestForm test = new SubjectTestForm() )
                    {
                        test.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MindmapTrainerForm_MouseMove);

                        test.label1.Text = subject + ":";
                        StringBuilder b = new StringBuilder();
                        foreach (string s in _mindMap[pair.Key].Keys)
                            if (b.Length>0)
                                b.AppendFormat("\r\n> {0}",s);
                            else
                                b.AppendFormat("> {0}",s);

                        test.label2.Text = b.ToString();

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
                            string prevResults = _training[subject];
                            string newResults = (bCorrect ? "1" : "0") + prevResults.Substring(0, prevResults.Length<5?prevResults.Length:5);
                            _training[subject] = newResults;
                            _totalErrors += newResults.Length - newResults.Replace("0", "").Length - (prevResults.Length - prevResults.Replace("0", "").Length);

                            // if the result was correct and we didn't repeat it because of earlier mistakes, then increment the number of correct answers
                            if (bCorrect && prevResults.IndexOf('0')<0)
                                _correctAnswers[subject]++;

                            EnableDisableMenu();                        
                        }
                    }

                    return bContinue;
                }
            }
            return bContinue;
        }


        private void EnableDisableMenu()
        {
            trainierenToolStripMenuItem.Enabled = intensivToolStripMenuItem.Enabled = _mindMap != null && _mindMap.Count > 0;
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            using (About form = new About())
            {
                form.ShowDialog(this);
            }
        }

        private void licenseMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.gnu.org/licenses/gpl-2.0.html");
        }

        private void licenseInUserLanguageMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.gnu.de/documents/gpl-2.0.de.html");
        }

    }
}
