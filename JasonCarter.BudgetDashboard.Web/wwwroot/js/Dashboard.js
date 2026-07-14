$(document).ready(function () {
    var _utilitiesOverviewData = null;


    var pageLoad = function () {
        _utilitiesOverviewData = model.UtililitesOverview;

        $("#UtilitiesOverview").kendoChart({
            legend: {
                position: "bottom"
            },
            seriesDefaults: {
                type: "line",
                style: "smooth",
            },
            dataSource: {
                data: _utilitiesOverviewData,
                group: { field: 'Year' },
            },
            chartArea: {
                width: '600px',
                height: '400px'
            },
            tooltip: {
                visible: true,
                format: "{0:c2}",
                template: "#= series.name #: #= kendo.toString(value, 'c') #"
            },
            theme: 'bootstrap',
            series: [{ type: 'line', field: 'Amount', categoryField: 'Month' }],
            //seriesClick: getGasSpendingBreakdown,
            categoryAxis: [
                {
                    labels: {
                        rotation: "auto"
                    }
                }
            ]
        });

    }


    pageLoad();
});