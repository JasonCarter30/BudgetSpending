$(document).ready(function () {

    var data = null;

    var categoryAxisFields = [];
    var seriesFields = [];

    var _widgetConfiguration = null;

    var loadDataSources = function (successCallBack) {
        new WebServiceProxy().GetDataSources(
            function (result) {
                var dataList = new DataList();

                $.each(result, function (index, item) {
                    dataList.DataItems.push(new DataItem(item.DataSourceId, item.Name));
                });

                populateSelectInput($("#selDataSources"), dataList.DataItems)
                successCallBack();
            },
            function (error) {

            });
    }

    var loadWidgetTypes = function () {
        new WebServiceProxy().GetWidgetTypes(
            function (result) {
                var dataList = new DataList();

                $.each(result, function (index, item) {
                    dataList.DataItems.push(new DataItem(item.WidgetTypeId, item.Title));
                });

                populateSelectInput($("#selWidgetTypes"), dataList.DataItems)
            },
            function (error) {

            });
    }

    



    var loadWidget = function (successCallBack) {
        var selectedWidgetTypeId = $("#selWidgetTypes").val();
        var selectedDataSourceId = $("#selDataSources").val();

        var input = {
            widgetTypeId: selectedWidgetTypeId,
            dataSourceId: selectedDataSourceId,
        }

        new WebServiceProxy().GetWidgetConfigurationByWidgetType(input,
            function (result) {
                var chartShellStart = result.chartShellStart;

                _widgetConfiguration = result.configuration;

                $.each(result.configuration, function (key, val) {
                    var newItem = null;
                    newItem = key + " : " + JSON.stringify(val);
                    chartShellStart += newItem + ", "
                });

                chartShellStart = chartShellStart.substring(0, (chartShellStart.length - 2))
                chartShellStart += "})";
                eval(chartShellStart);

                var chart = eval(result.chartObject);
                chart.dataSource.read();
                chart.redraw();
                chart.refresh();
                successCallBack();
            },
            function (error) {

            });

    }

    var loadWidgetTypeEditableConfigurations = function (successCallBack) {
        var selectedWidgetTypeId = $("#selWidgetTypes").val();

        var input = {
            widgetTypeId: selectedWidgetTypeId,
        }

        new WebServiceProxy().GetEditableWidgetConfigurationsByWidgetType(input,
            function (result) {
                var hey = result;

                displayEditableWidgetConfigurations(result, function () {
                    displayDataConfigWidgetConfigurations(result)
                });

                successCallBack();
            },
            function (error) {

            });
    }

    var selectedDataFields = [];
    var displayDataConfigWidgetConfigurations = function (widgetConfigurations) {
        var parentContainer = $("#divWidgetStuff");

        parentContainer.empty();


        $.each(widgetConfigurations, function (index, widgetConfiguration) {
            if (widgetConfiguration.WidgetConfiguration.IsDataConfig) {
                var ctls = [];
                var html = "";

                html += "<div class='panel panel-default'>";
                html += "<div class='panel-heading clearfix'>" + widgetConfiguration.WidgetConfiguration.Name;

                var buttonId = "btnAddWidgetConfiguration" + widgetConfiguration.WidgetConfiguration.Name;

                html += "<button type='button' id='btnAddWidgetConfiguration" + widgetConfiguration.WidgetConfiguration.Name + "'  class='btn btn-default btn-sm pull-right'>"
                html += "<span class='glyphicon glyphicon-plus pull-right' aria-hidden='true'></span>";
                html += "</button>";

                html += "</div>";

                html += " <div class='panel-body'>";

                html += " <div id='divConfiguration" + widgetConfiguration.WidgetConfiguration.Name + "' class='list-group'></div>";


                html += "</div>";
                html += "</div>";

                parentContainer.append(html);

                var btn = parentContainer.find("#" + buttonId);
                var divBody = parentContainer.find("#divConfiguration" + widgetConfiguration.WidgetConfiguration.Name);

                $(btn).on("click", function (e) {
                    var selectedDataSourceId = $("#selDataSources").val();

                    var inputModalDialog = new InputModalDialog("Select field", selectedDataSourceId);

                    inputModalDialog.modal.on("DataItemSelected", function (dataItem) {

                        var span = document.createElement("SPAN");
                        span.className = "list-group-item";
                        span.innerText = dataItem.name;

                        var html = "";
                        html += "<button type='button' id='btnAddWidgetConfiguration" + widgetConfiguration.WidgetConfiguration.Name + "'  class='btn btn-default btn-sm pull-right'>"
                        html += "   <span class='glyphicon glyphicon-plus pull-right' aria-hidden='true'></span>";
                        html += "</button>";

                        divBody.append(span);


                        $.each(dataItem, function (key, item) {
                            if (key === dataItem.name) {
                                var hey = "you";
                            }

                        });

                      

                        var obj = null;

                        switch (widgetConfiguration.WidgetConfiguration.Name) {
                            case "categoryAxis":
                                {
                                    obj = new categoryAxis(dataItem.name);
                                    break;
                                }
                            case "series":
                                {
                                    obj = new series(dataItem.name)
                                    break;
                                }
                        }

                        var dataField = new DataField(widgetConfiguration.WidgetConfiguration.Name, dataItem.name);

                        if (dataItem.name == "Date") {
                            dataField.DataTransformType = "MonthYear";
                            //dataField.DataTransformType = "Year";
                        }

                        selectedDataFields.push(dataField);

                        

                        var input = {
                            dataSourceId: selectedDataSourceId,
                            dataFields: selectedDataFields
                            
                        }

                        new WebServiceProxy().GetDataSourceDataByDataSourceIdAndDataFields(input,
                            function (result) {
                                updateDataSource(result);
                                updateWidgetConfiguration(widgetConfiguration.WidgetConfiguration.Name, obj);
                                //updateKendoWidgetConfiguration(widgetConfiguration.WidgetConfiguration.Name, obj);

                                updateKendoChart();

                                //if (widgetConfiguration.WidgetConfiguration.Name == "series") {
                                //    updateWidgetConfigurationProperty(widgetConfiguration.WidgetConfiguration.Name, "categoryfield", categoryAxisFields[0]);
                                //}

                                //updateChart(widgetConfiguration.WidgetConfiguration.Name);
                                
                            },
                            function (error) {

                            });

                    });

                    inputModalDialog.Show();
                })
            }
        });
    }

    var getWidgetConfigurationArrayObject = function (configuration) {
        var obj = null;

        $.each(_widgetConfiguration, function (key, val) {
            var found = false;
            if (key == configuration) {
                obj = val;
            }
        });

        return obj;
    }

    var displayEditableWidgetConfigurations = function (widgetConfigurations, successCallBack) {
        var parentContainer = $("#divWidgetConfigurations");

        parentContainer.empty();

        $.each(widgetConfigurations, function (index, widgetConfiguration) {
            if (!widgetConfiguration.WidgetConfiguration.IsDataConfig) {
                var ctls = [];
                var html = "";

                html += "<div class='panel panel-default'>";
                html += "<div class='panel-heading'>" + widgetConfiguration.WidgetConfiguration.Name + "</div>";
                html += " <div id='divConfiguration" + widgetConfiguration.WidgetConfiguration.Name + "' class='panel-body'>";

                html += "</div>";
                html += "</div>";

                parentContainer.append(html);

                var divConfigurationContainerId = "#divConfiguration" + widgetConfiguration.WidgetConfiguration.Name;
                var container = parentContainer.find(divConfigurationContainerId)

                if (widgetConfiguration.WidgetConfiguration.WidgetConfigurationProperties.length == 0) {
                    var propertyInputControl = new InputControl(container, widgetConfiguration.WidgetConfiguration.ObjectType.Name, widgetConfiguration.WidgetConfiguration.Name, "");
                    propertyInputControl.BuildInputControl();

                    propertyInputControl.inputEntered = function (value) {
                        updateWidgetConfiguration(widgetConfiguration.WidgetConfiguration.Name, value);

                    }
                }
                else {
                    $.each(widgetConfiguration.WidgetConfiguration.WidgetConfigurationProperties, function (index, prop) {
                        var inputName = widgetConfiguration.WidgetConfiguration.Name + prop.Property.Name;

                        var supportedValuesDataList = getSupportedValuesList(prop.WidgetConfigurationPropertySupportedValues);
                        supportedValuesDataList.DefaultValue = prop.DefaultValue;

                        var propertyInputControl = new InputControl(container, prop.Property.ObjectType.Name, inputName, prop.Property.Name, prop.DefaultValue, supportedValuesDataList);
                        propertyInputControl.BuildInputControl();

                        propertyInputControl.inputEntered = function (value) {
                            updateWidgetConfigurationProperty(widgetConfiguration.WidgetConfiguration.Name, prop.Property.Name, value);
                            updateKendoChart();
                        }
                    });
                }
            }
        });

        successCallBack();
    }

    var getSupportedValuesList = function (supportedValues) {
        var returnValue = [];

        $.each(supportedValues, function (index, item) {
            returnValue.push(new DataItem(item.SupportedValue.SupportedValueId, item.SupportedValue.Name));
        });

        var dataList = new DataList();

        dataList.DefaultValue = supportedValues.DefaultValue;
        dataList.DataItems = returnValue;

        return dataList;
    }

    $("#btnCreateWidget").on("click", function (e) {
        loadWidget(function () {
            loadWidgetTypeEditableConfigurations(function () {
                //loaded
            });
        })

    });


    var updateDataSource = function (ds) {
        var chart = eval("$('#divWidgetCanvas').data('kendoChart')");

        chart.setDataSource(ds);
    }

    var updateKendoChart = function () {
        var chart = eval("$('#divWidgetCanvas').data('kendoChart')");

        $.each(_widgetConfiguration, function (key, val) {
            var obj = getWidgetConfigurationArrayObject(key);

            if (key != "dataSource") {
                var cmd = "chart.setOptions({" + key + " : _widgetConfiguration." + key + " });";
                eval(cmd);
            }

            //if (obj != null) {
            //    chart.options[key] = obj;
            //}

            
        });

        //chart.refresh();
    }


    var updateKendoWidgetConfiguration = function (configuration, value) {
        var chart = eval("$('#divWidgetCanvas').data('kendoChart')");

        $.each(chart.options, function (key, val) {
            var found = false;
            if (key == configuration) {

                if (Array.isArray(chart.options[key])) {
                    var objArray = getWidgetConfigurationArrayObject(configuration);
                    //chart.options[key].push(value);
                    chart.options[key] = objArray;
                }
                else {
                    chart.options[key] = getWidgetConfigurationArrayObject(configuration);
                }

                chart.refresh();
            }
        });
    }


    var updateChart = function (configurationName) {
        var chart = eval("$('#divWidgetCanvas').data('kendoChart')");

        $.each(chart.options, function (key, val) {
            if (key == configurationName) {
                var cmd = "chart.setOptions({" + configurationName + " : _widgetConfiguration." + configurationName + " });";
                eval(cmd);
            }
        });
    }

    var updateWidgetConfiguration = function (configuration, value) {
        $.each(_widgetConfiguration, function (key, val) {
            var found = false;
            if (key == configuration) {
                var found = false;

                if (Array.isArray(val)) {


                    //ExistsInArrayByField(val, propertyName, value);

                    val.push(value);

                    //if (val.length > 0) {
                    //    for (var i = 0; i < val.length; i++) {
                    //        $.each(val[i], function (subKey, subVal) {
                    //            if (subKey == propertyName) {
                    //                val[i][subKey] = value;
                    //                found = true;
                    //            }
                    //        });

                    //        if (!found) {
                    //            val[i][propertyName] = value
                    //        }
                    //    }
                    //}
                    //else {
                    //    //var obj = {};
                    //    //obj[propertyName] = value;

                    //    val.push(value);
                    //}


                }
                else {
                    $.each(val, function (subKey, subVal) {
                        if (subKey == propertyName) {
                            val[subKey] = value;
                            found = true;
                        }
                    });

                    if (!found) {
                        val[propertyName] = value
                    }
                }
            }
        });
    }



    var updateWidgetConfigurationProperty = function (configuration, propertyName, value) {
        $.each(_widgetConfiguration, function (key, val) {
            var found = false;
            if (key == configuration) {
                var found = false;

                if (Array.isArray(val)) {


                    ExistsInArrayByField(val, propertyName, value);

                    if (val.length > 0) {
                        for (var i = 0; i < val.length; i++) {
                            $.each(val[i], function (subKey, subVal) {
                                if (subKey == propertyName) {
                                    val[i][subKey] = value;
                                    found = true;
                                }
                            });

                            if (!found) {
                                val[i][propertyName] = value
                            }
                        }
                    }
                    else {
                        var obj = {};
                        obj[propertyName] = value;

                        val.push(obj);
                    }


                }
                else {
                    $.each(val, function (subKey, subVal) {
                        if (subKey == propertyName) {
                            val[subKey] = value;
                            found = true;
                        }
                    });

                    if (!found) {
                        val[propertyName] = value
                    }
                }
            }
        });
    }


    var loadPage = function () {
        loadDataSources(function () {
            loadWidgetTypes();
        });

        
    }

    loadPage();
});