namespace Finanzas.Forms.Movimientos
{
    partial class frmEdición
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtImporte = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbTransacciones = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbRubros = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtFecha = new System.Windows.Forms.DateTimePicker();
            this.cbCuentas = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(390, 191);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancelar);
            this.panel1.Controls.Add(this.btnAceptar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 159);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(384, 29);
            this.panel1.TabIndex = 0;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(300, 3);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnAceptar
            // 
            this.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAceptar.Location = new System.Drawing.Point(219, 3);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 0;
            this.btnAceptar.Text = "&Grabar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtImporte);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.cbTransacciones);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.cbRubros);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.dtFecha);
            this.panel2.Controls.Add(this.cbCuentas);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(384, 150);
            this.panel2.TabIndex = 1;
            // 
            // txtImporte
            // 
            this.txtImporte.Location = new System.Drawing.Point(99, 116);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(145, 20);
            this.txtImporte.TabIndex = 4;
            this.txtImporte.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtImporte.Leave += new System.EventHandler(this.txtImporte_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Importe:";
            // 
            // cbTransacciones
            // 
            this.cbTransacciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTransacciones.FormattingEnabled = true;
            this.cbTransacciones.Location = new System.Drawing.Point(99, 89);
            this.cbTransacciones.Name = "cbTransacciones";
            this.cbTransacciones.Size = new System.Drawing.Size(261, 21);
            this.cbTransacciones.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Transacción:";
            // 
            // cbRubros
            // 
            this.cbRubros.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRubros.FormattingEnabled = true;
            this.cbRubros.Location = new System.Drawing.Point(99, 62);
            this.cbRubros.Name = "cbRubros";
            this.cbRubros.Size = new System.Drawing.Size(261, 21);
            this.cbRubros.TabIndex = 2;
            this.cbRubros.SelectedIndexChanged += new System.EventHandler(this.cbRubros_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Rubro:";
            // 
            // dtFecha
            // 
            this.dtFecha.CustomFormat = "dddd, dd/MM/yyyy";
            this.dtFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFecha.Location = new System.Drawing.Point(99, 36);
            this.dtFecha.Name = "dtFecha";
            this.dtFecha.Size = new System.Drawing.Size(145, 20);
            this.dtFecha.TabIndex = 1;
            // 
            // cbCuentas
            // 
            this.cbCuentas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCuentas.FormattingEnabled = true;
            this.cbCuentas.Location = new System.Drawing.Point(99, 9);
            this.cbCuentas.Name = "cbCuentas";
            this.cbCuentas.Size = new System.Drawing.Size(261, 21);
            this.cbCuentas.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Fecha:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cuenta:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmEdición
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(390, 191);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmEdición";
            this.Text = "frmEdición";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbCuentas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.DateTimePicker dtFecha;
        private System.Windows.Forms.ComboBox cbTransacciones;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbRubros;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtImporte;
        private System.Windows.Forms.Label label5;
    }
}