$(document).ready(function () {
    var selectedAccountTransactionId;
    
    var saveAccountTransaction = function () {
        var payloadData = {
            AccountTransactionId: selectedAccountTransactionId,
            Date: getInputValue($("#txtDate")[0]),
            TransactionTypeId: getInputValue($("#lstTransactionTypes")[0]),
            Amount: getInputValue($("#txtAmount")[0]),
            Note: getInputValue($("#txtNotes")[0]),
            TransactionSourceId: $("#txtTransactionSource").attr("data-id"),
            TransactionSourceName: getInputValue($("#txtTransactionSource")[0])
        };

        new WebServiceProxy().UpdateAccountTransaction(payloadData,
            function (data) {
               
            },
            function (error) {

            }
        );

    }

    var loadAccountTransaction = function () {
        var input = {
            accountTransactionId: selectedAccountTransactionId
        }

        new WebServiceProxy().GetAccountTransactionByAccountTransactionId(input,
            function (result) {
                setInputValue($("#txtDate")[0], result.Date);
                setInputValue($("#txtAmount")[0], result.Amount);
                setInputValue($("#txtTransactionSource")[0], result.TransactionSource.Name);
                $("#txtTransactionSource").attr("data-id", result.TransactionSource.TransactionSourceId)
                setInputValue($("#txtNotes")[0], result.Notes);

            },
            function (error) {

            });
    }


    $("#btnSaveAccountTransaction").on("click", function(e) {
        saveAccountTransaction();
        $("#myModal").modal('hide');
        loadCurrentMonthTransactions();    
    })

    $("#myModal").on('show.bs.modal', function () {
        selectedAccountTransactionId = $(event.target).closest('span').data('id');
        loadAccountTransaction();
    });


  

    var loadCurrentMonthTransactions = function () {
        $("#divGridAccountTransactions").kendoGrid({
            dataSource:
            {
                data: model.Data,
                aggregate: [{
                    field: "Date",
                    aggregate: "count"
                },
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
                    format: "{0: MM/dd/yyyy}",
                    footerTemplate: "Total: #=count#"
                },
                {
                    field: "TransactionSource",
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
                    title: "Notes"
                },
                {
                    template: "<span name='btnEditAccountTransaction' onmouseover='$(this).css(&quot;cursor&quot;,&quot;pointer&quot;)' class='glyphicon glyphicon-edit' data-id='#:AccountTransactionId#' data-toggle='modal' data-target='\\#myModal'></span>",
                    title: "",
                    attributes: { class: 'text-center' }
                }
            ]
        });
    }


    loadCurrentMonthTransactions();
});