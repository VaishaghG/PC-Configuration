Module FormTracker
    Public Sub ClearTable()
        ' ✅ SQL Server Connection String (Using Windows Authentication)
        Dim connectionString As String = "server=JARVIS;database=mydb;TrustServerCertificate=True;Integrated Security=True"
        Dim query As String = "DELETE FROM cart;"

        ' ✅ Use SqlConnection for SQL Server
        Using conn As New Microsoft.Data.SqlClient.SqlConnection(connectionString)
            Try
                conn.Open()
                Using cmd As New Microsoft.Data.SqlClient.SqlCommand(query, conn)
                    cmd.ExecuteNonQuery()
                End Using
            Catch ex As Exception
                MessageBox.Show("Error clearing table: " & ex.Message)
            End Try
        End Using
    End Sub

End Module
