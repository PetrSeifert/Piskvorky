namespace Piskvorky
{
    partial class Form1
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            this.playBoard1 = new Piskvorky.PlayBoard();
            this.SuspendLayout();
            // 
            // playBoard1
            // 
            this.playBoard1.BoardSize = 19;
            this.playBoard1.ColorGrid = System.Drawing.Color.Red;
            this.playBoard1.FieldSize = 20;
            this.playBoard1.Location = new System.Drawing.Point(12, 12);
            this.playBoard1.Name = "playBoard1";
            this.playBoard1.Size = new System.Drawing.Size(386, 384);
            this.playBoard1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 406);
            this.Controls.Add(this.playBoard1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private PlayBoard playBoard1;
    }
}

