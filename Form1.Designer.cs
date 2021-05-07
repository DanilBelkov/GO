namespace Go
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.smallImageList = new System.Windows.Forms.ImageList(this.components);
            this.B_show_points = new System.Windows.Forms.Button();
            this.B_draw = new System.Windows.Forms.Button();
            this.label_Way = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.B_River = new System.Windows.Forms.Button();
            this.B_Lake = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.B_to = new System.Windows.Forms.Button();
            this.B_from = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.B_open = new System.Windows.Forms.ToolStripButton();
            this.B_plus = new System.Windows.Forms.ToolStripButton();
            this.B_minus = new System.Windows.Forms.ToolStripButton();
            this.label_scale = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.B_createPoints = new System.Windows.Forms.ToolStripButton();
            this.Tool_B_create_flora = new System.Windows.Forms.ToolStripButton();
            this.Tool_B_create_hydrography = new System.Windows.Forms.ToolStripButton();
            this.Tool_B_create_artificalObject = new System.Windows.Forms.ToolStripButton();
            this.Tool_B_create_landform = new System.Windows.Forms.ToolStripButton();
            this.ToolStrip_LandformLevel = new System.Windows.Forms.ToolStripSplitButton();
            this.Tool_B_create_stone = new System.Windows.Forms.ToolStripButton();
            this.tool_B_subtype = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Tool_B_area = new System.Windows.Forms.ToolStripButton();
            this.Tool_B_line = new System.Windows.Forms.ToolStripButton();
            this.Tool_B_item = new System.Windows.Forms.ToolStripButton();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.Landform_Plus = new System.Windows.Forms.ToolStripMenuItem();
            this.Landform_Minus = new System.Windows.Forms.ToolStripMenuItem();
            this.PanelWithTypes = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.PanelWithTypes.SuspendLayout();
            this.SuspendLayout();
            // 
            // smallImageList
            // 
            this.smallImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("smallImageList.ImageStream")));
            this.smallImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.smallImageList.Images.SetKeyName(0, "garden.jpg");
            this.smallImageList.Images.SetKeyName(1, "grape.jpg");
            this.smallImageList.Images.SetKeyName(2, "hardRunForest.jpg");
            this.smallImageList.Images.SetKeyName(3, "notRunArea.jpg");
            this.smallImageList.Images.SetKeyName(4, "notRunSwamp.jpg");
            this.smallImageList.Images.SetKeyName(5, "openSpace.jpg");
            this.smallImageList.Images.SetKeyName(6, "openSpace2.jpg");
            this.smallImageList.Images.SetKeyName(7, "openStonyArea.jpg");
            this.smallImageList.Images.SetKeyName(8, "plowed.jpg");
            this.smallImageList.Images.SetKeyName(9, "puddle.jpg");
            // 
            // B_show_points
            // 
            this.B_show_points.Location = new System.Drawing.Point(20, 514);
            this.B_show_points.Name = "B_show_points";
            this.B_show_points.Size = new System.Drawing.Size(75, 35);
            this.B_show_points.TabIndex = 7;
            this.B_show_points.Text = "Показать все точки";
            this.B_show_points.UseVisualStyleBackColor = true;
            this.B_show_points.Click += new System.EventHandler(this.B_show_points_Click);
            // 
            // B_draw
            // 
            this.B_draw.BackColor = System.Drawing.Color.White;
            this.B_draw.Location = new System.Drawing.Point(126, 526);
            this.B_draw.Name = "B_draw";
            this.B_draw.Size = new System.Drawing.Size(75, 23);
            this.B_draw.TabIndex = 6;
            this.B_draw.Text = "нарисовать";
            this.B_draw.UseVisualStyleBackColor = false;
            this.B_draw.Click += new System.EventHandler(this.B_draw_Click);
            // 
            // label_Way
            // 
            this.label_Way.AutoSize = true;
            this.label_Way.Location = new System.Drawing.Point(17, 576);
            this.label_Way.Name = "label_Way";
            this.label_Way.Size = new System.Drawing.Size(29, 13);
            this.label_Way.TabIndex = 5;
            this.label_Way.Text = "Way";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.splitContainer1.Panel2.Controls.Add(this.PanelWithTypes);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.B_to);
            this.splitContainer1.Panel2.Controls.Add(this.B_from);
            this.splitContainer1.Panel2.Controls.Add(this.label_Way);
            this.splitContainer1.Panel2.Controls.Add(this.B_draw);
            this.splitContainer1.Panel2.Controls.Add(this.B_show_points);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(1290, 627);
            this.splitContainer1.SplitterDistance = 1060;
            this.splitContainer1.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1057, 627);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // B_River
            // 
            this.B_River.Location = new System.Drawing.Point(37, 17);
            this.B_River.Name = "B_River";
            this.B_River.Size = new System.Drawing.Size(25, 25);
            this.B_River.TabIndex = 13;
            this.B_River.Text = "River";
            this.B_River.UseVisualStyleBackColor = true;
            this.B_River.Click += new System.EventHandler(this.B_River_Click);
            // 
            // B_Lake
            // 
            this.B_Lake.Location = new System.Drawing.Point(6, 17);
            this.B_Lake.Name = "B_Lake";
            this.B_Lake.Size = new System.Drawing.Size(25, 25);
            this.B_Lake.TabIndex = 12;
            this.B_Lake.Text = "Lake";
            this.B_Lake.UseVisualStyleBackColor = true;
            this.B_Lake.Click += new System.EventHandler(this.B_Lake_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(88, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "-------->";
            // 
            // B_to
            // 
            this.B_to.Location = new System.Drawing.Point(160, 37);
            this.B_to.Name = "B_to";
            this.B_to.Size = new System.Drawing.Size(41, 23);
            this.B_to.TabIndex = 10;
            this.B_to.Text = "до";
            this.B_to.UseVisualStyleBackColor = true;
            this.B_to.Click += new System.EventHandler(this.B_to_Click);
            // 
            // B_from
            // 
            this.B_from.Location = new System.Drawing.Point(32, 37);
            this.B_from.Name = "B_from";
            this.B_from.Size = new System.Drawing.Size(41, 23);
            this.B_from.TabIndex = 9;
            this.B_from.Text = "от";
            this.B_from.UseVisualStyleBackColor = true;
            this.B_from.Click += new System.EventHandler(this.B_from_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.DarkTurquoise;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.B_open,
            this.B_plus,
            this.B_minus,
            this.label_scale,
            this.toolStripSeparator1,
            this.B_createPoints,
            this.Tool_B_create_flora,
            this.Tool_B_create_hydrography,
            this.Tool_B_create_artificalObject,
            this.Tool_B_create_landform,
            this.ToolStrip_LandformLevel,
            this.Tool_B_create_stone,
            this.tool_B_subtype,
            this.Tool_B_area,
            this.Tool_B_line,
            this.Tool_B_item,
            this.toolStripSplitButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1290, 25);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // B_open
            // 
            this.B_open.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.B_open.Image = ((System.Drawing.Image)(resources.GetObject("B_open.Image")));
            this.B_open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.B_open.Name = "B_open";
            this.B_open.Size = new System.Drawing.Size(23, 22);
            this.B_open.Text = "open";
            this.B_open.ToolTipText = "Открыть файл";
            this.B_open.Click += new System.EventHandler(this.B_open_Click);
            // 
            // B_plus
            // 
            this.B_plus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.B_plus.Enabled = false;
            this.B_plus.Image = global::Go.Properties.Resources.plus_zoom;
            this.B_plus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.B_plus.Name = "B_plus";
            this.B_plus.Size = new System.Drawing.Size(23, 22);
            this.B_plus.Text = "toolStripButton1";
            this.B_plus.ToolTipText = "Увеличить";
            this.B_plus.Click += new System.EventHandler(this.B_plus_Click);
            // 
            // B_minus
            // 
            this.B_minus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.B_minus.Enabled = false;
            this.B_minus.Image = global::Go.Properties.Resources.out_plus_zoom;
            this.B_minus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.B_minus.Name = "B_minus";
            this.B_minus.Size = new System.Drawing.Size(23, 22);
            this.B_minus.Text = "toolStripButton2";
            this.B_minus.ToolTipText = "Уменьшить";
            this.B_minus.Click += new System.EventHandler(this.B_minus_Click);
            // 
            // label_scale
            // 
            this.label_scale.Name = "label_scale";
            this.label_scale.Size = new System.Drawing.Size(35, 22);
            this.label_scale.Text = "100%";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // B_createPoints
            // 
            this.B_createPoints.BackColor = System.Drawing.Color.DarkTurquoise;
            this.B_createPoints.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.B_createPoints.Image = ((System.Drawing.Image)(resources.GetObject("B_createPoints.Image")));
            this.B_createPoints.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.B_createPoints.Margin = new System.Windows.Forms.Padding(30, 1, 0, 2);
            this.B_createPoints.Name = "B_createPoints";
            this.B_createPoints.Size = new System.Drawing.Size(23, 22);
            this.B_createPoints.Text = "toolStripButton1";
            this.B_createPoints.ToolTipText = "Отрисовать точки";
            this.B_createPoints.Click += new System.EventHandler(this.B_createPoints_Click);
            // 
            // Tool_B_create_flora
            // 
            this.Tool_B_create_flora.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Tool_B_create_flora.Enabled = false;
            this.Tool_B_create_flora.Image = ((System.Drawing.Image)(resources.GetObject("Tool_B_create_flora.Image")));
            this.Tool_B_create_flora.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Tool_B_create_flora.Name = "Tool_B_create_flora";
            this.Tool_B_create_flora.Size = new System.Drawing.Size(23, 22);
            this.Tool_B_create_flora.Text = "toolStripButton1";
            this.Tool_B_create_flora.ToolTipText = "Растительность";
            this.Tool_B_create_flora.Click += new System.EventHandler(this.Tool_B_create_flora_Click);
            // 
            // Tool_B_create_hydrography
            // 
            this.Tool_B_create_hydrography.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Tool_B_create_hydrography.Enabled = false;
            this.Tool_B_create_hydrography.Image = ((System.Drawing.Image)(resources.GetObject("Tool_B_create_hydrography.Image")));
            this.Tool_B_create_hydrography.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Tool_B_create_hydrography.Name = "Tool_B_create_hydrography";
            this.Tool_B_create_hydrography.Size = new System.Drawing.Size(23, 22);
            this.Tool_B_create_hydrography.Text = "toolStripButton1";
            this.Tool_B_create_hydrography.ToolTipText = "Гидрография";
            this.Tool_B_create_hydrography.Click += new System.EventHandler(this.Tool_B_create_hydrography_Click);
            // 
            // Tool_B_create_artificalObject
            // 
            this.Tool_B_create_artificalObject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Tool_B_create_artificalObject.Enabled = false;
            this.Tool_B_create_artificalObject.Image = ((System.Drawing.Image)(resources.GetObject("Tool_B_create_artificalObject.Image")));
            this.Tool_B_create_artificalObject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Tool_B_create_artificalObject.Name = "Tool_B_create_artificalObject";
            this.Tool_B_create_artificalObject.Size = new System.Drawing.Size(23, 22);
            this.Tool_B_create_artificalObject.Text = "toolStripButton2";
            this.Tool_B_create_artificalObject.ToolTipText = "Искусственный объект";
            this.Tool_B_create_artificalObject.Click += new System.EventHandler(this.Tool_B_create_artificalObject_Click);
            // 
            // Tool_B_create_landform
            // 
            this.Tool_B_create_landform.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Tool_B_create_landform.Enabled = false;
            this.Tool_B_create_landform.Image = ((System.Drawing.Image)(resources.GetObject("Tool_B_create_landform.Image")));
            this.Tool_B_create_landform.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Tool_B_create_landform.Name = "Tool_B_create_landform";
            this.Tool_B_create_landform.Size = new System.Drawing.Size(23, 22);
            this.Tool_B_create_landform.Text = "toolStripButton3";
            this.Tool_B_create_landform.ToolTipText = "Рельеф";
            this.Tool_B_create_landform.Click += new System.EventHandler(this.Tool_B_create_landform_Click);
            // 
            // ToolStrip_LandformLevel
            // 
            this.ToolStrip_LandformLevel.Name = "ToolStrip_LandformLevel";
            this.ToolStrip_LandformLevel.Size = new System.Drawing.Size(16, 22);
            // 
            // Tool_B_create_stone
            // 
            this.Tool_B_create_stone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Tool_B_create_stone.Enabled = false;
            this.Tool_B_create_stone.Image = ((System.Drawing.Image)(resources.GetObject("Tool_B_create_stone.Image")));
            this.Tool_B_create_stone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Tool_B_create_stone.Name = "Tool_B_create_stone";
            this.Tool_B_create_stone.Size = new System.Drawing.Size(23, 22);
            this.Tool_B_create_stone.Text = "toolStripButton4";
            this.Tool_B_create_stone.ToolTipText = "Камни(скалы)";
            this.Tool_B_create_stone.Click += new System.EventHandler(this.Tool_B_create_stone_Click);
            // 
            // tool_B_subtype
            // 
            this.tool_B_subtype.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.tool_B_subtype.Image = ((System.Drawing.Image)(resources.GetObject("tool_B_subtype.Image")));
            this.tool_B_subtype.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_B_subtype.Margin = new System.Windows.Forms.Padding(50, 1, 0, 2);
            this.tool_B_subtype.Name = "tool_B_subtype";
            this.tool_B_subtype.Size = new System.Drawing.Size(77, 22);
            this.tool_B_subtype.Text = "Подтип";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStripMenuItem1.Image = global::Go.Properties.Resources.swamp;
            this.toolStripMenuItem1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(230, 28);
            this.toolStripMenuItem1.Text = "toolStripMenuItem1";
            // 
            // Tool_B_area
            // 
            this.Tool_B_area.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Tool_B_area.Enabled = false;
            this.Tool_B_area.Image = ((System.Drawing.Image)(resources.GetObject("Tool_B_area.Image")));
            this.Tool_B_area.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Tool_B_area.Margin = new System.Windows.Forms.Padding(150, 1, 0, 2);
            this.Tool_B_area.Name = "Tool_B_area";
            this.Tool_B_area.Size = new System.Drawing.Size(23, 22);
            this.Tool_B_area.Text = "toolStripButton1";
            this.Tool_B_area.ToolTipText = "Область";
            this.Tool_B_area.Click += new System.EventHandler(this.Tool_B_area_Click);
            // 
            // Tool_B_line
            // 
            this.Tool_B_line.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Tool_B_line.Enabled = false;
            this.Tool_B_line.Image = ((System.Drawing.Image)(resources.GetObject("Tool_B_line.Image")));
            this.Tool_B_line.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Tool_B_line.Name = "Tool_B_line";
            this.Tool_B_line.Size = new System.Drawing.Size(23, 22);
            this.Tool_B_line.Text = "toolStripButton2";
            this.Tool_B_line.ToolTipText = "Линия";
            this.Tool_B_line.Click += new System.EventHandler(this.Tool_B_line_Click);
            // 
            // Tool_B_item
            // 
            this.Tool_B_item.BackColor = System.Drawing.Color.DarkTurquoise;
            this.Tool_B_item.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Tool_B_item.Enabled = false;
            this.Tool_B_item.Image = ((System.Drawing.Image)(resources.GetObject("Tool_B_item.Image")));
            this.Tool_B_item.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Tool_B_item.Name = "Tool_B_item";
            this.Tool_B_item.Size = new System.Drawing.Size(23, 22);
            this.Tool_B_item.Text = "toolStripButton3";
            this.Tool_B_item.ToolTipText = "Объект";
            this.Tool_B_item.Click += new System.EventHandler(this.Tool_B_item_Click);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(149, 22);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem2.Text = "toolStripMenuItem2";
            // 
            // Landform_Plus
            // 
            this.Landform_Plus.Name = "Landform_Plus";
            this.Landform_Plus.Size = new System.Drawing.Size(32, 19);
            // 
            // Landform_Minus
            // 
            this.Landform_Minus.Name = "Landform_Minus";
            this.Landform_Minus.Size = new System.Drawing.Size(32, 19);
            // 
            // PanelWithTypes
            // 
            this.PanelWithTypes.AutoScroll = true;
            this.PanelWithTypes.BackColor = System.Drawing.Color.Azure;
            this.PanelWithTypes.Controls.Add(this.button3);
            this.PanelWithTypes.Controls.Add(this.button4);
            this.PanelWithTypes.Controls.Add(this.button1);
            this.PanelWithTypes.Controls.Add(this.button2);
            this.PanelWithTypes.Controls.Add(this.B_Lake);
            this.PanelWithTypes.Controls.Add(this.B_River);
            this.PanelWithTypes.Location = new System.Drawing.Point(14, 78);
            this.PanelWithTypes.Name = "PanelWithTypes";
            this.PanelWithTypes.Size = new System.Drawing.Size(200, 430);
            this.PanelWithTypes.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(68, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 25);
            this.button1.TabIndex = 14;
            this.button1.Text = "Pond";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(99, 17);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(25, 25);
            this.button2.TabIndex = 15;
            this.button2.Text = "ImpassableSwamp";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(130, 17);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(25, 25);
            this.button3.TabIndex = 16;
            this.button3.Text = "Lake";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(161, 17);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(25, 25);
            this.button4.TabIndex = 17;
            this.button4.Text = "Swamp";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1290, 652);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "Go";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.PanelWithTypes.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label_Way;
        private System.Windows.Forms.Button B_draw;
        private System.Windows.Forms.Button B_show_points;
        private System.Windows.Forms.ImageList smallImageList;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton B_open;
        private System.Windows.Forms.ToolStripButton B_plus;
        private System.Windows.Forms.ToolStripButton B_minus;
        private System.Windows.Forms.ToolStripLabel label_scale;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton B_createPoints;
        private System.Windows.Forms.ToolStripButton Tool_B_create_flora;
        private System.Windows.Forms.ToolStripButton Tool_B_create_hydrography;
        private System.Windows.Forms.ToolStripButton Tool_B_create_artificalObject;
        private System.Windows.Forms.ToolStripButton Tool_B_create_stone;
        private System.Windows.Forms.ToolStripDropDownButton tool_B_subtype;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton Tool_B_area;
        private System.Windows.Forms.ToolStripButton Tool_B_line;
        private System.Windows.Forms.ToolStripButton Tool_B_item;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripButton Tool_B_create_landform;
        private System.Windows.Forms.ToolStripSplitButton ToolStrip_LandformLevel;
        private System.Windows.Forms.ToolStripMenuItem Landform_Plus;
        private System.Windows.Forms.ToolStripMenuItem Landform_Minus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button B_to;
        private System.Windows.Forms.Button B_from;
        private System.Windows.Forms.Button B_Lake;
        private System.Windows.Forms.Button B_River;
        private System.Windows.Forms.Panel PanelWithTypes;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

