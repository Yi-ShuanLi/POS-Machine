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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(993, 657);
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
            this.discountComboBox.Location = new System.Drawing.Point(741, 647);
            this.discountComboBox.Name = "discountComboBox";
            this.discountComboBox.Size = new System.Drawing.Size(225, 23);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 695);
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
    }
}

