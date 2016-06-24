namespace PawcioreX
{
    partial class mainForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.tsBtnNewFile = new System.Windows.Forms.ToolStripButton();
            this.tsBtnOpenFile = new System.Windows.Forms.ToolStripButton();
            this.tsBtnSaveFile = new System.Windows.Forms.ToolStripButton();
            this.tsUndo = new System.Windows.Forms.ToolStripButton();
            this.tsRedo = new System.Windows.Forms.ToolStripButton();
            this.tsBtnPrint = new System.Windows.Forms.ToolStripButton();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsLabelState = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStripFile = new System.Windows.Forms.MenuStrip();
            this.tsMenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuFileNewRtf = new System.Windows.Forms.ToolStripMenuItem();
            this.otwórzPlikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.drukujToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wyjścieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuEditUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuEditRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsMenuEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuEditPasteImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuEditPasteDataFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.formatujCzcionkęToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuFormatAlignLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuFormatAlignCenter = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuFormatAlignRight = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuToolsFind = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuHelpDocumentation = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsMenuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsComboBoxFontChoice = new System.Windows.Forms.ToolStripComboBox();
            this.tsComboBoxFontSizeChoice = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripFont = new System.Windows.Forms.ToolStrip();
            this.tsBtnFontIncrease = new System.Windows.Forms.ToolStripButton();
            this.tsBtnFontDecrease = new System.Windows.Forms.ToolStripButton();
            this.tsBtnBold = new System.Windows.Forms.ToolStripButton();
            this.tsBtnItalic = new System.Windows.Forms.ToolStripButton();
            this.tsBtnUnderline = new System.Windows.Forms.ToolStripButton();
            this.tsBtnStrikeout = new System.Windows.Forms.ToolStripButton();
            this.tsBtnSuperscript = new System.Windows.Forms.ToolStripButton();
            this.tsBtnSubscript = new System.Windows.Forms.ToolStripButton();
            this.tsBtnFontColor = new System.Windows.Forms.ToolStripSplitButton();
            this.tsBtnFontBackColor = new System.Windows.Forms.ToolStripSplitButton();
            this.pn1_BtnCut = new System.Windows.Forms.Button();
            this.pn1_BtnKopiuj = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pn1_BtnPaste = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsBtnLeftAllignement = new System.Windows.Forms.ToolStripButton();
            this.tsBtnCenterAllignment = new System.Windows.Forms.ToolStripButton();
            this.tsBtnRightAlignment = new System.Windows.Forms.ToolStripButton();
            this.BtnImage = new System.Windows.Forms.Button();
            this.date_time = new System.Windows.Forms.Button();
            this.dlaProgramistyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paletteBackColor = new PawcioreX.PaletteBackColor();
            this.paletteFontColor = new PawcioreX.PaletteFontColor();
            this.richTextBox = new PawcioreX.FixedRichTextBox();
            this.toolStripMain.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            this.menuStripFile.SuspendLayout();
            this.toolStripFont.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMain
            // 
            this.toolStripMain.AutoSize = false;
            this.toolStripMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripMain.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnNewFile,
            this.tsBtnOpenFile,
            this.tsBtnSaveFile,
            this.tsUndo,
            this.tsRedo,
            this.tsBtnPrint});
            this.toolStripMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStripMain.Location = new System.Drawing.Point(8, 32);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripMain.Size = new System.Drawing.Size(79, 48);
            this.toolStripMain.TabIndex = 1;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // tsBtnNewFile
            // 
            this.tsBtnNewFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnNewFile.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnNewFile.Image")));
            this.tsBtnNewFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnNewFile.Name = "tsBtnNewFile";
            this.tsBtnNewFile.Size = new System.Drawing.Size(23, 20);
            this.tsBtnNewFile.Text = "Utwórz nowy plik *.rtf";
            this.tsBtnNewFile.ToolTipText = "Utwórz nowy plik *.rtf";
            this.tsBtnNewFile.Click += new System.EventHandler(this.tsBtnNewFile_Click);
            // 
            // tsBtnOpenFile
            // 
            this.tsBtnOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnOpenFile.Image")));
            this.tsBtnOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnOpenFile.Name = "tsBtnOpenFile";
            this.tsBtnOpenFile.Size = new System.Drawing.Size(23, 20);
            this.tsBtnOpenFile.Text = "Otwórz plik *.rtf / *.txt";
            this.tsBtnOpenFile.ToolTipText = "Otwórz plik *.rtf / *.txt";
            this.tsBtnOpenFile.Click += new System.EventHandler(this.tsBtnOpenFile_Click);
            // 
            // tsBtnSaveFile
            // 
            this.tsBtnSaveFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnSaveFile.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnSaveFile.Image")));
            this.tsBtnSaveFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnSaveFile.Name = "tsBtnSaveFile";
            this.tsBtnSaveFile.Size = new System.Drawing.Size(23, 20);
            this.tsBtnSaveFile.Text = "Zapisz";
            this.tsBtnSaveFile.ToolTipText = "Zapisz";
            this.tsBtnSaveFile.Click += new System.EventHandler(this.tsBtnSaveFile_Click);
            // 
            // tsUndo
            // 
            this.tsUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsUndo.Enabled = false;
            this.tsUndo.Image = ((System.Drawing.Image)(resources.GetObject("tsUndo.Image")));
            this.tsUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsUndo.Name = "tsUndo";
            this.tsUndo.Size = new System.Drawing.Size(23, 20);
            this.tsUndo.Text = "Cofnij";
            this.tsUndo.ToolTipText = "Cofnij";
            this.tsUndo.Click += new System.EventHandler(this.tsUndo_Click);
            // 
            // tsRedo
            // 
            this.tsRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsRedo.Enabled = false;
            this.tsRedo.Image = ((System.Drawing.Image)(resources.GetObject("tsRedo.Image")));
            this.tsRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRedo.Name = "tsRedo";
            this.tsRedo.Size = new System.Drawing.Size(23, 20);
            this.tsRedo.Text = "Powtórz wyraz";
            this.tsRedo.ToolTipText = "Przywróć";
            this.tsRedo.Click += new System.EventHandler(this.tsRedo_Click);
            // 
            // tsBtnPrint
            // 
            this.tsBtnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnPrint.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnPrint.Image")));
            this.tsBtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnPrint.Name = "tsBtnPrint";
            this.tsBtnPrint.Size = new System.Drawing.Size(23, 20);
            this.tsBtnPrint.Text = "Drukuj";
            this.tsBtnPrint.ToolTipText = "Drukuj";
            this.tsBtnPrint.Click += new System.EventHandler(this.tsBtnPrint_Click);
            // 
            // statusStripMain
            // 
            this.statusStripMain.AutoSize = false;
            this.statusStripMain.BackColor = System.Drawing.Color.LightSteelBlue;
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tsLabelState});
            this.statusStripMain.Location = new System.Drawing.Point(0, 621);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Padding = new System.Windows.Forms.Padding(2, 0, 14, 0);
            this.statusStripMain.Size = new System.Drawing.Size(853, 22);
            this.statusStripMain.TabIndex = 3;
            this.statusStripMain.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(812, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // tsLabelState
            // 
            this.tsLabelState.Name = "tsLabelState";
            this.tsLabelState.Size = new System.Drawing.Size(25, 17);
            this.tsLabelState.Text = "      ";
            // 
            // menuStripFile
            // 
            this.menuStripFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuFile,
            this.tsMenuEdit,
            this.tsMenuFormat,
            this.tsMenuTools,
            this.tsMenuHelp});
            this.menuStripFile.Location = new System.Drawing.Point(0, 0);
            this.menuStripFile.Name = "menuStripFile";
            this.menuStripFile.Size = new System.Drawing.Size(853, 24);
            this.menuStripFile.TabIndex = 4;
            this.menuStripFile.Text = "menuStrip1";
            // 
            // tsMenuFile
            // 
            this.tsMenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuFileNewRtf,
            this.otwórzPlikToolStripMenuItem,
            this.tsMenuFileSaveAs,
            this.drukujToolStripMenuItem,
            this.wyjścieToolStripMenuItem});
            this.tsMenuFile.Name = "tsMenuFile";
            this.tsMenuFile.Size = new System.Drawing.Size(38, 20);
            this.tsMenuFile.Text = "Plik";
            // 
            // tsMenuFileNewRtf
            // 
            this.tsMenuFileNewRtf.Name = "tsMenuFileNewRtf";
            this.tsMenuFileNewRtf.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.tsMenuFileNewRtf.Size = new System.Drawing.Size(237, 22);
            this.tsMenuFileNewRtf.Text = "Nowy dokument RTF...";
            this.tsMenuFileNewRtf.Click += new System.EventHandler(this.tsMenuFileNewRtf_Click);
            // 
            // otwórzPlikToolStripMenuItem
            // 
            this.otwórzPlikToolStripMenuItem.Name = "otwórzPlikToolStripMenuItem";
            this.otwórzPlikToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.otwórzPlikToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.otwórzPlikToolStripMenuItem.Text = "Otwórz...";
            this.otwórzPlikToolStripMenuItem.Click += new System.EventHandler(this.otwórzPlikToolStripMenuItem_Click);
            // 
            // tsMenuFileSaveAs
            // 
            this.tsMenuFileSaveAs.Name = "tsMenuFileSaveAs";
            this.tsMenuFileSaveAs.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.tsMenuFileSaveAs.Size = new System.Drawing.Size(237, 22);
            this.tsMenuFileSaveAs.Text = "Zapisz jako...";
            this.tsMenuFileSaveAs.Click += new System.EventHandler(this.tsMenuFileSaveAs_Click);
            // 
            // drukujToolStripMenuItem
            // 
            this.drukujToolStripMenuItem.Name = "drukujToolStripMenuItem";
            this.drukujToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.drukujToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.drukujToolStripMenuItem.Text = "Drukuj...";
            this.drukujToolStripMenuItem.Click += new System.EventHandler(this.drukujToolStripMenuItem_Click);
            // 
            // wyjścieToolStripMenuItem
            // 
            this.wyjścieToolStripMenuItem.Name = "wyjścieToolStripMenuItem";
            this.wyjścieToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.wyjścieToolStripMenuItem.Text = "Wyjście...";
            this.wyjścieToolStripMenuItem.Click += new System.EventHandler(this.wyjścieToolStripMenuItem_Click);
            // 
            // tsMenuEdit
            // 
            this.tsMenuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuEditUndo,
            this.tsMenuEditRedo,
            this.toolStripSeparator1,
            this.tsMenuEditCut,
            this.tsMenuEditCopy,
            this.tsMenuEditPaste,
            this.tsMenuEditPasteImage,
            this.tsMenuEditPasteDataFormat});
            this.tsMenuEdit.Name = "tsMenuEdit";
            this.tsMenuEdit.Size = new System.Drawing.Size(53, 20);
            this.tsMenuEdit.Text = "Edycja";
            // 
            // tsMenuEditUndo
            // 
            this.tsMenuEditUndo.Name = "tsMenuEditUndo";
            this.tsMenuEditUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.tsMenuEditUndo.Size = new System.Drawing.Size(183, 22);
            this.tsMenuEditUndo.Text = "Cofnij...";
            this.tsMenuEditUndo.Click += new System.EventHandler(this.tsMenuEditUndo_Click);
            // 
            // tsMenuEditRedo
            // 
            this.tsMenuEditRedo.Name = "tsMenuEditRedo";
            this.tsMenuEditRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.tsMenuEditRedo.Size = new System.Drawing.Size(183, 22);
            this.tsMenuEditRedo.Text = "Przywróć...";
            this.tsMenuEditRedo.Click += new System.EventHandler(this.tsMenuEditRedo_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(180, 6);
            // 
            // tsMenuEditCut
            // 
            this.tsMenuEditCut.Name = "tsMenuEditCut";
            this.tsMenuEditCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.tsMenuEditCut.Size = new System.Drawing.Size(183, 22);
            this.tsMenuEditCut.Text = "Wytnij...";
            this.tsMenuEditCut.Click += new System.EventHandler(this.tsMenuEditCut_Click);
            // 
            // tsMenuEditCopy
            // 
            this.tsMenuEditCopy.Name = "tsMenuEditCopy";
            this.tsMenuEditCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.tsMenuEditCopy.Size = new System.Drawing.Size(183, 22);
            this.tsMenuEditCopy.Text = "Kopiuj...";
            this.tsMenuEditCopy.Click += new System.EventHandler(this.tsMenuEditCopy_Click);
            // 
            // tsMenuEditPaste
            // 
            this.tsMenuEditPaste.Name = "tsMenuEditPaste";
            this.tsMenuEditPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.tsMenuEditPaste.Size = new System.Drawing.Size(183, 22);
            this.tsMenuEditPaste.Text = "Wklej...";
            this.tsMenuEditPaste.Click += new System.EventHandler(this.tsMenuEditPaste_Click);
            // 
            // tsMenuEditPasteImage
            // 
            this.tsMenuEditPasteImage.Name = "tsMenuEditPasteImage";
            this.tsMenuEditPasteImage.Size = new System.Drawing.Size(183, 22);
            this.tsMenuEditPasteImage.Text = "Wstaw obraz...";
            this.tsMenuEditPasteImage.Click += new System.EventHandler(this.tsMenuEditPasteImage_Click);
            // 
            // tsMenuEditPasteDataFormat
            // 
            this.tsMenuEditPasteDataFormat.Name = "tsMenuEditPasteDataFormat";
            this.tsMenuEditPasteDataFormat.Size = new System.Drawing.Size(183, 22);
            this.tsMenuEditPasteDataFormat.Text = "Wstaw format daty...";
            this.tsMenuEditPasteDataFormat.Click += new System.EventHandler(this.tsMenuEditPasteDataFormat_Click);
            // 
            // tsMenuFormat
            // 
            this.tsMenuFormat.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formatujCzcionkęToolStripMenuItem});
            this.tsMenuFormat.Name = "tsMenuFormat";
            this.tsMenuFormat.Size = new System.Drawing.Size(67, 20);
            this.tsMenuFormat.Text = "Formatuj";
            // 
            // formatujCzcionkęToolStripMenuItem
            // 
            this.formatujCzcionkęToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuFormatAlignLeft,
            this.tsMenuFormatAlignCenter,
            this.tsMenuFormatAlignRight});
            this.formatujCzcionkęToolStripMenuItem.Name = "formatujCzcionkęToolStripMenuItem";
            this.formatujCzcionkęToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.formatujCzcionkęToolStripMenuItem.Text = "Wyrównaj tekst...";
            // 
            // tsMenuFormatAlignLeft
            // 
            this.tsMenuFormatAlignLeft.Name = "tsMenuFormatAlignLeft";
            this.tsMenuFormatAlignLeft.Size = new System.Drawing.Size(139, 22);
            this.tsMenuFormatAlignLeft.Text = "Do lewej...";
            this.tsMenuFormatAlignLeft.Click += new System.EventHandler(this.tsMenuFormatAlignLeft_Click);
            // 
            // tsMenuFormatAlignCenter
            // 
            this.tsMenuFormatAlignCenter.Name = "tsMenuFormatAlignCenter";
            this.tsMenuFormatAlignCenter.Size = new System.Drawing.Size(139, 22);
            this.tsMenuFormatAlignCenter.Text = "Wyśrodkuj...";
            this.tsMenuFormatAlignCenter.Click += new System.EventHandler(this.tsMenuFormatAlignCenter_Click);
            // 
            // tsMenuFormatAlignRight
            // 
            this.tsMenuFormatAlignRight.Name = "tsMenuFormatAlignRight";
            this.tsMenuFormatAlignRight.Size = new System.Drawing.Size(139, 22);
            this.tsMenuFormatAlignRight.Text = "Do prawej...";
            this.tsMenuFormatAlignRight.Click += new System.EventHandler(this.tsMenuFormatAlignRight_Click);
            // 
            // tsMenuTools
            // 
            this.tsMenuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuToolsFind});
            this.tsMenuTools.Name = "tsMenuTools";
            this.tsMenuTools.Size = new System.Drawing.Size(70, 20);
            this.tsMenuTools.Text = "Narzędzia";
            // 
            // tsMenuToolsFind
            // 
            this.tsMenuToolsFind.Name = "tsMenuToolsFind";
            this.tsMenuToolsFind.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.tsMenuToolsFind.Size = new System.Drawing.Size(245, 22);
            this.tsMenuToolsFind.Text = "Wyszukaj/Zamień tekst...";
            this.tsMenuToolsFind.Click += new System.EventHandler(this.tsMenuToolsFind_Click);
            // 
            // tsMenuHelp
            // 
            this.tsMenuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuHelpDocumentation,
            this.dlaProgramistyToolStripMenuItem,
            this.toolStripSeparator2,
            this.tsMenuHelpAbout});
            this.tsMenuHelp.Name = "tsMenuHelp";
            this.tsMenuHelp.Size = new System.Drawing.Size(57, 20);
            this.tsMenuHelp.Text = "Pomoc";
            // 
            // tsMenuHelpDocumentation
            // 
            this.tsMenuHelpDocumentation.Name = "tsMenuHelpDocumentation";
            this.tsMenuHelpDocumentation.Size = new System.Drawing.Size(229, 22);
            this.tsMenuHelpDocumentation.Text = "Dokumentacja użytkownika...";
            this.tsMenuHelpDocumentation.Click += new System.EventHandler(this.tsMenuHelpDocumentation_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(226, 6);
            // 
            // tsMenuHelpAbout
            // 
            this.tsMenuHelpAbout.Name = "tsMenuHelpAbout";
            this.tsMenuHelpAbout.Size = new System.Drawing.Size(229, 22);
            this.tsMenuHelpAbout.Text = "O autorze...";
            this.tsMenuHelpAbout.Click += new System.EventHandler(this.tsMenuHelpAbout_Click);
            // 
            // tsComboBoxFontChoice
            // 
            this.tsComboBoxFontChoice.AutoSize = false;
            this.tsComboBoxFontChoice.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.tsComboBoxFontChoice.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tsComboBoxFontChoice.Name = "tsComboBoxFontChoice";
            this.tsComboBoxFontChoice.Size = new System.Drawing.Size(121, 21);
            this.tsComboBoxFontChoice.Text = "Consolas";
            this.tsComboBoxFontChoice.DropDownClosed += new System.EventHandler(this.tsComboBoxFontChoice_DropDownClosed_1);
            this.tsComboBoxFontChoice.TextUpdate += new System.EventHandler(this.tsComboBoxFontChoice_TextUpdate);
            this.tsComboBoxFontChoice.Enter += new System.EventHandler(this.tsComboBoxFontChoice_Enter);
            this.tsComboBoxFontChoice.Leave += new System.EventHandler(this.tsComboBoxFontChoice_Leave_1);
            this.tsComboBoxFontChoice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tsComboBoxFontChoice_KeyDown_1);
            this.tsComboBoxFontChoice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tsComboBoxFontChoice_KeyPress);
            this.tsComboBoxFontChoice.Click += new System.EventHandler(this.tsComboBoxFontChoice_Click);
            // 
            // tsComboBoxFontSizeChoice
            // 
            this.tsComboBoxFontSizeChoice.AutoSize = false;
            this.tsComboBoxFontSizeChoice.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.tsComboBoxFontSizeChoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tsComboBoxFontSizeChoice.Items.AddRange(new object[] {
            "8",
            "9",
            "10",
            "11",
            "12",
            "14",
            "16",
            "18",
            "20",
            "22",
            "24",
            "26",
            "28",
            "36",
            "48",
            "72"});
            this.tsComboBoxFontSizeChoice.Name = "tsComboBoxFontSizeChoice";
            this.tsComboBoxFontSizeChoice.Size = new System.Drawing.Size(50, 21);
            this.tsComboBoxFontSizeChoice.Text = "11";
            this.tsComboBoxFontSizeChoice.DropDownClosed += new System.EventHandler(this.tsComboBoxFontChoice_DropDownClosed);
            this.tsComboBoxFontSizeChoice.Enter += new System.EventHandler(this.tsComboBoxFontSizeChoice_Enter);
            this.tsComboBoxFontSizeChoice.Leave += new System.EventHandler(this.tsComboBoxFontChoice_Leave);
            this.tsComboBoxFontSizeChoice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tsComboBoxFontChoice_KeyDown);
            this.tsComboBoxFontSizeChoice.Click += new System.EventHandler(this.tsComboBoxFontSizeChoice_Click);
            // 
            // toolStripFont
            // 
            this.toolStripFont.AutoSize = false;
            this.toolStripFont.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripFont.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsComboBoxFontChoice,
            this.tsComboBoxFontSizeChoice,
            this.tsBtnFontIncrease,
            this.tsBtnFontDecrease,
            this.tsBtnBold,
            this.tsBtnItalic,
            this.tsBtnUnderline,
            this.tsBtnStrikeout,
            this.tsBtnSuperscript,
            this.tsBtnSubscript,
            this.tsBtnFontColor,
            this.tsBtnFontBackColor});
            this.toolStripFont.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStripFont.Location = new System.Drawing.Point(213, 32);
            this.toolStripFont.Name = "toolStripFont";
            this.toolStripFont.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripFont.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripFont.Size = new System.Drawing.Size(231, 48);
            this.toolStripFont.TabIndex = 2;
            this.toolStripFont.Text = "toolStrip1";
            // 
            // tsBtnFontIncrease
            // 
            this.tsBtnFontIncrease.AutoSize = false;
            this.tsBtnFontIncrease.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnFontIncrease.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnFontIncrease.Image")));
            this.tsBtnFontIncrease.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnFontIncrease.Name = "tsBtnFontIncrease";
            this.tsBtnFontIncrease.Size = new System.Drawing.Size(24, 20);
            this.tsBtnFontIncrease.Text = "toolStripButton1";
            this.tsBtnFontIncrease.ToolTipText = "Zwiększ czcionkę";
            this.tsBtnFontIncrease.Click += new System.EventHandler(this.tsBtnFontIncrease_Click);
            // 
            // tsBtnFontDecrease
            // 
            this.tsBtnFontDecrease.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnFontDecrease.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnFontDecrease.Image")));
            this.tsBtnFontDecrease.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnFontDecrease.Name = "tsBtnFontDecrease";
            this.tsBtnFontDecrease.Size = new System.Drawing.Size(23, 20);
            this.tsBtnFontDecrease.Text = "toolStripButton2";
            this.tsBtnFontDecrease.ToolTipText = "Zmniejsz czcionkę";
            this.tsBtnFontDecrease.Click += new System.EventHandler(this.tsBtnFontDecrease_Click);
            // 
            // tsBtnBold
            // 
            this.tsBtnBold.AutoSize = false;
            this.tsBtnBold.CheckOnClick = true;
            this.tsBtnBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnBold.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnBold.Image")));
            this.tsBtnBold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnBold.Name = "tsBtnBold";
            this.tsBtnBold.Size = new System.Drawing.Size(25, 20);
            this.tsBtnBold.Text = "toolStripButton3";
            this.tsBtnBold.ToolTipText = "Pogrubienie";
            this.tsBtnBold.Click += new System.EventHandler(this.tsBtnBold_Click);
            // 
            // tsBtnItalic
            // 
            this.tsBtnItalic.AutoSize = false;
            this.tsBtnItalic.CheckOnClick = true;
            this.tsBtnItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnItalic.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnItalic.Image")));
            this.tsBtnItalic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnItalic.Name = "tsBtnItalic";
            this.tsBtnItalic.Size = new System.Drawing.Size(25, 20);
            this.tsBtnItalic.Text = "toolStripButton4";
            this.tsBtnItalic.ToolTipText = "Kursywa";
            this.tsBtnItalic.Click += new System.EventHandler(this.tsBtnItalic_Click);
            // 
            // tsBtnUnderline
            // 
            this.tsBtnUnderline.AutoSize = false;
            this.tsBtnUnderline.CheckOnClick = true;
            this.tsBtnUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnUnderline.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnUnderline.Image")));
            this.tsBtnUnderline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnUnderline.Name = "tsBtnUnderline";
            this.tsBtnUnderline.Size = new System.Drawing.Size(25, 20);
            this.tsBtnUnderline.Text = "toolStripButton5";
            this.tsBtnUnderline.ToolTipText = "Podkreślenie";
            this.tsBtnUnderline.Click += new System.EventHandler(this.tsBtnUnderline_Click);
            // 
            // tsBtnStrikeout
            // 
            this.tsBtnStrikeout.AutoSize = false;
            this.tsBtnStrikeout.CheckOnClick = true;
            this.tsBtnStrikeout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnStrikeout.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnStrikeout.Image")));
            this.tsBtnStrikeout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnStrikeout.Name = "tsBtnStrikeout";
            this.tsBtnStrikeout.Size = new System.Drawing.Size(25, 20);
            this.tsBtnStrikeout.Text = "toolStripButton6";
            this.tsBtnStrikeout.ToolTipText = "Przekreślenie";
            this.tsBtnStrikeout.Click += new System.EventHandler(this.tsBtnStrikeout_Click);
            // 
            // tsBtnSuperscript
            // 
            this.tsBtnSuperscript.AutoSize = false;
            this.tsBtnSuperscript.CheckOnClick = true;
            this.tsBtnSuperscript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnSuperscript.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnSuperscript.Image")));
            this.tsBtnSuperscript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnSuperscript.Name = "tsBtnSuperscript";
            this.tsBtnSuperscript.Size = new System.Drawing.Size(25, 20);
            this.tsBtnSuperscript.Text = "toolStripButton7";
            this.tsBtnSuperscript.ToolTipText = "Indeks górny";
            this.tsBtnSuperscript.Click += new System.EventHandler(this.tsBtnSuperscript_Click);
            // 
            // tsBtnSubscript
            // 
            this.tsBtnSubscript.AutoSize = false;
            this.tsBtnSubscript.CheckOnClick = true;
            this.tsBtnSubscript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnSubscript.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnSubscript.Image")));
            this.tsBtnSubscript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnSubscript.Name = "tsBtnSubscript";
            this.tsBtnSubscript.Size = new System.Drawing.Size(25, 20);
            this.tsBtnSubscript.Text = "toolStripButton8";
            this.tsBtnSubscript.ToolTipText = "Indeks dolny";
            this.tsBtnSubscript.Click += new System.EventHandler(this.tsBtnSubscript_Click);
            // 
            // tsBtnFontColor
            // 
            this.tsBtnFontColor.AutoSize = false;
            this.tsBtnFontColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tsBtnFontColor.BackgroundImage = global::PawcioreX.Properties.Resources.HackToolbarBackColor;
            this.tsBtnFontColor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tsBtnFontColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnFontColor.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnFontColor.Image")));
            this.tsBtnFontColor.ImageTransparentColor = System.Drawing.SystemColors.Control;
            this.tsBtnFontColor.Name = "tsBtnFontColor";
            this.tsBtnFontColor.Size = new System.Drawing.Size(32, 20);
            this.tsBtnFontColor.Text = "toolStripSplitButton1";
            this.tsBtnFontColor.ToolTipText = "Kolor wyróżnienia tekstu";
            this.tsBtnFontColor.ButtonClick += new System.EventHandler(this.tsBtnFontColor_ButtonClick);
            this.tsBtnFontColor.DropDownOpening += new System.EventHandler(this.tsBtnFontColor_DropDownOpening);
            // 
            // tsBtnFontBackColor
            // 
            this.tsBtnFontBackColor.BackColor = System.Drawing.Color.White;
            this.tsBtnFontBackColor.BackgroundImage = global::PawcioreX.Properties.Resources.HackToolbarBackColor;
            this.tsBtnFontBackColor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tsBtnFontBackColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnFontBackColor.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnFontBackColor.Image")));
            this.tsBtnFontBackColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnFontBackColor.Name = "tsBtnFontBackColor";
            this.tsBtnFontBackColor.Size = new System.Drawing.Size(32, 20);
            this.tsBtnFontBackColor.Text = "toolStripSplitButton1";
            this.tsBtnFontBackColor.ToolTipText = "Kolor zaznaczenia";
            this.tsBtnFontBackColor.ButtonClick += new System.EventHandler(this.tsBtnFontBackColor_ButtonClick);
            this.tsBtnFontBackColor.DropDownOpening += new System.EventHandler(this.tsBtnFontBackColor_DropDownOpening);
            // 
            // pn1_BtnCut
            // 
            this.pn1_BtnCut.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pn1_BtnCut.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pn1_BtnCut.Enabled = false;
            this.pn1_BtnCut.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonFace;
            this.pn1_BtnCut.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.pn1_BtnCut.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.pn1_BtnCut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pn1_BtnCut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.pn1_BtnCut.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.pn1_BtnCut.Location = new System.Drawing.Point(0, 5);
            this.pn1_BtnCut.Name = "pn1_BtnCut";
            this.pn1_BtnCut.Size = new System.Drawing.Size(70, 25);
            this.pn1_BtnCut.TabIndex = 0;
            this.pn1_BtnCut.Text = "Wytnij";
            this.pn1_BtnCut.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.pn1_BtnCut.UseVisualStyleBackColor = false;
            this.pn1_BtnCut.Click += new System.EventHandler(this.pn1_BtnCut_Click);
            // 
            // pn1_BtnKopiuj
            // 
            this.pn1_BtnKopiuj.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pn1_BtnKopiuj.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pn1_BtnKopiuj.Enabled = false;
            this.pn1_BtnKopiuj.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonFace;
            this.pn1_BtnKopiuj.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.pn1_BtnKopiuj.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.pn1_BtnKopiuj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pn1_BtnKopiuj.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.pn1_BtnKopiuj.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.pn1_BtnKopiuj.Location = new System.Drawing.Point(0, 28);
            this.pn1_BtnKopiuj.Name = "pn1_BtnKopiuj";
            this.pn1_BtnKopiuj.Size = new System.Drawing.Size(70, 25);
            this.pn1_BtnKopiuj.TabIndex = 1;
            this.pn1_BtnKopiuj.Text = "Kopiuj";
            this.pn1_BtnKopiuj.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.pn1_BtnKopiuj.UseVisualStyleBackColor = false;
            this.pn1_BtnKopiuj.Click += new System.EventHandler(this.pn1_BtnKopiuj_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pn1_BtnPaste);
            this.panel1.Controls.Add(this.pn1_BtnKopiuj);
            this.panel1.Controls.Add(this.pn1_BtnCut);
            this.panel1.Location = new System.Drawing.Point(90, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(115, 53);
            this.panel1.TabIndex = 9;
            // 
            // pn1_BtnPaste
            // 
            this.pn1_BtnPaste.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pn1_BtnPaste.Enabled = false;
            this.pn1_BtnPaste.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonFace;
            this.pn1_BtnPaste.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.pn1_BtnPaste.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.pn1_BtnPaste.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pn1_BtnPaste.Location = new System.Drawing.Point(71, 9);
            this.pn1_BtnPaste.Name = "pn1_BtnPaste";
            this.pn1_BtnPaste.Size = new System.Drawing.Size(45, 41);
            this.pn1_BtnPaste.TabIndex = 2;
            this.pn1_BtnPaste.Text = "Wklej";
            this.pn1_BtnPaste.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.pn1_BtnPaste.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.pn1_BtnPaste.UseVisualStyleBackColor = false;
            this.pn1_BtnPaste.Click += new System.EventHandler(this.pn1_BtnPaste_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnLeftAllignement,
            this.tsBtnCenterAllignment,
            this.tsBtnRightAlignment});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(451, 47);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(74, 21);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsBtnLeftAllignement
            // 
            this.tsBtnLeftAllignement.AutoSize = false;
            this.tsBtnLeftAllignement.CheckOnClick = true;
            this.tsBtnLeftAllignement.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnLeftAllignement.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnLeftAllignement.Image")));
            this.tsBtnLeftAllignement.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnLeftAllignement.Name = "tsBtnLeftAllignement";
            this.tsBtnLeftAllignement.Size = new System.Drawing.Size(23, 20);
            this.tsBtnLeftAllignement.Text = "toolStripButton1";
            this.tsBtnLeftAllignement.ToolTipText = "Wyrównaj tekst do lewej";
            this.tsBtnLeftAllignement.Click += new System.EventHandler(this.tsBtnLeftAllignement_Click);
            // 
            // tsBtnCenterAllignment
            // 
            this.tsBtnCenterAllignment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnCenterAllignment.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnCenterAllignment.Image")));
            this.tsBtnCenterAllignment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnCenterAllignment.Name = "tsBtnCenterAllignment";
            this.tsBtnCenterAllignment.Size = new System.Drawing.Size(23, 20);
            this.tsBtnCenterAllignment.Text = "toolStripButton2";
            this.tsBtnCenterAllignment.ToolTipText = "Wyśrodkuj tekst";
            this.tsBtnCenterAllignment.Click += new System.EventHandler(this.tsBtnCenterAllignment_Click);
            // 
            // tsBtnRightAlignment
            // 
            this.tsBtnRightAlignment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnRightAlignment.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnRightAlignment.Image")));
            this.tsBtnRightAlignment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnRightAlignment.Name = "tsBtnRightAlignment";
            this.tsBtnRightAlignment.Size = new System.Drawing.Size(23, 20);
            this.tsBtnRightAlignment.Text = "toolStripButton3";
            this.tsBtnRightAlignment.ToolTipText = "Wyrównaj tekst do prawej";
            this.tsBtnRightAlignment.Click += new System.EventHandler(this.tsBtnRightAlignment_Click);
            // 
            // BtnImage
            // 
            this.BtnImage.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnImage.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnImage.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.BtnImage.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.BtnImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnImage.Location = new System.Drawing.Point(532, 34);
            this.BtnImage.Name = "BtnImage";
            this.BtnImage.Size = new System.Drawing.Size(49, 47);
            this.BtnImage.TabIndex = 11;
            this.BtnImage.Text = "Obraz";
            this.BtnImage.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BtnImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnImage.UseVisualStyleBackColor = false;
            this.BtnImage.Click += new System.EventHandler(this.BtnImage_Click);
            // 
            // date_time
            // 
            this.date_time.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.date_time.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonFace;
            this.date_time.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.date_time.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.date_time.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.date_time.Location = new System.Drawing.Point(584, 34);
            this.date_time.Name = "date_time";
            this.date_time.Size = new System.Drawing.Size(74, 47);
            this.date_time.TabIndex = 12;
            this.date_time.Text = "Data/godz.";
            this.date_time.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.date_time.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.date_time.UseVisualStyleBackColor = false;
            this.date_time.Click += new System.EventHandler(this.date_time_Click);
            // 
            // dlaProgramistyToolStripMenuItem
            // 
            this.dlaProgramistyToolStripMenuItem.Name = "dlaProgramistyToolStripMenuItem";
            this.dlaProgramistyToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.dlaProgramistyToolStripMenuItem.Text = "Dla programisty...";
            this.dlaProgramistyToolStripMenuItem.Click += new System.EventHandler(this.dlaProgramistyToolStripMenuItem_Click);
            // 
            // paletteBackColor
            // 
            this.paletteBackColor.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.paletteBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paletteBackColor.Location = new System.Drawing.Point(671, 222);
            this.paletteBackColor.Name = "paletteBackColor";
            this.paletteBackColor.Size = new System.Drawing.Size(185, 154);
            this.paletteBackColor.TabIndex = 14;
            this.paletteBackColor.Text = "PaletteBackColor";
            this.paletteBackColor.Visible = false;
            // 
            // paletteFontColor
            // 
            this.paletteFontColor.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.paletteFontColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paletteFontColor.Location = new System.Drawing.Point(671, 32);
            this.paletteFontColor.Name = "paletteFontColor";
            this.paletteFontColor.Size = new System.Drawing.Size(182, 184);
            this.paletteFontColor.TabIndex = 13;
            this.paletteFontColor.Text = "Pallete1";
            this.paletteFontColor.Visible = false;
            // 
            // richTextBox
            // 
            this.richTextBox.AcceptsTab = true;
            this.richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.richTextBox.ForeColor = System.Drawing.Color.Black;
            this.richTextBox.HideSelection = false;
            this.richTextBox.Location = new System.Drawing.Point(56, 87);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(744, 535);
            this.richTextBox.TabIndex = 5;
            this.richTextBox.Text = "";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 643);
            this.Controls.Add(this.paletteBackColor);
            this.Controls.Add(this.paletteFontColor);
            this.Controls.Add(this.date_time);
            this.Controls.Add(this.BtnImage);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.toolStripFont);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.menuStripFile);
            this.MainMenuStrip = this.menuStripFile;
            this.Name = "mainForm";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.menuStripFile.ResumeLayout(false);
            this.menuStripFile.PerformLayout();
            this.toolStripFont.ResumeLayout(false);
            this.toolStripFont.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton tsBtnNewFile;
        private System.Windows.Forms.ToolStripButton tsBtnOpenFile;
        private System.Windows.Forms.ToolStripButton tsBtnSaveFile;
        private System.Windows.Forms.ToolStripButton tsUndo;
        private System.Windows.Forms.ToolStripButton tsRedo;
        private System.Windows.Forms.ToolStripButton tsBtnPrint;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripStatusLabel tsLabelState;
        private System.Windows.Forms.MenuStrip menuStripFile;
        private System.Windows.Forms.ToolStripMenuItem tsMenuFile;
        private System.Windows.Forms.ToolStripMenuItem tsMenuFileSaveAs;
        private FixedRichTextBox richTextBox;
        private System.Windows.Forms.ToolStripComboBox tsComboBoxFontChoice;
        private System.Windows.Forms.ToolStripComboBox tsComboBoxFontSizeChoice;
        private System.Windows.Forms.ToolStripButton tsBtnFontIncrease;
        private System.Windows.Forms.ToolStripButton tsBtnFontDecrease;
        private System.Windows.Forms.ToolStripButton tsBtnBold;
        private System.Windows.Forms.ToolStripButton tsBtnItalic;
        private System.Windows.Forms.ToolStripButton tsBtnUnderline;
        private System.Windows.Forms.ToolStripButton tsBtnStrikeout;
        private System.Windows.Forms.ToolStripButton tsBtnSuperscript;
        private System.Windows.Forms.ToolStripButton tsBtnSubscript;
        private System.Windows.Forms.ToolStripSplitButton tsBtnFontColor;
        private System.Windows.Forms.ToolStrip toolStripFont;
        private System.Windows.Forms.ToolStripSplitButton tsBtnFontBackColor;
        private System.Windows.Forms.Button pn1_BtnCut;
        private System.Windows.Forms.Button pn1_BtnKopiuj;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button pn1_BtnPaste;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsBtnLeftAllignement;
        private System.Windows.Forms.ToolStripButton tsBtnCenterAllignment;
        private System.Windows.Forms.ToolStripButton tsBtnRightAlignment;
        private System.Windows.Forms.Button BtnImage;
        private System.Windows.Forms.Button date_time;
        private PaletteFontColor paletteFontColor;
        private PaletteBackColor paletteBackColor;
        private System.Windows.Forms.ToolStripMenuItem tsMenuFileNewRtf;
        private System.Windows.Forms.ToolStripMenuItem otwórzPlikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drukujToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wyjścieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsMenuEdit;
        private System.Windows.Forms.ToolStripMenuItem tsMenuEditUndo;
        private System.Windows.Forms.ToolStripMenuItem tsMenuEditRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsMenuEditCut;
        private System.Windows.Forms.ToolStripMenuItem tsMenuEditCopy;
        private System.Windows.Forms.ToolStripMenuItem tsMenuEditPaste;
        private System.Windows.Forms.ToolStripMenuItem tsMenuEditPasteImage;
        private System.Windows.Forms.ToolStripMenuItem tsMenuEditPasteDataFormat;
        private System.Windows.Forms.ToolStripMenuItem tsMenuFormat;
        private System.Windows.Forms.ToolStripMenuItem formatujCzcionkęToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsMenuFormatAlignLeft;
        private System.Windows.Forms.ToolStripMenuItem tsMenuFormatAlignCenter;
        private System.Windows.Forms.ToolStripMenuItem tsMenuFormatAlignRight;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem tsMenuTools;
        private System.Windows.Forms.ToolStripMenuItem tsMenuToolsFind;
        private System.Windows.Forms.ToolStripMenuItem tsMenuHelp;
        private System.Windows.Forms.ToolStripMenuItem tsMenuHelpAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsMenuHelpDocumentation;
        private System.Windows.Forms.ToolStripMenuItem dlaProgramistyToolStripMenuItem;
    }
}

