$(document).ready(function () {

    var transactionsSubset = null;
    var gasTransactions = null;
    
    var loadDebitCreditTotalsChart = function () {
        var input = {
            year: 2018
        }


        new WebServiceProxy().GetDebitCreditTotalsGroupByMonthByYear(input,
            function (data) {
                $("#divDebitCreditTotalsChart").kendoChart({
                    title: {
                        text: "Debit/Credit totals"
                    },
                    legend: {
                        visible: true
                    },
                    seriesDefaults: {
                        type: "bar",
                        labels: {
                            visible: true,
                            format: "{0:c2}",
                            background:"transparent"
                        }
                    },
                    dataSource: {
                        data: data
                    },
                   
                    theme: 'bootstrap',
                    series: [{ type: 'bar', field: 'DebitTotal', name: 'Debit Total', categoryField: 'PeriodText' }, { type: 'bar', field: 'CreditTotal', name: 'Credit Total', categoryField: 'PeriodText' }],
                    valueAxis: {
                        labels: {
                            format: "{0:c2}",
                            rotation: '-45'
                        }
                    }
                });


            },
            function (error) {

            }
        );

    }

    var getGasSpendingBreakdown = function (e) {
        var hey = e;
        var year = e.dataItem.Year;
        var month = e.dataItem.Month;

        var input = {
            year: year,
            month: month
        }

        new WebServiceProxy().GetGasSpendingDetailsByYearAndMonth(input,
            function (data) {
                gasTransactions = data;
                $("#GasTransactionsModal").modal();
            },
            function (error) {

            }
        );
    }

    var loadMonthlyGasSummaryByYear = function () {

        var input = {
            year: 2018
        }

        new WebServiceProxy().GetMonthlyGasSummaryByYear(input,
            function (data) {
                $("#divGasSpendingbyMonth").kendoChart({
                    legend: {
                        visible: true
                    },
                    seriesDefaults: {
                        type: "line",
                        style:"smooth",
                    },
                    dataSource: {
                        data: data
                    },
                    tooltip: {
                        visible: true,
                        format: "{0:c2}",
                        template: "#= series.name #: #= kendo.toString(value, 'c') #"
                    },
                    theme: 'bootstrap',
                    series: [{ type: 'line', field: 'DebitTotal', name: 'Debit Total', categoryField: 'PeriodText' }],
                    valueAxis: {
                        labels: {
                            format: "{0:c2}"
                        }
                    },
                    seriesClick: getGasSpendingBreakdown,
                    categoryAxis: {
                        labels: {
                            rotation: '-45'
                        }
                    }
                });


            },
            function (error) {

            }
        );

    }


    var loadYearlyTransactionSourceSummary = function () {
        var input = {
            year: 2018
        }

        new WebServiceProxy().GetYearlyTransactionSourceSummary(input,
            function (data) {
                $("#divYearlyTransactionSourceSummary").kendoGrid({
                    dataSource: data,
                    theme: 'bootstrap',
                    resizable: true,
                    sortable: true,
                    dataBound: onDataBound,
                    columns: [
                        {
                            field: "Name",
                            title: "Name"
                        },
                        {
                            field: "Transactions",
                            title: "Transactions"
                        },

                        {
                            field: "TransactionType",
                            title: "Transation Type"
                        },
                        {
                            field: "January",
                            title: "January",
                            format: "{0: $##,###.00}"
                        },
                        {
                            field: "February",
                            title: "February",
                            format: "{0: $##,###.00}"
                        },
                        {
                            field: "March",
                            title: "March",
                            format: "{0: $##,###.00}"
                        },
                        {
                            field: "July",
                            title: "July",
                            format: "{0: $##,###.00}"
                        },
                        {
                            field: "August",
                            title: "August",
                            format: "{0: $##,###.00}"
                        },
                        {
                            field: "September",
                            title: "September",
                            format: "{0: $##,###.00}"
                        },
                        {
                            field: "October",
                            title: "October",
                            format: "{0: $##,###.00}"
                        },
                        {
                            field: "November",
                            title: "November",
                            format: "{0: $##,###.00}"
                        },
                        {
                            field: "December",
                            title: "December",
                            format: "{0: $##,###.00}"
                        }
                    ]
                });
            },
            function (error) {

            }
        );
    }

    function onDataBound(e) {
        var grid = $("#divYearlyTransactionSourceSummary").data("kendoGrid");

        var item = grid.data;


        $(grid.tbody).on("mouseover", "td", function (e) {
            var row = $(this).closest("tr");
            var colIdx = $("td", row).index(this);
            var columns = grid.columns;

            if (monthsArray.indexOf(columns[colIdx].field) > 0) {
                $(this).css("cursor", "pointer");
            }
        });


        $(grid.tbody).on("click", "td", function (e) {
            var row = $(this).closest("tr");
            var rowIdx = $("tr", grid.tbody).index(row);
            var colIdx = $("td", row).index(this);

            var item2 = grid.dataItem(row);
            var columns = grid.columns;

            transactionsSubset = null;

            if (monthsArray.indexOf(columns[colIdx].field) >0) {
                var data = {
                    transactionSourceId: item2.TransactionSourceId,
                    transactionTypeId: item2.TransactionTypeId,
                    month: months[columns[colIdx].field],
                    year: 2018
                }


                new WebServiceProxy().GetTransactionsByTransactionSourceIdTransactionTypeIdMonthAndYear(data,
                    function (data) {
                        transactionsSubset = data;
                        $("#myModal").modal();
                    },
                    function (error) {

                    });

                
            }
        });
    }

    loadDebitCreditTotalsChart();


    loadMonthlyGasSummaryByYear();


    loadYearlyTransactionSourceSummary();



    $("#myModal").on('show.bs.modal', function () {
        $("#divTransactionsSubset").kendoGrid({
            dataSource: {
                data: transactionsSubset,
                aggregate: [{
                    field: "date",
                    aggregate: "count"
                },{
                    field: "amount",
                    aggregate: "sum"
                }]
            },
                        
            theme: 'bootstrap',
            sortable: true,
            scrollable: true,
            
            columns: [
                {
                    field: "date",
                    title: "Date",
                    type: "date",
                    format: "{0: MM/dd/yyyy}",
                    footerTemplate: "Total: #=count#"
                },
                {
                    field: "transactionSource.name",
                    title: "Transaction Source"
                },

                {
                    field: "transactionType.name",
                    title: "Transaction Type"
                },
                {
                    field: "amount",
                    title: "Amount",
                    format: "{0: $##,###.00}",
                    footerTemplate: "#=kendo.toString(sum,'c')#"
                },
                {
                    field: "notes",
                    title: "Notes",
                }
            ]
        });
    });

    $("#GasTransactionsModal").on('show.bs.modal', function () {
        $("#divGastTransactions").kendoGrid({
            dataSource: {
                data: gasTransactions,
                aggregate: [{
                    field: "Date",
                    aggregate: "count"
                    },{
                    field: "Amount",
                    aggregate: "sum"
                }]
            },

            theme: 'bootstrap',
            sortable: true,
            scrollable: true,

            columns: [
                {
                    field: "Date",
                    title: "Date",
                    type: "date",
                    format: "{0: MM/dd/yyyy}",
                    footerTemplate: "Total: #=count#"
                },
                {
                    field: "TransactionName",
                    title: "Transaction Source"
                },

                {
                    field: "Amount",
                    title: "Amount",
                    format: "{0: $##,###.00}",
                    footerTemplate: "#=kendo.toString(sum,'c')#"
                },
            ]
        });
    });

    var getTotal = function () {
        var grid = $("#divTransactionsSubset").data("kendoGrid");

        var items = grid.data;



    }


});
