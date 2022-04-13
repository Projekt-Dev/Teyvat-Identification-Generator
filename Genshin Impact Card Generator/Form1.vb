

'When finishing laying out textboxes use event handler for imageUpdate()
'Find a different font to use for UUID and one for subheader
'Find a UI layout and color scheme
Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FormDefaults()
        Me.MinimumSize = New Size(1280, 720)
        Me.MaximumSize = New Size(1280, 720)
    End Sub
    'Anything we would want to run on form load put in here.
    Private Sub FormDefaults()
        Images.GrabImages()
        pnlAvatars.AutoScroll = False
        pnlAvatars.HorizontalScroll.Enabled = False
        pnlAvatars.AutoScroll = True

    End Sub


#Region "Button Events"
    Private Sub TxtBoxUUID_TextChanged(sender As Object, e As EventArgs) Handles txtBoxUUID.TextChanged
        'Check for character limit of 9
        Dim charLimit = 9
        Dim currentCharCount = txtBoxUUID.Text.Length
        If currentCharCount > charLimit Then
            MessageBox.Show("UUID's are 9 numbers long.")
            txtBoxUUID.Text = txtBoxUUID.Text.Remove(txtBoxUUID.Text.Length - 1)
        Else
            Images.ImageUpdate()
        End If
    End Sub
    'Makes the TextBox only allow numbers
    Private Sub TxtBoxUUID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBoxUUID.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso (e.KeyChar <> ".") Then
            e.Handled = True
        End If
    End Sub
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Images.ImageSave()
    End Sub

#End Region

End Class