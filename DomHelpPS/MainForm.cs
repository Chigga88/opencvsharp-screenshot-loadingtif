
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using Microsoft.VisualBasic;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace DomHelpPS
{
    public partial class MainForm : Form
    {
        private bool blTrackRectangle = false;
        private IEnvelope mIEnvelope;
        private IGraphicsContainer mIGraphicsContainer;
        private IFeatureLayer mIFeatureLayer;
        private int[] quality = new int[2];
        DateTime aa = new DateTime();
        public MainForm()
        {
            this.InitializeComponent();
            Console.Write("");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.TOCControl.SetBuddyControl(this.MapControl);
            this.MapControl.LoadMxFile(@"D:\20210203\IMG\666.mxd");
            this.MapControl.Extent = this.MapControl.FullExtent;
            ICommand mICommand = new ControlsMapPanToolClass();
            mICommand.OnCreate(this.MapControl.Object);
            this.MapControl.CurrentTool = mICommand as ITool;
            this.mIGraphicsContainer = this.MapControl.Map as IGraphicsContainer;
            this.mIFeatureLayer = this.MapControl.GetILayer("Rectangle") as IFeatureLayer;
        }

        /// <summary>
        /// 根据栅格图层创建SHP，栅格图层要在根目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreatePolygenShpFromRasterLayerButton_Click(object sender, EventArgs e)
        {
            string strFileNameWithType = "Rectangle.shp";//shp文件名
            string strFolderPath = Application.StartupPath;
            IWorkspaceFactory mIWorkspaceFactory = new ShapefileWorkspaceFactory();//文件夹
            IWorkspace mIWorkspace = mIWorkspaceFactory.OpenFromFile(strFolderPath, 0);
            IFeatureWorkspace mIFeatureWorkspace = mIWorkspace as IFeatureWorkspace;
            IFeatureClass mIFeatureClass;
            if (File.Exists(strFolderPath + "\\" + strFileNameWithType))
            {
                mIFeatureClass = mIFeatureWorkspace.OpenFeatureClass(strFileNameWithType);//fileName为文件名(不包含路径)
                IDataset mIDataset = mIFeatureClass as IDataset;
                mIDataset.Delete();
            }
            //指定类型和空间参考
            IGeometryDef mIGeometryDef = new GeometryDefClass();
            IGeometryDefEdit mIGeometryDefEdit = mIGeometryDef as IGeometryDefEdit;
            mIGeometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
            mIGeometryDefEdit.SpatialReference_2 = this.MapControl.SpatialReference;
            //建立Shp文件的字段集
            IFields mIFields = new FieldsClass();
            IFieldsEdit mIFieldsEdit = mIFields as IFieldsEdit;
            IField mIField = new FieldClass();
            IFieldEdit mIFieldEdit = mIField as IFieldEdit;
            //建立shape（几何）字段
            mIFieldEdit.Name_2 = "Shape";
            mIFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            mIFieldEdit.GeometryDef_2 = mIGeometryDef;
            mIFieldsEdit.AddField(mIField);
            //新建Name字段
            mIField = new FieldClass();
            mIFieldEdit = mIField as IFieldEdit;
            mIFieldEdit.Length_2 = 32;
            mIFieldEdit.Name_2 = "Name";
            mIFieldEdit.AliasName_2 = "Name";
            mIFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            mIFieldsEdit.AddField(mIField);

            //新建Xmin字段
            mIField = new FieldClass();
            mIFieldEdit = mIField as IFieldEdit;
            mIFieldEdit.Length_2 = 32;
            mIFieldEdit.Name_2 = "Xmin";
            mIFieldEdit.AliasName_2 = "Xmin";
            mIFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            mIFieldsEdit.AddField(mIField);

            //新建Ymin字段
            mIField = new FieldClass();
            mIFieldEdit = mIField as IFieldEdit;
            mIFieldEdit.Length_2 = 32;
            mIFieldEdit.Name_2 = "Ymin";
            mIFieldEdit.AliasName_2 = "Ymin";
            mIFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            mIFieldsEdit.AddField(mIField);

            //新建Xmax字段
            mIField = new FieldClass();
            mIFieldEdit = mIField as IFieldEdit;
            mIFieldEdit.Length_2 = 32;
            mIFieldEdit.Name_2 = "Xmax";
            mIFieldEdit.AliasName_2 = "Xmax";
            mIFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            mIFieldsEdit.AddField(mIField);

            //新建Ymax字段
            mIField = new FieldClass();
            mIFieldEdit = mIField as IFieldEdit;
            mIFieldEdit.Length_2 = 32;
            mIFieldEdit.Name_2 = "Ymax";
            mIFieldEdit.AliasName_2 = "Ymax";
            mIFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            mIFieldsEdit.AddField(mIField);

            //新建CSize字段
            mIField = new FieldClass();
            mIFieldEdit = mIField as IFieldEdit;
            mIFieldEdit.Length_2 = 32;
            mIFieldEdit.Name_2 = "CSize";
            mIFieldEdit.AliasName_2 = "CSize";
            mIFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            mIFieldsEdit.AddField(mIField);

            //新建Width字段
            mIField = new FieldClass();
            mIFieldEdit = mIField as IFieldEdit;
            mIFieldEdit.Length_2 = 32;
            mIFieldEdit.Name_2 = "Width";
            mIFieldEdit.AliasName_2 = "Width";
            mIFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
            mIFieldsEdit.AddField(mIField);

            //新建Height字段
            mIField = new FieldClass();
            mIFieldEdit = mIField as IFieldEdit;
            mIFieldEdit.Length_2 = 32;
            mIFieldEdit.Name_2 = "Height";
            mIFieldEdit.AliasName_2 = "Height";
            mIFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
            mIFieldsEdit.AddField(mIField);

            //最后创建Shp文件
            mIFeatureClass = mIFeatureWorkspace.CreateFeatureClass(strFileNameWithType, mIFields, null, null, esriFeatureType.esriFTSimple, "Shape", "");
            for (int i = 0; i < this.MapControl.LayerCount; i++)
            {
                ILayer mILayer = this.MapControl.get_Layer(i);
                IRasterLayer mIRasterLayer = mILayer as IRasterLayer;
                if (mIRasterLayer != null)
                {
                    IRasterProps mIRasterProps = mIRasterLayer.Raster as IRasterProps;
                    IFeature mIFeature = mIFeatureClass.CreateFeature();
                    IPointCollection mIPointCollection = new PolygonClass();
                    mIPointCollection.AddPoint(mIRasterProps.Extent.UpperLeft);
                    mIPointCollection.AddPoint(mIRasterProps.Extent.UpperRight);
                    mIPointCollection.AddPoint(mIRasterProps.Extent.LowerRight);
                    mIPointCollection.AddPoint(mIRasterProps.Extent.LowerLeft);
                    mIPointCollection.AddPoint(mIRasterProps.Extent.UpperLeft);
                    IPolygon mIPolygon = (IPolygon)mIPointCollection;
                    mIFeature.Shape = mIPolygon;
                    mIFeature.set_Value(mIFeature.Fields.FindField("Name"), mILayer.Name);
                    mIFeature.set_Value(mIFeature.Fields.FindField("Xmin"), mIRasterProps.Extent.XMin);
                    mIFeature.set_Value(mIFeature.Fields.FindField("Ymin"), mIRasterProps.Extent.YMin);
                    mIFeature.set_Value(mIFeature.Fields.FindField("Xmax"), mIRasterProps.Extent.XMax);
                    mIFeature.set_Value(mIFeature.Fields.FindField("Ymax"), mIRasterProps.Extent.YMax);
                    mIFeature.set_Value(mIFeature.Fields.FindField("CSize"), mIRasterProps.MeanCellSize().X.RetentionDecimal(3));
                    //douCellSize = mIRasterProps.MeanCellSize().X.RetentionDecimal(3);
                    mIFeature.set_Value(mIFeature.Fields.FindField("Width"), mIRasterProps.Width);
                    mIFeature.set_Value(mIFeature.Fields.FindField("Height"), mIRasterProps.Height);
                    mIFeature.Store();
                }
            }
            //解除锁定
            IWorkspaceFactoryLockControl mIWorkspaceFactoryLockControl = mIWorkspaceFactory as IWorkspaceFactoryLockControl;
            if (mIWorkspaceFactoryLockControl.SchemaLockingEnabled)
            {
                mIWorkspaceFactoryLockControl.DisableSchemaLocking();
            }
        }

        /// <summary>
        /// 全图浏览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FullExtentButton_Click(object sender, EventArgs e)
        {
            this.MapControl.Extent = this.MapControl.FullExtent;
        }

        /// <summary>
        /// 框选范围
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RectangleSelectButton_Click(object sender, EventArgs e)
        {
            this.blTrackRectangle = true;
            this.MapControl.MousePointer = esriControlsMousePointer.esriPointerArrow;
            this.MapControl.CurrentTool = null;
            this.mIGraphicsContainer.DeleteAllElements();
            this.MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, this.mIEnvelope);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapControl_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (this.blTrackRectangle == true)
            {
                this.mIEnvelope = this.MapControl.TrackRectangle();
                this.blTrackRectangle = false;
                ICommand mICommand = new ControlsMapPanToolClass();
                mICommand.OnCreate(this.MapControl.Object);
                this.MapControl.CurrentTool = mICommand as ITool;
                ISimpleLineSymbol mISimpleLineSymbol = new SimpleLineSymbolClass();
                RgbColor mRgbColor = new RgbColor();
                mRgbColor.Red = 255;
                mRgbColor.Green = 0;
                mRgbColor.Blue = 0;
                mISimpleLineSymbol.Color = mRgbColor;
                mISimpleLineSymbol.Width = 1;
                ILineElement mILineElement = new LineElementClass();
                mILineElement.Symbol = mISimpleLineSymbol;
                IElement mIElement = mILineElement as IElement;
                IPointCollection mIPointCollection = new PolylineClass();
                mIPointCollection.AddPoint(this.mIEnvelope.UpperLeft);
                mIPointCollection.AddPoint(this.mIEnvelope.LowerLeft);
                mIPointCollection.AddPoint(this.mIEnvelope.LowerRight);
                mIPointCollection.AddPoint(this.mIEnvelope.UpperRight);
                mIPointCollection.AddPoint(this.mIEnvelope.UpperLeft);
                IPolyline mIPolyline = (IPolyline)mIPointCollection;
                mIElement.Geometry = mIPolyline;
                this.mIGraphicsContainer.AddElement(mIElement, 0);
                this.MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, this.mIEnvelope);
                List<IFeature> lstIFeatureall = this.mIFeatureLayer.GetLstIFeature("", false);
                List<double> Xmins = new List<double>();
                List<double> Xmaxs = new List<double>();
                List<double> Ymins = new List<double>();
                List<double> Ymaxs = new List<double>();
                for (int i = 0; i < lstIFeatureall.Count; i++)
                {
                    Xmins.Add(lstIFeatureall[i].GetStringValue("Xmin").ToDouble().RetentionDecimal(6));
                    Xmaxs.Add(lstIFeatureall[i].GetStringValue("Xmax").ToDouble().RetentionDecimal(6));
                    Ymins.Add(lstIFeatureall[i].GetStringValue("Ymin").ToDouble().RetentionDecimal(6));
                    Ymaxs.Add(lstIFeatureall[i].GetStringValue("Ymax").ToDouble().RetentionDecimal(6));
                }
                double XmaxAll = Xmaxs.Max();
                double XminAll = Xmins.Min();
                double YminAll = Ymins.Min();
                double YmaxAll = Ymaxs.Max();
                List<IFeature> lstIFeature = this.mIFeatureLayer.GetLstIFeature(this.mIEnvelope, "", esriSpatialRelEnum.esriSpatialRelIntersects);
                if (lstIFeature.Count == 0)
                {
                    MessageBox.Show("选了0块！请重新框！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                GC.Collect();
                MessageBox.Show("框中" + lstIFeature.Count.ToString() + "个", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                aa = DateTime.Now;
                double ownXmin = this.mIEnvelope.XMin.RetentionDecimal(6);//自己框Xmin
                double ownXmax = this.mIEnvelope.XMax.RetentionDecimal(6);//自己框Xmin
                double ownYmin = this.mIEnvelope.YMin.RetentionDecimal(5);//自己框Xmin
                double ownYmax = this.mIEnvelope.YMax.RetentionDecimal(5);//自己框Xmin
                if (ownXmax > XmaxAll)
                {
                    ownXmax = XmaxAll;
                }
                if (ownYmax > YmaxAll)
                {
                    ownYmax = YmaxAll;
                }
                if (ownXmin < XminAll)
                {
                    ownXmin = XminAll;
                }
                if (ownYmin < YminAll)
                {
                    ownYmin = YminAll;
                }
                double CellSize = lstIFeature[0].GetStringValue("CSize").ToDouble().RetentionDecimal(3);//影像像素宽度
                int allrow = Convert.ToInt32(Math.Round((XmaxAll - XminAll) / CellSize / lstIFeatureall[0].GetStringValue("Width").ToInt32()));
                int allY = Convert.ToInt32((YmaxAll - YminAll) / CellSize);
                int X_max = Convert.ToInt32((ownXmax - XminAll) / CellSize);//像素坐标Xmax
                int X_min = Convert.ToInt32((ownXmin - XminAll) / CellSize);//像素坐标Xmin
                int Y_max = Convert.ToInt32((ownYmax - YminAll) / CellSize);//像素坐标Ymax
                int Y_min = Convert.ToInt32((ownYmin - YminAll) / CellSize);//像素坐标Ymin
                if (X_max - X_min >= 10000 || Y_max - Y_min >= 10000)
                {
                    MessageBox.Show("框选范围过大请再次尝试,请将范围控制在10000*10000像素");
                    return;
                }
                List<img> imgs = new List<img>();
                List<int> rows = new List<int>();
                List<int> cols = new List<int>();
                List<string> names = new List<string>();
                for (int i = 0; i < lstIFeature.Count; i++)
                {
                    string Name = lstIFeature[i].GetStringValue("Name");//影像名字
                    ILayer mILayer = this.MapControl.GetILayer(Name);
                    IRasterLayer mIRasterLayer = mILayer as IRasterLayer;
                    string Path = mIRasterLayer.FilePath;//影像文件路径
                    int Width = lstIFeature[i].GetStringValue("Width").ToInt32();//影像宽
                    int Height = lstIFeature[i].GetStringValue("Height").ToInt32();//影像高
                    double Xmin = lstIFeature[i].GetStringValue("Xmin").ToDouble().RetentionDecimal(6);//影像包围框Xmin
                    double Xmax = lstIFeature[i].GetStringValue("Xmax").ToDouble().RetentionDecimal(6);//影像包围框Xmax
                    double Ymin = lstIFeature[i].GetStringValue("Ymin").ToDouble().RetentionDecimal(5);//影像包围框Ymin
                    double Ymax = lstIFeature[i].GetStringValue("Ymax").ToDouble().RetentionDecimal(5);//影像包围框Ymax
                    int col = Convert.ToInt32(Math.Ceiling(Math.Round((Xmax - XminAll) / CellSize) / lstIFeatureall[0].GetStringValue("Width").ToInt32()));
                    int row = allrow - Convert.ToInt32(Math.Ceiling(Math.Round((Ymax - YminAll) / CellSize) / lstIFeatureall[0].GetStringValue("Height").ToInt32())) + 1;
                    rows.Add(row);
                    cols.Add(col);
                    names.Add(Name);
                    imgs.Add(new img(row, col, new Mat(Path), Path, Convert.ToInt32((Xmax - XminAll) / CellSize), Convert.ToInt32((Xmin - XminAll) / CellSize), Convert.ToInt32(allY - (Ymax - YminAll) / CellSize), Convert.ToInt32(allY - (Ymin - YminAll) / CellSize)));
                }
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "*.txt|*.txt";
                paixu(ref imgs, rows, cols);
                printscreen(imgs, cols, rows, X_min, allY - Y_max, X_max, allY - Y_min, names, saveFileDialog);
            }
        }
        /// <summary>
        /// 截取图片
        /// </summary>
        /// <param name="imgs"></param>
        /// <param name="cols"></param>
        /// <param name="rows"></param>
        /// <param name="xmin"></param>
        /// <param name="ymin"></param>
        /// <param name="xmax"></param>
        /// <param name="ymax"></param>
        /// <param name="names"></param>
        private void printscreen(List<img> imgs, List<int> cols, List<int> rows, int xmin, int ymin, int xmax, int ymax, List<string> names, SaveFileDialog saveFileDialog)
        {
            quality[0] = (int)ImwriteFlags.JpegQuality;
            quality[1] = 100;
            List<Rect> rects = new List<Rect>();
            int maxcol = cols.Max();
            int maxrow = rows.Max();
            int mincol = cols.Min();
            int minrow = rows.Min();
            for (int i = 0; i < imgs.Count; i++)
            {
                if (maxcol == mincol && maxrow == minrow)
                {
                    rects.Add(new Rect(
                     xmin - imgs[i].xmin
                     , ymin - imgs[i].ymin
                     , xmax - xmin
                     , ymax - ymin
                     ));
                }
                else if (imgs[i].col == mincol && imgs[i].row == minrow)
                {
                    if (maxcol == mincol)
                    {
                        rects.Add(new Rect(
                          xmin - imgs[i].xmin
                       , ymin - imgs[i].ymin
                       , xmax - xmin
                       , imgs[i].ymax - ymin
                       ));
                    }
                    else if (maxrow == minrow)
                    {
                        rects.Add(new Rect(
                         xmin - imgs[i].xmin
                         , ymin - imgs[i].ymin
                         , imgs[i].xmax - xmin
                         , ymax - ymin
                        ));
                    }
                    else
                    {
                        rects.Add(new Rect(
                        xmin - imgs[i].xmin
                        , ymin - imgs[i].ymin
                        , imgs[i].xmax - xmin
                        , imgs[i].ymax - ymin
                        ));
                    }
                }
                else if (imgs[i].col == maxcol || imgs[i].row == minrow)
                {
                    if (imgs[i].row == minrow && imgs[i].col == maxcol)
                    {
                        if (maxrow == minrow)
                        {
                            rects.Add(new Rect(
                            0
                            , ymin - imgs[i].ymin
                            , xmax - imgs[i].xmin
                            , ymax - ymin
                            ));
                        }
                        else
                        {
                            rects.Add(new Rect(
                            imgs[i].mat.Width * imgs[i].col - (imgs[i].col) * imgs[i].mat.Width
                            , ymin - imgs[i].ymin
                            , xmax - imgs[i].xmin
                            , imgs[i].ymax - ymin
                            ));
                        }
                    }
                    else if (imgs[i].col == maxcol && imgs[i].row == maxrow)
                    {
                        if (maxcol == mincol)
                        {
                            rects.Add(new Rect(
                            xmin - imgs[i].xmin
                            , 0
                            , xmax - xmin
                            , ymax - imgs[i].ymin
                            ));
                        }
                        else
                        {
                            rects.Add(new Rect(
                            0
                            , 0
                            , xmax - imgs[i].xmin
                            , ymax - imgs[i].ymin
                            ));
                        }
                    }
                }
                else if (imgs[i].row == maxrow || imgs[i].col == mincol)
                {
                    if (imgs[i].col == mincol && imgs[i].row == maxrow)
                    {
                        rects.Add(new Rect(
                        xmin - imgs[i].xmin
                        , imgs[i].mat.Height * imgs[i].row - (imgs[i].row) * imgs[i].mat.Height
                        , imgs[i].xmax - xmin
                        , ymax - imgs[i].ymin
                        ));
                    }
                }
            }
            List<Mat> smallimg = new List<Mat>();
            List<imginfo> imginfo = new List<imginfo>();
            for (int i = 0; i < rects.Count; i++)
            {
                imginfo.Add(new imginfo(imgs[i].path, rects[i].X, rects[i].Y, rects[i].Width, rects[i].Height, imgs[i].col, imgs[i].row, System.IO.Path.GetFileName(imgs[i].path)));
                smallimg.Add(new Mat(imgs[i].mat, rects[i]));
            }
            if (smallimg.Count == 3)
            {
                Mat newmat = new Mat(imginfo[0].height, imginfo[2].width, MatType.CV_8UC3);
                imginfo.Insert(1, new imginfo("D:\\null", 0, rects[0].Y, newmat.Width, newmat.Height, imgs[0].col + 1, imgs[2].row - 1, "null"));
                smallimg.Insert(1, newmat);
            }
            int num = 0;
            Mat img = new Mat();
            List<Mat> Allimg = new List<Mat>();
            Mat newimg = new Mat();
            for (int i = 0; i < maxrow - (rows.Min() - 1); i++)
            {
                for (int j = 0; j < maxcol - (cols.Min() - 1); j++)
                {
                    if (j != 0)
                    {
                        Cv2.HConcat(img, smallimg[num], img);
                    }
                    else
                    {
                        img = smallimg[num];
                    }
                    num++;
                }
                Allimg.Add(img);
                if (i == 0)
                {
                    newimg = Allimg[i];
                }
                else
                {
                    Cv2.VConcat(newimg, Allimg[i], newimg);
                }
            }
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < imginfo.Count; i++)
                {
                    sb.Append("fname:" + imginfo[i].fname + "\n");
                    sb.Append("X:" + imginfo[i].X + "\n");
                    sb.Append("Y:" + imginfo[i].Y + "\n");
                    sb.Append("width:" + imginfo[i].width + "\n");
                    sb.Append("height:" + imginfo[i].height + "\n");
                    sb.Append("col:" + imginfo[i].col + "\n");
                    sb.Append("row:" + imginfo[i].row + "\n");
                    sb.Append("imgname:" + imginfo[i].imgname + "\n");
                }
                string localFilePath = saveFileDialog.FileName.ToString();
                string FilePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));
                string fileName = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);
                string[] Array1 = fileName.Split('.');
                Cv2.ImWrite(FilePath + "\\" + Array1[0] + ".tif", newimg, quality);
                StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName);
                streamWriter.Write(sb.ToString());
                streamWriter.Close();
                streamWriter.Dispose();
            }
            DateTime bb = DateTime.Now;
            MessageBox.Show("完成（"+(bb - aa).TotalSeconds + "s)");
        }
        /// <summary>
        /// 装在图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 装载图片(object sender, EventArgs e)
        {
            GC.Collect();
            aa = DateTime.Now;
            quality[0] = (int)ImwriteFlags.JpegQuality;
            quality[1] = 100;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件夹";
            dialog.Filter = "图片和文本(*.tif)|*.tif|All files(*.*)|*.*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filejpg = null;
                List<string> list = new List<string>();
                List<Rect> rects = new List<Rect>();
                List<Rect> rects1 = new List<Rect>();
                List<int> width = new List<int>();
                List<int> height = new List<int>();
                List<Mat> newjpg = new List<Mat>();
                string path = null;
                List<Mat> bjpg = new List<Mat>();
                List<string> tifname = new List<string>();
                List<string> imgnames = new List<string>();
                List<int> X = new List<int>();
                List<int> Y = new List<int>();
                List<Mat> tif = new List<Mat>();
                List<int> cols = new List<int>();
                List<int> rows = new List<int>();
                path = System.IO.Path.GetDirectoryName(dialog.FileName);
                string txtname = System.IO.Path.GetFileNameWithoutExtension(dialog.FileName);
                filejpg = dialog.FileName.ToString();
                if (!File.Exists(path + "\\" + txtname + ".txt"))
                {
                    MessageBox.Show("txt文件不存在!");
                    return;
                }
                System.IO.FileStream txtfs = new System.IO.FileStream(path + "\\" + txtname + ".txt", FileMode.Open);
                StreamReader txtsr = new StreamReader(txtfs, Encoding.UTF8);
                string strLine;
                //读取文件中的一行 
                do
                {
                    strLine = txtsr.ReadLine();
                    list.Add(strLine);
                } while (strLine != null);//判断是否为空，表示到文件最后一行了
                list.Remove(list[list.Count - 1]);
                foreach (string item in list)
                {
                    if (item.Contains("width:"))
                    {
                        string[] Array1 = item.Split(':');
                        width.Add(Convert.ToInt32(Array1[1]));
                    }
                    else if (item.Contains("height:"))
                    {
                        string[] Array1 = item.Split(':');
                        height.Add(Convert.ToInt32(Array1[1]));
                    }
                    else if (item.Contains("fname:"))
                    {
                        string[] Array1 = item.Split(':');
                        tifname.Add(Array1[1] + ":" + Array1[2]);
                    }
                    else if (item.Contains("X:"))
                    {
                        string[] Array1 = item.Split(':');
                        X.Add(Convert.ToInt32(Array1[1]));
                    }
                    else if (item.Contains("Y:"))
                    {
                        string[] Array1 = item.Split(':');
                        Y.Add(Convert.ToInt32(Array1[1]));
                    }
                    else if (item.Contains("col:"))
                    {
                        string[] Array1 = item.Split(':');
                        cols.Add(Convert.ToInt32(Array1[1]));
                    }
                    else if (item.Contains("row:"))
                    {
                        string[] Array1 = item.Split(':');
                        rows.Add(Convert.ToInt32(Array1[1]));
                    }
                    else if (item.Contains("imgname:"))
                    {
                        string[] Array1 = item.Split(':');
                        imgnames.Add(Array1[1]);
                    }
                }
                txtfs.Close();
                txtsr.Close();
                int maxcol = cols.Max();
                int maxrow = rows.Max();
                List<int> XS = new List<int>();
                List<int> YS = new List<int>();
                int xx = 0;
                int yy = 0;
                HashSet<int> xh = new HashSet<int>(width);
                HashSet<int> yh = new HashSet<int>(height);
                List<int> xs = xh.ToList<int>();
                List<int> ys = yh.ToList<int>();
                for (int i = 0; i < maxcol - (cols.Min() - 1); i++)
                {
                    XS.Add(xx);
                    xx += xs[i];
                }
                for (int i = 0; i < maxrow - (rows.Min() - 1); i++)
                {
                    YS.Add(yy);
                    yy += ys[i];
                }
                int a = 0;
                int b = 0;
                for (int j = 0; j < height.Count; j++)
                {
                    rects.Add(new Rect(XS[a], YS[b], width[j], height[j]));
                    if (a == maxcol - (cols.Min() - 1) - 1)
                    {
                        b++;
                        a = 0;
                        continue;
                    }
                    a++;
                }
                List<Mat> newimg = new List<Mat>();
                Mat bigimg = new Mat(filejpg);
                for (int i = 0; i < rects.Count; i++)
                {
                    newimg.Add(new Mat(bigimg, rects[i]));
                    newjpg.Add(newimg[i]);
                }
                for (int i = 0; i < newjpg.Count; i++)
                {
                    if (File.Exists(tifname[i]))
                    {
                        tif.Add(new Mat(tifname[i]));
                        rects1.Add(new Rect(X[i], Y[i], width[i], height[i]));
                        Mat imageROI = new Mat(tif[i], rects1[i]);
                        newjpg[i].CopyTo(imageROI);
                        Cv2.ImWrite(tifname[i], tif[i], quality);
                    }
                    else
                    {
                        tif.Add(new Mat());
                        rects1.Add(new Rect());
                    }
                }
                for (int i = 0; i < imgnames.Count; i++)
                {
                    if (imgnames[i].Equals("null") || imgnames[i] == null)
                    {
                        continue;
                    }
                    for (int j = 0; j < this.MapControl.LayerCount; j++)
                    {
                        ILayer mILayer = this.MapControl.get_Layer(j);
                        if (mILayer.Name == imgnames[i])
                        {
                            this.MapControl.DeleteLayer(j);//删除图层
                            IDataLayer mIDataLayer = (IDataLayer)mILayer;
                            IName mIName = mIDataLayer.DataSourceName;
                            IDataset mIDataset = (IDataset)mIName.Open();
                            IRasterPyramid3 mIRasterPyramid3 = mIDataset as IRasterPyramid3;
                            mIRasterPyramid3.DeletePyramid();//删除缓存
                            break;
                        }
                    }
                    string strFolderPath = System.IO.Path.GetDirectoryName(tifname[i]);
                    string strFileName = System.IO.Path.GetFileName(tifname[i]);
                    IWorkspaceFactory mIWorkspaceFactory = new RasterWorkspaceFactoryClass();
                    IWorkspace mIWorkspace = mIWorkspaceFactory.OpenFromFile(strFolderPath, 0);
                    IRasterWorkspace mIRasterWorkspace = mIWorkspace as IRasterWorkspace;
                    IRasterDataset mIRasterDataset = mIRasterWorkspace.OpenRasterDataset(strFileName);
                    IRasterPyramid mIRasterPyramid = mIRasterDataset as IRasterPyramid;
                    if (mIRasterPyramid != null)
                    {
                        if (mIRasterPyramid.Present == false)
                        {
                            mIRasterPyramid.Create();//创建缓存
                        }
                    }
                    IRaster mIRaster = mIRasterDataset.CreateDefaultRaster();
                    IRasterLayer mIRasterLayer = new RasterLayerClass();
                    mIRasterLayer.CreateFromRaster(mIRaster);
                    ILayer newILayer = mIRasterLayer as ILayer;
                    this.MapControl.AddLayer(newILayer, 0);//加入地图
                }
                DateTime bb = DateTime.Now;
                MessageBox.Show("完成（" + (bb - aa).TotalSeconds + "s)");
            }
        }
        /// <summary>
        /// 清除红框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearIElementButton_Click(object sender, EventArgs e)
        {
            this.mIGraphicsContainer.DeleteAllElements();
            this.MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, this.mIEnvelope);
        }
        /// <summary>
        /// 图像排序
        /// </summary>
        /// <param name="imgs"></param>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        private void paixu(ref List<img> imgs, List<int> rows, List<int> cols)
        {
            List<img> test = new List<img>();
            List<int> row = rows.Distinct().ToList();
            List<int> col = cols.Distinct().ToList();
            row.Sort();
            col.Sort();
            for (int i = 0; i < row.Count; i++)
            {
                for (int j = 0; j < col.Count; j++)
                {
                    for (int z = 0; z < imgs.Count; z++)
                    {
                        if (imgs[z].row == row[i] && imgs[z].col == col[j])
                        {
                            test.Add(imgs[z]);
                        }
                    }
                }
            }
            imgs = test;
        }
    }
}