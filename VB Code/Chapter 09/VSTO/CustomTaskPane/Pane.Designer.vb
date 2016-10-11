<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NamePane
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.btnInsert = New System.Windows.Forms.Button
        Me.lstItems = New System.Windows.Forms.ListBox
        Me.SuspendLayout()
        '
        'btnInsert
        '
        Me.btnInsert.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnInsert.Location = New System.Drawing.Point(0, 127)
        Me.btnInsert.Name = "btnInsert"
        Me.btnInsert.Size = New System.Drawing.Size(150, 23)
        Me.btnInsert.TabIndex = 0
        Me.btnInsert.Text = "Insert"
        Me.btnInsert.UseVisualStyleBackColor = True
        '
        'lstItems
        '
        Me.lstItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstItems.FormattingEnabled = True
        Me.lstItems.Location = New System.Drawing.Point(0, 0)
        Me.lstItems.Name = "lstItems"
        Me.lstItems.Size = New System.Drawing.Size(150, 121)
        Me.lstItems.TabIndex = 2
        '
        'NamePane
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lstItems)
        Me.Controls.Add(Me.btnInsert)
        Me.Name = "NamePane"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnInsert As System.Windows.Forms.Button
    Friend WithEvents lstItems As System.Windows.Forms.ListBox

End Class
