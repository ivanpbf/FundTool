// (C) Copyright 2018 by  
//
using System;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using static FundTool.Indirectas;
using static FundTool.Directas;
using System.Collections.Generic;

// This line is not mandatory, but improves loading performances
[assembly: CommandClass(typeof(FundTool.MyCommands))]

namespace FundTool
{

    // This class is instantiated by AutoCAD for each document when
    // a command is called by the user the first time in the context
    // of a given document. In other words, non static data in this class
    // is implicitly per-document!
    public class MyCommands
    {
        [CommandMethod("FundToolDirecta")]
        public static void GetInfoDirectas() {
            bool? oked = false;
            /*object input1 = null;
            object input2 = null;*/

            System.Windows.Window win = new Directas();

            oked = Application.ShowModalWindow(win);
            oked = true;
            if (oked.HasValue && oked.Value) {

            }
    }

        [CommandMethod("FundToolIndirecta")]
        public static void GetInfoIndirectas()
        {

            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            bool? oked = false;
            /*object input1 = null;
            object input2 = null;*/

            System.Windows.Window win = new Indirectas();

            oked = Application.ShowModalWindow(win);
            oked = true;
            if (oked.HasValue && oked.Value)
            {
                Indirectas instance = (Indirectas)win;
                List<Indirectas.Apoyo> apoyos;
                if (instance.apoyos != null)
                {
                    apoyos = instance.apoyos;

                    using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                    {
                        // Open the Block table for read
                        BlockTable acBlkTbl;
                        acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId,
                                                        OpenMode.ForRead) as BlockTable;

                        // Open the Block table record Model space for write
                        BlockTableRecord acBlkTblRec;
                        acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                                        OpenMode.ForWrite) as BlockTableRecord;
                        for (int i = 0; i < apoyos.Count; i++)
                        {
                            // Create a polyline with two segments (3 points)
                            using (Polyline acPoly = new Polyline())
                            {
                                acPoly.AddVertexAt(0, new Point2d(apoyos[i].Vertice1X, apoyos[i].Vertice1Y), 0, 0, 0);
                                acPoly.AddVertexAt(0, new Point2d(apoyos[i].Vertice2X, apoyos[i].Vertice2Y), 0, 0, 0);
                                acPoly.AddVertexAt(0, new Point2d(apoyos[i].Vertice4X, apoyos[i].Vertice4Y), 0, 0, 0);
                                acPoly.AddVertexAt(0, new Point2d(apoyos[i].Vertice3X, apoyos[i].Vertice3Y), 0, 0, 0);
                                acPoly.AddVertexAt(0, new Point2d(apoyos[i].Vertice1X, apoyos[i].Vertice1Y), 0, 0, 0);
                                // Add the new object to the block table record and the transaction
                                acBlkTblRec.AppendEntity(acPoly);
                                acTrans.AddNewlyCreatedDBObject(acPoly, true);
                            }
                            for(int j = 0; j < apoyos[i].Pilotes.Count; j++)
                            {
                                using (Circle circle = new Circle())
                                {
                                    circle.Center = new Point3d(apoyos[i].Pilotes[j].PosicionX, apoyos[i].Pilotes[j].PosicionY, 0);
                                    circle.Radius = apoyos[i].DiametroPilotes/100;
                                    acBlkTblRec.AppendEntity(circle);
                                    acTrans.AddNewlyCreatedDBObject(circle, true);
                                }
                            }
                            
                            // Save the new object to the database       
                        }
                        acTrans.Commit();
                    }
                    
                }
            }
            
        }
    }


        /*[CommandMethod("InputFromWinForm")]
        public static void GetInputFromWinForm(){
            var oked = false;
            object intp1 = null;
            object input2 = null;
            using (var dlg = new MyWinForm()){
                var res = Application.ShowModalDialog(dlg);
                if (res == System.Windows.Forms.DialogResult.OK){
                    oked = true;
                    input1 = dlg.Property1;
                    input2 = dlg.Property2;
                }
            }
            if (oked){
                //Do sothing
            }*/

}
