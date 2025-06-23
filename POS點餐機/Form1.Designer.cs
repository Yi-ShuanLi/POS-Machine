namespace POS點餐機
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.discountComboBox = new System.Windows.Forms.ComboBox();
            this.foodsContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.reson_Lab = new System.Windows.Forms.Label();
            this.enableAIRecommend_checkbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1171, 649);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "0";
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.AutoScroll = true;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(734, 31);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(420, 589);
            this.flowLayoutPanel5.TabIndex = 3;
            // 
            // discountComboBox
            // 
            this.discountComboBox.FormattingEnabled = true;
            this.discountComboBox.Location = new System.Drawing.Point(836, 646);
            this.discountComboBox.Name = "discountComboBox";
            this.discountComboBox.Size = new System.Drawing.Size(295, 23);
            this.discountComboBox.TabIndex = 4;
            this.discountComboBox.SelectedIndexChanged += new System.EventHandler(this.DiscountComboBox_SelectedIndexChanged);
            // 
            // foodsContainer
            // 
            this.foodsContainer.AutoScroll = true;
            this.foodsContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.foodsContainer.Location = new System.Drawing.Point(27, 13);
            this.foodsContainer.Name = "foodsContainer";
            this.foodsContainer.Size = new System.Drawing.Size(650, 658);
            this.foodsContainer.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1171, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "推薦理由:";
            // 
            // reson_Lab
            // 
            this.reson_Lab.Location = new System.Drawing.Point(1171, 63);
            this.reson_Lab.Name = "reson_Lab";
            this.reson_Lab.Size = new System.Drawing.Size(236, 557);
            this.reson_Lab.TabIndex = 6;
            // 
            // enableAIRecommend_checkbox
            // 
            this.enableAIRecommend_checkbox.AutoSize = true;
            this.enableAIRecommend_checkbox.Location = new System.Drawing.Point(726, 648);
            this.enableAIRecommend_checkbox.Name = "enableAIRecommend_checkbox";
            this.enableAIRecommend_checkbox.Size = new System.Drawing.Size(104, 19);
            this.enableAIRecommend_checkbox.TabIndex = 7;
            this.enableAIRecommend_checkbox.Text = "啟用AI推薦";
            this.enableAIRecommend_checkbox.UseVisualStyleBackColor = true;
            this.enableAIRecommend_checkbox.CheckedChanged += new System.EventHandler(this.DiscountComboBox_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1439, 695);
            this.Controls.Add(this.enableAIRecommend_checkbox);
            this.Controls.Add(this.reson_Lab);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.foodsContainer);
            this.Controls.Add(this.discountComboBox);
            this.Controls.Add(this.flowLayoutPanel5);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.ComboBox discountComboBox;
        private System.Windows.Forms.FlowLayoutPanel foodsContainer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label reson_Lab;
        private System.Windows.Forms.CheckBox enableAIRecommend_checkbox;
    }
}

