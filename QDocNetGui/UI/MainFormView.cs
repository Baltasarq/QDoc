// QDocNet - (c) 2018 Baltasar MIT License <baltasarq@gmail.com>

namespace QDocNetGui.UI {
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    using QDocNetLib;

    public class MainFormView: Form {
        public MainFormView()
        {
            this.Build();
        }

        Bitmap CreateIcon(char c, int width, int height)
        {
            var msg = Convert.ToString( c );
            var toret = new Bitmap( width, height );
            var graphics = Graphics.FromImage( toret );
            var font = new Font( this.BaseFont.FontFamily, 8, FontStyle.Bold );
            var brush = new SolidBrush( Color.White );

            graphics.Clear( Color.Navy );
            graphics.DrawString( msg, font, brush, 0, 0 );

            return toret;
        }

        void BuildIcons()
        {
            try {
                this.AppIcon = new Bitmap(
                    System.Reflection.Assembly.GetEntryAssembly( ).
                    GetManifestResourceStream( "QDocNetGui.Res.app_icon.png" ) );

                this.openIcon = new Bitmap(
                    System.Reflection.Assembly.GetEntryAssembly( ).
                    GetManifestResourceStream( "QDocNetGui.Res.open_icon.png" ) );

                this.saveIcon = new Bitmap(
                    System.Reflection.Assembly.GetEntryAssembly( ).
                    GetManifestResourceStream( "QDocNetGui.Res.save_icon.png" ) );

                this.aboutIcon = new Bitmap(
                    System.Reflection.Assembly.GetEntryAssembly( ).
                    GetManifestResourceStream( "QDocNetGui.Res.about_icon.png" ) );

            } catch(Exception e)
            {
                Debug.WriteLine( "ERROR loading icons: " + e.Message );

                if ( this.AppIcon == null ) {
                    this.AppIcon = this.CreateIcon( 'D', 32, 32 );
                    Debug.WriteLine( "App icon created instead of loaded" );
                }

                if ( this.openIcon == null ) {
                    this.openIcon = this.CreateIcon( 'O', 32, 32 );
                    Debug.WriteLine( "Open icon created instead of loaded" );
                }

                if ( this.saveIcon == null ) {
                    this.saveIcon = this.CreateIcon( 'S', 32, 32 );
                    Debug.WriteLine( "Save icon created instead of loaded" );
                }

                if ( this.aboutIcon == null ) {
                    this.aboutIcon = this.CreateIcon( '?', 32, 32 );
                    Debug.WriteLine( "About icon created instead of loaded" );
                }
            }

            return;
        }
        
        SplitContainer BuildSplitContainer()
        {
            var toret = new SplitContainer { Dock = DockStyle.Fill };
            
            toret.Orientation = Orientation.Vertical;
            
            return toret;
        }
        
        TreeView BuildTreeView()
        {
            return new TreeView { Dock = DockStyle.Fill };
        }
        
        TextBox BuildDocViewer()
        {
            // Documentation description
            return new TextBox {
                ReadOnly = true,
                Multiline = true,
                Dock = DockStyle.Fill
            };
        }

        ToolBar BuildToolbar()
        {
            var toret = new ToolBar();

            // Create image list
            var imgList = new ImageList{ ImageSize = new Size( 24, 24 ) };
            imgList.Images.AddRange( new Image[] {
                this.openIcon,
                this.saveIcon,
                this.aboutIcon,
            });

            // Buttons
            this.TbbOpen = new ToolBarButton        { ImageIndex = 0 };
            this.TbbSave = new ToolBarButton        { ImageIndex = 1 };
            this.TbbAbout = new ToolBarButton       { ImageIndex = 2 };

            // Polishing
            toret.ShowToolTips = true;
            toret.ImageList = imgList;
            toret.Dock = DockStyle.Top;
            toret.BorderStyle = BorderStyle.None;
            toret.Appearance = ToolBarAppearance.Flat;
            toret.Buttons.AddRange( new ToolBarButton[] {
                this.TbbOpen, this.TbbSave, this.TbbAbout
            });

            return toret;
        }

        void Build()
        {
            this.BaseFont = new Font(   SystemFonts.DefaultFont.FontFamily,
                                     12,
                                     FontStyle.Regular );
            
            this.BuildIcons();
            this.PnlAbout = this.BuildAboutPanel();
            this.TbBar = this.BuildToolbar();
            this.SplitContainer =  this.BuildSplitContainer();
            this.TreeView = BuildTreeView();
            this.TbDoc = BuildDocViewer();

            this.SplitContainer.Panel1.Controls.Add( this.TreeView );
            this.SplitContainer.Panel2.Controls.Add( this.TbDoc );
            this.SplitContainer.FixedPanel = FixedPanel.Panel1; 

            this.Controls.Add( this.SplitContainer );
            this.Controls.Add( this.TbBar );
            this.Controls.Add( this.PnlAbout );
            
            this.MinimumSize = new Size( 620, 460 );
            this.Size = this.MinimumSize;
            this.SplitContainer.SplitterDistance = 200;
            this.Icon = Icon.FromHandle( this.AppIcon.GetHicon() );
        }

        private Panel BuildAboutPanel()
        {
            // Sizes
            Graphics grf = this.CreateGraphics();
            SizeF fontSize = grf.MeasureString( "M", this.BaseFont );
            int charSize = (int) fontSize.Width + 5;

            // Panel for about info
            var toret = new Panel() {
                Dock = DockStyle.Bottom,
                BackColor = Color.LightYellow,
                ForeColor = Color.Black
            };

            toret.SuspendLayout();

            this.LblAbout = new Label {
                Text = LibInfo.Header + ", " + LibInfo.Author,
                Dock = DockStyle.Left,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true,
                Font = new Font( this.BaseFont, FontStyle.Bold )
            };

            var btCloseAboutPanel = new Button() {
                Text = "X",
                Dock = DockStyle.Right,
                Width = charSize * 5,
                FlatStyle = FlatStyle.Flat,
                Font = new Font( this.BaseFont, FontStyle.Bold )
            };

            btCloseAboutPanel.FlatAppearance.BorderSize = 0;
            btCloseAboutPanel.Click += (o, evt) => toret.Hide();
            toret.Controls.Add( LblAbout );
            toret.Controls.Add( btCloseAboutPanel );
            toret.Hide();
            toret.MinimumSize = new Size( this.Width, this.LblAbout.Height + 5 );
            toret.MaximumSize = new Size( Int32.MaxValue, this.LblAbout.Height + 5 );

            toret.ResumeLayout( false );

            return toret;
        }

        public Panel PnlAbout {
            get; private set;
        }

        public Bitmap AppIcon {
            get; set;   
        }

        public TreeView TreeView {
            get; private set;
        }

        public SplitContainer SplitContainer {
            get; private set;
        }

        public Font BaseFont {
            get; private set;
        }

        public ToolBar TbBar {
            get; private set;
        }

        public ToolBarButton TbbOpen {
            get; private set;
        }

        public ToolBarButton TbbSave {
            get; private set;
        }

        public ToolBarButton TbbAbout {
            get; private set;
        }

        public Label LblAbout {
            get; private set;
        }
        
        public TextBox TbDoc {
            get; private set;
        }

        Bitmap openIcon;
        Bitmap saveIcon;
        Bitmap aboutIcon;
    }
}
