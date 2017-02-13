<%@ Page validateRequest="false" Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>
		<link rel="stylesheet" type="text/css" href="StyleSheet.css" />
		<script type="text/javascript">
		    function addLink() {
		        var txta = document.getElementById("textBox");
		        var pageName = document.getElementById("pageList").value + ".html";
		        var start = "<a href='" + pageName + "'>";
		        var end = "</a>";

		        if (txta.selectionStart || txta.selectionStart == '0') {
		            var startPos = txta.selectionStart;
		            var endPos = txta.selectionEnd;
		            var tag_seltxt = start + txta.value.substring(startPos, endPos) + end;
		            txta.value = txta.value.substring(0, startPos) + tag_seltxt + txta.value.substring(endPos, txta.value.length);

		            txta.setSelectionRange((endPos + start.length), (endPos + start.length));
		            txta.focus();
		        }
		        return tag_seltxt;
		    }
		    function textHeader() {
		        var txta = document.getElementById("textBox");
		        var start = "<h3>";
		        var end = "</h3>";

		        if (txta.selectionStart || txta.selectionStart == '0') {
		            var startPos = txta.selectionStart;
		            var endPos = txta.selectionEnd;
		            var tag_seltxt = start + txta.value.substring(startPos, endPos) + end;
		            txta.value = txta.value.substring(0, startPos) + tag_seltxt + txta.value.substring(endPos, txta.value.length);

		            txta.setSelectionRange((endPos + start.length), (endPos + start.length));
		            txta.focus();
		        }
		        return tag_seltxt;
		    }
		    function lineBreak() {
		        var txta = document.getElementById("textBox");
		        var start = "</br>";

		        if (txta.selectionStart || txta.selectionStart == '0') {
		            var startPos = txta.selectionStart;
		            var endPos = txta.selectionEnd;
		            txta.value = txta.value.substring(0, startPos) + start + txta.value.substring(endPos, txta.value.length);

		            txta.setSelectionRange((endPos + start.length), (endPos + start.length));
		            txta.focus();
		        }
		        return start;
		    }
		    
		  </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="errorLabel" runat="server"></asp:Label>
        <div id="navbar">
            <div id="navbarCenter">
				<asp:DropDownList ID="redirectList" runat="server">
                </asp:DropDownList>
                <asp:Button ID="redirectButton" runat="server" Text="Open Page" OnClick="redirectButton_Click" />
                <asp:Button ID="editButton" runat="server" Text="Edit Page" OnClick="editButton_Click" />
                <asp:Button ID="deleteButton" runat="server" Text="Delete Page" OnClick="deleteButton_Click" Enabled="False" />
                <asp:CheckBox ID="deleteEnable" runat="server" AutoPostBack="True" OnCheckedChanged="deleteEnable_CheckedChanged" />
            </div>
            <asp:Label ID="folderLabel" runat="server"></asp:Label>
		</div>
        <div id="sideBar">
            <asp:DropDownList ID="pageList" runat="server">
            </asp:DropDownList>	
            <button id="linkButton" type="button" onClick="addLink();">Link Text</button>
            <asp:FileUpload ID="imageUpload" runat="server" />
            <asp:Button ID="imageSubmit" runat="server" Text="Upload" OnClick="imageSubmit_Click" />
            <button id="headButton" type="button" onClick="textHeader();">Text is Header</button>
            <button id="breakButton" type="button" onClick="lineBreak();">Line Break</button>
            
        </div>
        <div id="sideBar2">
            <asp:TextBox ID="newFolderBox" runat="server"></asp:TextBox>
            <asp:Button ID="newFolderButton" runat="server" Text="Create Folder" OnClick="newFolderButton_Click" />
            <asp:DropDownList ID="folderList" runat="server"></asp:DropDownList>
            <asp:Button ID="folderButton" runat="server" Text="Open Folder" OnClick="folderButton_Click" />
            <asp:Button ID="deleteFolderButton" runat="server" Text="Delete Active Folder" Enabled="False" OnClick="deleteFolderButton_Click" />
        </div>
        <div id="main">
			<div id="container">
                <asp:TextBox ID="titleBox" runat="server"></asp:TextBox>
                <asp:Button ID="submitButton" runat="server" Text="Submit" OnClick="submitButton_Click" />&nbsp;
				<asp:TextBox ID="textBox" runat="server" TextMode="MultiLine"></asp:TextBox>
			</div>
		</div>
    </form>
</body>
</html>
