<%@ Page Title="" Language="C#" MasterPageFile="~/PSIsystem.Master" AutoEventWireup="true" CodeBehind="PurchaseAdd.aspx.cs" Inherits="PSIsystem.PurchaseAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .jumbotron {
            background-color: snow !important;
        }

        #context {
            margin-top: -70px;
            height:200px;
            overflow:auto;
        }
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="jumbotron">
        <h1>進貨單管理</h1>
        單據編號:<asp:TextBox ID="IDText" runat="server" Enabled="false"></asp:TextBox><br />
        進貨日期:<asp:TextBox ID="PurchaseDate" runat="server" TextMode="Date"></asp:TextBox>
        進貨時間:<asp:TextBox ID="PurchaseTime" runat="server" TextMode="Time"></asp:TextBox><br />
        <button type="button" data-toggle="modal" data-target="#Order" id="AddOrderBtn">+</button>

    </div>
    <div id="context">
        <table class="table">
            <thead>
                <tr>
                    <th>商品編號</th>
                    <th>商品名稱</th>
                    <th>單據</th>
                    <th>數量</th>
                    <th>小計</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="ProductRepeater" runat="server" OnItemCommand="ProductRepeater_ItemCommand">
                    <ItemTemplate>
                        <tr>
                                <td>
                                <asp:Button ID="DeleteItem" runat="server" Text="刪除" CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('確定要刪除嗎？');"/>
                                <%# Eval("ID") %>
                                </td>
                            <td><%#Eval("Name") %></td>
                            <td><%#Eval("Price") %></td>
                            <td><%#Eval("Count") %></td>
                            <td><%#Eval("Total") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    <div>
        總計:<asp:Label ID="TotalMoney" runat="server" Text="0"></asp:Label>
    </div>

    <div id="Order" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="modal-title ">
                        選擇商品
                    </div>
                    <button type="button" class="close" data-dismiss="modal">
                        <span>&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    商品:<asp:DropDownList ID="ProductList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ProductList_SelectedIndexChanged"></asp:DropDownList><br />
                    單價:<asp:Label ID="Price" runat="server" Text="0"></asp:Label><br />
                    數量:<asp:TextBox ID="Count" runat="server" TextMode="Number" min="0" MaxLength="10">0</asp:TextBox><br />
                    小計:<asp:TextBox ID="Total" runat="server" BorderStyle="None">0</asp:TextBox>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="AddProduct" runat="server" Text="加入" OnClick="AddProduct_Click" OnClientClick="return confirm('確定要新增嗎？');"/>
                    <button class="btn">取消</button>
                </div>
            </div>
        </div>
    </div>
    建立者:<asp:Label ID="Creator" runat="server" Text="Label"></asp:Label>建立時間<asp:Label ID="CreateTime" runat="server" Text="Label"></asp:Label><br />
    <asp:Button ID="SaveBtn" runat="server" Text="儲存" OnClick="SaveBtn_Click"  OnClientClick="return confirm('確定要新增嗎？');"/><asp:Button ID="CancelBtn" runat="server" Text="取消" OnClick="CancelBtn_Click"  OnClientClick="return confirm('確定要取消嗎？');"/>
    <script>
        $("#AddOrderBtn").onclick = function () {
            $("#Order").modal('show');
        }
        var Total = document.getElementById("<%=this.Total.ClientID%>");
        var Count = document.getElementById("<%=this.Count.ClientID%>");
        var Price = document.getElementById("<%=this.Price.ClientID%>");
        Count.onchange = function () {
            Total.value = Count.value * Price.textContent;
            
        }
        
    </script>
</asp:Content>
