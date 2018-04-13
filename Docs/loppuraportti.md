# Loppuraportti - TTZC0800

* Leevi Kopakkala - K8292 | K8292@student.jamk.fi
* Aku Lehtonen - K9264 | K9264@student.jamk.fi

## Login

Login ruutua varten piti kirjoittaa hieman VB koodia, jotta se oikeasti toimisi:

```VB

Option Compare Database

Private Sub Command1_Click()
Dim User As String
Dim TempPass As String
Dim ID As Integer
Dim UserName As String
Dim TempID As String


If IsNull(Me.txtUsername) Then
 MsgBox "Please enter Username", vbInformation, "Username required"
 Me.txtUsername.SetFocus
ElseIf IsNull(Me.txtPassword) Then
 MsgBox "Please enter Password", vbInformation, "Password required"
 Me.txtPassword.SetFocus
Else
 If (IsNull(DLookup("UserName", "User", "UserName = '" & Me.txtUsername.Value & "' And Password = '" & Me.txtPassword.Value & "'"))) Then
 MsgBox "Invalid Username or Password!"
 Else
 UserName = DLookup("[Username]", "User", "[UserName] = '" & Me.txtUsername.Value & "'")
 TempPass = DLookup("[Password]", "User", "[UserName] = '" & Me.txtUsername.Value & "'")
 DoCmd.OpenForm "Profile Subform", , , "UserName = '" & Me!txtUsername & "'"

 End If
 End If
End Sub

Private Sub Form_Load()
Me.txtUsername.SetFocus
End Sub ```

## Create User

```VB

Option Compare Database

Private Sub Command11_Click()

Dim db As Database
Dim rec As Recordset

Set db = CurrentDb
Set rec = db.OpenRecordset("Select * from User")

rec.AddNew
rec("FirstName") = Me.txtFirstName
rec("LastName") = Me.txtLastname
rec("UserName") = Me.txtUsername
rec("Email") = Me.txtEmail
rec("Password") = Me.txtPassword
rec.Update

Set rec = Nothing
Set db = Nothing
End Sub

```

