$(document).ready(function () {
    var selectedTransactionType = 1;
   
    var filterTransactionsByTransactionType = function (transactionTypeId) {
        let result = null;


        if (model.accountTransactions) { 
            result = model.accountTransactions.filter(function (item) {
                if (item.transactionType.transactionTypeId == transactionTypeId) {
                    return item;
                }
            });
        }

        return result;
    }


    $("#lstTransactionType").on("change", function (e) {
        selectedTransactionType = getInputValue($("#lstTransactionType")[0]);

        var result = filterTransactionsByTransactionType(selectedTransactionType);

        $("#divGridAccountTransactions").data("kendoGrid").dataSource.data(result);
    })


    var loadAllTransactions = function () {

        let transactions = filterTransactionsByTransactionType(selectedTransactionType);


        $("#divGridAccountTransactions").kendoGrid({
            dataSource: {
                data: transactions,
                aggregate: [
                    {
                        field: "Debit",
                        aggregate: "sum"
                    },
                    {
                        field: "Credit",
                        aggregate: "sum"
                    }
                ]
            },
            theme: 'bootstrap',
            sortable: true,
            groupable: true,
            columns: [
                {
                    field: "date",
                    title: "Date",
                    type: "date",
                    format: "{0: MM/dd/yyyy}"
                },
                {
                    field: "transactionSource.name",
                    title: "Transaction Source"
                },
                {
                    field: "debit",
                    title: "Debit",
                    format: "{0: $##,###.00}",
                    //footerTemplate: "#=kendo.toString(sum,'c')#"
                },
                {
                    field: "credit",
                    title: "Credit",
                    format: "{0: $##,###.00}",
                    //footerTemplate: "#=kendo.toString(sum,'c')#"
                },

                {
                    field: "notes",
                    title: "Notes",
                }
            ]
        });
    }


    loadAllTransactions();
});