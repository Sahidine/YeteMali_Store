namespace YeteMali
{
    partial class ImportationBilanPassif
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Button btImporter;
            System.Windows.Forms.Button btEnregistrer;
            this.rectangleShape1 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.dgvImporation = new System.Windows.Forms.DataGridView();
            this.cbdate = new System.Windows.Forms.DateTimePicker();
            this.cbCaisse = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            btImporter = new System.Windows.Forms.Button();
            btEnregistrer = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImporation)).BeginInit();
            this.SuspendLayout();
            // 
            // rectangleShape1
            // 
            this.rectangleShape1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rectangleShape1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.rectangleShape1.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.rectangleShape1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.rectangleShape1.FillColor = System.Drawing.Color.Lime;
            this.rectangleShape1.FillGradientColor = System.Drawing.Color.Lime;
            this.rectangleShape1.Location = new System.Drawing.Point(-1, 0);
            this.rectangleShape1.Name = "rectangleShape1";
            this.rectangleShape1.Size = new System.Drawing.Size(1649, 72);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(1576, 272);
            this.shapeContainer1.TabIndex = 0;
            this.shapeContainer1.TabStop = false;
            // 
            // dgvImporation
            // 
            this.dgvImporation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvImporation.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvImporation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvImporation.Location = new System.Drawing.Point(-4, 74);
            this.dgvImporation.Name = "dgvImporation";
            this.dgvImporation.Size = new System.Drawing.Size(1580, 195);
            this.dgvImporation.TabIndex = 78;
            // 
            // cbdate
            // 
            this.cbdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbdate.Location = new System.Drawing.Point(509, 41);
            this.cbdate.Name = "cbdate";
            this.cbdate.Size = new System.Drawing.Size(265, 26);
            this.cbdate.TabIndex = 74;
            // 
            // cbCaisse
            // 
            this.cbCaisse.DisplayMember = "NomCaisse";
            this.cbCaisse.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCaisse.FormattingEnabled = true;
            this.cbCaisse.Location = new System.Drawing.Point(78, 40);
            this.cbCaisse.Name = "cbCaisse";
            this.cbCaisse.Size = new System.Drawing.Size(249, 28);
            this.cbCaisse.TabIndex = 73;
            this.cbCaisse.ValueMember = "Id";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(396, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 20);
            this.label1.TabIndex = 75;
            this.label1.Text = "Date de suivi  :";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 76;
            this.label2.Text = "Caisse  :";
            // 
            // btImporter
            // 
            btImporter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            btImporter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            btImporter.FlatAppearance.BorderSize = 0;
            btImporter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btImporter.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btImporter.ForeColor = System.Drawing.Color.White;
            btImporter.Image = global::YeteMali.Properties.Resources.ddddds3;
            btImporter.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            btImporter.Location = new System.Drawing.Point(1193, 29);
            btImporter.Name = "btImporter";
            btImporter.Size = new System.Drawing.Size(140, 37);
            btImporter.TabIndex = 72;
            btImporter.Text = "Importer";
            btImporter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            btImporter.UseVisualStyleBackColor = false;
            btImporter.Click += new System.EventHandler(this.btImporter_Click);
            // 
            // btEnregistrer
            // 
            btEnregistrer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            btEnregistrer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            btEnregistrer.FlatAppearance.BorderSize = 0;
            btEnregistrer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btEnregistrer.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btEnregistrer.ForeColor = System.Drawing.Color.White;
            btEnregistrer.Image = global::YeteMali.Properties.Resources.check_24849_960_7201;
            btEnregistrer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btEnregistrer.Location = new System.Drawing.Point(1346, 29);
            btEnregistrer.Name = "btEnregistrer";
            btEnregistrer.Size = new System.Drawing.Size(164, 37);
            btEnregistrer.TabIndex = 71;
            btEnregistrer.Text = "Enregistrer";
            btEnregistrer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            btEnregistrer.UseVisualStyleBackColor = false;
            btEnregistrer.Click += new System.EventHandler(this.btEnregistrer_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(294, 20);
            this.label3.TabIndex = 79;
            this.label3.Text = "Importer les données pour le bilan passif";
            // 
            // ImportationBilanPassif
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1576, 272);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvImporation);
            this.Controls.Add(this.cbdate);
            this.Controls.Add(this.cbCaisse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(btImporter);
            this.Controls.Add(btEnregistrer);
            this.Controls.Add(this.shapeContainer1);
            this.Name = "ImportationBilanPassif";
            this.Text = "ImportationBilanPassif";
            this.Load += new System.EventHandler(this.ImportationBilanPassif_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvImporation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private System.Windows.Forms.DataGridView dgvImporation;
        private System.Windows.Forms.DateTimePicker cbdate;
        private System.Windows.Forms.ComboBox cbCaisse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}