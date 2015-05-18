<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GameUI.aspx.cs" Inherits="GOLWebApplicationUI.GameUI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GOL CSharp Server</title>
    <script runat="server">  
        protected void Timer1_Tick ( object sender, EventArgs e )
        {
            _getNextGeneration();
        }
    </script>
</head>
<body>
    <h1>Game of Life</h1>
    <h2>by sldeskins</h2>

    <form id="formSetGameGrid" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <fieldset title="Game Controls">
            <legend>Game Controls</legend>
            <div>
                <div>
                    <asp:Label ID="LabelRows" runat="server" Text="# of rows:"></asp:Label>
                    <asp:TextBox ID="TextBoxRows" runat="server" OnTextChanged="UpdateGameGridRowsColumns">20</asp:TextBox>
                </div>
                <div>
                    <asp:Label ID="LabelColumns" runat="server" Text="# of columns"></asp:Label>
                    <asp:TextBox ID="TextBoxColumns" runat="server" OnTextChanged="UpdateGameGridRowsColumns">30</asp:TextBox>
                </div>
            </div>
            <div>
                <asp:Button ID="ButtonMakeGameGrid" runat="server" OnClick="ResetGameAndLayoutGrid" Text="Make New Game" />
            </div>
        </fieldset>
        <div>
            <table title="hail">
                <tr>
                    <td>
                        <fieldset>
                            <legend>Game Play Controls</legend>
                            <span>
                                <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="2000" Enabled="false" />
                                <asp:Label ID="LabelTimer" runat="server" Text="# of seconds:"></asp:Label>
                                <asp:TextBox ID="TextBoxTimer" runat="server" OnTextChanged="UpdateTimerInterval">2</asp:TextBox>
                                <asp:CheckBox ID="CheckBoxTimeOnOff" runat="server" Text="Turn Timer On/Off (Auto Timer starts after next generation click)" OnCheckedChanged="TurnTimerOnOff" Checked="false" />

                            </span>
                            <div>
                                <span>
                                    <asp:Button ID="ButtonGetNextGeneration" runat="server" Text="Start / Next Generation" OnClick="GetNextGeneration" />
                                </span>
                                <span id="PanelReplayGameArea">
                                    <asp:Button ID="ButtonReplayGame" runat="server" Text="Click Reset Inital State of Last Game" OnClick="ReplayGame" Enabled="false" />
                                </span>
                                <asp:Panel ID="PanelSaveGameArea" runat="server" Visible="false">
                                    <asp:Button ID="ButtonSaveGame" runat="server" Text="Click Here to Save Game" OnClick="SaveGame" />
                                    <asp:TextBox ID="TextBoxSavedDescription" runat="server">&lt; put saved game description here&gt;</asp:TextBox>
                                </asp:Panel>

                            </div>

                        </fieldset>
                    </td>
                    <td>
                        <asp:Panel ID="PanelExampleGames" runat="server" Visible="false">
                            <fieldset>
                                <legend>Example and Saved Games Controls</legend>
                                <div>
                                    <asp:Label ID="LabelExample" runat="server" Text="Example Games" style="vertical-align:top" ></asp:Label>
                                    <asp:ListBox ID="ListBoxExamples" runat="server" style="vertical-align:top" Rows="1" OnSelectedIndexChanged="ListBoxExamples_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                    <asp:TextBox ID="TextBoxExamplesInfo" runat="server" Wrap="true" ScrollBars="Vertical" Rows="5" OnTextChanged="TextBoxExamplesInfo_TextChanged" Width="156px" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <div>
                                    <asp:Button ID="ButtonInitialize" runat="server" Text="Initial Game Board With Selected Game" OnClick="InitialWithExample" Enabled="false" />
                                    <asp:Panel ID="TextBoxExampleGame" runat="server"  >
                                    </asp:Panel>
                                </div>

                            </fieldset>
                        </asp:Panel>
                    </td>
                </tr>
            </table>

        </div>
        <asp:Panel ID="gameMessageArea" runat="server">
            Make a Game Grid
        </asp:Panel>



        <asp:Panel ID="GridAreaPanel" runat="server">
            <triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" />
        </triggers>
        </asp:Panel>

    </form>
</body>
</html>
