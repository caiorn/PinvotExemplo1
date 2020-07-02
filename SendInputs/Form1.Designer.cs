namespace SendInputs
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnDesenhar = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnCapturarTexto = new System.Windows.Forms.Button();
            this.bntEnviarClique = new System.Windows.Forms.Button();
            this.txtNomeButton = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAlterarTextoEdit = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnDesenhar
            // 
            this.btnDesenhar.Location = new System.Drawing.Point(31, 191);
            this.btnDesenhar.Name = "btnDesenhar";
            this.btnDesenhar.Size = new System.Drawing.Size(70, 23);
            this.btnDesenhar.TabIndex = 0;
            this.btnDesenhar.Text = "Desenhar";
            this.btnDesenhar.UseVisualStyleBackColor = true;
            this.btnDesenhar.Click += new System.EventHandler(this.btnDesenhar_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 25);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.Click += new System.EventHandler(this.comboBox1_Click);
            // 
            // btnCapturarTexto
            // 
            this.btnCapturarTexto.Location = new System.Drawing.Point(158, 8);
            this.btnCapturarTexto.Name = "btnCapturarTexto";
            this.btnCapturarTexto.Size = new System.Drawing.Size(149, 23);
            this.btnCapturarTexto.TabIndex = 3;
            this.btnCapturarTexto.Text = "Capture Text";
            this.toolTip1.SetToolTip(this.btnCapturarTexto, "Selecione o processo.");
            this.btnCapturarTexto.UseVisualStyleBackColor = true;
            this.btnCapturarTexto.Click += new System.EventHandler(this.captureTextHandle_Click);
            // 
            // bntEnviarClique
            // 
            this.bntEnviarClique.Location = new System.Drawing.Point(12, 96);
            this.bntEnviarClique.Name = "bntEnviarClique";
            this.bntEnviarClique.Size = new System.Drawing.Size(89, 23);
            this.bntEnviarClique.TabIndex = 5;
            this.bntEnviarClique.Text = "Enviar Clique";
            this.toolTip1.SetToolTip(this.bntEnviarClique, "Selecione o formulario e escreva o nome do botao acima ou selecione o numero da k" +
        "ey, para demonstração do clique!");
            this.bntEnviarClique.UseVisualStyleBackColor = true;
            this.bntEnviarClique.Click += new System.EventHandler(this.btnEnviarClique_Click);
            // 
            // txtNomeButton
            // 
            this.txtNomeButton.Location = new System.Drawing.Point(12, 70);
            this.txtNomeButton.Name = "txtNomeButton";
            this.txtNomeButton.Size = new System.Drawing.Size(105, 20);
            this.txtNomeButton.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Nome do botão ou label:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Processo:";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(158, 37);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(200, 177);
            this.listView1.TabIndex = 10;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Key (handle)";
            this.columnHeader1.Width = 73;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "value";
            this.columnHeader2.Width = 124;
            // 
            // btnAlterarTextoEdit
            // 
            this.btnAlterarTextoEdit.Location = new System.Drawing.Point(12, 125);
            this.btnAlterarTextoEdit.Name = "btnAlterarTextoEdit";
            this.btnAlterarTextoEdit.Size = new System.Drawing.Size(89, 23);
            this.btnAlterarTextoEdit.TabIndex = 12;
            this.btnAlterarTextoEdit.Text = "Alterar Texto";
            this.toolTip1.SetToolTip(this.btnAlterarTextoEdit, "Escreva algo no campo de texto do outro forumário, clique em Capture Text e veja " +
        "a key que corresponde o value que foi digitado e insira no campo\r\n acima.");
            this.btnAlterarTextoEdit.UseVisualStyleBackColor = true;
            this.btnAlterarTextoEdit.Click += new System.EventHandler(this.btnAlterarTextoEdit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 216);
            this.Controls.Add(this.btnAlterarTextoEdit);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNomeButton);
            this.Controls.Add(this.bntEnviarClique);
            this.Controls.Add(this.btnCapturarTexto);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnDesenhar);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pinvot test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDesenhar;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnCapturarTexto;
        private System.Windows.Forms.Button bntEnviarClique;
        private System.Windows.Forms.TextBox txtNomeButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btnAlterarTextoEdit;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

