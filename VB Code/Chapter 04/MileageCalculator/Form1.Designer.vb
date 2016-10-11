<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.numMileage = New System.Windows.Forms.NumericUpDown
        Me.numRate = New System.Windows.Forms.Label
        Me.numReimbursement = New System.Windows.Forms.Label
        Me.btnSubmit = New System.Windows.Forms.Button
        CType(Me.numMileage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'numMileage
        '
        Me.numMileage.Location = New System.Drawing.Point(12, 12)
        Me.numMileage.Name = "numMileage"
        Me.numMileage.Size = New System.Drawing.Size(120, 20)
        Me.numMileage.TabIndex = 0
        '
        'numRate
        '
        Me.numRate.AutoSize = True
        Me.numRate.Location = New System.Drawing.Point(12, 51)
        Me.numRate.Name = "numRate"
        Me.numRate.Size = New System.Drawing.Size(30, 13)
        Me.numRate.TabIndex = 1
        Me.numRate.Text = "Rate"
        '
        'numReimbursement
        '
        Me.numReimbursement.AutoSize = True
        Me.numReimbursement.Location = New System.Drawing.Point(12, 82)
        Me.numReimbursement.Name = "numReimbursement"
        Me.numReimbursement.Size = New System.Drawing.Size(80, 13)
        Me.numReimbursement.TabIndex = 2
        Me.numReimbursement.Text = "Reimbursement"
        '
        'btnSubmit
        '
        Me.btnSubmit.Location = New System.Drawing.Point(15, 125)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(75, 23)
        Me.btnSubmit.TabIndex = 3
        Me.btnSubmit.Text = "Calculate"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 273)
        Me.Controls.Add(Me.btnSubmit)
        Me.Controls.Add(Me.numReimbursement)
        Me.Controls.Add(Me.numRate)
        Me.Controls.Add(Me.numMileage)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.numMileage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents numMileage As System.Windows.Forms.NumericUpDown
    Friend WithEvents numRate As System.Windows.Forms.Label
    Friend WithEvents numReimbursement As System.Windows.Forms.Label
    Friend WithEvents btnSubmit As System.Windows.Forms.Button

End Class
