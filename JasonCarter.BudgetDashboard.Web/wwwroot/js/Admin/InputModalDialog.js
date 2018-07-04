var InputModalDialog = function (title, dataSourceId)
{
    this.title = title;
    this.dataSourceId = dataSourceId;
    var baseModal = new BaseModal(this.title, this.message);

    var self = this;

    baseModal.Title = title;

    this.modal = $(self);

    function onChange(e) {
        var selectedRows = this.select();
        var selectedDataItems = [];
        for (var i = 0; i < selectedRows.length; i++) {
            var dataItem = this.dataItem(selectedRows[i]);
            selectedDataItems.push(dataItem);

            var dataItemSelectedEvnet = jQuery.Event("DataItemSelected", dataItem);
            $(self).trigger(dataItemSelectedEvnet);
            baseModal.Modal.modal('hide');
            break;
        }
    }


    var createDateSourceFieldsGrid = function () {
        var div = document.createElement("DIV");
        div.id = "divDataSourceFields"
        baseModal.BaseModalBodyContainer[0].appendChild(div);

        var divDataSourceFields = baseModal.BaseModalBodyContainer.find("#" + "divDataSourceFields");

        var input = {
            'DataSourceId': self.dataSourceId
        }

        new WebServiceProxy().GetDataSourceColumns(input,
            function (result) {
                divDataSourceFields.kendoGrid({
                    dataSource: {
                        data: result,
                    },

                    theme: 'bootstrap',
                    sortable: true,
                    selectable: 'row',
                    scrollable: true,
                    change: onChange,
                    columns: [
                        {
                            field: "name",
                            title: "Field"
                        },
                        {
                            field: "system_type_name",
                            title: "Type"
                        }
                    ]
                }
               );
            },
            function (error) {
            }
        );
    }


    createDateSourceFieldsGrid();


    InputModalDialog.prototype.Show = function () {
        baseModal.Modal.modal();
        baseModal.Modal.modal('show');
    }


}