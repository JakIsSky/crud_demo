
Imports MySql.Data.MySqlClient

Public Class Form1
    Dim conn As MySqlConnection
    Dim COMMAND As MySqlCommand

    Private Sub ButtonConnect_Click(sender As Object, e As EventArgs) Handles ButtonConnect.Click
        conn = New MySqlConnection
        conn.ConnectionString = "server=localhost; userid=root; password=root; database=crud_demo_db;"

        Try
            conn.Open()
            MessageBox.Show("Connected")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            conn.Close()
        End Try
    End Sub

    Private Sub ButtonInsert_Click(sender As Object, e As EventArgs) Handles ButtonInsert.Click
        Dim query As String = "INSERT INTO student_tbl (name, age, email) VALUES (@name, @age, @email)"
        Try
            Using conn As New MySqlConnection("server=localhost; userid=root; password=root; database=crud_demo_db;")
                conn.Open()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@name", TextBoxName.Text)
                    cmd.Parameters.AddWithValue("@age", CInt(TextBoxAge.Text))
                    cmd.Parameters.AddWithValue("@email", TextBoxEmail.Text)
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Record insert successfully!")
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ButtonRead_Click(sender As Object, e As EventArgs) Handles ButtonRead.Click
        Dim query As String = "SELECT * FROM crud_demo_db.student_tbl;"
        Try
            Using conn As New MySqlConnection("server=localhost; userid=root; password=root; database=crud_demo_db;")
                Dim adapter As New MySqlDataAdapter(query, conn) ' Get from Database
                Dim table As New DataTable() ' Table Object
                adapter.Fill(table) ' From Adapter to Table Object
                DataGridView1.DataSource = table ' Display to DataGridView
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ButtonEdit_Click(sender As Object, e As EventArgs) Handles ButtonEdit.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Select a row to update.")
        End If
        Dim row As DataGridViewRow = DataGridView1.SelectedRows(0)
        Dim Id As Integer = row.Cells("id").Value

        Dim query As String = "UPDATE student_tbl SET name = @name, age = @age, email = @email WHERE id = @id"
        Try
            Using conn As New MySqlConnection("server=localhost; userid=root; password=root; database=crud_demo_db;")
                conn.Open()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@name", TextBoxName.Text)
                    cmd.Parameters.AddWithValue("@age", CInt(TextBoxAge.Text))
                    cmd.Parameters.AddWithValue("@email", TextBoxEmail.Text)
                    cmd.Parameters.AddWithValue("@id", Id)
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Record updated successfully!")
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ButtonDelete_Click(sender As Object, e As EventArgs) Handles ButtonDelete.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Select a row to delete.")
        End If
        Dim row As DataGridViewRow = DataGridView1.SelectedRows(0)
        Dim Id As Integer = row.Cells("id").Value

        If MessageBox.Show("Delete this record?", "Confirm", MessageBoxButtons.YesNo) = DialogResult.No Then
            Return
        End If

        Dim query As String = "DELETE FROM student_tbl WHERE id = @id"
        Try
            Using conn As New MySqlConnection("server=localhost; userid=root; password=root; database=crud_demo_db;")
                conn.Open()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", Id)
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Record deleted successfully!")
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            TextBoxName.Text = selectedRow.Cells("name").Value.ToString()
            TextBoxAge.Text = selectedRow.Cells("age").Value.ToString()
            TextBoxEmail.Text = selectedRow.Cells("email").Value.ToString()

            TextBoxHiddenId.Text = selectedRow.Cells("id").Value.ToString()

        End If
    End Sub
End Class