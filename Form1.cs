using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Text;

namespace asgn5v1
{
    /// <summary>
    /// Summary description for Transformer.
    /// </summary>
    public class Transformer : System.Windows.Forms.Form
	{
        const int MATRIX_SIZE = 4;
        double[,] orgPoint;
        private System.ComponentModel.IContainer components;
		//private bool GetNewData();

		// basic data for Transformer

		int numpts = 0;
		int numlines = 0;
		bool gooddata = false;		
		double[,] vertices;
		double[,] scrnpts; // result of verticies * ctrans
		double[,] ctrans = new double[4,4];  //your main transformation matrix
        private System.Windows.Forms.Timer xticker = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer yticker = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer zticker = new System.Windows.Forms.Timer();
		private System.Windows.Forms.ImageList tbimages;
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ToolBarButton transleftbtn;
		private System.Windows.Forms.ToolBarButton transrightbtn;
		private System.Windows.Forms.ToolBarButton transupbtn;
		private System.Windows.Forms.ToolBarButton transdownbtn;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton scaleupbtn;
		private System.Windows.Forms.ToolBarButton scaledownbtn;
		private System.Windows.Forms.ToolBarButton toolBarButton2;
		private System.Windows.Forms.ToolBarButton rotxby1btn;
		private System.Windows.Forms.ToolBarButton rotyby1btn;
		private System.Windows.Forms.ToolBarButton rotzby1btn;
		private System.Windows.Forms.ToolBarButton toolBarButton3;
		private System.Windows.Forms.ToolBarButton rotxbtn;
		private System.Windows.Forms.ToolBarButton rotybtn;
		private System.Windows.Forms.ToolBarButton rotzbtn;
		private System.Windows.Forms.ToolBarButton toolBarButton4;
		private System.Windows.Forms.ToolBarButton shearrightbtn;
		private System.Windows.Forms.ToolBarButton shearleftbtn;
		private System.Windows.Forms.ToolBarButton toolBarButton5;
		private System.Windows.Forms.ToolBarButton resetbtn;
		private System.Windows.Forms.ToolBarButton exitbtn;
		int[,] lines;

		public Transformer()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			Text = "COMP 4560:  Assignment 5 (200830) | Spencer Pollock | COMP4560 | SET 4B";
			ResizeRedraw = true;
			BackColor = Color.Black;
			MenuItem miNewDat = new MenuItem("New &Data...",
				new EventHandler(MenuNewDataOnClick));
			MenuItem miExit = new MenuItem("E&xit", 
				new EventHandler(MenuFileExitOnClick));
			MenuItem miDash = new MenuItem("-");
			MenuItem miFile = new MenuItem("&File",
				new MenuItem[] {miNewDat, miDash, miExit});
			MenuItem miAbout = new MenuItem("&About",
				new EventHandler(MenuAboutOnClick));
			Menu = new MainMenu(new MenuItem[] {miFile, miAbout});

            xticker.Interval = 10;
            yticker.Interval = 10;
            zticker.Interval = 10;
            xticker.Tick += new System.EventHandler(contXRotate);
            yticker.Tick += new System.EventHandler(contYRotate);
            zticker.Tick += new System.EventHandler(contZRotate);
            
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Transformer));
            this.tbimages = new System.Windows.Forms.ImageList(this.components);
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.transleftbtn = new System.Windows.Forms.ToolBarButton();
            this.transrightbtn = new System.Windows.Forms.ToolBarButton();
            this.transupbtn = new System.Windows.Forms.ToolBarButton();
            this.transdownbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.scaleupbtn = new System.Windows.Forms.ToolBarButton();
            this.scaledownbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.rotxby1btn = new System.Windows.Forms.ToolBarButton();
            this.rotyby1btn = new System.Windows.Forms.ToolBarButton();
            this.rotzby1btn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
            this.rotxbtn = new System.Windows.Forms.ToolBarButton();
            this.rotybtn = new System.Windows.Forms.ToolBarButton();
            this.rotzbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
            this.shearrightbtn = new System.Windows.Forms.ToolBarButton();
            this.shearleftbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
            this.resetbtn = new System.Windows.Forms.ToolBarButton();
            this.exitbtn = new System.Windows.Forms.ToolBarButton();
            this.SuspendLayout();
            // 
            // tbimages
            // 
            this.tbimages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tbimages.ImageStream")));
            this.tbimages.TransparentColor = System.Drawing.Color.Transparent;
            this.tbimages.Images.SetKeyName(0, "");
            this.tbimages.Images.SetKeyName(1, "");
            this.tbimages.Images.SetKeyName(2, "");
            this.tbimages.Images.SetKeyName(3, "");
            this.tbimages.Images.SetKeyName(4, "");
            this.tbimages.Images.SetKeyName(5, "");
            this.tbimages.Images.SetKeyName(6, "");
            this.tbimages.Images.SetKeyName(7, "");
            this.tbimages.Images.SetKeyName(8, "");
            this.tbimages.Images.SetKeyName(9, "");
            this.tbimages.Images.SetKeyName(10, "");
            this.tbimages.Images.SetKeyName(11, "");
            this.tbimages.Images.SetKeyName(12, "");
            this.tbimages.Images.SetKeyName(13, "");
            this.tbimages.Images.SetKeyName(14, "");
            this.tbimages.Images.SetKeyName(15, "");
            // 
            // toolBar1
            // 
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.transleftbtn,
            this.transrightbtn,
            this.transupbtn,
            this.transdownbtn,
            this.toolBarButton1,
            this.scaleupbtn,
            this.scaledownbtn,
            this.toolBarButton2,
            this.rotxby1btn,
            this.rotyby1btn,
            this.rotzby1btn,
            this.toolBarButton3,
            this.rotxbtn,
            this.rotybtn,
            this.rotzbtn,
            this.toolBarButton4,
            this.shearrightbtn,
            this.shearleftbtn,
            this.toolBarButton5,
            this.resetbtn,
            this.exitbtn});
            this.toolBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.tbimages;
            this.toolBar1.Location = new System.Drawing.Point(484, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(24, 306);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // transleftbtn
            // 
            this.transleftbtn.ImageIndex = 1;
            this.transleftbtn.Name = "transleftbtn";
            this.transleftbtn.ToolTipText = "translate left";
            // 
            // transrightbtn
            // 
            this.transrightbtn.ImageIndex = 0;
            this.transrightbtn.Name = "transrightbtn";
            this.transrightbtn.ToolTipText = "translate right";
            // 
            // transupbtn
            // 
            this.transupbtn.ImageIndex = 2;
            this.transupbtn.Name = "transupbtn";
            this.transupbtn.ToolTipText = "translate up";
            // 
            // transdownbtn
            // 
            this.transdownbtn.ImageIndex = 3;
            this.transdownbtn.Name = "transdownbtn";
            this.transdownbtn.ToolTipText = "translate down";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // scaleupbtn
            // 
            this.scaleupbtn.ImageIndex = 4;
            this.scaleupbtn.Name = "scaleupbtn";
            this.scaleupbtn.ToolTipText = "scale up";
            // 
            // scaledownbtn
            // 
            this.scaledownbtn.ImageIndex = 5;
            this.scaledownbtn.Name = "scaledownbtn";
            this.scaledownbtn.ToolTipText = "scale down";
            // 
            // toolBarButton2
            // 
            this.toolBarButton2.Name = "toolBarButton2";
            this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // rotxby1btn
            // 
            this.rotxby1btn.ImageIndex = 6;
            this.rotxby1btn.Name = "rotxby1btn";
            this.rotxby1btn.ToolTipText = "rotate about x by 1";
            // 
            // rotyby1btn
            // 
            this.rotyby1btn.ImageIndex = 7;
            this.rotyby1btn.Name = "rotyby1btn";
            this.rotyby1btn.ToolTipText = "rotate about y by 1";
            // 
            // rotzby1btn
            // 
            this.rotzby1btn.ImageIndex = 8;
            this.rotzby1btn.Name = "rotzby1btn";
            this.rotzby1btn.ToolTipText = "rotate about z by 1";
            // 
            // toolBarButton3
            // 
            this.toolBarButton3.Name = "toolBarButton3";
            this.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // rotxbtn
            // 
            this.rotxbtn.ImageIndex = 9;
            this.rotxbtn.Name = "rotxbtn";
            this.rotxbtn.ToolTipText = "rotate about x continuously";
            // 
            // rotybtn
            // 
            this.rotybtn.ImageIndex = 10;
            this.rotybtn.Name = "rotybtn";
            this.rotybtn.ToolTipText = "rotate about y continuously";
            // 
            // rotzbtn
            // 
            this.rotzbtn.ImageIndex = 11;
            this.rotzbtn.Name = "rotzbtn";
            this.rotzbtn.ToolTipText = "rotate about z continuously";
            // 
            // toolBarButton4
            // 
            this.toolBarButton4.Name = "toolBarButton4";
            this.toolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // shearrightbtn
            // 
            this.shearrightbtn.ImageIndex = 12;
            this.shearrightbtn.Name = "shearrightbtn";
            this.shearrightbtn.ToolTipText = "shear right";
            // 
            // shearleftbtn
            // 
            this.shearleftbtn.ImageIndex = 13;
            this.shearleftbtn.Name = "shearleftbtn";
            this.shearleftbtn.ToolTipText = "shear left";
            // 
            // toolBarButton5
            // 
            this.toolBarButton5.Name = "toolBarButton5";
            this.toolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // resetbtn
            // 
            this.resetbtn.ImageIndex = 14;
            this.resetbtn.Name = "resetbtn";
            this.resetbtn.ToolTipText = "restore the initial image";
            // 
            // exitbtn
            // 
            this.exitbtn.ImageIndex = 15;
            this.exitbtn.Name = "exitbtn";
            this.exitbtn.ToolTipText = "exit the program";
            // 
            // Transformer
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(508, 306);
            this.Controls.Add(this.toolBar1);
            this.Name = "Transformer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Transformer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Transformer());
		}

		protected override void OnPaint(PaintEventArgs pea)
		{
			Graphics grfx = pea.Graphics;
            Pen pen = new Pen(Color.White, 3);
			double temp;
			int k;
            double firstPoint = 0;
            if (gooddata)
            {
                //create the screen coordinates:
                // scrnpts = vertices * ctrans

                for (int i = 0; i < numpts; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        temp = 0.0d;
                        for (k = 0; k < 4; k++)
                        {
                            temp += vertices[i, k] * ctrans[k, j];
                        }
                        scrnpts[i, j] = temp;
                    }
                }

                //now draw the lines

                for (int i = 0; i < numlines; i++)
                {
                    grfx.DrawLine(pen, (int)scrnpts[lines[i, 0], 0], (int)scrnpts[lines[i, 0], 1],
                        (int)scrnpts[lines[i, 1], 0], (int)scrnpts[lines[i, 1], 1]);
                }


            } // end of gooddata block	
		} // end of OnPaint

		void MenuNewDataOnClick(object obj, EventArgs ea)
		{
			//MessageBox.Show("New Data item clicked.");
			gooddata = GetNewData();
			RestoreInitialImage();			
		}

		void MenuFileExitOnClick(object obj, EventArgs ea)
		{
			Close();
		}

		void MenuAboutOnClick(object obj, EventArgs ea)
		{
			AboutDialogBox dlg = new AboutDialogBox();
			dlg.ShowDialog();
		}

		void RestoreInitialImage()
		{
			Invalidate();
		} // end of RestoreInitialImage

		bool GetNewData()
		{
			string strinputfile,text;
			ArrayList coorddata = new ArrayList();
			ArrayList linesdata = new ArrayList();
			OpenFileDialog opendlg = new OpenFileDialog();
			opendlg.Title = "Choose File with Coordinates of Vertices";
			if (opendlg.ShowDialog() == DialogResult.OK)
			{
				strinputfile=opendlg.FileName;				
				FileInfo coordfile = new FileInfo(strinputfile);
				StreamReader reader = coordfile.OpenText();
				do
				{
					text = reader.ReadLine();
					if (text != null) coorddata.Add(text);
				} while (text != null);
				reader.Close();
				DecodeCoords(coorddata);
			}
			else
			{
				MessageBox.Show("***Failed to Open Coordinates File***");
				return false;
			}
            
			opendlg.Title = "Choose File with Data Specifying Lines";
			if (opendlg.ShowDialog() == DialogResult.OK)
			{
				strinputfile=opendlg.FileName;
				FileInfo linesfile = new FileInfo(strinputfile);
				StreamReader reader = linesfile.OpenText();
				do
				{
					text = reader.ReadLine();
					if (text != null) linesdata.Add(text);
				} while (text != null);
				reader.Close();
				DecodeLines(linesdata);
			}
			else
			{
				MessageBox.Show("***Failed to Open Line Data File***");
				return false;
			}
			scrnpts = new double[numpts,4];
			setIdentity(ctrans,4,4);  //initialize transformation matrix to identity
            double imgX = sizeofimgX(vertices);
            double sx = (ClientSize.Height / imgX) / 2, sy = (ClientSize.Height / imgX) / 2, sz = (ClientSize.Height / imgX) / 2,
                mx = (ClientSize.Width / 2), my = (ClientSize.Height / 2), mz = (ClientSize.Height / 2);

            double[,] scaletest = createScaleMatrix(sx, sy, sz);
            double[,] movetest = createMoveMatrix(mx, my, mz);
            double[,] centertest = createMoveMatrix(-vertices[0, 0], -vertices[0, 1], -vertices[0,2]);
            double[,] fliptest = createFlipMatrix(); // flip over y

            // testing
            ctrans = matrixMultiply(ctrans, centertest);
            ctrans = matrixMultiply(ctrans, fliptest);
            ctrans = matrixMultiply(ctrans, scaletest);
            ctrans = matrixMultiply(ctrans, movetest);

            orgPoint = ctrans;

            stopAllTickers();

            return true;
		} // end of GetNewData

		void DecodeCoords(ArrayList coorddata)
		{
			//this may allocate slightly more rows that necessary
			vertices = new double[coorddata.Count,4];
			numpts = 0;
			string [] text = null;
			for (int i = 0; i < coorddata.Count; i++)
			{
				text = coorddata[i].ToString().Split(' ',',');
				vertices[numpts,0]=double.Parse(text[0]);
				if (vertices[numpts,0] < 0.0d) break;
				vertices[numpts,1]=double.Parse(text[1]);
				vertices[numpts,2]=double.Parse(text[2]);
				vertices[numpts,3] = 1.0d;
				numpts++;						
			}
			
		}// end of DecodeCoords

		void DecodeLines(ArrayList linesdata)
		{
			//this may allocate slightly more rows that necessary
			lines = new int[linesdata.Count,2];
			numlines = 0;
			string [] text = null;
			for (int i = 0; i < linesdata.Count; i++)
			{
				text = linesdata[i].ToString().Split(' ',',');
				lines[numlines,0]=int.Parse(text[0]);
				if (lines[numlines,0] < 0) break;
				lines[numlines,1]=int.Parse(text[1]);
				numlines++;						
			}
		} // end of DecodeLines

        // setup c trans, where we start from
		void setIdentity(double[,] A, int nrow, int ncol)
		{
            // flip over the x
			for (int i = 0; i < nrow;i++) 
			{
				for (int j = 0; j < ncol; j++) A[i,j] = 0.0d;
				A[i,i] = 1.0d;
			}
		}// end of setIdentity

        /// <summary>
        /// Gets the size of the image y.
        /// </summary>
        /// <param name="A">Verticies matrix</param>
        /// <returns>Size of the image.</returns>
        double sizeofimgX(double[,] A)
        {
            double size, min = double.MaxValue, max = 0;
            for (int x = 0; x < A.GetLength(0); x++)
            {
                if (A[x, 0] < min)
                {
                    min = A[x, 0];
                }
                else if (A[x, 0] > max)
                {
                    max = A[x, 0];
                }
            }
            size = Math.Abs(max) - Math.Abs(min);
            return size;
        }

        /// <summary>
        /// Create a basic matrix.
        /// </summary>
        /// <returns>Basic matrix</returns>
        double[,] basicMatrix()
        {
            double[,] mout = new double[MATRIX_SIZE, MATRIX_SIZE];
            for (int x = 0; x < MATRIX_SIZE; x++)
            {
                for (int y = 0; y < MATRIX_SIZE; y++) { mout[x, y] = 0; }
                mout[x, x] = 1;
            }
            return mout;
        }

        /// <summary>
        /// Create scaling matrix.
        /// </summary>
        /// <param name="xSc">Scale x by</param>
        /// <param name="ySc">Scale y by</param>
        /// <param name="zSc">Scale z by</param>
        /// <returns>n*n matrix</returns>
        double[,] createScaleMatrix(double xSc, double ySc, double zSc)
        {
            double[,] mout = basicMatrix();
            mout[0,0] = xSc;
            mout[1,1] = ySc;
            mout[2,2] = zSc;
            return mout;
        }

        /// <summary>
        /// Create a moving matrix.
        /// </summary>
        /// <param name="mvx">Move x by</param>
        /// <param name="mvy">Move y by</param>
        /// <param name="mvz">Move z by</param>
        /// <returns>n*n matrix</returns>
        double[,] createMoveMatrix(double mvx, double mvy, double mvz)
        {
            double[,] mout = basicMatrix();
            mout[3,0] = mvx;
            mout[3,1] = mvy;
            mout[3,2] = mvz;
            return mout;
        }

        /// <summary>
        /// Create a flip matrix.
        /// </summary>
        /// <param name="flipxyz">1 for x, 2 for y, 3 for z. Default y(2)</param>
        /// <returns>Returns a new flip matrix</returns>
        double[,] createFlipMatrix(double flipxyz = 2)
        {
            double[,] mout = basicMatrix();
            if(flipxyz == 1)
            {
                mout[0,0] *= -1;
            }else if(flipxyz == 2)
            {
                mout[1,1] *= -1;
            }
            else
            {
                mout[2,2] *= -1;
            }
            return mout;
        }

		/// <summary>
		/// Ccws X rot.
		/// </summary>
		/// <returns>The X rotation</returns>
		/// <param name="deg">Degree</param>
        double[,] ccwXRot(double deg)
        {
            double[,] mout = basicMatrix();
            mout[1,1] = Math.Cos(deg);
	        mout[1,2] = -Math.Sin(deg);
	        mout[2,1] = Math.Sin(deg);
	        mout[2,2] = Math.Cos(deg);

            return mout;
        }

		/// <summary>
		/// Ccws Y rotation
		/// </summary>
		/// <returns>The Y rotation</returns>
		/// <param name="deg">Degtree</param>
        double[,] ccwYRot(double deg)
        {
            double[,] mout = basicMatrix();
            mout[0,0] = Math.Cos(deg);
	        mout[0,2] = Math.Sin(deg);
	        mout[2,0] = -Math.Sin(deg);
	        mout[2,2] = Math.Cos(deg);

            return mout;
        }

		/// <summary>
		/// Ccws Z rotation
		/// </summary>
		/// <returns>The Z rottation</returns>
		/// <param name="deg">Degree</param>
        double[,] ccwZRot(double deg)
        {
            double[,] mout = basicMatrix();
            mout[0,0] = Math.Cos(deg);
	        mout[0,1] = -Math.Sin(deg);
	        mout[1,0] = Math.Sin(deg);
	        mout[1,1] = Math.Cos(deg);

            return mout;
        }

        /// <summary>
        /// Rotate about x.
        /// </summary>
        void xRotate()
        {
            double[,] centertest = createMoveMatrix(-scrnpts[0, 0], -scrnpts[0, 1], -scrnpts[0, 2]);
            double[,] ocentertest = createMoveMatrix(scrnpts[0, 0], scrnpts[0, 1], scrnpts[0, 2]);
            double[,] rot = ccwXRot(0.05);

            ctrans = matrixMultiply(ctrans, centertest);
            ctrans = matrixMultiply(ctrans, rot);
            ctrans = matrixMultiply(ctrans, ocentertest);
            Refresh();
        }

        /// <summary>
        /// Rotate about y.
        /// </summary>
        void yRotate()
        {
            double[,] centertest = createMoveMatrix(-scrnpts[0, 0], -scrnpts[0, 1], -scrnpts[0, 2]);
            double[,] ocentertest = createMoveMatrix(scrnpts[0, 0], scrnpts[0, 1], scrnpts[0, 2]);
            double[,] rot = ccwYRot(0.05);

            ctrans = matrixMultiply(ctrans, centertest);
            ctrans = matrixMultiply(ctrans, rot);
            ctrans = matrixMultiply(ctrans, ocentertest);
            Refresh();
        }

        /// <summary>
        /// Rotate about z.
        /// </summary>
        void zRotate()
        {
            double[,] centertest = createMoveMatrix(-scrnpts[0, 0], -scrnpts[0, 1], -scrnpts[0, 2]);
            double[,] ocentertest = createMoveMatrix(scrnpts[0, 0], scrnpts[0, 1], scrnpts[0, 2]);
            double[,] rot = ccwZRot(0.05);

            ctrans = matrixMultiply(ctrans, centertest);
            ctrans = matrixMultiply(ctrans, rot);
            ctrans = matrixMultiply(ctrans, ocentertest);
            Refresh();
        }

        /// <summary>
        /// Function for continuous rotation, calls xRotate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void contXRotate(Object sender, EventArgs e)
        {
            xRotate();
        }

        /// <summary>
        /// Function for continuous rotation, calls yRotate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void contYRotate(Object sender, EventArgs e)
        {
            yRotate();
        }

        /// <summary>
        /// Function for continuous rotation, calls zRotate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void contZRotate(Object sender, EventArgs e)
        {
            zRotate();
        }

		/// <summary>
		/// Shears the character in the x direction with respect to y.
		/// </summary>
		/// <returns>Shear matrix.</returns>
		/// <param name="perc">Percent of shearing, given as 0.x.</param>
		double[,] createShearMatrix(double perc){
			double[,] shear = basicMatrix ();
			shear [1, 0] = perc;
			return shear;
		}

        /// <summary>
        /// Multiplies matrix
        /// </summary>
        /// <param name="mout">Output</param>
        /// <param name="m1">Left matrix</param>
        /// <param name="m2">Right matrix</param>
        void matrixMultiply(double[,] mout, double[,] m1, double[,] m2)
        {
            double temp = 0;
            for(int x = 0; x < 4; x++)
            {
                for(int y = 0; y < 4; y++)
                {
                    temp = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        temp += m1[x, i] * m2[i, y];
                    }
                    mout[x, y] = temp;
                }
            }
        }

        /// <summary>
        /// Multiplies two matricies.
        /// </summary>
        /// <param name="m1">Matrix 1</param>
        /// <param name="m2">Matrix 2</param>
        /// <returns>Product</returns>
        double[,] matrixMultiply(double[,] m1, double[,] m2)
        {
            double temp = 0;
            double[,] mout = new double[4,4];
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    temp = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        temp += m1[x, i] * m2[i, y];
                    }
                    mout[x, y] = temp;
                }
            }
            return mout;
        }

        private void stopAllTickers()
        {
            xticker.Stop();
            yticker.Stop();
            zticker.Stop();
        }

		private void Transformer_Load(object sender, System.EventArgs e)
		{
			
		}

		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
            // Work with ctrans each time
			if (e.Button == transleftbtn)
			{
                stopAllTickers();
                // move left
                double[,] move = createMoveMatrix(-75, 0, 0);
                matrixMultiply(ctrans, ctrans, move);
				Refresh();
			}
			if (e.Button == transrightbtn) 
			{
                stopAllTickers();
                // move right
                double[,] move = createMoveMatrix(75, 0, 0);
                matrixMultiply(ctrans, ctrans, move);
                Refresh();
			}
			if (e.Button == transupbtn)
			{
                stopAllTickers();
                // move up
                double[,] move = createMoveMatrix(0, -35, 0);
                matrixMultiply(ctrans, ctrans, move);
                Refresh();
			}
			
			if(e.Button == transdownbtn)
			{
                stopAllTickers();
                // move down
                double[,] move = createMoveMatrix(0, 35, 0);
                matrixMultiply(ctrans, ctrans, move);
                Refresh();
			}
			if (e.Button == scaleupbtn) 
			{
                stopAllTickers();
                // scale up by 10% (110%)
                // gotta move around son
                // Not scaling from the center position
                double[,] scale = createScaleMatrix(1.1, 1.1, 1.1);
                double[,] centertest = createMoveMatrix(-scrnpts[0, 0], -scrnpts[0, 1], -scrnpts[0, 2]);
                double[,] ocentertest = createMoveMatrix(scrnpts[0, 0], scrnpts[0, 1], scrnpts[0, 2]);
                double[,] test = createMoveMatrix(0, 0, 0); // basic matrix

                // testing
                ctrans = matrixMultiply(ctrans, centertest);
                ctrans = matrixMultiply(ctrans, scale);
                ctrans = matrixMultiply(ctrans, ocentertest);
                Refresh();
			}
			if (e.Button == scaledownbtn) 
			{
                stopAllTickers();
                double[,] scale = createScaleMatrix(0.9, 0.9, 0.9);
                double[,] centertest = createMoveMatrix(-scrnpts[0, 0], -scrnpts[0, 1], -scrnpts[0, 2]);
                double[,] ocentertest = createMoveMatrix(scrnpts[0, 0], scrnpts[0, 1], scrnpts[0, 2]);
                double[,] test = createMoveMatrix(0, 0, 0); // basic matrix

                // testing
                ctrans = matrixMultiply(ctrans, centertest);
                ctrans = matrixMultiply(ctrans, scale);
                ctrans = matrixMultiply(ctrans, ocentertest);
                Refresh();
			}
			if (e.Button == rotxby1btn) 
			{
                stopAllTickers();
                xRotate();
			}
			if (e.Button == rotyby1btn) 
			{
                stopAllTickers();
                yRotate();
			}
			if (e.Button == rotzby1btn) 
			{
                stopAllTickers();
                zRotate();
			}

			if (e.Button == rotxbtn) 
			{
				// timer
                stopAllTickers();
                xticker.Start();
			}
			if (e.Button == rotybtn) 
			{
                // timer
                stopAllTickers();
                yticker.Start();
            }

            if (e.Button == rotzbtn) 
			{
                // timer
                stopAllTickers();
                zticker.Start();
            }

            if (e.Button == shearleftbtn)
			{
                stopAllTickers();
				double[,] move = createMoveMatrix (-ctrans [3, 0], -ctrans [3, 1], -ctrans [3, 2]);
				double[,] shear = createShearMatrix (0.1); // -10%
				double[,] omove = createMoveMatrix (ctrans [3, 0], ctrans [3, 1], ctrans [3, 2]);
				double[,] tnet = basicMatrix ();

                ctrans = matrixMultiply(ctrans, move);
                ctrans = matrixMultiply(ctrans, shear);
                ctrans = matrixMultiply(ctrans, omove);
				Refresh();
			}

			if (e.Button == shearrightbtn) 
			{
                stopAllTickers();
				double[,] move = createMoveMatrix (-ctrans [3, 0], -ctrans [3, 1], -ctrans [3, 2]);
				double[,] shear = createShearMatrix (-0.1); // -10%
				double[,] omove = createMoveMatrix (ctrans [3, 0], ctrans [3, 1], ctrans [3, 2]);
				double[,] tnet = basicMatrix ();

                ctrans = matrixMultiply(ctrans, move);
                ctrans = matrixMultiply(ctrans, shear);
                ctrans = matrixMultiply(ctrans, omove);
				Refresh ();
			}

			if (e.Button == resetbtn)
			{
                stopAllTickers();
                ctrans = orgPoint;
				RestoreInitialImage();
			}

			if(e.Button == exitbtn) 
			{
				Close();
			}

		}
	}
}
