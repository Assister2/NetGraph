using CefSharp.DevTools.CSS;
using CyConex.Graph;
using EnvDTE;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CyConex.Forms
{
    public partial class ReportUtility
    {
        private WordDocument document = null;
        private string sourceFileName = "";
        public string fullFileName = "";
        private IWParagraph paragraph;

       

        public string ReturnFileName()
        {
            // return file name 
            return System.IO.Path.GetFileName(fullFileName);
        }

        public string GetFileDirectory()
        {
            // return file name 
            return System.IO.Path.GetDirectoryName(fullFileName);
        }

        public void LoadDocumentIntoMemory()
        {
            try
            {
                if (fullFileName != null)
                {
                    document = new WordDocument(fullFileName, Syncfusion.DocIO.FormatType.Docx);
                }
            }
            catch (IOException ex)
            {
                // Handle the error
                MessageBox.Show("Unable to open file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Handle other errors
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void SaveDocumentToFile(string filename)
        {
            if (document == null)
                return;

            try
            {
                if (filename == null || filename == "")
                    document.Save(fullFileName, Syncfusion.DocIO.FormatType.Docx);
                else
                    document.Save(filename, Syncfusion.DocIO.FormatType.Docx);
            }
            catch (IOException ex)
            {
                // Handle the error
                MessageBox.Show("Unable to save file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Handle other errors
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void OpenFile(string filename)
        {
            if (filename == null)
                return;
            if (!File.Exists(filename))
                return;

            try
            {
                string filePath;

                if (string.IsNullOrEmpty(filename))
                {
                    filePath = fullFileName;
                }
                else
                {
                    filePath = filename;
                }

                document.Save(filePath, Syncfusion.DocIO.FormatType.Docx);

                // Open the document
                System.Diagnostics.Process.Start(filePath);
            }
            catch (IOException ex)
            {
                // Handle the error
                MessageBox.Show("An I/O error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Handle other errors
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



        public void GetAllNodesList(string searchText, string nodeType, string style, string format)
        {
            if (document == null)
                return;

            Syncfusion.DocIO.DLS.TextSelection selection = document.Find(searchText, false, false);
            if (selection != null)
            {
                List<string> nodeIDs = new List<string>();
                switch (nodeType)
                {
                    case "actor":
                        nodeIDs = GraphUtil.GetActorNodes();
                        break;
                    case "asset":
                        nodeIDs = GraphUtil.GetAssetNodes();
                        break;
                    case "asset-group":
                        nodeIDs = GraphUtil.GetAssetGroupNodes();
                        break;
                    case "attack":
                        nodeIDs = GraphUtil.GetAttackNodes();
                        break;
                    case "vulnerability":
                        nodeIDs = GraphUtil.GetVulnerabilityNodes();
                        break;
                    case "control":
                        nodeIDs = GraphUtil.GetControlNodes();
                        break;
                    case "objective":
                        nodeIDs = GraphUtil.GetObjectiveNodes();
                        break;
                }


                foreach (IWSection section in document.Sections)
                {
                    for (int i = 0; i < section.Paragraphs.Count; i++)
                    {
                        if (section.Paragraphs[i].Text.Contains(searchText))
                        {
                            section.Paragraphs.RemoveAt(i);
                            int tempCount = 0;
                            foreach (string nodeID in nodeIDs)
                            {
                                string tempString = string.Empty;
                                switch (format)
                                {
                                    case "T": //Title
                                        tempString = Utility.RemoveHTML(GraphUtil.GetNodeTitle(nodeID));
                                        break;

                                    case "TD": //Title and Description
                                        tempString = Utility.RemoveHTML(GraphUtil.GetNodeTitle(nodeID)) + ", " + Utility.DecodeBase64TextandRemoveRTF(GraphUtil.GetNodeDescription(nodeID));
                                        break;
                                    case "TRD": //Title, Refrence and Descrition
                                        tempString = Utility.RemoveHTML(GraphUtil.GetNodeTitle(nodeID)) + ", " + GraphUtil.GetNodeFrameworkReference(nodeID) + ", " + Utility.DecodeBase64TextandRemoveRTF(GraphUtil.GetNodeDescription(nodeID));
                                        break;
                                }

                                switch (style)
                                {
                                    case "B": //BulletList
                                        {
                                            IWParagraph tempParagraph = section.AddParagraph();
                                            tempParagraph.ListFormat.ApplyDefBulletStyle();
                                            tempParagraph.AppendText(tempString);
                                            tempParagraph.ListFormat.ContinueListNumbering();
                                            section.Paragraphs.Insert(i + tempCount, tempParagraph);
                                            tempCount++;
                                        }
                                        break;
                                    case "N": //Nubered List
                                        {
                                            IWParagraph tempParagraph = (IWParagraph)section.Paragraphs[i].Clone();
                                            tempParagraph.ChildEntities.Clear();
                                            tempParagraph.AppendText(tempString);
                                            if (tempCount == 0)
                                            {
                                                tempParagraph.ListFormat.ApplyDefNumberedStyle();
                                                tempParagraph.ListFormat.RestartNumbering = true;
                                            }
                                            else
                                                tempParagraph.ListFormat.ContinueListNumbering();
                                            section.Paragraphs.Insert(i + tempCount, tempParagraph);
                                            tempCount++;
                                        }
                                        break;

                                }


                            }


                        }

                    }
                }
            }
        }

        public void ReportItem(string searchText)
        {
            switch (searchText)
            {
                case "<<AssetNodeTitlesBulletList>>":
                    GetAllNodesList(searchText, "asset", "B", "T");
                    break;
                case "<<AssetGroupNodeTitlesBulletList>>":
                    GetAllNodesList(searchText, "asset-group", "B", "T");
                    break;
                case "<<ActorNodeTitlesBulletList>>":
                    GetAllNodesList(searchText, "actor", "B", "T");
                    break;
                case "<<AttackNodeTitlesBulletList>>":
                    GetAllNodesList(searchText, "attack", "B", "T");
                    break;
                case "<<VulnerabilityNodeTitlesBulletList>>":
                    GetAllNodesList(searchText, "vulnerability", "B", "T");
                    break;
                case "<<ControlNodeTitlesBulletList>>":
                    GetAllNodesList(searchText, "control", "B", "T");
                    break;
                case "<<ObjectiveNodeTitlesBulletList>>":
                    GetAllNodesList(searchText, "objective", "B", "T");
                    break;
                case "<<AssetNodeTitlesDescriptionBulletList>>":
                    GetAllNodesList(searchText, "asset", "B", "TD");
                    break;
                case "<<AssetGroupNodeTitlesDescriptionBulletList>>":
                    GetAllNodesList(searchText, "asset-group", "B", "TD");
                    break;
                case "<<ActorNodeTitlesDescriptionBulletList>>":
                    GetAllNodesList(searchText, "actor", "B", "TD");
                    break;
                case "<<AttackNodeTitlesDescriptionBulletList>>":
                    GetAllNodesList(searchText, "attack", "B", "TD");
                    break;
                case "<<VulnerabilityNodeTitlesDescriptionBulletList>>":
                    GetAllNodesList(searchText, "vulnerability", "B", "TD");
                    break;
                case "<<ControlNodeTitlesDescriptionBulletList>>":
                    GetAllNodesList(searchText, "control", "B", "TD");
                    break;
                case "<<ObjectiveNodeTitlesDescriptionBulletList>>":
                    GetAllNodesList(searchText, "objective", "B", "TD");
                    break;
                case "<<AssetNodeTitlesRefrenceDescriptionBulletList>>":
                    GetAllNodesList(searchText, "asset", "B", "TRD");
                    break;
                case "<<AssetGroupNodeTitlesRefrenceDescriptionBulletList>>":
                    GetAllNodesList(searchText, "asset-group", "B", "TRD");
                    break;
                case "<<ActorNodeTitlesRefrenceDescriptionBulletList>>":
                    GetAllNodesList(searchText, "actor", "B", "TRD");
                    break;
                case "<<AttackNodeTitlesRefrenceDescriptionBulletList>>":
                    GetAllNodesList(searchText, "attack", "B", "TRD");
                    break;
                case "<<VulnerabilityNodeTitlesRefrenceDescriptionBulletList>>":
                    GetAllNodesList(searchText, "vulnerability", "B", "TRD");
                    break;
                case "<<ControlNodeTitlesRefrenceDescriptionBulletList>>":
                    GetAllNodesList(searchText, "control", "B", "TRD");
                    break;
                case "<<ObjectiveNodeTitlesRefrenceDescriptionBulletList>>":
                    GetAllNodesList(searchText, "objective", "B", "TRD");
                    break;
                case "<<AssetNodeTitlesNumberedList>>":
                    GetAllNodesList(searchText, "asset", "N", "T");
                    break;
                case "<<AssetGroupNodeTitlesNumberedList>>":
                    GetAllNodesList(searchText, "asset-group", "N", "T");
                    break;
                case "<<ActorNodeTitlesNumberedList>>":
                    GetAllNodesList(searchText, "actor", "N", "T");
                    break;
                case "<<AttackNodeTitlesNumberedList>>":
                    GetAllNodesList(searchText, "attack", "N", "T");
                    break;
                case "<<VulnerabilityNodeTitlesNumberedList>>":
                    GetAllNodesList(searchText, "vulnerability", "N", "T");
                    break;
                case "<<ControlNodeTitlesNumberedList>>":
                    GetAllNodesList(searchText, "control", "N", "T");
                    break;
                case "<<ObjectiveNodeTitlesNumberedList>>":
                    GetAllNodesList(searchText, "objective", "N", "T");
                    break;
                case "<<AssetNodeTitlesDescriptionNumberedList>>":
                    GetAllNodesList(searchText, "asset", "N", "TD");
                    break;
                case "<<AssetGroupNodeTitlesDescriptionNumberedList>>":
                    GetAllNodesList(searchText, "asset-group", "N", "TD");
                    break;
                case "<<ActorNodeTitlesDescriptionNumberedList>>":
                    GetAllNodesList(searchText, "actor", "N", "TD");
                    break;
                case "<<AttackNodeTitlesDescriptionNumberedList>>":
                    GetAllNodesList(searchText, "attack", "N", "TD");
                    break;
                case "<<VulnerabilityNodeTitlesDescriptionNumberedList>>":
                    GetAllNodesList(searchText, "vulnerability", "N", "TD");
                    break;
                case "<<ControlNodeTitlesDescriptionNumberedList>>":
                    GetAllNodesList(searchText, "control", "N", "T");
                    break;
                case "<<ObjectiveNodeTitlesDescriptionNumberedList>>":
                    GetAllNodesList(searchText, "objective", "N", "TD");
                    break;
                case "<<AssetNodeTitlesRefrenceDescriptionNumberedList>>":
                    GetAllNodesList(searchText, "asset", "N", "TD");
                    break;
                case "<<AssetGroupNodeTitlesRefrenceDescriptionNumberedList>>":
                    GetAllNodesList(searchText, "asset-group", "N", "TD");
                    break;
                case "<<ActorNodeTitlesRefrenceDescriptionNumberedList>>":
                    GetAllNodesList(searchText, "actor", "N", "TD");
                    break;
                case "<<AttackNodeTitlesRefrenceDescriptionNumberedList>>":
                    GetAllNodesList(searchText, "attack", "N", "TD");
                    break;
                case "<<VulnerabilityNodeTitlesRefrenceDescriptionNumberedList>>":
                    GetAllNodesList(searchText, "vulnerability", "N", "TD");
                    break;
                case "<<ControlNodeTitlesRefrenceDescriptionNumberedList>>":
                    GetAllNodesList(searchText, "control", "N", "T");
                    break;
                case "<<ObjectiveNodeTitlesRefrenceDescriptionNumberedList>>":
                    GetAllNodesList(searchText, "objective", "N", "TD");
                    break;
                case "<<AssetNodeTitlesRefrenceDescriptionTable>>":
                    GetAllNodesTable(searchText, "asset");
                    break;
                case "<<AssetGroupNodeTitlesRefrenceDescriptionTable>>":
                    GetAllNodesTable(searchText, "asset-group");
                    break;
                case "<<ActorNodeTitlesRefrenceDescriptionTable>>":
                    GetAllNodesTable(searchText, "actor");
                    break;
                case "<<AttackNodeTitlesRefrenceDescriptionTable>>":
                    GetAllNodesTable(searchText, "attack");
                    break;
                case "<<VulnerabilityNodeTitlesRefrenceDescriptionTable>>":
                    GetAllNodesTable(searchText, "vulnerability");
                    break;
                case "<<ControlNodeTitlesRefrenceDescriptionTable>>":
                    GetAllNodesTable(searchText, "control");
                    break;
                case "<<ObjectiveNodeTitlesRefrenceDescriptionTable>>":
                    GetAllNodesTable(searchText, "objective");
                    break;
                case "<<ControlNodeScoreTDFRSITableLow>>":
                    GetAllConOrObjNodesTable(searchText, "control", 0, "Lowest");
                    break;
                case "<<ControlNodeScoreTDFRSITableLow3>>":
                    GetAllConOrObjNodesTable(searchText, "control", 3, "Lowest");
                    break;
                case "<<ControlNodeScoreTDFRSITableLow5>>":
                    GetAllConOrObjNodesTable(searchText, "control", 5, "Lowest");
                    break;
                case "<<ControlNodeScoreTDFRSITableLow10>>":
                    GetAllConOrObjNodesTable(searchText, "control", 10, "Lowest");
                    break;
                case "<<ControlNodeScoreTDFRSITableHigh>>":
                    GetAllConOrObjNodesTable(searchText, "control", 0, "Highest");
                    break;
                case "<<ControlNodeScoreTDFRSITableHigh3>>":
                    GetAllConOrObjNodesTable(searchText, "control", 3, "Highest");
                    break;
                case "<<ControlNodeScoreTDFRSITableHigh5>>":
                    GetAllConOrObjNodesTable(searchText, "control", 5, "Highest");
                    break;
                case "<<ControlNodeScoreTDFRSITableHigh10>>":
                    GetAllConOrObjNodesTable(searchText, "control", 10, "Highest");
                    break;
                case "<<ObjectiveNodeScoreTDFRSITableLow>>":
                    GetAllConOrObjNodesTable(searchText, "objective", 0, "Lowest");
                    break;
                case "<<ObjectiveNodeScoreTDFRSITableLow3>>":
                    GetAllConOrObjNodesTable(searchText, "objective", 3, "Lowest");
                    break;
                case "<<ObjectiveNodeScoreTDFRSITableLow5>>":
                    GetAllConOrObjNodesTable(searchText, "objective", 5, "Lowest");
                    break;
                case "<<ObjectiveNodeScoreTDFRSITableLow10>>":
                    GetAllConOrObjNodesTable(searchText, "objective", 10, "Lowest");
                    break;
                case "<<ObjectiveNodeScoreTDFRSITableHigh>>":
                    GetAllConOrObjNodesTable(searchText, "objective", 0, "Highest");
                    break;
                case "<<ObjectiveNodeScoreTDFRSITableHigh3>>":
                    GetAllConOrObjNodesTable(searchText, "objective", 3, "Highest");
                    break;
                case "<<ObjectiveNodeScoreTDFRSITableHigh5>>":
                    GetAllConOrObjNodesTable(searchText, "objective", 5, "Highest");
                    break;
                case "<<ObjectiveNodeScoreTDFRSITableHigh10>>":
                    GetAllConOrObjNodesTable(searchText, "objective", 10, "Highest");
                    break;
            }
        }


        public void SimpleTextReplace(string searchText, string replaceText)
        {
            if (document == null)
                return;

            Syncfusion.DocIO.DLS.TextSelection selection = document.Find(searchText, false, false);
            if (selection != null)
            {
                document.Replace(searchText, replaceText, true, true);
            }
        }

        public void NodeCount(string searchText)
        {

            switch (searchText)
            {
                case "asset":
                    SimpleTextReplace("<<AssetGroupNodeCount>>", GraphUtil.GetAssetNodeCount().ToString());
                    break;
                case "asset-group":
                    SimpleTextReplace("<<AssetGroupGroupNodeCount>>", GraphUtil.GetAssetGroupNodeCount().ToString());
                    break;
                case "actor":
                    SimpleTextReplace("<<ActorGroupNodeCount>>", GraphUtil.GetActorNodeCount().ToString());
                    break;
                case "attack":
                    SimpleTextReplace("<<AttackGroupNodeCount>>", GraphUtil.GetAttackNodeCount().ToString());
                    break;
                case "vulnerability":
                    SimpleTextReplace("<<VulnerabilityGroupNodeCount>>", GraphUtil.GetVulnerabilityNodeCount().ToString());
                    break;
                case "control":
                    SimpleTextReplace("<<ControlGroupNodeCount>>", GraphUtil.GetControlNodeCount().ToString());
                    break;
                case "objective":
                    SimpleTextReplace("<<ObjectiveGroupNodeCount>>", GraphUtil.GetObjectiveNodeCount().ToString());
                    break;
            }
        }

        public void GetAllNodesTable(string searchText, string nodeType)
        {
            if (document == null)
                return;

            Syncfusion.DocIO.DLS.TextSelection selection = document.Find(searchText, false, false);
            if (selection != null)
            {
                List<string> nodeIDs = new List<string>();
                switch (nodeType)
                {
                    case "actor":
                        nodeIDs = GraphUtil.GetActorNodes();
                        break;
                    case "asset":
                        nodeIDs = GraphUtil.GetAssetNodes();
                        break;
                    case "asset-group":
                        nodeIDs = GraphUtil.GetAssetGroupNodes();
                        break;
                    case "attack":
                        nodeIDs = GraphUtil.GetAttackNodes();
                        break;
                    case "vulnerability":
                        nodeIDs = GraphUtil.GetVulnerabilityNodes();
                        break;
                    case "control":
                        nodeIDs = GraphUtil.GetControlNodes();
                        break;
                    case "objective":
                        nodeIDs = GraphUtil.GetObjectiveNodes();
                        break;

                }

                for (int j = 0; j < document.Sections.Count; j++)
                {
                    for (int i = 0; i < document.Sections[j].Paragraphs.Count; i++)
                    {
                        if (document.Sections[j].Paragraphs[i].Text.Contains(searchText))
                        {
                            document.Replace(searchText, string.Empty, true, true);
                            WSection newSection = new WSection(document);
                            newSection.BreakCode = SectionBreakCode.NoBreak;

                            IWTable table = newSection.AddTable();
                            WTableRow row = table.AddRow();
                            WTableCell cell = row.AddCell();
                            cell.Width = (float)(newSection.PageSetup.ClientWidth * 0.3);
                            cell.AddParagraph().AppendText("Title");
                            cell = row.AddCell();
                            cell.Width = (float)(newSection.PageSetup.ClientWidth * 0.2);
                            cell.AddParagraph().AppendText("Refrence");
                            cell = row.AddCell();
                            cell.Width = (float)(newSection.PageSetup.ClientWidth * 0.48);
                            cell.AddParagraph().AppendText("Description");
                            foreach (string nodeID in nodeIDs)
                            {
                                row = table.AddRow(true, false);
                                cell = row.AddCell();
                                cell.Width = (float)(newSection.PageSetup.ClientWidth * 0.3);
                                cell.AddParagraph().AppendText(Utility.RemoveHTML(GraphUtil.GetNodeTitle(nodeID)));
                                cell = row.AddCell();
                                cell.Width = (float)(newSection.PageSetup.ClientWidth * 0.2);
                                cell.AddParagraph().AppendText(GraphUtil.GetNodeFrameworkReference(nodeID));
                                cell = row.AddCell();
                                cell.Width = (float)(newSection.PageSetup.ClientWidth * 0.48);
                                cell.AddParagraph().AppendText(Utility.DecodeBase64TextandRemoveRTF(GraphUtil.GetNodeDescription(nodeID)));

                            }
                            document.Sections.Insert(j + 1, newSection);

                            j++;
                        }
                    }
                }
            }
        }

        public void GetAllConOrObjNodesTable(string searchText, string nodeType, int recordCount, string direction)
        {
            if (document == null)
                return;

            Syncfusion.DocIO.DLS.TextSelection selection = document.Find(searchText, false, false);
            if (selection != null)
            {
                List<string> nodeIDs = new List<string>();
                switch (nodeType)
                {
                    case "control":
                        nodeIDs = GraphUtil.GetControlNodes();
                        break;
                    case "objective":
                        nodeIDs = GraphUtil.GetObjectiveNodes();
                        break;
                }

                Dictionary<string, double> nodeScores = new Dictionary<string, double>();
                foreach (var item in nodeIDs)
                {
                    if (!nodeScores.ContainsKey(item))
                        nodeScores.Add(item, GraphUtil.GetControlNodeAssessedScore(item));
                }

                var orderedNodeScores = direction == "LowestFirst"
                    ? nodeScores.OrderBy(pair => pair.Value)
                    : nodeScores.OrderByDescending(pair => pair.Value);

                for (int j = 0; j < document.Sections.Count; j++)
                {
                    for (int i = 0; i < document.Sections[j].Paragraphs.Count; i++)
                    {
                        if (document.Sections[j].Paragraphs[i].Text.Contains(searchText))
                        {
                            document.Sections[j].Paragraphs.RemoveAt(i);
                            //document.Replace(searchText, string.Empty, true, true);
                            WSection newSection = document.Sections[j];
                            //newSection.BreakCode = SectionBreakCode.NoBreak;

                            IWTable table = newSection.AddTable();
                            WTableRow row = table.AddRow();

                            AddTableCell(row, ("Score"), 0.10, newSection.PageSetup.ClientWidth);
                            AddTableCell(row, ("Title"), 0.20, newSection.PageSetup.ClientWidth);
                            AddTableCell(row, ("Description"), 0.20, newSection.PageSetup.ClientWidth);
                            AddTableCell(row, ("Framework"), 0.15, newSection.PageSetup.ClientWidth);
                            AddTableCell(row, ("Reference"), 0.15, newSection.PageSetup.ClientWidth);
                            AddTableCell(row, ("Strength"), 0.10, newSection.PageSetup.ClientWidth);
                            AddTableCell(row, ("Implem."), 0.10, newSection.PageSetup.ClientWidth);

                            if (recordCount == 0)
                                recordCount = orderedNodeScores.Count();

                            int count = 0;
                            foreach (var pair in orderedNodeScores)
                            {
                                if (count >= recordCount)
                                    break;

                                string key = pair.Key;
                                double value = pair.Value;

                                row = table.AddRow(true, false);
                                AddTableCell(row, (value.ToString("F2")), 0.10, newSection.PageSetup.ClientWidth);
                                AddTableCell(row, Utility.RemoveHTML(GraphUtil.GetNodeTitle(key)), 0.20, newSection.PageSetup.ClientWidth);
                                AddTableCell(row, Utility.DecodeBase64TextandRemoveRTF(GraphUtil.GetNodeDescription(key)), 0.20, newSection.PageSetup.ClientWidth);
                                AddTableCell(row, GraphUtil.GetNodeFrameworkName(key), 0.15, newSection.PageSetup.ClientWidth);
                                AddTableCell(row, GraphUtil.GetNodeFrameworkReference(key), 0.15, newSection.PageSetup.ClientWidth);
                                AddTableCell(row, GraphUtil.GetPreviousNodeControlBaseScore(key).ToString("F2"), 0.10, newSection.PageSetup.ClientWidth);
                                AddTableCell(row, GraphUtil.GetControlNodeAssessedScore(key).ToString("F2"), 0.10, newSection.PageSetup.ClientWidth);

                                count++;
                            }
                            table.ApplyStyle(BuiltinTableStyle.LightShading);
                            document.Sections.Insert(j, newSection);
                        }
                    }
                }
            }
        }


        private void AddTableCell(WTableRow row, string text, double width, float clientWidth)
        {
            WTableCell cell = row.AddCell();
            cell.Width = (float)(clientWidth * width);
            cell.AddParagraph().AppendText(text);
        }
    }
}
