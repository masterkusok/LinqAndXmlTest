namespace LinqToXmlExample
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GoodsListView = new System.Windows.Forms.ListView();
            this.SortComboBox = new System.Windows.Forms.ComboBox();
            this.SearchTextBox = new System.Windows.Forms.TextBox();
            this.ApplyFiltersBtn = new System.Windows.Forms.Button();
            this.GoodTypeComboBox = new System.Windows.Forms.ComboBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.SuspendLayout();
            // 
            // GoodsListView
            // 
            this.GoodsListView.BackColor = System.Drawing.Color.Ivory;
            this.GoodsListView.Location = new System.Drawing.Point(12, 83);
            this.GoodsListView.Name = "GoodsListView";
            this.GoodsListView.Size = new System.Drawing.Size(726, 357);
            this.GoodsListView.TabIndex = 0;
            this.GoodsListView.UseCompatibleStateImageBehavior = false;
            // 
            // SortComboBox
            // 
            this.SortComboBox.FormattingEnabled = true;
            this.SortComboBox.Location = new System.Drawing.Point(0, 31);
            this.SortComboBox.Name = "SortComboBox";
            this.SortComboBox.Size = new System.Drawing.Size(151, 28);
            this.SortComboBox.TabIndex = 1;
            this.SortComboBox.Text = "Sort by";
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.Location = new System.Drawing.Point(0, -2);
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new System.Drawing.Size(255, 27);
            this.SearchTextBox.TabIndex = 2;
            this.SearchTextBox.Text = "Search...";
            // 
            // ApplyFiltersBtn
            // 
            this.ApplyFiltersBtn.Location = new System.Drawing.Point(261, -2);
            this.ApplyFiltersBtn.Name = "ApplyFiltersBtn";
            this.ApplyFiltersBtn.Size = new System.Drawing.Size(94, 29);
            this.ApplyFiltersBtn.TabIndex = 3;
            this.ApplyFiltersBtn.Text = "Apply";
            this.ApplyFiltersBtn.UseVisualStyleBackColor = true;
            // 
            // GoodTypeComboBox
            // 
            this.GoodTypeComboBox.FormattingEnabled = true;
            this.GoodTypeComboBox.Location = new System.Drawing.Point(361, 2);
            this.GoodTypeComboBox.Name = "GoodTypeComboBox";
            this.GoodTypeComboBox.Size = new System.Drawing.Size(151, 28);
            this.GoodTypeComboBox.TabIndex = 4;
            this.GoodTypeComboBox.Text = "Type of good";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(547, 31);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(92, 56);
            this.trackBar1.TabIndex = 5;
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(629, 31);
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(109, 56);
            this.trackBar2.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(518, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Goods price: min      max";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 452);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.GoodTypeComboBox);
            this.Controls.Add(this.ApplyFiltersBtn);
            this.Controls.Add(this.SearchTextBox);
            this.Controls.Add(this.SortComboBox);
            this.Controls.Add(this.GoodsListView);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListView GoodsListView;
        private ComboBox SortComboBox;
        private TextBox SearchTextBox;
        private Button ApplyFiltersBtn;
        private ComboBox GoodTypeComboBox;
        private TrackBar trackBar1;
        private TrackBar trackBar2;
        private Label label1;
    }
}