// (C) Copyright 2018 
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
using System.Linq;

[assembly: CommandClass(typeof(FundTool.MyCommands))]

namespace FundTool
{

    /*Creado por
     * Raquel Sandoval
     * Ivan Loscher
     */
    public class MyCommands
    {
        [CommandMethod("FundToolDirecta")]
        public static void GetInfoDirectas() {
            bool? oked = false;
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;


            System.Windows.Window win = new Directas();

            oked = Application.ShowModalWindow(win);
            oked = true;
            if (oked.HasValue && oked.Value)
            {
                Directas instance = (Directas)win;
                List<Directas.Apoyo> apoyos;
                if (instance.finalizo)
                {
                    apoyos = instance.apoyos;

                    using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                    {
                        BlockTable acBlkTbl;
                        acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId,
                                                        OpenMode.ForRead) as BlockTable;

                        BlockTableRecord acBlkTblRec;
                        acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                                        OpenMode.ForWrite) as BlockTableRecord;
                        Polyline[] acPoly = new Polyline[apoyos.Count];
                        Polyline[] columna = new Polyline[apoyos.Count];
                        Point3d[] points = new Point3d[apoyos.Count];
                        for (int i = 0; i < apoyos.Count; i++)
                        {
                            if (apoyos[i].Dimensionar)
                            {
                                using (acPoly[i] = new Polyline())
                                {
                                    acPoly[i].AddVertexAt(0, new Point2d(apoyos[i].Vertice1X, apoyos[i].Vertice1Y), 0, 0, 0);
                                    acPoly[i].AddVertexAt(1, new Point2d(apoyos[i].Vertice2X, apoyos[i].Vertice2Y), 0, 0, 0);
                                    acPoly[i].AddVertexAt(2, new Point2d(apoyos[i].Vertice4X, apoyos[i].Vertice4Y), 0, 0, 0);
                                    acPoly[i].AddVertexAt(3, new Point2d(apoyos[i].Vertice3X, apoyos[i].Vertice3Y), 0, 0, 0);
                                    acPoly[i].AddVertexAt(4, new Point2d(apoyos[i].Vertice1X, apoyos[i].Vertice1Y), 0, 0, 0);
                                    acBlkTblRec.AppendEntity(acPoly[i]);
                                    acTrans.AddNewlyCreatedDBObject(acPoly[i], true);
                                }
                            }
                            using (MText acMText = new MText())
                            {
                                if(apoyos[i].ZapataConjuntaX)//vertical
                                {
                                    acMText.Location = new Point3d(((apoyos[i].CoordEjeX-apoyos[i].B/2)-0.3), apoyos[i].CoordEjeY, 0);
                                    acMText.Width = apoyos[i].Vertice2X - apoyos[i].Vertice1X;
                                    acMText.Contents = ("-Carga Actuante " + apoyos[i].Carga + "Ton \n-Qadmisible "+Math.Round((apoyos[i].Qadmisible * 0.1),2) + "kg/cm² \n-B " + Math.Round(apoyos[i].B, 2) + "mts \n-L " + apoyos[i].Ltotal + "mts");
                                }
                                else if (apoyos[i].ZapataConjuntaY) //horizontal
                                {
                                    acMText.Location = new Point3d(apoyos[i].CoordEjeX, ((apoyos[i].CoordEjeY - apoyos[i].B / 2) - 0.2), 0);
                                    acMText.Width = apoyos[i].Vertice2X - apoyos[i].Vertice1X;
                                    acMText.Contents = ("-Carga Actuante " + apoyos[i].Carga + "Ton \n-Qadmisible " + Math.Round((apoyos[i].Qadmisible * 0.1), 2) + "kg/cm² \n-B " + Math.Round(apoyos[i].B,2) + "mts \n-L " + apoyos[i].Ltotal + "mts");
                                }
                                else
                                {
                                    acMText.Location = new Point3d(apoyos[i].Vertice3X, apoyos[i].Vertice3Y - 0.3, 0);
                                    acMText.Width = apoyos[i].Vertice2X - apoyos[i].Vertice1X;
                                    acMText.Contents = ("-Carga Actuante " + apoyos[i].Carga + "Ton \n-Qadmisible " + Math.Round((apoyos[i].Qadmisible * 0.1), 2) + "kg/cm² \n-B " + Math.Round(apoyos[i].B, 2) + "mts");
                                }
                                acBlkTblRec.AppendEntity(acMText);
                                acTrans.AddNewlyCreatedDBObject(acMText, true);
                            }
                            using (columna[i] = new Polyline())
                            {
                                columna[i].AddVertexAt(0, new Point2d(apoyos[i].ColumnaV1X, apoyos[i].ColumnaV1Y), 0, 0, 0);
                                Point3d punto1 = new Point3d(apoyos[i].ColumnaV1X, apoyos[i].ColumnaV1Y, 0);
                                columna[i].AddVertexAt(1, new Point2d(apoyos[i].ColumnaV2X, apoyos[i].ColumnaV2Y), 0, 0, 0);
                                Point3d punto2 = new Point3d(apoyos[i].ColumnaV2X, apoyos[i].ColumnaV2Y, 0);
                                columna[i].AddVertexAt(2, new Point2d(apoyos[i].ColumnaV4X, apoyos[i].ColumnaV4Y), 0, 0, 0);
                                columna[i].AddVertexAt(3, new Point2d(apoyos[i].ColumnaV3X, apoyos[i].ColumnaV3Y), 0, 0, 0);
                                Point3d punto3 = new Point3d(apoyos[i].ColumnaV3X, apoyos[i].ColumnaV3Y, 0);
                                columna[i].AddVertexAt(4, new Point2d(apoyos[i].ColumnaV1X, apoyos[i].ColumnaV1Y), 0, 0, 0);
                                Point3d punto4 = new Point3d(apoyos[i].ColumnaV1X, apoyos[i].ColumnaV1Y, 0);
                                acBlkTblRec.AppendEntity(columna[i]);
                                acTrans.AddNewlyCreatedDBObject(columna[i], true);
                                using (RotatedDimension dimc1 = new RotatedDimension())
                                {
                                    dimc1.XLine1Point = punto1;
                                    dimc1.XLine2Point = punto2;
                                    dimc1.DimLinePoint = new Point3d(0, apoyos[i].ColumnaV1Y + 0.5, 0);
                                    dimc1.DimensionStyle = acCurDb.Dimstyle;
                                    acBlkTblRec.AppendEntity(dimc1);
                                    acTrans.AddNewlyCreatedDBObject(dimc1, true);
                                }
                                using (RotatedDimension dimc2 = new RotatedDimension())
                                {
                                    dimc2.XLine1Point = punto3;
                                    dimc2.XLine2Point = punto4;
                                    dimc2.DimLinePoint = new Point3d(apoyos[i].ColumnaV1X - 0.5, 0, 0);
                                    dimc2.Rotation = Math.PI / 2.0;
                                    dimc2.DimensionStyle = acCurDb.Dimstyle;
                                    acBlkTblRec.AppendEntity(dimc2);
                                    acTrans.AddNewlyCreatedDBObject(dimc2, true);
                                }
                            }
 
                        }
                        double[] xCoords = points.Select(p => p.X).Distinct().OrderBy(x => x).ToArray();
                        double[] yCoords = points.Select(p => p.Y).Distinct().OrderBy(y => y).ToArray();
                        for (int i = 0; i < yCoords.Length - 1; i++)
                        {
                            var dim = new RotatedDimension();
                            dim.XLine1Point = new Point3d(xCoords[0], yCoords[i], 0.0);
                            dim.XLine2Point = new Point3d(xCoords[0], yCoords[i + 1], 0.0);
                            dim.DimLinePoint = new Point3d(xCoords[0] - 1.5, 0.0, 0.0);
                            dim.Rotation = Math.PI / 2.0;
                            acBlkTblRec.AppendEntity(dim);
                            acTrans.AddNewlyCreatedDBObject(dim, true);
                        }
                        for (int i = 0; i < xCoords.Length - 1; i++)
                        {
                            var dim = new RotatedDimension();
                            dim.XLine1Point = new Point3d(xCoords[i], yCoords[0], 0.0);
                            dim.XLine2Point = new Point3d(xCoords[i + 1], yCoords[0], 0.0);
                            dim.DimLinePoint = new Point3d(0.0, yCoords[0] - 1.5, 0.0);
                            //ojo, -1.5 porque usualmente esta cota va la ultima fila
                            dim.Rotation = 0.0;
                            acBlkTblRec.AppendEntity(dim);
                            acTrans.AddNewlyCreatedDBObject(dim, true);
                        }
                        acTrans.Commit();
                    }

                }
            }
        }

        [CommandMethod("FundToolIndirecta")]
        public static void GetInfoIndirectas()
        {

            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            bool? oked = false;


            System.Windows.Window win = new Indirectas();

            oked = Application.ShowModalWindow(win);
            oked = true;
            if (oked.HasValue && oked.Value)
            {
                Indirectas instance = (Indirectas)win;
                List<Indirectas.Apoyo> apoyos;
                if (instance.finalizo)
                {
                    apoyos = instance.apoyos;

                    using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                    {
                        BlockTable acBlkTbl;
                        acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId,
                                                        OpenMode.ForRead) as BlockTable;

                        BlockTableRecord acBlkTblRec;
                        acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                                        OpenMode.ForWrite) as BlockTableRecord;
                        Polyline[] acPoly = new Polyline[apoyos.Count];
                        Polyline[] columna = new Polyline[apoyos.Count];
                        Point3d[] points = new Point3d[apoyos.Count];
                        for (int i = 0; i < apoyos.Count; i++)
                        {
                            points[i] = new Point3d(apoyos[i].CoordEjeX, apoyos[i].CoordEjeY, 0);
                            using (acPoly[i] = new Polyline())
                            {
                                acPoly[i].AddVertexAt(0, new Point2d(apoyos[i].Vertice1X, apoyos[i].Vertice1Y), 0, 0, 0);
                                acPoly[i].AddVertexAt(1, new Point2d(apoyos[i].Vertice2X, apoyos[i].Vertice2Y), 0, 0, 0);
                                acPoly[i].AddVertexAt(2, new Point2d(apoyos[i].Vertice4X, apoyos[i].Vertice4Y), 0, 0, 0);
                                acPoly[i].AddVertexAt(3, new Point2d(apoyos[i].Vertice3X, apoyos[i].Vertice3Y), 0, 0, 0);
                                acPoly[i].AddVertexAt(4, new Point2d(apoyos[i].Vertice1X, apoyos[i].Vertice1Y), 0, 0, 0);
                                acBlkTblRec.AppendEntity(acPoly[i]);
                                acTrans.AddNewlyCreatedDBObject(acPoly[i], true);
                            }
                            using (MText acMText = new MText())
                            {
                                acMText.Location = new Point3d(apoyos[i].Vertice3X, apoyos[i].Vertice3Y-0.2, 0);
                                acMText.Width = apoyos[i].Vertice2X- apoyos[i].Vertice1X + apoyos[i].DistanciaMinimaEntrePilotes;
                                acMText.Contents = "-Carga actuante "+apoyos[i].Carga+"Ton \n-Pilotes "+apoyos[i].Pilotes.Count+ " ∅" + (apoyos[i].Pilotes[0].Diametro/100)+"mts \n-Altura del Cabezal "+apoyos[i].GrosorCabezal+"mts \n-Acero Longitudinal "+apoyos[i].NumeroCabillas+ " ∅" + 
                                    apoyos[i].DiametroTeoricoPulgadas+ "” \n-QADM de Grupo " + Math.Round(apoyos[i].QadmisibleGrupo, 2) + "Ton \n-QEST " + Math.Round(apoyos[i].Qestructural, 2)+"Ton \n-Momento en X "+apoyos[i].MtoEnEjeX+"Ton.m  \n-Momento en Y "+apoyos[i].MtoEnEjeY+"Ton.m  \n-Mayor Carga de Pilote "+ Math.Round(apoyos[i].MayorCargaPilote,2) + "Ton";

                                acBlkTblRec.AppendEntity(acMText);
                                acTrans.AddNewlyCreatedDBObject(acMText, true);
                            }
                            using (columna[i] = new Polyline())
                            {
                                columna[i].AddVertexAt(0, new Point2d(apoyos[i].ColumnaV1X, apoyos[i].ColumnaV1Y), 0, 0, 0);
                                Point3d punto1 = new Point3d(apoyos[i].ColumnaV1X, apoyos[i].ColumnaV1Y, 0);
                                columna[i].AddVertexAt(1, new Point2d(apoyos[i].ColumnaV2X, apoyos[i].ColumnaV2Y), 0, 0, 0);
                                Point3d punto2 = new Point3d(apoyos[i].ColumnaV2X, apoyos[i].ColumnaV2Y, 0);
                                columna[i].AddVertexAt(2, new Point2d(apoyos[i].ColumnaV4X, apoyos[i].ColumnaV4Y), 0, 0, 0);
                                columna[i].AddVertexAt(3, new Point2d(apoyos[i].ColumnaV3X, apoyos[i].ColumnaV3Y), 0, 0, 0);
                                Point3d punto3 = new Point3d(apoyos[i].ColumnaV3X, apoyos[i].ColumnaV3Y, 0);
                                columna[i].AddVertexAt(4, new Point2d(apoyos[i].ColumnaV1X, apoyos[i].ColumnaV1Y), 0, 0, 0);
                                Point3d punto4 = new Point3d(apoyos[i].ColumnaV1X, apoyos[i].ColumnaV1Y, 0);
                                acBlkTblRec.AppendEntity(columna[i]);
                                acTrans.AddNewlyCreatedDBObject(columna[i], true);
                                using (RotatedDimension dimc1 = new RotatedDimension())
                                {
                                    dimc1.XLine1Point = punto1;
                                    dimc1.XLine2Point = punto2;
                                    dimc1.DimLinePoint = new Point3d(0, apoyos[i].ColumnaV1Y+0.5, 0);
                                    dimc1.DimensionStyle = acCurDb.Dimstyle;
                                    acBlkTblRec.AppendEntity(dimc1);
                                    acTrans.AddNewlyCreatedDBObject(dimc1, true);
                                }
                                using (RotatedDimension dimc2 = new RotatedDimension())
                                {
                                    dimc2.XLine1Point = punto3;
                                    dimc2.XLine2Point = punto4;
                                    dimc2.DimLinePoint = new Point3d(apoyos[i].ColumnaV1X-0.5, 0, 0);
                                    dimc2.Rotation = Math.PI / 2.0;
                                    dimc2.DimensionStyle = acCurDb.Dimstyle;
                                    acBlkTblRec.AppendEntity(dimc2);
                                    acTrans.AddNewlyCreatedDBObject(dimc2, true);
                                }
                            }

                            Circle[] circle = new Circle[apoyos[i].Pilotes.Count];
                            for(int j = 0; j < apoyos[i].Pilotes.Count; j++)
                            {
                                using (circle[j] = new Circle())
                                {
                                    circle[j].Center = new Point3d(apoyos[i].Pilotes[j].PosicionX, apoyos[i].Pilotes[j].PosicionY, 0);
                                    circle[j].Radius = apoyos[i].DiametroPilotes/200;
                                    acBlkTblRec.AppendEntity(circle[j]);
                                    acTrans.AddNewlyCreatedDBObject(circle[j], true);
                                    /*using (RadialDimension acRadDim = new RadialDimension())
                                    {
                                        acRadDim.Center = circle[j].Center;
                                        acRadDim.ChordPoint = new Point3d(apoyos[i].Pilotes[j].PosicionX + circle[j].Radius, apoyos[i].Pilotes[j].PosicionY, 0);
                                        acRadDim.DimensionStyle = acCurDb.Dimstyle;
                                        acBlkTblRec.AppendEntity(acRadDim);
                                        acTrans.AddNewlyCreatedDBObject(acRadDim, true);

                                    }este codigo es para mostrar el diametro del circulo, no hace falta ya que se imprime*/
                                }
                            }


                        }
                        double[] xCoords = points.Select(p => p.X).Distinct().OrderBy(x => x).ToArray();
                        double[] yCoords = points.Select(p => p.Y).Distinct().OrderBy(y => y).ToArray();
                        for (int i = 0; i < yCoords.Length - 1; i++)
                        {
                            var dim = new RotatedDimension();
                            dim.XLine1Point = new Point3d(xCoords[0], yCoords[i], 0.0);
                            dim.XLine2Point = new Point3d(xCoords[0], yCoords[i + 1], 0.0);
                            dim.DimLinePoint = new Point3d(xCoords[0]-1.5, 0.0, 0.0);
                            dim.Rotation = Math.PI / 2.0;
                            acBlkTblRec.AppendEntity(dim);
                            acTrans.AddNewlyCreatedDBObject(dim, true);
                        }
                        for (int i = 0; i < xCoords.Length - 1; i++)
                        {
                            var dim = new RotatedDimension();
                            dim.XLine1Point = new Point3d(xCoords[i], yCoords[0], 0.0);
                            dim.XLine2Point = new Point3d(xCoords[i + 1], yCoords[0], 0.0);
                            dim.DimLinePoint = new Point3d(0.0, yCoords[0]-1.5, 0.0);
                            //ojo, -1.5 porque usualmente esta cota va la ultima fila
                            dim.Rotation = 0.0;
                            acBlkTblRec.AppendEntity(dim);
                            acTrans.AddNewlyCreatedDBObject(dim, true);
                        }
                        acTrans.Commit();
                    }
                    
                }
            }
            
        }
    }

}
