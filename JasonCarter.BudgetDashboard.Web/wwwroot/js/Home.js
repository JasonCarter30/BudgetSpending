$(document).ready(function (){

    $("#divGridAccountTransactions").kendoGrid({
        dataSource: model.AccountTransactions,
        theme: 'bootstrap',
        sortable: true,
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
                field: "TransationType.Name",
                title: "Transation Type"
            },
            {
                field: "Amount",
                title: "Amount",
                format: "{0: $##,###.00}"
            },
            {
                field: "Notes",
                field: "Notes",
            }
        ]
    });

});