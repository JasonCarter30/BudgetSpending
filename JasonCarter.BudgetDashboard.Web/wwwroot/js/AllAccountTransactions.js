$(document).ready(function () {
    var selectedTransactionType = 1;
   
    var filterTransactionsByTransactionType = function (transactionTypeId) {
        var result = model.AccountTransactions.filter(function (item) {
            if (item.TransactionType.TransactionTypeId == transactionTypeId) {
                return item;
            }
        })

        return result;
    }


    $("#lstTransactionType").on("change", function (e) {
        selectedTransactionType = getInputValue($("#lstTransactionType")[0]);

        var result = filterTransactionsByTransactionType(selectedTransactionType);

        $("#divGridAccountTransactions").data("kendoGrid").dataSource.data(result);
    })


    var loadAllTransactions = function () {

        var transactions = filterTransactionsByTransactionType(selectedTransactionType);


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
                    field: "Date",
                    title: "Date",
                    type: "date",
                    format: "{0: MM/dd/yyyy}"
                },
                {
                    field: "TransactionSource.Name",
                    title: "Transaction Source"
                },
                {
                    field: "Debit",
                    title: "Debit",
                    format: "{0: $##,###.00}",
                    footerTemplate: "#=kendo.toString(sum,'c')#"
                },
                {
                    field: "Credit",
                    title: "Credit",
                    format: "{0: $##,###.00}",
                    footerTemplate: "#=kendo.toString(sum,'c')#"
                },

                {
                    field: "Notes",
                    title: "Notes",
                }
            ]
        });
    }


    loadAllTransactions();
});