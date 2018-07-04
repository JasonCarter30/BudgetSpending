$(document).ready(function () {


    var loadDashboards = function () {
        new WebServiceProxy().GetDashboards(
            function (result) {
                loadGrid(result);
            },
            function (error) {

        });
    }


    var loadGrid = function (data) {
        $("#divGridDashboards").kendoGrid({
            dataSource:
                {
                    data: data
                },
            theme: 'bootstrap',
            sortable: true,
            columns: [
                {
                    template: "<a href='/Admin/Dashboard?DashboardId=#=DashboardId#'>#=Name#</a>",
                    title: "Name",
                    attributes: { class: 'text-center' }

                },
                {
                    field: "Title",
                    title: "Title"
                },
                {
                    field: "CreateDate",
                    title: "Create Date"
                }

            ]
        });
    }


    loadDashboards();
});