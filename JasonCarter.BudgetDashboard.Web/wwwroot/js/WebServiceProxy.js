

var WebServiceProxy = function () {
    var getAppConfigValue = function (keySearch) {
        var result = null;


        $.each(model.AppConfiguration.Items, function (key, value) {
            if (value.Name === keySearch) {
                result = value.Value;
            }
        });

        return result;
    }


    

    var insertAccountTransactionURL = model.BaseURL + getAppConfigValue("InsertAccountTransactionURL");
        var updateAccountTransactionURL = model.BaseURL + getAppConfigValue("UpdateAccountTransactionURL");


    var getTransactionSourcesByLookupValueURL = model.BaseURL + getAppConfigValue("GetTransactionSourcesURL");
    var getDebitCreditTotalsGroupByMonthURL = model.BaseURL + getAppConfigValue("GetDebitCreditTotalsGroupByMonthURL");
    var getYearlyTransactionSourceSummaryURL = model.BaseURL + getAppConfigValue("GetYearlyTransactionSourceSummaryURL");

    var getTransactionsByTransactionSourceIdTransactionTypeIdMonthYearURL = model.BaseURL + getAppConfigValue("GetTransactionsByTransactionSourceIdTransactionTypeIdMonthYearURL");


    var getDataSourcesURL = model.BaseURL + getAppConfigValue("GetDataSourcesURL");
    var getDashboardsURL = model.BaseURL + getAppConfigValue("GetDashboardsURL");
    var getWidgetTypesURL = model.BaseURL + getAppConfigValue("GetWidgetTypesURL");
    var getDataSourceDataURL = model.BaseURL + getAppConfigValue("GetDataSourceDataURL");
    var getDataSourceColumnsURL = model.BaseURL + getAppConfigValue("GetDataSourceColumnsURL");
    var getEditableWidgetConfigurationsByWidgetTypeURL = model.BaseURL + getAppConfigValue("GetEditableWidgetConfigurationsByWidgetTypeURL");


    var getWidgetConfigurationByWidgetTypeURL = model.BaseURL + getAppConfigValue("GetWidgetConfigurationByWidgetTypeURL");

    
    
    var getMonthlyGasSummaryByYearURL = model.BaseURL + getAppConfigValue("GetMonthlyGasSummaryByYearURL");

    var getGasSpendingDetailsByYearAndMonthURL = model.BaseURL + getAppConfigValue("GetGasSpendingDetailsByYearAndMonthURL");

    var getDataSourceDataByDataSourceIdAndDataFieldsURL = model.BaseURL + getAppConfigValue("GetDataSourceDataByDataSourceIdAndDataFieldsURL");

    
    var getAccountTransactionByAccountTransactionIdURL = getAppConfigValue("GetAccountTransactionByAccountTransactionId");

    var logErrorURL = model.BaseURL + "/Error/LogError";

  


    this.GetAccountTransactionByAccountTransactionId = function (input, successCallback, errorCallback) {
        var webProxy = this;

        $.ajax({
            type: "GET",
            async: false,
            cache:false,
            data: input,
            contentType: 'application/json',
            dataType: 'json',
            url: getAccountTransactionByAccountTransactionIdURL
        }).done(function (result) {
            successCallback(result);
        }).fail(function (error) {
            webProxy.LogError("GetDashboardMatterByMatterId: " + error.status + " " + error.statusText + ".  URL: " + getAccountTransactionByAccountTransactionIdURL);
            errorCallback(error);
        });
    }




    this.GetTransactionSourcesByLookupValue = function (lookupValue, successCallback, errorCallback) {
        var input = { lookupValue: lookupValue };

        var webProxy = this;

        $.ajax({
            type: "GET",
            async: false,
            data: input,
            url: getTransactionSourcesByLookupValueURL
        }).done(function (result) {
            successCallback(result);
        }).fail(function (error) {
            webProxy.LogError("GetDashboardMatterByMatterId: " + error.status + " " + error.statusText + ".  URL: " + getDashboardMatterByMatterIdURL);
            errorCallback(error);
        });
    }


    this.GetDataSources = function (successCallback, errorCallback) {
        var webProxy = this;

        $.ajax({
            type: "GET",
            async: true,
            url: getDataSourcesURL
        }).done(function (result) {
            successCallback(result);
        }).fail(function (error) {
            webProxy.LogError("GetDataSources: " + error.status + " " + error.statusText + ".  URL: " + getDataSourcesURL);
            errorCallback(error);
        });
    }

    this.GetEditableWidgetConfigurationsByWidgetType = function (input, successCallback, errorCallback) {
        var webProxy = this;

        $.ajax({
            type: "GET",
            data: input,
            async: true,
            url: getEditableWidgetConfigurationsByWidgetTypeURL
        }).done(function (result) {
            successCallback(result);
        }).fail(function (error) {
            webProxy.LogError("GetEditableWidgetConfigurationsByWidgetType: " + error.status + " " + error.statusText + ".  URL: " + getEditableWidgetConfigurationsByWidgetTypeURL);
            errorCallback(error);
        });
    }



    this.GetWidgetConfigurationByWidgetType = function (input, successCallback, errorCallback) {
        var webProxy = this;

        $.ajax({
            data: input,
            type: "GET",
            async: true,
            url: getWidgetConfigurationByWidgetTypeURL
        }).done(function (result) {
            successCallback(result);
        }).fail(function (error) {
            webProxy.LogError("GetWidgetConfigurationByWidgetType: " + error.status + " " + error.statusText + ".  URL: " + getWidgetConfigurationByWidgetTypeURL);
            errorCallback(error);
        });
    }


    this.GetWidgetTypes = function (successCallback, errorCallback) {
        var webProxy = this;

        $.ajax({
            type: "GET",
            async: true,
            url: getWidgetTypesURL
        }).done(function (result) {
            successCallback(result);
        }).fail(function (error) {
            webProxy.LogError("GetDataSources: " + error.status + " " + error.statusText + ".  URL: " + getWidgetTypesURL);
            errorCallback(error);
        });
    }


    
    


    this.GetDashboards = function (successCallback, errorCallback) {
        var webProxy = this;

        $.ajax({
            type: "GET",
            async: true,
            url: getDashboardsURL
        }).done(function (result) {
            successCallback(result);
        }).fail(function (error) {
            webProxy.LogError("GetDataSources: " + error.status + " " + error.statusText + ".  URL: " + getDashboardsURL);
            errorCallback(error);
        });
    }



    this.GetDataSourceColumns = function (input, successCallback, errorCallback) {
        var webProxy = this;

        $.ajax({
            type: "GET",
            async: true,
            data: input,
            url: getDataSourceColumnsURL
        }).done(function (result) {
            successCallback(result);
        }).fail(function (error) {
            webProxy.LogError("getDataSourceColumns: " + error.status + " " + error.statusText + ".  URL: " + getDataSourceColumnsURL);
            errorCallback(error);
        });
    }



    this.GetDataSourceDataByDataSourceIdAndDataFields = function (input, successCallback, errorCallback) {
        var webProxy = this;

        $.ajax({
            type: "POST",
            async: true,
            data: input,
            url: getDataSourceDataByDataSourceIdAndDataFieldsURL
        }).done(function (result) {
            successCallback(result);
        }).fail(function (error) {
            webProxy.LogError("getDataSourceDataByDataSourceIdAndDataFieldsURL: " + error.status + " " + error.statusText + ".  URL: " + getDataSourceDataByDataSourceIdAndDataFieldsURL);
            errorCallback(error);
        });
    }




    this.GetDataSourceData = function (input, successCallback, errorCallback) {
        var webProxy = this;

        $.ajax({
            type: "GET",
            async: true,
            data: input,
            url: getDataSourceDataURL
        }).done(function (result) {
            successCallback(result);
        }).fail(function (error) {
            webProxy.LogError("GetDataSources: " + error.status + " " + error.statusText + ".  URL: " + getDataSourcesURL);
            errorCallback(error);
        });
    }




    this.GetYearlyTransactionSourceSummary = function(input, successCallback, errorCallback) {
        var webProxy = this;

        $.ajax({
            type: "GET",
            data: input,
            async: true,
            url: getYearlyTransactionSourceSummaryURL
        }).done(function (result) {
            successCallback(result);
        }).fail(function (error) {
            webProxy.LogError("GetDashboardMattersBySearchValue: " + error.status + " " + error.statusText + ".  URL: " + getDashboardMatterByMatterIdURL);
            errorCallback(error);
        });
    }
    
    this.GetTransactionsByTransactionSourceIdTransactionTypeIdMonthAndYear = function (input, successCallback, errorCallback) {
        var webProxy = this;

        var data = input;
        
        $.ajax({
            type: "GET",
            async: false,
            data:data,
            url: getTransactionsByTransactionSourceIdTransactionTypeIdMonthYearURL
        }).done(function (result) {
            successCallback(result);
        }).fail(function (error) {
            webProxy.LogError("GetDashboardMattersBySearchValue: " + error.status + " " + error.statusText + ".  URL: " + getDashboardMatterByMatterIdURL);
            errorCallback(error);
        });
    }

    this.GetMonthlyGasSummaryByYear = function (input, successCallback, errorCallback) {
        var webProxy = this;

        var data = input;

        $.ajax({
            type: "GET",
            async: false,
            data: data,
            url: getMonthlyGasSummaryByYearURL
        }).done(function (result) {
            successCallback(result);
        }).fail(function (error) {
            webProxy.LogError("GetDashboardMattersBySearchValue: " + error.status + " " + error.statusText + ".  URL: " + getMonthlyGasSummaryByYearURL);
            errorCallback(error);
        });
    }

    this.GetGasSpendingDetailsByYearAndMonth = function (input, successCallback, errorCallback) {
        var webProxy = this;

        var data = input;

        $.ajax({
            type: "GET",
            async: false,
            cache: false,
            data: data,
            url: getGasSpendingDetailsByYearAndMonthURL
        }).done(function (result) {
            successCallback(result);
        }).fail(function (error) {
            webProxy.LogError("GetGasSpendingDetailsByYearAndMonth: " + error.status + " " + error.statusText + ".  URL: " + getGasSpendingDetailsByYearAndMonthURL);
            errorCallback(error);
        });
    }



    this.GetTransactionSourcesBySearchValue = function (lookupValue, successCallback, errorCallback) {
        var input = { lookupValue: lookupValue };

        var webProxy = this;

        $.ajax({
            type: "GET",
            async: false,
            data: input,
            url: getDashboardMattersBySearchValueURL
        }).success(function (result) {
            successCallback(result);
        }).error(function (error) {
            webProxy.LogError("GetDashboardMattersBySearchValue: " + error.status + " " + error.statusText + ".  URL: " + getDashboardMatterByMatterIdURL);
            errorCallback(error);
        });
    }


    
    this.GetDebitCreditTotalsGroupByMonthByYear = function (input, successCallback, errorCallback) {
        var webProxy = this;

        $.ajax({
            type: "GET",
            data:input,
            async: true,
            url: getDebitCreditTotalsGroupByMonthURL
        }).done(function (result) {
            successCallback(result);
        }).fail(function (error) {
            webProxy.LogError("GetDashboardMattersBySearchValue: " + error.status + " " + error.statusText + ".  URL: " + getDashboardMatterByMatterIdURL);
            errorCallback(error);
        });
    }

    



    this.UpdateAccountTransaction = function (payloadData, successCallback, errorCallback) {
        var webProxy = this;

        $(this).css('cursor', 'progress');

        $.ajax({
            type: "POST",
            async: false,
            data: payloadData,
            url: updateAccountTransactionURL
        }).done(function (result) {
            successCallback(result);
            $(this).css('cursor', 'pointer');
        }).fail(function (error) {
            $(this).css('cursor', 'pointer');
            webProxy.LogError("InsertReport: " + error.status + " " + error.statusText + ".  URL: " + insertAccountTransactionURL);
            errorCallback(error);
        });
    }



    this.InsertAccountTransaction = function (payloadData, successCallback, errorCallback) {
        var webProxy = this;

        $(this).css('cursor', 'progress');

        $.ajax({
            type: "POST",
            async: false,
            data: payloadData,
            url: insertAccountTransactionURL
        }).done(function (result) {
            successCallback(result);
            $(this).css('cursor', 'pointer');
        }).fail(function (error) {
            $(this).css('cursor', 'pointer');
            webProxy.LogError("InsertReport: " + error.status + " " + error.statusText + ".  URL: " + insertAccountTransactionURL);
            errorCallback(error);
        });
    }


    this.LogError = function (errorMessage) {
        var data = {
            error: errorMessage
        }

        $.ajax({
            type: "POST",
            async: false,
            data: data,
            url: logErrorURL
        }).success(function (result) {

            //successCallback(result);
        }).error(function (error) {
            //errorCallback(error);
        });
    }


    this.DeactivateReport = function (reportId, reportSubTypeId, successCallback, errorCallback) {
        var webProxy = this;

        var input = {
            reportId: reportId,
            reportSubTypeId: reportSubTypeId
        };

        $.ajax({
            type: "POST",
            async: false,
            data: input,
            url: deactivateReportURL
        }).success(function (result) {
            successCallback(result);
        }).error(function (error) {
            webProxy.LogError("DeactivateReport: " + error.status + " " + error.statusText + ".  URL: " + deactivateReportURL);
            errorCallback(error);
        });
    }



    this.DeactivateReportCase = function (reportId, reportSubTypeId, matterId, successCallback, errorCallback) {
        var webProxy = this;

        var input = {
            reportId: reportId,
            reportSubTypeId: reportSubTypeId,
            matterId: matterId
        };

        $.ajax({
            type: "POST",
            async: false,
            data: input,
            url: deactivateReportCaseURL
        }).success(function (result) {
            successCallback(result);
        }).error(function (error) {
            webProxy.LogError("DeactivateReportCase: " + error.status + " " + error.statusText + ".  URL: " + deactivateReportCaseURL);
            errorCallback(error);
        });
    }


    this.InsertReportCaseMatter = function (payloadData, successCallback, errorCallback) {
        var webProxy = this;

        $.ajax({
            type: "POST",
            async: false,
            data: payloadData,
            url: insertReportCaseMatterURL
        }).success(function (result) {
            successCallback(result);
        }).error(function (error) {
            webProxy.LogError("InsertReportCaseMatter: " + error.status + " " + error.statusText + ".  URL: " + insertReportCaseMatterURL);
            errorCallback(error);
        });
    }


    this.LoadDashboardMattersIntoCache = function (successCallback, errorCallback) {
        var webProxy = this;

        $.ajax({
            type: "POST",
            async: true,
            url: loadDashboardMattersIntoCacheURL
        }).success(function (result) {
            successCallback(result);
        }).error(function (error) {
            webProxy.LogError("LoadDashboardMattersIntoCache: " + error.status + " " + error.statusText + ".  URL: " + loadDashboardMattersIntoCacheURL);
            errorCallback(error);
        });
    }


    this.UpdateReport = function (payloadData, successCallback, errorCallback) {
        var webProxy = this;

        $(this).css('cursor', 'pointer');

        $.ajax({
            type: "POST",
            async: false,
            data: payloadData,
            url: updateReportURL
        }).success(function (result) {
            $(this).css('cursor', 'auto');
            successCallback(result);
        }).error(function (error) {
            $(this).css('cursor', 'auto');
            webProxy.LogError("UpdateReport: " + error.status + " " + error.statusText + ".  URL: " + updateReportURL);
            errorCallback(error);
        });
    }

    this.UpdateReportCase = function (payloadData, successCallback, errorCallback) {
        var webProxy = this;

        $(this).css('cursor', 'progress');

        $.ajax({
            type: "POST",
            //async: false,
            data: payloadData,
            url: updateReportCaseURL
        }).success(function (result) {
            successCallback(result);
            $(this).css('cursor', 'auto');
        }).error(function (error) {
            $(this).css('cursor', 'auto');
            webProxy.LogError("UpdateReportCase: " + error.status + " " + error.statusText + ".  URL: " + updateReportCaseURL);
            errorCallback(error);
        });
    }
}

