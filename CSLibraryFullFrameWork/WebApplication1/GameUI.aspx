<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GameUI.aspx.cs" Inherits="WebApplication1.GameUI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script runat="server">  
        protected void Timer1_Tick ( object sender, EventArgs e )
        {
            _getNextGeneration();
        }

    </script>
</head>
<body>
    <h1>Game of Life</h1>

    <form id="formSetGameGrid" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="10000" Enabled="false" />
        <fieldset title="Game Controls">
            <div>
                <span>
                    <div>
                        <asp:Label ID="LabelRows" runat="server" Text="# of rows:"></asp:Label>
                        <asp:TextBox ID="TextBoxRows" runat="server" OnTextChanged="UpdateGameGridRowsColumns">20</asp:TextBox>
                    </div>
                    <div>
                        <asp:Label ID="LabelColumns" runat="server" Text="# of columns"></asp:Label>
                        <asp:TextBox ID="TextBoxColumns" runat="server" OnTextChanged="UpdateGameGridRowsColumns">30</asp:TextBox>
                    </div>
                </span>

            </div>
            <div>
                <asp:Button ID="ButtonMakeGameGrid" runat="server" OnClick="ResetGameAndLayoutGrid" Text="Make New Game" />

            </div>
        </fieldset>
        <fieldset>
            <span>
                <asp:Label ID="LabelTimer" runat="server" Text="# of seconds"></asp:Label>
                <asp:TextBox ID="TextBoxTimer" runat="server" OnTextChanged="UpdateTimerInterval">2</asp:TextBox>
                <asp:CheckBox ID="CheckBoxTimeOnOff" runat="server" Text="Turn Timer On/Off (Auto Timer starts after next generation click)" OnCheckedChanged="TurnTimerOnOff" Checked="false" />

            </span>
            <div>
                <asp:Button ID="ButtonGetNextGeneration" runat="server" Text="Start / Next Generation" OnClick="GetNextGeneration" />
                <asp:Panel ID="PanelSaveGameArea" runat="server" Visible="false">
                     <asp:Button ID="ButtonSaveGame" runat="server" Text="Click Here to Save Game" OnClick="SaveGame" />
                    <asp:TextBox ID="TextBoxSavedDescription" runat="server">&lg; put saved game description here&gt;</asp:TextBox>
               </asp:Panel>
                <div>
                    <%--   <asp:Button ID="ButtonStart" runat="server" Text="Start" OnClick="StartNextGeneration" />
                      <asp:Button ID="ButtonPause" runat="server" Text="Pause" />
                    <asp:Button ID="ButtonStop" runat="server" Text="Stop" OnClick="StopNextGeneration" />--%>
                </div>
            </div>

        </fieldset>
        <asp:Panel ID="gameMessageArea" runat="server">
            Make a Game Grid
        </asp:Panel>



        <asp:Panel ID="GridAreaPanel" runat="server" UpdateMode="Conditional">
            <triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" />
        </triggers>
        </asp:Panel>

    </form>
</body>
</html>
