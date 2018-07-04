$(document).ready(function () {


    var loadDataSources = function () {
        new WebServiceProxy().GetDataSources(
            function (result) {
                loadGrid(result);
            },
            function (error) {

        });
    }


    var loadGrid = function (data) {
        $("#divGridDataSources").kendoGrid({
            dataSource:
                {
                    data: data
                },
            theme: 'bootstrap',
            sortable: true,
            columns: [
                {
                    template: "<a href='/Admin/DataSource?DataSourceId=#=DataSourceId#'>#=Name#</a>",
                    title: "Name",
                    attributes: { class: 'text-center' }

                },
                {
                    field: "CommandText",
                    title: "CommandText"
                }
            ]
        });
    }


    loadDataSources();
});