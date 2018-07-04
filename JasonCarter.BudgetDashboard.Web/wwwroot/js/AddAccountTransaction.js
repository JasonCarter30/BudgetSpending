$(document).ready(function () {


    $('#datetimepicker1').datetimepicker({
        defaultDate: new Date(),
        format: 'MM/DD/YYYY h:mm:ss A'
    });

    $('#datetimepicker1').on('changeDate', function (ev) {
        $('#datetimepicker1').datetimepicker('hide');
    });

    var selectedTransactionSource = null;

    $('#txtTransactionSource').typeahead({
        source: function (request, response) {
            new WebServiceProxy().GetTransactionSourcesByLookupValue(request,
                function (result) {
                    var items = [];
                    response($.map(result, function (item) {
                        items.push(item);
                    }));
                    response(items);


                    // SET THE WIDTH AND HEIGHT OF UI AS "auto" ALONG WITH FONT.
                    // YOU CAN CUSTOMIZE ITS PROPERTIES.
                    $(".dropdown-menu").css("width", "auto");
                    $(".dropdown-menu").css("height", "auto");
                    $(".dropdown-menu").css("font", "15px Verdana");

                }, function (error) {
                    displayLookupMatterModalErrorMessage("There was an error searching for matter.");
                });

        },
        updater: function (selection) {
            console.log("You selected: " + selection);
            selectedTransactionSource = selection;
        },
        afterSelect: function (item) {
            $('#txtTransactionSource').val(selectedTransactionSource.name);
            $('#txtTransactionSource').attr("data-id", selectedTransactionSource.transactionSourceId);
        },
        displayText: function (item) {
            var result = item.name;
            return result;
        },
        hint: true,             // SHOW HINT (DEFAULT IS "true").
        highlight: true,        // HIGHLIGHT (SET <strong> or <b> BOLD). DEFAULT IS "true".
        minLength: 2,            // MINIMUM 1 CHARACTER TO START WITH.
        items: 'all'
    });



    $("#btnSaveAccountTransaction").on("click", function (e) {
        var payloadData = {
            Date: getInputValue($("#txtDate")[0]),
            TransactionTypeId: getInputValue($("#selTransactionType")[0]),
            Amount: getInputValue($("#txtAmount")[0]),
            Note: getInputValue($("#txtNote")[0]),
            TransactionSourceId: $("#txtTransactionSource").attr("data-id"),
            TransactionSourceName: getInputValue($("#txtTransactionSource")[0])
        };

        new WebServiceProxy().InsertAccountTransaction(payloadData,
            function (data) {
                window.location.href = model.BaseURL; 
            },
            function (error) {

            }
        );
    });


    var clearErrorMessage = function () {
        $("#divAccountTransactionErrorMessage").css("display", "none");
    }


    clearErrorMessage();
});