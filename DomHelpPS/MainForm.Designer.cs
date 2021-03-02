namespace DomHelpPS
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Menu = new System.Windows.Forms.MenuStrip();
            this.RectangleSelectButton = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearIElementButton = new System.Windows.Forms.ToolStripMenuItem();
            this.FullExtentButton = new System.Windows.Forms.ToolStripMenuItem();
            this.CreatePolygenShpFromRasterLayerButton = new System.Windows.Forms.ToolStripMenuItem();
            this.装在图片ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.TOCControl = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.LicenseControl = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.MapControl = new ESRI.ArcGIS.Controls.AxMapControl();
            this.Menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TOCControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LicenseControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapControl)).BeginInit();
            this.SuspendLayout();
            // 
            // Menu
            // 
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RectangleSelectButton,
            this.ClearIElementButton,
            this.FullExtentButton,
            this.CreatePolygenShpFromRasterLayerButton,
            this.装在图片ToolStripMenuItem});
            this.Menu.Location = new System.Drawing.Point(0, 0);
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(978, 25);
            this.Menu.TabIndex = 0;
            // 
            // RectangleSelectButton
            // 
            this.RectangleSelectButton.Name = "RectangleSelectButton";
            this.RectangleSelectButton.Size = new System.Drawing.Size(68, 21);
            this.RectangleSelectButton.Text = "框选范围";
            this.RectangleSelectButton.Click += new System.EventHandler(this.RectangleSelectButton_Click);
            // 
            // ClearIElementButton
            // 
            this.ClearIElementButton.Name = "ClearIElementButton";
            this.ClearIElementButton.Size = new System.Drawing.Size(68, 21);
            this.ClearIElementButton.Text = "清除红框";
            this.ClearIElementButton.Click += new System.EventHandler(this.ClearIElementButton_Click);
            // 
            // FullExtentButton
            // 
            this.FullExtentButton.Name = "FullExtentButton";
            this.FullExtentButton.Size = new System.Drawing.Size(44, 21);
            this.FullExtentButton.Text = "全图";
            this.FullExtentButton.Click += new System.EventHandler(this.FullExtentButton_Click);
            // 
            // CreatePolygenShpFromRasterLayerButton
            // 
            this.CreatePolygenShpFromRasterLayerButton.Name = "CreatePolygenShpFromRasterLayerButton";
            this.CreatePolygenShpFromRasterLayerButton.Size = new System.Drawing.Size(115, 21);
            this.CreatePolygenShpFromRasterLayerButton.Text = "创建栅格范围SHP";
            this.CreatePolygenShpFromRasterLayerButton.Visible = false;
            this.CreatePolygenShpFromRasterLayerButton.Click += new System.EventHandler(this.CreatePolygenShpFromRasterLayerButton_Click);
            // 
            // 装在图片ToolStripMenuItem
            // 
            this.装在图片ToolStripMenuItem.Name = "装在图片ToolStripMenuItem";
            this.装在图片ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.装在图片ToolStripMenuItem.Text = "装载图片";
            this.装在图片ToolStripMenuItem.Click += new System.EventHandler(this.装载图片);
            // 
            // MainSplitContainer
            // 
            this.MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplitContainer.Location = new System.Drawing.Point(0, 25);
            this.MainSplitContainer.Name = "MainSplitContainer";
            // 
            // MainSplitContainer.Panel1
            // 
            this.MainSplitContainer.Panel1.Controls.Add(this.TOCControl);
            // 
            // MainSplitContainer.Panel2
            // 
            this.MainSplitContainer.Panel2.Controls.Add(this.LicenseControl);
            this.MainSplitContainer.Panel2.Controls.Add(this.MapControl);
            this.MainSplitContainer.Size = new System.Drawing.Size(978, 594);
            this.MainSplitContainer.SplitterDistance = 325;
            this.MainSplitContainer.TabIndex = 1;
            // 
            // TOCControl
            // 
            this.TOCControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TOCControl.Location = new System.Drawing.Point(0, 0);
            this.TOCControl.Name = "TOCControl";
            this.TOCControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("TOCControl.OcxState")));
            this.TOCControl.Size = new System.Drawing.Size(325, 594);
            this.TOCControl.TabIndex = 0;
            // 
            // LicenseControl
            // 
            this.LicenseControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.LicenseControl.Enabled = true;
            this.LicenseControl.Location = new System.Drawing.Point(0, 0);
            this.LicenseControl.Name = "LicenseControl";
            this.LicenseControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("LicenseControl.OcxState")));
            this.LicenseControl.Size = new System.Drawing.Size(32, 32);
            this.LicenseControl.TabIndex = 1;
            // 
            // MapControl
            // 
            this.MapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MapControl.Location = new System.Drawing.Point(0, 0);
            this.MapControl.Name = "MapControl";
            this.MapControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("MapControl.OcxState")));
            this.MapControl.Size = new System.Drawing.Size(649, 594);
            this.MapControl.TabIndex = 0;
            this.MapControl.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.MapControl_OnMouseDown);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 619);
            this.Controls.Add(this.MainSplitContainer);
            this.Controls.Add(this.Menu);
            this.MainMenuStrip = this.Menu;
            this.Name = "MainForm";
            this.Text = "影像PS辅助系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
            this.MainSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TOCControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LicenseControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip Menu;
        private System.Windows.Forms.SplitContainer MainSplitContainer;
        private ESRI.ArcGIS.Controls.AxTOCControl TOCControl;
        private ESRI.ArcGIS.Controls.AxLicenseControl LicenseControl;
        private ESRI.ArcGIS.Controls.AxMapControl MapControl;
        private System.Windows.Forms.ToolStripMenuItem RectangleSelectButton;
        private System.Windows.Forms.ToolStripMenuItem CreatePolygenShpFromRasterLayerButton;
        private System.Windows.Forms.ToolStripMenuItem FullExtentButton;
        private System.Windows.Forms.ToolStripMenuItem ClearIElementButton;
        private System.Windows.Forms.ToolStripMenuItem 装在图片ToolStripMenuItem;
    }
}

