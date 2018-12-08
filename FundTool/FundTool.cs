// (C) Copyright 2018 by  
//
using System;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;

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

            if (oked.HasValue && oked.Value) {
                /* input1 = win.Property1;
                   input2 = win.Property2;*/
                // DO something based in inputs
            }
        }

        [CommandMethod("FundToolIndirecta")]
        public static void GetInfoIndirectas()
        {
            bool? oked = false;
            /*object input1 = null;
            object input2 = null;*/

            System.Windows.Window win = new Indirectas();

            oked = Application.ShowModalWindow(win);

            if (oked.HasValue && oked.Value)
            {
                /* input1 = win.Property1;
                   input2 = win.Property2;*/
                // DO something based in inputs
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

}   