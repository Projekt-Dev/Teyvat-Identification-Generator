<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.pb1 = New System.Windows.Forms.PictureBox()
        Me.btnUUID = New System.Windows.Forms.Button()
        Me.txtBoxUUID = New System.Windows.Forms.TextBox()
        Me.txtBoxName = New System.Windows.Forms.TextBox()
        Me.pbTest = New System.Windows.Forms.PictureBox()
        Me.pnlImages = New System.Windows.Forms.Panel()
        CType(Me.pb1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbTest, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlImages.SuspendLayout()
        Me.SuspendLayout()
        '
        'pb1
        '
        Me.pb1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pb1.Location = New System.Drawing.Point(0, 0)
        Me.pb1.Name = "pb1"
        Me.pb1.Size = New System.Drawing.Size(420, 200)
        Me.pb1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pb1.TabIndex = 0
        Me.pb1.TabStop = False
        '
        'btnUUID
        '
        Me.btnUUID.Font = New System.Drawing.Font("Inter", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUUID.Location = New System.Drawing.Point(426, 205)
        Me.btnUUID.Name = "btnUUID"
        Me.btnUUID.Size = New System.Drawing.Size(75, 23)
        Me.btnUUID.TabIndex = 1
        Me.btnUUID.Text = "Set UUID"
        Me.btnUUID.UseVisualStyleBackColor = True
        '
        'txtBoxUUID
        '
        Me.txtBoxUUID.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.txtBoxUUID.Location = New System.Drawing.Point(12, 208)
        Me.txtBoxUUID.Name = "txtBoxUUID"
        Me.txtBoxUUID.Size = New System.Drawing.Size(408, 20)
        Me.txtBoxUUID.TabIndex = 2
        '
        'txtBoxName
        '
        Me.txtBoxName.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.txtBoxName.Location = New System.Drawing.Point(12, 234)
        Me.txtBoxName.Name = "txtBoxName"
        Me.txtBoxName.Size = New System.Drawing.Size(408, 20)
        Me.txtBoxName.TabIndex = 4
        '
        'pbTest
        '
        Me.pbTest.BackgroundImage = Global.Genshin_Impact_Card_Generator.My.Resources.Resources.Achievement__2_
        Me.pbTest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pbTest.Location = New System.Drawing.Point(30, 0)
        Me.pbTest.Name = "pbTest"
        Me.pbTest.Size = New System.Drawing.Size(420, 200)
        Me.pbTest.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pbTest.TabIndex = 5
        Me.pbTest.TabStop = False
        '
        'pnlImages
        '
        Me.pnlImages.Controls.Add(Me.pbTest)
        Me.pnlImages.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlImages.Location = New System.Drawing.Point(782, 0)
        Me.pnlImages.Name = "pnlImages"
        Me.pnlImages.Size = New System.Drawing.Size(482, 681)
        Me.pnlImages.TabIndex = 6
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1264, 681)
        Me.Controls.Add(Me.pnlImages)
        Me.Controls.Add(Me.txtBoxName)
        Me.Controls.Add(Me.txtBoxUUID)
        Me.Controls.Add(Me.btnUUID)
        Me.Controls.Add(Me.pb1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.pb1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbTest, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlImages.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pb1 As PictureBox
    Friend WithEvents btnUUID As Button
    Friend WithEvents txtBoxUUID As TextBox
    Friend WithEvents txtBoxName As TextBox
    Friend WithEvents pbTest As PictureBox
    Friend WithEvents pnlImages As Panel
End Class
