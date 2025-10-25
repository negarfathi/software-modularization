using System;
using System.IO;
using System.Linq;
using RationalRose;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Project
{
    public partial class Form1 : Form
    {
        OpenFileDialog DependencyFiles = new OpenFileDialog();
        OpenFileDialog ClusterFiles = new OpenFileDialog();
        DialogResult DP_result;
        DialogResult CL_result;
        string[] DepFiles;
        string[] CluFiles;
        string[] PackageNames;
        int PackageNumber = 0;
        List<string> PackageList = new List<string>();
        List<string> ClassList = new List<string>();
        List<RoseCategory> CategoryList = new List<RoseCategory>();
        Dictionary<string, RoseCategory> CategoryContainer = new Dictionary<string, RoseCategory>();
        int[,] CategoryRelation;
        List<RoseSubsystem> SubSystemList = new List<RoseSubsystem>();
        int[,] SubSystemRelation;
        public Form1()
        {
            InitializeComponent();
        }
        private void buttonSelectDependencyFiles_Click(object sender, EventArgs e)
        {
            listBoxDependencyFiles.Items.Clear();
            DependencyFiles.Title = "Select Dependency File(s)";
            DependencyFiles.Multiselect = true;
            DependencyFiles.Filter = "Text files (*.txt)|*.txt";
            DP_result = DependencyFiles.ShowDialog();
            if (DP_result == DialogResult.Cancel) return;
            foreach (string filename in DependencyFiles.FileNames)
            {
                listBoxDependencyFiles.Items.Add(filename);
            }
            DepFiles = DependencyFiles.FileNames;
        }
        private void buttonSelectClusterFiles_Click(object sender, EventArgs e)
        {
            listBoxClusterFiles.Items.Clear();
            ClusterFiles.Title = "Select Cluster File(s)";
            ClusterFiles.Multiselect = true;
            ClusterFiles.Filter = "Dotty files (*.dot)|*.dot";
            CL_result = ClusterFiles.ShowDialog();
            if (CL_result == DialogResult.Cancel) return;
            int i = 0;
            foreach (string filename in ClusterFiles.FileNames)
            {
                listBoxClusterFiles.Items.Add(filename);
                i++;
            }
            PackageNumber = i;
            CluFiles = ClusterFiles.FileNames;
            PackageNames = ClusterFiles.SafeFileNames;
        }
        private void buttonExtractPackageDiagram_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < PackageNumber; i++)
            {
                string[] parts = PackageNames[i].Split('.');
                PackageNames[i] = parts[0];
            }
            CategoryRelation = new int[PackageNumber, PackageNumber];
            SubSystemRelation = new int[PackageNumber, PackageNumber];
            RoseApplication rapp = new RoseApplication();
            RoseModel rmdl = rapp.NewModel();
            RoseClassDiagramCollection RCDC = rmdl.RootCategory.ClassDiagrams;
            RoseClassDiagram MainClassDiagram = RCDC.GetFirst("Main");
            MainClassDiagram.Visible = true;
            RoseModuleDiagramCollection RMDC = rmdl.RootSubsystem.ModuleDiagrams;
            RoseModuleDiagram MainModuleDiagram = RMDC.GetFirst("Main");
            MainModuleDiagram.Visible = true;
            short xPos = 0;
            short yPos = 100;
            short rowCounter = 1;
            for (int j = 0; j < PackageNumber; j++)
            {
                RoseCategory ClassPackage = rmdl.RootCategory.AddCategory(PackageNames[j]);
                CategoryList.Add(ClassPackage);
                CategoryContainer.Add(ClassPackage.Name, ClassPackage);
                IRoseClassDiagram ClassDiagram = ClassPackage.AddClassDiagram("Main");
                MainClassDiagram.AddCategory(ClassPackage);
                RoseSubsystem ModulePackage = rmdl.RootSubsystem.AddSubsystem(PackageNames[j]);
                SubSystemList.Add(ModulePackage);
                IRoseModuleDiagram ModuleDiagram = ModulePackage.AddModuleDiagram("Main");
                RoseSubsystemView MainRSSV = MainModuleDiagram.AddSubsystemView(ModulePackage);
                MainRSSV.Width = 300;
                if (j == 1)
                {
                    xPos = (short)200;
                    yPos = (short)200;
                }
                else if (j % 3 == 0)
                {
                    xPos = (short)200;
                    yPos = (short)((rowCounter) * 500);
                    if (j != 1) rowCounter++;
                }
                else
                {
                    xPos = (short)(xPos + 700);
                    yPos = (short)(yPos + 50);
                }
                MainRSSV.XPosition = xPos;
                MainRSSV.YPosition = yPos;
                PackageModeling(DepFiles[j], CluFiles[j], ClassPackage, ClassDiagram, ModulePackage, ModuleDiagram);
            }
            for (int i = 0; i < ClassList.Count; i++)
            {
                string CName = ClassList[i];
                string SPName = PackageList[i];
                List<string> results = ClassList.FindAll(item => item == CName);
                if (results.Count > 1)
                {
                    int SearchIndex = 0;
                    for (int j = 0; j < results.Count; j++)
                    {
                        int FindIndex = ClassList.FindIndex(SearchIndex, item => item == CName);
                        string DPName = PackageList[FindIndex];
                        if (FindIndex > i && SPName != DPName)
                        {
                            RoseCategory sCat = CategoryContainer[SPName];
                            RoseCategory dCat = CategoryContainer[DPName];
                            int sCatIndex = CategoryList.IndexOf(sCat);
                            int dCatIndex = CategoryList.IndexOf(dCat);
                            CategoryRelation[sCatIndex, dCatIndex]++;
                        }
                        SearchIndex = FindIndex + 1;
                    }
                }
            }
            RoseCategory SCategory;
            RoseCategory DCategory;
            RoseCategoryDependency rCatDep;
            for (int si = 0; si < PackageNumber; si++)
            {
                SCategory = CategoryList[si];
                for (int di = 0; di < PackageNumber; di++)
                {
                    DCategory = CategoryList[di];
                    if (si != di)
                    {
                        if (CategoryRelation[si, di] > 0)
                        {
                            string RelationAmount = Convert.ToString(CategoryRelation[si, di]);
                            rCatDep = SCategory.AddCategoryDependency(RelationAmount, DCategory.Name);
                            MainClassDiagram.AddRelationView((RoseRelation)rCatDep);
                        }
                    }
                }
            }
        }
        private void PackageModeling(string DepFile, string ClusterFile, RoseCategory ClassPackage, IRoseClassDiagram ClassDiagram, RoseSubsystem ModulePackage, IRoseModuleDiagram ModuleDiagram)
        {
            Dictionary<string, string> classComp = new Dictionary<string, string>();
            try
            {
                string[] depLines = File.ReadAllLines(DepFile);
                string[] clusLines = File.ReadAllLines(ClusterFile);
                RoseModule theModule;
                RoseComponentView rComView;
                Dictionary<string, RoseModule> catContainer = new Dictionary<string, RoseModule>();
                Dictionary<string, RoseClass> classContainer = new Dictionary<string, RoseClass>();
                int clusCounter = 0;
                int i = 0;
                short xPos = 0;
                short yPos = 100;
                short rowCounter = 1;
                List<RoseModule> modList = new List<RoseModule>();
                RoseClass theClass;
                while (i < clusLines.Count())
                {
                    if (clusLines[i].StartsWith("subgraph"))
                    {
                        clusCounter++;
                        i++;
                        if (clusLines[i].StartsWith("label"))
                        {
                            string[] parts1 = clusLines[i].Split(':');
                            string[] parts2 = parts1[1].Split('"');
                            string label = parts2[0];
                            theModule = ModulePackage.AddModule(label);
                        }
                        else
                        {
                            theModule = ModulePackage.AddModule("comp" + clusCounter);
                        }
                        theModule.Stereotype = "library";
                        modList.Add(theModule);
                        rComView = ModuleDiagram.AddComponentView(theModule);
                        rComView.Width = 500;
                        if (clusCounter == 1)
                        {
                            xPos = (short)200;
                            yPos = (short)200;
                        }
                        else if (clusCounter % 3 == 0)
                        {
                            xPos = (short)200;
                            yPos = (short)((rowCounter) * 500);
                            if (clusCounter != 1) rowCounter++;
                        }
                        else
                        {
                            xPos = (short)(xPos + 700);
                            yPos = (short)(yPos + 50);
                        }
                        rComView.XPosition = xPos;
                        rComView.YPosition = yPos;

                        catContainer.Add(theModule.Name, theModule);
                        i++;
                        while (!clusLines[i].StartsWith("}"))
                        {
                            if (clusLines[i].StartsWith("\""))
                            {
                                RoseClass rClass = new RoseClass();
                                string[] parts = clusLines[i].Split('"');
                                rClass.Name = parts[1];
                                classComp.Add(rClass.Name, theModule.Name);
                                theClass = ClassPackage.AddClass(parts[1]);
                                rClass.AddAssignedModule(theModule);
                                ClassDiagram.AddClass(theClass);
                                classContainer.Add(rClass.Name, theClass);
                                PackageList.Add(ClassPackage.Name);
                                ClassList.Add(rClass.Name);
                            }
                            i++;
                        }
                    }
                    i++;
                }
                i = 0;
                int[,] m = new int[clusCounter, clusCounter];
                int sModIndex;
                int dModIndex;
                while (i < depLines.Count())
                {
                    string[] dep = depLines[i].Split(' ');
                    string cls1 = dep[0];
                    string cls2 = dep[1];
                    String classDep = dep[2];
                    int depVal = Convert.ToInt32(dep[2]);
                    RoseClass sClass = classContainer[cls1];
                    RoseClassDependency rClsDep = sClass.AddClassDependency(classDep, cls2);
                    ClassDiagram.AddRelationView((RoseRelation)rClsDep);
                    string sourceCom = classComp[cls1];
                    string desCom = classComp[cls2];
                    RoseModule sMod = catContainer[sourceCom];
                    RoseModule dMod = catContainer[desCom];
                    sModIndex = modList.IndexOf(sMod);
                    dModIndex = modList.IndexOf(dMod);
                    m[sModIndex, dModIndex] = m[sModIndex, dModIndex] + depVal;
                    i++;
                }
                RoseModule sModule;
                RoseModule dModule;
                RoseModuleVisibilityRelationship rModRel;
                for (int si = 0; si < clusCounter; si++)
                {
                    sModule = modList[si];
                    for (int di = 0; di < clusCounter; di++)
                    {
                        dModule = modList[di];
                        if (si != di)
                        {
                            if (m[si, di] > 0)
                            {
                                rModRel = sModule.AddVisibilityRelationship(dModule);
                                rModRel.Name = Convert.ToString(m[si, di]);
                                ModuleDiagram.AddRelationView((RoseRelation)rModRel);
                            }
                        }
                    }
                }
            }
            catch (Exception) { }
        }
    }
}