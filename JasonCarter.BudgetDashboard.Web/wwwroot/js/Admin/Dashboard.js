$(document).ready(function () {




    loadPage();


    var showDataSourceData = function(data) {
        var columns = [];

        for (var property in data[0]) {
            var column = { field: property, title: property };

            if (Date.parse(data[0][property])) {
                column.type = "date"
                column.format = "{0: MM/dd/yyyy}"
            }
            columns.push(column);
        }

        $("#divDataSourceResults").kendoGrid({
            dataSource:
                {
                    data: data
                },
            theme: 'bootstrap',
            sortable: true,
            columns: columns
        });
    }


    $("#btnExecuteDataSource").on("click", function (e) {
        var input = {
            'DataSourceId': model.DataSource.DataSourceId
        }
        
        new WebServiceProxy().GetDataSourceData(input,
            function (result) {
                showDataSourceData(result);
            },
            function (error) {

            }
        );
    });
});