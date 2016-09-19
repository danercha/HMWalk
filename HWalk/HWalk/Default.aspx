<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HWalk._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script src="Scripts/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.datepicker').datepicker({
                pickTime: false,
                format: 'mm/dd/yyyy',
                startDate: '-3d',
                todayHighlight: true,
                autoclose: true
            });

            $('.datepicker').datepicker('update', new Date());

            $("#tblWalkers tbody tr").on("click", function () {
                var $row = $(this).closest("tr"),
                    $tds = $row.find("td:nth-child(1)"),
                    $name = $row.find("td:nth-child(2)");


                var rex = new RegExp("hi", 'i');
                var _name = "";

                $.each($tds, function () {
                    var a = $(this).text().trim();
                    $("input[id=MainContent_hdnCurrentId]").val(a);
                    rex = new RegExp(a, 'i');
                });

                $.each($name, function () {

                    $("#snapfor").text($(this).text().trim());
                });


                $('.searchable tr').hide();
                $('.searchable tr').filter(function () {
                    return rex.test($(this).text());
                }).show();
            });

        });
    </script>
    <div class="jumbotron">
        <h1>Hitchiner Walks</h1>

        <table class="table table-hover" id="tblWalkers">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Name</th>
                    <th>Total Miles</th>
                    <th>View</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="rptWalker">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="hdnId">W<%# Eval("Walker_Id") %></asp:Label>

                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblName" CssClass="label label-default"><%# Eval("Name") %></asp:Label>
                            </td>
                            <td>

                                <asp:Label runat="server" ID="Label1" CssClass="label label-default"><%# Eval("Milage") %></asp:Label></td>
                            <td>
                                <button type="button" class="btn btn-info btn-sm modalfocus" id="modalbutton" data-toggle="modal" data-target="#myModal"><span class=""></span>View</button>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
        <asp:HiddenField runat="server" ID="hdnCurrentId" />
    </div>

    <div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Details for <span id="snapfor">Dan</span></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <table class="table table-hover">
                            <thead>
                                <tr>
                                    <th></th>
                                    <th>Date</th>
                                    <th>Milage</th>
                                </tr>
                            </thead>
                            <tbody class="searchable">
                                <asp:Repeater runat="server" ID="rptMileDetails">
                                    <ItemTemplate>
                                        <tr>
                                            <td>W<%# Eval("Walker_Id") %></td>
                                            <td><%# Eval("Mileage_Date", "{0:d}") %></td>
                                            <td><%# Eval("Mileage") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                    <div class="row">
                        <br />
                        <br />
                        <table class="table">
                            <tr>
                                <td>
                                    <label for="tbDate">Date</label>
                                    <div class="input-group date">
                                        <div class="input-group date">
                                            <asp:TextBox runat="server" ID="tbDate" placeholder="Date" CssClass="datepicker form-control"></asp:TextBox>
                                            <div class="input-group-addon">
                                                <span class="glyphicon glyphicon-th"></span>
                                            </div>
                                        </div>
                                    </div>

                                </td>
                                <td>
                                    <label for="tbNewMilage">Milage</label>
                                    <asp:TextBox runat="server" ID="tbNewMilage" CssClass="form-control" placeholder="Milage"></asp:TextBox>

                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="row">
                        <br />
                        <br />
                        <p style="text-align: center;">
                            <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn btn-primary btn-sm closeform" OnClick="btnSave_Click" />
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--<div class="row">
        <div class="col-md-4">
            <h2>Getting started</h2>
            <p>
                ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Get more libraries</h2>
            <p>
                NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Web Hosting</h2>
            <p>
                You can easily find a web hosting company that offers the right mix of features and price for your applications.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
            </p>
        </div>
    </div>--%>
</asp:Content>
